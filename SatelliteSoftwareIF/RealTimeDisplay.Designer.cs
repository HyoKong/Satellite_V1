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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend5 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series9 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series10 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend6 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series11 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series12 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea7 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend7 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series13 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series14 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea8 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend8 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series15 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series16 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
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
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart4)).BeginInit();
            this.SuspendLayout();
            // 
            // chart1
            // 
            this.chart1.BackColor = System.Drawing.Color.LightBlue;
            this.chart1.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
            chartArea5.AxisX.LineWidth = 2;
            chartArea5.AxisX.TextOrientation = System.Windows.Forms.DataVisualization.Charting.TextOrientation.Horizontal;
            chartArea5.AxisX.Title = "guzhzhzh";
            chartArea5.AxisX.TitleForeColor = System.Drawing.Color.Blue;
            chartArea5.AxisY.IsInterlaced = true;
            chartArea5.AxisY.LineWidth = 2;
            chartArea5.AxisY.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea5.AxisY.TitleForeColor = System.Drawing.Color.DodgerBlue;
            chartArea5.BackColor = System.Drawing.Color.LightSkyBlue;
            chartArea5.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
            chartArea5.BackImageAlignment = System.Windows.Forms.DataVisualization.Charting.ChartImageAlignmentStyle.Center;
            chartArea5.CursorY.IsUserEnabled = true;
            chartArea5.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea5);
            this.chart1.Cursor = System.Windows.Forms.Cursors.IBeam;
            legend5.Alignment = System.Drawing.StringAlignment.Far;
            legend5.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend5.ForeColor = System.Drawing.Color.DimGray;
            legend5.MaximumAutoSize = 20F;
            legend5.Name = "Legend1";
            this.chart1.Legends.Add(legend5);
            this.chart1.Location = new System.Drawing.Point(80, 138);
            this.chart1.Margin = new System.Windows.Forms.Padding(2);
            this.chart1.Name = "chart1";
            this.chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright;
            series9.ChartArea = "ChartArea1";
            series9.IsValueShownAsLabel = true;
            series9.Legend = "Legend1";
            series9.Name = "Series1";
            series10.ChartArea = "ChartArea1";
            series10.Legend = "Legend1";
            series10.Name = "Series_yuzhi";
            this.chart1.Series.Add(series9);
            this.chart1.Series.Add(series10);
            this.chart1.Size = new System.Drawing.Size(777, 338);
            this.chart1.TabIndex = 2;
            this.chart1.Text = "chart1";
            this.chart1.Click += new System.EventHandler(this.chart1_Click);
            // 
            // DllState
            // 
            this.DllState.AutoSize = true;
            this.DllState.Font = new System.Drawing.Font("SimSun", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.DllState.ForeColor = System.Drawing.Color.Lime;
            this.DllState.Location = new System.Drawing.Point(271, 87);
            this.DllState.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.DllState.Name = "DllState";
            this.DllState.Size = new System.Drawing.Size(75, 30);
            this.DllState.TabIndex = 43;
            this.DllState.Text = "正常";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("SimSun", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(77, 87);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(199, 30);
            this.label3.TabIndex = 42;
            this.label3.Text = "动量轮状态：";
            // 
            // DllTime
            // 
            this.DllTime.AutoSize = true;
            this.DllTime.Font = new System.Drawing.Font("SimSun", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.DllTime.ForeColor = System.Drawing.Color.RoyalBlue;
            this.DllTime.Location = new System.Drawing.Point(517, 87);
            this.DllTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.DllTime.Name = "DllTime";
            this.DllTime.Size = new System.Drawing.Size(137, 30);
            this.DllTime.TabIndex = 48;
            this.DllTime.Text = "实时时间";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("SimSun", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(419, 87);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 30);
            this.label2.TabIndex = 47;
            this.label2.Text = "时间：";
            // 
            // chartTimer
            // 
            this.chartTimer.Enabled = true;
            this.chartTimer.Interval = 500;
            this.chartTimer.Tick += new System.EventHandler(this.chartTimer_Tick);
            // 
            // chart2
            // 
            this.chart2.BackColor = System.Drawing.Color.LightBlue;
            this.chart2.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
            chartArea6.AxisX.LineWidth = 2;
            chartArea6.AxisX.TextOrientation = System.Windows.Forms.DataVisualization.Charting.TextOrientation.Horizontal;
            chartArea6.AxisX.Title = "guzhzhzh";
            chartArea6.AxisX.TitleForeColor = System.Drawing.Color.Blue;
            chartArea6.AxisY.IsInterlaced = true;
            chartArea6.AxisY.LineWidth = 2;
            chartArea6.AxisY.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea6.AxisY.TitleForeColor = System.Drawing.Color.DodgerBlue;
            chartArea6.BackColor = System.Drawing.Color.LightSkyBlue;
            chartArea6.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
            chartArea6.BackImageAlignment = System.Windows.Forms.DataVisualization.Charting.ChartImageAlignmentStyle.Center;
            chartArea6.CursorY.IsUserEnabled = true;
            chartArea6.Name = "ChartArea1";
            this.chart2.ChartAreas.Add(chartArea6);
            this.chart2.Cursor = System.Windows.Forms.Cursors.IBeam;
            legend6.Alignment = System.Drawing.StringAlignment.Far;
            legend6.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend6.ForeColor = System.Drawing.Color.DimGray;
            legend6.MaximumAutoSize = 20F;
            legend6.Name = "Legend1";
            this.chart2.Legends.Add(legend6);
            this.chart2.Location = new System.Drawing.Point(1034, 138);
            this.chart2.Margin = new System.Windows.Forms.Padding(2);
            this.chart2.Name = "chart2";
            this.chart2.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright;
            series11.ChartArea = "ChartArea1";
            series11.IsValueShownAsLabel = true;
            series11.Legend = "Legend1";
            series11.Name = "Series1";
            series12.ChartArea = "ChartArea1";
            series12.Legend = "Legend1";
            series12.Name = "Series_yuzhi";
            this.chart2.Series.Add(series11);
            this.chart2.Series.Add(series12);
            this.chart2.Size = new System.Drawing.Size(773, 338);
            this.chart2.TabIndex = 49;
            this.chart2.Text = "chart2";
            // 
            // DyTime
            // 
            this.DyTime.AutoSize = true;
            this.DyTime.Font = new System.Drawing.Font("SimSun", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.DyTime.ForeColor = System.Drawing.Color.RoyalBlue;
            this.DyTime.Location = new System.Drawing.Point(1471, 87);
            this.DyTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.DyTime.Name = "DyTime";
            this.DyTime.Size = new System.Drawing.Size(137, 30);
            this.DyTime.TabIndex = 53;
            this.DyTime.Text = "实时时间";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("SimSun", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(1373, 87);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 30);
            this.label4.TabIndex = 52;
            this.label4.Text = "时间：";
            // 
            // DyState
            // 
            this.DyState.AutoSize = true;
            this.DyState.Font = new System.Drawing.Font("SimSun", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.DyState.ForeColor = System.Drawing.Color.Lime;
            this.DyState.Location = new System.Drawing.Point(1225, 87);
            this.DyState.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.DyState.Name = "DyState";
            this.DyState.Size = new System.Drawing.Size(75, 30);
            this.DyState.TabIndex = 51;
            this.DyState.Text = "正常";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("SimSun", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(1031, 87);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(168, 30);
            this.label6.TabIndex = 50;
            this.label6.Text = "电源状态：";
            // 
            // FsjTime
            // 
            this.FsjTime.AutoSize = true;
            this.FsjTime.Font = new System.Drawing.Font("SimSun", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FsjTime.ForeColor = System.Drawing.Color.RoyalBlue;
            this.FsjTime.Location = new System.Drawing.Point(517, 585);
            this.FsjTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.FsjTime.Name = "FsjTime";
            this.FsjTime.Size = new System.Drawing.Size(137, 30);
            this.FsjTime.TabIndex = 58;
            this.FsjTime.Text = "实时时间";
            this.FsjTime.Click += new System.EventHandler(this.label7_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("SimSun", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(419, 585);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(106, 30);
            this.label8.TabIndex = 57;
            this.label8.Text = "时间：";
            // 
            // FsjState
            // 
            this.FsjState.AutoSize = true;
            this.FsjState.Font = new System.Drawing.Font("SimSun", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FsjState.ForeColor = System.Drawing.Color.Lime;
            this.FsjState.Location = new System.Drawing.Point(271, 585);
            this.FsjState.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.FsjState.Name = "FsjState";
            this.FsjState.Size = new System.Drawing.Size(75, 30);
            this.FsjState.TabIndex = 56;
            this.FsjState.Text = "正常";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("SimSun", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(77, 585);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(199, 30);
            this.label10.TabIndex = 55;
            this.label10.Text = "辐射计状态：";
            // 
            // chart3
            // 
            this.chart3.BackColor = System.Drawing.Color.LightBlue;
            this.chart3.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
            chartArea7.AxisX.LineWidth = 2;
            chartArea7.AxisX.TextOrientation = System.Windows.Forms.DataVisualization.Charting.TextOrientation.Horizontal;
            chartArea7.AxisX.Title = "guzhzhzh";
            chartArea7.AxisX.TitleForeColor = System.Drawing.Color.Blue;
            chartArea7.AxisY.IsInterlaced = true;
            chartArea7.AxisY.LineWidth = 2;
            chartArea7.AxisY.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea7.AxisY.TitleForeColor = System.Drawing.Color.DodgerBlue;
            chartArea7.BackColor = System.Drawing.Color.LightSkyBlue;
            chartArea7.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
            chartArea7.BackImageAlignment = System.Windows.Forms.DataVisualization.Charting.ChartImageAlignmentStyle.Center;
            chartArea7.CursorY.IsUserEnabled = true;
            chartArea7.Name = "ChartArea1";
            this.chart3.ChartAreas.Add(chartArea7);
            this.chart3.Cursor = System.Windows.Forms.Cursors.IBeam;
            legend7.Alignment = System.Drawing.StringAlignment.Far;
            legend7.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend7.ForeColor = System.Drawing.Color.DimGray;
            legend7.MaximumAutoSize = 20F;
            legend7.Name = "Legend1";
            this.chart3.Legends.Add(legend7);
            this.chart3.Location = new System.Drawing.Point(80, 636);
            this.chart3.Margin = new System.Windows.Forms.Padding(2);
            this.chart3.Name = "chart3";
            this.chart3.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright;
            series13.ChartArea = "ChartArea1";
            series13.IsValueShownAsLabel = true;
            series13.Legend = "Legend1";
            series13.Name = "Series1";
            series14.ChartArea = "ChartArea1";
            series14.Legend = "Legend1";
            series14.Name = "Series_yuzhi";
            this.chart3.Series.Add(series13);
            this.chart3.Series.Add(series14);
            this.chart3.Size = new System.Drawing.Size(777, 338);
            this.chart3.TabIndex = 54;
            this.chart3.Text = "chart3";
            // 
            // TcyTime
            // 
            this.TcyTime.AutoSize = true;
            this.TcyTime.Font = new System.Drawing.Font("SimSun", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TcyTime.ForeColor = System.Drawing.Color.RoyalBlue;
            this.TcyTime.Location = new System.Drawing.Point(1471, 585);
            this.TcyTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.TcyTime.Name = "TcyTime";
            this.TcyTime.Size = new System.Drawing.Size(137, 30);
            this.TcyTime.TabIndex = 63;
            this.TcyTime.Text = "实时时间";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("SimSun", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.Location = new System.Drawing.Point(1373, 585);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(106, 30);
            this.label12.TabIndex = 62;
            this.label12.Text = "时间：";
            // 
            // TcyState
            // 
            this.TcyState.AutoSize = true;
            this.TcyState.Font = new System.Drawing.Font("SimSun", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TcyState.ForeColor = System.Drawing.Color.Lime;
            this.TcyState.Location = new System.Drawing.Point(1225, 585);
            this.TcyState.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.TcyState.Name = "TcyState";
            this.TcyState.Size = new System.Drawing.Size(75, 30);
            this.TcyState.TabIndex = 61;
            this.TcyState.Text = "正常";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("SimSun", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.Location = new System.Drawing.Point(1031, 585);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(199, 30);
            this.label14.TabIndex = 60;
            this.label14.Text = "探测仪状态：";
            // 
            // chart4
            // 
            this.chart4.BackColor = System.Drawing.Color.LightBlue;
            this.chart4.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
            chartArea8.AxisX.LineWidth = 2;
            chartArea8.AxisX.TextOrientation = System.Windows.Forms.DataVisualization.Charting.TextOrientation.Horizontal;
            chartArea8.AxisX.Title = "guzhzhzh";
            chartArea8.AxisX.TitleForeColor = System.Drawing.Color.Blue;
            chartArea8.AxisY.IsInterlaced = true;
            chartArea8.AxisY.LineWidth = 2;
            chartArea8.AxisY.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea8.AxisY.TitleForeColor = System.Drawing.Color.DodgerBlue;
            chartArea8.BackColor = System.Drawing.Color.LightSkyBlue;
            chartArea8.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
            chartArea8.BackImageAlignment = System.Windows.Forms.DataVisualization.Charting.ChartImageAlignmentStyle.Center;
            chartArea8.CursorY.IsUserEnabled = true;
            chartArea8.Name = "ChartArea1";
            this.chart4.ChartAreas.Add(chartArea8);
            this.chart4.Cursor = System.Windows.Forms.Cursors.IBeam;
            legend8.Alignment = System.Drawing.StringAlignment.Far;
            legend8.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend8.ForeColor = System.Drawing.Color.DimGray;
            legend8.MaximumAutoSize = 20F;
            legend8.Name = "Legend1";
            this.chart4.Legends.Add(legend8);
            this.chart4.Location = new System.Drawing.Point(1034, 636);
            this.chart4.Margin = new System.Windows.Forms.Padding(2);
            this.chart4.Name = "chart4";
            this.chart4.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright;
            series15.ChartArea = "ChartArea1";
            series15.IsValueShownAsLabel = true;
            series15.Legend = "Legend1";
            series15.Name = "Series1";
            series16.ChartArea = "ChartArea1";
            series16.Legend = "Legend1";
            series16.Name = "Series_yuzhi";
            this.chart4.Series.Add(series15);
            this.chart4.Series.Add(series16);
            this.chart4.Size = new System.Drawing.Size(773, 338);
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
            this.FsjTimer.Interval = 500;
            // 
            // TcyTimer
            // 
            this.TcyTimer.Enabled = true;
            this.TcyTimer.Interval = 500;
            // 
            // RealTimeDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(1904, 1041);
            this.Controls.Add(this.TcyTime);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.TcyState);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.chart4);
            this.Controls.Add(this.FsjTime);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.FsjState);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.chart3);
            this.Controls.Add(this.DyTime);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.DyState);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.chart2);
            this.Controls.Add(this.DllTime);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.DllState);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.chart1);
            this.Name = "RealTimeDisplay";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RealTimeDisplay";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.RealTimeDisplay_Load);
            this.Shown += new System.EventHandler(this.RealTimeDisplay_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
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
    }
}