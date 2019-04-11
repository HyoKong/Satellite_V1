using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SatelliteSoftwareIF
{
    public partial class Msg : Form
    {
        public Msg()
        {
            InitializeComponent();
        }

        private void Msg_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string[] str = File.ReadAllText("./in.txt", Encoding.Default).Split('|');
                if (textBox1.Text == str[0].TrimEnd() && textBox2.Text == str[1].TrimEnd())
                {
                    MessageBox.Show("登录成功!");
                    Logon.ActiveForm.Hide();
                    Msg.ActiveForm.Hide();
                    Logon L = new Logon();
                    L.Show();
                    /*
                    qushi q = new qushi();
                    q.Show();
                    zhenduan c = new zhenduan();
                    c.Show();*/
                }
                else
                {
                    textBox2.Clear();
                    MessageBox.Show("登录失败!请检查账号密码是否正确");
                }
            }
            catch
            {
                MessageBox.Show("登录失败!请检查数据文件是否存在");
            }
        }

        private void Msg_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }
    }
}
