namespace SatelliteSoftwareIF
{
    partial class RealTimeDisplay
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
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series8 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.DllState = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.DllTime = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.chartTimer = new System.Windows.Forms.Timer(this.components);
            this.chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.DyTime = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.DyState = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.FsjTime = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.FsjState = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.chart3 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.TcyTime = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.TcyState = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.chart4 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.DyTimer = new System.Windows.Forms.Timer(this.components);
            this.FsjTimer = new System.Windows.Forms.Timer(this.components);
            this.TcyTimer = new System.Windows.Forms.Timer(this.components);
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnFsjFaultLog = new System.Windows.Forms.Button();
            this.btnTcyFaultLog = new System.Windows.Forms.Button();
            this.btnDyFaultLog = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnDllFaultLog = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // DllState
            // 
            this.DllState.AutoSize = true;
            this.DllState.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DllState.Font = new System.Drawing.Font("SimSun", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.DllState.ForeColor = System.Drawing.Color.Green;
            this.DllState.Location = new System.Drawing.Point(98, 18);
            this.DllState.Margin = new System.Windows.Forms.Padding(2, 18, 2, 5);
            this.DllState.Name = "DllState";
            this.DllState.Size = new System.Drawing.Size(232, 13);
            this.DllState.TabIndex = 43;
            this.DllState.Text = "正常";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("SimSun", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(2, 18);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 18, 2, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 42;
            this.label3.Text = "动量轮状态：";
            // 
            // DllTime
            // 
            this.DllTime.AutoSize = true;
            this.DllTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DllTime.Font = new System.Drawing.Font("SimSun", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.DllTime.ForeColor = System.Drawing.Color.RoyalBlue;
            this.DllTime.Location = new System.Drawing.Point(98, 51);
            this.DllTime.Margin = new System.Windows.Forms.Padding(2, 15, 2, 5);
            this.DllTime.Name = "DllTime";
            this.DllTime.Size = new System.Drawing.Size(232, 16);
            this.DllTime.TabIndex = 48;
            this.DllTime.Text = "实时时间";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("SimSun", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(2, 51);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 15, 2, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 16);
            this.label2.TabIndex = 47;
            this.label2.Text = "时间：";
            // 
            // chartTimer
            // 
            this.chartTimer.Enabled = true;
            this.chartTimer.Interval = 1500;
            this.chartTimer.Tick += new System.EventHandler(this.chartTimer_Tick);
            // 
            // chart2
            // 
            this.chart2.BackColor = System.Drawing.Color.LightBlue;
            this.chart2.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
            chartArea1.AxisX.LineWidth = 2;
            chartArea1.AxisX.TextOrientation = System.Windows.Forms.DataVisualization.Charting.TextOrientation.Horizontal;
            chartArea1.AxisX.Title = "guzhzhzh";
            chartArea1.AxisX.TitleForeColor = System.Drawing.Color.Blue;
            chartArea1.AxisY.IsInterlaced = true;
            chartArea1.AxisY.LineWidth = 2;
            chartArea1.AxisY.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.AxisY.TitleForeColor = System.Drawing.Color.DodgerBlue;
            chartArea1.BackColor = System.Drawing.Color.LightSkyBlue;
            chartArea1.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
            chartArea1.BackImageAlignment = System.Windows.Forms.DataVisualization.Charting.ChartImageAlignmentStyle.Center;
            chartArea1.CursorY.IsUserEnabled = true;
            chartArea1.Name = "ChartArea1";
            this.chart2.ChartAreas.Add(chartArea1);
            this.tableLayoutPanel1.SetColumnSpan(this.chart2, 3);
            this.chart2.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.chart2.Dock = System.Windows.Forms.DockStyle.Fill;
            legend4.Alignment = System.Drawing.StringAlignment.Far;
            legend4.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend4.ForeColor = System.Drawing.Color.DimGray;
            legend4.MaximumAutoSize = 20F;
            legend4.Name = "Legend1";
            this.chart2.Legends.Add(legend4);
            this.chart2.Location = new System.Drawing.Point(506, 74);
            this.chart2.Margin = new System.Windows.Forms.Padding(2);
            this.chart2.Name = "chart2";
            this.chart2.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright;
            series7.ChartArea = "ChartArea1";
            series7.IsValueShownAsLabel = true;
            series7.Legend = "Legend1";
            series7.Name = "Series1";
            series8.ChartArea = "ChartArea1";
            series8.Legend = "Legend1";
            series8.Name = "Series_yuzhi";
            this.chart2.Series.Add(series7);
            this.chart2.Series.Add(series8);
            this.chart2.Size = new System.Drawing.Size(500, 285);
            this.chart2.TabIndex = 49;
            this.chart2.Text = "chart2";
            // 
            // DyTime
            // 
            this.DyTime.AutoSize = true;
            this.DyTime.Font = new System.Drawing.Font("SimSun", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.DyTime.ForeColor = System.Drawing.Color.RoyalBlue;
            this.DyTime.Location = new System.Drawing.Point(602, 51);
            this.DyTime.Margin = new System.Windows.Forms.Padding(2, 15, 2, 5);
            this.DyTime.Name = "DyTime";
            this.DyTime.Size = new System.Drawing.Size(98, 16);
            this.DyTime.TabIndex = 53;
            this.DyTime.Text = "实时时间";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("SimSun", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(506, 51);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 15, 2, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 16);
            this.label4.TabIndex = 52;
            this.label4.Text = "时间：";
            // 
            // DyState
            // 
            this.DyState.AutoSize = true;
            this.DyState.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DyState.Font = new System.Drawing.Font("SimSun", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.DyState.ForeColor = System.Drawing.Color.Green;
            this.DyState.Location = new System.Drawing.Point(602, 18);
            this.DyState.Margin = new System.Windows.Forms.Padding(2, 18, 2, 5);
            this.DyState.Name = "DyState";
            this.DyState.Size = new System.Drawing.Size(232, 13);
            this.DyState.TabIndex = 51;
            this.DyState.Text = "正常";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("SimSun", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(506, 18);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 18, 2, 5);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(92, 13);
            this.label6.TabIndex = 50;
            this.label6.Text = "电源状态：";
            // 
            // FsjTime
            // 
            this.FsjTime.AutoSize = true;
            this.FsjTime.Font = new System.Drawing.Font("SimSun", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FsjTime.ForeColor = System.Drawing.Color.RoyalBlue;
            this.FsjTime.Location = new System.Drawing.Point(98, 417);
            this.FsjTime.Margin = new System.Windows.Forms.Padding(2, 15, 2, 5);
            this.FsjTime.Name = "FsjTime";
            this.FsjTime.Size = new System.Drawing.Size(98, 16);
            this.FsjTime.TabIndex = 58;
            this.FsjTime.Text = "实时时间";
            this.FsjTime.Click += new System.EventHandler(this.label7_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("SimSun", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(2, 417);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 15, 2, 5);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(76, 16);
            this.label8.TabIndex = 57;
            this.label8.Text = "时间：";
            // 
            // FsjState
            // 
            this.FsjState.AutoSize = true;
            this.FsjState.Font = new System.Drawing.Font("SimSun", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FsjState.ForeColor = System.Drawing.Color.Green;
            this.FsjState.Location = new System.Drawing.Point(98, 384);
            this.FsjState.Margin = new System.Windows.Forms.Padding(2, 18, 2, 5);
            this.FsjState.Name = "FsjState";
            this.FsjState.Size = new System.Drawing.Size(52, 13);
            this.FsjState.TabIndex = 56;
            this.FsjState.Text = "正常";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("SimSun", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(2, 384);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 18, 2, 5);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(76, 13);
            this.label10.TabIndex = 55;
            this.label10.Text = "辐射计状态：";
            // 
            // chart3
            // 
            this.chart3.BackColor = System.Drawing.Color.LightBlue;
            this.chart3.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
            chartArea4.AxisX.LineWidth = 2;
            chartArea4.AxisX.TextOrientation = System.Windows.Forms.DataVisualization.Charting.TextOrientation.Horizontal;
            chartArea4.AxisX.Title = "guzhzhzh";
            chartArea4.AxisX.TitleForeColor = System.Drawing.Color.Blue;
            chartArea4.AxisY.IsInterlaced = true;
            chartArea4.AxisY.LineWidth = 2;
            chartArea4.AxisY.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea4.AxisY.TitleForeColor = System.Drawing.Color.DodgerBlue;
            chartArea4.BackColor = System.Drawing.Color.LightSkyBlue;
            chartArea4.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
            chartArea4.BackImageAlignment = System.Windows.Forms.DataVisualization.Charting.ChartImageAlignmentStyle.Center;
            chartArea4.CursorY.IsUserEnabled = true;
            chartArea4.Name = "ChartArea1";
            this.chart3.ChartAreas.Add(chartArea4);
            this.tableLayoutPanel1.SetColumnSpan(this.chart3, 3);
            this.chart3.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.chart3.Dock = System.Windows.Forms.DockStyle.Fill;
            legend3.Alignment = System.Drawing.StringAlignment.Far;
            legend3.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend3.ForeColor = System.Drawing.Color.DimGray;
            legend3.MaximumAutoSize = 20F;
            legend3.Name = "Legend1";
            this.chart3.Legends.Add(legend3);
            this.chart3.Location = new System.Drawing.Point(2, 440);
            this.chart3.Margin = new System.Windows.Forms.Padding(2);
            this.chart3.Name = "chart3";
            this.chart3.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright;
            series5.ChartArea = "ChartArea1";
            series5.IsValueShownAsLabel = true;
            series5.Legend = "Legend1";
            series5.Name = "Series1";
            series6.ChartArea = "ChartArea1";
            series6.Legend = "Legend1";
            series6.Name = "Series_yuzhi";
            this.chart3.Series.Add(series5);
            this.chart3.Series.Add(series6);
            this.chart3.Size = new System.Drawing.Size(496, 287);
            this.chart3.TabIndex = 54;
            this.chart3.Text = "chart3";
            // 
            // TcyTime
            // 
            this.TcyTime.AutoSize = true;
            this.TcyTime.Font = new System.Drawing.Font("SimSun", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TcyTime.ForeColor = System.Drawing.Color.RoyalBlue;
            this.TcyTime.Location = new System.Drawing.Point(602, 417);
            this.TcyTime.Margin = new System.Windows.Forms.Padding(2, 15, 2, 5);
            this.TcyTime.Name = "TcyTime";
            this.TcyTime.Size = new System.Drawing.Size(98, 16);
            this.TcyTime.TabIndex = 63;
            this.TcyTime.Text = "实时时间";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Font = new System.Drawing.Font("SimSun", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.Location = new System.Drawing.Point(506, 417);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 15, 2, 5);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(92, 16);
            this.label12.TabIndex = 62;
            this.label12.Text = "时间：";
            // 
            // TcyState
            // 
            this.TcyState.AutoSize = true;
            this.TcyState.Font = new System.Drawing.Font("SimSun", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TcyState.ForeColor = System.Drawing.Color.Green;
            this.TcyState.Location = new System.Drawing.Point(602, 384);
            this.TcyState.Margin = new System.Windows.Forms.Padding(2, 18, 2, 5);
            this.TcyState.Name = "TcyState";
            this.TcyState.Size = new System.Drawing.Size(52, 13);
            this.TcyState.TabIndex = 61;
            this.TcyState.Text = "正常";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label14.Font = new System.Drawing.Font("SimSun", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.Location = new System.Drawing.Point(506, 384);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 18, 2, 5);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(92, 13);
            this.label14.TabIndex = 60;
            this.label14.Text = "探测仪状态：";
            // 
            // chart4
            // 
            this.chart4.BackColor = System.Drawing.Color.LightBlue;
            this.chart4.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
            chartArea3.AxisX.LineWidth = 2;
            chartArea3.AxisX.TextOrientation = System.Windows.Forms.DataVisualization.Charting.TextOrientation.Horizontal;
            chartArea3.AxisX.Title = "guzhzhzh";
            chartArea3.AxisX.TitleForeColor = System.Drawing.Color.Blue;
            chartArea3.AxisY.IsInterlaced = true;
            chartArea3.AxisY.LineWidth = 2;
            chartArea3.AxisY.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea3.AxisY.TitleForeColor = System.Drawing.Color.DodgerBlue;
            chartArea3.BackColor = System.Drawing.Color.LightSkyBlue;
            chartArea3.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
            chartArea3.BackImageAlignment = System.Windows.Forms.DataVisualization.Charting.ChartImageAlignmentStyle.Center;
            chartArea3.CursorY.IsUserEnabled = true;
            chartArea3.Name = "ChartArea1";
            this.chart4.ChartAreas.Add(chartArea3);
            this.tableLayoutPanel1.SetColumnSpan(this.chart4, 3);
            this.chart4.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.chart4.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Alignment = System.Drawing.StringAlignment.Far;
            legend2.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend2.ForeColor = System.Drawing.Color.DimGray;
            legend2.MaximumAutoSize = 20F;
            legend2.Name = "Legend1";
            this.chart4.Legends.Add(legend2);
            this.chart4.Location = new System.Drawing.Point(506, 440);
            this.chart4.Margin = new System.Windows.Forms.Padding(2);
            this.chart4.Name = "chart4";
            this.chart4.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright;
            series3.ChartArea = "ChartArea1";
            series3.IsValueShownAsLabel = true;
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            series4.ChartArea = "ChartArea1";
            series4.Legend = "Legend1";
            series4.Name = "Series_yuzhi";
            this.chart4.Series.Add(series3);
            this.chart4.Series.Add(series4);
            this.chart4.Size = new System.Drawing.Size(500, 287);
            this.chart4.TabIndex = 59;
            this.chart4.Text = "chart4";
            // 
            // DyTimer
            // 
            this.DyTimer.Enabled = true;
            this.DyTimer.Interval = 1500;
            // 
            // FsjTimer
            // 
            this.FsjTimer.Enabled = true;
            this.FsjTimer.Interval = 1500;
            // 
            // TcyTimer
            // 
            this.TcyTimer.Enabled = true;
            this.TcyTimer.Interval = 1500;
            // 
            // chart1
            // 
            this.chart1.BackColor = System.Drawing.Color.LightBlue;
            this.chart1.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
            chartArea2.AxisX.LineWidth = 2;
            chartArea2.AxisX.TextOrientation = System.Windows.Forms.DataVisualization.Charting.TextOrientation.Horizontal;
            chartArea2.AxisX.Title = "guzhzhzh";
            chartArea2.AxisX.TitleForeColor = System.Drawing.Color.Blue;
            chartArea2.AxisY.IsInterlaced = true;
            chartArea2.AxisY.LineWidth = 2;
            chartArea2.AxisY.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea2.AxisY.TitleForeColor = System.Drawing.Color.DodgerBlue;
            chartArea2.BackColor = System.Drawing.Color.LightSkyBlue;
            chartArea2.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
            chartArea2.BackImageAlignment = System.Windows.Forms.DataVisualization.Charting.ChartImageAlignmentStyle.Center;
            chartArea2.CursorY.IsUserEnabled = true;
            chartArea2.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea2);
            this.tableLayoutPanel1.SetColumnSpan(this.chart1, 3);
            this.chart1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Alignment = System.Drawing.StringAlignment.Far;
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend1.ForeColor = System.Drawing.Color.DimGray;
            legend1.MaximumAutoSize = 20F;
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(2, 74);
            this.chart1.Margin = new System.Windows.Forms.Padding(2);
            this.chart1.Name = "chart1";
            this.chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright;
            series1.ChartArea = "ChartArea1";
            series1.IsValueShownAsLabel = true;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series_yuzhi";
            this.chart1.Series.Add(series1);
            this.chart1.Series.Add(series2);
            this.chart1.Size = new System.Drawing.Size(496, 285);
            this.chart1.TabIndex = 64;
            this.chart1.Text = "chart1";
            // 
            // btnFsjFaultLog
            // 
            this.btnFsjFaultLog.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnFsjFaultLog.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnFsjFaultLog.Font = new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnFsjFaultLog.Location = new System.Drawing.Point(367, 401);
            this.btnFsjFaultLog.Margin = new System.Windows.Forms.Padding(35, 35, 0, 5);
            this.btnFsjFaultLog.Name = "btnFsjFaultLog";
            this.tableLayoutPanel1.SetRowSpan(this.btnFsjFaultLog, 2);
            this.btnFsjFaultLog.Size = new System.Drawing.Size(133, 30);
            this.btnFsjFaultLog.TabIndex = 66;
            this.btnFsjFaultLog.Text = "查看故障日志";
            this.btnFsjFaultLog.UseVisualStyleBackColor = false;
            this.btnFsjFaultLog.Click += new System.EventHandler(this.btnFsjFaultLog_Click);
            // 
            // btnTcyFaultLog
            // 
            this.btnTcyFaultLog.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnTcyFaultLog.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnTcyFaultLog.Font = new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnTcyFaultLog.Location = new System.Drawing.Point(871, 401);
            this.btnTcyFaultLog.Margin = new System.Windows.Forms.Padding(35, 35, 0, 5);
            this.btnTcyFaultLog.Name = "btnTcyFaultLog";
            this.btnTcyFaultLog.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tableLayoutPanel1.SetRowSpan(this.btnTcyFaultLog, 2);
            this.btnTcyFaultLog.Size = new System.Drawing.Size(137, 30);
            this.btnTcyFaultLog.TabIndex = 67;
            this.btnTcyFaultLog.Text = "查看故障日志";
            this.btnTcyFaultLog.UseVisualStyleBackColor = false;
            this.btnTcyFaultLog.Click += new System.EventHandler(this.btnTcyFaultLog_Click);
            // 
            // btnDyFaultLog
            // 
            this.btnDyFaultLog.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnDyFaultLog.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDyFaultLog.Font = new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDyFaultLog.Location = new System.Drawing.Point(871, 35);
            this.btnDyFaultLog.Margin = new System.Windows.Forms.Padding(35, 35, 0, 5);
            this.btnDyFaultLog.Name = "btnDyFaultLog";
            this.tableLayoutPanel1.SetRowSpan(this.btnDyFaultLog, 2);
            this.btnDyFaultLog.Size = new System.Drawing.Size(135, 30);
            this.btnDyFaultLog.TabIndex = 68;
            this.btnDyFaultLog.Text = "查看故障日志";
            this.btnDyFaultLog.UseVisualStyleBackColor = false;
            this.btnDyFaultLog.Click += new System.EventHandler(this.btnDyFaultLog_Click);
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tableLayoutPanel1.SetColumnSpan(this.label1, 3);
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(0, 361);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(500, 5);
            this.label1.TabIndex = 69;
            // 
            // label5
            // 
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(500, 0);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.tableLayoutPanel1.SetRowSpan(this.label5, 7);
            this.label5.Size = new System.Drawing.Size(4, 729);
            this.label5.TabIndex = 70;
            // 
            // btnDllFaultLog
            // 
            this.btnDllFaultLog.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnDllFaultLog.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDllFaultLog.Font = new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDllFaultLog.Location = new System.Drawing.Point(367, 35);
            this.btnDllFaultLog.Margin = new System.Windows.Forms.Padding(35, 35, 0, 5);
            this.btnDllFaultLog.Name = "btnDllFaultLog";
            this.tableLayoutPanel1.SetRowSpan(this.btnDllFaultLog, 2);
            this.btnDllFaultLog.Size = new System.Drawing.Size(133, 32);
            this.btnDllFaultLog.TabIndex = 71;
            this.btnDllFaultLog.Text = "查看故障日志";
            this.btnDllFaultLog.UseVisualStyleBackColor = false;
            this.btnDllFaultLog.Click += new System.EventHandler(this.btnDllFaultLog_Click_1);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoScroll = true;
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 7;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.598712F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.50184F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.67456F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 0.4497741F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.598712F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.50184F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.67456F));
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnTcyFaultLog, 6, 4);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.TcyTime, 5, 5);
            this.tableLayoutPanel1.Controls.Add(this.TcyState, 5, 4);
            this.tableLayoutPanel1.Controls.Add(this.label12, 4, 5);
            this.tableLayoutPanel1.Controls.Add(this.label5, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.chart1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label14, 4, 4);
            this.tableLayoutPanel1.Controls.Add(this.DllState, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnDyFaultLog, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.FsjTime, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.chart4, 4, 6);
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.DllTime, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.FsjState, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.label6, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.label10, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.DyState, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.DyTime, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.chart3, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.label7, 4, 3);
            this.tableLayoutPanel1.Controls.Add(this.chart2, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnDllFaultLog, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnFsjFaultLog, 2, 4);
            this.tableLayoutPanel1.Cursor = System.Windows.Forms.Cursors.Default;
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.964967F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.964968F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 39.71974F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 0.7006413F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.964968F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.964968F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 39.71974F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1008, 729);
            this.tableLayoutPanel1.TabIndex = 72;
            // 
            // label7
            // 
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tableLayoutPanel1.SetColumnSpan(this.label7, 3);
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Location = new System.Drawing.Point(504, 361);
            this.label7.Margin = new System.Windows.Forms.Padding(0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(504, 5);
            this.label7.TabIndex = 72;
            // 
            // RealTimeDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "RealTimeDisplay";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RealTimeDisplay";
            this.Load += new System.EventHandler(this.RealTimeDisplay_Load);
            this.Shown += new System.EventHandler(this.RealTimeDisplay_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label DllState;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label DllTime;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.Timer chartTimer;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart2;
        private System.Windows.Forms.Label DyTime;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label DyState;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label FsjTime;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label FsjState;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart3;
        private System.Windows.Forms.Label TcyTime;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label TcyState;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart4;
        public System.Windows.Forms.Timer DyTimer;
        public System.Windows.Forms.Timer FsjTimer;
        public System.Windows.Forms.Timer TcyTimer;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Button btnFsjFaultLog;
        private System.Windows.Forms.Button btnTcyFaultLog;
        private System.Windows.Forms.Button btnDyFaultLog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnDllFaultLog;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label7;
    }
}