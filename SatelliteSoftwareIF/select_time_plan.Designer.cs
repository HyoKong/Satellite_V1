namespace SatelliteSoftwareIF
{
    partial class SelectTime
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
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.nianyueri_start_tB = new System.Windows.Forms.TextBox();
            this.nianyueri_end_tB = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.shifen_start_tB = new System.Windows.Forms.TextBox();
            this.shifen_end_tB = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("SimSun", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(35, 143);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(120, 22);
            this.label5.TabIndex = 54;
            this.label5.Text = "停止时间：";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("SimSun", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(35, 100);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 22);
            this.label2.TabIndex = 53;
            this.label2.Text = "开始时间：";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("SimSun", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(159, 68);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 22);
            this.label1.TabIndex = 55;
            this.label1.Text = "年 月 日";
            // 
            // nianyueri_start_tB
            // 
            this.nianyueri_start_tB.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nianyueri_start_tB.Location = new System.Drawing.Point(148, 100);
            this.nianyueri_start_tB.Margin = new System.Windows.Forms.Padding(2);
            this.nianyueri_start_tB.Name = "nianyueri_start_tB";
            this.nianyueri_start_tB.Size = new System.Drawing.Size(115, 26);
            this.nianyueri_start_tB.TabIndex = 62;
            this.nianyueri_start_tB.Text = "20171218";
            this.nianyueri_start_tB.TextChanged += new System.EventHandler(this.nianyueri_start_tB_TextChanged);
            // 
            // nianyueri_end_tB
            // 
            this.nianyueri_end_tB.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nianyueri_end_tB.Location = new System.Drawing.Point(148, 143);
            this.nianyueri_end_tB.Margin = new System.Windows.Forms.Padding(2);
            this.nianyueri_end_tB.Name = "nianyueri_end_tB";
            this.nianyueri_end_tB.Size = new System.Drawing.Size(115, 26);
            this.nianyueri_end_tB.TabIndex = 63;
            this.nianyueri_end_tB.Text = "20171218";
            this.nianyueri_end_tB.TextChanged += new System.EventHandler(this.nianyueri_end_tB_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("SimSun", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(297, 68);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 22);
            this.label3.TabIndex = 64;
            this.label3.Text = "时  分";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // shifen_start_tB
            // 
            this.shifen_start_tB.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.shifen_start_tB.Location = new System.Drawing.Point(283, 100);
            this.shifen_start_tB.Margin = new System.Windows.Forms.Padding(2);
            this.shifen_start_tB.Name = "shifen_start_tB";
            this.shifen_start_tB.Size = new System.Drawing.Size(93, 26);
            this.shifen_start_tB.TabIndex = 65;
            this.shifen_start_tB.Text = "08:02";
            this.shifen_start_tB.TextChanged += new System.EventHandler(this.shifen_start_tB_TextChanged);
            // 
            // shifen_end_tB
            // 
            this.shifen_end_tB.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.shifen_end_tB.Location = new System.Drawing.Point(283, 143);
            this.shifen_end_tB.Margin = new System.Windows.Forms.Padding(2);
            this.shifen_end_tB.Name = "shifen_end_tB";
            this.shifen_end_tB.Size = new System.Drawing.Size(93, 26);
            this.shifen_end_tB.TabIndex = 66;
            this.shifen_end_tB.Text = "09:02";
            this.shifen_end_tB.TextChanged += new System.EventHandler(this.shifen_end_tB_TextChanged);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("SimSun", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.ForeColor = System.Drawing.Color.Maroon;
            this.button1.Location = new System.Drawing.Point(109, 215);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(74, 40);
            this.button1.TabIndex = 67;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("SimSun", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.ForeColor = System.Drawing.Color.Maroon;
            this.button2.Location = new System.Drawing.Point(248, 215);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(74, 40);
            this.button2.TabIndex = 68;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("SimSun", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.MediumBlue;
            this.label4.Location = new System.Drawing.Point(22, 24);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(125, 22);
            this.label4.TabIndex = 69;
            this.label4.Text = "诊断部件：";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "动量轮",
            "电源",
            "探测仪",
            "辐射计"});
            this.comboBox1.Location = new System.Drawing.Point(140, 28);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(110, 20);
            this.comboBox1.TabIndex = 70;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            this.comboBox1.SelectedValueChanged += new System.EventHandler(this.comboBox1_SelectedValueChanged);
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(274, 28);
            this.comboBox2.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(130, 20);
            this.comboBox2.TabIndex = 71;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // SelectTime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(428, 274);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.shifen_end_tB);
            this.Controls.Add(this.shifen_start_tB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.nianyueri_end_tB);
            this.Controls.Add(this.nianyueri_start_tB);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.ForeColor = System.Drawing.Color.Sienna;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "SelectTime";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "时间段选择";
            this.Load += new System.EventHandler(this.SelectTime_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox nianyueri_start_tB;
        private System.Windows.Forms.TextBox nianyueri_end_tB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox shifen_start_tB;
        private System.Windows.Forms.TextBox shifen_end_tB;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
    }
}