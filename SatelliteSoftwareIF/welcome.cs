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
    public partial class Welcome : Form
    {
        public Welcome()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Welcome_Load(object sender, EventArgs e)
        {
            Msg m = new Msg();
            m.Show();
        }
    }
}
