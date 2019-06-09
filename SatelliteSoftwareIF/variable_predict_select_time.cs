using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SatelliteSoftwareIF
{
    public partial class variable_predict_select_time : Form
    {
        public variable_predict_select_time()
        {
            InitializeComponent();
        }

        // btn 'cancel
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            //VariablePrediction variablePrediction = new VariablePrediction();
            //variablePrediction.ShowDialog();
            //this.Dispose();
        }

        // initialize Variables combo box according to different parts.
        private void cbParts_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] dll_str = new string[] {"动量轮1马达电流","动量轮2马达电流","动量轮4马达电流","动量轮5马达电流"
                ,"动量轮1轴温","动量轮2轴温","动量轮3轴温","动量轮4轴温","动量轮5轴温","动量轮6轴温"};
            string[] dy_str = new string[] { "28V负载电压", "28V母线电压", "42V负载电压", "42V母线电压", "A组电池电压1", "B组电池电压1" };
            string[] tcy_str = new string[] { "探测仪东西电机A相电流", "探测仪东西电机B相电流", "探测仪东西电机总电流", "探测仪南北电机A相电流", "探测仪南北电机B相电流", "探测仪南北电机总电流" };
            string[] fsj_str = new string[] { "辐射计东西总电流", "辐射计南北总电流" };
            if (cbParts.SelectedItem.ToString() == "动量轮")
            {
                cbParts.Items.Clear();
                for (int i = 0; i < dll_str.Length; i++)
                    cbVariables.Items.Add(dll_str[i]);
            }
            else if (cbParts.SelectedItem.ToString() == "电源")
            {
                cbVariables.Items.Clear();
                for (int i = 0; i < dy_str.Length; i++)
                    cbVariables.Items.Add(dy_str[i]);
            }
            else if (cbParts.SelectedItem.ToString() == "探测仪")
            {
                cbVariables.Items.Clear();
                for (int i = 0; i < tcy_str.Length; i++)
                    cbVariables.Items.Add(tcy_str[i]);
            }
            else if (cbParts.SelectedItem.ToString() == "辐射计")
            {
                cbVariables.Items.Clear();
                for (int i = 0; i < fsj_str.Length; i++)
                    cbVariables.Items.Add(fsj_str[i]);
            }
        }

        private void cbVariables_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbParts.SelectedItem == "动量轮")
            {
                nianyueri_start_tB.Text = "20171218";
                nianyueri_end_tB.Text = "20171218";
                shifen_start_tB.Text = "08";
                shifen_end_tB.Text = "09";
            }
            else if (cbParts.SelectedItem == "电源")
            {
                nianyueri_start_tB.Text = "20171218";
                nianyueri_end_tB.Text = "20171218";
                shifen_start_tB.Text = "08:30";
                shifen_end_tB.Text = "09:00";
            }
            else if (cbParts.SelectedItem == "辐射计")
            {
                nianyueri_start_tB.Text = "2018115";
                nianyueri_end_tB.Text = "2018115";
                shifen_start_tB.Text = "18:48";
                shifen_end_tB.Text = "18:49";
            }
            else if (cbParts.SelectedItem == "探测仪")
            {
                nianyueri_start_tB.Text = "2018115";
                nianyueri_end_tB.Text = "2018115";
                shifen_start_tB.Text = "19:02";
                shifen_end_tB.Text = "19:03";
            }
        }
    }
}
