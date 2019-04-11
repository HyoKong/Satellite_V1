using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;
using Microsoft.VisualBasic;
using MathWorks.MATLAB.NET.Arrays;
using MathWorks.MATLAB.NET.Utility;
using lybplus;
using satellite;
using TEST;

namespace SatelliteSoftwareIF
{
    public partial class Form1 : Form
    {
        private Queue<double> dataQueue_prob = new Queue<double>(100);      //监测故障概率
        private Queue<double> dataQueue_data = new Queue<double>(100);     //监测数据
        private Queue<string> timeQueue = new Queue<string>(100);          //横坐标时间
        private Queue<double> dataQueue_prob_ST = new Queue<double>(100);   //选择时间段内的故障概率
        private Queue<double> dataQueue_data_ST = new Queue<double>(100);  //选择时间段内监测数据
       
        private int curValue = 0;

        private string GuzhangText = "";
        
        private int dll_c1 = 0;
        private int dll_c2 = 0;
        private int dll_c4 = 0;
        private int dll_c5 = 0;
        private int dll_t1 = 0;
        private int dll_t2 = 0;
        private int dll_t3 = 0;
        private int dll_t4 = 0;
        private int dll_t5 = 0;
        private int dll_t6 = 0;
        public string qushi_flag = "动量轮1马达电流";
        public string tcy_qushi_flag = "探测仪东西电机A相电流";
        public string fsj_qushi_flag = "辐射计东西总电流";
        public string dy_qushi_flag = "28V母线电压";

        private int ST_flag = 0; //时间段选择标识
        private int diag_btn_judge = 0;//判断故障诊断按钮有没有被点击
        private int ST_start = 0;//开始在chart中显示标识
        private int ST_end = 0;//停止在chart中显示标识

        bool overscan = false; //全视角未开启
        bool follow = false;  //细节追踪未开启
        bool zoom = false; //移动功能未开启

        private int num = 5;//每次删除增加几个点

        private int read_first_num = 100;//读取txt文件最先开始读的行数
        private int read_first_dy_num = 300;//电源文件最开始读取的行数

        bool flag1 = false; //chart1是否启用局部放大,FALSE--未启用
        bool flag2 = false; //chart2是否启用局部放大,FALSE--未启用

        //声明年月日时分的全局变量
        public string dll_nianyueri_start_ST = "";
        public string dll_shifen_start_ST = "";
        public string dll_nianyueri_end_ST = "";
        public string dll_shifen_end_ST = "";

        //存储读取txt文件的变量
        public string[] dll_file = getFileName("./读取多个文件实验/动量轮");
        public string[] fsj_file = getFileName("./读取多个文件实验/辐射计");
        public string[] tcy_file = getFileName("./读取多个文件实验/探测仪");
        public string[] dy_file = getFileName("./读取多个文件实验/电源");

        public Form1()
        {
            InitializeComponent();
            this.ControlBox = false;//
            this.WindowState = FormWindowState.Maximized;
            //string llll = "569285316552485";
            //Console.WriteLine(TimeStampToDateTime(llll));
            //################################chart1###############################
            chart1.Series[0].ChartType = SeriesChartType.Line;
            chart1.Series[0].IsValueShownAsLabel = false;//图上不显示数据点的值
            chart1.Series[0].ToolTip = "#VALX,#VALY";//鼠标停留在数据点上，显示XY值
            chart1.Series[0].Label = "#VAL";

            //启用X游标，以支持局部区域选择放大
            chart1.ChartAreas[0].CursorY.IsUserEnabled = true;
            chart1.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;
            chart1.ChartAreas[0].CursorY.LineColor = Color.Pink;
            chart1.ChartAreas[0].CursorY.IntervalType = DateTimeIntervalType.Auto;
            chart1.ChartAreas[0].AxisY.ScaleView.Zoomable = false;
            chart1.ChartAreas[0].AxisY.ScrollBar.ButtonStyle = ScrollBarButtonStyles.All;//启用X轴滚动条按钮
            
            comboBox1.Items.AddRange(new object[] { "OverView", "Follow" });
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.SelectedIndex = 0;
            //################################chart2#################################
            chart2.Series[0].ChartType = SeriesChartType.Line;
            chart2.Series[0].IsValueShownAsLabel = false;//图上不显示数据点的值
            chart2.Series[0].ToolTip = "#VALX,#VALY";//鼠标停留在数据点上，显示XY值

            //启用X游标，以支持局部区域选择放大
            chart2.ChartAreas[0].CursorY.IsUserEnabled = true;
            chart2.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;
            chart2.ChartAreas[0].CursorY.LineColor = Color.Pink;
            chart2.ChartAreas[0].CursorY.IntervalType = DateTimeIntervalType.Auto;
            chart2.ChartAreas[0].AxisY.ScaleView.Zoomable = false;
            chart2.ChartAreas[0].AxisY.ScrollBar.ButtonStyle = ScrollBarButtonStyles.All;//启用X轴滚动条按钮

            comboBox2.Items.AddRange(new object[] { "OverView", "Follow" });
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.SelectedIndex = 0;
            //界面打开就开始运行程序
            InitChart();
            this.timer1.Start();
        }

        private void btn_abnstate_cle_Click(object sender, EventArgs e)
        {
            //ReadTxtToLst(abn_state_lstbox,"E:\\工作\\教研室资料\\卫星项目\\Abalone.txt");
            //ReadTxtToLst(abn_state_lstbox, "E:\\工作\\教研室资料\\卫星项目\\动量轮\\2017_12_17动量轮.txt");
            abn_state_lstbox.Items.Clear();
            //
            label_dll.Text = string.Format("正常");
            label_dll.ForeColor = System.Drawing.Color.Lime;
            //
            label_tcy.Text = string.Format("正常");
            label_tcy.ForeColor = System.Drawing.Color.Lime;
            //
            label_fsj.Text = string.Format("正常");
            label_fsj.ForeColor = System.Drawing.Color.Lime;
            //
            label_dy.Text = string.Format("正常");
            label_dy.ForeColor = System.Drawing.Color.Lime;
        }

        /// <summary>
        /// 初始化图表
        /// </summary>
        private void InitChart()
        {
            //##############################chart1##############################################
            //定义图表区域
            this.chart1.ChartAreas.Clear();
            ChartArea chartArea1 = new ChartArea("C1");
            this.chart1.ChartAreas.Add(chartArea1);
            //定义存储和显示点的容器
            this.chart1.Series.Clear();
            Series series1 = new Series("故障概率曲线");
            //Series series_yuzhi = new Series("故障警戒线");
            series1.ChartArea = "C1";
            //series_yuzhi.ChartArea = "C1";
            this.chart1.Series.Add(series1);
            //this.chart1.Series.Add(series_yuzhi);
            
            
            //设置图表显示样式
            this.chart1.ChartAreas[0].AxisY.Minimum = 0;
            this.chart1.ChartAreas[0].AxisY.Maximum = 1;
            this.chart1.ChartAreas[0].AxisY.LabelStyle.Format = "0%";
            this.chart1.ChartAreas[0].AxisX.Interval = 5;
            this.chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.Silver;
            this.chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.Silver;
            this.chart1.ChartAreas[0].AxisX.Title = "采样点";
            this.chart1.ChartAreas[0].AxisX.TitleForeColor = System.Drawing.Color.Red;
            this.chart1.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.chart1.ChartAreas[0].AxisY.Title = "故障概率";
            this.chart1.ChartAreas[0].AxisY.TitleForeColor = System.Drawing.Color.Red;
            this.chart1.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            //设置标题
            this.chart1.Titles.Clear();
            this.chart1.Titles.Add("S01");
            this.chart1.Titles[0].Text = "动量轮故障监测结果显示";
            this.chart1.Titles[0].ForeColor = Color.RoyalBlue;
            this.chart1.Titles[0].Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            
            //设置图表显示样式
            this.chart1.Series[0].Color = Color.Red;
            this.chart1.Series[0].Points.Clear();
            //this.chart1.ChartAreas[0].AxisX.Title.Text = "shijian";
            //this.chart1.Series[1].ChartType = SeriesChartType.Line;
            //this.chart1.Series[1].Color = Color.Red;

            //启用X游标，以支持局部区域选择放大
            this.chart1.ChartAreas[0].CursorX.IsUserEnabled = true;
            this.chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            this.chart1.ChartAreas[0].CursorX.LineColor = Color.Pink;
            this.chart1.ChartAreas[0].CursorX.IntervalType = DateTimeIntervalType.Auto;
            this.chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = false;
            this.chart1.ChartAreas[0].AxisX.ScrollBar.ButtonStyle = ScrollBarButtonStyles.All;//启用X轴滚动条按钮
    
            //##############################chart2##############################################
            //定义图表区域
            this.chart2.ChartAreas.Clear();
            ChartArea chartArea2 = new ChartArea("C2");
            this.chart2.ChartAreas.Add(chartArea2);
            //定义存储和显示点的容器
            this.chart2.Series.Clear();
            Series series2 = new Series(string.Format("{0}", 动量轮1马达电流ToolStripMenuItem));
            //Series series_max = new Series("阈值上限");
            //Series series_min = new Series("阈值下限");
            series2.ChartArea = "C2";
            //series_max.ChartArea = "C2";
            //series_min.ChartArea = "C2";
            this.chart2.Series.Add(series2);
            //this.chart2.Series.Add(series_max);
            //this.chart2.Series.Add(series_min);

            //设置图表显示样式
            //this.chart2.ChartAreas[0].AxisY.Minimum = 0;
            //this.chart2.ChartAreas[0].AxisY.Maximum = 0.4;
            this.chart2.ChartAreas[0].AxisX.Interval = 5;
            this.chart2.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.Silver;
            this.chart2.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.Silver;
            this.chart2.ChartAreas[0].AxisX.Title = "采样点";
            this.chart2.ChartAreas[0].AxisX.TitleForeColor = System.Drawing.Color.Blue;
            this.chart2.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.chart2.ChartAreas[0].AxisY.Title = string.Format("{0}/A", 动量轮1马达电流ToolStripMenuItem);
            this.chart2.ChartAreas[0].AxisY.TitleForeColor = System.Drawing.Color.Blue;
            this.chart2.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            //this.chart2.ChartAreas[0].AxisX.IsMarginVisible = false;//横坐标显示完整
            //设置标题
            this.chart2.Titles.Clear();
            this.chart2.Titles.Add("S02");
            this.chart2.Titles[0].Text = "动量轮1马达电流";
            this.chart2.Titles[0].ForeColor = Color.RoyalBlue;
            this.chart2.Titles[0].Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            //设置图表显示样式
            this.chart2.Series[0].Color = Color.Red;
            if (dll_c1 == 1)
            {
                this.chart2.Titles[0].Text = string.Format("{0}", 动量轮1马达电流ToolStripMenuItem);
                this.chart2.Series[0].ChartType = SeriesChartType.Line;
            }
            this.chart2.Series[0].Points.Clear();
            //this.chart2.Series[1].ChartType = SeriesChartType.Line;
            //this.chart2.Series[2].ChartType = SeriesChartType.Line;
            //this.chart2.Series[1].Color = Color.Lime;
            //this.chart2.Series[2].Color = Color.Lime;
            //this.chart2.Series[1].BorderWidth = 2;
            //this.chart2.Series[2].BorderWidth = 2;

            //启用X游标，以支持局部区域选择放大
            this.chart2.ChartAreas[0].CursorX.IsUserEnabled = true;
            this.chart2.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            this.chart2.ChartAreas[0].CursorX.LineColor = Color.Pink;
            this.chart2.ChartAreas[0].CursorX.IntervalType = DateTimeIntervalType.Auto;
            this.chart2.ChartAreas[0].AxisX.ScaleView.Zoomable = false;
            this.chart2.ChartAreas[0].AxisY.ScrollBar.ButtonStyle = ScrollBarButtonStyles.All;//启用X轴滚动条按钮
        }

        /// <summary>
        /// 初始化事件
        /// 开始事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>


        private void timer1_Tick(object sender, EventArgs e)
        {
            //###########################  四个部件同时开始诊断   #############################
            //read_dll();
            //read_fsj();
            //read_tcy();
            //read_dy();
            //###########################  四个部件同时开始诊断   #############################

            //chart1.Series[0].Label = "#VAL";
            //chart2.Series[0].Label = "#VAL";
        
            UpdateQueueValue();
            this.chart1.Series[0].Points.Clear();
            for (int i = 0; i < dataQueue_prob.Count; i++)
            {
                //this.chart1.Series[0].Points.AddXY((i + 1), dataQueue_prob.ElementAt(i));
                this.chart1.Series[0].Points.AddXY(timeQueue.ElementAt(i), dataQueue_prob.ElementAt(i));
                //this.chart1.Series[0].Color = Color.Red;
                //this.chart1.Series[1].Points.AddXY(timeQueue.ElementAt(i),0.5);
            }
            //####################chart2################################
            this.chart2.Series[0].Points.Clear();
            for (int i = 0; i < dataQueue_data.Count; i++)
            {
                //this.chart2.Series[0].Points.AddXY((i + 1), dataQueue_data.ElementAt(i));
                this.chart2.Series[0].Points.AddXY(timeQueue.ElementAt(i), dataQueue_data.ElementAt(i));
                this.chart2.Series[0].Color = Color.Blue;
                //this.chart2.Series[1].Points.AddXY(timeQueue.ElementAt(i), 2.0);
                //this.chart2.Series[2].Points.AddXY(timeQueue.ElementAt(i), -2.0);
            }

            //chart1启用局部放大
            if (flag1)
            {
                //this.textBox1.Text = chart1.ChartAreas[0].AxisX.ScaleView.Size.ToString();
                //this.textBox2.Text = chart1.ChartAreas[0].AxisX.ScaleView.Position.ToString();
                return;
            }

            //未启动局部放大##########chart1############
            if (comboBox1.SelectedItem.ToString() == "OverView")
            {
                chart1.ChartAreas[0].AxisX.ScaleView.Position = 1;
                if (dataQueue_prob.Count > 10)
                {
                    double max = chart1.ChartAreas[0].AxisX.Maximum;
                    max = (dataQueue_prob.Count / 10 + 1) * 10;
                    chart1.ChartAreas[0].AxisX.Interval = max / 10;
                }
                chart1.ChartAreas[0].AxisX.ScaleView.Size = dataQueue_prob.Count * 1.1;
            }
            if (comboBox1.SelectedItem.ToString() == "Follow")
            {
                chart1.ChartAreas[0].AxisX.Interval = 1D;
                chart1.ChartAreas[0].AxisX.ScaleView.Size = 8D;
                if (dataQueue_prob.Count <= chart1.ChartAreas[0].AxisX.ScaleView.Size)
                    chart1.ChartAreas[0].AxisX.ScaleView.Position = 1;
                else
                    chart1.ChartAreas[0].AxisX.ScaleView.Position = dataQueue_prob.Count - chart1.ChartAreas[0].AxisX.ScaleView.Size;
            }
            //chart2启用局部放大
            if (flag2)
            {
                //this.textBox1.Text = chart1.ChartAreas[0].AxisX.ScaleView.Size.ToString();
                //this.textBox2.Text = chart1.ChartAreas[0].AxisX.ScaleView.Position.ToString();
                return;
            }
            //未启动局部放大##########chart2############
            if (comboBox2.SelectedItem.ToString() == "OverView")
            {
                chart2.ChartAreas[0].AxisX.ScaleView.Position = 1;
                if (dataQueue_data.Count > 10)
                {
                    double max = chart2.ChartAreas[0].AxisX.Maximum;
                    max = (dataQueue_data.Count / 10 + 1) * 10;
                    chart2.ChartAreas[0].AxisX.Interval = max / 10;
                }
                chart2.ChartAreas[0].AxisX.ScaleView.Size = dataQueue_data.Count * 1.1;
            }
            if (comboBox2.SelectedItem.ToString() == "Follow")
            {
                chart2.ChartAreas[0].AxisX.Interval = 1D;
                chart2.ChartAreas[0].AxisX.ScaleView.Size = 8D;
                if (dataQueue_data.Count <= chart2.ChartAreas[0].AxisX.ScaleView.Size)
                    chart2.ChartAreas[0].AxisX.ScaleView.Position = 1;
                else
                    chart2.ChartAreas[0].AxisX.ScaleView.Position = dataQueue_data.Count - chart2.ChartAreas[0].AxisX.ScaleView.Size;
            }
        }

