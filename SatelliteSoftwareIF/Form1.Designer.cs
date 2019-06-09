namespace SatelliteSoftwareIF
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.开始ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.开启ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.管理员登录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.修改密码ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.通讯ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.参数配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.动量轮1马达电流ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.动量轮2马达电流ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.动量轮4马达电流ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.动量轮5马达电流ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.动量轮1轴温ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.动量轮2轴温ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.动量轮3轴温ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.动量轮4轴温ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.动量轮5轴温ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.动量轮6轴温ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.载荷扫描镜ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.探测仪东西电机A相电流ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.探测仪东西电机B相电流ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.探测仪东西电机总电流ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.探测仪南北电机A相电流ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.探测仪南北电机B相电流ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.探测仪南北电机总电流ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.辐射计ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.辐射计东西总电流ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.辐射计南北总电流ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.电源ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.母线28V电压ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.负载28V电流ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.母线42V电压ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.负载42V电流ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.a组电池电压1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.b组电池电压1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.趋势监测ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.选择时段监测ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.算法ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.故障监测算法ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pLSLRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pLSRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pCAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.异常状态信息导出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.故障诊断结果文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.软件介绍ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.abn_state_lstbox = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_abnstate_cle = new System.Windows.Forms.Button();
            this.btn_save = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.timer_ST = new System.Windows.Forms.Timer(this.components);
            this.dll_rb = new System.Windows.Forms.RadioButton();
            this.label7 = new System.Windows.Forms.Label();
            this.dy_rb = new System.Windows.Forms.RadioButton();
            this.tcy_rb = new System.Windows.Forms.RadioButton();
            this.fsj_rb = new System.Windows.Forms.RadioButton();
            this.label_dll = new System.Windows.Forms.Label();
            this.label_dy = new System.Windows.Forms.Label();
            this.label_tcy = new System.Windows.Forms.Label();
            this.label_fsj = new System.Windows.Forms.Label();
            this.label_shijian = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.PapayaWhip;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.开始ToolStripMenuItem,
            this.通讯ToolStripMenuItem,
            this.趋势监测ToolStripMenuItem,
            this.算法ToolStripMenuItem,
            this.文件ToolStripMenuItem,
            this.帮助ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1443, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 开始ToolStripMenuItem
            // 
            this.开始ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.开启ToolStripMenuItem1,
            this.退出ToolStripMenuItem1});
            this.开始ToolStripMenuItem.Name = "开始ToolStripMenuItem";
            this.开始ToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.开始ToolStripMenuItem.Text = "开始";
            // 
            // 开启ToolStripMenuItem1
            // 
            this.开启ToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.管理员登录ToolStripMenuItem,
            this.修改密码ToolStripMenuItem});
            this.开启ToolStripMenuItem1.Name = "开启ToolStripMenuItem1";
            this.开启ToolStripMenuItem1.Size = new System.Drawing.Size(100, 22);
            this.开启ToolStripMenuItem1.Text = "登录";
            // 
            // 管理员登录ToolStripMenuItem
            // 
            this.管理员登录ToolStripMenuItem.Name = "管理员登录ToolStripMenuItem";
            this.管理员登录ToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.管理员登录ToolStripMenuItem.Text = "管理员登录";
            this.管理员登录ToolStripMenuItem.Click += new System.EventHandler(this.管理员登录ToolStripMenuItem_Click);
            // 
            // 修改密码ToolStripMenuItem
            // 
            this.修改密码ToolStripMenuItem.Name = "修改密码ToolStripMenuItem";
            this.修改密码ToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.修改密码ToolStripMenuItem.Text = "修改密码";
            this.修改密码ToolStripMenuItem.Click += new System.EventHandler(this.修改密码ToolStripMenuItem_Click);
            // 
            // 退出ToolStripMenuItem1
            // 
            this.退出ToolStripMenuItem1.Name = "退出ToolStripMenuItem1";
            this.退出ToolStripMenuItem1.Size = new System.Drawing.Size(100, 22);
            this.退出ToolStripMenuItem1.Text = "退出";
            this.退出ToolStripMenuItem1.Click += new System.EventHandler(this.退出ToolStripMenuItem1_Click);
            // 
            // 通讯ToolStripMenuItem
            // 
            this.通讯ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.参数配置ToolStripMenuItem,
            this.载荷扫描镜ToolStripMenuItem,
            this.辐射计ToolStripMenuItem,
            this.电源ToolStripMenuItem});
            this.通讯ToolStripMenuItem.Name = "通讯ToolStripMenuItem";
            this.通讯ToolStripMenuItem.Size = new System.Drawing.Size(97, 20);
            this.通讯ToolStripMenuItem.Text = "部件变量选择";
            // 
            // 参数配置ToolStripMenuItem
            // 
            this.参数配置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.动量轮1马达电流ToolStripMenuItem,
            this.动量轮2马达电流ToolStripMenuItem,
            this.动量轮4马达电流ToolStripMenuItem,
            this.动量轮5马达电流ToolStripMenuItem,
            this.动量轮1轴温ToolStripMenuItem,
            this.动量轮2轴温ToolStripMenuItem,
            this.动量轮3轴温ToolStripMenuItem,
            this.动量轮4轴温ToolStripMenuItem,
            this.动量轮5轴温ToolStripMenuItem,
            this.动量轮6轴温ToolStripMenuItem});
            this.参数配置ToolStripMenuItem.Name = "参数配置ToolStripMenuItem";
            this.参数配置ToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.参数配置ToolStripMenuItem.Text = "动量轮";
            // 
            // 动量轮1马达电流ToolStripMenuItem
            // 
            this.动量轮1马达电流ToolStripMenuItem.Name = "动量轮1马达电流ToolStripMenuItem";
            this.动量轮1马达电流ToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.动量轮1马达电流ToolStripMenuItem.Text = "动量轮1马达电流";
            this.动量轮1马达电流ToolStripMenuItem.Click += new System.EventHandler(this.动量轮1马达电流ToolStripMenuItem_Click);
            // 
            // 动量轮2马达电流ToolStripMenuItem
            // 
            this.动量轮2马达电流ToolStripMenuItem.Name = "动量轮2马达电流ToolStripMenuItem";
            this.动量轮2马达电流ToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.动量轮2马达电流ToolStripMenuItem.Text = "动量轮2马达电流";
            this.动量轮2马达电流ToolStripMenuItem.Click += new System.EventHandler(this.动量轮2马达电流ToolStripMenuItem_Click_1);
            // 
            // 动量轮4马达电流ToolStripMenuItem
            // 
            this.动量轮4马达电流ToolStripMenuItem.Name = "动量轮4马达电流ToolStripMenuItem";
            this.动量轮4马达电流ToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.动量轮4马达电流ToolStripMenuItem.Text = "动量轮4马达电流";
            this.动量轮4马达电流ToolStripMenuItem.Click += new System.EventHandler(this.动量轮4马达电流ToolStripMenuItem_Click_1);
            // 
            // 动量轮5马达电流ToolStripMenuItem
            // 
            this.动量轮5马达电流ToolStripMenuItem.Name = "动量轮5马达电流ToolStripMenuItem";
            this.动量轮5马达电流ToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.动量轮5马达电流ToolStripMenuItem.Text = "动量轮5马达电流";
            this.动量轮5马达电流ToolStripMenuItem.Click += new System.EventHandler(this.动量轮5马达电流ToolStripMenuItem_Click_1);
            // 
            // 动量轮1轴温ToolStripMenuItem
            // 
            this.动量轮1轴温ToolStripMenuItem.Name = "动量轮1轴温ToolStripMenuItem";
            this.动量轮1轴温ToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.动量轮1轴温ToolStripMenuItem.Text = "动量轮1轴温";
            this.动量轮1轴温ToolStripMenuItem.Click += new System.EventHandler(this.动量轮1轴温ToolStripMenuItem_Click_1);
            // 
            // 动量轮2轴温ToolStripMenuItem
            // 
            this.动量轮2轴温ToolStripMenuItem.Name = "动量轮2轴温ToolStripMenuItem";
            this.动量轮2轴温ToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.动量轮2轴温ToolStripMenuItem.Text = "动量轮2轴温";
            this.动量轮2轴温ToolStripMenuItem.Click += new System.EventHandler(this.动量轮2轴温ToolStripMenuItem_Click_1);
            // 
            // 动量轮3轴温ToolStripMenuItem
            // 
            this.动量轮3轴温ToolStripMenuItem.Name = "动量轮3轴温ToolStripMenuItem";
            this.动量轮3轴温ToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.动量轮3轴温ToolStripMenuItem.Text = "动量轮3轴温";
            this.动量轮3轴温ToolStripMenuItem.Click += new System.EventHandler(this.动量轮3轴温ToolStripMenuItem_Click_1);
            // 
            // 动量轮4轴温ToolStripMenuItem
            // 
            this.动量轮4轴温ToolStripMenuItem.Name = "动量轮4轴温ToolStripMenuItem";
            this.动量轮4轴温ToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.动量轮4轴温ToolStripMenuItem.Text = "动量轮4轴温";
            this.动量轮4轴温ToolStripMenuItem.Click += new System.EventHandler(this.动量轮4轴温ToolStripMenuItem_Click_1);
            // 
            // 动量轮5轴温ToolStripMenuItem
            // 
            this.动量轮5轴温ToolStripMenuItem.Name = "动量轮5轴温ToolStripMenuItem";
            this.动量轮5轴温ToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.动量轮5轴温ToolStripMenuItem.Text = "动量轮5轴温";
            this.动量轮5轴温ToolStripMenuItem.Click += new System.EventHandler(this.动量轮5轴温ToolStripMenuItem_Click_1);
            // 
            // 动量轮6轴温ToolStripMenuItem
            // 
            this.动量轮6轴温ToolStripMenuItem.Name = "动量轮6轴温ToolStripMenuItem";
            this.动量轮6轴温ToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.动量轮6轴温ToolStripMenuItem.Text = "动量轮6轴温";
            this.动量轮6轴温ToolStripMenuItem.Click += new System.EventHandler(this.动量轮6轴温ToolStripMenuItem_Click_1);
            // 
            // 载荷扫描镜ToolStripMenuItem
            // 
            this.载荷扫描镜ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.探测仪东西电机A相电流ToolStripMenuItem,
            this.探测仪东西电机B相电流ToolStripMenuItem,
            this.探测仪东西电机总电流ToolStripMenuItem,
            this.探测仪南北电机A相电流ToolStripMenuItem,
            this.探测仪南北电机B相电流ToolStripMenuItem,
            this.探测仪南北电机总电流ToolStripMenuItem});
            this.载荷扫描镜ToolStripMenuItem.Name = "载荷扫描镜ToolStripMenuItem";
            this.载荷扫描镜ToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.载荷扫描镜ToolStripMenuItem.Text = "探测仪";
            // 
            // 探测仪东西电机A相电流ToolStripMenuItem
            // 
            this.探测仪东西电机A相电流ToolStripMenuItem.Name = "探测仪东西电机A相电流ToolStripMenuItem";
            this.探测仪东西电机A相电流ToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.探测仪东西电机A相电流ToolStripMenuItem.Text = "探测仪东西电机A相电流";
            this.探测仪东西电机A相电流ToolStripMenuItem.Click += new System.EventHandler(this.探测仪东西电机A相电流ToolStripMenuItem_Click);
            // 
            // 探测仪东西电机B相电流ToolStripMenuItem
            // 
            this.探测仪东西电机B相电流ToolStripMenuItem.Name = "探测仪东西电机B相电流ToolStripMenuItem";
            this.探测仪东西电机B相电流ToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.探测仪东西电机B相电流ToolStripMenuItem.Text = "探测仪东西电机B相电流";
            this.探测仪东西电机B相电流ToolStripMenuItem.Click += new System.EventHandler(this.探测仪东西电机B相电流ToolStripMenuItem_Click);
            // 
            // 探测仪东西电机总电流ToolStripMenuItem
            // 
            this.探测仪东西电机总电流ToolStripMenuItem.Name = "探测仪东西电机总电流ToolStripMenuItem";
            this.探测仪东西电机总电流ToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.探测仪东西电机总电流ToolStripMenuItem.Text = "探测仪东西电机总电流";
            this.探测仪东西电机总电流ToolStripMenuItem.Click += new System.EventHandler(this.探测仪东西电机总电流ToolStripMenuItem_Click);
            // 
            // 探测仪南北电机A相电流ToolStripMenuItem
            // 
            this.探测仪南北电机A相电流ToolStripMenuItem.Name = "探测仪南北电机A相电流ToolStripMenuItem";
            this.探测仪南北电机A相电流ToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.探测仪南北电机A相电流ToolStripMenuItem.Text = "探测仪南北电机A相电流";
            this.探测仪南北电机A相电流ToolStripMenuItem.Click += new System.EventHandler(this.探测仪南北电机A相电流ToolStripMenuItem_Click);
            // 
            // 探测仪南北电机B相电流ToolStripMenuItem
            // 
            this.探测仪南北电机B相电流ToolStripMenuItem.Name = "探测仪南北电机B相电流ToolStripMenuItem";
            this.探测仪南北电机B相电流ToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.探测仪南北电机B相电流ToolStripMenuItem.Text = "探测仪南北电机B相电流";
            this.探测仪南北电机B相电流ToolStripMenuItem.Click += new System.EventHandler(this.探测仪南北电机B相电流ToolStripMenuItem_Click);
            // 
            // 探测仪南北电机总电流ToolStripMenuItem
            // 
            this.探测仪南北电机总电流ToolStripMenuItem.Name = "探测仪南北电机总电流ToolStripMenuItem";
            this.探测仪南北电机总电流ToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.探测仪南北电机总电流ToolStripMenuItem.Text = "探测仪南北电机总电流";
            this.探测仪南北电机总电流ToolStripMenuItem.Click += new System.EventHandler(this.探测仪南北电机总电流ToolStripMenuItem_Click);
            // 
            // 辐射计ToolStripMenuItem
            // 
            this.辐射计ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.辐射计东西总电流ToolStripMenuItem,
            this.辐射计南北总电流ToolStripMenuItem});
            this.辐射计ToolStripMenuItem.Name = "辐射计ToolStripMenuItem";
            this.辐射计ToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.辐射计ToolStripMenuItem.Text = "辐射计";
            // 
            // 辐射计东西总电流ToolStripMenuItem
            // 
            this.辐射计东西总电流ToolStripMenuItem.Name = "辐射计东西总电流ToolStripMenuItem";
            this.辐射计东西总电流ToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.辐射计东西总电流ToolStripMenuItem.Text = "辐射计东西总电流";
            this.辐射计东西总电流ToolStripMenuItem.Click += new System.EventHandler(this.辐射计东西总电流ToolStripMenuItem_Click);
            // 
            // 辐射计南北总电流ToolStripMenuItem
            // 
            this.辐射计南北总电流ToolStripMenuItem.Name = "辐射计南北总电流ToolStripMenuItem";
            this.辐射计南北总电流ToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.辐射计南北总电流ToolStripMenuItem.Text = "辐射计南北总电流";
            this.辐射计南北总电流ToolStripMenuItem.Click += new System.EventHandler(this.辐射计南北总电流ToolStripMenuItem_Click);
            // 
            // 电源ToolStripMenuItem
            // 
            this.电源ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.母线28V电压ToolStripMenuItem,
            this.负载28V电流ToolStripMenuItem,
            this.母线42V电压ToolStripMenuItem,
            this.负载42V电流ToolStripMenuItem1,
            this.a组电池电压1ToolStripMenuItem,
            this.b组电池电压1ToolStripMenuItem});
            this.电源ToolStripMenuItem.Name = "电源ToolStripMenuItem";
            this.电源ToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.电源ToolStripMenuItem.Text = "电源";
            // 
            // 母线28V电压ToolStripMenuItem
            // 
            this.母线28V电压ToolStripMenuItem.Name = "母线28V电压ToolStripMenuItem";
            this.母线28V电压ToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.母线28V电压ToolStripMenuItem.Text = "28V母线电压";
            this.母线28V电压ToolStripMenuItem.Click += new System.EventHandler(this.母线28V电压ToolStripMenuItem_Click);
            // 
            // 负载28V电流ToolStripMenuItem
            // 
            this.负载28V电流ToolStripMenuItem.Name = "负载28V电流ToolStripMenuItem";
            this.负载28V电流ToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.负载28V电流ToolStripMenuItem.Text = "28V负载电流";
            this.负载28V电流ToolStripMenuItem.Click += new System.EventHandler(this.负载28V电流ToolStripMenuItem_Click);
            // 
            // 母线42V电压ToolStripMenuItem
            // 
            this.母线42V电压ToolStripMenuItem.Name = "母线42V电压ToolStripMenuItem";
            this.母线42V电压ToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.母线42V电压ToolStripMenuItem.Text = "42V母线电压";
            this.母线42V电压ToolStripMenuItem.Click += new System.EventHandler(this.母线42V电压ToolStripMenuItem_Click);
            // 
            // 负载42V电流ToolStripMenuItem1
            // 
            this.负载42V电流ToolStripMenuItem1.Name = "负载42V电流ToolStripMenuItem1";
            this.负载42V电流ToolStripMenuItem1.Size = new System.Drawing.Size(153, 22);
            this.负载42V电流ToolStripMenuItem1.Text = "42V负载电流";
            this.负载42V电流ToolStripMenuItem1.Click += new System.EventHandler(this.负载42V电流ToolStripMenuItem1_Click);
            // 
            // a组电池电压1ToolStripMenuItem
            // 
            this.a组电池电压1ToolStripMenuItem.Name = "a组电池电压1ToolStripMenuItem";
            this.a组电池电压1ToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.a组电池电压1ToolStripMenuItem.Text = "A组电池电压1";
            this.a组电池电压1ToolStripMenuItem.Click += new System.EventHandler(this.a组电池电压1ToolStripMenuItem_Click);
            // 
            // b组电池电压1ToolStripMenuItem
            // 
            this.b组电池电压1ToolStripMenuItem.Name = "b组电池电压1ToolStripMenuItem";
            this.b组电池电压1ToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.b组电池电压1ToolStripMenuItem.Text = "B组电池电压1";
            this.b组电池电压1ToolStripMenuItem.Click += new System.EventHandler(this.b组电池电压1ToolStripMenuItem_Click);
            // 
            // 趋势监测ToolStripMenuItem
            // 
            this.趋势监测ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.选择时段监测ToolStripMenuItem});
            this.趋势监测ToolStripMenuItem.Name = "趋势监测ToolStripMenuItem";
            this.趋势监测ToolStripMenuItem.Size = new System.Drawing.Size(97, 20);
            this.趋势监测ToolStripMenuItem.Text = "历史数据查看";
            // 
            // 选择时段监测ToolStripMenuItem
            // 
            this.选择时段监测ToolStripMenuItem.Name = "选择时段监测ToolStripMenuItem";
            this.选择时段监测ToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.选择时段监测ToolStripMenuItem.Text = "选择时段";
            this.选择时段监测ToolStripMenuItem.Click += new System.EventHandler(this.选择时段监测ToolStripMenuItem_Click);
            // 
            // 算法ToolStripMenuItem
            // 
            this.算法ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.故障监测算法ToolStripMenuItem,
            this.异常状态信息导出ToolStripMenuItem});
            this.算法ToolStripMenuItem.Name = "算法ToolStripMenuItem";
            this.算法ToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.算法ToolStripMenuItem.Text = "故障诊断";
            // 
            // 故障监测算法ToolStripMenuItem
            // 
            this.故障监测算法ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pLSLRToolStripMenuItem,
            this.pLSRToolStripMenuItem,
            this.pCAToolStripMenuItem});
            this.故障监测算法ToolStripMenuItem.Name = "故障监测算法ToolStripMenuItem";
            this.故障监测算法ToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.故障监测算法ToolStripMenuItem.Text = "故障监测算法";
            // 
            // pLSLRToolStripMenuItem
            // 
            this.pLSLRToolStripMenuItem.Name = "pLSLRToolStripMenuItem";
            this.pLSLRToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.pLSLRToolStripMenuItem.Text = "PLSLR";
            // 
            // pLSRToolStripMenuItem
            // 
            this.pLSRToolStripMenuItem.Name = "pLSRToolStripMenuItem";
            this.pLSRToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.pLSRToolStripMenuItem.Text = "PLSR";
            // 
            // pCAToolStripMenuItem
            // 
            this.pCAToolStripMenuItem.Name = "pCAToolStripMenuItem";
            this.pCAToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.pCAToolStripMenuItem.Text = "PCA";
            // 
            // 异常状态信息导出ToolStripMenuItem
            // 
            this.异常状态信息导出ToolStripMenuItem.Name = "异常状态信息导出ToolStripMenuItem";
            this.异常状态信息导出ToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.异常状态信息导出ToolStripMenuItem.Text = "异常状态信息导出";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.故障诊断结果文件ToolStripMenuItem});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // 故障诊断结果文件ToolStripMenuItem
            // 
            this.故障诊断结果文件ToolStripMenuItem.Name = "故障诊断结果文件ToolStripMenuItem";
            this.故障诊断结果文件ToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.故障诊断结果文件ToolStripMenuItem.Text = "故障诊断结果文件";
            this.故障诊断结果文件ToolStripMenuItem.Click += new System.EventHandler(this.故障诊断结果文件ToolStripMenuItem_Click);
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.软件介绍ToolStripMenuItem});
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.帮助ToolStripMenuItem.Text = "帮助";
            // 
            // 软件介绍ToolStripMenuItem
            // 
            this.软件介绍ToolStripMenuItem.Name = "软件介绍ToolStripMenuItem";
            this.软件介绍ToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.软件介绍ToolStripMenuItem.Text = "软件介绍";
            this.软件介绍ToolStripMenuItem.Click += new System.EventHandler(this.软件介绍ToolStripMenuItem_Click);
            // 
            // chart1
            // 
            this.chart1.BackColor = System.Drawing.Color.Transparent;
            chartArea1.AxisX.TextOrientation = System.Windows.Forms.DataVisualization.Charting.TextOrientation.Horizontal;
            chartArea1.AxisX.Title = "guzhzhzh";
            chartArea1.AxisX.TitleForeColor = System.Drawing.Color.Blue;
            chartArea1.AxisY.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.AxisY.TitleForeColor = System.Drawing.Color.DodgerBlue;
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Alignment = System.Drawing.StringAlignment.Far;
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend1.ForeColor = System.Drawing.Color.DimGray;
            legend1.MaximumAutoSize = 20F;
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(11, 135);
            this.chart1.Margin = new System.Windows.Forms.Padding(2);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series_yuzhi";
            this.chart1.Series.Add(series1);
            this.chart1.Series.Add(series2);
            this.chart1.Size = new System.Drawing.Size(809, 274);
            this.chart1.TabIndex = 1;
            this.chart1.Text = "chart1";
            this.chart1.SelectionRangeChanged += new System.EventHandler<System.Windows.Forms.DataVisualization.Charting.CursorEventArgs>(this.chart1_SelectionRangeChanged);
            this.chart1.AxisScrollBarClicked += new System.EventHandler<System.Windows.Forms.DataVisualization.Charting.ScrollBarEventArgs>(this.chart1_AxisScrollBarClicked);
            this.chart1.Click += new System.EventHandler(this.chart1_Click);
            // 
            // abn_state_lstbox
            // 
            this.abn_state_lstbox.Font = new System.Drawing.Font("SimSun", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.abn_state_lstbox.ForeColor = System.Drawing.Color.Red;
            this.abn_state_lstbox.FormattingEnabled = true;
            this.abn_state_lstbox.ItemHeight = 22;
            this.abn_state_lstbox.Location = new System.Drawing.Point(1012, 257);
            this.abn_state_lstbox.Margin = new System.Windows.Forms.Padding(2);
            this.abn_state_lstbox.Name = "abn_state_lstbox";
            this.abn_state_lstbox.Size = new System.Drawing.Size(378, 70);
            this.abn_state_lstbox.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("SimSun", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(578, 102);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 30);
            this.label2.TabIndex = 6;
            this.label2.Text = "时间：";
            // 
            // btn_abnstate_cle
            // 
            this.btn_abnstate_cle.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btn_abnstate_cle.FlatAppearance.BorderColor = System.Drawing.Color.Aqua;
            this.btn_abnstate_cle.Font = new System.Drawing.Font("SimSun", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_abnstate_cle.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_abnstate_cle.Location = new System.Drawing.Point(1155, 186);
            this.btn_abnstate_cle.Margin = new System.Windows.Forms.Padding(2);
            this.btn_abnstate_cle.Name = "btn_abnstate_cle";
            this.btn_abnstate_cle.Size = new System.Drawing.Size(117, 58);
            this.btn_abnstate_cle.TabIndex = 12;
            this.btn_abnstate_cle.Text = "清除显示";
            this.btn_abnstate_cle.UseVisualStyleBackColor = false;
            this.btn_abnstate_cle.Click += new System.EventHandler(this.btn_abnstate_cle_Click);
            // 
            // btn_save
            // 
            this.btn_save.BackColor = System.Drawing.SystemColors.Info;
            this.btn_save.Font = new System.Drawing.Font("SimSun", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_save.Location = new System.Drawing.Point(1012, 186);
            this.btn_save.Margin = new System.Windows.Forms.Padding(2);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(118, 58);
            this.btn_save.TabIndex = 13;
            this.btn_save.Text = "保存信息";
            this.btn_save.UseVisualStyleBackColor = false;
            this.btn_save.Click += new System.EventHandler(this.button4_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 200;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // comboBox1
            // 
            this.comboBox1.ForeColor = System.Drawing.Color.Gold;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(50, 156);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(100, 20);
            this.comboBox1.TabIndex = 26;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("SimSun", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(54, 49);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(199, 30);
            this.label3.TabIndex = 24;
            this.label3.Text = "动量轮状态：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("SimSun", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(349, 49);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(168, 30);
            this.label5.TabIndex = 28;
            this.label5.Text = "电源状态：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("SimSun", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(611, 49);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(199, 30);
            this.label6.TabIndex = 30;
            this.label6.Text = "探测仪状态：";
            // 
            // timer_ST
            // 
            this.timer_ST.Interval = 1;
            this.timer_ST.Tick += new System.EventHandler(this.timer_ST_Tick);
            // 
            // dll_rb
            // 
            this.dll_rb.AutoSize = true;
            this.dll_rb.Checked = true;
            this.dll_rb.Font = new System.Drawing.Font("SimHei", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dll_rb.ForeColor = System.Drawing.Color.Navy;
            this.dll_rb.Location = new System.Drawing.Point(58, 98);
            this.dll_rb.Margin = new System.Windows.Forms.Padding(2);
            this.dll_rb.Name = "dll_rb";
            this.dll_rb.Size = new System.Drawing.Size(124, 34);
            this.dll_rb.TabIndex = 33;
            this.dll_rb.TabStop = true;
            this.dll_rb.Text = "动量轮";
            this.dll_rb.UseVisualStyleBackColor = true;
            this.dll_rb.CheckedChanged += new System.EventHandler(this.dll_rb_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("SimSun", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(905, 49);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(199, 30);
            this.label7.TabIndex = 34;
            this.label7.Text = "辐射计状态：";
            // 
            // dy_rb
            // 
            this.dy_rb.AutoSize = true;
            this.dy_rb.Font = new System.Drawing.Font("SimHei", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dy_rb.ForeColor = System.Drawing.Color.Navy;
            this.dy_rb.Location = new System.Drawing.Point(194, 98);
            this.dy_rb.Margin = new System.Windows.Forms.Padding(2);
            this.dy_rb.Name = "dy_rb";
            this.dy_rb.Size = new System.Drawing.Size(93, 34);
            this.dy_rb.TabIndex = 38;
            this.dy_rb.Text = "电源";
            this.dy_rb.UseVisualStyleBackColor = true;
            // 
            // tcy_rb
            // 
            this.tcy_rb.AutoSize = true;
            this.tcy_rb.Font = new System.Drawing.Font("SimHei", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tcy_rb.ForeColor = System.Drawing.Color.Navy;
            this.tcy_rb.Location = new System.Drawing.Point(298, 98);
            this.tcy_rb.Margin = new System.Windows.Forms.Padding(2);
            this.tcy_rb.Name = "tcy_rb";
            this.tcy_rb.Size = new System.Drawing.Size(124, 34);
            this.tcy_rb.TabIndex = 39;
            this.tcy_rb.Text = "探测仪";
            this.tcy_rb.UseVisualStyleBackColor = true;
            // 
            // fsj_rb
            // 
            this.fsj_rb.AutoSize = true;
            this.fsj_rb.Font = new System.Drawing.Font("SimHei", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fsj_rb.ForeColor = System.Drawing.Color.Navy;
            this.fsj_rb.Location = new System.Drawing.Point(438, 98);
            this.fsj_rb.Margin = new System.Windows.Forms.Padding(2);
            this.fsj_rb.Name = "fsj_rb";
            this.fsj_rb.Size = new System.Drawing.Size(124, 34);
            this.fsj_rb.TabIndex = 40;
            this.fsj_rb.Text = "辐射计";
            this.fsj_rb.UseVisualStyleBackColor = true;
            this.fsj_rb.CheckedChanged += new System.EventHandler(this.radioButton4_CheckedChanged);
            // 
            // label_dll
            // 
            this.label_dll.AutoSize = true;
            this.label_dll.Font = new System.Drawing.Font("SimSun", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_dll.ForeColor = System.Drawing.Color.Lime;
            this.label_dll.Location = new System.Drawing.Point(220, 49);
            this.label_dll.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_dll.Name = "label_dll";
            this.label_dll.Size = new System.Drawing.Size(75, 30);
            this.label_dll.TabIndex = 41;
            this.label_dll.Text = "正常";
            // 
            // label_dy
            // 
            this.label_dy.AutoSize = true;
            this.label_dy.Font = new System.Drawing.Font("SimSun", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_dy.ForeColor = System.Drawing.Color.Lime;
            this.label_dy.Location = new System.Drawing.Point(491, 49);
            this.label_dy.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_dy.Name = "label_dy";
            this.label_dy.Size = new System.Drawing.Size(75, 30);
            this.label_dy.TabIndex = 42;
            this.label_dy.Text = "正常";
            // 
            // label_tcy
            // 
            this.label_tcy.AutoSize = true;
            this.label_tcy.Font = new System.Drawing.Font("SimSun", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_tcy.ForeColor = System.Drawing.Color.Lime;
            this.label_tcy.Location = new System.Drawing.Point(775, 49);
            this.label_tcy.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_tcy.Name = "label_tcy";
            this.label_tcy.Size = new System.Drawing.Size(75, 30);
            this.label_tcy.TabIndex = 43;
            this.label_tcy.Text = "正常";
            // 
            // label_fsj
            // 
            this.label_fsj.AutoSize = true;
            this.label_fsj.Font = new System.Drawing.Font("SimSun", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_fsj.ForeColor = System.Drawing.Color.Lime;
            this.label_fsj.Location = new System.Drawing.Point(1068, 49);
            this.label_fsj.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_fsj.Name = "label_fsj";
            this.label_fsj.Size = new System.Drawing.Size(75, 30);
            this.label_fsj.TabIndex = 44;
            this.label_fsj.Text = "正常";
            // 
            // label_shijian
            // 
            this.label_shijian.AutoSize = true;
            this.label_shijian.Font = new System.Drawing.Font("SimSun", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_shijian.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label_shijian.Location = new System.Drawing.Point(676, 102);
            this.label_shijian.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_shijian.Name = "label_shijian";
            this.label_shijian.Size = new System.Drawing.Size(137, 30);
            this.label_shijian.TabIndex = 46;
            this.label_shijian.Text = "实时时间";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.Highlight;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button1.Font = new System.Drawing.Font("SimSun", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(1248, 49);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(145, 58);
            this.button1.TabIndex = 47;
            this.button1.Text = "返回主界面";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // chart2
            // 
            this.chart2.BackColor = System.Drawing.Color.Transparent;
            chartArea2.AxisX.TextOrientation = System.Windows.Forms.DataVisualization.Charting.TextOrientation.Horizontal;
            chartArea2.AxisX.Title = "guzhzhzh";
            chartArea2.AxisX.TitleForeColor = System.Drawing.Color.Blue;
            chartArea2.AxisY.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea2.AxisY.TitleForeColor = System.Drawing.Color.DodgerBlue;
            chartArea2.Name = "ChartArea1";
            this.chart2.ChartAreas.Add(chartArea2);
            legend2.Alignment = System.Drawing.StringAlignment.Far;
            legend2.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend2.ForeColor = System.Drawing.Color.DimGray;
            legend2.MaximumAutoSize = 20F;
            legend2.Name = "Legend1";
            this.chart2.Legends.Add(legend2);
            this.chart2.Location = new System.Drawing.Point(59, 452);
            this.chart2.Margin = new System.Windows.Forms.Padding(2);
            this.chart2.Name = "chart2";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            series4.ChartArea = "ChartArea1";
            series4.Legend = "Legend1";
            series4.Name = "Series_yuzhi";
            this.chart2.Series.Add(series3);
            this.chart2.Series.Add(series4);
            this.chart2.Size = new System.Drawing.Size(791, 274);
            this.chart2.TabIndex = 1;
            this.chart2.Text = "chart2";
            // 
            // comboBox2
            // 
            this.comboBox2.ForeColor = System.Drawing.Color.Gold;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(58, 468);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(100, 20);
            this.comboBox2.TabIndex = 49;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lavender;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(1443, 846);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.chart2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label_shijian);
            this.Controls.Add(this.label_fsj);
            this.Controls.Add(this.label_tcy);
            this.Controls.Add(this.label_dy);
            this.Controls.Add(this.label_dll);
            this.Controls.Add(this.fsj_rb);
            this.Controls.Add(this.tcy_rb);
            this.Controls.Add(this.dy_rb);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dll_rb);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.btn_abnstate_cle);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.abn_state_lstbox);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SatelliteSoftware";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 通讯ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 帮助ToolStripMenuItem;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.ListBox abn_state_lstbox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_abnstate_cle;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.ToolStripMenuItem 参数配置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 软件介绍ToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem 开始ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 开启ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 算法ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 故障监测算法ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pLSLRToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pLSRToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pCAToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 载荷扫描镜ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 电源ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 管理员登录ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 异常状态信息导出ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 修改密码ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 故障诊断结果文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 趋势监测ToolStripMenuItem;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ToolStripMenuItem 选择时段监测ToolStripMenuItem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Timer timer_ST;
        private System.Windows.Forms.ToolStripMenuItem 探测仪东西电机总电流ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 探测仪东西电机A相电流ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 探测仪东西电机B相电流ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 母线28V电压ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 负载28V电流ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 母线42V电压ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 负载42V电流ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem a组电池电压1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem b组电池电压1ToolStripMenuItem;
        private System.Windows.Forms.RadioButton dll_rb;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RadioButton dy_rb;
        private System.Windows.Forms.RadioButton tcy_rb;
        private System.Windows.Forms.RadioButton fsj_rb;
        private System.Windows.Forms.Label label_dll;
        private System.Windows.Forms.Label label_dy;
        private System.Windows.Forms.Label label_tcy;
        private System.Windows.Forms.Label label_fsj;
        private System.Windows.Forms.Label label_shijian;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripMenuItem 辐射计ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 辐射计东西总电流ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 辐射计南北总电流ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 探测仪南北电机A相电流ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 探测仪南北电机B相电流ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 探测仪南北电机总电流ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 动量轮1马达电流ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 动量轮2马达电流ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 动量轮4马达电流ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 动量轮5马达电流ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 动量轮1轴温ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 动量轮2轴温ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 动量轮3轴温ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 动量轮4轴温ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 动量轮5轴温ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 动量轮6轴温ToolStripMenuItem;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart2;
        private System.Windows.Forms.ComboBox comboBox2;
    }
}

