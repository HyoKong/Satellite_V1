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
    public partial class VariablePrediction : Form
    {
        #region class properties and class initialization
        // class properties
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string StartHour { get; set; }
        public string EndHour { get; set; }
        public string Part { get; set; }
        public string Variable { get; set; }

        // root of txt file
        public string[] dll_file = getFileName("./读取多个文件实验/动量轮");
        public string[] fsj_file = getFileName("./读取多个文件实验/辐射计");
        public string[] tcy_file = getFileName("./读取多个文件实验/探测仪");
        public string[] dy_file = getFileName("./读取多个文件实验/电源");

        // initialize prediction datatable
        private DataTable dtPrediction = new DataTable();
        #endregion

        public VariablePrediction(string startDate, string startHour, string endDate,string endHour,string part,string variable)
        {
            InitializeComponent();
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.StartHour = startHour;
            this.EndHour = endHour;
            this.Part = part;
            this.Variable = variable;

        }
        #region chart
        //初始化图表
        private void InitChart()
        {
            dtPrediction.Columns.Add("X_Pro_Value", typeof(string));
            dtPrediction.Columns.Add("Y_Pro_Value", typeof(double));

            //######################################故障概率值画图###########################
            chartPrediction.DataSource = dtPrediction;//读取故障概率值画图
            chartPrediction.Series["Series1"].XValueMember = "X_Pro_Value";
            chartPrediction.Series["Series1"].YValueMembers = "Y_Pro_Value";
            chartPrediction.Series["Series1"].ChartType = SeriesChartType.Line;
            chartPrediction.ChartAreas[0].AxisY.LabelStyle.Format = "";
            //设置图表显示样式
            this.chartPrediction.Series["Series1"].IsVisibleInLegend = false;
            this.chartPrediction.Series["Series1"].Color = Color.Red;
            this.chartPrediction.Series["Series1"].BorderWidth = 1;
            this.chartPrediction.ChartAreas[0].AxisY.Minimum = 0;
            this.chartPrediction.ChartAreas[0].AxisY.Maximum = 1;
            this.chartPrediction.ChartAreas[0].AxisY.LabelStyle.Format = "0%";
            this.chartPrediction.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.Silver;
            this.chartPrediction.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.Silver;
            this.chartPrediction.ChartAreas[0].AxisX.IsMarginVisible = false;//横坐标显示完整
            //横纵坐标样式
            this.chartPrediction.ChartAreas[0].AxisX.Title = "时间";
            this.chartPrediction.ChartAreas[0].AxisX.TitleForeColor = System.Drawing.Color.Red;
            this.chartPrediction.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.chartPrediction.ChartAreas[0].AxisY.Title = "变量预测值";
            this.chartPrediction.ChartAreas[0].AxisY.TitleForeColor = System.Drawing.Color.Red;
            this.chartPrediction.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            //设置标题
            this.chartPrediction.Titles.Clear();
            this.chartPrediction.Titles.Add("S01");
            this.chartPrediction.Titles[0].Text = "变量预测结果";
            this.chartPrediction.Titles[0].ForeColor = Color.RoyalBlue;
            this.chartPrediction.Titles[0].Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
        }

        //绘图函数
        private void TableDraw()
        {

            chartPrediction.DataSource = dtPrediction;//读取故障概率值画图
            chartPrediction.Series["Series1"].XValueMember = "X_Pro_Value";
            chartPrediction.Series["Series1"].YValueMembers = "Y_Pro_Value";
            chartPrediction.Series["Series1"].ChartType = SeriesChartType.Line;
            chartPrediction.ChartAreas[0].AxisY.LabelStyle.Format = "";

        }
        #endregion

        #region Get all txt file name under the current folder
        // sort by creation time
        public static void SortAsFileCreationTime(ref FileInfo[] arrFi)
        {
            Array.Sort(arrFi, delegate (FileInfo x, FileInfo y)
            {
                return x.LastWriteTime.CompareTo(y.LastWriteTime);
            }
            );
        }

        // get all txt file name under the current folder
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
    }
}
