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
using satellite;
using TEST;
using System.Threading;

namespace SatelliteSoftwareIF
{
    public partial class Form_ST : Form
    {
        //声明年月日时分的全局变量
        public string dll_nianyueri_start_ST = "";
        public string dll_shifen_start_ST = "";
        public string dll_nianyueri_end_ST = "";
        public string dll_shifen_end_ST = "";

        public int dll_nianyueri_start_int;
        public int dll_nianyueri_end_int;
        public string dll_shifen_start_string;
        public string dll_shifen_end_string;

        //声明部件和变量
        string msg = "";
        public string bujian_flag = "";
        public string bianliang_flag = "";
        int ST_flag = 0;//是否画图的标志
        //存储读取txt文件的变量
        public string[] dll_file = getFileName("./读取多个文件实验/动量轮");
        public string[] fsj_file = getFileName("./读取多个文件实验/辐射计");
        public string[] tcy_file = getFileName("./读取多个文件实验/探测仪");
        public string[] dy_file = getFileName("./读取多个文件实验/电源");

        private int read_first_num = 100;//读取txt文件最先开始读的行数
        private int count_temp_txt_readline = 0;//当前txt文本内的数据行数
        private int count_txt_readline = 0;//当前时间范围内的数据行数
        private int count_config_chartpoint = 800;//图表的元素个数
        //   FormMessage fm = new FormMessage();

        private DataTable dt_pro = new DataTable();//存储故障概率值
        private DataTable dt_data = new DataTable();//存储变量值
        public Form_ST(string s1, string s2, string s3, string s4, string s5, string s6)
        {
            InitializeComponent();
            this.dll_nianyueri_start_ST = s1;
            this.dll_shifen_start_ST = s2;
            this.dll_nianyueri_end_ST = s3;
            this.dll_shifen_end_ST = s4;
            this.bujian_flag = s5;
            this.bianliang_flag = s6;

            this.dll_nianyueri_start_int = int.Parse(s1);
            this.dll_nianyueri_end_int = int.Parse(s3);
            this.dll_shifen_start_string = s2;
            this.dll_shifen_end_string = s4;
            
            inti_table();//初始化图表
            //label_progress.Text = "处理进度：";
            Thread td = new Thread(readData);
            td.Start();
        }
        #region 新函数
        //初始化图表
        private void inti_table()
        {
            //int t = 0;

            dt_pro.Columns.Add("X_Pro_Value", typeof(string));
            dt_pro.Columns.Add("Y_Pro_Value", typeof(double));

            dt_data.Columns.Add("X_Data_Value", typeof(string));
            dt_data.Columns.Add("Y_Data_Value", typeof(double));


            //######################################故障概率值画图###########################
            chart_pro.DataSource = dt_pro;//读取故障概率值画图
            chart_pro.Series["Series1"].XValueMember = "X_Pro_Value";
            chart_pro.Series["Series1"].YValueMembers = "Y_Pro_Value";
            chart_pro.Series["Series1"].ChartType = SeriesChartType.Line;
            chart_pro.ChartAreas[0].AxisY.LabelStyle.Format = "";
            //设置图表显示样式
            this.chart_pro.Series["Series1"].IsVisibleInLegend = false;
            this.chart_pro.Series["Series1"].Color = Color.Red;
            this.chart_pro.Series["Series1"].BorderWidth = 1;
            this.chart_pro.ChartAreas[0].AxisY.Minimum = 0;
            this.chart_pro.ChartAreas[0].AxisY.Maximum = 1;
            this.chart_pro.ChartAreas[0].AxisY.LabelStyle.Format = "0%";
            this.chart_pro.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.Silver;
            this.chart_pro.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.Silver;
            this.chart_pro.ChartAreas[0].AxisX.IsMarginVisible = false;//横坐标显示完整
            //横纵坐标样式
            this.chart_pro.ChartAreas[0].AxisX.Title = "时间";
            this.chart_pro.ChartAreas[0].AxisX.TitleForeColor = System.Drawing.Color.Red;
            this.chart_pro.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.chart_pro.ChartAreas[0].AxisY.Title = "故障概率";
            this.chart_pro.ChartAreas[0].AxisY.TitleForeColor = System.Drawing.Color.Red;
            this.chart_pro.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            //设置标题
            this.chart_pro.Titles.Clear();
            this.chart_pro.Titles.Add("S01");
            this.chart_pro.Titles[0].Text = "选时段故障诊断结果";
            this.chart_pro.Titles[0].ForeColor = Color.RoyalBlue;
            this.chart_pro.Titles[0].Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            //##############################################################################################
            //######################################故障概率值画图###########################
            chart_data.DataSource = dt_data;//读取故障概率值画图
            chart_data.Series["Series1"].XValueMember = "X_Data_Value";
            chart_data.Series["Series1"].YValueMembers = "Y_Data_Value";
            chart_data.Series["Series1"].ChartType = SeriesChartType.Line;
            chart_data.ChartAreas[0].AxisY.LabelStyle.Format = "";
            //设置图表显示样式
            this.chart_data.Series["Series1"].IsVisibleInLegend = false;
            this.chart_data.Series["Series1"].Color = Color.Blue;
            this.chart_data.Series["Series1"].BorderWidth = 1;
            this.chart_data.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.Silver;
            this.chart_data.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.Silver;
            this.chart_data.ChartAreas[0].AxisX.IsMarginVisible = false;
            //横纵坐标样式
            this.chart_data.ChartAreas[0].AxisX.Title = "时间";
            this.chart_data.ChartAreas[0].AxisX.TitleForeColor = System.Drawing.Color.Blue;
            this.chart_data.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.chart_data.ChartAreas[0].AxisY.Title = string.Format("{0}", bianliang_flag);
            this.chart_data.ChartAreas[0].AxisY.TitleForeColor = System.Drawing.Color.Blue;
            this.chart_data.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            //设置标题
            this.chart_data.Titles.Clear();
            this.chart_data.Titles.Add("S01");
            this.chart_data.Titles[0].Text = "该时段内变量趋势";
            this.chart_data.Titles[0].ForeColor = Color.RoyalBlue;
            this.chart_data.Titles[0].Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            //局部放大设置
            chart_data.Series[0].ChartType = SeriesChartType.Line;
            chart_data.Series[0].IsValueShownAsLabel = false;//图上不显示数据点的值
            chart_data.Series[0].ToolTip = "#VALX,#VALY";//鼠标停留在数据点上，显示XY值

            chart_data.ChartAreas[0].CursorY.IsUserEnabled = true;
            chart_data.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;
            chart_data.ChartAreas[0].CursorY.LineColor = Color.Pink;
            chart_data.ChartAreas[0].CursorY.IntervalType = DateTimeIntervalType.Auto;
            chart_data.ChartAreas[0].AxisY.ScaleView.Zoomable = false;
            chart_data.ChartAreas[0].AxisY.ScrollBar.ButtonStyle = ScrollBarButtonStyles.All;
            //##############################################################################################
        }
        //绘图函数
        private void tabledraw()
        {

            chart_pro.DataSource = dt_pro;//读取故障概率值画图
            chart_pro.Series["Series1"].XValueMember = "X_Pro_Value";
            chart_pro.Series["Series1"].YValueMembers = "Y_Pro_Value";
            chart_pro.Series["Series1"].ChartType = SeriesChartType.Line;
            chart_pro.ChartAreas[0].AxisY.LabelStyle.Format = "";


            //######################################故障概率值画图###########################
            chart_data.DataSource = dt_data;//读取故障概率值画图
            chart_data.Series["Series1"].XValueMember = "X_Data_Value";
            chart_data.Series["Series1"].YValueMembers = "Y_Data_Value";
            chart_data.Series["Series1"].ChartType = SeriesChartType.Line;
            chart_data.ChartAreas[0].AxisY.LabelStyle.Format = "";


        }
        //消息枚举与消息显示函数
        enum progress
        {
            waitcount,
            endcount,
            waithandle,
            endhandle,
            Error_noline
        }
        private void ShowMessage(progress progressoption)
        {
            /*
            switch (progressoption)
            {
                case progress.waitcount: label_dll.Invoke(new EventHandler(delegate { label_progress.Text+="\n"+ string.Format("正在计算数据量，请等待"); })); break;
                case progress.endcount: label_dll.Invoke(new EventHandler(delegate { label_progress.Text +="\n"+ string.Format("共有" + count_txt_readline + "条数据"); })); break;
                case progress.waithandle: label_dll.Invoke(new EventHandler(delegate { label_progress.Text+="\n" +string.Format("正在处理数据，请等待"); })); break;
                case progress.endhandle: label_dll.Invoke(new EventHandler(delegate { label_progress.Text +="\n"+ string.Format("处理完毕，开始制表"); })); break;

                case progress.Error_noline: MessageBox.Show("无数据！请检查所选时间范围内，是否有对应数据"); break;
            }
            */
            switch (progressoption)
            {
                case progress.waitcount: MessageBox.Show("正在计算数据量，请等待"); break;
                case progress.endcount: MessageBox.Show("共有" + count_txt_readline + "条数据"); break;
                case progress.waithandle: MessageBox.Show("正在处理数据，请等待"); break;
                case progress.endhandle: MessageBox.Show("处理完毕，开始制表"); break;

                case progress.Error_noline: MessageBox.Show("无数据！请检查所选时间范围内，是否有对应数据"); break;
            }
        }
        #endregion


        #region  计数器模块
        private void getlinecountdll()
        {
            for (int t = 0; t < dll_file.Length; t++)//循环txt文件的读取
            {
                using (StreamReader reader2 = new StreamReader("./读取多个文件实验/动量轮/" + dll_file[t]))
                {
                    string line2 = reader2.ReadLine();
                    // if (line2 == null) { MessageBox.Show("this file error"); }

                    count_temp_txt_readline = 0;
                    int i = 0;
                    for (; ; i++)
                    {
                        if (i >= read_first_num)//有效读取范围
                        {
                            //  MessageBox.Show(line2.ToString());
                            if (line2 == null)
                            {
                                //MessageBox.Show("file " + t + "  error in " + i + "!!!!!!!!!!!!");
                                break;
                            }//??????????????????????????????????????

                            string[] line_sp = line2.Split('\t');
                            string dll_shijian = line_sp[0];
                            dll_shijian.Replace(" ", "");
                            string dll_nianyueri = dll_shijian.Substring(0, 8);
                            string dll_shifen = dll_shijian.Substring(9, 5);

                            if (dll_nianyueri == dll_nianyueri_start_ST && dll_shifen == dll_shifen_start_ST)//如果到达起始时间，开始画图
                            { ST_flag = 1; count_temp_txt_readline++; }
                            else if (ST_flag == 1)
                            { count_temp_txt_readline++; }
                            if (dll_nianyueri == dll_nianyueri_end_ST && dll_shifen == dll_shifen_end_ST)
                            { count_txt_readline += count_temp_txt_readline; return; }
                        }

                        line2 = reader2.ReadLine();
                        // if (line2 == null) { MessageBox.Show("this line error "+i); }
                    }
                    count_txt_readline += count_temp_txt_readline;
                    //  MessageBox.Show("file" + t + "linecount=" + count_temp_txt_readline + "      mut=" + count_txt_readline);
                    if (count_txt_readline < count_config_chartpoint && count_txt_readline != 0) { count_config_chartpoint = count_txt_readline; }                      //  MessageBox.Show("txtcount!!!  "+txt_count.ToString());
                }
            }
        }
        private void getlinecountfsj()
        {
            for (int t = 0; t < fsj_file.Length; t++)
            {
                using (StreamReader reader2 = new StreamReader("./读取多个文件实验/辐射计/" + fsj_file[t]))
                {

                    string line2 = reader2.ReadLine();
                    int ST_flag = 0;//是否画图的标志

                    int i = 0;
                    for (; ; i++)
                    {
                        if (i >= read_first_num)//有效读取范围
                        {
                            if (line2 == null)
                            {
                                //    MessageBox.Show(count_txt_readline + "!!!!!!!!!!!!");
                                break;
                            }

                            string[] line_sp = line2.Split('\t');
                            string dll_shijian = line_sp[0];
                            //#############################################################
                            //判断开始时间
                            string time_X = string.Format("{0}", TimeStampToDateTime(line_sp[0]));
                            string dll_nianyueri = time_X.Replace("/", "").Substring(0, 7);
                            string dll_shifen = time_X.Substring(10, 5);


                            if (dll_nianyueri == dll_nianyueri_start_ST && dll_shifen == dll_shifen_start_ST)//如果到达起始时间，开始画图
                            {
                                ST_flag = 1;
                                count_temp_txt_readline++;
                            }
                            else if (ST_flag == 1)
                            {
                                count_temp_txt_readline++;
                            }
                            if (dll_nianyueri == dll_nianyueri_end_ST && dll_shifen == dll_shifen_end_ST)
                            {
                                count_txt_readline += count_temp_txt_readline; return;
                            }
                        }

                        line2 = reader2.ReadLine();
                        //   if (line2 == null) { MessageBox.Show("this line error " + i); }
                    }
                    count_txt_readline += count_temp_txt_readline;
                    //    MessageBox.Show("file" + t + "linecount=" + count_temp_txt_readline + "      mut=" + count_txt_readline);
                    if (count_txt_readline < count_config_chartpoint && count_txt_readline != 0)
                    { count_config_chartpoint = count_txt_readline; }
                }
                //  MessageBox.Show(count_txt_readline.ToString());
            }
        }
        private void getlinecounttcy()
        {
            for (int t = 0; t < tcy_file.Length; t++)
            {
                using (StreamReader reader2 = new StreamReader("./读取多个文件实验/探测仪/" + tcy_file[t]))
                {
                    string line2 = reader2.ReadLine();
                    int ST_flag = 0;//是否画图的标志

                    int i = 0;
                    for (; ; i++)
                    {
                        if (i >= read_first_num)//有效读取范围
                        {
                            //  MessageBox.Show(line2.ToString());
                            if (line2 == null)
                            {
                                // MessageBox.Show(count_txt_readline + "!!!!!!!!!!!!"); 
                                break;
                            }

                            string[] line_sp = line2.Split('\t');
                            string dll_shijian = line_sp[0];
                            //#############################################################
                            //判断开始时间
                            string time_X = string.Format("{0}", TimeStampToDateTime(line_sp[0]));
                            string dll_nianyueri = time_X.Replace("/", "").Substring(0, 7);
                            string dll_shifen = time_X.Substring(10, 5);

                            if (dll_nianyueri == dll_nianyueri_start_ST && dll_shifen == dll_shifen_start_ST)//如果到达起始时间，开始画图
                            { ST_flag = 1; count_temp_txt_readline++; }
                            else if (ST_flag == 1)
                            { count_temp_txt_readline++; }
                            if (dll_nianyueri == dll_nianyueri_end_ST && dll_shifen == dll_shifen_end_ST)
                            {
                                count_txt_readline += count_temp_txt_readline; return;
                            }
                        }
                        line2 = reader2.ReadLine();
                    }
                    count_txt_readline += count_temp_txt_readline;
                    if (count_txt_readline < count_config_chartpoint && count_txt_readline != 0)
                    { count_config_chartpoint = count_txt_readline; }
                }
            }
        }
        private void getlinecountdy()
        {

            for (int t = 0; t < dy_file.Length; t++)
            {

                using (StreamReader reader2 = new StreamReader("./读取多个文件实验/电源/" + dy_file[t]))
                {
                    string line2 = reader2.ReadLine();
                    int ST_flag = 0;//是否画图的标志

                    int i = 0;
                    for (; ; i++)
                    {
                        if (i >= read_first_num)//有效读取范围
                        {
                            // MessageBox.Show(line2.ToString());
                            if (line2 == null)
                            {
                                //  MessageBox.Show(count_txt_readline + "!!!!!!!!!!!!"); 
                                break;
                            }

                            string[] line_sp = line2.Split('\t');
                            string dy_shijian = line_sp[0];


                            dy_shijian.Replace(" ", "");
                            string dy_nianyueri = dy_shijian.Substring(0, 8);
                            string dy_shifen = dy_shijian.Substring(9, 5);


                            if (dy_nianyueri == dll_nianyueri_start_ST && dy_shifen == dll_shifen_start_ST)//如果到达起始时间，开始画图
                            {
                                ST_flag = 1;
                                count_temp_txt_readline++;
                            }
                            else if (ST_flag == 1)
                            { count_temp_txt_readline++; }
                            if (dy_nianyueri == dll_nianyueri_end_ST && dy_shifen == dll_shifen_end_ST)
                            {
                                count_txt_readline += count_temp_txt_readline; return;
                            }
                        }

                        line2 = reader2.ReadLine();
                        //    if (line2 == null) { MessageBox.Show("this line error " + i); }
                    }
                    count_txt_readline += count_temp_txt_readline;
                    //   MessageBox.Show("file" + t + "linecount=" + count_temp_txt_readline + "      mut=" + count_txt_readline);
                    if (count_txt_readline < count_config_chartpoint && count_txt_readline != 0)
                    { count_config_chartpoint = count_txt_readline; }                      //  MessageBox.Show("txtcount!!!  "+txt_count.ToString());
                }
            }
        }
        #endregion

