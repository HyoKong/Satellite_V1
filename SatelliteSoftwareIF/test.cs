using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;


namespace SatelliteSoftwareIF
{
    public partial class test : Form
    {
        public test()
        {
            InitializeComponent();
            InitChart();
            
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
        System.Windows.Forms.Timer chartTimer = new System.Windows.Forms.Timer();
        //System.Timers.Timer chartTimer = new System.Timers.Timer(5000);

        private void InitChart()
        {
            //DateTime time = DateTime.Now;
            //chartTimer.Interval = 500;
            //chartTimer.Tick += chartTimer_Tick;
            //chartTimer.Elapsed += chartTimer_Tick;
            //chartTimer.Elapsed += new System.Timers.ElapsedEventHandler(chartTimer_Tick);
            chartTimer.Enabled = true;
            //chartTimer.AutoReset = false;
            //chartDemo.DoubleClick += chartDemo_DoubleClick;

            Series series = chartDemo.Series[0];
            series.ChartType = SeriesChartType.Spline;

            chartDemo.ChartAreas[0].AxisX.LabelStyle.Format = "HH:mm:ss";
            chartDemo.ChartAreas[0].AxisX.ScaleView.Size = 10;
            chartDemo.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;
            chartDemo.ChartAreas[0].AxisX.ScrollBar.Enabled = true;

            chartTimer.Start();

        }


        void chartDemo_DoubleClick(object sender, EventArgs e)
        {
            chartDemo.ChartAreas[0].AxisX.ScaleView.Size = 5;
            chartDemo.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;
            chartDemo.ChartAreas[0].AxisX.ScrollBar.Enabled = true;
            //throw new NotImplementedException();
        }

        void chartTimer_Tick(object sender, EventArgs e)
        {
            
            //chartTimer.Enabled = false;
            //while (true)
            //{
                Random ra = new Random();
                Series series = chartDemo.Series[0];
                series.Points.AddXY(DateTime.Now, ra.Next(1, 10));

                chartDemo.ChartAreas[0].AxisX.ScaleView.Position = series.Points.Count - 10;

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
            //}
        }

        //public static void Delay(int milliSecond)
        //{
        //    int start = Environment.TickCount;
        //    while (Math.Abs(Environment.TickCount - start) < milliSecond)
        //    {
        //        Application.DoEvents();
        //    }
        //}

        private void test_Load(object sender, EventArgs e)
        {
            //while (true)
            //{
            //    Random ra = new Random();
            //    Series series = chartDemo.Series[0];
            //    series.Points.AddXY(DateTime.Now, ra.Next(1, 10));

            //    chartDemo.ChartAreas[0].AxisX.ScaleView.Position = series.Points.Count - 10;
            //    System.Threading.Thread.Sleep(2000);
            //    //Delay(1000);

            //}

        }

        private void test_Shown(object sender, EventArgs e)
        {
            //    Series series = chartDemo.Series[0];
            //    chartTimer.Enabled = false;
            //    while (true)
            //    {
            //        Random ra = new Random();

            //        series.Points.AddXY(DateTime.Now, ra.Next(1, 10));

            //        chartDemo.ChartAreas[0].AxisX.ScaleView.Position = series.Points.Count - 10;

            //        //long i = 0;
            //        //long j = 0;
            //        //long iMax = 200000000;
            //        //while (i < iMax)
            //        //{
            //        //    while (j < iMax)
            //        //    {
            //        //        j++;
            //        //    }
            //        //    i++;
            //        //}
            //    }
            SplineChartExample();
            DateTime beforDT = System.DateTime.Now;
            string filePath = @"D:\Satellite\2019二月修改(0325)\SatelliteSoftwareIF\SatelliteSoftwareIF\bin\x64\Debug\读取多个文件实验\探测仪\探测仪_1_2018_01_15_19_00_00.txt";
            int numberOfColumns = 9;
            ConvertToDataTable(filePath,numberOfColumns);

            DateTime afterDT = System.DateTime.Now;
            TimeSpan ts = afterDT.Subtract(beforDT);
            Console.WriteLine("DateTime总共花费{0}ms.", ts.TotalMilliseconds);
            



        }

        private void SplineChartExample()
        {
            this.chartDemo.Series.Clear();

            this.chartDemo.Titles.Add("Total Income");

            Series series = this.chartDemo.Series.Add("Total Income");
            series.ChartType = SeriesChartType.Spline;
            series.Points.AddXY("September", 100);
            series.Points.AddXY("Obtober", 300);
            series.Points.AddXY("November", 800);
            series.Points.AddXY("December", 200);
            series.Points.AddXY("January", 600);
            series.Points.AddXY("February", 400);
        }

        public DataTable ConvertToDataTable(string filePath, int numberOfColumns)
        {
            DataTable tbl = new DataTable();

            //for (int col = 0; col < numberOfColumns; col++)
            //    tbl.Columns.Add(new DataColumn("Column" + (col + 1).ToString()));


            string[] lines = System.IO.File.ReadAllLines(filePath);
            int count = 0;
            foreach (string line in lines)
            {
                var cols = line.Split('\t');

                //DataRow dr = tbl.NewRow();
                //for (int cIndex = 0; cIndex < numberOfColumns; cIndex++)
                //{
                //    dr[cIndex] = cols[cIndex];
                //}

                //tbl.Rows.Add(dr);
                count++;
            }
            Console.WriteLine(count);

            return tbl;
        }
    }
}
