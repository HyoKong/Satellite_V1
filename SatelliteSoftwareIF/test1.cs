using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Windows.Forms.DataVisualization.Charting;

namespace SatelliteSoftwareIF
{
    public partial class test1 : Form
    {


        private double dm = 0;
        private double vm = 0;         //速度

        private double ss, st = 0;        //时间间隔
        private List<double> VM = new List<double>(100);
        private List<double> DM = new List<double>(100);
        private Queue<double> Vm = new Queue<double>(100);
        private Queue<double> Dm = new Queue<double>(100);
        private bool Ready = false;
        private bool contrl = false;

        public test1()
        {
            InitializeComponent();
        }
        //--------距离监测图表-----------//
        //入口参数:  无                      //
        //                                   //
        private void DistanceData_Line()
        {
            this.chart1.ChartAreas.Clear();
            ChartArea chartarea = new ChartArea("距离监测");
            this.chart1.ChartAreas.Add(chartarea);

            chartarea.AxisX.MajorGrid.LineColor = System.Drawing.Color.Transparent;
            chartarea.AxisY.MajorGrid.LineColor = System.Drawing.Color.Black;
            chartarea.AxisX.ScrollBar.Enabled = false;
            //chartarea.AxisY.Minimum = 0;
            //chartarea.AxisY.Maximum = 10000;
            chartarea.AxisX.Interval = 5;
            chartarea.AxisX.MajorGrid.LineColor = System.Drawing.Color.Transparent;
            chartarea.AxisY.MajorGrid.LineColor = System.Drawing.Color.Black;

            //chartarea.AxisX.ScrollBar.Enabled = false;
            //////指当前显示的是第几个           
            //chartarea.AxisX.ScaleView.Size = 10;
            //表头
            this.chart1.Titles.Clear();
            this.chart1.Titles.Add("距离监测");
            this.chart1.Titles[0].Text = "距离监测";
            this.chart1.Titles[0].ForeColor = Color.Red;
            this.chart1.Titles[0].Font = new System.Drawing.Font("Microsoft Sans serif", 12f);

            //定义存储和显示点的容器
            this.chart1.Series.Clear();
            Series series = new Series("距离");
            series.ChartType = SeriesChartType.Spline;
            series.ChartArea = "距离监测";
            series.Color = Color.Yellow;
            series.MarkerBorderWidth = 2;
            series.MarkerSize = 4;
            series.MarkerStyle = MarkerStyle.Diamond;
            series.ToolTip = "距离" + "#VAL \r\n #AXISLABEL";
            chart1.Series.Add(series);

        }