        #region 文件筛选
        private string[] get_dll_file_extent(string[] file)
        {
            int indexstart = 0;//选定的时间范围内，文件索引
            int indexend = 0;
            for (int i = 0; i < file.Length; i++)
            {
                int year = int.Parse(file[i].Substring(0, 4));
                int mon = int.Parse(file[i].Substring(5, 2));
                int day = int.Parse(file[i].Substring(8, 2));
                int data = year * 10000 + mon * 100 + day;
                if (data == dll_nianyueri_start_int && i != 0)
                {
                    indexstart = i - 1;
                }
                else if (data == dll_nianyueri_start_int && i == 0)
                {
                    indexstart = i;
                }
                if (data == dll_nianyueri_end_int)
                {
                    indexend = i;
                }
            }

            string[] extent = new string[indexend - indexstart + 1];
            int j = 0;
            for (int i = indexstart; i != indexend + 1; i++)
            {
                extent[j] = file[i];
                j++;
            }
            return extent;
        }
        private string[] get_fsj_file_extent(string[] file)
        {
            int indexstart = 0;//选定的时间范围内，文件索引D:\Satellite\2019二月修改(0325)\SatelliteSoftwareIF\SatelliteSoftwareIF\Program.cs
            int indexend = 0;
            for (int i = 0; i < file.Length; i++)
            {
                
                //int year = int.Parse(file[i].Substring(6, 4));
                //int mon = int.Parse(file[i].Substring(11, 2));
                //int day = int.Parse(file[i].Substring(14, 2));
                string[] sArray = file[i].Split('_');
                int year = int.Parse(sArray[2]);
                int mon = int.Parse(sArray[3]);
                int day = int.Parse(sArray[4]);
                int hour = int.Parse(sArray[5]);
                int min = int.Parse(sArray[6]);
                //int data = year * 10000 + mon * 100 + day;
                string data = sArray[2] + mon.ToString() + sArray[4]+sArray[5]+":"+sArray[6];
                string startFlag = dll_nianyueri_start_int.ToString() + dll_shifen_end_string;
                string stopFlag = dll_nianyueri_end_int.ToString() + dll_shifen_end_string;
                if (data == startFlag && i != 0)
                {
                    indexstart = i - 1;
                }
                else if (data == startFlag && i == 0)
                {
                    indexstart = i;
                }
                if (data == stopFlag)
                {
                    indexend = i;
                }
            }

            string[] extent = new string[indexend - indexstart + 1];
            int j = 0;
            for (int i = indexstart; i != indexend + 1; i++)
            {
                extent[j] = file[i];
                j++;
            }
            return extent;
        }
        #endregion
        //读取数据入口
        private void readData()
        {
            //  this.Invoke(new EventHandler(delegate{readData_ST();}));
            // readData_ST();
            if (bujian_flag == "动量轮") { readData_dll();}
            else if (bujian_flag == "探测仪") { readData_tcy();}
            else if (bujian_flag == "辐射计") { readData_fsj();}
            else if (bujian_flag == "电源") { readData_dy(); }
            else { return; }
        }
        #region 处理数据模块
        private void readData_dll()
        {
           dll_file= get_dll_file_extent(dll_file);
            
            ShowMessage(progress.waitcount);
            getlinecountdll();
            ST_flag = 0;
            if (count_txt_readline == 0) { ShowMessage(progress.Error_noline); return; }
            ShowMessage(progress.endcount);
            ShowMessage(progress.waithandle);
            for (int t = 0; t < dll_file.Length; t++)
            {
                using (StreamReader reader = new StreamReader("./读取多个文件实验/动量轮/" + dll_file[t]))
                {
                    int i = 0;//当前行数
                    string line = reader.ReadLine();
                    while (reader.Peek() > -1)
                    {
                        if (i >= read_first_num)//有效读取范围
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
                            //label_shijian.Text = string.Format("{0}", line_sp[0]);
                            double a = TEST.Class1.test_dll(value_nums);
                            double b = 0;
                            //#############################################################
                            //判断开始时间 
                            string dll_shijian = line_sp[0];
                            dll_shijian.Replace(" ", "");
                            string dll_nianyueri = dll_shijian.Substring(0, 8);
                            string dll_shifen = dll_shijian.Substring(9, 5);
                            string dll_shijian_hengzuobiao = dll_shijian.Substring(0, 14);
                            //Console.WriteLine(dll_nianyueri, dll_shifen);
                            if (dll_nianyueri == dll_nianyueri_start_ST && dll_shifen == dll_shifen_start_ST)//如果到达起始时间，开始画图
                            {
                                ST_flag = 1;
                                //#############################################################
                                //读取“动量轮1马达电流”的数据并判断是否故障
                                #region
                                if (bianliang_flag == "动量轮1马达电流")
                                {
                                    b = value_nums[0];//读取动量轮某一列数据

                                    if (a >= 0.5)
                                    {

                                        label_dll.Invoke(new EventHandler(delegate { label_dll.Text = string.Format("故障"); }));
                                        label_dll.ForeColor = System.Drawing.Color.Red;
                                        if (value_nums[0] >= 2.0 || value_nums[0] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮1马达电流");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            // abn_state_lstbox.Items.Add("故障部件：动量轮1马达电流");
                                        }
                                        else if (value_nums[1] >= 2.0 || value_nums[1] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮2马达电流");
                                            }));
                                            //  abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //  abn_state_lstbox.Items.Add("故障部件：动量轮2马达电流");
                                        }
                                        else if (value_nums[2] >= 2.0 || value_nums[2] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮4马达电流");
                                            }));
                                            //  abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //   abn_state_lstbox.Items.Add("故障部件：动量轮4马达电流");
                                        }
                                        else if (value_nums[3] >= 2.0 || value_nums[3] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮5马达电流");
                                            }));
                                            //    abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //    abn_state_lstbox.Items.Add("故障部件：动量轮5马达电流");
                                        }
                                        else if (value_nums[4] >= 45.0 || value_nums[4] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮1轴温");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            // abn_state_lstbox.Items.Add("故障部件：动量轮1轴温");
                                        }
                                        else if (value_nums[5] >= 45.0 || value_nums[5] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮2轴温");
                                            }));
                                            //    abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //    abn_state_lstbox.Items.Add("故障部件：动量轮2轴温");
                                        }
                                        else if (value_nums[6] >= 45.0 || value_nums[8] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮3轴温");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            // abn_state_lstbox.Items.Add("故障部件：动量轮3轴温");
                                        }
                                        else if (value_nums[7] >= 45.0 || value_nums[7] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮4轴温");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //  abn_state_lstbox.Items.Add("故障部件：动量轮4轴温");
                                        }
                                        else if (value_nums[8] >= 45.0 || value_nums[8] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮5轴温");
                                            }));
                                            //    abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //     abn_state_lstbox.Items.Add("故障部件：动量轮5轴温");
                                        }
                                        else if (value_nums[9] >= 45.0 || value_nums[9] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮6轴温");
                                            }));
                                            //  abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //  abn_state_lstbox.Items.Add("故障部件：动量轮6轴温");
                                        }
                                    }
                                }
                                //读取“动量轮2马达电流”的数据并判断是否故障
                                else if (bianliang_flag == "动量轮2马达电流")
                                {
                                    b = value_nums[1];//读取动量轮某一列数据

                                    if (a >= 0.5)
                                    {
                                        label_dll.Invoke(new EventHandler(delegate { label_dll.Text = string.Format("故障"); }));
                                        label_dll.ForeColor = System.Drawing.Color.Red;
                                        if (value_nums[0] >= 2.0 || value_nums[0] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮1马达电流");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            // abn_state_lstbox.Items.Add("故障部件：动量轮1马达电流");
                                        }
                                        else if (value_nums[1] >= 2.0 || value_nums[1] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮2马达电流");
                                            }));
                                            //  abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //  abn_state_lstbox.Items.Add("故障部件：动量轮2马达电流");
                                        }
                                        else if (value_nums[2] >= 2.0 || value_nums[2] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮4马达电流");
                                            }));
                                            //  abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //   abn_state_lstbox.Items.Add("故障部件：动量轮4马达电流");
                                        }
                                        else if (value_nums[3] >= 2.0 || value_nums[3] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮5马达电流");
                                            }));
                                            //    abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //    abn_state_lstbox.Items.Add("故障部件：动量轮5马达电流");
                                        }
                                        else if (value_nums[4] >= 45.0 || value_nums[4] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮1轴温");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            // abn_state_lstbox.Items.Add("故障部件：动量轮1轴温");
                                        }
                                        else if (value_nums[5] >= 45.0 || value_nums[5] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮2轴温");
                                            }));
                                            //    abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //    abn_state_lstbox.Items.Add("故障部件：动量轮2轴温");
                                        }
                                        else if (value_nums[6] >= 45.0 || value_nums[8] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮3轴温");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            // abn_state_lstbox.Items.Add("故障部件：动量轮3轴温");
                                        }
                                        else if (value_nums[7] >= 45.0 || value_nums[7] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮4轴温");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //  abn_state_lstbox.Items.Add("故障部件：动量轮4轴温");
                                        }
                                        else if (value_nums[8] >= 45.0 || value_nums[8] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮5轴温");
                                            }));
                                            //    abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //     abn_state_lstbox.Items.Add("故障部件：动量轮5轴温");
                                        }
                                        else if (value_nums[9] >= 45.0 || value_nums[9] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮6轴温");
                                            }));
                                            //  abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //  abn_state_lstbox.Items.Add("故障部件：动量轮6轴温");
                                        }
                                    }
                                }
                                //读取“动量轮4马达电流”的数据并判断是否故障
                                else if (bianliang_flag == "动量轮4马达电流")
                                {
                                    b = value_nums[2];//读取动量轮某一列数据

                                    if (a >= 0.5)
                                    {
                                        label_dll.Invoke(new EventHandler(delegate { label_dll.Text = string.Format("故障"); }));
                                        label_dll.ForeColor = System.Drawing.Color.Red;
                                        if (value_nums[0] >= 2.0 || value_nums[0] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮1马达电流");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            // abn_state_lstbox.Items.Add("故障部件：动量轮1马达电流");
                                        }
                                        else if (value_nums[1] >= 2.0 || value_nums[1] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮2马达电流");
                                            }));
                                            //  abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //  abn_state_lstbox.Items.Add("故障部件：动量轮2马达电流");
                                        }
                                        else if (value_nums[2] >= 2.0 || value_nums[2] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮4马达电流");
                                            }));
                                            //  abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //   abn_state_lstbox.Items.Add("故障部件：动量轮4马达电流");
                                        }
                                        else if (value_nums[3] >= 2.0 || value_nums[3] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮5马达电流");
                                            }));
                                            //    abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //    abn_state_lstbox.Items.Add("故障部件：动量轮5马达电流");
                                        }
                                        else if (value_nums[4] >= 45.0 || value_nums[4] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮1轴温");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            // abn_state_lstbox.Items.Add("故障部件：动量轮1轴温");
                                        }
                                        else if (value_nums[5] >= 45.0 || value_nums[5] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮2轴温");
                                            }));
                                            //    abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //    abn_state_lstbox.Items.Add("故障部件：动量轮2轴温");
                                        }
                                        else if (value_nums[6] >= 45.0 || value_nums[8] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮3轴温");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            // abn_state_lstbox.Items.Add("故障部件：动量轮3轴温");
                                        }
                                        else if (value_nums[7] >= 45.0 || value_nums[7] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮4轴温");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //  abn_state_lstbox.Items.Add("故障部件：动量轮4轴温");
                                        }
                                        else if (value_nums[8] >= 45.0 || value_nums[8] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮5轴温");
                                            }));
                                            //    abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //     abn_state_lstbox.Items.Add("故障部件：动量轮5轴温");
                                        }
                                        else if (value_nums[9] >= 45.0 || value_nums[9] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮6轴温");
                                            }));
                                            //  abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //  abn_state_lstbox.Items.Add("故障部件：动量轮6轴温");
                                        }
                                    }
                                }
                                //读取“动量轮5马达电流”的数据并判断是否故障
                                else if (bianliang_flag == "动量轮5马达电流")
                                {
                                    b = value_nums[3];//读取动量轮某一列数据

                                    if (a >= 0.5)
                                    {
                                        label_dll.Invoke(new EventHandler(delegate { label_dll.Text = string.Format("故障"); }));
                                        label_dll.ForeColor = System.Drawing.Color.Red;
                                        if (value_nums[0] >= 2.0 || value_nums[0] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮1马达电流");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            // abn_state_lstbox.Items.Add("故障部件：动量轮1马达电流");
                                        }
                                        else if (value_nums[1] >= 2.0 || value_nums[1] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮2马达电流");
                                            }));
                                            //  abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //  abn_state_lstbox.Items.Add("故障部件：动量轮2马达电流");
                                        }
                                        else if (value_nums[2] >= 2.0 || value_nums[2] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮4马达电流");
                                            }));
                                            //  abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //   abn_state_lstbox.Items.Add("故障部件：动量轮4马达电流");
                                        }
                                        else if (value_nums[3] >= 2.0 || value_nums[3] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮5马达电流");
                                            }));
                                            //    abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //    abn_state_lstbox.Items.Add("故障部件：动量轮5马达电流");
                                        }
                                        else if (value_nums[4] >= 45.0 || value_nums[4] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮1轴温");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            // abn_state_lstbox.Items.Add("故障部件：动量轮1轴温");
                                        }
                                        else if (value_nums[5] >= 45.0 || value_nums[5] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮2轴温");
                                            }));
                                            //    abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //    abn_state_lstbox.Items.Add("故障部件：动量轮2轴温");
                                        }
                                        else if (value_nums[6] >= 45.0 || value_nums[8] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮3轴温");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            // abn_state_lstbox.Items.Add("故障部件：动量轮3轴温");
                                        }
                                        else if (value_nums[7] >= 45.0 || value_nums[7] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮4轴温");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //  abn_state_lstbox.Items.Add("故障部件：动量轮4轴温");
                                        }
                                        else if (value_nums[8] >= 45.0 || value_nums[8] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮5轴温");
                                            }));
                                            //    abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //     abn_state_lstbox.Items.Add("故障部件：动量轮5轴温");
                                        }
                                        else if (value_nums[9] >= 45.0 || value_nums[9] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮6轴温");
                                            }));
                                            //  abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //  abn_state_lstbox.Items.Add("故障部件：动量轮6轴温");
                                        }
                                    }
                                }
                                //读取“动量轮1轴温”的数据并判断是否故障
                                else if (bianliang_flag == "动量轮1轴温")
                                {
                                    b = value_nums[4];//读取动量轮某一列数据

                                    if (a >= 0.5)
                                    {
                                        label_dll.Invoke(new EventHandler(delegate { label_dll.Text = string.Format("故障"); }));
                                        label_dll.ForeColor = System.Drawing.Color.Red;
                                        if (value_nums[0] >= 2.0 || value_nums[0] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮1马达电流");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            // abn_state_lstbox.Items.Add("故障部件：动量轮1马达电流");
                                        }
                                        else if (value_nums[1] >= 2.0 || value_nums[1] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮2马达电流");
                                            }));
                                            //  abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //  abn_state_lstbox.Items.Add("故障部件：动量轮2马达电流");
                                        }
                                        else if (value_nums[2] >= 2.0 || value_nums[2] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮4马达电流");
                                            }));
                                            //  abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //   abn_state_lstbox.Items.Add("故障部件：动量轮4马达电流");
                                        }
                                        else if (value_nums[3] >= 2.0 || value_nums[3] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮5马达电流");
                                            }));
                                            //    abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //    abn_state_lstbox.Items.Add("故障部件：动量轮5马达电流");
                                        }
                                        else if (value_nums[4] >= 45.0 || value_nums[4] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮1轴温");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            // abn_state_lstbox.Items.Add("故障部件：动量轮1轴温");
                                        }
                                        else if (value_nums[5] >= 45.0 || value_nums[5] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮2轴温");
                                            }));
                                            //    abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //    abn_state_lstbox.Items.Add("故障部件：动量轮2轴温");
                                        }
                                        else if (value_nums[6] >= 45.0 || value_nums[8] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮3轴温");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            // abn_state_lstbox.Items.Add("故障部件：动量轮3轴温");
                                        }
                                        else if (value_nums[7] >= 45.0 || value_nums[7] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮4轴温");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //  abn_state_lstbox.Items.Add("故障部件：动量轮4轴温");
                                        }
                                        else if (value_nums[8] >= 45.0 || value_nums[8] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮5轴温");
                                            }));
                                            //    abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //     abn_state_lstbox.Items.Add("故障部件：动量轮5轴温");
                                        }
                                        else if (value_nums[9] >= 45.0 || value_nums[9] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮6轴温");
                                            }));
                                            //  abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //  abn_state_lstbox.Items.Add("故障部件：动量轮6轴温");
                                        }
                                    }
                                }
                                //读取“动量轮2轴温”的数据并判断是否故障
                                else if (bianliang_flag == "动量轮2轴温")
                                {
                                    b = value_nums[5];//读取动量轮某一列数据

                                    if (a >= 0.5)
                                    {
                                        label_dll.Invoke(new EventHandler(delegate { label_dll.Text = string.Format("故障"); }));
                                        label_dll.ForeColor = System.Drawing.Color.Red;
                                        if (value_nums[0] >= 2.0 || value_nums[0] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮1马达电流");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            // abn_state_lstbox.Items.Add("故障部件：动量轮1马达电流");
                                        }
                                        else if (value_nums[1] >= 2.0 || value_nums[1] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮2马达电流");
                                            }));
                                            //  abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //  abn_state_lstbox.Items.Add("故障部件：动量轮2马达电流");
                                        }
                                        else if (value_nums[2] >= 2.0 || value_nums[2] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮4马达电流");
                                            }));
                                            //  abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //   abn_state_lstbox.Items.Add("故障部件：动量轮4马达电流");
                                        }
                                        else if (value_nums[3] >= 2.0 || value_nums[3] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮5马达电流");
                                            }));
                                            //    abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //    abn_state_lstbox.Items.Add("故障部件：动量轮5马达电流");
                                        }
                                        else if (value_nums[4] >= 45.0 || value_nums[4] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮1轴温");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            // abn_state_lstbox.Items.Add("故障部件：动量轮1轴温");
                                        }
                                        else if (value_nums[5] >= 45.0 || value_nums[5] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮2轴温");
                                            }));
                                            //    abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //    abn_state_lstbox.Items.Add("故障部件：动量轮2轴温");
                                        }
                                        else if (value_nums[6] >= 45.0 || value_nums[8] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮3轴温");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            // abn_state_lstbox.Items.Add("故障部件：动量轮3轴温");
                                        }
                                        else if (value_nums[7] >= 45.0 || value_nums[7] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮4轴温");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //  abn_state_lstbox.Items.Add("故障部件：动量轮4轴温");
                                        }
                                        else if (value_nums[8] >= 45.0 || value_nums[8] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮5轴温");
                                            }));
                                            //    abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //     abn_state_lstbox.Items.Add("故障部件：动量轮5轴温");
                                        }
                                        else if (value_nums[9] >= 45.0 || value_nums[9] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮6轴温");
                                            }));
                                            //  abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //  abn_state_lstbox.Items.Add("故障部件：动量轮6轴温");
                                        }
                                    }
                                }
                                //读取“动量轮3轴温”的数据并判断是否故障
                                else if (bianliang_flag == "动量轮3轴温")
                                {
                                    b = value_nums[6];//读取动量轮某一列数据

                                    if (a >= 0.5)
                                    {
                                        label_dll.Invoke(new EventHandler(delegate { label_dll.Text = string.Format("故障"); }));
                                        label_dll.ForeColor = System.Drawing.Color.Red;
                                        if (value_nums[0] >= 2.0 || value_nums[0] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮1马达电流");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            // abn_state_lstbox.Items.Add("故障部件：动量轮1马达电流");
                                        }
                                        else if (value_nums[1] >= 2.0 || value_nums[1] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮2马达电流");
                                            }));
                                            //  abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //  abn_state_lstbox.Items.Add("故障部件：动量轮2马达电流");
                                        }
                                        else if (value_nums[2] >= 2.0 || value_nums[2] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮4马达电流");
                                            }));
                                            //  abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //   abn_state_lstbox.Items.Add("故障部件：动量轮4马达电流");
                                        }
                                        else if (value_nums[3] >= 2.0 || value_nums[3] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮5马达电流");
                                            }));
                                            //    abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //    abn_state_lstbox.Items.Add("故障部件：动量轮5马达电流");
                                        }
                                        else if (value_nums[4] >= 45.0 || value_nums[4] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮1轴温");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            // abn_state_lstbox.Items.Add("故障部件：动量轮1轴温");
                                        }
                                        else if (value_nums[5] >= 45.0 || value_nums[5] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮2轴温");
                                            }));
                                            //    abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //    abn_state_lstbox.Items.Add("故障部件：动量轮2轴温");
                                        }
                                        else if (value_nums[6] >= 45.0 || value_nums[8] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮3轴温");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            // abn_state_lstbox.Items.Add("故障部件：动量轮3轴温");
                                        }
                                        else if (value_nums[7] >= 45.0 || value_nums[7] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮4轴温");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //  abn_state_lstbox.Items.Add("故障部件：动量轮4轴温");
                                        }
                                        else if (value_nums[8] >= 45.0 || value_nums[8] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮5轴温");
                                            }));
                                            //    abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //     abn_state_lstbox.Items.Add("故障部件：动量轮5轴温");
                                        }
                                        else if (value_nums[9] >= 45.0 || value_nums[9] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮6轴温");
                                            }));
                                            //  abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //  abn_state_lstbox.Items.Add("故障部件：动量轮6轴温");
                                        }
                                    }
                                }
                                //读取“动量轮4轴温”的数据并判断是否故障
                                else if (bianliang_flag == "动量轮4轴温")
                                {
                                    b = value_nums[7];//读取动量轮某一列数据

                                    if (a >= 0.5)
                                    {
                                        label_dll.Invoke(new EventHandler(delegate { label_dll.Text = string.Format("故障"); }));
                                        label_dll.ForeColor = System.Drawing.Color.Red;
                                        if (value_nums[0] >= 2.0 || value_nums[0] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮1马达电流");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            // abn_state_lstbox.Items.Add("故障部件：动量轮1马达电流");
                                        }
                                        else if (value_nums[1] >= 2.0 || value_nums[1] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮2马达电流");
                                            }));
                                            //  abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //  abn_state_lstbox.Items.Add("故障部件：动量轮2马达电流");
                                        }
                                        else if (value_nums[2] >= 2.0 || value_nums[2] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮4马达电流");
                                            }));
                                            //  abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //   abn_state_lstbox.Items.Add("故障部件：动量轮4马达电流");
                                        }
                                        else if (value_nums[3] >= 2.0 || value_nums[3] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮5马达电流");
                                            }));
                                            //    abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //    abn_state_lstbox.Items.Add("故障部件：动量轮5马达电流");
                                        }
                                        else if (value_nums[4] >= 45.0 || value_nums[4] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮1轴温");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            // abn_state_lstbox.Items.Add("故障部件：动量轮1轴温");
                                        }
                                        else if (value_nums[5] >= 45.0 || value_nums[5] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮2轴温");
                                            }));
                                            //    abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //    abn_state_lstbox.Items.Add("故障部件：动量轮2轴温");
                                        }
                                        else if (value_nums[6] >= 45.0 || value_nums[8] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮3轴温");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            // abn_state_lstbox.Items.Add("故障部件：动量轮3轴温");
                                        }
                                        else if (value_nums[7] >= 45.0 || value_nums[7] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮4轴温");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //  abn_state_lstbox.Items.Add("故障部件：动量轮4轴温");
                                        }
                                        else if (value_nums[8] >= 45.0 || value_nums[8] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮5轴温");
                                            }));
                                            //    abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //     abn_state_lstbox.Items.Add("故障部件：动量轮5轴温");
                                        }
                                        else if (value_nums[9] >= 45.0 || value_nums[9] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮6轴温");
                                            }));
                                            //  abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //  abn_state_lstbox.Items.Add("故障部件：动量轮6轴温");
                                        }
                                    }
                                }
                                //读取“动量轮5轴温”的数据并判断是否故障
                                else if (bianliang_flag == "动量轮5轴温")
                                {
                                    b = value_nums[8];//读取动量轮某一列数据

                                    if (a >= 0.5)
                                    {
                                        label_dll.Invoke(new EventHandler(delegate { label_dll.Text = string.Format("故障"); }));
                                        label_dll.ForeColor = System.Drawing.Color.Red;
                                        if (value_nums[0] >= 2.0 || value_nums[0] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮1马达电流");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            // abn_state_lstbox.Items.Add("故障部件：动量轮1马达电流");
                                        }
                                        else if (value_nums[1] >= 2.0 || value_nums[1] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮2马达电流");
                                            }));
                                            //  abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //  abn_state_lstbox.Items.Add("故障部件：动量轮2马达电流");
                                        }
                                        else if (value_nums[2] >= 2.0 || value_nums[2] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮4马达电流");
                                            }));
                                            //  abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //   abn_state_lstbox.Items.Add("故障部件：动量轮4马达电流");
                                        }
                                        else if (value_nums[3] >= 2.0 || value_nums[3] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮5马达电流");
                                            }));
                                            //    abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //    abn_state_lstbox.Items.Add("故障部件：动量轮5马达电流");
                                        }
                                        else if (value_nums[4] >= 45.0 || value_nums[4] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮1轴温");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            // abn_state_lstbox.Items.Add("故障部件：动量轮1轴温");
                                        }
                                        else if (value_nums[5] >= 45.0 || value_nums[5] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮2轴温");
                                            }));
                                            //    abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //    abn_state_lstbox.Items.Add("故障部件：动量轮2轴温");
                                        }
                                        else if (value_nums[6] >= 45.0 || value_nums[8] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮3轴温");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            // abn_state_lstbox.Items.Add("故障部件：动量轮3轴温");
                                        }
                                        else if (value_nums[7] >= 45.0 || value_nums[7] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮4轴温");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //  abn_state_lstbox.Items.Add("故障部件：动量轮4轴温");
                                        }
                                        else if (value_nums[8] >= 45.0 || value_nums[8] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮5轴温");
                                            }));
                                            //    abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //     abn_state_lstbox.Items.Add("故障部件：动量轮5轴温");
                                        }
                                        else if (value_nums[9] >= 45.0 || value_nums[9] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮6轴温");
                                            }));
                                            //  abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //  abn_state_lstbox.Items.Add("故障部件：动量轮6轴温");
                                        }
                                    }
                                }
                                //读取“动量轮6轴温”的数据并判断是否故障
                                else if (bianliang_flag == "动量轮6轴温")
                                {
                                    b = value_nums[9];//读取动量轮某一列数据
                                    /*
                                    this.chart_data.ChartAreas[0].AxisY.Title = "动量轮6轴温/度";
                                    this.chart_data.Titles[0].Text = "动量轮6轴温";
                                    this.chart_data.Series[0].LegendText = "动量轮6轴温";
                                    */
                                    if (a >= 0.5)
                                    {
                                        label_dll.Invoke(new EventHandler(delegate { label_dll.Text = string.Format("故障"); }));
                                        label_dll.ForeColor = System.Drawing.Color.Red;
                                        if (value_nums[0] >= 2.0 || value_nums[0] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮1马达电流");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            // abn_state_lstbox.Items.Add("故障部件：动量轮1马达电流");
                                        }
                                        else if (value_nums[1] >= 2.0 || value_nums[1] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮2马达电流");
                                            }));
                                            //  abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //  abn_state_lstbox.Items.Add("故障部件：动量轮2马达电流");
                                        }
                                        else if (value_nums[2] >= 2.0 || value_nums[2] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮4马达电流");
                                            }));
                                            //  abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //   abn_state_lstbox.Items.Add("故障部件：动量轮4马达电流");
                                        }
                                        else if (value_nums[3] >= 2.0 || value_nums[3] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮5马达电流");
                                            }));
                                            //    abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //    abn_state_lstbox.Items.Add("故障部件：动量轮5马达电流");
                                        }
                                        else if (value_nums[4] >= 45.0 || value_nums[4] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮1轴温");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            // abn_state_lstbox.Items.Add("故障部件：动量轮1轴温");
                                        }
                                        else if (value_nums[5] >= 45.0 || value_nums[5] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮2轴温");
                                            }));
                                            //    abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //    abn_state_lstbox.Items.Add("故障部件：动量轮2轴温");
                                        }
                                        else if (value_nums[6] >= 45.0 || value_nums[8] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮3轴温");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            // abn_state_lstbox.Items.Add("故障部件：动量轮3轴温");
                                        }
                                        else if (value_nums[7] >= 45.0 || value_nums[7] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮4轴温");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //  abn_state_lstbox.Items.Add("故障部件：动量轮4轴温");
                                        }
                                        else if (value_nums[8] >= 45.0 || value_nums[8] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮5轴温");
                                            }));
                                            //    abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //     abn_state_lstbox.Items.Add("故障部件：动量轮5轴温");
                                        }
                                        else if (value_nums[9] >= 45.0 || value_nums[9] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮6轴温");
                                            }));
                                            //  abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //  abn_state_lstbox.Items.Add("故障部件：动量轮6轴温");
                                        }
                                    }
                                }
                                #endregion
                                // MessageBox.Show("count2 "+txt_count.ToString());
                                if (i % (count_txt_readline / count_config_chartpoint) == 0)
                                {
                                    dt_pro.Rows.Add(dll_shijian_hengzuobiao, a);
                                    dt_data.Rows.Add(dll_shijian_hengzuobiao, b);
                                }
                                //chartRef(dt_pro, dt_data);
                                // this.Invoke(new EventHandler(delegate { tabledraw(); }));

                            }
                            else if (ST_flag == 1)//时间范围内，继续画图
                            {
                                //#############################################################
                                //读取“动量轮1马达电流”的数据并判断是否故障
                                #region
                                if (bianliang_flag == "动量轮1马达电流")
                                {
                                    b = value_nums[0];//读取动量轮某一列数据

                                    if (a >= 0.5)
                                    {

                                        label_dll.Invoke(new EventHandler(delegate { label_dll.Text = string.Format("故障"); }));
                                        label_dll.ForeColor = System.Drawing.Color.Red;
                                        if (value_nums[0] >= 2.0 || value_nums[0] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮1马达电流");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            // abn_state_lstbox.Items.Add("故障部件：动量轮1马达电流");
                                        }
                                        else if (value_nums[1] >= 2.0 || value_nums[1] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮2马达电流");
                                            }));
                                            //  abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //  abn_state_lstbox.Items.Add("故障部件：动量轮2马达电流");
                                        }
                                        else if (value_nums[2] >= 2.0 || value_nums[2] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮4马达电流");
                                            }));
                                            //  abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //   abn_state_lstbox.Items.Add("故障部件：动量轮4马达电流");
                                        }
                                        else if (value_nums[3] >= 2.0 || value_nums[3] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮5马达电流");
                                            }));
                                            //    abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //    abn_state_lstbox.Items.Add("故障部件：动量轮5马达电流");
                                        }
                                        else if (value_nums[4] >= 45.0 || value_nums[4] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮1轴温");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            // abn_state_lstbox.Items.Add("故障部件：动量轮1轴温");
                                        }
                                        else if (value_nums[5] >= 45.0 || value_nums[5] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮2轴温");
                                            }));
                                            //    abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //    abn_state_lstbox.Items.Add("故障部件：动量轮2轴温");
                                        }
                                        else if (value_nums[6] >= 45.0 || value_nums[8] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮3轴温");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            // abn_state_lstbox.Items.Add("故障部件：动量轮3轴温");
                                        }
                                        else if (value_nums[7] >= 45.0 || value_nums[7] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮4轴温");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //  abn_state_lstbox.Items.Add("故障部件：动量轮4轴温");
                                        }
                                        else if (value_nums[8] >= 45.0 || value_nums[8] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮5轴温");
                                            }));
                                            //    abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //     abn_state_lstbox.Items.Add("故障部件：动量轮5轴温");
                                        }
                                        else if (value_nums[9] >= 45.0 || value_nums[9] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮6轴温");
                                            }));
                                            //  abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //  abn_state_lstbox.Items.Add("故障部件：动量轮6轴温");
                                        }
                                    }
                                }
                                //读取“动量轮2马达电流”的数据并判断是否故障
                                else if (bianliang_flag == "动量轮2马达电流")
                                {
                                    b = value_nums[1];//读取动量轮某一列数据

                                    if (a >= 0.5)
                                    {
                                        label_dll.Invoke(new EventHandler(delegate { label_dll.Text = string.Format("故障"); }));
                                        label_dll.ForeColor = System.Drawing.Color.Red;
                                        if (value_nums[0] >= 2.0 || value_nums[0] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮1马达电流");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            // abn_state_lstbox.Items.Add("故障部件：动量轮1马达电流");
                                        }
                                        else if (value_nums[1] >= 2.0 || value_nums[1] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮2马达电流");
                                            }));
                                            //  abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //  abn_state_lstbox.Items.Add("故障部件：动量轮2马达电流");
                                        }
                                        else if (value_nums[2] >= 2.0 || value_nums[2] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮4马达电流");
                                            }));
                                            //  abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //   abn_state_lstbox.Items.Add("故障部件：动量轮4马达电流");
                                        }
                                        else if (value_nums[3] >= 2.0 || value_nums[3] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮5马达电流");
                                            }));
                                            //    abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //    abn_state_lstbox.Items.Add("故障部件：动量轮5马达电流");
                                        }
                                        else if (value_nums[4] >= 45.0 || value_nums[4] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮1轴温");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            // abn_state_lstbox.Items.Add("故障部件：动量轮1轴温");
                                        }
                                        else if (value_nums[5] >= 45.0 || value_nums[5] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮2轴温");
                                            }));
                                            //    abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //    abn_state_lstbox.Items.Add("故障部件：动量轮2轴温");
                                        }
                                        else if (value_nums[6] >= 45.0 || value_nums[8] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮3轴温");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            // abn_state_lstbox.Items.Add("故障部件：动量轮3轴温");
                                        }
                                        else if (value_nums[7] >= 45.0 || value_nums[7] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮4轴温");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //  abn_state_lstbox.Items.Add("故障部件：动量轮4轴温");
                                        }
                                        else if (value_nums[8] >= 45.0 || value_nums[8] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮5轴温");
                                            }));
                                            //    abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //     abn_state_lstbox.Items.Add("故障部件：动量轮5轴温");
                                        }
                                        else if (value_nums[9] >= 45.0 || value_nums[9] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮6轴温");
                                            }));
                                            //  abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //  abn_state_lstbox.Items.Add("故障部件：动量轮6轴温");
                                        }
                                    }
                                }
                                //读取“动量轮4马达电流”的数据并判断是否故障
                                else if (bianliang_flag == "动量轮4马达电流")
                                {
                                    b = value_nums[2];//读取动量轮某一列数据

                                    if (a >= 0.5)
                                    {
                                        label_dll.Invoke(new EventHandler(delegate { label_dll.Text = string.Format("故障"); }));
                                        label_dll.ForeColor = System.Drawing.Color.Red;
                                        if (value_nums[0] >= 2.0 || value_nums[0] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮1马达电流");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            // abn_state_lstbox.Items.Add("故障部件：动量轮1马达电流");
                                        }
                                        else if (value_nums[1] >= 2.0 || value_nums[1] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮2马达电流");
                                            }));
                                            //  abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //  abn_state_lstbox.Items.Add("故障部件：动量轮2马达电流");
                                        }
                                        else if (value_nums[2] >= 2.0 || value_nums[2] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮4马达电流");
                                            }));
                                            //  abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //   abn_state_lstbox.Items.Add("故障部件：动量轮4马达电流");
                                        }
                                        else if (value_nums[3] >= 2.0 || value_nums[3] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮5马达电流");
                                            }));
                                            //    abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //    abn_state_lstbox.Items.Add("故障部件：动量轮5马达电流");
                                        }
                                        else if (value_nums[4] >= 45.0 || value_nums[4] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮1轴温");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            // abn_state_lstbox.Items.Add("故障部件：动量轮1轴温");
                                        }
                                        else if (value_nums[5] >= 45.0 || value_nums[5] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮2轴温");
                                            }));
                                            //    abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //    abn_state_lstbox.Items.Add("故障部件：动量轮2轴温");
                                        }
                                        else if (value_nums[6] >= 45.0 || value_nums[8] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮3轴温");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            // abn_state_lstbox.Items.Add("故障部件：动量轮3轴温");
                                        }
                                        else if (value_nums[7] >= 45.0 || value_nums[7] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮4轴温");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //  abn_state_lstbox.Items.Add("故障部件：动量轮4轴温");
                                        }
                                        else if (value_nums[8] >= 45.0 || value_nums[8] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮5轴温");
                                            }));
                                            //    abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //     abn_state_lstbox.Items.Add("故障部件：动量轮5轴温");
                                        }
                                        else if (value_nums[9] >= 45.0 || value_nums[9] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮6轴温");
                                            }));
                                            //  abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //  abn_state_lstbox.Items.Add("故障部件：动量轮6轴温");
                                        }
                                    }
                                }
                                //读取“动量轮5马达电流”的数据并判断是否故障
                                else if (bianliang_flag == "动量轮5马达电流")
                                {
                                    b = value_nums[3];//读取动量轮某一列数据

                                    if (a >= 0.5)
                                    {
                                        label_dll.Invoke(new EventHandler(delegate { label_dll.Text = string.Format("故障"); }));
                                        label_dll.ForeColor = System.Drawing.Color.Red;
                                        if (value_nums[0] >= 2.0 || value_nums[0] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮1马达电流");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            // abn_state_lstbox.Items.Add("故障部件：动量轮1马达电流");
                                        }
                                        else if (value_nums[1] >= 2.0 || value_nums[1] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮2马达电流");
                                            }));
                                            //  abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //  abn_state_lstbox.Items.Add("故障部件：动量轮2马达电流");
                                        }
                                        else if (value_nums[2] >= 2.0 || value_nums[2] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮4马达电流");
                                            }));
                                            //  abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //   abn_state_lstbox.Items.Add("故障部件：动量轮4马达电流");
                                        }
                                        else if (value_nums[3] >= 2.0 || value_nums[3] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮5马达电流");
                                            }));
                                            //    abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //    abn_state_lstbox.Items.Add("故障部件：动量轮5马达电流");
                                        }
                                        else if (value_nums[4] >= 45.0 || value_nums[4] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮1轴温");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            // abn_state_lstbox.Items.Add("故障部件：动量轮1轴温");
                                        }
                                        else if (value_nums[5] >= 45.0 || value_nums[5] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮2轴温");
                                            }));
                                            //    abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //    abn_state_lstbox.Items.Add("故障部件：动量轮2轴温");
                                        }
                                        else if (value_nums[6] >= 45.0 || value_nums[8] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮3轴温");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            // abn_state_lstbox.Items.Add("故障部件：动量轮3轴温");
                                        }
                                        else if (value_nums[7] >= 45.0 || value_nums[7] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮4轴温");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //  abn_state_lstbox.Items.Add("故障部件：动量轮4轴温");
                                        }
                                        else if (value_nums[8] >= 45.0 || value_nums[8] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮5轴温");
                                            }));
                                            //    abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //     abn_state_lstbox.Items.Add("故障部件：动量轮5轴温");
                                        }
                                        else if (value_nums[9] >= 45.0 || value_nums[9] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮6轴温");
                                            }));
                                            //  abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //  abn_state_lstbox.Items.Add("故障部件：动量轮6轴温");
                                        }
                                    }
                                }
                                //读取“动量轮1轴温”的数据并判断是否故障
                                else if (bianliang_flag == "动量轮1轴温")
                                {
                                    b = value_nums[4];//读取动量轮某一列数据

                                    if (a >= 0.5)
                                    {
                                        label_dll.Invoke(new EventHandler(delegate { label_dll.Text = string.Format("故障"); }));
                                        label_dll.ForeColor = System.Drawing.Color.Red;
                                        if (value_nums[0] >= 2.0 || value_nums[0] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮1马达电流");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            // abn_state_lstbox.Items.Add("故障部件：动量轮1马达电流");
                                        }
                                        else if (value_nums[1] >= 2.0 || value_nums[1] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮2马达电流");
                                            }));
                                            //  abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //  abn_state_lstbox.Items.Add("故障部件：动量轮2马达电流");
                                        }
                                        else if (value_nums[2] >= 2.0 || value_nums[2] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮4马达电流");
                                            }));
                                            //  abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //   abn_state_lstbox.Items.Add("故障部件：动量轮4马达电流");
                                        }
                                        else if (value_nums[3] >= 2.0 || value_nums[3] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮5马达电流");
                                            }));
                                            //    abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //    abn_state_lstbox.Items.Add("故障部件：动量轮5马达电流");
                                        }
                                        else if (value_nums[4] >= 45.0 || value_nums[4] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮1轴温");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            // abn_state_lstbox.Items.Add("故障部件：动量轮1轴温");
                                        }
                                        else if (value_nums[5] >= 45.0 || value_nums[5] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮2轴温");
                                            }));
                                            //    abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //    abn_state_lstbox.Items.Add("故障部件：动量轮2轴温");
                                        }
                                        else if (value_nums[6] >= 45.0 || value_nums[8] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮3轴温");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            // abn_state_lstbox.Items.Add("故障部件：动量轮3轴温");
                                        }
                                        else if (value_nums[7] >= 45.0 || value_nums[7] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮4轴温");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //  abn_state_lstbox.Items.Add("故障部件：动量轮4轴温");
                                        }
                                        else if (value_nums[8] >= 45.0 || value_nums[8] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮5轴温");
                                            }));
                                            //    abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //     abn_state_lstbox.Items.Add("故障部件：动量轮5轴温");
                                        }
                                        else if (value_nums[9] >= 45.0 || value_nums[9] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮6轴温");
                                            }));
                                            //  abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //  abn_state_lstbox.Items.Add("故障部件：动量轮6轴温");
                                        }
                                    }
                                }
                                //读取“动量轮2轴温”的数据并判断是否故障
                                else if (bianliang_flag == "动量轮2轴温")
                                {
                                    b = value_nums[5];//读取动量轮某一列数据

                                    if (a >= 0.5)
                                    {
                                        label_dll.Invoke(new EventHandler(delegate { label_dll.Text = string.Format("故障"); }));
                                        label_dll.ForeColor = System.Drawing.Color.Red;
                                        if (value_nums[0] >= 2.0 || value_nums[0] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮1马达电流");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            // abn_state_lstbox.Items.Add("故障部件：动量轮1马达电流");
                                        }
                                        else if (value_nums[1] >= 2.0 || value_nums[1] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮2马达电流");
                                            }));
                                            //  abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //  abn_state_lstbox.Items.Add("故障部件：动量轮2马达电流");
                                        }
                                        else if (value_nums[2] >= 2.0 || value_nums[2] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮4马达电流");
                                            }));
                                            //  abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //   abn_state_lstbox.Items.Add("故障部件：动量轮4马达电流");
                                        }
                                        else if (value_nums[3] >= 2.0 || value_nums[3] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮5马达电流");
                                            }));
                                            //    abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //    abn_state_lstbox.Items.Add("故障部件：动量轮5马达电流");
                                        }
                                        else if (value_nums[4] >= 45.0 || value_nums[4] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮1轴温");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            // abn_state_lstbox.Items.Add("故障部件：动量轮1轴温");
                                        }
                                        else if (value_nums[5] >= 45.0 || value_nums[5] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮2轴温");
                                            }));
                                            //    abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //    abn_state_lstbox.Items.Add("故障部件：动量轮2轴温");
                                        }
                                        else if (value_nums[6] >= 45.0 || value_nums[8] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮3轴温");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            // abn_state_lstbox.Items.Add("故障部件：动量轮3轴温");
                                        }
                                        else if (value_nums[7] >= 45.0 || value_nums[7] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮4轴温");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //  abn_state_lstbox.Items.Add("故障部件：动量轮4轴温");
                                        }
                                        else if (value_nums[8] >= 45.0 || value_nums[8] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮5轴温");
                                            }));
                                            //    abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //     abn_state_lstbox.Items.Add("故障部件：动量轮5轴温");
                                        }
                                        else if (value_nums[9] >= 45.0 || value_nums[9] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮6轴温");
                                            }));
                                            //  abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //  abn_state_lstbox.Items.Add("故障部件：动量轮6轴温");
                                        }
                                    }
                                }
                                //读取“动量轮3轴温”的数据并判断是否故障
                                else if (bianliang_flag == "动量轮3轴温")
                                {
                                    b = value_nums[6];//读取动量轮某一列数据

                                    if (a >= 0.5)
                                    {
                                        label_dll.Invoke(new EventHandler(delegate { label_dll.Text = string.Format("故障"); }));
                                        label_dll.ForeColor = System.Drawing.Color.Red;
                                        if (value_nums[0] >= 2.0 || value_nums[0] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮1马达电流");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            // abn_state_lstbox.Items.Add("故障部件：动量轮1马达电流");
                                        }
                                        else if (value_nums[1] >= 2.0 || value_nums[1] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮2马达电流");
                                            }));
                                            //  abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //  abn_state_lstbox.Items.Add("故障部件：动量轮2马达电流");
                                        }
                                        else if (value_nums[2] >= 2.0 || value_nums[2] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮4马达电流");
                                            }));
                                            //  abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //   abn_state_lstbox.Items.Add("故障部件：动量轮4马达电流");
                                        }
                                        else if (value_nums[3] >= 2.0 || value_nums[3] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮5马达电流");
                                            }));
                                            //    abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //    abn_state_lstbox.Items.Add("故障部件：动量轮5马达电流");
                                        }
                                        else if (value_nums[4] >= 45.0 || value_nums[4] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮1轴温");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            // abn_state_lstbox.Items.Add("故障部件：动量轮1轴温");
                                        }
                                        else if (value_nums[5] >= 45.0 || value_nums[5] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮2轴温");
                                            }));
                                            //    abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //    abn_state_lstbox.Items.Add("故障部件：动量轮2轴温");
                                        }
                                        else if (value_nums[6] >= 45.0 || value_nums[8] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮3轴温");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            // abn_state_lstbox.Items.Add("故障部件：动量轮3轴温");
                                        }
                                        else if (value_nums[7] >= 45.0 || value_nums[7] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮4轴温");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //  abn_state_lstbox.Items.Add("故障部件：动量轮4轴温");
                                        }
                                        else if (value_nums[8] >= 45.0 || value_nums[8] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮5轴温");
                                            }));
                                            //    abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //     abn_state_lstbox.Items.Add("故障部件：动量轮5轴温");
                                        }
                                        else if (value_nums[9] >= 45.0 || value_nums[9] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮6轴温");
                                            }));
                                            //  abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //  abn_state_lstbox.Items.Add("故障部件：动量轮6轴温");
                                        }
                                    }
                                }
                                //读取“动量轮4轴温”的数据并判断是否故障
                                else if (bianliang_flag == "动量轮4轴温")
                                {
                                    b = value_nums[7];//读取动量轮某一列数据

                                    if (a >= 0.5)
                                    {
                                        label_dll.Invoke(new EventHandler(delegate { label_dll.Text = string.Format("故障"); }));
                                        label_dll.ForeColor = System.Drawing.Color.Red;
                                        if (value_nums[0] >= 2.0 || value_nums[0] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮1马达电流");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            // abn_state_lstbox.Items.Add("故障部件：动量轮1马达电流");
                                        }
                                        else if (value_nums[1] >= 2.0 || value_nums[1] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮2马达电流");
                                            }));
                                            //  abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //  abn_state_lstbox.Items.Add("故障部件：动量轮2马达电流");
                                        }
                                        else if (value_nums[2] >= 2.0 || value_nums[2] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮4马达电流");
                                            }));
                                            //  abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //   abn_state_lstbox.Items.Add("故障部件：动量轮4马达电流");
                                        }
                                        else if (value_nums[3] >= 2.0 || value_nums[3] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮5马达电流");
                                            }));
                                            //    abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //    abn_state_lstbox.Items.Add("故障部件：动量轮5马达电流");
                                        }
                                        else if (value_nums[4] >= 45.0 || value_nums[4] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮1轴温");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            // abn_state_lstbox.Items.Add("故障部件：动量轮1轴温");
                                        }
                                        else if (value_nums[5] >= 45.0 || value_nums[5] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮2轴温");
                                            }));
                                            //    abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //    abn_state_lstbox.Items.Add("故障部件：动量轮2轴温");
                                        }
                                        else if (value_nums[6] >= 45.0 || value_nums[8] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮3轴温");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            // abn_state_lstbox.Items.Add("故障部件：动量轮3轴温");
                                        }
                                        else if (value_nums[7] >= 45.0 || value_nums[7] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮4轴温");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //  abn_state_lstbox.Items.Add("故障部件：动量轮4轴温");
                                        }
                                        else if (value_nums[8] >= 45.0 || value_nums[8] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮5轴温");
                                            }));
                                            //    abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //     abn_state_lstbox.Items.Add("故障部件：动量轮5轴温");
                                        }
                                        else if (value_nums[9] >= 45.0 || value_nums[9] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮6轴温");
                                            }));
                                            //  abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //  abn_state_lstbox.Items.Add("故障部件：动量轮6轴温");
                                        }
                                    }
                                }
                                //读取“动量轮5轴温”的数据并判断是否故障
                                else if (bianliang_flag == "动量轮5轴温")
                                {
                                    b = value_nums[8];//读取动量轮某一列数据

                                    if (a >= 0.5)
                                    {
                                        label_dll.Invoke(new EventHandler(delegate { label_dll.Text = string.Format("故障"); }));
                                        label_dll.ForeColor = System.Drawing.Color.Red;
                                        if (value_nums[0] >= 2.0 || value_nums[0] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮1马达电流");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            // abn_state_lstbox.Items.Add("故障部件：动量轮1马达电流");
                                        }
                                        else if (value_nums[1] >= 2.0 || value_nums[1] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮2马达电流");
                                            }));
                                            //  abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //  abn_state_lstbox.Items.Add("故障部件：动量轮2马达电流");
                                        }
                                        else if (value_nums[2] >= 2.0 || value_nums[2] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮4马达电流");
                                            }));
                                            //  abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //   abn_state_lstbox.Items.Add("故障部件：动量轮4马达电流");
                                        }
                                        else if (value_nums[3] >= 2.0 || value_nums[3] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮5马达电流");
                                            }));
                                            //    abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //    abn_state_lstbox.Items.Add("故障部件：动量轮5马达电流");
                                        }
                                        else if (value_nums[4] >= 45.0 || value_nums[4] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮1轴温");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            // abn_state_lstbox.Items.Add("故障部件：动量轮1轴温");
                                        }
                                        else if (value_nums[5] >= 45.0 || value_nums[5] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮2轴温");
                                            }));
                                            //    abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //    abn_state_lstbox.Items.Add("故障部件：动量轮2轴温");
                                        }
                                        else if (value_nums[6] >= 45.0 || value_nums[8] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮3轴温");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            // abn_state_lstbox.Items.Add("故障部件：动量轮3轴温");
                                        }
                                        else if (value_nums[7] >= 45.0 || value_nums[7] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮4轴温");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //  abn_state_lstbox.Items.Add("故障部件：动量轮4轴温");
                                        }
                                        else if (value_nums[8] >= 45.0 || value_nums[8] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮5轴温");
                                            }));
                                            //    abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //     abn_state_lstbox.Items.Add("故障部件：动量轮5轴温");
                                        }
                                        else if (value_nums[9] >= 45.0 || value_nums[9] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮6轴温");
                                            }));
                                            //  abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //  abn_state_lstbox.Items.Add("故障部件：动量轮6轴温");
                                        }
                                    }
                                }
                                //读取“动量轮6轴温”的数据并判断是否故障
                                else if (bianliang_flag == "动量轮6轴温")
                                {
                                    b = value_nums[9];//读取动量轮某一列数据
                                    /*
                                    this.chart_data.ChartAreas[0].AxisY.Title = "动量轮6轴温/度";
                                    this.chart_data.Titles[0].Text = "动量轮6轴温";
                                    this.chart_data.Series[0].LegendText = "动量轮6轴温";
                                    */
                                    if (a >= 0.5)
                                    {
                                        label_dll.Invoke(new EventHandler(delegate { label_dll.Text = string.Format("故障"); }));
                                        label_dll.ForeColor = System.Drawing.Color.Red;
                                        if (value_nums[0] >= 2.0 || value_nums[0] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮1马达电流");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            // abn_state_lstbox.Items.Add("故障部件：动量轮1马达电流");
                                        }
                                        else if (value_nums[1] >= 2.0 || value_nums[1] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮2马达电流");
                                            }));
                                            //  abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //  abn_state_lstbox.Items.Add("故障部件：动量轮2马达电流");
                                        }
                                        else if (value_nums[2] >= 2.0 || value_nums[2] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮4马达电流");
                                            }));
                                            //  abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //   abn_state_lstbox.Items.Add("故障部件：动量轮4马达电流");
                                        }
                                        else if (value_nums[3] >= 2.0 || value_nums[3] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮5马达电流");
                                            }));
                                            //    abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //    abn_state_lstbox.Items.Add("故障部件：动量轮5马达电流");
                                        }
                                        else if (value_nums[4] >= 45.0 || value_nums[4] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮1轴温");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            // abn_state_lstbox.Items.Add("故障部件：动量轮1轴温");
                                        }
                                        else if (value_nums[5] >= 45.0 || value_nums[5] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮2轴温");
                                            }));
                                            //    abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //    abn_state_lstbox.Items.Add("故障部件：动量轮2轴温");
                                        }
                                        else if (value_nums[6] >= 45.0 || value_nums[8] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮3轴温");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            // abn_state_lstbox.Items.Add("故障部件：动量轮3轴温");
                                        }
                                        else if (value_nums[7] >= 45.0 || value_nums[7] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮4轴温");
                                            }));
                                            // abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //  abn_state_lstbox.Items.Add("故障部件：动量轮4轴温");
                                        }
                                        else if (value_nums[8] >= 45.0 || value_nums[8] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮5轴温");
                                            }));
                                            //    abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //     abn_state_lstbox.Items.Add("故障部件：动量轮5轴温");
                                        }
                                        else if (value_nums[9] >= 45.0 || value_nums[9] <= -5.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：动量轮6轴温");
                                            }));
                                            //  abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                            //  abn_state_lstbox.Items.Add("故障部件：动量轮6轴温");
                                        }
                                    }
                                }
                                #endregion

                                if (i % (count_txt_readline / count_config_chartpoint) == 0)
                                {

                                    dt_pro.Rows.Add(dll_shijian_hengzuobiao, a);
                                    dt_data.Rows.Add(dll_shijian_hengzuobiao, b);
                                }

                                if (dll_nianyueri == dll_nianyueri_end_ST && dll_shifen == dll_shifen_end_ST)//如果到达结束时间，停止画图
                                {
                                    ST_flag = 0;
                                    ShowMessage(progress.endhandle);
                                    this.Invoke(new EventHandler(delegate { tabledraw(); }));
                                    return;
                                }
                            }
                        }
                        line = reader.ReadLine();
                        i += 1;
                    }
                }
            }
        }
        private void readData_fsj()
        {
            //fsj_file = get_fsj_file_extent(fsj_file);
            ShowMessage(progress.waitcount);
            getlinecountfsj();
            if (count_txt_readline == 0) { ShowMessage(progress.Error_noline); return; }
            ST_flag = 0;
            ShowMessage(progress.endcount);
            ShowMessage(progress.waithandle);
            for (int t = 0; t < fsj_file.Length; t++)
            {
                using (StreamReader reader = new StreamReader("./读取多个文件实验/辐射计/" + fsj_file[t]))
                {
                    int i = 0;
                    //int ST_flag = 0;//是否画图的标志

                    string line = reader.ReadLine();
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (i >= 5)
                        {
                            string[] line_sp = line.Split('\t');// MessageBox.Show(line_sp[1]);
                            double[] value_nums = new double[2];
                            value_nums[0] = double.Parse(line_sp[3]);
                            try { value_nums[1] = double.Parse(line_sp[6]); }
                            catch (Exception e) { MessageBox.Show(e + "!!!!" + i + "!!!"); }
                            // value_nums[1] = double.Parse(line_sp[6]);
                            //double a = TEST.Class1.test_fsj(value_nums);
                            double a = 0;
                            double b = 0;
                            //#############################################################
                            //判断开始时间
                            string time_X = string.Format("{0}", TimeStampToDateTime(line_sp[0]));
                            string dll_nianyueri = time_X.Replace("/", "").Substring(0, 7);
                            string dll_shifen = time_X.Substring(10, 5);
                            string dll_shijian_hengzuobiao = time_X.Substring(0, 15);
                            //Console.WriteLine(dll_nianyueri, dll_shifen);
                            if (dll_nianyueri == dll_nianyueri_start_ST && dll_shifen == dll_shifen_start_ST)
                            {
                                ST_flag = 1;
                                //#############################################################
                                //读取“读取辐射计东西总电流”的数据并判断是否故障
                                #region --------------------------------------------------------------
                                if (bianliang_flag == "辐射计东西总电流")
                                {
                                    b = value_nums[0];
                                    a = TEST.Class1.test_fsj(value_nums);//
                                    if (a >= 0.5)
                                    {
                                        label_dll.Invoke(new EventHandler(delegate { label_dll.Text = string.Format("故障"); }));
                                        label_dll.ForeColor = System.Drawing.Color.Red;
                                        if (value_nums[0] >= 2.0 || value_nums[0] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：辐射计东西总电流");
                                            }));

                                        }
                                        else if (value_nums[1] >= 2.0 || value_nums[1] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：辐射计南北总电流");
                                            }));

                                        }
                                    }
                                }
                                //读取“辐射计南北总电流”的数据并判断是否故障
                                else if (bianliang_flag == "辐射计南北总电流")
                                {
                                    b = value_nums[1];//读取动量轮某一列数据
                                    a = TEST.Class1.test_fsj(value_nums);
                                    if (a >= 0.5)
                                    {
                                        label_dll.Invoke(new EventHandler(delegate { label_dll.Text = string.Format("故障"); }));
                                        label_dll.ForeColor = System.Drawing.Color.Red;
                                        if (value_nums[0] >= 2.0 || value_nums[0] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：辐射计东西总电流");
                                            }));

                                        }
                                        else if (value_nums[1] >= 2.0 || value_nums[1] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：辐射计南北总电流");
                                            }));

                                        }
                                    }
                                }
                                #endregion
                                //if (i % (float)(count_txt_readline / count_config_chartpoint+1) == 0)
                                if (i % 2 == 0)
                                    {
                                    dt_pro.Rows.Add(dll_shijian_hengzuobiao, a);
                                    dt_data.Rows.Add(dll_shijian_hengzuobiao, b);
                                }
                            }
                            else if (ST_flag == 1)
                            {
                                //#############################################################
                                //读取“读取辐射计东西总电流”的数据并判断是否故障
                                #region---------------------------------------------------------
                                if (bianliang_flag == "辐射计东西总电流")
                                {
                                    b = value_nums[0];//
                                    a = TEST.Class1.test_fsj(value_nums);
                                    if (a >= 0.5)
                                    {
                                        label_dll.Invoke(new EventHandler(delegate { label_dll.Text = string.Format("故障"); }));
                                        label_dll.ForeColor = System.Drawing.Color.Red;
                                        if (value_nums[0] >= 2.0 || value_nums[0] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：辐射计东西总电流");
                                            }));

                                        }
                                        else if (value_nums[1] >= 2.0 || value_nums[1] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：辐射计南北总电流");
                                            }));

                                        }
                                    }
                                }
                                //读取“辐射计南北总电流”的数据并判断是否故障
                                else if (bianliang_flag == "辐射计南北总电流")
                                {
                                    b = value_nums[1];//读取动量轮某一列数据
                                    a = TEST.Class1.test_fsj(value_nums);
                                    if (a >= 0.5)
                                    {
                                        label_dll.Invoke(new EventHandler(delegate { label_dll.Text = string.Format("故障"); }));
                                        label_dll.ForeColor = System.Drawing.Color.Red;
                                        if (value_nums[0] >= 2.0 || value_nums[0] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：辐射计东西总电流");
                                            }));

                                        }
                                        else if (value_nums[1] >= 2.0 || value_nums[1] <= -2.0)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：辐射计南北总电流");
                                            }));

                                        }
                                    }
                                }
                                #endregion
                                if (i % 2 == 0)
                                {
                                    dt_pro.Rows.Add(dll_shijian_hengzuobiao, a);
                                    dt_data.Rows.Add(dll_shijian_hengzuobiao, b);
                                }
                                if (dll_nianyueri == dll_nianyueri_end_ST && dll_shifen == dll_shifen_end_ST)
                                {
                                    ST_flag = 0;
                                    this.Invoke(new EventHandler(delegate { tabledraw(); }));
                                    ShowMessage(progress.endhandle);

                                    return;
                                }
                            }

                        }
                        line = reader.ReadLine();
                        i += 1;
                    }
                    //read_first_num += 5;
                    //  this.Invoke(new EventHandler(delegate { tabledraw(); }));
                }
            }

        }
        private void readData_tcy()
        {
            tcy_file = get_fsj_file_extent(tcy_file);
            ShowMessage(progress.waitcount);
            getlinecounttcy();
            if (count_txt_readline == 0) { ShowMessage(progress.Error_noline); return; }
            ST_flag = 0;
            ShowMessage(progress.endcount);
            ShowMessage(progress.waithandle);
            for (int t = 0; t < tcy_file.Length; t++)
            {
                using (StreamReader reader = new StreamReader("./读取多个文件实验/探测仪/" + tcy_file[t]))
                {
                    int i = 0;
                    // int ST_flag = 0;//是否画图的标志

                    string line = reader.ReadLine();
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (i >= read_first_num)
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

                            tcy_value_nums[0] = double.Parse(line_sp[3]);
                            tcy_value_nums[1] = double.Parse(line_sp[6]);
                            double a = TEST.Class1.test_tcy(tcy_value_nums);
                            double b = 0;
                            //#############################################################
                            //判断开始时间
                            string time_X = string.Format("{0}", TimeStampToDateTime(line_sp[0]));
                            string dll_nianyueri = time_X.Replace("/", "").Substring(0, 7);
                            string dll_shifen = time_X.Substring(10, 5);
                            string dll_shijian_hengzuobiao = time_X.Substring(0, 15);
                            //Console.WriteLine(dll_nianyueri, dll_shifen);
                            if (dll_nianyueri == dll_nianyueri_start_ST && dll_shifen == dll_shifen_start_ST)
                            {
                                ST_flag = 1;
                                //#############################################################
                                //读取“探测仪东西电机A相电流”的数据并判断是否故障
                                #region

                                if (bianliang_flag == "探测仪东西电机A相电流")
                                {
                                    b = value_nums[0];//
                                    if (a >= 0.5)
                                    {
                                        label_dll.Invoke(new EventHandler(delegate { label_dll.Text = string.Format("故障"); }));
                                        label_dll.ForeColor = System.Drawing.Color.Red;
                                        if (value_nums[0] >= 0.6 || value_nums[0] <= -0.6)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：探测仪东西电机A相电流");
                                            }));

                                        }
                                        else if (value_nums[1] >= 0.6 || value_nums[1] <= -0.6)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：探测仪东西电机B相电流");
                                            }));

                                        }
                                        else if (value_nums[2] >= 0.6 || value_nums[2] <= -0.6)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：探测仪东西电机总电流");
                                            }));

                                        }
                                        else if (value_nums[3] >= 0.6 || value_nums[3] <= -0.6)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：探测仪南北电机A相电流");
                                            }));

                                        }
                                        else if (value_nums[4] >= 0.6 || value_nums[4] <= -0.6)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：探测仪南北电机B相电流");
                                            }));

                                        }
                                        else if (value_nums[5] >= 0.6 || value_nums[5] <= -0.6)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：探测仪南北电机总电流");
                                            }));

                                        }
                                    }
                                }
                                //读取“探测仪东西电机B相电流”的数据并判断是否故障
                                else if (bianliang_flag == "探测仪东西电机B相电流")
                                {
                                    b = value_nums[1];//读取动量轮某一列数据

                                    if (a >= 0.5)
                                    {
                                        label_dll.Invoke(new EventHandler(delegate { label_dll.Text = string.Format("故障"); }));
                                        label_dll.ForeColor = System.Drawing.Color.Red;
                                        if (value_nums[0] >= 0.6 || value_nums[0] <= -0.6)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：探测仪东西电机A相电流");
                                            }));

                                        }
                                        else if (value_nums[1] >= 0.6 || value_nums[1] <= -0.6)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：探测仪东西电机B相电流");
                                            }));

                                        }
                                        else if (value_nums[2] >= 0.6 || value_nums[2] <= -0.6)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：探测仪东西电机总电流");
                                            }));

                                        }
                                        else if (value_nums[3] >= 0.6 || value_nums[3] <= -0.6)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：探测仪南北电机A相电流");
                                            }));

                                        }
                                        else if (value_nums[4] >= 0.6 || value_nums[4] <= -0.6)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：探测仪南北电机B相电流");
                                            }));

                                        }
                                        else if (value_nums[5] >= 0.6 || value_nums[5] <= -0.6)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：探测仪南北电机总电流");
                                            }));

                                        }
                                    }
                                }
                                //读取“探测仪东西电机总电流”的数据并判断是否故障
                                else if (bianliang_flag == "探测仪东西电机总电流")
                                {
                                    b = value_nums[2];//读取动量轮某一列数据

                                    if (a >= 0.5)
                                    {
                                        label_dll.Invoke(new EventHandler(delegate { label_dll.Text = string.Format("故障"); }));
                                        label_dll.ForeColor = System.Drawing.Color.Red;
                                        if (value_nums[0] >= 0.6 || value_nums[0] <= -0.6)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：探测仪东西电机A相电流");
                                            }));

                                        }
                                        else if (value_nums[1] >= 0.6 || value_nums[1] <= -0.6)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：探测仪东西电机B相电流");
                                            }));

                                        }
                                        else if (value_nums[2] >= 0.6 || value_nums[2] <= -0.6)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：探测仪东西电机总电流");
                                            }));

                                        }
                                        else if (value_nums[3] >= 0.6 || value_nums[3] <= -0.6)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：探测仪南北电机A相电流");
                                            }));

                                        }
                                        else if (value_nums[4] >= 0.6 || value_nums[4] <= -0.6)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：探测仪南北电机B相电流");
                                            }));

                                        }
                                        else if (value_nums[5] >= 0.6 || value_nums[5] <= -0.6)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：探测仪南北电机总电流");
                                            }));

                                        }
                                    }
                                }
                                //读取“探测仪南北电机A相电流”的数据并判断是否故障
                                else if (bianliang_flag == "探测仪南北电机A相电流")
                                {
                                    b = value_nums[3];//读取动量轮某一列数据

                                    if (a >= 0.5)
                                    {
                                        label_dll.Invoke(new EventHandler(delegate { label_dll.Text = string.Format("故障"); }));
                                        label_dll.ForeColor = System.Drawing.Color.Red;
                                        if (value_nums[0] >= 0.6 || value_nums[0] <= -0.6)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：探测仪东西电机A相电流");
                                            }));

                                        }
                                        else if (value_nums[1] >= 0.6 || value_nums[1] <= -0.6)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：探测仪东西电机B相电流");
                                            }));

                                        }
                                        else if (value_nums[2] >= 0.6 || value_nums[2] <= -0.6)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：探测仪东西电机总电流");
                                            }));

                                        }
                                        else if (value_nums[3] >= 0.6 || value_nums[3] <= -0.6)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：探测仪南北电机A相电流");
                                            }));

                                        }
                                        else if (value_nums[4] >= 0.6 || value_nums[4] <= -0.6)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：探测仪南北电机B相电流");
                                            }));

                                        }
                                        else if (value_nums[5] >= 0.6 || value_nums[5] <= -0.6)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：探测仪南北电机总电流");
                                            }));

                                        }
                                    }
                                }
                                //读取“探测仪南北电机B相电流”的数据并判断是否故障
                                else if (bianliang_flag == "探测仪南北电机B相电流")
                                {
                                    b = value_nums[4];//读取动量轮某一列数据

                                    if (a >= 0.5)
                                    {
                                        label_dll.Invoke(new EventHandler(delegate { label_dll.Text = string.Format("故障"); }));
                                        label_dll.ForeColor = System.Drawing.Color.Red;
                                        if (value_nums[0] >= 0.6 || value_nums[0] <= -0.6)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：探测仪东西电机A相电流");
                                            }));

                                        }
                                        else if (value_nums[1] >= 0.6 || value_nums[1] <= -0.6)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：探测仪东西电机B相电流");
                                            }));

                                        }
                                        else if (value_nums[2] >= 0.6 || value_nums[2] <= -0.6)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：探测仪东西电机总电流");
                                            }));

                                        }
                                        else if (value_nums[3] >= 0.6 || value_nums[3] <= -0.6)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：探测仪南北电机A相电流");
                                            }));

                                        }
                                        else if (value_nums[4] >= 0.6 || value_nums[4] <= -0.6)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：探测仪南北电机B相电流");
                                            }));

                                        }
                                        else if (value_nums[5] >= 0.6 || value_nums[5] <= -0.6)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：探测仪南北电机总电流");
                                            }));

                                        }
                                    }
                                }
                                //读取“探测仪南北电机总电流”的数据并判断是否故障
                                else if (bianliang_flag == "探测仪南北电机总电流")
                                {
                                    b = value_nums[5];//读取动量轮某一列数据

                                    if (a >= 0.5)
                                    {
                                        label_dll.Invoke(new EventHandler(delegate { label_dll.Text = string.Format("故障"); }));
                                        label_dll.ForeColor = System.Drawing.Color.Red;
                                        if (value_nums[0] >= 0.6 || value_nums[0] <= -0.6)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：探测仪东西电机A相电流");
                                            }));

                                        }
                                        else if (value_nums[1] >= 0.6 || value_nums[1] <= -0.6)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：探测仪东西电机B相电流");
                                            }));

                                        }
                                        else if (value_nums[2] >= 0.6 || value_nums[2] <= -0.6)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：探测仪东西电机总电流");
                                            }));

                                        }
                                        else if (value_nums[3] >= 0.6 || value_nums[3] <= -0.6)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：探测仪南北电机A相电流");
                                            }));

                                        }
                                        else if (value_nums[4] >= 0.6 || value_nums[4] <= -0.6)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：探测仪南北电机B相电流");
                                            }));

                                        }
                                        else if (value_nums[5] >= 0.6 || value_nums[5] <= -0.6)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：探测仪南北电机总电流");
                                            }));

                                        }
                                    }
                                }
                                #endregion
                                if (i % (count_txt_readline / count_config_chartpoint) == 0)
                                {
                                    dt_pro.Rows.Add(dll_shijian_hengzuobiao, a);
                                    dt_data.Rows.Add(dll_shijian_hengzuobiao, b);
                                }
                            }
                            else if (ST_flag == 1)
                            {
                                //#############################################################
                                //读取“探测仪东西电机A相电流”的数据并判断是否故障
                                #region

                                if (bianliang_flag == "探测仪东西电机A相电流")
                                {
                                    b = value_nums[0];//
                                    if (a >= 0.5)
                                    {
                                        label_dll.Invoke(new EventHandler(delegate { label_dll.Text = string.Format("故障"); }));
                                        label_dll.ForeColor = System.Drawing.Color.Red;
                                        if (value_nums[0] >= 0.6 || value_nums[0] <= -0.6)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：探测仪东西电机A相电流");
                                            }));

                                        }
                                        else if (value_nums[1] >= 0.6 || value_nums[1] <= -0.6)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：探测仪东西电机B相电流");
                                            }));

                                        }
                                        else if (value_nums[2] >= 0.6 || value_nums[2] <= -0.6)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：探测仪东西电机总电流");
                                            }));

                                        }
                                        else if (value_nums[3] >= 0.6 || value_nums[3] <= -0.6)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：探测仪南北电机A相电流");
                                            }));

                                        }
                                        else if (value_nums[4] >= 0.6 || value_nums[4] <= -0.6)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：探测仪南北电机B相电流");
                                            }));

                                        }
                                        else if (value_nums[5] >= 0.6 || value_nums[5] <= -0.6)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：探测仪南北电机总电流");
                                            }));

                                        }
                                    }
                                }
                                //读取“探测仪东西电机B相电流”的数据并判断是否故障
                                else if (bianliang_flag == "探测仪东西电机B相电流")
                                {
                                    b = value_nums[1];//读取动量轮某一列数据

                                    if (a >= 0.5)
                                    {
                                        label_dll.Invoke(new EventHandler(delegate { label_dll.Text = string.Format("故障"); }));
                                        label_dll.ForeColor = System.Drawing.Color.Red;
                                        if (value_nums[0] >= 0.6 || value_nums[0] <= -0.6)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：探测仪东西电机A相电流");
                                            }));

                                        }
                                        else if (value_nums[1] >= 0.6 || value_nums[1] <= -0.6)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：探测仪东西电机B相电流");
                                            }));

                                        }
                                        else if (value_nums[2] >= 0.6 || value_nums[2] <= -0.6)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：探测仪东西电机总电流");
                                            }));

                                        }
                                        else if (value_nums[3] >= 0.6 || value_nums[3] <= -0.6)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：探测仪南北电机A相电流");
                                            }));

                                        }
                                        else if (value_nums[4] >= 0.6 || value_nums[4] <= -0.6)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：探测仪南北电机B相电流");
                                            }));

                                        }
                                        else if (value_nums[5] >= 0.6 || value_nums[5] <= -0.6)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：探测仪南北电机总电流");
                                            }));

                                        }
                                    }
                                }
                                //读取“探测仪东西电机总电流”的数据并判断是否故障
                                else if (bianliang_flag == "探测仪东西电机总电流")
                                {
                                    b = value_nums[2];//读取动量轮某一列数据

                                    if (a >= 0.5)
                                    {
                                        label_dll.Invoke(new EventHandler(delegate { label_dll.Text = string.Format("故障"); }));
                                        label_dll.ForeColor = System.Drawing.Color.Red;
                                        if (value_nums[0] >= 0.6 || value_nums[0] <= -0.6)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：探测仪东西电机A相电流");
                                            }));

                                        }
                                        else if (value_nums[1] >= 0.6 || value_nums[1] <= -0.6)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：探测仪东西电机B相电流");
                                            }));

                                        }
                                        else if (value_nums[2] >= 0.6 || value_nums[2] <= -0.6)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：探测仪东西电机总电流");
                                            }));

                                        }
                                        else if (value_nums[3] >= 0.6 || value_nums[3] <= -0.6)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：探测仪南北电机A相电流");
                                            }));

                                        }
                                        else if (value_nums[4] >= 0.6 || value_nums[4] <= -0.6)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：探测仪南北电机B相电流");
                                            }));

                                        }
                                        else if (value_nums[5] >= 0.6 || value_nums[5] <= -0.6)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：探测仪南北电机总电流");
                                            }));

                                        }
                                    }
                                }
                                //读取“探测仪南北电机A相电流”的数据并判断是否故障
                                else if (bianliang_flag == "探测仪南北电机A相电流")
                                {
                                    b = value_nums[3];//读取动量轮某一列数据

                                    if (a >= 0.5)
                                    {
                                        label_dll.Invoke(new EventHandler(delegate { label_dll.Text = string.Format("故障"); }));
                                        label_dll.ForeColor = System.Drawing.Color.Red;
                                        if (value_nums[0] >= 0.6 || value_nums[0] <= -0.6)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：探测仪东西电机A相电流");
                                            }));

                                        }
                                        else if (value_nums[1] >= 0.6 || value_nums[1] <= -0.6)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：探测仪东西电机B相电流");
                                            }));

                                        }
                                        else if (value_nums[2] >= 0.6 || value_nums[2] <= -0.6)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：探测仪东西电机总电流");
                                            }));

                                        }
                                        else if (value_nums[3] >= 0.6 || value_nums[3] <= -0.6)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：探测仪南北电机A相电流");
                                            }));

                                        }
                                        else if (value_nums[4] >= 0.6 || value_nums[4] <= -0.6)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：探测仪南北电机B相电流");
                                            }));

                                        }
                                        else if (value_nums[5] >= 0.6 || value_nums[5] <= -0.6)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：探测仪南北电机总电流");
                                            }));

                                        }
                                    }
                                }
                                //读取“探测仪南北电机B相电流”的数据并判断是否故障
                                else if (bianliang_flag == "探测仪南北电机B相电流")
                                {
                                    b = value_nums[4];//读取动量轮某一列数据

                                    if (a >= 0.5)
                                    {
                                        label_dll.Invoke(new EventHandler(delegate { label_dll.Text = string.Format("故障"); }));
                                        label_dll.ForeColor = System.Drawing.Color.Red;
                                        if (value_nums[0] >= 0.6 || value_nums[0] <= -0.6)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：探测仪东西电机A相电流");
                                            }));

                                        }
                                        else if (value_nums[1] >= 0.6 || value_nums[1] <= -0.6)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：探测仪东西电机B相电流");
                                            }));

                                        }
                                        else if (value_nums[2] >= 0.6 || value_nums[2] <= -0.6)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：探测仪东西电机总电流");
                                            }));

                                        }
                                        else if (value_nums[3] >= 0.6 || value_nums[3] <= -0.6)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：探测仪南北电机A相电流");
                                            }));

                                        }
                                        else if (value_nums[4] >= 0.6 || value_nums[4] <= -0.6)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：探测仪南北电机B相电流");
                                            }));

                                        }
                                        else if (value_nums[5] >= 0.6 || value_nums[5] <= -0.6)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：探测仪南北电机总电流");
                                            }));

                                        }
                                    }
                                }
                                //读取“探测仪南北电机总电流”的数据并判断是否故障
                                else if (bianliang_flag == "探测仪南北电机总电流")
                                {
                                    b = value_nums[5];//读取动量轮某一列数据

                                    if (a >= 0.5)
                                    {
                                        label_dll.Invoke(new EventHandler(delegate { label_dll.Text = string.Format("故障"); }));
                                        label_dll.ForeColor = System.Drawing.Color.Red;
                                        if (value_nums[0] >= 0.6 || value_nums[0] <= -0.6)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：探测仪东西电机A相电流");
                                            }));

                                        }
                                        else if (value_nums[1] >= 0.6 || value_nums[1] <= -0.6)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：探测仪东西电机B相电流");
                                            }));

                                        }
                                        else if (value_nums[2] >= 0.6 || value_nums[2] <= -0.6)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：探测仪东西电机总电流");
                                            }));

                                        }
                                        else if (value_nums[3] >= 0.6 || value_nums[3] <= -0.6)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：探测仪南北电机A相电流");
                                            }));

                                        }
                                        else if (value_nums[4] >= 0.6 || value_nums[4] <= -0.6)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：探测仪南北电机B相电流");
                                            }));

                                        }
                                        else if (value_nums[5] >= 0.6 || value_nums[5] <= -0.6)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：探测仪南北电机总电流");
                                            }));

                                        }
                                    }
                                }
                                #endregion
                                if (i % (count_txt_readline / count_config_chartpoint) == 0)
                                {
                                    dt_pro.Rows.Add(dll_shijian_hengzuobiao, a);
                                    dt_data.Rows.Add(dll_shijian_hengzuobiao, b);
                                }
                                if (dll_nianyueri == dll_nianyueri_end_ST && dll_shifen == dll_shifen_end_ST)
                                {
                                    ST_flag = 0;
                                    ShowMessage(progress.endhandle);
                                    this.Invoke(new EventHandler(delegate { tabledraw(); }));
                                    return;
                                }
                            }


                        }
                        line = reader.ReadLine();
                        i += 1;
                    }
                    //read_first_num += 5;
                    // this.Invoke(new EventHandler(delegate { tabledraw(); }));
                }
            }
        }
        private void readData_dy()
        {
            dy_file = get_dll_file_extent(dy_file);
            ShowMessage(progress.waitcount);
            getlinecountdy();
            if (count_txt_readline == 0) { ShowMessage(progress.Error_noline); return; }
            ST_flag = 0;
            ShowMessage(progress.endcount);
            ShowMessage(progress.waithandle);
            for (int t = 0; t < dy_file.Length; t++)
            {

                using (StreamReader reader = new StreamReader("./读取多个文件实验/电源/" + dy_file[t]))
                {
                    int i = 0;
                    // int ST_flag = 0;//是否画图的标志
                    string line = reader.ReadLine();
                    while (reader.Peek() > -1)
                    {
                        if (i >= read_first_num)
                        {
                            string[] line_sp = line.Split('\t');
                            double[] value_nums = new double[49];
                            Console.WriteLine("liu");
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

                            Console.WriteLine("yibo");
                            double a = 0;
                            double b = 0;
                            //#############################################################
                            //判断开始时间
                            string dy_shijian = line_sp[0];
                            dy_shijian.Replace(" ", "");
                            string dy_nianyueri = dy_shijian.Substring(0, 8);
                        //    if (dy_nianyueri != dll_nianyueri_start_ST) { continue; }
                            string dy_shifen = dy_shijian.Substring(9, 5);
                            string dy_shijian_hengzuobiao = dy_shijian.Substring(0, 14);
                            //dt_pro.Rows.Add(dy_shijian_hengzuobiao, a);
                            //dt_data.Rows.Add(dy_shijian_hengzuobiao, b);
                            //Console.WriteLine(dll_nianyueri, dll_shifen);
                            if (dy_nianyueri == dll_nianyueri_start_ST && dy_shifen == dll_shifen_start_ST)
                            {
                                ST_flag = 1;
                                //#############################################################
                                //读取“读取电源28V母线电压”的数据并判断是否故障
                                #region
                                if (bianliang_flag == "28V母线电压")
                                {
                                    b = value_nums[0];
                                    a = TEST.Class1.test_dy(value_nums);//
                                    if (a >= 0.5)
                                    {
                                        label_dll.Invoke(new EventHandler(delegate { label_dll.Text = string.Format("故障"); }));
                                        label_dll.ForeColor = System.Drawing.Color.Red;
                                        if (value_nums[0] >= 28.5 || value_nums[0] <= 27.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：28V母线电压");
                                            }));

                                        }
                                        else if (value_nums[1] >= 43 || value_nums[1] <= -0.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：28V负载电流");
                                            }));

                                        }
                                        else if (value_nums[2] >= 43 || value_nums[2] <= 41)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：42V母线电压");
                                            }));

                                        }
                                        else if (value_nums[3] >= 78 || value_nums[3] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：42V负载电流");
                                            }));

                                        }
                                        else if (value_nums[4] >= 39 || value_nums[4] <= -0.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组电池电压1");
                                            }));

                                        }
                                        else if (value_nums[5] >= 39 || value_nums[5] <= -0.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组电池电压1");
                                            }));

                                        }
                                        else if (value_nums[6] >= 110 || value_nums[6] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：太阳电池阵输出电流");
                                            }));

                                        }
                                        else if (value_nums[7] >= 39 || value_nums[7] <= -0.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组电池电压2");
                                            }));

                                        }
                                        else if (value_nums[8] >= 39 || value_nums[8] <= -0.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组电池电压2");
                                            }));

                                        }
                                        else if (value_nums[9] >= 22 || value_nums[9] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：MEA电压");
                                            }));

                                        }
                                        else if (value_nums[10] >= 35 || value_nums[10] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组电池温度1");
                                            }));

                                        }
                                        else if (value_nums[11] >= 35 || value_nums[11] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组电池温度2");
                                            }));

                                        }
                                        else if (value_nums[12] >= 35 || value_nums[12] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组电池温度1");
                                            }));

                                        }
                                        else if (value_nums[13] >= 35 || value_nums[13] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组电池温度2");
                                            }));

                                        }
                                        else if (value_nums[14] >= 35 || value_nums[14] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组电池温度4");
                                            }));

                                        }
                                        else if (value_nums[15] >= 35 || value_nums[15] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组电池温度3");
                                            }));

                                        }
                                        else if (value_nums[16] >= 35 || value_nums[16] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组电池温度4");
                                            }));

                                        }
                                        else if (value_nums[17] >= 55 || value_nums[17] <= -15)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：充放电模块温度");
                                            }));

                                        }
                                        else if (value_nums[18] >= 55 || value_nums[18] <= -15)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：分流模块温度");
                                            }));

                                        }
                                        else if (value_nums[19] >= 12 || value_nums[19] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A1充电电流");
                                            }));

                                        }
                                        else if (value_nums[20] >= 12 || value_nums[20] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A2充电电流");
                                            }));

                                        }
                                        else if (value_nums[21] >= 12 || value_nums[21] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B1充电电流");
                                            }));

                                        }
                                        else if (value_nums[22] >= 12 || value_nums[22] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B2充电电流");
                                            }));

                                        }
                                        else if (value_nums[23] >= 20 || value_nums[23] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A1放电电流");
                                            }));

                                        }
                                        else if (value_nums[24] >= 20 || value_nums[24] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A2放电电流");
                                            }));

                                        }
                                        else if (value_nums[25] >= 20 || value_nums[25] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A3放电电流");
                                            }));

                                        }
                                        else if (value_nums[26] >= 20 || value_nums[26] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A4放电电流");
                                            }));

                                        }
                                        else if (value_nums[27] >= 20 || value_nums[27] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B1放电电流");
                                            }));

                                        }
                                        else if (value_nums[28] >= 20 || value_nums[28] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B2放电电流");
                                            }));

                                        }
                                        else if (value_nums[29] >= 20 || value_nums[29] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B3放电电流");
                                            }));

                                        }
                                        else if (value_nums[30] >= 20 || value_nums[30] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B4放电电流");
                                            }));

                                        }
                                        else if (value_nums[31] >= 4.5 || value_nums[31] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体1电压");
                                            }));

                                        }
                                        else if (value_nums[32] >= 4.5 || value_nums[32] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体2电压");
                                            }));

                                        }
                                        else if (value_nums[33] >= 4.5 || value_nums[33] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体3电压");
                                            }));

                                        }
                                        else if (value_nums[34] >= 4.5 || value_nums[34] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体4电压");
                                            }));

                                        }
                                        else if (value_nums[35] >= 4.5 || value_nums[35] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体5电压");
                                            }));

                                        }
                                        else if (value_nums[36] >= 4.5 || value_nums[36] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体6电压");
                                            }));

                                        }
                                        else if (value_nums[37] >= 4.5 || value_nums[37] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体7电压");
                                            }));

                                        }
                                        else if (value_nums[38] >= 4.5 || value_nums[38] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体8电压");
                                            }));

                                        }
                                        else if (value_nums[39] >= 4.5 || value_nums[39] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体9电压");
                                            }));

                                        }
                                        else if (value_nums[40] >= 4.5 || value_nums[40] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体1电压");
                                            }));

                                        }
                                        else if (value_nums[41] >= 4.5 || value_nums[41] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体2电压");
                                            }));

                                        }
                                        else if (value_nums[42] >= 4.5 || value_nums[42] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体3电压");
                                            }));

                                        }
                                        else if (value_nums[43] >= 4.5 || value_nums[43] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体4电压");
                                            }));

                                        }
                                        else if (value_nums[44] >= 4.5 || value_nums[44] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体5电压");
                                            }));

                                        }
                                        else if (value_nums[45] >= 4.5 || value_nums[45] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体6电压");
                                            }));

                                        }
                                        else if (value_nums[46] >= 4.5 || value_nums[46] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体7电压");
                                            }));

                                        }
                                        else if (value_nums[47] >= 4.5 || value_nums[47] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体8电压");
                                            }));

                                        }
                                        else if (value_nums[48] >= 4.5 || value_nums[48] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体9电压");
                                            }));

                                        }
                                    }
                                }
                                //读取“读取电源28V负载电流”的数据并判断是否故障
                                else if (bianliang_flag == "28V负载电流")
                                {
                                    b = value_nums[1];
                                    a = TEST.Class1.test_dy(value_nums);//
                                    if (a >= 0.5)
                                    {
                                        label_dll.Invoke(new EventHandler(delegate { label_dll.Text = string.Format("故障"); }));
                                        label_dll.ForeColor = System.Drawing.Color.Red;
                                        if (value_nums[0] >= 28.5 || value_nums[0] <= 27.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：28V母线电压");
                                            }));

                                        }
                                        else if (value_nums[1] >= 43 || value_nums[1] <= -0.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：28V负载电流");
                                            }));

                                        }
                                        else if (value_nums[2] >= 43 || value_nums[2] <= 41)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：42V母线电压");
                                            }));

                                        }
                                        else if (value_nums[3] >= 78 || value_nums[3] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：42V负载电流");
                                            }));

                                        }
                                        else if (value_nums[4] >= 39 || value_nums[4] <= -0.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组电池电压1");
                                            }));

                                        }
                                        else if (value_nums[5] >= 39 || value_nums[5] <= -0.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组电池电压1");
                                            }));

                                        }
                                        else if (value_nums[6] >= 110 || value_nums[6] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：太阳电池阵输出电流");
                                            }));

                                        }
                                        else if (value_nums[7] >= 39 || value_nums[7] <= -0.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组电池电压2");
                                            }));

                                        }
                                        else if (value_nums[8] >= 39 || value_nums[8] <= -0.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组电池电压2");
                                            }));

                                        }
                                        else if (value_nums[9] >= 22 || value_nums[9] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：MEA电压");
                                            }));

                                        }
                                        else if (value_nums[10] >= 35 || value_nums[10] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组电池温度1");
                                            }));

                                        }
                                        else if (value_nums[11] >= 35 || value_nums[11] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组电池温度2");
                                            }));

                                        }
                                        else if (value_nums[12] >= 35 || value_nums[12] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组电池温度1");
                                            }));

                                        }
                                        else if (value_nums[13] >= 35 || value_nums[13] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组电池温度2");
                                            }));

                                        }
                                        else if (value_nums[14] >= 35 || value_nums[14] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组电池温度4");
                                            }));

                                        }
                                        else if (value_nums[15] >= 35 || value_nums[15] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组电池温度3");
                                            }));

                                        }
                                        else if (value_nums[16] >= 35 || value_nums[16] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组电池温度4");
                                            }));

                                        }
                                        else if (value_nums[17] >= 55 || value_nums[17] <= -15)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：充放电模块温度");
                                            }));

                                        }
                                        else if (value_nums[18] >= 55 || value_nums[18] <= -15)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：分流模块温度");
                                            }));

                                        }
                                        else if (value_nums[19] >= 12 || value_nums[19] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A1充电电流");
                                            }));

                                        }
                                        else if (value_nums[20] >= 12 || value_nums[20] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A2充电电流");
                                            }));

                                        }
                                        else if (value_nums[21] >= 12 || value_nums[21] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B1充电电流");
                                            }));

                                        }
                                        else if (value_nums[22] >= 12 || value_nums[22] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B2充电电流");
                                            }));

                                        }
                                        else if (value_nums[23] >= 20 || value_nums[23] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A1放电电流");
                                            }));

                                        }
                                        else if (value_nums[24] >= 20 || value_nums[24] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A2放电电流");
                                            }));

                                        }
                                        else if (value_nums[25] >= 20 || value_nums[25] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A3放电电流");
                                            }));

                                        }
                                        else if (value_nums[26] >= 20 || value_nums[26] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A4放电电流");
                                            }));

                                        }
                                        else if (value_nums[27] >= 20 || value_nums[27] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B1放电电流");
                                            }));

                                        }
                                        else if (value_nums[28] >= 20 || value_nums[28] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B2放电电流");
                                            }));

                                        }
                                        else if (value_nums[29] >= 20 || value_nums[29] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B3放电电流");
                                            }));

                                        }
                                        else if (value_nums[30] >= 20 || value_nums[30] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B4放电电流");
                                            }));

                                        }
                                        else if (value_nums[31] >= 4.5 || value_nums[31] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体1电压");
                                            }));

                                        }
                                        else if (value_nums[32] >= 4.5 || value_nums[32] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体2电压");
                                            }));

                                        }
                                        else if (value_nums[33] >= 4.5 || value_nums[33] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体3电压");
                                            }));

                                        }
                                        else if (value_nums[34] >= 4.5 || value_nums[34] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体4电压");
                                            }));

                                        }
                                        else if (value_nums[35] >= 4.5 || value_nums[35] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体5电压");
                                            }));

                                        }
                                        else if (value_nums[36] >= 4.5 || value_nums[36] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体6电压");
                                            }));

                                        }
                                        else if (value_nums[37] >= 4.5 || value_nums[37] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体7电压");
                                            }));

                                        }
                                        else if (value_nums[38] >= 4.5 || value_nums[38] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体8电压");
                                            }));

                                        }
                                        else if (value_nums[39] >= 4.5 || value_nums[39] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体9电压");
                                            }));

                                        }
                                        else if (value_nums[40] >= 4.5 || value_nums[40] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体1电压");
                                            }));

                                        }
                                        else if (value_nums[41] >= 4.5 || value_nums[41] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体2电压");
                                            }));

                                        }
                                        else if (value_nums[42] >= 4.5 || value_nums[42] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体3电压");
                                            }));

                                        }
                                        else if (value_nums[43] >= 4.5 || value_nums[43] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体4电压");
                                            }));

                                        }
                                        else if (value_nums[44] >= 4.5 || value_nums[44] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体5电压");
                                            }));

                                        }
                                        else if (value_nums[45] >= 4.5 || value_nums[45] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体6电压");
                                            }));

                                        }
                                        else if (value_nums[46] >= 4.5 || value_nums[46] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体7电压");
                                            }));

                                        }
                                        else if (value_nums[47] >= 4.5 || value_nums[47] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体8电压");
                                            }));

                                        }
                                        else if (value_nums[48] >= 4.5 || value_nums[48] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体9电压");
                                            }));

                                        }
                                    }
                                }
                                //读取“读取电源42V母线电压”的数据并判断是否故障
                                else if (bianliang_flag == "42V母线电压")
                                {
                                    b = value_nums[2];
                                    a = TEST.Class1.test_dy(value_nums);//
                                    if (a >= 0.5)
                                    {
                                        label_dll.Invoke(new EventHandler(delegate { label_dll.Text = string.Format("故障"); }));
                                        abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                        label_dll.ForeColor = System.Drawing.Color.Red;
                                        if (value_nums[0] >= 28.5 || value_nums[0] <= 27.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：28V母线电压");
                                            }));

                                        }
                                        else if (value_nums[1] >= 43 || value_nums[1] <= -0.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：28V负载电流");
                                            }));

                                        }
                                        else if (value_nums[2] >= 43 || value_nums[2] <= 41)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：42V母线电压");
                                            }));

                                        }
                                        else if (value_nums[3] >= 78 || value_nums[3] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：42V负载电流");
                                            }));

                                        }
                                        else if (value_nums[4] >= 39 || value_nums[4] <= -0.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组电池电压1");
                                            }));

                                        }
                                        else if (value_nums[5] >= 39 || value_nums[5] <= -0.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组电池电压1");
                                            }));

                                        }
                                        else if (value_nums[6] >= 110 || value_nums[6] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：太阳电池阵输出电流");
                                            }));

                                        }
                                        else if (value_nums[7] >= 39 || value_nums[7] <= -0.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组电池电压2");
                                            }));

                                        }
                                        else if (value_nums[8] >= 39 || value_nums[8] <= -0.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组电池电压2");
                                            }));

                                        }
                                        else if (value_nums[9] >= 22 || value_nums[9] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：MEA电压");
                                            }));

                                        }
                                        else if (value_nums[10] >= 35 || value_nums[10] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组电池温度1");
                                            }));

                                        }
                                        else if (value_nums[11] >= 35 || value_nums[11] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组电池温度2");
                                            }));

                                        }
                                        else if (value_nums[12] >= 35 || value_nums[12] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组电池温度1");
                                            }));

                                        }
                                        else if (value_nums[13] >= 35 || value_nums[13] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组电池温度2");
                                            }));

                                        }
                                        else if (value_nums[14] >= 35 || value_nums[14] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组电池温度4");
                                            }));

                                        }
                                        else if (value_nums[15] >= 35 || value_nums[15] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组电池温度3");
                                            }));

                                        }
                                        else if (value_nums[16] >= 35 || value_nums[16] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组电池温度4");
                                            }));

                                        }
                                        else if (value_nums[17] >= 55 || value_nums[17] <= -15)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：充放电模块温度");
                                            }));

                                        }
                                        else if (value_nums[18] >= 55 || value_nums[18] <= -15)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：分流模块温度");
                                            }));

                                        }
                                        else if (value_nums[19] >= 12 || value_nums[19] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A1充电电流");
                                            }));

                                        }
                                        else if (value_nums[20] >= 12 || value_nums[20] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A2充电电流");
                                            }));

                                        }
                                        else if (value_nums[21] >= 12 || value_nums[21] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B1充电电流");
                                            }));

                                        }
                                        else if (value_nums[22] >= 12 || value_nums[22] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B2充电电流");
                                            }));

                                        }
                                        else if (value_nums[23] >= 20 || value_nums[23] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A1放电电流");
                                            }));

                                        }
                                        else if (value_nums[24] >= 20 || value_nums[24] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A2放电电流");
                                            }));

                                        }
                                        else if (value_nums[25] >= 20 || value_nums[25] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A3放电电流");
                                            }));

                                        }
                                        else if (value_nums[26] >= 20 || value_nums[26] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A4放电电流");
                                            }));

                                        }
                                        else if (value_nums[27] >= 20 || value_nums[27] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B1放电电流");
                                            }));

                                        }
                                        else if (value_nums[28] >= 20 || value_nums[28] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B2放电电流");
                                            }));

                                        }
                                        else if (value_nums[29] >= 20 || value_nums[29] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B3放电电流");
                                            }));

                                        }
                                        else if (value_nums[30] >= 20 || value_nums[30] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B4放电电流");
                                            }));

                                        }
                                        else if (value_nums[31] >= 4.5 || value_nums[31] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体1电压");
                                            }));

                                        }
                                        else if (value_nums[32] >= 4.5 || value_nums[32] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体2电压");
                                            }));

                                        }
                                        else if (value_nums[33] >= 4.5 || value_nums[33] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体3电压");
                                            }));

                                        }
                                        else if (value_nums[34] >= 4.5 || value_nums[34] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体4电压");
                                            }));

                                        }
                                        else if (value_nums[35] >= 4.5 || value_nums[35] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体5电压");
                                            }));

                                        }
                                        else if (value_nums[36] >= 4.5 || value_nums[36] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体6电压");
                                            }));

                                        }
                                        else if (value_nums[37] >= 4.5 || value_nums[37] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体7电压");
                                            }));

                                        }
                                        else if (value_nums[38] >= 4.5 || value_nums[38] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体8电压");
                                            }));

                                        }
                                        else if (value_nums[39] >= 4.5 || value_nums[39] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体9电压");
                                            }));

                                        }
                                        else if (value_nums[40] >= 4.5 || value_nums[40] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体1电压");
                                            }));

                                        }
                                        else if (value_nums[41] >= 4.5 || value_nums[41] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体2电压");
                                            }));

                                        }
                                        else if (value_nums[42] >= 4.5 || value_nums[42] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体3电压");
                                            }));

                                        }
                                        else if (value_nums[43] >= 4.5 || value_nums[43] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体4电压");
                                            }));

                                        }
                                        else if (value_nums[44] >= 4.5 || value_nums[44] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体5电压");
                                            }));

                                        }
                                        else if (value_nums[45] >= 4.5 || value_nums[45] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体6电压");
                                            }));

                                        }
                                        else if (value_nums[46] >= 4.5 || value_nums[46] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体7电压");
                                            }));

                                        }
                                        else if (value_nums[47] >= 4.5 || value_nums[47] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体8电压");
                                            }));

                                        }
                                        else if (value_nums[48] >= 4.5 || value_nums[48] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体9电压");
                                            }));

                                        }
                                    }
                                }
                                //读取“读取电源42V负载电流”的数据并判断是否故障
                                else if (bianliang_flag == "42V负载电流")
                                {
                                    b = value_nums[3];
                                    a = TEST.Class1.test_dy(value_nums);//
                                    if (a >= 0.5)
                                    {
                                        label_dll.Invoke(new EventHandler(delegate { label_dll.Text = string.Format("故障"); }));
                                        label_dll.ForeColor = System.Drawing.Color.Red;
                                        if (value_nums[0] >= 28.5 || value_nums[0] <= 27.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：28V母线电压");
                                            }));

                                        }
                                        else if (value_nums[1] >= 43 || value_nums[1] <= -0.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：28V负载电流");
                                            }));

                                        }
                                        else if (value_nums[2] >= 43 || value_nums[2] <= 41)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：42V母线电压");
                                            }));

                                        }
                                        else if (value_nums[3] >= 78 || value_nums[3] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：42V负载电流");
                                            }));

                                        }
                                        else if (value_nums[4] >= 39 || value_nums[4] <= -0.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组电池电压1");
                                            }));

                                        }
                                        else if (value_nums[5] >= 39 || value_nums[5] <= -0.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组电池电压1");
                                            }));

                                        }
                                        else if (value_nums[6] >= 110 || value_nums[6] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：太阳电池阵输出电流");
                                            }));

                                        }
                                        else if (value_nums[7] >= 39 || value_nums[7] <= -0.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组电池电压2");
                                            }));

                                        }
                                        else if (value_nums[8] >= 39 || value_nums[8] <= -0.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组电池电压2");
                                            }));

                                        }
                                        else if (value_nums[9] >= 22 || value_nums[9] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：MEA电压");
                                            }));

                                        }
                                        else if (value_nums[10] >= 35 || value_nums[10] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组电池温度1");
                                            }));

                                        }
                                        else if (value_nums[11] >= 35 || value_nums[11] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组电池温度2");
                                            }));

                                        }
                                        else if (value_nums[12] >= 35 || value_nums[12] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组电池温度1");
                                            }));

                                        }
                                        else if (value_nums[13] >= 35 || value_nums[13] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组电池温度2");
                                            }));

                                        }
                                        else if (value_nums[14] >= 35 || value_nums[14] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组电池温度4");
                                            }));

                                        }
                                        else if (value_nums[15] >= 35 || value_nums[15] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组电池温度3");
                                            }));

                                        }
                                        else if (value_nums[16] >= 35 || value_nums[16] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组电池温度4");
                                            }));

                                        }
                                        else if (value_nums[17] >= 55 || value_nums[17] <= -15)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：充放电模块温度");
                                            }));

                                        }
                                        else if (value_nums[18] >= 55 || value_nums[18] <= -15)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：分流模块温度");
                                            }));

                                        }
                                        else if (value_nums[19] >= 12 || value_nums[19] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A1充电电流");
                                            }));

                                        }
                                        else if (value_nums[20] >= 12 || value_nums[20] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A2充电电流");
                                            }));

                                        }
                                        else if (value_nums[21] >= 12 || value_nums[21] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B1充电电流");
                                            }));

                                        }
                                        else if (value_nums[22] >= 12 || value_nums[22] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B2充电电流");
                                            }));

                                        }
                                        else if (value_nums[23] >= 20 || value_nums[23] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A1放电电流");
                                            }));

                                        }
                                        else if (value_nums[24] >= 20 || value_nums[24] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A2放电电流");
                                            }));

                                        }
                                        else if (value_nums[25] >= 20 || value_nums[25] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A3放电电流");
                                            }));

                                        }
                                        else if (value_nums[26] >= 20 || value_nums[26] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A4放电电流");
                                            }));

                                        }
                                        else if (value_nums[27] >= 20 || value_nums[27] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B1放电电流");
                                            }));

                                        }
                                        else if (value_nums[28] >= 20 || value_nums[28] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B2放电电流");
                                            }));

                                        }
                                        else if (value_nums[29] >= 20 || value_nums[29] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B3放电电流");
                                            }));

                                        }
                                        else if (value_nums[30] >= 20 || value_nums[30] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B4放电电流");
                                            }));

                                        }
                                        else if (value_nums[31] >= 4.5 || value_nums[31] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体1电压");
                                            }));

                                        }
                                        else if (value_nums[32] >= 4.5 || value_nums[32] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体2电压");
                                            }));

                                        }
                                        else if (value_nums[33] >= 4.5 || value_nums[33] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体3电压");
                                            }));

                                        }
                                        else if (value_nums[34] >= 4.5 || value_nums[34] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体4电压");
                                            }));

                                        }
                                        else if (value_nums[35] >= 4.5 || value_nums[35] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体5电压");
                                            }));

                                        }
                                        else if (value_nums[36] >= 4.5 || value_nums[36] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体6电压");
                                            }));

                                        }
                                        else if (value_nums[37] >= 4.5 || value_nums[37] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体7电压");
                                            }));

                                        }
                                        else if (value_nums[38] >= 4.5 || value_nums[38] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体8电压");
                                            }));

                                        }
                                        else if (value_nums[39] >= 4.5 || value_nums[39] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体9电压");
                                            }));

                                        }
                                        else if (value_nums[40] >= 4.5 || value_nums[40] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体1电压");
                                            }));

                                        }
                                        else if (value_nums[41] >= 4.5 || value_nums[41] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体2电压");
                                            }));

                                        }
                                        else if (value_nums[42] >= 4.5 || value_nums[42] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体3电压");
                                            }));

                                        }
                                        else if (value_nums[43] >= 4.5 || value_nums[43] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体4电压");
                                            }));

                                        }
                                        else if (value_nums[44] >= 4.5 || value_nums[44] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体5电压");
                                            }));

                                        }
                                        else if (value_nums[45] >= 4.5 || value_nums[45] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体6电压");
                                            }));

                                        }
                                        else if (value_nums[46] >= 4.5 || value_nums[46] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体7电压");
                                            }));

                                        }
                                        else if (value_nums[47] >= 4.5 || value_nums[47] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体8电压");
                                            }));

                                        }
                                        else if (value_nums[48] >= 4.5 || value_nums[48] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体9电压");
                                            }));

                                        }
                                    }
                                }
                                //读取“读取电源A组电池电压1”的数据并判断是否故障
                                else if (bianliang_flag == "A组电池电压1")
                                {
                                    b = value_nums[4];
                                    a = TEST.Class1.test_dy(value_nums);//
                                    if (a >= 0.5)
                                    {
                                        label_dll.Invoke(new EventHandler(delegate { label_dll.Text = string.Format("故障"); }));
                                        label_dll.ForeColor = System.Drawing.Color.Red;
                                        if (value_nums[0] >= 28.5 || value_nums[0] <= 27.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：28V母线电压");
                                            }));

                                        }
                                        else if (value_nums[1] >= 43 || value_nums[1] <= -0.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：28V负载电流");
                                            }));

                                        }
                                        else if (value_nums[2] >= 43 || value_nums[2] <= 41)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：42V母线电压");
                                            }));

                                        }
                                        else if (value_nums[3] >= 78 || value_nums[3] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：42V负载电流");
                                            }));

                                        }
                                        else if (value_nums[4] >= 39 || value_nums[4] <= -0.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组电池电压1");
                                            }));

                                        }
                                        else if (value_nums[5] >= 39 || value_nums[5] <= -0.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组电池电压1");
                                            }));

                                        }
                                        else if (value_nums[6] >= 110 || value_nums[6] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：太阳电池阵输出电流");
                                            }));

                                        }
                                        else if (value_nums[7] >= 39 || value_nums[7] <= -0.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组电池电压2");
                                            }));

                                        }
                                        else if (value_nums[8] >= 39 || value_nums[8] <= -0.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组电池电压2");
                                            }));

                                        }
                                        else if (value_nums[9] >= 22 || value_nums[9] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：MEA电压");
                                            }));

                                        }
                                        else if (value_nums[10] >= 35 || value_nums[10] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组电池温度1");
                                            }));

                                        }
                                        else if (value_nums[11] >= 35 || value_nums[11] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组电池温度2");
                                            }));

                                        }
                                        else if (value_nums[12] >= 35 || value_nums[12] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组电池温度1");
                                            }));

                                        }
                                        else if (value_nums[13] >= 35 || value_nums[13] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组电池温度2");
                                            }));

                                        }
                                        else if (value_nums[14] >= 35 || value_nums[14] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组电池温度4");
                                            }));

                                        }
                                        else if (value_nums[15] >= 35 || value_nums[15] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组电池温度3");
                                            }));

                                        }
                                        else if (value_nums[16] >= 35 || value_nums[16] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组电池温度4");
                                            }));

                                        }
                                        else if (value_nums[17] >= 55 || value_nums[17] <= -15)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：充放电模块温度");
                                            }));

                                        }
                                        else if (value_nums[18] >= 55 || value_nums[18] <= -15)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：分流模块温度");
                                            }));

                                        }
                                        else if (value_nums[19] >= 12 || value_nums[19] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A1充电电流");
                                            }));

                                        }
                                        else if (value_nums[20] >= 12 || value_nums[20] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A2充电电流");
                                            }));

                                        }
                                        else if (value_nums[21] >= 12 || value_nums[21] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B1充电电流");
                                            }));

                                        }
                                        else if (value_nums[22] >= 12 || value_nums[22] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B2充电电流");
                                            }));

                                        }
                                        else if (value_nums[23] >= 20 || value_nums[23] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A1放电电流");
                                            }));

                                        }
                                        else if (value_nums[24] >= 20 || value_nums[24] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A2放电电流");
                                            }));

                                        }
                                        else if (value_nums[25] >= 20 || value_nums[25] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A3放电电流");
                                            }));

                                        }
                                        else if (value_nums[26] >= 20 || value_nums[26] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A4放电电流");
                                            }));

                                        }
                                        else if (value_nums[27] >= 20 || value_nums[27] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B1放电电流");
                                            }));

                                        }
                                        else if (value_nums[28] >= 20 || value_nums[28] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B2放电电流");
                                            }));

                                        }
                                        else if (value_nums[29] >= 20 || value_nums[29] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B3放电电流");
                                            }));

                                        }
                                        else if (value_nums[30] >= 20 || value_nums[30] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B4放电电流");
                                            }));

                                        }
                                        else if (value_nums[31] >= 4.5 || value_nums[31] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体1电压");
                                            }));

                                        }
                                        else if (value_nums[32] >= 4.5 || value_nums[32] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体2电压");
                                            }));

                                        }
                                        else if (value_nums[33] >= 4.5 || value_nums[33] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体3电压");
                                            }));

                                        }
                                        else if (value_nums[34] >= 4.5 || value_nums[34] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体4电压");
                                            }));

                                        }
                                        else if (value_nums[35] >= 4.5 || value_nums[35] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体5电压");
                                            }));

                                        }
                                        else if (value_nums[36] >= 4.5 || value_nums[36] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体6电压");
                                            }));

                                        }
                                        else if (value_nums[37] >= 4.5 || value_nums[37] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体7电压");
                                            }));

                                        }
                                        else if (value_nums[38] >= 4.5 || value_nums[38] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体8电压");
                                            }));

                                        }
                                        else if (value_nums[39] >= 4.5 || value_nums[39] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体9电压");
                                            }));

                                        }
                                        else if (value_nums[40] >= 4.5 || value_nums[40] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体1电压");
                                            }));

                                        }
                                        else if (value_nums[41] >= 4.5 || value_nums[41] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体2电压");
                                            }));

                                        }
                                        else if (value_nums[42] >= 4.5 || value_nums[42] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体3电压");
                                            }));

                                        }
                                        else if (value_nums[43] >= 4.5 || value_nums[43] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体4电压");
                                            }));

                                        }
                                        else if (value_nums[44] >= 4.5 || value_nums[44] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体5电压");
                                            }));

                                        }
                                        else if (value_nums[45] >= 4.5 || value_nums[45] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体6电压");
                                            }));

                                        }
                                        else if (value_nums[46] >= 4.5 || value_nums[46] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体7电压");
                                            }));

                                        }
                                        else if (value_nums[47] >= 4.5 || value_nums[47] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体8电压");
                                            }));

                                        }
                                        else if (value_nums[48] >= 4.5 || value_nums[48] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体9电压");
                                            }));

                                        }
                                    }
                                }
                                //读取“读取电源B组电池电压1”的数据并判断是否故障
                                else if (bianliang_flag == "B组电池电压1")
                                {
                                    b = value_nums[5];
                                    a = TEST.Class1.test_dy(value_nums);//
                                    if (a >= 0.5)
                                    {
                                        label_dll.Invoke(new EventHandler(delegate { label_dll.Text = string.Format("故障"); }));
                                        label_dll.ForeColor = System.Drawing.Color.Red;
                                        if (value_nums[0] >= 28.5 || value_nums[0] <= 27.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：28V母线电压");
                                            }));

                                        }
                                        else if (value_nums[1] >= 43 || value_nums[1] <= -0.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：28V负载电流");
                                            }));

                                        }
                                        else if (value_nums[2] >= 43 || value_nums[2] <= 41)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：42V母线电压");
                                            }));

                                        }
                                        else if (value_nums[3] >= 78 || value_nums[3] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：42V负载电流");
                                            }));

                                        }
                                        else if (value_nums[4] >= 39 || value_nums[4] <= -0.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组电池电压1");
                                            }));

                                        }
                                        else if (value_nums[5] >= 39 || value_nums[5] <= -0.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组电池电压1");
                                            }));

                                        }
                                        else if (value_nums[6] >= 110 || value_nums[6] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：太阳电池阵输出电流");
                                            }));

                                        }
                                        else if (value_nums[7] >= 39 || value_nums[7] <= -0.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组电池电压2");
                                            }));

                                        }
                                        else if (value_nums[8] >= 39 || value_nums[8] <= -0.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组电池电压2");
                                            }));

                                        }
                                        else if (value_nums[9] >= 22 || value_nums[9] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：MEA电压");
                                            }));

                                        }
                                        else if (value_nums[10] >= 35 || value_nums[10] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组电池温度1");
                                            }));

                                        }
                                        else if (value_nums[11] >= 35 || value_nums[11] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组电池温度2");
                                            }));

                                        }
                                        else if (value_nums[12] >= 35 || value_nums[12] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组电池温度1");
                                            }));

                                        }
                                        else if (value_nums[13] >= 35 || value_nums[13] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组电池温度2");
                                            }));

                                        }
                                        else if (value_nums[14] >= 35 || value_nums[14] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组电池温度4");
                                            }));

                                        }
                                        else if (value_nums[15] >= 35 || value_nums[15] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组电池温度3");
                                            }));

                                        }
                                        else if (value_nums[16] >= 35 || value_nums[16] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组电池温度4");
                                            }));

                                        }
                                        else if (value_nums[17] >= 55 || value_nums[17] <= -15)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：充放电模块温度");
                                            }));

                                        }
                                        else if (value_nums[18] >= 55 || value_nums[18] <= -15)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：分流模块温度");
                                            }));

                                        }
                                        else if (value_nums[19] >= 12 || value_nums[19] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A1充电电流");
                                            }));

                                        }
                                        else if (value_nums[20] >= 12 || value_nums[20] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A2充电电流");
                                            }));

                                        }
                                        else if (value_nums[21] >= 12 || value_nums[21] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B1充电电流");
                                            }));

                                        }
                                        else if (value_nums[22] >= 12 || value_nums[22] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B2充电电流");
                                            }));

                                        }
                                        else if (value_nums[23] >= 20 || value_nums[23] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A1放电电流");
                                            }));

                                        }
                                        else if (value_nums[24] >= 20 || value_nums[24] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A2放电电流");
                                            }));

                                        }
                                        else if (value_nums[25] >= 20 || value_nums[25] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A3放电电流");
                                            }));

                                        }
                                        else if (value_nums[26] >= 20 || value_nums[26] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A4放电电流");
                                            }));

                                        }
                                        else if (value_nums[27] >= 20 || value_nums[27] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B1放电电流");
                                            }));

                                        }
                                        else if (value_nums[28] >= 20 || value_nums[28] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B2放电电流");
                                            }));

                                        }
                                        else if (value_nums[29] >= 20 || value_nums[29] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B3放电电流");
                                            }));

                                        }
                                        else if (value_nums[30] >= 20 || value_nums[30] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B4放电电流");
                                            }));

                                        }
                                        else if (value_nums[31] >= 4.5 || value_nums[31] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体1电压");
                                            }));

                                        }
                                        else if (value_nums[32] >= 4.5 || value_nums[32] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体2电压");
                                            }));

                                        }
                                        else if (value_nums[33] >= 4.5 || value_nums[33] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体3电压");
                                            }));

                                        }
                                        else if (value_nums[34] >= 4.5 || value_nums[34] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体4电压");
                                            }));

                                        }
                                        else if (value_nums[35] >= 4.5 || value_nums[35] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体5电压");
                                            }));

                                        }
                                        else if (value_nums[36] >= 4.5 || value_nums[36] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体6电压");
                                            }));

                                        }
                                        else if (value_nums[37] >= 4.5 || value_nums[37] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体7电压");
                                            }));

                                        }
                                        else if (value_nums[38] >= 4.5 || value_nums[38] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体8电压");
                                            }));

                                        }
                                        else if (value_nums[39] >= 4.5 || value_nums[39] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体9电压");
                                            }));

                                        }
                                        else if (value_nums[40] >= 4.5 || value_nums[40] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体1电压");
                                            }));

                                        }
                                        else if (value_nums[41] >= 4.5 || value_nums[41] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体2电压");
                                            }));

                                        }
                                        else if (value_nums[42] >= 4.5 || value_nums[42] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体3电压");
                                            }));

                                        }
                                        else if (value_nums[43] >= 4.5 || value_nums[43] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体4电压");
                                            }));

                                        }
                                        else if (value_nums[44] >= 4.5 || value_nums[44] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体5电压");
                                            }));

                                        }
                                        else if (value_nums[45] >= 4.5 || value_nums[45] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体6电压");
                                            }));

                                        }
                                        else if (value_nums[46] >= 4.5 || value_nums[46] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体7电压");
                                            }));

                                        }
                                        else if (value_nums[47] >= 4.5 || value_nums[47] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体8电压");
                                            }));

                                        }
                                        else if (value_nums[48] >= 4.5 || value_nums[48] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体9电压");
                                            }));

                                        }
                                    }
                                }
                                #endregion
                                if (i % (count_txt_readline / count_config_chartpoint) == 0)
                                {
                                    dt_pro.Rows.Add(dy_shijian_hengzuobiao, a);
                                    dt_data.Rows.Add(dy_shijian_hengzuobiao, b);
                                }
                            }
                            else if (ST_flag == 1)
                            {
                                //#############################################################
                                //读取“读取辐射计东西总电流”的数据并判断是否故障
                                #region
                                if (bianliang_flag == "28V母线电压")
                                {
                                    b = value_nums[0];
                                    a = TEST.Class1.test_dy(value_nums);//
                                    if (a >= 0.5)
                                    {
                                        label_dll.Invoke(new EventHandler(delegate { label_dll.Text = string.Format("故障"); }));
                                        label_dll.ForeColor = System.Drawing.Color.Red;
                                        if (value_nums[0] >= 28.5 || value_nums[0] <= 27.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：28V母线电压");
                                            }));

                                        }
                                        else if (value_nums[1] >= 43 || value_nums[1] <= -0.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：28V负载电流");
                                            }));

                                        }
                                        else if (value_nums[2] >= 43 || value_nums[2] <= 41)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：42V母线电压");
                                            }));

                                        }
                                        else if (value_nums[3] >= 78 || value_nums[3] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：42V负载电流");
                                            }));

                                        }
                                        else if (value_nums[4] >= 39 || value_nums[4] <= -0.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组电池电压1");
                                            }));

                                        }
                                        else if (value_nums[5] >= 39 || value_nums[5] <= -0.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组电池电压1");
                                            }));

                                        }
                                        else if (value_nums[6] >= 110 || value_nums[6] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：太阳电池阵输出电流");
                                            }));

                                        }
                                        else if (value_nums[7] >= 39 || value_nums[7] <= -0.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组电池电压2");
                                            }));

                                        }
                                        else if (value_nums[8] >= 39 || value_nums[8] <= -0.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组电池电压2");
                                            }));

                                        }
                                        else if (value_nums[9] >= 22 || value_nums[9] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：MEA电压");
                                            }));

                                        }
                                        else if (value_nums[10] >= 35 || value_nums[10] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组电池温度1");
                                            }));

                                        }
                                        else if (value_nums[11] >= 35 || value_nums[11] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组电池温度2");
                                            }));

                                        }
                                        else if (value_nums[12] >= 35 || value_nums[12] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组电池温度1");
                                            }));

                                        }
                                        else if (value_nums[13] >= 35 || value_nums[13] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组电池温度2");
                                            }));

                                        }
                                        else if (value_nums[14] >= 35 || value_nums[14] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组电池温度4");
                                            }));

                                        }
                                        else if (value_nums[15] >= 35 || value_nums[15] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组电池温度3");
                                            }));

                                        }
                                        else if (value_nums[16] >= 35 || value_nums[16] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组电池温度4");
                                            }));

                                        }
                                        else if (value_nums[17] >= 55 || value_nums[17] <= -15)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：充放电模块温度");
                                            }));

                                        }
                                        else if (value_nums[18] >= 55 || value_nums[18] <= -15)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：分流模块温度");
                                            }));

                                        }
                                        else if (value_nums[19] >= 12 || value_nums[19] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A1充电电流");
                                            }));

                                        }
                                        else if (value_nums[20] >= 12 || value_nums[20] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A2充电电流");
                                            }));

                                        }
                                        else if (value_nums[21] >= 12 || value_nums[21] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B1充电电流");
                                            }));

                                        }
                                        else if (value_nums[22] >= 12 || value_nums[22] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B2充电电流");
                                            }));

                                        }
                                        else if (value_nums[23] >= 20 || value_nums[23] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A1放电电流");
                                            }));

                                        }
                                        else if (value_nums[24] >= 20 || value_nums[24] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A2放电电流");
                                            }));

                                        }
                                        else if (value_nums[25] >= 20 || value_nums[25] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A3放电电流");
                                            }));

                                        }
                                        else if (value_nums[26] >= 20 || value_nums[26] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A4放电电流");
                                            }));

                                        }
                                        else if (value_nums[27] >= 20 || value_nums[27] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B1放电电流");
                                            }));

                                        }
                                        else if (value_nums[28] >= 20 || value_nums[28] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B2放电电流");
                                            }));

                                        }
                                        else if (value_nums[29] >= 20 || value_nums[29] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B3放电电流");
                                            }));

                                        }
                                        else if (value_nums[30] >= 20 || value_nums[30] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B4放电电流");
                                            }));

                                        }
                                        else if (value_nums[31] >= 4.5 || value_nums[31] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体1电压");
                                            }));

                                        }
                                        else if (value_nums[32] >= 4.5 || value_nums[32] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体2电压");
                                            }));

                                        }
                                        else if (value_nums[33] >= 4.5 || value_nums[33] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体3电压");
                                            }));

                                        }
                                        else if (value_nums[34] >= 4.5 || value_nums[34] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体4电压");
                                            }));

                                        }
                                        else if (value_nums[35] >= 4.5 || value_nums[35] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体5电压");
                                            }));

                                        }
                                        else if (value_nums[36] >= 4.5 || value_nums[36] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体6电压");
                                            }));

                                        }
                                        else if (value_nums[37] >= 4.5 || value_nums[37] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体7电压");
                                            }));

                                        }
                                        else if (value_nums[38] >= 4.5 || value_nums[38] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体8电压");
                                            }));

                                        }
                                        else if (value_nums[39] >= 4.5 || value_nums[39] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体9电压");
                                            }));

                                        }
                                        else if (value_nums[40] >= 4.5 || value_nums[40] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体1电压");
                                            }));

                                        }
                                        else if (value_nums[41] >= 4.5 || value_nums[41] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体2电压");
                                            }));

                                        }
                                        else if (value_nums[42] >= 4.5 || value_nums[42] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体3电压");
                                            }));

                                        }
                                        else if (value_nums[43] >= 4.5 || value_nums[43] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体4电压");
                                            }));

                                        }
                                        else if (value_nums[44] >= 4.5 || value_nums[44] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体5电压");
                                            }));

                                        }
                                        else if (value_nums[45] >= 4.5 || value_nums[45] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体6电压");
                                            }));

                                        }
                                        else if (value_nums[46] >= 4.5 || value_nums[46] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体7电压");
                                            }));

                                        }
                                        else if (value_nums[47] >= 4.5 || value_nums[47] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体8电压");
                                            }));

                                        }
                                        else if (value_nums[48] >= 4.5 || value_nums[48] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体9电压");
                                            }));

                                        }
                                    }
                                }
                                //读取“读取电源28V负载电流”的数据并判断是否故障
                                else if (bianliang_flag == "28V负载电流")
                                {
                                    b = value_nums[1];
                                    a = TEST.Class1.test_dy(value_nums);//
                                    if (a >= 0.5)
                                    {
                                        label_dll.Invoke(new EventHandler(delegate { label_dll.Text = string.Format("故障"); }));
                                        label_dll.ForeColor = System.Drawing.Color.Red;
                                        if (value_nums[0] >= 28.5 || value_nums[0] <= 27.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：28V母线电压");
                                            }));

                                        }
                                        else if (value_nums[1] >= 43 || value_nums[1] <= -0.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：28V负载电流");
                                            }));

                                        }
                                        else if (value_nums[2] >= 43 || value_nums[2] <= 41)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：42V母线电压");
                                            }));

                                        }
                                        else if (value_nums[3] >= 78 || value_nums[3] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：42V负载电流");
                                            }));

                                        }
                                        else if (value_nums[4] >= 39 || value_nums[4] <= -0.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组电池电压1");
                                            }));

                                        }
                                        else if (value_nums[5] >= 39 || value_nums[5] <= -0.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组电池电压1");
                                            }));

                                        }
                                        else if (value_nums[6] >= 110 || value_nums[6] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：太阳电池阵输出电流");
                                            }));

                                        }
                                        else if (value_nums[7] >= 39 || value_nums[7] <= -0.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组电池电压2");
                                            }));

                                        }
                                        else if (value_nums[8] >= 39 || value_nums[8] <= -0.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组电池电压2");
                                            }));

                                        }
                                        else if (value_nums[9] >= 22 || value_nums[9] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：MEA电压");
                                            }));

                                        }
                                        else if (value_nums[10] >= 35 || value_nums[10] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组电池温度1");
                                            }));

                                        }
                                        else if (value_nums[11] >= 35 || value_nums[11] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组电池温度2");
                                            }));

                                        }
                                        else if (value_nums[12] >= 35 || value_nums[12] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组电池温度1");
                                            }));

                                        }
                                        else if (value_nums[13] >= 35 || value_nums[13] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组电池温度2");
                                            }));

                                        }
                                        else if (value_nums[14] >= 35 || value_nums[14] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组电池温度4");
                                            }));

                                        }
                                        else if (value_nums[15] >= 35 || value_nums[15] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组电池温度3");
                                            }));

                                        }
                                        else if (value_nums[16] >= 35 || value_nums[16] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组电池温度4");
                                            }));

                                        }
                                        else if (value_nums[17] >= 55 || value_nums[17] <= -15)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：充放电模块温度");
                                            }));

                                        }
                                        else if (value_nums[18] >= 55 || value_nums[18] <= -15)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：分流模块温度");
                                            }));

                                        }
                                        else if (value_nums[19] >= 12 || value_nums[19] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A1充电电流");
                                            }));

                                        }
                                        else if (value_nums[20] >= 12 || value_nums[20] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A2充电电流");
                                            }));

                                        }
                                        else if (value_nums[21] >= 12 || value_nums[21] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B1充电电流");
                                            }));

                                        }
                                        else if (value_nums[22] >= 12 || value_nums[22] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B2充电电流");
                                            }));

                                        }
                                        else if (value_nums[23] >= 20 || value_nums[23] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A1放电电流");
                                            }));

                                        }
                                        else if (value_nums[24] >= 20 || value_nums[24] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A2放电电流");
                                            }));

                                        }
                                        else if (value_nums[25] >= 20 || value_nums[25] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A3放电电流");
                                            }));

                                        }
                                        else if (value_nums[26] >= 20 || value_nums[26] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A4放电电流");
                                            }));

                                        }
                                        else if (value_nums[27] >= 20 || value_nums[27] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B1放电电流");
                                            }));

                                        }
                                        else if (value_nums[28] >= 20 || value_nums[28] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B2放电电流");
                                            }));

                                        }
                                        else if (value_nums[29] >= 20 || value_nums[29] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B3放电电流");
                                            }));

                                        }
                                        else if (value_nums[30] >= 20 || value_nums[30] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B4放电电流");
                                            }));

                                        }
                                        else if (value_nums[31] >= 4.5 || value_nums[31] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体1电压");
                                            }));

                                        }
                                        else if (value_nums[32] >= 4.5 || value_nums[32] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体2电压");
                                            }));

                                        }
                                        else if (value_nums[33] >= 4.5 || value_nums[33] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体3电压");
                                            }));

                                        }
                                        else if (value_nums[34] >= 4.5 || value_nums[34] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体4电压");
                                            }));

                                        }
                                        else if (value_nums[35] >= 4.5 || value_nums[35] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体5电压");
                                            }));

                                        }
                                        else if (value_nums[36] >= 4.5 || value_nums[36] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体6电压");
                                            }));

                                        }
                                        else if (value_nums[37] >= 4.5 || value_nums[37] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体7电压");
                                            }));

                                        }
                                        else if (value_nums[38] >= 4.5 || value_nums[38] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体8电压");
                                            }));

                                        }
                                        else if (value_nums[39] >= 4.5 || value_nums[39] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体9电压");
                                            }));

                                        }
                                        else if (value_nums[40] >= 4.5 || value_nums[40] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体1电压");
                                            }));

                                        }
                                        else if (value_nums[41] >= 4.5 || value_nums[41] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体2电压");
                                            }));

                                        }
                                        else if (value_nums[42] >= 4.5 || value_nums[42] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体3电压");
                                            }));

                                        }
                                        else if (value_nums[43] >= 4.5 || value_nums[43] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体4电压");
                                            }));

                                        }
                                        else if (value_nums[44] >= 4.5 || value_nums[44] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体5电压");
                                            }));

                                        }
                                        else if (value_nums[45] >= 4.5 || value_nums[45] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体6电压");
                                            }));

                                        }
                                        else if (value_nums[46] >= 4.5 || value_nums[46] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体7电压");
                                            }));

                                        }
                                        else if (value_nums[47] >= 4.5 || value_nums[47] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体8电压");
                                            }));

                                        }
                                        else if (value_nums[48] >= 4.5 || value_nums[48] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体9电压");
                                            }));

                                        }
                                    }
                                }
                                //读取“读取电源42V母线电压”的数据并判断是否故障
                                else if (bianliang_flag == "42V母线电压")
                                {
                                    b = value_nums[2];
                                    a = TEST.Class1.test_dy(value_nums);//
                                    if (a >= 0.8)
                                    {
                                        label_dll.Invoke(new EventHandler(delegate { label_dll.Text = string.Format("故障"); }));
                                        label_dll.ForeColor = System.Drawing.Color.Red;
                                        if (value_nums[0] >= 28.5 || value_nums[0] <= 27.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：28V母线电压");
                                            }));

                                        }
                                        else if (value_nums[1] >= 43 || value_nums[1] <= -0.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：28V负载电流");
                                            }));

                                        }
                                        else if (value_nums[2] >= 43 || value_nums[2] <= 41)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：42V母线电压");
                                            }));

                                        }
                                        else if (value_nums[3] >= 78 || value_nums[3] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：42V负载电流");
                                            }));

                                        }
                                        else if (value_nums[4] >= 39 || value_nums[4] <= -0.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组电池电压1");
                                            }));

                                        }
                                        else if (value_nums[5] >= 39 || value_nums[5] <= -0.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组电池电压1");
                                            }));

                                        }
                                        else if (value_nums[6] >= 110 || value_nums[6] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：太阳电池阵输出电流");
                                            }));

                                        }
                                        else if (value_nums[7] >= 39 || value_nums[7] <= -0.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组电池电压2");
                                            }));

                                        }
                                        else if (value_nums[8] >= 39 || value_nums[8] <= -0.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组电池电压2");
                                            }));

                                        }
                                        else if (value_nums[9] >= 22 || value_nums[9] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：MEA电压");
                                            }));

                                        }
                                        else if (value_nums[10] >= 35 || value_nums[10] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组电池温度1");
                                            }));

                                        }
                                        else if (value_nums[11] >= 35 || value_nums[11] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组电池温度2");
                                            }));

                                        }
                                        else if (value_nums[12] >= 35 || value_nums[12] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组电池温度1");
                                            }));

                                        }
                                        else if (value_nums[13] >= 35 || value_nums[13] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组电池温度2");
                                            }));

                                        }
                                        else if (value_nums[14] >= 35 || value_nums[14] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组电池温度4");
                                            }));

                                        }
                                        else if (value_nums[15] >= 35 || value_nums[15] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组电池温度3");
                                            }));

                                        }
                                        else if (value_nums[16] >= 35 || value_nums[16] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组电池温度4");
                                            }));

                                        }
                                        else if (value_nums[17] >= 55 || value_nums[17] <= -15)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：充放电模块温度");
                                            }));

                                        }
                                        else if (value_nums[18] >= 55 || value_nums[18] <= -15)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：分流模块温度");
                                            }));

                                        }
                                        else if (value_nums[19] >= 12 || value_nums[19] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A1充电电流");
                                            }));

                                        }
                                        else if (value_nums[20] >= 12 || value_nums[20] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A2充电电流");
                                            }));

                                        }
                                        else if (value_nums[21] >= 12 || value_nums[21] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B1充电电流");
                                            }));

                                        }
                                        else if (value_nums[22] >= 12 || value_nums[22] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B2充电电流");
                                            }));

                                        }
                                        else if (value_nums[23] >= 20 || value_nums[23] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A1放电电流");
                                            }));

                                        }
                                        else if (value_nums[24] >= 20 || value_nums[24] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A2放电电流");
                                            }));

                                        }
                                        else if (value_nums[25] >= 20 || value_nums[25] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A3放电电流");
                                            }));

                                        }
                                        else if (value_nums[26] >= 20 || value_nums[26] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A4放电电流");
                                            }));

                                        }
                                        else if (value_nums[27] >= 20 || value_nums[27] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B1放电电流");
                                            }));

                                        }
                                        else if (value_nums[28] >= 20 || value_nums[28] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B2放电电流");
                                            }));

                                        }
                                        else if (value_nums[29] >= 20 || value_nums[29] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B3放电电流");
                                            }));

                                        }
                                        else if (value_nums[30] >= 20 || value_nums[30] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B4放电电流");
                                            }));

                                        }
                                        else if (value_nums[31] >= 4.5 || value_nums[31] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体1电压");
                                            }));

                                        }
                                        else if (value_nums[32] >= 4.5 || value_nums[32] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体2电压");
                                            }));

                                        }
                                        else if (value_nums[33] >= 4.5 || value_nums[33] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体3电压");
                                            }));

                                        }
                                        else if (value_nums[34] >= 4.5 || value_nums[34] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体4电压");
                                            }));

                                        }
                                        else if (value_nums[35] >= 4.5 || value_nums[35] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体5电压");
                                            }));

                                        }
                                        else if (value_nums[36] >= 4.5 || value_nums[36] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体6电压");
                                            }));

                                        }
                                        else if (value_nums[37] >= 4.5 || value_nums[37] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体7电压");
                                            }));

                                        }
                                        else if (value_nums[38] >= 4.5 || value_nums[38] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体8电压");
                                            }));

                                        }
                                        else if (value_nums[39] >= 4.5 || value_nums[39] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体9电压");
                                            }));

                                        }
                                        else if (value_nums[40] >= 4.5 || value_nums[40] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体1电压");
                                            }));

                                        }
                                        else if (value_nums[41] >= 4.5 || value_nums[41] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体2电压");
                                            }));

                                        }
                                        else if (value_nums[42] >= 4.5 || value_nums[42] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体3电压");
                                            }));

                                        }
                                        else if (value_nums[43] >= 4.5 || value_nums[43] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体4电压");
                                            }));

                                        }
                                        else if (value_nums[44] >= 4.5 || value_nums[44] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体5电压");
                                            }));

                                        }
                                        else if (value_nums[45] >= 4.5 || value_nums[45] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体6电压");
                                            }));

                                        }
                                        else if (value_nums[46] >= 4.5 || value_nums[46] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体7电压");
                                            }));

                                        }
                                        else if (value_nums[47] >= 4.5 || value_nums[47] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体8电压");
                                            }));

                                        }
                                        else if (value_nums[48] >= 4.5 || value_nums[48] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体9电压");
                                            }));

                                        }
                                    }
                                }
                                //读取“读取电源42V负载电流”的数据并判断是否故障
                                else if (bianliang_flag == "42V负载电流")
                                {
                                    b = value_nums[3];
                                    a = TEST.Class1.test_dy(value_nums);//
                                    if (a >= 0.5)
                                    {
                                        label_dll.Invoke(new EventHandler(delegate { label_dll.Text = string.Format("故障"); }));
                                        label_dll.ForeColor = System.Drawing.Color.Red;
                                        if (value_nums[0] >= 28.5 || value_nums[0] <= 27.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：28V母线电压");
                                            }));

                                        }
                                        else if (value_nums[1] >= 43 || value_nums[1] <= -0.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：28V负载电流");
                                            }));

                                        }
                                        else if (value_nums[2] >= 43 || value_nums[2] <= 41)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：42V母线电压");
                                            }));

                                        }
                                        else if (value_nums[3] >= 78 || value_nums[3] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：42V负载电流");
                                            }));

                                        }
                                        else if (value_nums[4] >= 39 || value_nums[4] <= -0.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组电池电压1");
                                            }));

                                        }
                                        else if (value_nums[5] >= 39 || value_nums[5] <= -0.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组电池电压1");
                                            }));

                                        }
                                        else if (value_nums[6] >= 110 || value_nums[6] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：太阳电池阵输出电流");
                                            }));

                                        }
                                        else if (value_nums[7] >= 39 || value_nums[7] <= -0.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组电池电压2");
                                            }));

                                        }
                                        else if (value_nums[8] >= 39 || value_nums[8] <= -0.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组电池电压2");
                                            }));

                                        }
                                        else if (value_nums[9] >= 22 || value_nums[9] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：MEA电压");
                                            }));

                                        }
                                        else if (value_nums[10] >= 35 || value_nums[10] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组电池温度1");
                                            }));

                                        }
                                        else if (value_nums[11] >= 35 || value_nums[11] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组电池温度2");
                                            }));

                                        }
                                        else if (value_nums[12] >= 35 || value_nums[12] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组电池温度1");
                                            }));

                                        }
                                        else if (value_nums[13] >= 35 || value_nums[13] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组电池温度2");
                                            }));

                                        }
                                        else if (value_nums[14] >= 35 || value_nums[14] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组电池温度4");
                                            }));

                                        }
                                        else if (value_nums[15] >= 35 || value_nums[15] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组电池温度3");
                                            }));

                                        }
                                        else if (value_nums[16] >= 35 || value_nums[16] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组电池温度4");
                                            }));

                                        }
                                        else if (value_nums[17] >= 55 || value_nums[17] <= -15)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：充放电模块温度");
                                            }));

                                        }
                                        else if (value_nums[18] >= 55 || value_nums[18] <= -15)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：分流模块温度");
                                            }));

                                        }
                                        else if (value_nums[19] >= 12 || value_nums[19] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A1充电电流");
                                            }));

                                        }
                                        else if (value_nums[20] >= 12 || value_nums[20] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A2充电电流");
                                            }));

                                        }
                                        else if (value_nums[21] >= 12 || value_nums[21] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B1充电电流");
                                            }));

                                        }
                                        else if (value_nums[22] >= 12 || value_nums[22] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B2充电电流");
                                            }));

                                        }
                                        else if (value_nums[23] >= 20 || value_nums[23] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A1放电电流");
                                            }));

                                        }
                                        else if (value_nums[24] >= 20 || value_nums[24] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A2放电电流");
                                            }));

                                        }
                                        else if (value_nums[25] >= 20 || value_nums[25] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A3放电电流");
                                            }));

                                        }
                                        else if (value_nums[26] >= 20 || value_nums[26] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A4放电电流");
                                            }));

                                        }
                                        else if (value_nums[27] >= 20 || value_nums[27] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B1放电电流");
                                            }));

                                        }
                                        else if (value_nums[28] >= 20 || value_nums[28] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B2放电电流");
                                            }));

                                        }
                                        else if (value_nums[29] >= 20 || value_nums[29] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B3放电电流");
                                            }));

                                        }
                                        else if (value_nums[30] >= 20 || value_nums[30] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B4放电电流");
                                            }));

                                        }
                                        else if (value_nums[31] >= 4.5 || value_nums[31] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体1电压");
                                            }));

                                        }
                                        else if (value_nums[32] >= 4.5 || value_nums[32] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体2电压");
                                            }));

                                        }
                                        else if (value_nums[33] >= 4.5 || value_nums[33] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体3电压");
                                            }));

                                        }
                                        else if (value_nums[34] >= 4.5 || value_nums[34] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体4电压");
                                            }));

                                        }
                                        else if (value_nums[35] >= 4.5 || value_nums[35] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体5电压");
                                            }));

                                        }
                                        else if (value_nums[36] >= 4.5 || value_nums[36] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体6电压");
                                            }));

                                        }
                                        else if (value_nums[37] >= 4.5 || value_nums[37] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体7电压");
                                            }));

                                        }
                                        else if (value_nums[38] >= 4.5 || value_nums[38] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体8电压");
                                            }));

                                        }
                                        else if (value_nums[39] >= 4.5 || value_nums[39] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体9电压");
                                            }));

                                        }
                                        else if (value_nums[40] >= 4.5 || value_nums[40] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体1电压");
                                            }));

                                        }
                                        else if (value_nums[41] >= 4.5 || value_nums[41] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体2电压");
                                            }));

                                        }
                                        else if (value_nums[42] >= 4.5 || value_nums[42] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体3电压");
                                            }));

                                        }
                                        else if (value_nums[43] >= 4.5 || value_nums[43] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体4电压");
                                            }));

                                        }
                                        else if (value_nums[44] >= 4.5 || value_nums[44] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体5电压");
                                            }));

                                        }
                                        else if (value_nums[45] >= 4.5 || value_nums[45] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体6电压");
                                            }));

                                        }
                                        else if (value_nums[46] >= 4.5 || value_nums[46] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体7电压");
                                            }));

                                        }
                                        else if (value_nums[47] >= 4.5 || value_nums[47] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体8电压");
                                            }));

                                        }
                                        else if (value_nums[48] >= 4.5 || value_nums[48] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体9电压");
                                            }));

                                        }
                                    }
                                }
                                //读取“读取电源A组电池电压1”的数据并判断是否故障
                                else if (bianliang_flag == "A组电池电压1")
                                {
                                    b = value_nums[4];
                                    a = TEST.Class1.test_dy(value_nums);//
                                    if (a >= 0.5)
                                    {
                                        label_dll.Invoke(new EventHandler(delegate { label_dll.Text = string.Format("故障"); }));
                                        label_dll.ForeColor = System.Drawing.Color.Red;
                                        if (value_nums[0] >= 28.5 || value_nums[0] <= 27.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：28V母线电压");
                                            }));

                                        }
                                        else if (value_nums[1] >= 43 || value_nums[1] <= -0.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：28V负载电流");
                                            }));

                                        }
                                        else if (value_nums[2] >= 43 || value_nums[2] <= 41)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：42V母线电压");
                                            }));

                                        }
                                        else if (value_nums[3] >= 78 || value_nums[3] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：42V负载电流");
                                            }));

                                        }
                                        else if (value_nums[4] >= 39 || value_nums[4] <= -0.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组电池电压1");
                                            }));

                                        }
                                        else if (value_nums[5] >= 39 || value_nums[5] <= -0.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组电池电压1");
                                            }));

                                        }
                                        else if (value_nums[6] >= 110 || value_nums[6] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：太阳电池阵输出电流");
                                            }));

                                        }
                                        else if (value_nums[7] >= 39 || value_nums[7] <= -0.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组电池电压2");
                                            }));

                                        }
                                        else if (value_nums[8] >= 39 || value_nums[8] <= -0.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组电池电压2");
                                            }));

                                        }
                                        else if (value_nums[9] >= 22 || value_nums[9] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：MEA电压");
                                            }));

                                        }
                                        else if (value_nums[10] >= 35 || value_nums[10] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组电池温度1");
                                            }));

                                        }
                                        else if (value_nums[11] >= 35 || value_nums[11] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组电池温度2");
                                            }));

                                        }
                                        else if (value_nums[12] >= 35 || value_nums[12] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组电池温度1");
                                            }));

                                        }
                                        else if (value_nums[13] >= 35 || value_nums[13] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组电池温度2");
                                            }));

                                        }
                                        else if (value_nums[14] >= 35 || value_nums[14] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组电池温度4");
                                            }));

                                        }
                                        else if (value_nums[15] >= 35 || value_nums[15] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组电池温度3");
                                            }));

                                        }
                                        else if (value_nums[16] >= 35 || value_nums[16] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组电池温度4");
                                            }));

                                        }
                                        else if (value_nums[17] >= 55 || value_nums[17] <= -15)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：充放电模块温度");
                                            }));

                                        }
                                        else if (value_nums[18] >= 55 || value_nums[18] <= -15)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：分流模块温度");
                                            }));

                                        }
                                        else if (value_nums[19] >= 12 || value_nums[19] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A1充电电流");
                                            }));

                                        }
                                        else if (value_nums[20] >= 12 || value_nums[20] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A2充电电流");
                                            }));

                                        }
                                        else if (value_nums[21] >= 12 || value_nums[21] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B1充电电流");
                                            }));

                                        }
                                        else if (value_nums[22] >= 12 || value_nums[22] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B2充电电流");
                                            }));

                                        }
                                        else if (value_nums[23] >= 20 || value_nums[23] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A1放电电流");
                                            }));

                                        }
                                        else if (value_nums[24] >= 20 || value_nums[24] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A2放电电流");
                                            }));

                                        }
                                        else if (value_nums[25] >= 20 || value_nums[25] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A3放电电流");
                                            }));

                                        }
                                        else if (value_nums[26] >= 20 || value_nums[26] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A4放电电流");
                                            }));

                                        }
                                        else if (value_nums[27] >= 20 || value_nums[27] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B1放电电流");
                                            }));

                                        }
                                        else if (value_nums[28] >= 20 || value_nums[28] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B2放电电流");
                                            }));

                                        }
                                        else if (value_nums[29] >= 20 || value_nums[29] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B3放电电流");
                                            }));

                                        }
                                        else if (value_nums[30] >= 20 || value_nums[30] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B4放电电流");
                                            }));

                                        }
                                        else if (value_nums[31] >= 4.5 || value_nums[31] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体1电压");
                                            }));

                                        }
                                        else if (value_nums[32] >= 4.5 || value_nums[32] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体2电压");
                                            }));

                                        }
                                        else if (value_nums[33] >= 4.5 || value_nums[33] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体3电压");
                                            }));

                                        }
                                        else if (value_nums[34] >= 4.5 || value_nums[34] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体4电压");
                                            }));

                                        }
                                        else if (value_nums[35] >= 4.5 || value_nums[35] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体5电压");
                                            }));

                                        }
                                        else if (value_nums[36] >= 4.5 || value_nums[36] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体6电压");
                                            }));

                                        }
                                        else if (value_nums[37] >= 4.5 || value_nums[37] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体7电压");
                                            }));

                                        }
                                        else if (value_nums[38] >= 4.5 || value_nums[38] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体8电压");
                                            }));

                                        }
                                        else if (value_nums[39] >= 4.5 || value_nums[39] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体9电压");
                                            }));

                                        }
                                        else if (value_nums[40] >= 4.5 || value_nums[40] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体1电压");
                                            }));

                                        }
                                        else if (value_nums[41] >= 4.5 || value_nums[41] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体2电压");
                                            }));

                                        }
                                        else if (value_nums[42] >= 4.5 || value_nums[42] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体3电压");
                                            }));

                                        }
                                        else if (value_nums[43] >= 4.5 || value_nums[43] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体4电压");
                                            }));

                                        }
                                        else if (value_nums[44] >= 4.5 || value_nums[44] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体5电压");
                                            }));

                                        }
                                        else if (value_nums[45] >= 4.5 || value_nums[45] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体6电压");
                                            }));

                                        }
                                        else if (value_nums[46] >= 4.5 || value_nums[46] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体7电压");
                                            }));

                                        }
                                        else if (value_nums[47] >= 4.5 || value_nums[47] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体8电压");
                                            }));

                                        }
                                        else if (value_nums[48] >= 4.5 || value_nums[48] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体9电压");
                                            }));

                                        }
                                    }
                                }
                                //读取“读取电源B组电池电压1”的数据并判断是否故障
                                else if (bianliang_flag == "B组电池电压1")
                                {
                                    b = value_nums[5];
                                    a = TEST.Class1.test_dy(value_nums);//
                                    if (a >= 0.5)
                                    {
                                        label_dll.Invoke(new EventHandler(delegate { label_dll.Text = string.Format("故障"); }));
                                        label_dll.ForeColor = System.Drawing.Color.Red;
                                        if (value_nums[0] >= 28.5 || value_nums[0] <= 27.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：28V母线电压");
                                            }));

                                        }
                                        else if (value_nums[1] >= 43 || value_nums[1] <= -0.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：28V负载电流");
                                            }));

                                        }
                                        else if (value_nums[2] >= 43 || value_nums[2] <= 41)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：42V母线电压");
                                            }));

                                        }
                                        else if (value_nums[3] >= 78 || value_nums[3] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：42V负载电流");
                                            }));

                                        }
                                        else if (value_nums[4] >= 39 || value_nums[4] <= -0.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组电池电压1");
                                            }));

                                        }
                                        else if (value_nums[5] >= 39 || value_nums[5] <= -0.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组电池电压1");
                                            }));

                                        }
                                        else if (value_nums[6] >= 110 || value_nums[6] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：太阳电池阵输出电流");
                                            }));

                                        }
                                        else if (value_nums[7] >= 39 || value_nums[7] <= -0.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组电池电压2");
                                            }));

                                        }
                                        else if (value_nums[8] >= 39 || value_nums[8] <= -0.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组电池电压2");
                                            }));

                                        }
                                        else if (value_nums[9] >= 22 || value_nums[9] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：MEA电压");
                                            }));

                                        }
                                        else if (value_nums[10] >= 35 || value_nums[10] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组电池温度1");
                                            }));

                                        }
                                        else if (value_nums[11] >= 35 || value_nums[11] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组电池温度2");
                                            }));

                                        }
                                        else if (value_nums[12] >= 35 || value_nums[12] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组电池温度1");
                                            }));

                                        }
                                        else if (value_nums[13] >= 35 || value_nums[13] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组电池温度2");
                                            }));

                                        }
                                        else if (value_nums[14] >= 35 || value_nums[14] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组电池温度4");
                                            }));

                                        }
                                        else if (value_nums[15] >= 35 || value_nums[15] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组电池温度3");
                                            }));

                                        }
                                        else if (value_nums[16] >= 35 || value_nums[16] <= -10)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组电池温度4");
                                            }));

                                        }
                                        else if (value_nums[17] >= 55 || value_nums[17] <= -15)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：充放电模块温度");
                                            }));

                                        }
                                        else if (value_nums[18] >= 55 || value_nums[18] <= -15)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：分流模块温度");
                                            }));

                                        }
                                        else if (value_nums[19] >= 12 || value_nums[19] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A1充电电流");
                                            }));

                                        }
                                        else if (value_nums[20] >= 12 || value_nums[20] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A2充电电流");
                                            }));

                                        }
                                        else if (value_nums[21] >= 12 || value_nums[21] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B1充电电流");
                                            }));

                                        }
                                        else if (value_nums[22] >= 12 || value_nums[22] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B2充电电流");
                                            }));

                                        }
                                        else if (value_nums[23] >= 20 || value_nums[23] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A1放电电流");
                                            }));

                                        }
                                        else if (value_nums[24] >= 20 || value_nums[24] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A2放电电流");
                                            }));

                                        }
                                        else if (value_nums[25] >= 20 || value_nums[25] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A3放电电流");
                                            }));

                                        }
                                        else if (value_nums[26] >= 20 || value_nums[26] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A4放电电流");
                                            }));

                                        }
                                        else if (value_nums[27] >= 20 || value_nums[27] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B1放电电流");
                                            }));

                                        }
                                        else if (value_nums[28] >= 20 || value_nums[28] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B2放电电流");
                                            }));

                                        }
                                        else if (value_nums[29] >= 20 || value_nums[29] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B3放电电流");
                                            }));

                                        }
                                        else if (value_nums[30] >= 20 || value_nums[30] <= -1)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B4放电电流");
                                            }));

                                        }
                                        else if (value_nums[31] >= 4.5 || value_nums[31] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体1电压");
                                            }));

                                        }
                                        else if (value_nums[32] >= 4.5 || value_nums[32] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体2电压");
                                            }));

                                        }
                                        else if (value_nums[33] >= 4.5 || value_nums[33] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体3电压");
                                            }));

                                        }
                                        else if (value_nums[34] >= 4.5 || value_nums[34] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体4电压");
                                            }));

                                        }
                                        else if (value_nums[35] >= 4.5 || value_nums[35] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体5电压");
                                            }));

                                        }
                                        else if (value_nums[36] >= 4.5 || value_nums[36] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体6电压");
                                            }));

                                        }
                                        else if (value_nums[37] >= 4.5 || value_nums[37] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体7电压");
                                            }));

                                        }
                                        else if (value_nums[38] >= 4.5 || value_nums[38] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体8电压");
                                            }));

                                        }
                                        else if (value_nums[39] >= 4.5 || value_nums[39] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：A组单体9电压");
                                            }));

                                        }
                                        else if (value_nums[40] >= 4.5 || value_nums[40] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体1电压");
                                            }));

                                        }
                                        else if (value_nums[41] >= 4.5 || value_nums[41] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体2电压");
                                            }));

                                        }
                                        else if (value_nums[42] >= 4.5 || value_nums[42] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体3电压");
                                            }));

                                        }
                                        else if (value_nums[43] >= 4.5 || value_nums[43] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体4电压");
                                            }));

                                        }
                                        else if (value_nums[44] >= 4.5 || value_nums[44] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体5电压");
                                            }));

                                        }
                                        else if (value_nums[45] >= 4.5 || value_nums[45] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体6电压");
                                            }));

                                        }
                                        else if (value_nums[46] >= 4.5 || value_nums[46] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体7电压");
                                            }));

                                        }
                                        else if (value_nums[47] >= 4.5 || value_nums[47] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体8电压");
                                            }));

                                        }
                                        else if (value_nums[48] >= 4.5 || value_nums[48] <= 2.5)
                                        {
                                            abn_state_lstbox.Invoke(new EventHandler(delegate
                                            {
                                                abn_state_lstbox.Items.Add("故障时间：" + line_sp[0]);
                                                abn_state_lstbox.Items.Add("故障部件：B组单体9电压");
                                            }));

                                        }
                                    }
                                }
                                #endregion
                                if (i % (count_txt_readline / count_config_chartpoint) == 0)
                                {
                                    dt_pro.Rows.Add(dy_shijian_hengzuobiao, a);
                                    dt_data.Rows.Add(dy_shijian_hengzuobiao, b);
                                }
                                if (dy_nianyueri == dll_nianyueri_end_ST && dy_shifen == dll_shifen_end_ST)
                                {

                                    ST_flag = 0;
                                    ShowMessage(progress.endhandle);
                                    this.Invoke(new EventHandler(delegate { tabledraw(); }));
                                    return;
                                }
                            }

                        }
                        line = reader.ReadLine();
                        i += 1;
                    }
                    //read_first_num += 5;

                }
            }

        }
        #endregion
        //程序主体
        #region 原有函数
        //时间戳转换成时间
        private DateTime TimeStampToDateTime(string TimeStamp_l)
        {
            DateTime otime = new DateTime();
            string t1 = DateTime.UtcNow.ToString("2000-01-01 12:00:00");
            DateTime utc1 = DateTime.Parse(t1).AddTicks(long.Parse(TimeStamp_l) * 10);
            DateTime local1 = utc1.ToLocalTime();//转成本地时间
            otime = local1;
            return otime;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (abn_state_lstbox.Items == null)
            {
            }
            else
            {

                StreamWriter sw = File.CreateText(string.Format("D://{0}时段故障.txt", dll_nianyueri_start_ST.Replace(":", "-") + dll_shifen_start_ST.Replace(":", "-") + "至" + dll_nianyueri_end_ST.Replace(":", "-") + dll_shifen_end_ST.Replace(":", "-")));
                for (int i = 0; i < abn_state_lstbox.Items.Count; i++)
                {
                    sw.WriteLine(abn_state_lstbox.Items[i]);
                }
                sw.Close();

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Logon L = new Logon();
            //L.Show();
            this.WindowState = FormWindowState.Minimized;
        }

        #region 读取文件夹下所有txt文件
        //按照文件创建时间排序
        public static void SortAsFileCreationTime(ref FileInfo[] arrFi)
        {
            Array.Sort(arrFi, delegate (FileInfo x, FileInfo y)
            {
                return x.LastWriteTime.CompareTo(y.LastWriteTime);
            }
            );
        }

        //读取指定路径下所有文件名d
        public static string[] getFileName(string path)
        {
            DirectoryInfo root = new DirectoryInfo(path);
            FileInfo[] fsinfos = root.GetFiles("*.*");
            //string[] txt_name_list = new string[]{};
            List<string> strList = new List<string>();
            SortAsFileCreationTime(ref fsinfos);
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
        #endregion

        #endregion

        private void chart_pro_Click(object sender, EventArgs e)
        {
            
        }
    }
}
