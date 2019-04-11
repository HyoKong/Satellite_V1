namespace SatelliteSoftwareIF
{
    partial class Form_ST
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart_pro = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chart_data = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btn_save = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.abn_state_lstbox = new System.Windows.Forms.ListBox();
            this.button3 = new System.Windows.Forms.Button();
            this.label_dll = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chart_pro)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_data)).BeginInit();
            this.SuspendLayout();
            // 
            // chart_pro
            // 
            this.chart_pro.BackColor = System.Drawing.Color.Transparent;
            chartArea3.Name = "ChartArea1";
            this.chart_pro.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.chart_pro.Legends.Add(legend3);
            this.chart_pro.Location = new System.Drawing.Point(40, 52);
            this.chart_pro.Margin = new System.Windows.Forms.Padding(2);
            this.chart_pro.Name = "chart_pro";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.chart_pro.Series.Add(series3);
            this.chart_pro.Size = new System.Drawing.Size(979, 364);
            this.chart_pro.TabIndex = 0;
            this.chart_pro.Text = "chart1";
            // 
            // chart_data
            // 
            this.chart_data.BackColor = System.Drawing.Color.Transparent;
            chartArea4.Name = "ChartArea1";
            this.chart_data.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend1";
            this.chart_data.Legends.Add(legend4);
            this.chart_data.Location = new System.Drawing.Point(40, 421);
            this.chart_data.Margin = new System.Windows.Forms.Padding(2);
            this.chart_data.Name = "chart_data";
            series4.ChartArea = "ChartArea1";
            series4.Legend = "Legend1";
            series4.Name = "Series1";
            this.chart_data.Series.Add(series4);
            this.chart_data.Size = new System.Drawing.Size(979, 346);
            this.chart_data.TabIndex = 1;
            this.chart_data.Text = "chart2";
            // 
            // btn_save
            // 
            this.btn_save.BackColor = System.Drawing.SystemColors.Info;
            this.btn_save.Font = new System.Drawing.Font("SimSun", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_save.Location = new System.Drawing.Point(1024, 70);
            this.btn_save.Margin = new System.Windows.Forms.Padding(2);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(152, 58);
            this.btn_save.TabIndex = 17;
            this.btn_save.Text = "保存诊断结果";
            this.btn_save.UseVisualStyleBackColor = false;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft YaHei", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(1024, 19);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(146, 42);
            this.label4.TabIndex = 15;
            this.label4.Text = "诊断结果";
            // 
            // abn_state_lstbox
            // 
            this.abn_state_lstbox.Font = new System.Drawing.Font("SimSun", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.abn_state_lstbox.ForeColor = System.Drawing.Color.Red;
            this.abn_state_lstbox.FormattingEnabled = true;
            this.abn_state_lstbox.ItemHeight = 22;
            this.abn_state_lstbox.Location = new System.Drawing.Point(1024, 142);
            this.abn_state_lstbox.Margin = new System.Windows.Forms.Padding(2);
            this.abn_state_lstbox.Name = "abn_state_lstbox";
            this.abn_state_lstbox.Size = new System.Drawing.Size(378, 576);
            this.abn_state_lstbox.TabIndex = 14;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.SystemColors.Highlight;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button3.Font = new System.Drawing.Font("SimSun", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button3.Location = new System.Drawing.Point(1196, 70);
            this.button3.Margin = new System.Windows.Forms.Padding(2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(145, 58);
            this.button3.TabIndex = 48;
            this.button3.Text = "返回主界面";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label_dll
            // 
            this.label_dll.AutoSize = true;
            this.label_dll.Font = new System.Drawing.Font("SimSun", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_dll.ForeColor = System.Drawing.Color.Green;
            this.label_dll.Location = new System.Drawing.Point(1178, 27);
            this.label_dll.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_dll.Name = "label_dll";
            this.label_dll.Size = new System.Drawing.Size(75, 30);
            this.label_dll.TabIndex = 49;
            this.label_dll.Text = "正常";
            // 
            // Form_ST
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.ClientSize = new System.Drawing.Size(1443, 846);
            this.Controls.Add(this.label_dll);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.abn_state_lstbox);
            this.Controls.Add(this.chart_data);
            this.Controls.Add(this.chart_pro);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form_ST";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form_ST";
            ((System.ComponentModel.ISupportInitialize)(this.chart_pro)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_data)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart_pro;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_data;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox abn_state_lstbox;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label_dll;
    }
}