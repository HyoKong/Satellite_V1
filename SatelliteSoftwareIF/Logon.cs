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
    public partial class Logon : Form
    {
        //声明年月日时分的全局变量
        public string dll_nianyueri_start_ST = "";
        public string dll_shifen_start_ST = "";
        public string dll_nianyueri_end_ST = "";
        public string dll_shifen_end_ST = "";
        public int ST_flag = 0;
        public Logon()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void Logon_Load(object sender, EventArgs e)
        {
            //Msg m = new Msg();
            //m.Show();
            
        }

        private void Logon_FormClosing(object sender, FormClosingEventArgs e)
        {
            Logon l = new Logon();
            l.Show();
            CloseMsg m = new CloseMsg();
            m.Show();
            //System.Environment.Exit(0);
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btn_fault_dignosis_Click(object sender, EventArgs e)
        {
            Form1 F1 = new Form1();
            F1.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SelectTime ST = new SelectTime();
            ST.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CloseMsg m = new CloseMsg();
            m.Show();
            //System.Environment.Exit(0);
        }

        private void help_btn_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("Explorer.exe", @".\软件说明书\");
        }
        public void Add(string item1, string item2, string item3, string item4, int start)
        {
            //this.abn_state_lstbox.Items.Add(item1);
            this.dll_nianyueri_start_ST = item1;
            this.dll_shifen_start_ST = item2;
            this.dll_nianyueri_end_ST = item3;
            this.dll_shifen_end_ST = item4;
            this.ST_flag = start;
        }
    }
}