        //--------速度监测图表-----------//
        //入口参数:   无                       //      
        private void VelocityData_Line()
        {

            //定义表区域
            this.chart2.ChartAreas.Clear();
            ChartArea chartarea = new ChartArea("速度监测");
            this.chart2.ChartAreas.Add(chartarea);
            chartarea.AxisX.MajorGrid.LineColor = System.Drawing.Color.Transparent;
            chartarea.AxisY.MajorGrid.LineColor = System.Drawing.Color.Black;
            chartarea.AxisX.ScrollBar.Enabled = false;
            //chartarea.AxisY.Minimum = 0;
            //chartarea.AxisY.Maximum = 100;
            chartarea.AxisX.Interval = 5;
            chartarea.AxisX.MajorGrid.LineColor = System.Drawing.Color.Transparent;
            chartarea.AxisY.MajorGrid.LineColor = System.Drawing.Color.Black;
            //chartarea.AxisX.ScrollBar.Enabled = false;
            ////指当前显示的是第几个           
            //chartarea.AxisX.ScaleView.Size = 10;
            //表头
            this.chart2.Titles.Clear();
            this.chart2.Titles.Add("速度监测");
            this.chart2.Titles[0].Text = "速度监测";
            this.chart2.Titles[0].ForeColor = Color.Red;
            this.chart2.Titles[0].Font = new System.Drawing.Font("Microsoft Sans serif", 12f);
            //定义存储和显示点的容器

            this.chart2.Series.Clear();
            Series series = new Series("速度");
            chart2.Series.Add(series);
            series.ChartArea = "速度监测";
            series.ChartType = SeriesChartType.Spline;
            series.MarkerBorderWidth = 2;
            series.MarkerSize = 4;
            series.MarkerStyle = MarkerStyle.Diamond;
            series.ToolTip = "速度" + "#VAL \r\n #AXISLABEL";

        }
        //--------飞行数据监测图表-----------//
        //入口参数:   无                       //      
        private void FlyData_Line()
        {

            //定义表区域
            this.chart3.ChartAreas.Clear();
            ChartArea chartarea = new ChartArea("数据监测");
            this.chart3.ChartAreas.Add(chartarea);
            chartarea.AxisX.Maximum = VM.Count;
            chartarea.AxisX.MajorGrid.LineColor = System.Drawing.Color.Transparent;
            chartarea.AxisY.MajorGrid.LineColor = System.Drawing.Color.Black;
            chartarea.AxisX.ScrollBar.Enabled = false;
            //chartarea.AxisY.Minimum = 0;
            //chartarea.AxisY.Maximum = 100;
            chartarea.AxisX.Interval = 5;
            chartarea.AxisX.MajorGrid.LineColor = System.Drawing.Color.Transparent;
            chartarea.AxisY.MajorGrid.LineColor = System.Drawing.Color.Black;
            //chartarea.AxisX.ScrollBar.Enabled = false;
            ////指当前显示的是第几个           
            //chartarea.AxisX.ScaleView.Size = 10;
            //表头
            this.chart3.Titles.Clear();
            this.chart3.Titles.Add("数据监测");
            this.chart3.Titles[0].Text = "数据监测";
            this.chart3.Titles[0].ForeColor = Color.Red;
            this.chart3.Titles[0].Font = new System.Drawing.Font("Microsoft Sans serif", 12f);
            //定义存储和显示点的容器
            this.chart3.Series.Clear();
            for (int i = 0; i < 2; i++)
            {
                Series series = new Series(DATATYPE[i]);
                chart3.Series.Add(series);
                series.ChartArea = "数据监测";
                series.ChartType = SeriesChartType.Spline;
                series.MarkerBorderWidth = 2;
                series.MarkerSize = 4;
                series.MarkerStyle = MarkerStyle.Diamond;
                series.ToolTip = DATATYPE[i] + "#VAL \r\n #AXISLABEL";
            }
            this.chart3.Series[0].YAxisType = AxisType.Primary;
            this.chart3.Series[1].YAxisType = AxisType.Secondary;
        }

        private static string[] DATATYPE = { "速度", "距离" };
        //--------飞行轨迹数据导入图表-------------------------           //
        //入口参数:                                                       //
        //距离:  dM                                                       //
        //速度:  vM                                                       //
        private void ChartData()
        {

            if (Vm.Count > 10)//当数据大于十，删除第一位数
            {
                Vm.Dequeue();

                Dm.Dequeue();

            }
            if (Ready == true)
            {

                VM.Add(vm);
                DM.Add(dm);
                Vm.Enqueue(vm);
                Dm.Enqueue(dm);


            }
            for (int i = 0; i < Vm.Count; i++)
            {
                this.chart1.Series[0].Points.AddY(Vm.ElementAt(i));
                this.chart2.Series[0].Points.AddY(Dm.ElementAt(i));
            }
            for (int i = 0; i < VM.Count; i++)
            {

                //指示速度是否显示
                if (checkedListBox1.GetItemChecked(0) == true)
                {
                    this.chart3.Series[0].Points.AddY(VM.ElementAt(i));
                }
                if (checkedListBox1.GetItemChecked(0) == false)
                {
                    this.chart3.Series[0].Points.Clear();
                }
                //指示距离是否显示
                if (checkedListBox1.GetItemChecked(1) == true)
                {
                    this.chart3.Series[1].Points.AddY(DM.ElementAt(i));
                }
                if (checkedListBox1.GetItemChecked(1) == false)
                {
                    this.chart3.Series[1].Points.Clear();
                }

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (contrl == false)
            {
                Ready = true;
                button1.Text = "停止";
                contrl = true;

            }
            else
            {
                Ready = false;
                button1.Text = "开始";
                contrl = true;

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, true);
            }
            timer1.Enabled = true;
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void test1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, true);
            }
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Random rd = new Random();
            vm = rd.Next(0, 100);
            dm = rd.Next(0, 500);

            FlyData_Line();
            VelocityData_Line();
            DistanceData_Line();
            ChartData();
        }
    }
}
