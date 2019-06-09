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
    public partial class RealTimeDisplay : Form
    {
        private Queue<double> dllDataQueue_prob = new Queue<double>(100);      //监测故障概率
        private Queue<string> dllTimeQueue = new Queue<string>(100);          //横坐标时间
        private Queue<double> dyDataQueue_prob = new Queue<double>(100);      //监测故障概率
        private Queue<string> dyTimeQueue = new Queue<string>(100);          //横坐标时间
        private Queue<double> fsjDataQueue_prob = new Queue<double>(100);      //监测故障概率
        private Queue<string> fsjTimeQueue = new Queue<string>(100);          //横坐标时间
        private Queue<double> tcyDataQueue_prob = new Queue<double>(100);      //监测故障概率
        private Queue<string> tcyTimeQueue = new Queue<string>(100);          //横坐标时间

        private Queue<double> dataQueue_prob_ST = new Queue<double>(100);   //选择时间段内的故障概率
        private Queue<double> dataQueue_data_ST = new Queue<double>(100);  //选择时间段内监测数据

        private string GuzhangText = "";

        private int num = 5;//每次删除增加几个点

        private int read_first_num = 100;//读取txt文件最先开始读的行数
        private int read_first_dy_num = 300;//电源文件最开始读取的行数

        private int dllCount = 10;
        private int dyCount = 10;
        private int fsjCount = 10;
        private int tcyCount = 10;

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
        //System.Windows.Forms.Timer chartTimer = new System.Windows.Forms.Timer();
        //UpdateQueueValue();
        public RealTimeDisplay()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;

            //################################chart1###############################
            chart1.Series[0].ChartType = SeriesChartType.Line;
            //chart1.Series[0].IsValueShownAsLabel = false;//图上不显示数据点的值
            //chart1.Series[0].ToolTip = "#VALX,#VALY";//鼠标停留在数据点上，显示XY值
            //chart1.Series[0].Label = "#VAL";
            //DateTime time = DateTime.Now;
            //chartTimer.Interval = 1000;
            //chartTimer.Tick += chartTimer_Tick;
            InitChart1();
            InitChart2();
            chartTimer.Start();
            DyTimer.Start();
            //UpdateQueueValue();
            //this.timer1.Start();

        }

        private void InitChart1()
        {

            DateTime time = DateTime.Now;
            chartTimer.Interval = 1500;
            chartTimer.Tick += chartTimer_Tick;
            //chartTimer. = 
            //chart1.DoubleClick += chartDemo_DoubleClick;


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


            Series series = chart1.Series[0];
            series.ChartType = SeriesChartType.Spline;

            //设置图表显示样式
            this.chart1.ChartAreas[0].AxisY.Minimum = -0.2;
            this.chart1.ChartAreas[0].AxisY.Maximum = 1;
            //this.chart1.ChartAreas[0].AxisY.LabelStyle.Format = "0%";
            this.chart1.ChartAreas[0].AxisX.Interval = 2;
            this.chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.Silver;
            this.chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.Silver;
            this.chart1.ChartAreas[0].AxisX.Title = "采样点";
            this.chart1.ChartAreas[0].AxisX.TitleForeColor = System.Drawing.Color.Red;
            this.chart1.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.chart1.ChartAreas[0].AxisY.Title = "故障概率";
            this.chart1.ChartAreas[0].AxisY.TitleForeColor = System.Drawing.Color.Red;
            this.chart1.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.chart1.ChartAreas[0].AxisX.ScaleView.Size = 20;
            this.chart1.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = false;
            this.chart1.ChartAreas[0].AxisX.ScrollBar.Enabled = false;
            

            //this.chart1.ChartAreas[0].AxisX.LabelStyle.Format = "HH:mm:ss";

            this.chart1.ChartAreas[0].CursorX.IsUserEnabled = true;
            this.chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            this.chart1.ChartAreas[0].CursorX.SelectionColor = Color.SkyBlue;
            this.chart1.ChartAreas[0].CursorY.IsUserEnabled = true;
            this.chart1.ChartAreas[0].CursorY.AutoScroll = true;
            this.chart1.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;
            this.chart1.ChartAreas[0].CursorY.SelectionColor = Color.SkyBlue;

            this.chart1.ChartAreas[0].CursorX.IntervalType = DateTimeIntervalType.Auto;
            this.chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = false;
            this.chart1.ChartAreas[0].AxisX.ScrollBar.ButtonStyle = ScrollBarButtonStyles.All;//启用X轴滚动条按钮

            this.chart1.ChartAreas[0].AxisX.IntervalOffset = 2.00D;

            //this.chart1.ChartAreas[0].BackColor = Color.AliceBlue;                      //背景色
            //this.chart1.ChartAreas[0].BackSecondaryColor = Color.White;                 //渐变背景色
            //this.chart1.ChartAreas[0].BackGradientStyle = GradientStyle.TopBottom;      //渐变方式
            //this.chart1.ChartAreas[0].BackHatchStyle = ChartHatchStyle.None;            //背景阴影
            //this.chart1.ChartAreas[0].BorderDashStyle = ChartDashStyle.NotSet;          //边框线样式
            //this.chart1.ChartAreas[0].BorderWidth = 1;                                  //边框宽度
            //this.chart1.ChartAreas[0].BorderColor = Color.Black;


            this.chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
            this.chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = true;



            //设置标题
            this.chart1.Titles.Clear();
            this.chart1.Titles.Add("S01");
            this.chart1.Titles[0].Text = "动量轮故障监测结果显示";
            this.chart1.Titles[0].ForeColor = Color.RoyalBlue;
            this.chart1.Titles[0].Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);

            //设置图表显示样式
            this.chart1.Series[0].Color = Color.Red;
            this.chart1.Series[0].Points.Clear();

            
        }

        private void InitChart2()
        {

            DateTime time = DateTime.Now;
            DyTimer.Interval = 1500;
            DyTimer.Tick += DyTimer_Tick;
            //chartTimer. = 
            //chart1.DoubleClick += chartDemo_DoubleClick;


            //##############################chart1##############################################
            //定义图表区域
            this.chart2.ChartAreas.Clear();
            ChartArea chartArea1 = new ChartArea("C1");
            this.chart2.ChartAreas.Add(chartArea1);
            //定义存储和显示点的容器
            this.chart2.Series.Clear();
            Series series1 = new Series("故障概率曲线");
            //Series series_yuzhi = new Series("故障警戒线");
            series1.ChartArea = "C1";
            //series_yuzhi.ChartArea = "C1";
            this.chart2.Series.Add(series1);
            //this.chart1.Series.Add(series_yuzhi);


            Series series = chart2.Series[0];
            series.ChartType = SeriesChartType.Spline;

            //设置图表显示样式
            this.chart2.ChartAreas[0].AxisY.Minimum = -0.2;
            this.chart2.ChartAreas[0].AxisY.Maximum = 1;
            //this.chart1.ChartAreas[0].AxisY.LabelStyle.Format = "0%";
            this.chart2.ChartAreas[0].AxisX.Interval = 2;
            this.chart2.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.Silver;
            this.chart2.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.Silver;
            this.chart2.ChartAreas[0].AxisX.Title = "采样点";
            this.chart2.ChartAreas[0].AxisX.TitleForeColor = System.Drawing.Color.Red;
            this.chart2.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.chart2.ChartAreas[0].AxisY.Title = "故障概率";
            this.chart2.ChartAreas[0].AxisY.TitleForeColor = System.Drawing.Color.Red;
            this.chart2.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.chart2.ChartAreas[0].AxisX.ScaleView.Size = 20;
            this.chart2.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = false;
            this.chart2.ChartAreas[0].AxisX.ScrollBar.Enabled = false;


            this.chart2.ChartAreas[0].AxisX.LabelStyle.Format = "HH:mm:ss";

            this.chart2.ChartAreas[0].CursorX.IsUserEnabled = true;
            this.chart2.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            this.chart2.ChartAreas[0].CursorX.SelectionColor = Color.SkyBlue;
            this.chart2.ChartAreas[0].CursorY.IsUserEnabled = true;
            this.chart2.ChartAreas[0].CursorY.AutoScroll = true;
            this.chart2.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;
            this.chart2.ChartAreas[0].CursorY.SelectionColor = Color.SkyBlue;

            this.chart2.ChartAreas[0].CursorX.IntervalType = DateTimeIntervalType.Auto;
            this.chart2.ChartAreas[0].AxisX.ScaleView.Zoomable = false;
            this.chart2.ChartAreas[0].AxisX.ScrollBar.ButtonStyle = ScrollBarButtonStyles.All;//启用X轴滚动条按钮

            //this.chart1.ChartAreas[0].BackColor = Color.AliceBlue;                      //背景色
            //this.chart1.ChartAreas[0].BackSecondaryColor = Color.White;                 //渐变背景色
            //this.chart1.ChartAreas[0].BackGradientStyle = GradientStyle.TopBottom;      //渐变方式
            //this.chart1.ChartAreas[0].BackHatchStyle = ChartHatchStyle.None;            //背景阴影
            //this.chart1.ChartAreas[0].BorderDashStyle = ChartDashStyle.NotSet;          //边框线样式
            //this.chart1.ChartAreas[0].BorderWidth = 1;                                  //边框宽度
            //this.chart1.ChartAreas[0].BorderColor = Color.Black;


            this.chart2.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
            this.chart2.ChartAreas[0].AxisY.MajorGrid.Enabled = true;



            //设置标题
            this.chart2.Titles.Clear();
            this.chart2.Titles.Add("S01");
            this.chart2.Titles[0].Text = "电源故障监测结果显示";
            this.chart2.Titles[0].ForeColor = Color.RoyalBlue;
            this.chart2.Titles[0].Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);

            //设置图表显示样式
            this.chart2.Series[0].Color = Color.Red;
            this.chart2.Series[0].Points.Clear();


        }

        void chartTimer_Tick(object sender, EventArgs e)
        {
            DllUpdateQueueValue();
        }

        void DyTimer_Tick(object sender, EventArgs e)
        {
            DyUpdateQueueValue();
        }



        private void DllUpdateQueueValue()
        {
            System.IO.StreamReader file = new System.IO.StreamReader(@"./读取多个文件实验/动量轮/2017_12_18动量轮.txt");
            string line;
            int dllTempCount = 0;
            dllCount++;

            if(dllDataQueue_prob.Count > 20)
            {
                dllDataQueue_prob.Dequeue();
                dllTimeQueue.Dequeue();
            }

            while ((line = file.ReadLine()) != null)
            {
                dllTempCount++;
                if (dllCount == dllTempCount)
                {
                    string[] line_sp = line.Split('\t');
                    double[] value_nums = new double[10];
                    value_nums[0] = FindFaultData(2.0, -2.0, double.Parse(line_sp[1]));
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
                    DllTime.Text = string.Format("{0}", line_sp[0]);

                    string time_X = line_sp[0];
                    time_X.Replace(" ", "");
                    dllTimeQueue.Enqueue(time_X.Substring(9, 8));

                    double a = TEST.Class1.test_dll(value_nums);
                    dllDataQueue_prob.Enqueue(Math.Round(a, 4));
                    //dataQueue_prob.Enqueue(value_nums[0]);

                    this.chart1.Series[0].Points.Clear();
                    for (int j = 0; j < dllDataQueue_prob.Count; j++)
                    {
                        this.chart1.Series[0].Points.AddXY(dllTimeQueue.ElementAt(j), dllDataQueue_prob.ElementAt(j));
                    }
                    break;
                }
            }

        }

        private void DyUpdateQueueValue()
        {
            
            System.IO.StreamReader file = new System.IO.StreamReader(@"./读取多个文件实验/电源/2017_12_19电源.txt");
            string line;
            int dyTempCount = 0;
            dyCount++;

            if (dyDataQueue_prob.Count > 20)
            {
                dyDataQueue_prob.Dequeue();
                dyTimeQueue.Dequeue();
            }

            while ((line = file.ReadLine()) != null)
            {
                dyTempCount++;
                if (2*dyCount == dyTempCount)
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

                    //读取时间和PLSR计算结果
                    DyTime.Text = string.Format("{0}", line_sp[0]);

                    string time_X = line_sp[0];
                    time_X.Replace(" ", "");
                    dyTimeQueue.Enqueue(time_X.Substring(9, 8));

                    double a = TEST.Class1.test_dll(value_nums);
                    //dyDataQueue_prob.Enqueue(Math.Round(a, 4));
                    dyDataQueue_prob.Enqueue(a-1.0);
                    //dataQueue_prob.Enqueue(value_nums[0]);

                    this.chart2.Series[0].Points.Clear();
                    for (int j = 0; j < dyDataQueue_prob.Count; j++)
                    {
                        this.chart2.Series[0].Points.AddXY(dyTimeQueue.ElementAt(j), dyDataQueue_prob.ElementAt(j));
                    }
                    break;
                }
            }

        }

        public void Delay()
        {
            long i = 0;
            long j = 0;
            long iMax = 200000000;
            while (i < iMax)
            {
                while (j < iMax)
                {
                    j++;
                }
                i++;
            }
        }

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

        private DateTime TimeStampToDateTime(string TimeStamp_l)
        {
            DateTime otime = new DateTime();
            string t1 = DateTime.UtcNow.ToString("2000-01-01 12:00:00");
            DateTime utc1 = DateTime.Parse(t1).AddTicks(long.Parse(TimeStamp_l) * 10);
            DateTime local1 = utc1.ToLocalTime();//转成本地时间
            otime = local1;
            return otime;

        }
        //人工剔除故障数据
        private double FindFaultData(double high, double low, double data)
        {
            //if (data >= high * 1000)
            //{
            //    return (low + high) / 2;
            //}
            return data;

        }

        private void RealTimeDisplay_Load(object sender, EventArgs e)
        {
            //UpdateQueueValue();
        }

        private void RealTimeDisplay_Shown(object sender, EventArgs e)
        {
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
                ////################################chart1###############################
                //chart1.Series[0].ChartType = SeriesChartType.Line;
                //chart1.Series[0].IsValueShownAsLabel = false;//图上不显示数据点的值
                //chart1.Series[0].ToolTip = "#VALX,#VALY";//鼠标停留在数据点上，显示XY值
                //chart1.Series[0].Label = "#VAL";
                //InitChart();
                //UpdateQueueValue();
            
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void chart4_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