        //更新队列中的值
        private void UpdateQueueValue()
        {

            if (dataQueue_prob.Count > 100)
            {
                //先出列
                for (int i = 0; i < num; i++)
                {
                    dataQueue_prob.Dequeue();
                }
            }
            if (dataQueue_data.Count > 100)
            {
                //先出列
                for (int i = 0; i < num; i++)
                {
                    dataQueue_data.Dequeue();
                }
            }
            if (timeQueue.Count > 100) 
            {
                for (int i = 0; i < num; i++) 
                {
                    timeQueue.Dequeue();
                }
            }

            //动量轮故障监测按钮按下后的数值更新
            if (dll_rb.Checked)
            {
                for (int t = 0; t < dll_file.Length; t++)
                {
                
                    //using (StreamReader reader = new StreamReader("./读取多个文件实验/动量轮//2017_12_17动量轮首.txt"))
                    //using (StreamReader reader = new StreamReader("./2017_12_18动量轮.txt"))
                    //FileStream afile = new FileStream("./读取多个文件实验/动量轮/" + dll_file[t],FileMode.Open);
                    using (StreamReader reader = new StreamReader("./读取多个文件实验/动量轮/" + dll_file[t]))
                    {

                        //Console.WriteLine(reader.EndOfStream);
                        //Console.WriteLine(f);
                        string line = reader.ReadLine();
                   
                        int i = 0;
        
                        while ((line = reader.ReadLine()) != null)
                        {

                            //line = reader.ReadLine();

                            if (i >= read_first_num && i <= num + read_first_num)
                            {
                                Console.WriteLine(reader.EndOfStream);

                                //end_flag = 1;
                                string[] line_sp = line.Split('\t');
                                double[] value_nums = new double[10];
                                value_nums[0] = FindFaultData(2.0,-2.0, double.Parse(line_sp[1]));
                                value_nums[1] = FindFaultData(2.0, -2.0, double.Parse(line_sp[2]));
                                value_nums[2] = FindFaultData(2.0, -2.0, double.Parse(line_sp[4]));
                                value_nums[3] = FindFaultData(2.0, -2.0, double.Parse(line_sp[5]));
                                value_nums[4] = FindFaultData(45.0, -5.0, double.Parse(line_sp[7]));
                                value_nums[5] = FindFaultData(45.0, -5.0, double.Parse(line_sp[8]));
                                value_nums[6] = FindFaultData(45.0, -5.0, double.Parse(line_sp[9]));
                                value_nums[7] = FindFaultData(45.0, -5.0, double.Parse(line_sp[10]));
                                value_nums[8] = FindFaultData(45.0, -5.0, double.Parse(line_sp[11]));
                                value_nums[9] = FindFaultData(45.0, -5.0, double.Parse(line_sp[12]));
                                //读取时间和PLSR计算结果
                                label_shijian.Text = string.Format("{0}", line_sp[0]);

                                string time_X = line_sp[0];
                                time_X.Replace(" ", "");
                                timeQueue.Enqueue(time_X.Substring(9, 8));

                                double a = TEST.Class1.test_dll(value_nums);
                                dataQueue_prob.Enqueue(Math.Round(a, 4));
                                //读取“动量轮1马达电流”的数据并判断是否故障
                                if (qushi_flag == "动量轮1马达电流")
                                {
                                    dataQueue_data.Enqueue(Math.Round(value_nums[0], 1));//读取动量轮某一列数据
                                    this.chart2.ChartAreas[0].AxisY.Title = string.Format("{0}/A", 动量轮1马达电流ToolStripMenuItem);
                                    this.chart2.Titles[0].Text = "动量轮1马达电流 (-2A,2A)";
                                    this.chart2.Series[0].LegendText = "动量轮1马达电流";

                                    if (a >= 0.5)
                                    {
                                        label_dll.Text = string.Format("故障");
                                        label_dll.ForeColor = System.Drawing.Color.Red;
                                        if (value_nums[0] >= 2.0 || value_nums[0] <= -2.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮1马达电流", a);
                                        }
                                        else if (value_nums[1] >= 2.0 || value_nums[1] <= -2.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮2马达电流", a);
                                        }
                                        else if (value_nums[2] >= 2.0 || value_nums[2] <= -2.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮4马达电流", a);
                                        }
                                        else if (value_nums[3] >= 2.0 || value_nums[3] <= -2.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮5马达电流", a);
                                        }
                                        else if (value_nums[4] >= 45.0 || value_nums[4] <= -5.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮1轴温", a);
                                        }
                                        else if (value_nums[5] >= 45.0 || value_nums[5] <= -5.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮2轴温", a);
                                        }
                                        else if (value_nums[6] >= 45.0 || value_nums[8] <= -5.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮3轴温", a);
                                        }
                                        else if (value_nums[7] >= 45.0 || value_nums[7] <= -5.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮4轴温", a);
                                        }
                                        else if (value_nums[8] >= 45.0 || value_nums[8] <= -5.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮5轴温", a);
                                        }
                                        else if (value_nums[9] >= 45.0 || value_nums[9] <= -5.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮6轴温", a);
                                        }
                                    }
                                }
                                //读取“动量轮2马达电流”的数据并判断是否故障
                                else if (qushi_flag == "动量轮2马达电流")
                                {
                                    dataQueue_data.Enqueue(value_nums[1]);//读取动量轮某一列数据
                                    this.chart2.ChartAreas[0].AxisY.Title = string.Format("{0}/A", 动量轮2马达电流ToolStripMenuItem);
                                    this.chart2.Titles[0].Text = "动量轮2马达电流 (-2A,2A)";
                                    this.chart2.Series[0].LegendText = "动量轮2马达电流";

                                    if (a >= 0.5)
                                    {
                                        label_dll.Text = string.Format("故障");
                                        label_dll.ForeColor = System.Drawing.Color.Red;
                                        if (value_nums[0] >= 2.0 || value_nums[0] <= -2.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮1马达电流", a);
                                        }
                                        else if (value_nums[1] >= 2.0 || value_nums[1] <= -2.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮2马达电流", a);
                                        }
                                        else if (value_nums[2] >= 2.0 || value_nums[2] <= -2.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮4马达电流", a);
                                        }
                                        else if (value_nums[3] >= 2.0 || value_nums[3] <= -2.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮5马达电流", a);
                                        }
                                        else if (value_nums[4] >= 45.0 || value_nums[4] <= -5.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮1轴温", a);
                                        }
                                        else if (value_nums[5] >= 45.0 || value_nums[5] <= -5.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮2轴温", a);
                                        }
                                        else if (value_nums[6] >= 45.0 || value_nums[8] <= -5.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮3轴温", a);
                                        }
                                        else if (value_nums[7] >= 45.0 || value_nums[7] <= -5.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮4轴温", a);
                                        }
                                        else if (value_nums[8] >= 45.0 || value_nums[8] <= -5.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮5轴温", a);
                                        }
                                        else if (value_nums[9] >= 45.0 || value_nums[9] <= -5.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮6轴温", a);
                                        }
                                    }
                                }
                                //读取“动量轮4马达电流”的数据并判断是否故障
                                else if (qushi_flag == "动量轮4马达电流")
                                {
                                    dataQueue_data.Enqueue(value_nums[2]);//读取动量轮某一列数据
                                    this.chart2.ChartAreas[0].AxisY.Title = string.Format("{0}/A", 动量轮4马达电流ToolStripMenuItem);
                                    this.chart2.Titles[0].Text = "动量轮4马达电流 (-2A,2A)";
                                    this.chart2.Series[0].LegendText = "动量轮4马达电流";

                                    if (a >= 0.5)
                                    {
                                        label_dll.Text = string.Format("故障");
                                        label_dll.ForeColor = System.Drawing.Color.Red;
                                        if (value_nums[0] >= 2.0 || value_nums[0] <= -2.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮1马达电流", a);
                                        }
                                        else if (value_nums[1] >= 2.0 || value_nums[1] <= -2.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮2马达电流", a);
                                        }
                                        else if (value_nums[2] >= 2.0 || value_nums[2] <= -2.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮4马达电流", a);
                                        }
                                        else if (value_nums[3] >= 2.0 || value_nums[3] <= -2.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮5马达电流", a);
                                        }
                                        else if (value_nums[4] >= 45.0 || value_nums[4] <= -5.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮1轴温", a);
                                        }
                                        else if (value_nums[5] >= 45.0 || value_nums[5] <= -5.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮2轴温", a);
                                        }
                                        else if (value_nums[6] >= 45.0 || value_nums[8] <= -5.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮3轴温", a);
                                        }
                                        else if (value_nums[7] >= 45.0 || value_nums[7] <= -5.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮4轴温", a);
                                        }
                                        else if (value_nums[8] >= 45.0 || value_nums[8] <= -5.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮5轴温", a);
                                        }
                                        else if (value_nums[9] >= 45.0 || value_nums[9] <= -5.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮6轴温", a);
                                        }
                                    }
                                }
                                //读取“动量轮1轴温”的数据并判断是否故障
                                else if (qushi_flag == "动量轮1轴温")
                                {
                                    dataQueue_data.Enqueue(value_nums[4]);//读取动量轮某一列数据
                                    this.chart2.ChartAreas[0].AxisY.Title = string.Format("{0}/度", 动量轮1轴温ToolStripMenuItem);
                                    this.chart2.Titles[0].Text = "动量轮1轴温 (-5°C,45°C)";
                                    this.chart2.Series[0].LegendText = "动量轮1轴温";

                                    if (a >= 0.5)
                                    {
                                        label_dll.Text = string.Format("故障");
                                        label_dll.ForeColor = System.Drawing.Color.Red;
                                        if (value_nums[0] >= 2.0 || value_nums[0] <= -2.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮1马达电流", a);
                                        }
                                        else if (value_nums[1] >= 2.0 || value_nums[1] <= -2.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮2马达电流", a);
                                        }
                                        else if (value_nums[2] >= 2.0 || value_nums[2] <= -2.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮4马达电流", a);
                                        }
                                        else if (value_nums[3] >= 2.0 || value_nums[3] <= -2.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮5马达电流", a);
                                        }
                                        else if (value_nums[4] >= 45.0 || value_nums[4] <= -5.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮1轴温", a);
                                        }
                                        else if (value_nums[5] >= 45.0 || value_nums[5] <= -5.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮2轴温", a);
                                        }
                                        else if (value_nums[6] >= 45.0 || value_nums[8] <= -5.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮3轴温", a);
                                        }
                                        else if (value_nums[7] >= 45.0 || value_nums[7] <= -5.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮4轴温", a);
                                        }
                                        else if (value_nums[8] >= 45.0 || value_nums[8] <= -5.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮5轴温", a);
                                        }
                                        else if (value_nums[9] >= 45.0 || value_nums[9] <= -5.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮6轴温", a);
                                        }
                                    }
                                }
                                //读取“动量轮2轴温”的数据并判断是否故障
                                else if (qushi_flag == "动量轮2轴温")
                                {
                                    dataQueue_data.Enqueue(value_nums[5]);//读取动量轮某一列数据
                                    this.chart2.ChartAreas[0].AxisY.Title = string.Format("{0}/A", 动量轮2轴温ToolStripMenuItem);
                                    this.chart2.Titles[0].Text = "动量轮2轴温 (-5°C,45°C)";
                                    this.chart2.Series[0].LegendText = "动量轮2轴温";

                                    if (a >= 0.5)
                                    {
                                        label_dll.Text = string.Format("故障");
                                        label_dll.ForeColor = System.Drawing.Color.Red;
                                        if (value_nums[0] >= 2.0 || value_nums[0] <= -2.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮1马达电流", a);
                                        }
                                        else if (value_nums[1] >= 2.0 || value_nums[1] <= -2.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮2马达电流", a);
                                        }
                                        else if (value_nums[2] >= 2.0 || value_nums[2] <= -2.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮4马达电流", a);
                                        }
                                        else if (value_nums[3] >= 2.0 || value_nums[3] <= -2.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮5马达电流", a);
                                        }
                                        else if (value_nums[4] >= 45.0 || value_nums[4] <= -5.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮1轴温", a);
                                        }
                                        else if (value_nums[5] >= 45.0 || value_nums[5] <= -5.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮2轴温", a);
                                        }
                                        else if (value_nums[6] >= 45.0 || value_nums[8] <= -5.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮3轴温", a);
                                        }
                                        else if (value_nums[7] >= 45.0 || value_nums[7] <= -5.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮4轴温", a);
                                        }
                                        else if (value_nums[8] >= 45.0 || value_nums[8] <= -5.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮5轴温", a);
                                        }
                                        else if (value_nums[9] >= 45.0 || value_nums[9] <= -5.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮6轴温", a);
                                        }
                                    }
                                }
                                //读取“动量轮3轴温”的数据并判断是否故障
                                else if (qushi_flag == "动量轮3轴温")
                                {
                                    dataQueue_data.Enqueue(value_nums[6]);//读取动量轮某一列数据
                                    this.chart2.ChartAreas[0].AxisY.Title = string.Format("{0}/度", 动量轮3轴温ToolStripMenuItem);
                                    this.chart2.Titles[0].Text = "动量轮3轴温 (-5°C,45°C)";
                                    this.chart2.Series[0].LegendText = "动量轮3轴温";

                                    if (a >= 0.5)
                                    {
                                        label_dll.Text = string.Format("故障");
                                        label_dll.ForeColor = System.Drawing.Color.Red;
                                        if (value_nums[0] >= 2.0 || value_nums[0] <= -2.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮1马达电流", a);
                                        }
                                        else if (value_nums[1] >= 2.0 || value_nums[1] <= -2.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮2马达电流", a);
                                        }
                                        else if (value_nums[2] >= 2.0 || value_nums[2] <= -2.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮4马达电流", a);
                                        }
                                        else if (value_nums[3] >= 2.0 || value_nums[3] <= -2.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮5马达电流", a);
                                        }
                                        else if (value_nums[4] >= 45.0 || value_nums[4] <= -5.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮1轴温", a);
                                        }
                                        else if (value_nums[5] >= 45.0 || value_nums[5] <= -5.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮2轴温", a);
                                        }
                                        else if (value_nums[6] >= 45.0 || value_nums[8] <= -5.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮3轴温", a);
                                        }
                                        else if (value_nums[7] >= 45.0 || value_nums[7] <= -5.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮4轴温", a);
                                        }
                                        else if (value_nums[8] >= 45.0 || value_nums[8] <= -5.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮5轴温", a);
                                        }
                                        else if (value_nums[9] >= 45.0 || value_nums[9] <= -5.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮6轴温", a);
                                        }
                                    }
                                }
                                //读取“动量轮4轴温”的数据并判断是否故障
                                else if (qushi_flag == "动量轮4轴温")
                                {
                                    dataQueue_data.Enqueue(value_nums[7]);//读取动量轮某一列数据
                                    this.chart2.ChartAreas[0].AxisY.Title = string.Format("{0}/度", 动量轮4轴温ToolStripMenuItem);
                                    this.chart2.Titles[0].Text = "动量轮4轴温 (-5°C,45°C)";
                                    this.chart2.Series[0].LegendText = "动量轮4轴温";

                                    if (a >= 0.5)
                                    {
                                        label_dll.Text = string.Format("故障");
                                        label_dll.ForeColor = System.Drawing.Color.Red;
                                        if (value_nums[0] >= 2.0 || value_nums[0] <= -2.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮1马达电流", a);
                                        }
                                        else if (value_nums[1] >= 2.0 || value_nums[1] <= -2.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮2马达电流", a);
                                        }
                                        else if (value_nums[2] >= 2.0 || value_nums[2] <= -2.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮4马达电流", a);
                                        }
                                        else if (value_nums[3] >= 2.0 || value_nums[3] <= -2.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮5马达电流", a);
                                        }
                                        else if (value_nums[4] >= 45.0 || value_nums[4] <= -5.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮1轴温", a);
                                        }
                                        else if (value_nums[5] >= 45.0 || value_nums[5] <= -5.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮2轴温", a);
                                        }
                                        else if (value_nums[6] >= 45.0 || value_nums[8] <= -5.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮3轴温", a);
                                        }
                                        else if (value_nums[7] >= 45.0 || value_nums[7] <= -5.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮4轴温", a);
                                        }
                                        else if (value_nums[8] >= 45.0 || value_nums[8] <= -5.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮5轴温", a);
                                        }
                                        else if (value_nums[9] >= 45.0 || value_nums[9] <= -5.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮6轴温", a);
                                        }
                                    }
                                }
                                //读取“动量轮5轴温”的数据并判断是否故障
                                else if (qushi_flag == "动量轮5轴温")
                                {
                                    dataQueue_data.Enqueue(value_nums[8]);//读取动量轮某一列数据
                                    this.chart2.ChartAreas[0].AxisY.Title = string.Format("{0}/度", 动量轮5轴温ToolStripMenuItem);
                                    this.chart2.Titles[0].Text = "动量轮5轴温 (-5°C,45°C)";
                                    this.chart2.Series[0].LegendText = "动量轮5轴温";

                                    if (a >= 0.5)
                                    {
                                        label_dll.Text = string.Format("故障");
                                        label_dll.ForeColor = System.Drawing.Color.Red;
                                        if (value_nums[0] >= 2.0 || value_nums[0] <= -2.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮1马达电流", a);
                                        }
                                        else if (value_nums[1] >= 2.0 || value_nums[1] <= -2.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮2马达电流", a);
                                        }
                                        else if (value_nums[2] >= 2.0 || value_nums[2] <= -2.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮4马达电流", a);
                                        }
                                        else if (value_nums[3] >= 2.0 || value_nums[3] <= -2.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮5马达电流", a);
                                        }
                                        else if (value_nums[4] >= 45.0 || value_nums[4] <= -5.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮1轴温", a);
                                        }
                                        else if (value_nums[5] >= 45.0 || value_nums[5] <= -5.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮2轴温", a);
                                        }
                                        else if (value_nums[6] >= 45.0 || value_nums[8] <= -5.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮3轴温", a);
                                        }
                                        else if (value_nums[7] >= 45.0 || value_nums[7] <= -5.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮4轴温", a);
                                        }
                                        else if (value_nums[8] >= 45.0 || value_nums[8] <= -5.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮5轴温", a);
                                        }
                                        else if (value_nums[9] >= 45.0 || value_nums[9] <= -5.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮6轴温", a);
                                        }
                                    }
                                }
                                //读取“动量轮6轴温”的数据并判断是否故障
                                else if (qushi_flag == "动量轮6轴温")
                                {
                                    dataQueue_data.Enqueue(value_nums[9]);//读取动量轮某一列数据
                                    this.chart2.ChartAreas[0].AxisY.Title = string.Format("{0}/度", 动量轮6轴温ToolStripMenuItem);
                                    this.chart2.Titles[0].Text = "动量轮6轴温 (-5°C,45°C)";
                                    this.chart2.Series[0].LegendText = "动量轮6轴温";

                                    if (a >= 0.5)
                                    {
                                        label_dll.Text = string.Format("故障");
                                        label_dll.ForeColor = System.Drawing.Color.Red;
                                        if (value_nums[0] >= 2.0 || value_nums[0] <= -2.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮1马达电流", a);
                                        }
                                        else if (value_nums[1] >= 2.0 || value_nums[1] <= -2.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮2马达电流", a);
                                        }
                                        else if (value_nums[2] >= 2.0 || value_nums[2] <= -2.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮4马达电流", a);
                                        }
                                        else if (value_nums[3] >= 2.0 || value_nums[3] <= -2.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮5马达电流", a);
                                        }
                                        else if (value_nums[4] >= 45.0 || value_nums[4] <= -5.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮1轴温", a);
                                        }
                                        else if (value_nums[5] >= 45.0 || value_nums[5] <= -5.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮2轴温", a);
                                        }
                                        else if (value_nums[6] >= 45.0 || value_nums[8] <= -5.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮3轴温", a);
                                        }
                                        else if (value_nums[7] >= 45.0 || value_nums[7] <= -5.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮4轴温", a);
                                        }
                                        else if (value_nums[8] >= 45.0 || value_nums[8] <= -5.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮5轴温", a);
                                        }
                                        else if (value_nums[9] >= 45.0 || value_nums[9] <= -5.0)
                                        {
                                            SaveListboxToTxt(line_sp[0], "动量轮6轴温", a);
                                        }
                                    }

                                }
                                
                            }
                            line = reader.ReadLine();

                            i += 1;
                        }
                        read_first_num += 5;

                    }
                
                }   
            
            }

            //辐射计故障监测按钮按下后的数值更新
            if (fsj_rb.Checked)
            {
                //qushi_flag = "辐射计东西总电流";
                //chart1的改变
                this.chart1.Titles[0].Text = "辐射计故障监测结果显示";
                //chart2的改变
                this.chart2.Titles[0].Text = "辐射计东西总电流";
                this.chart2.ChartAreas[0].AxisY.Title = string.Format("{0}/A", 辐射计东西总电流ToolStripMenuItem);
                for (int t = 0; t < fsj_file.Length; t++)
                {
                    using (StreamReader reader = new StreamReader("./读取多个文件实验/辐射计/" + fsj_file[t]))
                    {
                        int i = 0;
                        string line = reader.ReadLine();
                        while ((line = reader.ReadLine()) != null)
                        {
                            if (i >= read_first_num && i < num + read_first_num)
                            {
                                string[] line_sp = line.Split('\t');
                                double[] value_nums = new double[2];
                                value_nums[0] = FindFaultData(2.0,-2.0, double.Parse(line_sp[3]));
                                value_nums[1] = FindFaultData(2.0, -2.0, double.Parse(line_sp[6]));
                                label_shijian.Text = string.Format("{0}", TimeStampToDateTime(line_sp[0]));
                                string time_X = string.Format("{0}", TimeStampToDateTime(line_sp[0]));
                                timeQueue.Enqueue(time_X.Replace("/", "").Substring(8, 5));
                                double a = TEST.Class1.test_fsj(value_nums);
                                dataQueue_prob.Enqueue(a);
                                //读取“辐射计东西总电流”的数据并判断是否故障
                                if (fsj_qushi_flag == "辐射计东西总电流")
                                {
                                    dataQueue_data.Enqueue(value_nums[0]);//读取动量轮某一列数据
                                    this.chart2.ChartAreas[0].AxisY.Title = string.Format("{0}/A", 辐射计东西总电流ToolStripMenuItem);
                                    this.chart2.Titles[0].Text = "辐射计东西总电流 (-2A,2A)";
                                    this.chart2.Series[0].LegendText = "辐射计东西总电流";
                                    if (a >= 0.5) 
                                    {
                                        label_fsj.Text = string.Format("故障");
                                        label_fsj.ForeColor = System.Drawing.Color.Red;
                                        if (value_nums[0] >= 2.0 || value_nums[0] <= -2.0)
                                        {
                                            SaveListboxToTxt(time_X, "辐射计东西总电流", a);
                                        }
                                        else if (value_nums[1] >= 2.0 || value_nums[1] <= -2.0)
                                        {
                                            SaveListboxToTxt(time_X, "辐射计南北总电流", a);
                                        }
                                    }
                                }
                                //读取“辐射计南北总电流”的数据并判断是否故障
                                if (fsj_qushi_flag == "辐射计南北总电流")
                                {
                                    dataQueue_data.Enqueue(value_nums[1]);//读取动量轮某一列数据
                                    this.chart2.ChartAreas[0].AxisY.Title = string.Format("{0}/A", 辐射计南北总电流ToolStripMenuItem);
                                    this.chart2.Titles[0].Text = "辐射计南北总电流 (-2A,2A)";
                                    this.chart2.Series[0].LegendText = "辐射计南北总电流";
                                    if (a >= 0.5)
                                    {
                                        label_fsj.Text = string.Format("故障");
                                        label_fsj.ForeColor = System.Drawing.Color.Red;
                                        if (value_nums[0] >= 2.0 || value_nums[0] <= -2.0)
                                        {
                                            SaveListboxToTxt(time_X, "辐射计东西总电流", a);
                                        }
                                        else if (value_nums[1] >= 2.0 || value_nums[1] <= -2.0)
                                        {
                                            SaveListboxToTxt(time_X, "辐射计南北总电流", a);
                                        }
                                    }
                                }
                            }
                            line = reader.ReadLine();
                            i += 1;
                        }
                        read_first_num += 5;
                    }
                }
            }

            //探测仪故障监测按钮按下后的数值更新
            if (tcy_rb.Checked)
            {
                //chart1的改变
                this.chart1.Titles[0].Text = "探测仪故障监测结果显示";
                //chart2的改变
                this.chart2.Titles[0].Text = "探测仪东西电机A相电流";
                this.chart2.ChartAreas[0].AxisY.Title = string.Format("{0}/A", 探测仪东西电机A相电流ToolStripMenuItem);
                for (int t = 0; t < tcy_file.Length; t++)
                {
                    using (StreamReader reader = new StreamReader("./读取多个文件实验/探测仪/" + tcy_file[t]))
                    {
                        int i = 0;
                        string line = reader.ReadLine();
                        while ((line = reader.ReadLine()) != null)
                        {
                            if (i >= read_first_num && i < num + read_first_num)
                            {
                                string[] line_sp = line.Split('\t');
                                double[] value_nums = new double[6];
                                double[] tcy_value_nums = new double[2];
                                value_nums[0] = double.Parse(line_sp[1]);
                                value_nums[1] = double.Parse(line_sp[2]);
                                value_nums[2] = double.Parse(line_sp[3]);
                                value_nums[3] = double.Parse(line_sp[4]);
                                value_nums[4] = double.Parse(line_sp[5]);
                                value_nums[5] = double.Parse(line_sp[6]);
                                label_shijian.Text = string.Format("{0}", TimeStampToDateTime(line_sp[0]));
                                string time_X = string.Format("{0}", TimeStampToDateTime(line_sp[0]));
                                time_X.Replace(" ", "");
                                timeQueue.Enqueue(time_X.Substring(9, 9));
                                tcy_value_nums[0] = FindFaultData(0.6, -0.6, double.Parse(line_sp[3]));
                                tcy_value_nums[1] = FindFaultData(0.6, -0.6, double.Parse(line_sp[6]));
                                double a = TEST.Class1.test_tcy(tcy_value_nums);
                                dataQueue_prob.Enqueue(a);
                                //读取“探测仪东西电机A相电流”的数据并判断是否故障
                                if (tcy_qushi_flag == "探测仪东西电机A相电流")
                                {
                                    dataQueue_data.Enqueue(value_nums[0]);//读取动量轮某一列数据
                                    this.chart2.ChartAreas[0].AxisY.Title = string.Format("{0}/A", 探测仪东西电机A相电流ToolStripMenuItem);
                                    this.chart2.Titles[0].Text = "探测仪东西电机A相电流 (-0.6A,0.6A)";
                                    this.chart2.Series[0].LegendText = "探测仪东西电机A相电流";
                                    if (a >= 0.5)
                                    {
                                        label_tcy.Text = string.Format("故障");
                                        label_tcy.ForeColor = System.Drawing.Color.Red;
                                        if (value_nums[0] >= 0.6 || value_nums[0] <= -0.6)
                                        {
                                            SaveListboxToTxt(time_X, "探测仪东西电机A相电流", a);
                                        }
                                        else if (value_nums[1] >= 0.6 || value_nums[1] <= -0.6)
                                        {
                                            SaveListboxToTxt(time_X, "探测仪东西电机B相电流", a);
                                        }
                                        else if (value_nums[2] >= 0.6 || value_nums[2] <= -0.6)
                                        {
                                            SaveListboxToTxt(time_X, "探测仪东西电机总电流", a);
                                        }
                                        else if (value_nums[3] >= 0.6 || value_nums[3] <= -0.6)
                                        {
                                            SaveListboxToTxt(time_X, "探测仪南北电机A相电流", a);
                                        }
                                        else if (value_nums[4] >= 0.6 || value_nums[4] <= -0.6)
                                        {
                                            SaveListboxToTxt(time_X, "探测仪南北电机B相电流", a);
                                        }
                                        else if (value_nums[5] >= 0.6 || value_nums[5] <= -0.6)
                                        {
                                            SaveListboxToTxt(time_X, "探测仪南北电机总电流", a);
                                        }
                                    }
                                }
                                //读取“探测仪东西电机B相电流”的数据并判断是否故障
                                if (tcy_qushi_flag == "探测仪东西电机B相电流")
                                {
                                    dataQueue_data.Enqueue(value_nums[1]);//读取动量轮某一列数据
                                    this.chart2.ChartAreas[0].AxisY.Title = string.Format("{0}/A", 探测仪东西电机B相电流ToolStripMenuItem);
                                    this.chart2.Titles[0].Text = "探测仪东西电机B相电流 (-0.6A,0.6A)";
                                    this.chart2.Series[0].LegendText = "探测仪东西电机B相电流";
                                    if (a >= 0.5)
                                    {
                                        label_tcy.Text = string.Format("故障");
                                        label_tcy.ForeColor = System.Drawing.Color.Red;
                                        if (value_nums[0] >= 0.6 || value_nums[0] <= -0.6)
                                        {
                                            SaveListboxToTxt(time_X, "探测仪东西电机A相电流", a);
                                        }
                                        else if (value_nums[1] >= 0.6 || value_nums[1] <= -0.6)
                                        {
                                            SaveListboxToTxt(time_X, "探测仪东西电机B相电流", a);
                                        }
                                        else if (value_nums[2] >= 0.6 || value_nums[2] <= -0.6)
                                        {
                                            SaveListboxToTxt(time_X, "探测仪东西电机总电流", a);
                                        }
                                        else if (value_nums[3] >= 0.6 || value_nums[3] <= -0.6)
                                        {
                                            SaveListboxToTxt(time_X, "探测仪南北电机A相电流", a);
                                        }
                                        else if (value_nums[4] >= 0.6 || value_nums[4] <= -0.6)
                                        {
                                            SaveListboxToTxt(time_X, "探测仪南北电机B相电流", a);
                                        }
                                        else if (value_nums[5] >= 0.6 || value_nums[5] <= -0.6)
                                        {
                                            SaveListboxToTxt(time_X, "探测仪南北电机总电流", a);
                                        }
                                    }
                                }
                                //读取“探测仪东西电机总电流”的数据并判断是否故障
                                if (tcy_qushi_flag == "探测仪东西电机总电流")
                                {
                                    dataQueue_data.Enqueue(value_nums[2]);//读取动量轮某一列数据
                                    this.chart2.ChartAreas[0].AxisY.Title = string.Format("{0}/A", 探测仪东西电机总电流ToolStripMenuItem);
                                    this.chart2.Titles[0].Text = "探测仪东西电机总电流 (-0.6A,0.6A)";
                                    this.chart2.Series[0].LegendText = "探测仪东西电机总电流";
                                    if (a >= 0.5)
                                    {
                                        label_tcy.Text = string.Format("故障");
                                        label_tcy.ForeColor = System.Drawing.Color.Red;
                                        if (value_nums[0] >= 0.6 || value_nums[0] <= -0.6)
                                        {
                                            SaveListboxToTxt(time_X, "探测仪东西电机A相电流", a);
                                        }
                                        else if (value_nums[1] >= 0.6 || value_nums[1] <= -0.6)
                                        {
                                            SaveListboxToTxt(time_X, "探测仪东西电机B相电流", a);
                                        }
                                        else if (value_nums[2] >= 0.6 || value_nums[2] <= -0.6)
                                        {
                                            SaveListboxToTxt(time_X, "探测仪东西电机总电流", a);
                                        }
                                        else if (value_nums[3] >= 0.6 || value_nums[3] <= -0.6)
                                        {
                                            SaveListboxToTxt(time_X, "探测仪南北电机A相电流", a);
                                        }
                                        else if (value_nums[4] >= 0.6 || value_nums[4] <= -0.6)
                                        {
                                            SaveListboxToTxt(time_X, "探测仪南北电机B相电流", a);
                                        }
                                        else if (value_nums[5] >= 0.6 || value_nums[5] <= -0.6)
                                        {
                                            SaveListboxToTxt(time_X, "探测仪南北电机总电流", a);
                                        }
                                    }
                                }
                                //读取“探测仪南北电机A相电流”的数据并判断是否故障
                                if (tcy_qushi_flag == "探测仪南北电机A相电流")
                                {
                                    dataQueue_data.Enqueue(value_nums[3]);//读取动量轮某一列数据
                                    this.chart2.ChartAreas[0].AxisY.Title = string.Format("{0}/A", 探测仪南北电机A相电流ToolStripMenuItem);
                                    this.chart2.Titles[0].Text = "探测仪南北电机A相电流 (-0.6A,0.6A)";
                                    this.chart2.Series[0].LegendText = "探测仪南北电机A相电流";
                                    if (a >= 0.5)
                                    {
                                        label_tcy.Text = string.Format("故障");
                                        label_tcy.ForeColor = System.Drawing.Color.Red;
                                        if (value_nums[0] >= 0.6 || value_nums[0] <= -0.6)
                                        {
                                            SaveListboxToTxt(time_X, "探测仪东西电机A相电流", a);
                                        }
                                        else if (value_nums[1] >= 0.6 || value_nums[1] <= -0.6)
                                        {
                                            SaveListboxToTxt(time_X, "探测仪东西电机B相电流", a);
                                        }
                                        else if (value_nums[2] >= 0.6 || value_nums[2] <= -0.6)
                                        {
                                            SaveListboxToTxt(time_X, "探测仪东西电机总电流", a);
                                        }
                                        else if (value_nums[3] >= 0.6 || value_nums[3] <= -0.6)
                                        {
                                            SaveListboxToTxt(time_X, "探测仪南北电机A相电流", a);
                                        }
                                        else if (value_nums[4] >= 0.6 || value_nums[4] <= -0.6)
                                        {
                                            SaveListboxToTxt(time_X, "探测仪南北电机B相电流", a);
                                        }
                                        else if (value_nums[5] >= 0.6 || value_nums[5] <= -0.6)
                                        {
                                            SaveListboxToTxt(time_X, "探测仪南北电机总电流", a);
                                        }
                                    }
                                }
                                //读取“探测仪南北电机B相电流”的数据并判断是否故障
                                if (tcy_qushi_flag == "探测仪南北电机B相电流")
                                {
                                    dataQueue_data.Enqueue(value_nums[4]);//读取动量轮某一列数据
                                    this.chart2.ChartAreas[0].AxisY.Title = string.Format("{0}/A", 探测仪南北电机B相电流ToolStripMenuItem);
                                    this.chart2.Titles[0].Text = "探测仪南北电机B相电流 (-0.6A,0.6A)";
                                    this.chart2.Series[0].LegendText = "探测仪南北电机B相电流";
                                    if (a >= 0.5)
                                    {
                                        label_tcy.Text = string.Format("故障");
                                        label_tcy.ForeColor = System.Drawing.Color.Red;
                                        if (value_nums[0] >= 0.6 || value_nums[0] <= -0.6)
                                        {
                                            SaveListboxToTxt(time_X, "探测仪东西电机A相电流", a);
                                        }
                                        else if (value_nums[1] >= 0.6 || value_nums[1] <= -0.6)
                                        {
                                            SaveListboxToTxt(time_X, "探测仪东西电机B相电流", a);
                                        }
                                        else if (value_nums[2] >= 0.6 || value_nums[2] <= -0.6)
                                        {
                                            SaveListboxToTxt(time_X, "探测仪东西电机总电流", a);
                                        }
                                        else if (value_nums[3] >= 0.6 || value_nums[3] <= -0.6)
                                        {
                                            SaveListboxToTxt(time_X, "探测仪南北电机A相电流", a);
                                        }
                                        else if (value_nums[4] >= 0.6 || value_nums[4] <= -0.6)
                                        {
                                            SaveListboxToTxt(time_X, "探测仪南北电机B相电流", a);
                                        }
                                        else if (value_nums[5] >= 0.6 || value_nums[5] <= -0.6)
                                        {
                                            SaveListboxToTxt(time_X, "探测仪南北电机总电流", a);
                                        }
                                    }
                                }
                                //读取“探测仪南北电机总电流”的数据并判断是否故障
                                if (tcy_qushi_flag == "探测仪南北电机总电流")
                                {
                                    dataQueue_data.Enqueue(value_nums[5]);//读取动量轮某一列数据
                                    this.chart2.ChartAreas[0].AxisY.Title = string.Format("{0}/A", 探测仪南北电机总电流ToolStripMenuItem);
                                    this.chart2.Titles[0].Text = "探测仪南北电机总电流 (-0.6A,0.6A)";
                                    this.chart2.Series[0].LegendText = "探测仪南北电机总电流";
                                    if (a >= 0.5)
                                    {
                                        label_tcy.Text = string.Format("故障");
                                        label_tcy.ForeColor = System.Drawing.Color.Red;
                                        if (value_nums[0] >= 0.6 || value_nums[0] <= -0.6)
                                        {
                                            SaveListboxToTxt(time_X, "探测仪东西电机A相电流", a);
                                        }
                                        else if (value_nums[1] >= 0.6 || value_nums[1] <= -0.6)
                                        {
                                            SaveListboxToTxt(time_X, "探测仪东西电机B相电流", a);
                                        }
                                        else if (value_nums[2] >= 0.6 || value_nums[2] <= -0.6)
                                        {
                                            SaveListboxToTxt(time_X, "探测仪东西电机总电流", a);
                                        }
                                        else if (value_nums[3] >= 0.6 || value_nums[3] <= -0.6)
                                        {
                                            SaveListboxToTxt(time_X, "探测仪南北电机A相电流", a);
                                        }
                                        else if (value_nums[4] >= 0.6 || value_nums[4] <= -0.6)
                                        {
                                            SaveListboxToTxt(time_X, "探测仪南北电机B相电流", a);
                                        }
                                        else if (value_nums[5] >= 0.6 || value_nums[5] <= -0.6)
                                        {
                                            SaveListboxToTxt(time_X, "探测仪南北电机总电流", a);
                                        }
                                    }
                                }
                            }
                            line = reader.ReadLine();
                            i += 1;
                        }

                        read_first_num += 5;
                    }
                }
            }
            //电源故障监测按钮按下后的数值更新
            if (dy_rb.Checked)
            {
                //chart1的改变
                this.chart1.Titles[0].Text = "电源故障监测结果显示";
                //chart2的改变
                this.chart2.Titles[0].Text = "电源";
                this.chart2.ChartAreas[0].AxisY.Title = string.Format("{0}/V", 母线28V电压ToolStripMenuItem);
                for (int t = 0; t < dy_file.Length; t++)
                {
                    using (StreamReader reader = new StreamReader("./读取多个文件实验/电源/" + dy_file[t]))
                    {
                        int i = 0;
                        string line = reader.ReadLine();
                        while ((line = reader.ReadLine()) != null)
                        {
                            if (i >= read_first_dy_num && i < num + read_first_dy_num)
                            {
                                string[] line_sp = line.Split('\t');
                                double[] value_nums = new double[49];
                                value_nums[0] = FindFaultData(28.5, 27.5, double.Parse(line_sp[1])); 
                                value_nums[1] = FindFaultData(43.0, -0.5, double.Parse(line_sp[2]));
                                value_nums[2] = FindFaultData(43.0, 41.0, double.Parse(line_sp[3]));
                                value_nums[3] = FindFaultData(78.0, -1.0, double.Parse(line_sp[4]));
                                value_nums[4] = FindFaultData(39.0, -0.5, double.Parse(line_sp[5]));
                                value_nums[5] = FindFaultData(39.0, -0.5, double.Parse(line_sp[6]));
                                value_nums[6] = FindFaultData(110.0, -1.0, double.Parse(line_sp[7]));
                                value_nums[7] = FindFaultData(39.0, -0.5, double.Parse(line_sp[35]));
                                value_nums[8] = FindFaultData(39.0, -0.5, double.Parse(line_sp[36]));
                                value_nums[9] = FindFaultData(22.0, -1.0, double.Parse(line_sp[65]));
                                value_nums[10] = FindFaultData(35.0, -10.0, double.Parse(line_sp[12]));
                                value_nums[11] = FindFaultData(35.0, -10.0, double.Parse(line_sp[13]));
                                value_nums[12] = FindFaultData(35.0, -10.0, double.Parse(line_sp[17]));
                                value_nums[13] = FindFaultData(35.0, -10.0, double.Parse(line_sp[18]));
                                value_nums[14] = FindFaultData(35.0, -10.0, double.Parse(line_sp[39]));
                                value_nums[15] = FindFaultData(35.0, -10.0, double.Parse(line_sp[41]));
                                value_nums[16] = FindFaultData(35.0, -10.0, double.Parse(line_sp[42]));
                                value_nums[17] = FindFaultData(55.0, -15.0, double.Parse(line_sp[158]));
                                value_nums[18] = FindFaultData(55.0, -15.0, double.Parse(line_sp[159]));
                                value_nums[19] = FindFaultData(12.0, -1.0, double.Parse(line_sp[53]));
                                value_nums[20] = FindFaultData(12.0, -1.0, double.Parse(line_sp[54]));
                                value_nums[21] = FindFaultData(12.0, -1.0, double.Parse(line_sp[55]));
                                value_nums[22] = FindFaultData(12.0, -1.0, double.Parse(line_sp[56]));
                                value_nums[23] = FindFaultData(20.0, -1.0, double.Parse(line_sp[57]));
                                value_nums[24] = FindFaultData(20.0, -1.0, double.Parse(line_sp[58]));
                                value_nums[25] = FindFaultData(20.0, -1.0, double.Parse(line_sp[59]));
                                value_nums[26] = FindFaultData(20.0, -1.0, double.Parse(line_sp[60]));
                                value_nums[27] = FindFaultData(20.0, -1.0, double.Parse(line_sp[61]));
                                value_nums[28] = FindFaultData(20.0, -1.0, double.Parse(line_sp[62]));
                                value_nums[29] = FindFaultData(20.0, -1.0, double.Parse(line_sp[63]));
                                value_nums[30] = FindFaultData(20.0, -1.0, double.Parse(line_sp[64]));
                                value_nums[31] = FindFaultData(4.5, 2.5, double.Parse(line_sp[192]));
                                value_nums[32] = FindFaultData(4.5, 2.5, double.Parse(line_sp[193]));
                                value_nums[33] = FindFaultData(4.5, 2.5, double.Parse(line_sp[194]));
                                value_nums[34] = FindFaultData(4.5, 2.5, double.Parse(line_sp[195]));
                                value_nums[35] = FindFaultData(4.5, 2.5, double.Parse(line_sp[196]));
                                value_nums[36] = FindFaultData(4.5, 2.5, double.Parse(line_sp[197]));
                                value_nums[37] = FindFaultData(4.5, 2.5, double.Parse(line_sp[198]));
                                value_nums[38] = FindFaultData(4.5, 2.5, double.Parse(line_sp[199]));
                                value_nums[39] = FindFaultData(4.5, 2.5, double.Parse(line_sp[200]));
                                value_nums[40] = FindFaultData(4.5, 2.5, double.Parse(line_sp[201]));
                                value_nums[41] = FindFaultData(4.5, 2.5, double.Parse(line_sp[202]));
                                value_nums[42] = FindFaultData(4.5, 2.5, double.Parse(line_sp[203]));
                                value_nums[43] = FindFaultData(4.5, 2.5, double.Parse(line_sp[204]));
                                value_nums[44] = FindFaultData(4.5, 2.5, double.Parse(line_sp[205]));
                                value_nums[45] = FindFaultData(4.5, 2.5, double.Parse(line_sp[206]));
                                value_nums[46] = FindFaultData(4.5, 2.5, double.Parse(line_sp[207]));
                                value_nums[47] = FindFaultData(4.5, 2.5, double.Parse(line_sp[208]));
                                value_nums[48] = FindFaultData(4.5, 2.5, double.Parse(line_sp[209]));

                                label_shijian.Text = string.Format("{0}", line_sp[0]);
                                string time_X = line_sp[0];
                                time_X.Replace(" ", "");
                                timeQueue.Enqueue(time_X.Substring(9, 8));
                                double a = TEST.Class1.test_dy(value_nums);
                                dataQueue_prob.Enqueue(a);
                                //读取“28V母线电压”的数据并判断是否故障
                                if (dy_qushi_flag == "28V母线电压")
                                {
                                    dataQueue_data.Enqueue(value_nums[0]);
                                    this.chart2.ChartAreas[0].AxisY.Title = "28V母线电压/V";
                                    this.chart2.Titles[0].Text = "28V母线电压 (27.5A,28.5A)";
                                    this.chart2.Series[0].LegendText = "28V母线电压";
                                    if (a >= 0.5)
                                    {
                                        label_dy.Text = string.Format("故障");
                                        label_dy.ForeColor = System.Drawing.Color.Red;
                                        if (value_nums[0] >= 28.5 || value_nums[0] <= 27.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "28V母线电压", a);
                                        }
                                        else if (value_nums[1] >= 43 || value_nums[1] <= -0.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "28V负载电流", a);
                                        }
                                        else if (value_nums[2] >= 43 || value_nums[2] <= 41)
                                        {
                                            SaveListboxToTxt(line_sp[0], "42V母线电压", a);
                                        }
                                        else if (value_nums[3] >= 78 || value_nums[3] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "42V负载电流", a);
                                        }
                                        else if (value_nums[4] >= 39 || value_nums[4] <= -0.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组电池电压1", a);
                                        }
                                        else if (value_nums[5] >= 39 || value_nums[5] <= -0.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组电池电压1", a);
                                        }
                                        else if (value_nums[6] >= 110 || value_nums[6] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "太阳电池阵输出电流", a);
                                        }
                                        else if (value_nums[7] >= 39 || value_nums[7] <= -0.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组电池电压2", a);
                                        }
                                        else if (value_nums[8] >= 39 || value_nums[8] <= -0.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组电池电压2", a);
                                        }
                                        else if (value_nums[9] >= 22 || value_nums[9] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "MEA电压", a);
                                        }
                                        else if (value_nums[10] >= 35 || value_nums[10] <= -10)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组电池温度1", a);
                                        }
                                        else if (value_nums[11] >= 35 || value_nums[11] <= -10)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组电池温度2", a);
                                        }
                                        else if (value_nums[12] >= 35 || value_nums[12] <= -10)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组电池温度1", a);
                                        }
                                        else if (value_nums[13] >= 35 || value_nums[13] <= -10)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组电池温度2", a);
                                        }
                                        else if (value_nums[14] >= 35 || value_nums[14] <= -10)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组电池温度4", a);
                                        }
                                        else if (value_nums[15] >= 35 || value_nums[15] <= -10)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组电池温度3", a);
                                        }
                                        else if (value_nums[16] >= 35 || value_nums[16] <= -10)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组电池温度4", a);
                                        }
                                        else if (value_nums[17] >= 55 || value_nums[17] <= -15)
                                        {
                                            SaveListboxToTxt(line_sp[0], "充放电模块温度", a);
                                        }
                                        else if (value_nums[18] >= 55 || value_nums[18] <= -15)
                                        {
                                            SaveListboxToTxt(line_sp[0], "分流模块温度", a);
                                        }
                                        else if (value_nums[19] >= 12 || value_nums[19] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A1充电电流", a);
                                        }
                                        else if (value_nums[20] >= 12 || value_nums[20] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A2充电电流", a);
                                        }
                                        else if (value_nums[21] >= 12 || value_nums[21] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B1充电电流", a);
                                        }
                                        else if (value_nums[22] >= 12 || value_nums[22] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B2充电电流", a);
                                        }
                                        else if (value_nums[23] >= 20 || value_nums[23] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A1放电电流", a);
                                        }
                                        else if (value_nums[24] >= 20 || value_nums[24] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A2放电电流", a);
                                        }
                                        else if (value_nums[25] >= 20 || value_nums[25] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A3放电电流", a);
                                        }
                                        else if (value_nums[26] >= 20 || value_nums[26] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A4放电电流", a);
                                        }
                                        else if (value_nums[27] >= 20 || value_nums[27] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B1放电电流", a);
                                        }
                                        else if (value_nums[28] >= 20 || value_nums[28] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B2放电电流", a);
                                        }
                                        else if (value_nums[29] >= 20 || value_nums[29] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B3放电电流", a);
                                        }
                                        else if (value_nums[30] >= 20 || value_nums[30] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B4放电电流", a);
                                        }
                                        else if (value_nums[31] >= 4.5 || value_nums[31] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组单体1电压", a);
                                        }
                                        else if (value_nums[32] >= 4.5 || value_nums[32] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组单体2电压", a);
                                        }
                                        else if (value_nums[33] >= 4.5 || value_nums[33] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组单体3电压", a);
                                        }
                                        else if (value_nums[34] >= 4.5 || value_nums[34] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组单体4电压", a);
                                        }
                                        else if (value_nums[35] >= 4.5 || value_nums[35] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组单体5电压", a);
                                        }
                                        else if (value_nums[36] >= 4.5 || value_nums[36] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组单体6电压", a);
                                        }
                                        else if (value_nums[37] >= 4.5 || value_nums[37] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组单体7电压", a);
                                        }
                                        else if (value_nums[38] >= 4.5 || value_nums[38] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组单体8电压", a);
                                        }
                                        else if (value_nums[39] >= 4.5 || value_nums[39] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组单体9电压", a);
                                        }
                                        else if (value_nums[40] >= 4.5 || value_nums[40] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组单体1电压", a);
                                        }
                                        else if (value_nums[41] >= 4.5 || value_nums[41] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组单体2电压", a);
                                        }
                                        else if (value_nums[42] >= 4.5 || value_nums[42] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组单体3电压", a);
                                        }
                                        else if (value_nums[43] >= 4.5 || value_nums[43] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组单体4电压", a);
                                        }
                                        else if (value_nums[44] >= 4.5 || value_nums[44] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组单体5电压", a);
                                        }
                                        else if (value_nums[45] >= 4.5 || value_nums[45] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组单体6电压", a);
                                        }
                                        else if (value_nums[46] >= 4.5 || value_nums[46] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组单体7电压", a);
                                        }
                                        else if (value_nums[47] >= 4.5 || value_nums[47] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组单体8电压", a);
                                        }
                                        else if (value_nums[48] >= 4.5 || value_nums[48] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组单体9电压", a);
                                        }
                                    }
                                }
                                //读取“28V负载电流”的数据并判断是否故障
                                else if (dy_qushi_flag == "28V负载电流")
                                {
                                    dataQueue_data.Enqueue(value_nums[1]);
                                    this.chart2.ChartAreas[0].AxisY.Title = "28V负载电流/A";
                                    this.chart2.Titles[0].Text = "28V负载电流 (-0.5A,43A)";
                                    this.chart2.Series[0].LegendText = "28V负载电流";
                                    if (a >= 0.5)
                                    {
                                        label_dy.Text = string.Format("故障");
                                        label_dy.ForeColor = System.Drawing.Color.Red;
                                        if (value_nums[0] >= 28.5 || value_nums[0] <= 27.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "28V母线电压", a);
                                        }
                                        else if (value_nums[1] >= 43 || value_nums[1] <= -0.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "28V负载电流", a);
                                        }
                                        else if (value_nums[2] >= 43 || value_nums[2] <= 41)
                                        {
                                            SaveListboxToTxt(line_sp[0], "42V母线电压", a);
                                        }
                                        else if (value_nums[3] >= 78 || value_nums[3] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "42V负载电流", a);
                                        }
                                        else if (value_nums[4] >= 39 || value_nums[4] <= -0.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组电池电压1", a);
                                        }
                                        else if (value_nums[5] >= 39 || value_nums[5] <= -0.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组电池电压1", a);
                                        }
                                        else if (value_nums[6] >= 110 || value_nums[6] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "太阳电池阵输出电流", a);
                                        }
                                        else if (value_nums[7] >= 39 || value_nums[7] <= -0.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组电池电压2", a);
                                        }
                                        else if (value_nums[8] >= 39 || value_nums[8] <= -0.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组电池电压2", a);
                                        }
                                        else if (value_nums[9] >= 22 || value_nums[9] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "MEA电压", a);
                                        }
                                        else if (value_nums[10] >= 35 || value_nums[10] <= -10)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组电池温度1", a);
                                        }
                                        else if (value_nums[11] >= 35 || value_nums[11] <= -10)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组电池温度2", a);
                                        }
                                        else if (value_nums[12] >= 35 || value_nums[12] <= -10)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组电池温度1", a);
                                        }
                                        else if (value_nums[13] >= 35 || value_nums[13] <= -10)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组电池温度2", a);
                                        }
                                        else if (value_nums[14] >= 35 || value_nums[14] <= -10)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组电池温度4", a);
                                        }
                                        else if (value_nums[15] >= 35 || value_nums[15] <= -10)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组电池温度3", a);
                                        }
                                        else if (value_nums[16] >= 35 || value_nums[16] <= -10)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组电池温度4", a);
                                        }
                                        else if (value_nums[17] >= 55 || value_nums[17] <= -15)
                                        {
                                            SaveListboxToTxt(line_sp[0], "充放电模块温度", a);
                                        }
                                        else if (value_nums[18] >= 55 || value_nums[18] <= -15)
                                        {
                                            SaveListboxToTxt(line_sp[0], "分流模块温度", a);
                                        }
                                        else if (value_nums[19] >= 12 || value_nums[19] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A1充电电流", a);
                                        }
                                        else if (value_nums[20] >= 12 || value_nums[20] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A2充电电流", a);
                                        }
                                        else if (value_nums[21] >= 12 || value_nums[21] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B1充电电流", a);
                                        }
                                        else if (value_nums[22] >= 12 || value_nums[22] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B2充电电流", a);
                                        }
                                        else if (value_nums[23] >= 20 || value_nums[23] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A1放电电流", a);
                                        }
                                        else if (value_nums[24] >= 20 || value_nums[24] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A2放电电流", a);
                                        }
                                        else if (value_nums[25] >= 20 || value_nums[25] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A3放电电流", a);
                                        }
                                        else if (value_nums[26] >= 20 || value_nums[26] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A4放电电流", a);
                                        }
                                        else if (value_nums[27] >= 20 || value_nums[27] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B1放电电流", a);
                                        }
                                        else if (value_nums[28] >= 20 || value_nums[28] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B2放电电流", a);
                                        }
                                        else if (value_nums[29] >= 20 || value_nums[29] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B3放电电流", a);
                                        }
                                        else if (value_nums[30] >= 20 || value_nums[30] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B4放电电流", a);
                                        }
                                        else if (value_nums[31] >= 4.5 || value_nums[31] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组单体1电压", a);
                                        }
                                        else if (value_nums[32] >= 4.5 || value_nums[32] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组单体2电压", a);
                                        }
                                        else if (value_nums[33] >= 4.5 || value_nums[33] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组单体3电压", a);
                                        }
                                        else if (value_nums[34] >= 4.5 || value_nums[34] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组单体4电压", a);
                                        }
                                        else if (value_nums[35] >= 4.5 || value_nums[35] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组单体5电压", a);
                                        }
                                        else if (value_nums[36] >= 4.5 || value_nums[36] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组单体6电压", a);
                                        }
                                        else if (value_nums[37] >= 4.5 || value_nums[37] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组单体7电压", a);
                                        }
                                        else if (value_nums[38] >= 4.5 || value_nums[38] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组单体8电压", a);
                                        }
                                        else if (value_nums[39] >= 4.5 || value_nums[39] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组单体9电压", a);
                                        }
                                        else if (value_nums[40] >= 4.5 || value_nums[40] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组单体1电压", a);
                                        }
                                        else if (value_nums[41] >= 4.5 || value_nums[41] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组单体2电压", a);
                                        }
                                        else if (value_nums[42] >= 4.5 || value_nums[42] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组单体3电压", a);
                                        }
                                        else if (value_nums[43] >= 4.5 || value_nums[43] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组单体4电压", a);
                                        }
                                        else if (value_nums[44] >= 4.5 || value_nums[44] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组单体5电压", a);
                                        }
                                        else if (value_nums[45] >= 4.5 || value_nums[45] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组单体6电压", a);
                                        }
                                        else if (value_nums[46] >= 4.5 || value_nums[46] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组单体7电压", a);
                                        }
                                        else if (value_nums[47] >= 4.5 || value_nums[47] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组单体8电压", a);
                                        }
                                        else if (value_nums[48] >= 4.5 || value_nums[48] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组单体9电压", a);
                                        }
                                    }
                                }
                                //读取“42V母线电压”的数据并判断是否故障
                                else if (dy_qushi_flag == "42V母线电压")
                                {
                                    dataQueue_data.Enqueue(value_nums[2]);
                                    this.chart2.ChartAreas[0].AxisY.Title = "42V母线电压/V";
                                    this.chart2.Titles[0].Text = "42V母线电压  (41A,43A)";
                                    this.chart2.Series[0].LegendText = "42V母线电压";
                                    if (a >= 0.5)
                                    {
                                        label_dy.Text = string.Format("故障");
                                        label_dy.ForeColor = System.Drawing.Color.Red;
                                        if (value_nums[0] >= 28.5 || value_nums[0] <= 27.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "28V母线电压", a);
                                        }
                                        else if (value_nums[1] >= 43 || value_nums[1] <= -0.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "28V负载电流", a);
                                        }
                                        else if (value_nums[2] >= 43 || value_nums[2] <= 41)
                                        {
                                            SaveListboxToTxt(line_sp[0], "42V母线电压", a);
                                        }
                                        else if (value_nums[3] >= 78 || value_nums[3] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "42V负载电流", a);
                                        }
                                        else if (value_nums[4] >= 39 || value_nums[4] <= -0.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组电池电压1", a);
                                        }
                                        else if (value_nums[5] >= 39 || value_nums[5] <= -0.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组电池电压1", a);
                                        }
                                        else if (value_nums[6] >= 110 || value_nums[6] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "太阳电池阵输出电流", a);
                                        }
                                        else if (value_nums[7] >= 39 || value_nums[7] <= -0.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组电池电压2", a);
                                        }
                                        else if (value_nums[8] >= 39 || value_nums[8] <= -0.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组电池电压2", a);
                                        }
                                        else if (value_nums[9] >= 22 || value_nums[9] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "MEA电压", a);
                                        }
                                        else if (value_nums[10] >= 35 || value_nums[10] <= -10)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组电池温度1", a);
                                        }
                                        else if (value_nums[11] >= 35 || value_nums[11] <= -10)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组电池温度2", a);
                                        }
                                        else if (value_nums[12] >= 35 || value_nums[12] <= -10)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组电池温度1", a);
                                        }
                                        else if (value_nums[13] >= 35 || value_nums[13] <= -10)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组电池温度2", a);
                                        }
                                        else if (value_nums[14] >= 35 || value_nums[14] <= -10)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组电池温度4", a);
                                        }
                                        else if (value_nums[15] >= 35 || value_nums[15] <= -10)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组电池温度3", a);
                                        }
                                        else if (value_nums[16] >= 35 || value_nums[16] <= -10)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组电池温度4", a);
                                        }
                                        else if (value_nums[17] >= 55 || value_nums[17] <= -15)
                                        {
                                            SaveListboxToTxt(line_sp[0], "充放电模块温度", a);
                                        }
                                        else if (value_nums[18] >= 55 || value_nums[18] <= -15)
                                        {
                                            SaveListboxToTxt(line_sp[0], "分流模块温度", a);
                                        }
                                        else if (value_nums[19] >= 12 || value_nums[19] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A1充电电流", a);
                                        }
                                        else if (value_nums[20] >= 12 || value_nums[20] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A2充电电流", a);
                                        }
                                        else if (value_nums[21] >= 12 || value_nums[21] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B1充电电流", a);
                                        }
                                        else if (value_nums[22] >= 12 || value_nums[22] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B2充电电流", a);
                                        }
                                        else if (value_nums[23] >= 20 || value_nums[23] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A1放电电流", a);
                                        }
                                        else if (value_nums[24] >= 20 || value_nums[24] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A2放电电流", a);
                                        }
                                        else if (value_nums[25] >= 20 || value_nums[25] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A3放电电流", a);
                                        }
                                        else if (value_nums[26] >= 20 || value_nums[26] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A4放电电流", a);
                                        }
                                        else if (value_nums[27] >= 20 || value_nums[27] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B1放电电流", a);
                                        }
                                        else if (value_nums[28] >= 20 || value_nums[28] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B2放电电流", a);
                                        }
                                        else if (value_nums[29] >= 20 || value_nums[29] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B3放电电流", a);
                                        }
                                        else if (value_nums[30] >= 20 || value_nums[30] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B4放电电流", a);
                                        }
                                        else if (value_nums[31] >= 4.5 || value_nums[31] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组单体1电压", a);
                                        }
                                        else if (value_nums[32] >= 4.5 || value_nums[32] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组单体2电压", a);
                                        }
                                        else if (value_nums[33] >= 4.5 || value_nums[33] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组单体3电压", a);
                                        }
                                        else if (value_nums[34] >= 4.5 || value_nums[34] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组单体4电压", a);
                                        }
                                        else if (value_nums[35] >= 4.5 || value_nums[35] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组单体5电压", a);
                                        }
                                        else if (value_nums[36] >= 4.5 || value_nums[36] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组单体6电压", a);
                                        }
                                        else if (value_nums[37] >= 4.5 || value_nums[37] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组单体7电压", a);
                                        }
                                        else if (value_nums[38] >= 4.5 || value_nums[38] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组单体8电压", a);
                                        }
                                        else if (value_nums[39] >= 4.5 || value_nums[39] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组单体9电压", a);
                                        }
                                        else if (value_nums[40] >= 4.5 || value_nums[40] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组单体1电压", a);
                                        }
                                        else if (value_nums[41] >= 4.5 || value_nums[41] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组单体2电压", a);
                                        }
                                        else if (value_nums[42] >= 4.5 || value_nums[42] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组单体3电压", a);
                                        }
                                        else if (value_nums[43] >= 4.5 || value_nums[43] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组单体4电压", a);
                                        }
                                        else if (value_nums[44] >= 4.5 || value_nums[44] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组单体5电压", a);
                                        }
                                        else if (value_nums[45] >= 4.5 || value_nums[45] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组单体6电压", a);
                                        }
                                        else if (value_nums[46] >= 4.5 || value_nums[46] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组单体7电压", a);
                                        }
                                        else if (value_nums[47] >= 4.5 || value_nums[47] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组单体8电压", a);
                                        }
                                        else if (value_nums[48] >= 4.5 || value_nums[48] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组单体9电压", a);
                                        }
                                    }
                                }
                                //读取“42V负载电流”的数据并判断是否故障
                                else if (dy_qushi_flag == "42V负载电流")
                                {
                                    dataQueue_data.Enqueue(value_nums[3]);
                                    this.chart2.ChartAreas[0].AxisY.Title = "42V负载电流/A";
                                    this.chart2.Titles[0].Text = "42V负载电流 (-1A,78A)";
                                    this.chart2.Series[0].LegendText = "42V负载电流";
                                    if (a >= 0.5)
                                    {
                                        label_dy.Text = string.Format("故障");
                                        label_dy.ForeColor = System.Drawing.Color.Red;
                                        if (value_nums[0] >= 28.5 || value_nums[0] <= 27.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "28V母线电压", a);
                                        }
                                        else if (value_nums[1] >= 43 || value_nums[1] <= -0.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "28V负载电流", a);
                                        }
                                        else if (value_nums[2] >= 43 || value_nums[2] <= 41)
                                        {
                                            SaveListboxToTxt(line_sp[0], "42V母线电压", a);
                                        }
                                        else if (value_nums[3] >= 78 || value_nums[3] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "42V负载电流", a);
                                        }
                                        else if (value_nums[4] >= 39 || value_nums[4] <= -0.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组电池电压1", a);
                                        }
                                        else if (value_nums[5] >= 39 || value_nums[5] <= -0.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组电池电压1", a);
                                        }
                                        else if (value_nums[6] >= 110 || value_nums[6] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "太阳电池阵输出电流", a);
                                        }
                                        else if (value_nums[7] >= 39 || value_nums[7] <= -0.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组电池电压2", a);
                                        }
                                        else if (value_nums[8] >= 39 || value_nums[8] <= -0.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组电池电压2", a);
                                        }
                                        else if (value_nums[9] >= 22 || value_nums[9] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "MEA电压", a);
                                        }
                                        else if (value_nums[10] >= 35 || value_nums[10] <= -10)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组电池温度1", a);
                                        }
                                        else if (value_nums[11] >= 35 || value_nums[11] <= -10)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组电池温度2", a);
                                        }
                                        else if (value_nums[12] >= 35 || value_nums[12] <= -10)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组电池温度1", a);
                                        }
                                        else if (value_nums[13] >= 35 || value_nums[13] <= -10)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组电池温度2", a);
                                        }
                                        else if (value_nums[14] >= 35 || value_nums[14] <= -10)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组电池温度4", a);
                                        }
                                        else if (value_nums[15] >= 35 || value_nums[15] <= -10)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组电池温度3", a);
                                        }
                                        else if (value_nums[16] >= 35 || value_nums[16] <= -10)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组电池温度4", a);
                                        }
                                        else if (value_nums[17] >= 55 || value_nums[17] <= -15)
                                        {
                                            SaveListboxToTxt(line_sp[0], "充放电模块温度", a);
                                        }
                                        else if (value_nums[18] >= 55 || value_nums[18] <= -15)
                                        {
                                            SaveListboxToTxt(line_sp[0], "分流模块温度", a);
                                        }
                                        else if (value_nums[19] >= 12 || value_nums[19] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A1充电电流", a);
                                        }
                                        else if (value_nums[20] >= 12 || value_nums[20] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A2充电电流", a);
                                        }
                                        else if (value_nums[21] >= 12 || value_nums[21] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B1充电电流", a);
                                        }
                                        else if (value_nums[22] >= 12 || value_nums[22] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B2充电电流", a);
                                        }
                                        else if (value_nums[23] >= 20 || value_nums[23] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A1放电电流", a);
                                        }
                                        else if (value_nums[24] >= 20 || value_nums[24] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A2放电电流", a);
                                        }
                                        else if (value_nums[25] >= 20 || value_nums[25] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A3放电电流", a);
                                        }
                                        else if (value_nums[26] >= 20 || value_nums[26] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A4放电电流", a);
                                        }
                                        else if (value_nums[27] >= 20 || value_nums[27] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B1放电电流", a);
                                        }
                                        else if (value_nums[28] >= 20 || value_nums[28] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B2放电电流", a);
                                        }
                                        else if (value_nums[29] >= 20 || value_nums[29] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B3放电电流", a);
                                        }
                                        else if (value_nums[30] >= 20 || value_nums[30] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B4放电电流", a);
                                        }
                                        else if (value_nums[31] >= 4.5 || value_nums[31] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组单体1电压", a);
                                        }
                                        else if (value_nums[32] >= 4.5 || value_nums[32] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组单体2电压", a);
                                        }
                                        else if (value_nums[33] >= 4.5 || value_nums[33] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组单体3电压", a);
                                        }
                                        else if (value_nums[34] >= 4.5 || value_nums[34] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组单体4电压", a);
                                        }
                                        else if (value_nums[35] >= 4.5 || value_nums[35] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组单体5电压", a);
                                        }
                                        else if (value_nums[36] >= 4.5 || value_nums[36] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组单体6电压", a);
                                        }
                                        else if (value_nums[37] >= 4.5 || value_nums[37] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组单体7电压", a);
                                        }
                                        else if (value_nums[38] >= 4.5 || value_nums[38] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组单体8电压", a);
                                        }
                                        else if (value_nums[39] >= 4.5 || value_nums[39] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组单体9电压", a);
                                        }
                                        else if (value_nums[40] >= 4.5 || value_nums[40] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组单体1电压", a);
                                        }
                                        else if (value_nums[41] >= 4.5 || value_nums[41] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组单体2电压", a);
                                        }
                                        else if (value_nums[42] >= 4.5 || value_nums[42] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组单体3电压", a);
                                        }
                                        else if (value_nums[43] >= 4.5 || value_nums[43] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组单体4电压", a);
                                        }
                                        else if (value_nums[44] >= 4.5 || value_nums[44] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组单体5电压", a);
                                        }
                                        else if (value_nums[45] >= 4.5 || value_nums[45] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组单体6电压", a);
                                        }
                                        else if (value_nums[46] >= 4.5 || value_nums[46] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组单体7电压", a);
                                        }
                                        else if (value_nums[47] >= 4.5 || value_nums[47] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组单体8电压", a);
                                        }
                                        else if (value_nums[48] >= 4.5 || value_nums[48] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组单体9电压", a);
                                        }
                                    }
                                }
                                //读取“A组电池电压1”的数据并判断是否故障
                                else if (dy_qushi_flag == "A组电池电压1")
                                {
                                    dataQueue_data.Enqueue(value_nums[4]);
                                    this.chart2.ChartAreas[0].AxisY.Title = "A组电池电压1/V";
                                    this.chart2.Titles[0].Text = "A组电池电压1 (-0.5V,39V)";
                                    this.chart2.Series[0].LegendText = "A组电池电压1";
                                    if (a >= 0.5)
                                    {
                                        label_dy.Text = string.Format("故障");
                                        label_dy.ForeColor = System.Drawing.Color.Red;
                                        if (value_nums[0] >= 28.5 || value_nums[0] <= 27.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "28V母线电压", a);
                                        }
                                        else if (value_nums[1] >= 43 || value_nums[1] <= -0.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "28V负载电流", a);
                                        }
                                        else if (value_nums[2] >= 43 || value_nums[2] <= 41)
                                        {
                                            SaveListboxToTxt(line_sp[0], "42V母线电压", a);
                                        }
                                        else if (value_nums[3] >= 78 || value_nums[3] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "42V负载电流", a);
                                        }
                                        else if (value_nums[4] >= 39 || value_nums[4] <= -0.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组电池电压1", a);
                                        }
                                        else if (value_nums[5] >= 39 || value_nums[5] <= -0.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组电池电压1", a);
                                        }
                                        else if (value_nums[6] >= 110 || value_nums[6] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "太阳电池阵输出电流", a);
                                        }
                                        else if (value_nums[7] >= 39 || value_nums[7] <= -0.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组电池电压2", a);
                                        }
                                        else if (value_nums[8] >= 39 || value_nums[8] <= -0.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组电池电压2", a);
                                        }
                                        else if (value_nums[9] >= 22 || value_nums[9] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "MEA电压", a);
                                        }
                                        else if (value_nums[10] >= 35 || value_nums[10] <= -10)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组电池温度1", a);
                                        }
                                        else if (value_nums[11] >= 35 || value_nums[11] <= -10)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组电池温度2", a);
                                        }
                                        else if (value_nums[12] >= 35 || value_nums[12] <= -10)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组电池温度1", a);
                                        }
                                        else if (value_nums[13] >= 35 || value_nums[13] <= -10)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组电池温度2", a);
                                        }
                                        else if (value_nums[14] >= 35 || value_nums[14] <= -10)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组电池温度4", a);
                                        }
                                        else if (value_nums[15] >= 35 || value_nums[15] <= -10)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组电池温度3", a);
                                        }
                                        else if (value_nums[16] >= 35 || value_nums[16] <= -10)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组电池温度4", a);
                                        }
                                        else if (value_nums[17] >= 55 || value_nums[17] <= -15)
                                        {
                                            SaveListboxToTxt(line_sp[0], "充放电模块温度", a);
                                        }
                                        else if (value_nums[18] >= 55 || value_nums[18] <= -15)
                                        {
                                            SaveListboxToTxt(line_sp[0], "分流模块温度", a);
                                        }
                                        else if (value_nums[19] >= 12 || value_nums[19] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A1充电电流", a);
                                        }
                                        else if (value_nums[20] >= 12 || value_nums[20] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A2充电电流", a);
                                        }
                                        else if (value_nums[21] >= 12 || value_nums[21] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B1充电电流", a);
                                        }
                                        else if (value_nums[22] >= 12 || value_nums[22] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B2充电电流", a);
                                        }
                                        else if (value_nums[23] >= 20 || value_nums[23] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A1放电电流", a);
                                        }
                                        else if (value_nums[24] >= 20 || value_nums[24] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A2放电电流", a);
                                        }
                                        else if (value_nums[25] >= 20 || value_nums[25] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A3放电电流", a);
                                        }
                                        else if (value_nums[26] >= 20 || value_nums[26] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A4放电电流", a);
                                        }
                                        else if (value_nums[27] >= 20 || value_nums[27] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B1放电电流", a);
                                        }
                                        else if (value_nums[28] >= 20 || value_nums[28] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B2放电电流", a);
                                        }
                                        else if (value_nums[29] >= 20 || value_nums[29] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B3放电电流", a);
                                        }
                                        else if (value_nums[30] >= 20 || value_nums[30] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B4放电电流", a);
                                        }
                                        else if (value_nums[31] >= 4.5 || value_nums[31] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组单体1电压", a);
                                        }
                                        else if (value_nums[32] >= 4.5 || value_nums[32] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组单体2电压", a);
                                        }
                                        else if (value_nums[33] >= 4.5 || value_nums[33] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组单体3电压", a);
                                        }
                                        else if (value_nums[34] >= 4.5 || value_nums[34] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组单体4电压", a);
                                        }
                                        else if (value_nums[35] >= 4.5 || value_nums[35] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组单体5电压", a);
                                        }
                                        else if (value_nums[36] >= 4.5 || value_nums[36] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组单体6电压", a);
                                        }
                                        else if (value_nums[37] >= 4.5 || value_nums[37] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组单体7电压", a);
                                        }
                                        else if (value_nums[38] >= 4.5 || value_nums[38] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组单体8电压", a);
                                        }
                                        else if (value_nums[39] >= 4.5 || value_nums[39] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组单体9电压", a);
                                        }
                                        else if (value_nums[40] >= 4.5 || value_nums[40] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组单体1电压", a);
                                        }
                                        else if (value_nums[41] >= 4.5 || value_nums[41] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组单体2电压", a);
                                        }
                                        else if (value_nums[42] >= 4.5 || value_nums[42] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组单体3电压", a);
                                        }
                                        else if (value_nums[43] >= 4.5 || value_nums[43] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组单体4电压", a);
                                        }
                                        else if (value_nums[44] >= 4.5 || value_nums[44] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组单体5电压", a);
                                        }
                                        else if (value_nums[45] >= 4.5 || value_nums[45] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组单体6电压", a);
                                        }
                                        else if (value_nums[46] >= 4.5 || value_nums[46] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组单体7电压", a);
                                        }
                                        else if (value_nums[47] >= 4.5 || value_nums[47] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组单体8电压", a);
                                        }
                                        else if (value_nums[48] >= 4.5 || value_nums[48] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组单体9电压", a);
                                        }
                                    }
                                }
                                //读取“B组电池电压1”的数据并判断是否故障
                                else if (dy_qushi_flag == "B组电池电压1")
                                {
                                    dataQueue_data.Enqueue(value_nums[5]);
                                    this.chart2.ChartAreas[0].AxisY.Title = "B组电池电压1/V";
                                    this.chart2.Titles[0].Text = "B组电池电压1 (-0.5V,39V)";
                                    this.chart2.Series[0].LegendText = "B组电池电压1";
                                    if (a >= 0.5)
                                    {
                                        label_dy.Text = string.Format("故障");
                                        label_dy.ForeColor = System.Drawing.Color.Red;
                                        if (value_nums[0] >= 28.5 || value_nums[0] <= 27.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "28V母线电压", a);
                                        }
                                        else if (value_nums[1] >= 43 || value_nums[1] <= -0.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "28V负载电流", a);
                                        }
                                        else if (value_nums[2] >= 43 || value_nums[2] <= 41)
                                        {
                                            SaveListboxToTxt(line_sp[0], "42V母线电压", a);
                                        }
                                        else if (value_nums[3] >= 78 || value_nums[3] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "42V负载电流", a);
                                        }
                                        else if (value_nums[4] >= 39 || value_nums[4] <= -0.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组电池电压1", a);
                                        }
                                        else if (value_nums[5] >= 39 || value_nums[5] <= -0.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组电池电压1", a);
                                        }
                                        else if (value_nums[6] >= 110 || value_nums[6] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "太阳电池阵输出电流", a);
                                        }
                                        else if (value_nums[7] >= 39 || value_nums[7] <= -0.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组电池电压2", a);
                                        }
                                        else if (value_nums[8] >= 39 || value_nums[8] <= -0.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组电池电压2", a);
                                        }
                                        else if (value_nums[9] >= 22 || value_nums[9] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "MEA电压", a);
                                        }
                                        else if (value_nums[10] >= 35 || value_nums[10] <= -10)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组电池温度1", a);
                                        }
                                        else if (value_nums[11] >= 35 || value_nums[11] <= -10)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组电池温度2", a);
                                        }
                                        else if (value_nums[12] >= 35 || value_nums[12] <= -10)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组电池温度1", a);
                                        }
                                        else if (value_nums[13] >= 35 || value_nums[13] <= -10)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组电池温度2", a);
                                        }
                                        else if (value_nums[14] >= 35 || value_nums[14] <= -10)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组电池温度4", a);
                                        }
                                        else if (value_nums[15] >= 35 || value_nums[15] <= -10)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组电池温度3", a);
                                        }
                                        else if (value_nums[16] >= 35 || value_nums[16] <= -10)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组电池温度4", a);
                                        }
                                        else if (value_nums[17] >= 55 || value_nums[17] <= -15)
                                        {
                                            SaveListboxToTxt(line_sp[0], "充放电模块温度", a);
                                        }
                                        else if (value_nums[18] >= 55 || value_nums[18] <= -15)
                                        {
                                            SaveListboxToTxt(line_sp[0], "分流模块温度", a);
                                        }
                                        else if (value_nums[19] >= 12 || value_nums[19] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A1充电电流", a);
                                        }
                                        else if (value_nums[20] >= 12 || value_nums[20] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A2充电电流", a);
                                        }
                                        else if (value_nums[21] >= 12 || value_nums[21] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B1充电电流", a);
                                        }
                                        else if (value_nums[22] >= 12 || value_nums[22] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B2充电电流", a);
                                        }
                                        else if (value_nums[23] >= 20 || value_nums[23] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A1放电电流", a);
                                        }
                                        else if (value_nums[24] >= 20 || value_nums[24] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A2放电电流", a);
                                        }
                                        else if (value_nums[25] >= 20 || value_nums[25] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A3放电电流", a);
                                        }
                                        else if (value_nums[26] >= 20 || value_nums[26] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A4放电电流", a);
                                        }
                                        else if (value_nums[27] >= 20 || value_nums[27] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B1放电电流", a);
                                        }
                                        else if (value_nums[28] >= 20 || value_nums[28] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B2放电电流", a);
                                        }
                                        else if (value_nums[29] >= 20 || value_nums[29] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B3放电电流", a);
                                        }
                                        else if (value_nums[30] >= 20 || value_nums[30] <= -1)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B4放电电流", a);
                                        }
                                        else if (value_nums[31] >= 4.5 || value_nums[31] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组单体1电压", a);
                                        }
                                        else if (value_nums[32] >= 4.5 || value_nums[32] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组单体2电压", a);
                                        }
                                        else if (value_nums[33] >= 4.5 || value_nums[33] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组单体3电压", a);
                                        }
                                        else if (value_nums[34] >= 4.5 || value_nums[34] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组单体4电压", a);
                                        }
                                        else if (value_nums[35] >= 4.5 || value_nums[35] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组单体5电压", a);
                                        }
                                        else if (value_nums[36] >= 4.5 || value_nums[36] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组单体6电压", a);
                                        }
                                        else if (value_nums[37] >= 4.5 || value_nums[37] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组单体7电压", a);
                                        }
                                        else if (value_nums[38] >= 4.5 || value_nums[38] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组单体8电压", a);
                                        }
                                        else if (value_nums[39] >= 4.5 || value_nums[39] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "A组单体9电压", a);
                                        }
                                        else if (value_nums[40] >= 4.5 || value_nums[40] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组单体1电压", a);
                                        }
                                        else if (value_nums[41] >= 4.5 || value_nums[41] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组单体2电压", a);
                                        }
                                        else if (value_nums[42] >= 4.5 || value_nums[42] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组单体3电压", a);
                                        }
                                        else if (value_nums[43] >= 4.5 || value_nums[43] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组单体4电压", a);
                                        }
                                        else if (value_nums[44] >= 4.5 || value_nums[44] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组单体5电压", a);
                                        }
                                        else if (value_nums[45] >= 4.5 || value_nums[45] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组单体6电压", a);
                                        }
                                        else if (value_nums[46] >= 4.5 || value_nums[46] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组单体7电压", a);
                                        }
                                        else if (value_nums[47] >= 4.5 || value_nums[47] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组单体8电压", a);
                                        }
                                        else if (value_nums[48] >= 4.5 || value_nums[48] <= 2.5)
                                        {
                                            SaveListboxToTxt(line_sp[0], "B组单体9电压", a);
                                        }
                                    }
                                }
                            }
                            line = reader.ReadLine();
                            i += 1;
                        }
                        read_first_dy_num += 5;
                    }
                }
            }
       
        }


        private void button4_Click(object sender, EventArgs e)
        {
            if (abn_state_lstbox.Items == null)
            {
            }
            else  
            {
                
                StreamWriter sw = File.CreateText(string.Format("D://{0}故障.txt",GuzhangText.Replace(":","-")));
                for (int i = 0; i < abn_state_lstbox.Items.Count; i++) 
                {
                    sw.WriteLine(abn_state_lstbox.Items[i]);
                }
                sw.Close();
            
            }
        }

        //保存故障信息
        private void SaveListboxToTxt(string time,string bianliang,double a) 
        {
            abn_state_lstbox.Items.Add("故障时间：" + time);
            abn_state_lstbox.Items.Add("故障部件：动量轮1马达电流");
            abn_state_lstbox.Items.Add("故障等级：" + Math.Round(a, 4) * 100 + "%" + "故障");
            abn_state_lstbox.Items.Add("");
            if (abn_state_lstbox.Items == null)
            {
            }
            else
            {
                //FileStream fs = new FileStream("D://故障记录.txt",FileMode.OpenOrCreate);
                //StreamWriter sw = File.CreateText(string.Format("D://{0}故障.txt", GuzhangText.Replace(":", "-"),true));
                //sw.Write
                StreamWriter sw = new StreamWriter("D://故障记录.txt", true);
                sw.WriteLine("故障时间：" + time);
                sw.WriteLine("故障部件：动量轮1马达电流");
                sw.WriteLine("故障等级：" + Math.Round(a, 4) * 100 + "%" + "故障");
                sw.WriteLine("");
                //sw.Flush();
                sw.Close();
                //fs.Close();

            }
        }

        private void 修改密码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string name = Microsoft.VisualBasic.Interaction.InputBox("请输入用户名!", "");          
            string password = Microsoft.VisualBasic.Interaction.InputBox("请输入密码");

            File.WriteAllText("./in.txt", name.TrimEnd() + "|" + password.TrimEnd(),Encoding.Default);
            MessageBox.Show("修改密码成功!");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Form1 f = new Form1();
            //f.Show();
            //CloseMsg m = new CloseMsg();
            //m.Show();
            //System.Environment.Exit(0);
        }


        //chart1图表区域局部放大
        private void chart1_SelectionRangeChanged(object sender, CursorEventArgs e)
        {
            //无数据时返回
            if (chart1.Series[0].Points.Count == 0)
                return;

            double start_position = 0.0;
            double end_position = 0.0;
            double myInterval = 0.0;
            start_position = e.NewSelectionStart;
            end_position = e.NewSelectionEnd;
            myInterval = Math.Abs(start_position - end_position);
            if (myInterval == 0.0)
                return;

            //X轴视图起点
            chart1.ChartAreas[0].AxisX.ScaleView.Position = Math.Min(start_position, end_position);
            //X轴视图长度
            chart1.ChartAreas[0].AxisX.ScaleView.Size = myInterval;
            //X轴间隔
            if (myInterval < 11.0)
            {
                chart1.ChartAreas[0].AxisX.Interval = 1;
            }
            else
            {
                chart1.ChartAreas[0].AxisY.Interval = Math.Floor(myInterval / 10);
            }
            flag1 = true;
            if (!comboBox1.Items.Contains("Zoom"))
            {
                comboBox1.Items.Add("Zoom");
                comboBox1.SelectedItem = "Zoom";
            }
           
        }

        //局部放大后，恢复视图chart1
        private void chart1_AxisScrollBarClicked(object sender, ScrollBarEventArgs e)
        {
            if (e.ButtonType == ScrollBarButtonType.ZoomReset)
            {
                chart1.ChartAreas[0].AxisX.Interval = 0;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "Zoom")
            {
                flag1 = true;
            }
            else if (comboBox1.SelectedItem.ToString() == "OverView" || comboBox1.SelectedItem.ToString() == "Follow")
            {
                comboBox1.Items.Remove("Zoom");
                flag1 = false;
            }
        }

        private void chart2_SelectionRangeChanged(object sender, CursorEventArgs e)
        {
            //无数据时返回
            if (chart2.Series[0].Points.Count == 0)
                return;

            double start_position = 0.0;
            double end_position = 0.0;
            double myInterval = 0.0;
            start_position = e.NewSelectionStart;
            end_position = e.NewSelectionEnd;
            myInterval = Math.Abs(start_position - end_position);
            if (myInterval == 0.0)
                return;

            //X轴视图起点
            chart2.ChartAreas[0].AxisX.ScaleView.Position = Math.Min(start_position, end_position);
            //X轴视图长度
            chart2.ChartAreas[0].AxisX.ScaleView.Size = myInterval;
            //X轴间隔
            if (myInterval < 11.0)
            {
                chart2.ChartAreas[0].AxisX.Interval = 1;
            }
            else
            {
                chart2.ChartAreas[0].AxisY.Interval = Math.Floor(myInterval / 10);
            }
            flag2 = true;
            if (!comboBox2.Items.Contains("Zoom"))
            {
                comboBox2.Items.Add("Zoom");
                comboBox2.SelectedItem = "Zoom";
            }
        }
        //局部放大后，恢复视图chart2
        private void chart2_AxisScrollBarClicked(object sender, ScrollBarEventArgs e)
        {
            if (e.ButtonType == ScrollBarButtonType.ZoomReset)
            {
                chart2.ChartAreas[0].AxisX.Interval = 0;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem.ToString() == "Zoom")
            {
                flag2 = true;
            }
            else if (comboBox2.SelectedItem.ToString() == "OverView" || comboBox2.SelectedItem.ToString() == "Follow")
            {
                comboBox2.Items.Remove("Zoom");
                flag2 = false;
            }
        }
        

        private void 故障诊断结果文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK) 
            {
                string folder = fbd.SelectedPath;
            }*/
            System.Diagnostics.Process.Start("Explorer.exe", @"D:\");
        }

        private void 选择时段监测ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ST_flag = 1;
            SelectTime ST = new SelectTime();
            ST.Show();
            //Console.WriteLine(ST.mydelegate);
            //ST.mydelegate += new MyDelegate(Add);
        }


        private void 趋势预测模型ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("Explorer.exe", @"D:\");
        }

        private void 退出ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //ST_flag = 1;
            InitChart();
            //this.timer_ST.Start();
            //Console.WriteLine(dll_nianyueri_start_ST);
            
        }

        private void timer_ST_Tick(object sender, EventArgs e)
        {
            //UpdateQueueValue();
            this.chart1.Series[0].Points.Clear();
            for (int i = 0; i < dataQueue_prob_ST.Count; i++)
            {
                this.chart1.Series[0].Points.AddXY((i + 1), dataQueue_prob_ST.ElementAt(i));
                //this.chart1.Series[1].Points.AddXY((i + 1), dataQueue.ElementAt(i+1));
                this.chart1.Series[0].Color = Color.Red;
            }
            //####################chart2################################
            this.chart2.Series[0].Points.Clear();
            for (int i = 0; i < dataQueue_data_ST.Count; i++)
            {
                this.chart2.Series[0].Points.AddXY((i + 1), dataQueue_data_ST.ElementAt(i));
                this.chart2.Series[0].Color = Color.Blue;
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {

        }


        private void dll_rb_CheckedChanged(object sender, EventArgs e)
        {
            InitChart();
            this.timer1.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        //##动量轮#####选择变量#######################################################################
        private void 动量轮1马达电流ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dll_rb.Select();
            qushi_flag = 动量轮1马达电流ToolStripMenuItem.Text;
        }

        private void 动量轮2马达电流ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            dll_rb.Select();
            qushi_flag = 动量轮2马达电流ToolStripMenuItem.Text;
        }

        private void 动量轮4马达电流ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            dll_rb.Select();
            qushi_flag = 动量轮4马达电流ToolStripMenuItem.Text;
        }

        private void 动量轮5马达电流ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            dll_rb.Select();
            qushi_flag = 动量轮5马达电流ToolStripMenuItem.Text;
        }

        private void 动量轮1轴温ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            dll_rb.Select();
            qushi_flag = 动量轮1轴温ToolStripMenuItem.Text;
        }

        private void 动量轮2轴温ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            dll_rb.Select();
            qushi_flag = 动量轮2轴温ToolStripMenuItem.Text;
        }

        private void 动量轮3轴温ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            dll_rb.Select();
            qushi_flag = 动量轮3轴温ToolStripMenuItem.Text;
        }

        private void 动量轮4轴温ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            dll_rb.Select();
            qushi_flag = 动量轮4轴温ToolStripMenuItem.Text;
        }

        private void 动量轮5轴温ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            dll_rb.Select();
            qushi_flag = 动量轮5轴温ToolStripMenuItem.Text;
        }

        private void 动量轮6轴温ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            dll_rb.Select();
            qushi_flag = 动量轮6轴温ToolStripMenuItem.Text;
        }

        //##探测仪#####选择变量#######################################################################
        private void 探测仪东西电机A相电流ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tcy_rb.Select();
            tcy_qushi_flag = 探测仪东西电机A相电流ToolStripMenuItem.Text;
        }

        private void 探测仪东西电机B相电流ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tcy_rb.Select();
            tcy_qushi_flag = 探测仪东西电机B相电流ToolStripMenuItem.Text;
        }

        private void 探测仪东西电机总电流ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tcy_rb.Select();
            tcy_qushi_flag = 探测仪东西电机总电流ToolStripMenuItem.Text;
        }

        private void 探测仪南北电机A相电流ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tcy_rb.Select();
            tcy_qushi_flag = 探测仪南北电机A相电流ToolStripMenuItem.Text;
        }

        private void 探测仪南北电机B相电流ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tcy_rb.Select();
            tcy_qushi_flag = 探测仪南北电机B相电流ToolStripMenuItem.Text;
        }

        private void 探测仪南北电机总电流ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tcy_rb.Select();
            tcy_qushi_flag = 探测仪南北电机总电流ToolStripMenuItem.Text;
        }

        //##辐射计#####选择变量#######################################################################
        private void 辐射计东西总电流ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fsj_rb.Select();
            fsj_qushi_flag = 辐射计东西总电流ToolStripMenuItem.Text;
        }

        private void 辐射计南北总电流ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fsj_rb.Select();
            fsj_qushi_flag = 辐射计南北总电流ToolStripMenuItem.Text;
        }

        //##电源#####选择变量#######################################################################
        private void 母线28V电压ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dy_rb.Select();
            dy_qushi_flag = 母线28V电压ToolStripMenuItem.Text;
        }

        private void 负载28V电流ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dy_rb.Select();
            dy_qushi_flag = 负载28V电流ToolStripMenuItem.Text;
        }

        private void 母线42V电压ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dy_rb.Select();
            dy_qushi_flag = 母线42V电压ToolStripMenuItem.Text;
        }

        private void 负载42V电流ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            dy_rb.Select();
            dy_qushi_flag = 负载42V电流ToolStripMenuItem1.Text;
        }

        private void a组电池电压1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dy_rb.Select();
            dy_qushi_flag = a组电池电压1ToolStripMenuItem.Text;
        }

        private void b组电池电压1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dy_rb.Select();
            dy_qushi_flag = b组电池电压1ToolStripMenuItem.Text;
        }
        //##结束#####选择变量#######################################################################

        //时间戳转换成时间
        private DateTime TimeStampToDateTime(string TimeStamp_l) 
        {
            DateTime otime = new DateTime();
            string t1 = DateTime.UtcNow.ToString("2000-01-01 12:00:00");
            DateTime utc1 = DateTime.Parse(t1).AddTicks(long.Parse(TimeStamp_l) * 10);
            DateTime local1 = utc1.ToLocalTime();//转成本地时间
            otime = local1;
            return otime;
            /*
            string TimeStamp = TimeStamp_l.Substring(0,13); 
            DateTime time = DateTime.MinValue;
            DateTime StartTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1 ));
            try
            {
                time = TimeStamp.Length == 10 ? StartTime.AddSeconds(long.Parse(TimeStamp)/100) : StartTime.AddMilliseconds(long.Parse(TimeStamp)/100);
            }
            catch (Exception ex) 
            {
                return StartTime;
            }
            return time;
            */
        }

        private void 管理员登录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("Explorer.exe", @"E:\vs2012 project\八月最终定稿\十一月出差\");
        }

        //读取指定路径下所有文件名
        public static string[] getFileName(string path) 
        {
            DirectoryInfo root = new DirectoryInfo(path);
            FileSystemInfo[] fsinfos = root.GetFileSystemInfos();
            //string[] txt_name_list = new string[]{};
            List<string> strList = new List<string>();
            foreach (FileSystemInfo fsinfo in fsinfos) 
            {
                
                if (fsinfo is DirectoryInfo)
                {
                    getFileName(fsinfo.Name);
                }
                else 
                {
                    //Console.WriteLine(fsinfo.Name);
                    strList.Add(fsinfo.Name);
                }   
            }
            string[] strArray = strList.ToArray();
            return strArray;
        }

        //###########  读取动量轮文件
        private void read_dll() 
        {
            for (int t = 0; t < dll_file.Length; t++) 
            {
                using (StreamReader reader = new StreamReader("./读取多个文件实验/动量轮/" + dll_file[t]))
                {
                    string line = reader.ReadLine();

                    int i = 0;

                    while ((line = reader.ReadLine()) != null)
                    {
                        if (i >= read_first_num && i <= num + read_first_num)
                        {
                            string[] line_sp = line.Split('\t');
                            double[] value_nums = new double[10];
                            value_nums[0] = double.Parse(line_sp[1]);
                            value_nums[1] = double.Parse(line_sp[2]);
                            value_nums[2] = double.Parse(line_sp[4]);
                            value_nums[3] = double.Parse(line_sp[5]);
                            value_nums[4] = double.Parse(line_sp[7]);
                            value_nums[5] = double.Parse(line_sp[8]);
                            value_nums[6] = double.Parse(line_sp[9]);
                            value_nums[7] = double.Parse(line_sp[10]);
                            value_nums[8] = double.Parse(line_sp[11]);
                            value_nums[9] = double.Parse(line_sp[12]);
                            //读取时间和PLSR计算结果
                            double a = TEST.Class1.test_dll(value_nums);
                          
                            //判断是否故障
                            if (a >= 0.5)
                            {
                                label_dll.Text = string.Format("故障");
                                label_dll.ForeColor = System.Drawing.Color.Red;
                                if (value_nums[0] >= 2.0 || value_nums[0] <= -2.0)
                                {
                                    SaveListboxToTxt(line_sp[0], "动量轮1马达电流", a);
                                }
                                else if (value_nums[1] >= 2.0 || value_nums[1] <= -2.0)
                                {
                                    SaveListboxToTxt(line_sp[0], "动量轮2马达电流", a);
                                }
                                else if (value_nums[2] >= 2.0 || value_nums[2] <= -2.0)
                                {
                                    SaveListboxToTxt(line_sp[0], "动量轮4马达电流", a);
                                }
                                else if (value_nums[3] >= 2.0 || value_nums[3] <= -2.0)
                                {
                                    SaveListboxToTxt(line_sp[0], "动量轮5马达电流", a);
                                }
                                else if (value_nums[4] >= 45.0 || value_nums[4] <= -5.0)
                                {
                                    SaveListboxToTxt(line_sp[0], "动量轮1轴温", a);
                                }
                                else if (value_nums[5] >= 45.0 || value_nums[5] <= -5.0)
                                {
                                    SaveListboxToTxt(line_sp[0], "动量轮2轴温", a);
                                }
                                else if (value_nums[6] >= 45.0 || value_nums[8] <= -5.0)
                                {
                                    SaveListboxToTxt(line_sp[0], "动量轮3轴温", a);
                                }
                                else if (value_nums[7] >= 45.0 || value_nums[7] <= -5.0)
                                {
                                    SaveListboxToTxt(line_sp[0], "动量轮4轴温", a);
                                }
                                else if (value_nums[8] >= 45.0 || value_nums[8] <= -5.0)
                                {
                                    SaveListboxToTxt(line_sp[0], "动量轮5轴温", a);
                                }
                                else if (value_nums[9] >= 45.0 || value_nums[9] <= -5.0)
                                {
                                    SaveListboxToTxt(line_sp[0], "动量轮6轴温", a);
                                }
                            }
                            
                            
                        }
                        line = reader.ReadLine();

                        i += 1;
                    }
                    read_first_num += 5;

                }
            }
        }

        //###########  读取辐射计文件
        private void read_fsj()
        {
            for (int t = 0; t < fsj_file.Length; t++)
            {
                using (StreamReader reader = new StreamReader("./读取多个文件实验/辐射计/" + fsj_file[t]))
                {
                    int i = 0;
                    string line = reader.ReadLine();
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (i >= read_first_num && i < num + read_first_num)
                        {
                            string[] line_sp = line.Split('\t');
                            double[] value_nums = new double[2];
                            value_nums[0] = double.Parse(line_sp[3]);
                            value_nums[1] = double.Parse(line_sp[6]);
                            string time_X = string.Format("{0}", TimeStampToDateTime(line_sp[0]));
                            double a = TEST.Class1.test_fsj(value_nums);
                            //判断是否故障
                            if (fsj_qushi_flag == "辐射计东西总电流")
                            {
                                if (a >= 0.5)
                                {
                                    label_fsj.Text = string.Format("故障");
                                    label_fsj.ForeColor = System.Drawing.Color.Red;
                                    if (value_nums[0] >= 2.0 || value_nums[0] <= -2.0)
                                    {
                                        SaveListboxToTxt(time_X, "辐射计东西总电流", a);
                                    }
                                    else if (value_nums[1] >= 2.0 || value_nums[1] <= -2.0)
                                    {
                                        SaveListboxToTxt(time_X, "辐射计南北总电流", a);
                                    }
                                }
                            }
                        }
                        line = reader.ReadLine();
                        i += 1;
                    }
                    read_first_num += 5;
                }
            }
        }
        //###########  读取探测仪文件
        private void read_tcy()
        {
            for (int t = 0; t < tcy_file.Length; t++)
            {
                using (StreamReader reader = new StreamReader("./读取多个文件实验/探测仪/" + tcy_file[t]))
                {
                    int i = 0;
                    string line = reader.ReadLine();
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (i >= read_first_num && i < num + read_first_num)
                        {
                            string[] line_sp = line.Split('\t');
                            double[] value_nums = new double[6];
                            double[] tcy_value_nums = new double[2];
                            value_nums[0] = double.Parse(line_sp[1]);
                            value_nums[1] = double.Parse(line_sp[2]);
                            value_nums[2] = double.Parse(line_sp[3]);
                            value_nums[3] = double.Parse(line_sp[4]);
                            value_nums[4] = double.Parse(line_sp[5]);
                            value_nums[5] = double.Parse(line_sp[6]);
                            string time_X = string.Format("{0}", TimeStampToDateTime(line_sp[0]));
                            time_X.Replace(" ", "");
                            tcy_value_nums[0] = double.Parse(line_sp[3]);
                            tcy_value_nums[1] = double.Parse(line_sp[6]);
                            double a = TEST.Class1.test_tcy(tcy_value_nums);
                            //判断是否故障
                            if (a >= 0.5)
                            {
                                label_tcy.Text = string.Format("故障");
                                label_tcy.ForeColor = System.Drawing.Color.Red;
                                if (value_nums[0] >= 0.6 || value_nums[0] <= -0.6)
                                {
                                    SaveListboxToTxt(time_X, "探测仪东西电机A相电流", a);
                                }
                                else if (value_nums[1] >= 0.6 || value_nums[1] <= -0.6)
                                {
                                    SaveListboxToTxt(time_X, "探测仪东西电机B相电流", a);
                                }
                                else if (value_nums[2] >= 0.6 || value_nums[2] <= -0.6)
                                {
                                    SaveListboxToTxt(time_X, "探测仪东西电机总电流", a);
                                }
                                else if (value_nums[3] >= 0.6 || value_nums[3] <= -0.6)
                                {
                                    SaveListboxToTxt(time_X, "探测仪南北电机A相电流", a);
                                }
                                else if (value_nums[4] >= 0.6 || value_nums[4] <= -0.6)
                                {
                                    SaveListboxToTxt(time_X, "探测仪南北电机B相电流", a);
                                }
                                else if (value_nums[5] >= 0.6 || value_nums[5] <= -0.6)
                                {
                                    SaveListboxToTxt(time_X, "探测仪南北电机总电流", a);
                                }
                            }
                        }
                        line = reader.ReadLine();
                        i += 1;
                    }

                    read_first_num += 5;
                }
            }
        }
        //###########  读取电源文件
        private void read_dy()
        {
            for (int t = 0; t < dy_file.Length; t++)
            {
                using (StreamReader reader = new StreamReader("./读取多个文件实验/电源/" + dy_file[t]))
                {
                    int i = 0;
                    string line = reader.ReadLine();
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (i >= read_first_dy_num && i < num + read_first_dy_num)
                        {
                            string[] line_sp = line.Split('\t');
                            double[] value_nums = new double[49];
                            value_nums[0] = double.Parse(line_sp[1]);
                            value_nums[1] = double.Parse(line_sp[2]);
                            value_nums[2] = double.Parse(line_sp[3]);
                            value_nums[3] = double.Parse(line_sp[4]);
                            value_nums[4] = double.Parse(line_sp[5]);
                            value_nums[5] = double.Parse(line_sp[6]);
                            value_nums[6] = double.Parse(line_sp[7]);
                            value_nums[7] = double.Parse(line_sp[35]);
                            value_nums[8] = double.Parse(line_sp[36]);
                            value_nums[9] = double.Parse(line_sp[65]);
                            value_nums[10] = double.Parse(line_sp[12]);
                            value_nums[11] = double.Parse(line_sp[13]);
                            value_nums[12] = double.Parse(line_sp[17]);
                            value_nums[13] = double.Parse(line_sp[18]);
                            value_nums[14] = double.Parse(line_sp[39]);
                            value_nums[15] = double.Parse(line_sp[41]);
                            value_nums[16] = double.Parse(line_sp[42]);
                            value_nums[17] = double.Parse(line_sp[158]);
                            value_nums[18] = double.Parse(line_sp[159]);
                            value_nums[19] = double.Parse(line_sp[53]);
                            value_nums[20] = double.Parse(line_sp[54]);
                            value_nums[21] = double.Parse(line_sp[55]);
                            value_nums[22] = double.Parse(line_sp[56]);
                            value_nums[23] = double.Parse(line_sp[57]);
                            value_nums[24] = double.Parse(line_sp[58]);
                            value_nums[25] = double.Parse(line_sp[59]);
                            value_nums[26] = double.Parse(line_sp[60]);
                            value_nums[27] = double.Parse(line_sp[61]);
                            value_nums[28] = double.Parse(line_sp[62]);
                            value_nums[29] = double.Parse(line_sp[63]);
                            value_nums[30] = double.Parse(line_sp[64]);
                            value_nums[31] = double.Parse(line_sp[192]);
                            value_nums[32] = double.Parse(line_sp[193]);
                            value_nums[33] = double.Parse(line_sp[194]);
                            value_nums[34] = double.Parse(line_sp[195]);
                            value_nums[35] = double.Parse(line_sp[196]);
                            value_nums[36] = double.Parse(line_sp[197]);
                            value_nums[37] = double.Parse(line_sp[198]);
                            value_nums[38] = double.Parse(line_sp[199]);
                            value_nums[39] = double.Parse(line_sp[200]);
                            value_nums[40] = double.Parse(line_sp[201]);
                            value_nums[41] = double.Parse(line_sp[202]);
                            value_nums[42] = double.Parse(line_sp[203]);
                            value_nums[43] = double.Parse(line_sp[204]);
                            value_nums[44] = double.Parse(line_sp[205]);
                            value_nums[45] = double.Parse(line_sp[206]);
                            value_nums[46] = double.Parse(line_sp[207]);
                            value_nums[47] = double.Parse(line_sp[208]);
                            value_nums[48] = double.Parse(line_sp[209]);

                            string time_X = line_sp[0];
                            time_X.Replace(" ", "");
                            timeQueue.Enqueue(time_X.Substring(9, 8));
                            double a = TEST.Class1.test_dy(value_nums);
                            //读取“28V母线电压”的数据并判断是否故障
                            if (dy_qushi_flag == "28V母线电压")
                            {
                                if (a >= 0.5)
                                {
                                    label_dy.Text = string.Format("故障");
                                    label_dy.ForeColor = System.Drawing.Color.Red;
                                    if (value_nums[0] >= 28.5 || value_nums[0] <= 27.5)
                                    {
                                        SaveListboxToTxt(line_sp[0], "28V母线电压", a);
                                    }
                                    else if (value_nums[1] >= 43 || value_nums[1] <= -0.5)
                                    {
                                        SaveListboxToTxt(line_sp[0], "28V负载电流", a);
                                    }
                                    else if (value_nums[2] >= 43 || value_nums[2] <= 41)
                                    {
                                        SaveListboxToTxt(line_sp[0], "42V母线电压", a);
                                    }
                                    else if (value_nums[3] >= 78 || value_nums[3] <= -1)
                                    {
                                        SaveListboxToTxt(line_sp[0], "42V负载电流", a);
                                    }
                                    else if (value_nums[4] >= 39 || value_nums[4] <= -0.5)
                                    {
                                        SaveListboxToTxt(line_sp[0], "A组电池电压1", a);
                                    }
                                    else if (value_nums[5] >= 39 || value_nums[5] <= -0.5)
                                    {
                                        SaveListboxToTxt(line_sp[0], "B组电池电压1", a);
                                    }
                                    else if (value_nums[6] >= 110 || value_nums[6] <= -1)
                                    {
                                        SaveListboxToTxt(line_sp[0], "太阳电池阵输出电流", a);
                                    }
                                    else if (value_nums[7] >= 39 || value_nums[7] <= -0.5)
                                    {
                                        SaveListboxToTxt(line_sp[0], "A组电池电压2", a);
                                    }
                                    else if (value_nums[8] >= 39 || value_nums[8] <= -0.5)
                                    {
                                        SaveListboxToTxt(line_sp[0], "B组电池电压2", a);
                                    }
                                    else if (value_nums[9] >= 22 || value_nums[9] <= -1)
                                    {
                                        SaveListboxToTxt(line_sp[0], "MEA电压", a);
                                    }
                                    else if (value_nums[10] >= 35 || value_nums[10] <= -10)
                                    {
                                        SaveListboxToTxt(line_sp[0], "B组电池温度1", a);
                                    }
                                    else if (value_nums[11] >= 35 || value_nums[11] <= -10)
                                    {
                                        SaveListboxToTxt(line_sp[0], "B组电池温度2", a);
                                    }
                                    else if (value_nums[12] >= 35 || value_nums[12] <= -10)
                                    {
                                        SaveListboxToTxt(line_sp[0], "A组电池温度1", a);
                                    }
                                    else if (value_nums[13] >= 35 || value_nums[13] <= -10)
                                    {
                                        SaveListboxToTxt(line_sp[0], "A组电池温度2", a);
                                    }
                                    else if (value_nums[14] >= 35 || value_nums[14] <= -10)
                                    {
                                        SaveListboxToTxt(line_sp[0], "B组电池温度4", a);
                                    }
                                    else if (value_nums[15] >= 35 || value_nums[15] <= -10)
                                    {
                                        SaveListboxToTxt(line_sp[0], "A组电池温度3", a);
                                    }
                                    else if (value_nums[16] >= 35 || value_nums[16] <= -10)
                                    {
                                        SaveListboxToTxt(line_sp[0], "A组电池温度4", a);
                                    }
                                    else if (value_nums[17] >= 55 || value_nums[17] <= -15)
                                    {
                                        SaveListboxToTxt(line_sp[0], "充放电模块温度", a);
                                    }
                                    else if (value_nums[18] >= 55 || value_nums[18] <= -15)
                                    {
                                        SaveListboxToTxt(line_sp[0], "分流模块温度", a);
                                    }
                                    else if (value_nums[19] >= 12 || value_nums[19] <= -1)
                                    {
                                        SaveListboxToTxt(line_sp[0], "A1充电电流", a);
                                    }
                                    else if (value_nums[20] >= 12 || value_nums[20] <= -1)
                                    {
                                        SaveListboxToTxt(line_sp[0], "A2充电电流", a);
                                    }
                                    else if (value_nums[21] >= 12 || value_nums[21] <= -1)
                                    {
                                        SaveListboxToTxt(line_sp[0], "B1充电电流", a);
                                    }
                                    else if (value_nums[22] >= 12 || value_nums[22] <= -1)
                                    {
                                        SaveListboxToTxt(line_sp[0], "B2充电电流", a);
                                    }
                                    else if (value_nums[23] >= 20 || value_nums[23] <= -1)
                                    {
                                        SaveListboxToTxt(line_sp[0], "A1放电电流", a);
                                    }
                                    else if (value_nums[24] >= 20 || value_nums[24] <= -1)
                                    {
                                        SaveListboxToTxt(line_sp[0], "A2放电电流", a);
                                    }
                                    else if (value_nums[25] >= 20 || value_nums[25] <= -1)
                                    {
                                        SaveListboxToTxt(line_sp[0], "A3放电电流", a);
                                    }
                                    else if (value_nums[26] >= 20 || value_nums[26] <= -1)
                                    {
                                        SaveListboxToTxt(line_sp[0], "A4放电电流", a);
                                    }
                                    else if (value_nums[27] >= 20 || value_nums[27] <= -1)
                                    {
                                        SaveListboxToTxt(line_sp[0], "B1放电电流", a);
                                    }
                                    else if (value_nums[28] >= 20 || value_nums[28] <= -1)
                                    {
                                        SaveListboxToTxt(line_sp[0], "B2放电电流", a);
                                    }
                                    else if (value_nums[29] >= 20 || value_nums[29] <= -1)
                                    {
                                        SaveListboxToTxt(line_sp[0], "B3放电电流", a);
                                    }
                                    else if (value_nums[30] >= 20 || value_nums[30] <= -1)
                                    {
                                        SaveListboxToTxt(line_sp[0], "B4放电电流", a);
                                    }
                                    else if (value_nums[31] >= 4.5 || value_nums[31] <= 2.5)
                                    {
                                        SaveListboxToTxt(line_sp[0], "A组单体1电压", a);
                                    }
                                    else if (value_nums[32] >= 4.5 || value_nums[32] <= 2.5)
                                    {
                                        SaveListboxToTxt(line_sp[0], "A组单体2电压", a);
                                    }
                                    else if (value_nums[33] >= 4.5 || value_nums[33] <= 2.5)
                                    {
                                        SaveListboxToTxt(line_sp[0], "A组单体3电压", a);
                                    }
                                    else if (value_nums[34] >= 4.5 || value_nums[34] <= 2.5)
                                    {
                                        SaveListboxToTxt(line_sp[0], "A组单体4电压", a);
                                    }
                                    else if (value_nums[35] >= 4.5 || value_nums[35] <= 2.5)
                                    {
                                        SaveListboxToTxt(line_sp[0], "A组单体5电压", a);
                                    }
                                    else if (value_nums[36] >= 4.5 || value_nums[36] <= 2.5)
                                    {
                                        SaveListboxToTxt(line_sp[0], "A组单体6电压", a);
                                    }
                                    else if (value_nums[37] >= 4.5 || value_nums[37] <= 2.5)
                                    {
                                        SaveListboxToTxt(line_sp[0], "A组单体7电压", a);
                                    }
                                    else if (value_nums[38] >= 4.5 || value_nums[38] <= 2.5)
                                    {
                                        SaveListboxToTxt(line_sp[0], "A组单体8电压", a);
                                    }
                                    else if (value_nums[39] >= 4.5 || value_nums[39] <= 2.5)
                                    {
                                        SaveListboxToTxt(line_sp[0], "A组单体9电压", a);
                                    }
                                    else if (value_nums[40] >= 4.5 || value_nums[40] <= 2.5)
                                    {
                                        SaveListboxToTxt(line_sp[0], "B组单体1电压", a);
                                    }
                                    else if (value_nums[41] >= 4.5 || value_nums[41] <= 2.5)
                                    {
                                        SaveListboxToTxt(line_sp[0], "B组单体2电压", a);
                                    }
                                    else if (value_nums[42] >= 4.5 || value_nums[42] <= 2.5)
                                    {
                                        SaveListboxToTxt(line_sp[0], "B组单体3电压", a);
                                    }
                                    else if (value_nums[43] >= 4.5 || value_nums[43] <= 2.5)
                                    {
                                        SaveListboxToTxt(line_sp[0], "B组单体4电压", a);
                                    }
                                    else if (value_nums[44] >= 4.5 || value_nums[44] <= 2.5)
                                    {
                                        SaveListboxToTxt(line_sp[0], "B组单体5电压", a);
                                    }
                                    else if (value_nums[45] >= 4.5 || value_nums[45] <= 2.5)
                                    {
                                        SaveListboxToTxt(line_sp[0], "B组单体6电压", a);
                                    }
                                    else if (value_nums[46] >= 4.5 || value_nums[46] <= 2.5)
                                    {
                                        SaveListboxToTxt(line_sp[0], "B组单体7电压", a);
                                    }
                                    else if (value_nums[47] >= 4.5 || value_nums[47] <= 2.5)
                                    {
                                        SaveListboxToTxt(line_sp[0], "B组单体8电压", a);
                                    }
                                    else if (value_nums[48] >= 4.5 || value_nums[48] <= 2.5)
                                    {
                                        SaveListboxToTxt(line_sp[0], "B组单体9电压", a);
                                    }
                                }
                            }
                        }
                        line = reader.ReadLine();
                        i += 1;
                    }
                    read_first_dy_num += 5;

                }
            }
        }

        private void 软件介绍ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("Explorer.exe", @".\软件说明书\");
        }

        //人工剔除故障数据
        private double FindFaultData(double high, double low, double data) 
        {
            if (data >= high * 1000) 
            {
                return (low + high) / 2;
            }
            return data;
 
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
