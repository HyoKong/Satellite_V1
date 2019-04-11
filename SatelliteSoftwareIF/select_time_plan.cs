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
//using System.Text;
using System.Windows.Forms.DataVisualization.Charting;

namespace SatelliteSoftwareIF
{
    
    public partial class SelectTime : Form
    {

        public SelectTime()
        {
            InitializeComponent();
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            Form_ST fst = new Form_ST(this.nianyueri_start_tB.Text,this.shifen_start_tB.Text,this.nianyueri_end_tB.Text,this.shifen_end_tB.Text
                ,this.comboBox1.SelectedItem.ToString(),this.comboBox2.SelectedItem.ToString());
            fst.ShowDialog();
            //Form1_paln f_plan = new Form1_paln();
            //f_plan.ShowDialog();
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
            /*
            DataTable dt = new DataTable();
            dt.Columns.Add("X_Value", typeof(string));
            dt.Columns.Add("Y_Value", typeof(double));

            StreamReader sr = new StreamReader(@"E:\工作\教研室资料\卫星项目\软件设计\plot绘图（晗旸）.txt");
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] strarr = line.Split(',');

                dt.Rows.Add(strarr[0], strarr[1]);
            }
            
            chart1.DataSource = dt;
            chart1.Series["Series1"].XValueMember = "X_Value";
            chart1.Series["Series1"].YValueMembers = "Y_Value";
            chart1.Series["Series1"].ChartType = SeriesChartType.Line;
            chart1.ChartAreas[0].AxisY.LabelStyle.Format = "";
            if (sr != null) sr.Close(); 
             */
        }

        private void SelectTime_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] dll_str = new string[] {"动量轮1马达电流","动量轮2马达电流","动量轮4马达电流","动量轮5马达电流"
                ,"动量轮1轴温","动量轮2轴温","动量轮3轴温","动量轮4轴温","动量轮5轴温","动量轮6轴温"};
            string[] dy_str = new string[] { "28V负载电压", "28V母线电压", "42V负载电压", "42V母线电压", "A组电池电压1", "B组电池电压1" };
            string[] tcy_str = new string[] { "探测仪东西电机A相电流", "探测仪东西电机B相电流", "探测仪东西电机总电流", "探测仪南北电机A相电流", "探测仪南北电机B相电流", "探测仪南北电机总电流" };
            string[] fsj_str = new string[] { "辐射计东西总电流", "辐射计南北总电流" };
            if (comboBox1.SelectedItem.ToString() == "动量轮")
            {
                comboBox2.Items.Clear();
                for (int i = 0; i < dll_str.Length; i++)
                    comboBox2.Items.Add(dll_str[i]);
            }
            else if (comboBox1.SelectedItem.ToString() == "电源")
            {
                comboBox2.Items.Clear();
                for (int i = 0; i < dy_str.Length; i++)
                    comboBox2.Items.Add(dy_str[i]);
            }
            else if (comboBox1.SelectedItem.ToString() == "探测仪")
            {
                comboBox2.Items.Clear();
                for (int i = 0; i < tcy_str.Length; i++)
                    comboBox2.Items.Add(tcy_str[i]);
            }
            else if (comboBox1.SelectedItem.ToString() == "辐射计")
            {
                comboBox2.Items.Clear();
                for (int i = 0; i < fsj_str.Length; i++)
                    comboBox2.Items.Add(fsj_str[i]);
            }
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == "动量轮")
            {
                nianyueri_start_tB.Text = "20171218";
                nianyueri_end_tB.Text = "20171218";
                shifen_start_tB.Text = "08:02";
                shifen_end_tB.Text = "09:02";
            }
            else if (comboBox1.SelectedItem == "电源")
            {
                nianyueri_start_tB.Text = "20171218";
                nianyueri_end_tB.Text = "20171218";
                shifen_start_tB.Text = "08:30";
                shifen_end_tB.Text = "09:00";
            }
            else if (comboBox1.SelectedItem == "辐射计")
            {
                nianyueri_start_tB.Text = "2018115";
                nianyueri_end_tB.Text = "2018115";
                shifen_start_tB.Text = "18:48";
                shifen_end_tB.Text = "18:49";
            }
            else if (comboBox1.SelectedItem == "探测仪")
            {
                nianyueri_start_tB.Text = "2018115";
                nianyueri_end_tB.Text = "2018115";
                shifen_start_tB.Text = "19:02";
                shifen_end_tB.Text = "19:03";
            }
        }

        private void nianyueri_start_tB_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
