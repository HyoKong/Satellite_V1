namespace SatelliteSoftwareIF
{
    partial class variable_predict_select_time
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
            this.cbVariables = new System.Windows.Forms.ComboBox();
            this.cbParts = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btn_OK = new System.Windows.Forms.Button();
            this.shifen_end_tB = new System.Windows.Forms.TextBox();
            this.shifen_start_tB = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.nianyueri_end_tB = new System.Windows.Forms.TextBox();
            this.nianyueri_start_tB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cbVariables
            // 
            this.cbVariables.FormattingEnabled = true;
            this.cbVariables.Location = new System.Drawing.Point(333, 22);
            this.cbVariables.Margin = new System.Windows.Forms.Padding(2);
            this.cbVariables.Name = "cbVariables";
            this.cbVariables.Size = new System.Drawing.Size(130, 20);
            this.cbVariables.TabIndex = 84;
            this.cbVariables.SelectedValueChanged += new System.EventHandler(this.cbVariables_SelectedValueChanged);
            // 
            // cbParts
            // 
            this.cbParts.FormattingEnabled = true;
            this.cbParts.Items.AddRange(new object[] {
            "动量轮",
            "电源",
            "探测仪",
            "辐射计"});
            this.cbParts.Location = new System.Drawing.Point(209, 22);
            this.cbParts.Margin = new System.Windows.Forms.Padding(2);
            this.cbParts.Name = "cbParts";
            this.cbParts.Size = new System.Drawing.Size(110, 20);
            this.cbParts.TabIndex = 83;
            this.cbParts.SelectedIndexChanged += new System.EventHandler(this.cbParts_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("SimSun", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.MediumBlue;
            this.label4.Location = new System.Drawing.Point(23, 22);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(194, 22);
            this.label4.TabIndex = 82;
            this.label4.Text = "预测部件及变量：";
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("SimSun", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.ForeColor = System.Drawing.Color.Maroon;
            this.btnCancel.Location = new System.Drawing.Point(268, 213);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(74, 40);
            this.btnCancel.TabIndex = 81;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btn_OK
            // 
            this.btn_OK.Font = new System.Drawing.Font("SimSun", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_OK.ForeColor = System.Drawing.Color.Maroon;
            this.btn_OK.Location = new System.Drawing.Point(129, 213);
            this.btn_OK.Margin = new System.Windows.Forms.Padding(2);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(74, 40);
            this.btn_OK.TabIndex = 80;
            this.btn_OK.Text = "确定";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // shifen_end_tB
            // 
            this.shifen_end_tB.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.shifen_end_tB.Location = new System.Drawing.Point(345, 141);
            this.shifen_end_tB.Margin = new System.Windows.Forms.Padding(2);
            this.shifen_end_tB.Name = "shifen_end_tB";
            this.shifen_end_tB.Size = new System.Drawing.Size(93, 26);
            this.shifen_end_tB.TabIndex = 79;
            this.shifen_end_tB.Text = "09:02";
            // 
            // shifen_start_tB
            // 
            this.shifen_start_tB.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.shifen_start_tB.Location = new System.Drawing.Point(345, 98);
            this.shifen_start_tB.Margin = new System.Windows.Forms.Padding(2);
            this.shifen_start_tB.Name = "shifen_start_tB";
            this.shifen_start_tB.Size = new System.Drawing.Size(93, 26);
            this.shifen_start_tB.TabIndex = 78;
            this.shifen_start_tB.Text = "08:02";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("SimSun", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(359, 66);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 22);
            this.label3.TabIndex = 77;
            this.label3.Text = "时刻";
            // 
            // nianyueri_end_tB
            // 
            this.nianyueri_end_tB.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nianyueri_end_tB.Location = new System.Drawing.Point(210, 141);
            this.nianyueri_end_tB.Margin = new System.Windows.Forms.Padding(2);
            this.nianyueri_end_tB.Name = "nianyueri_end_tB";
            this.nianyueri_end_tB.Size = new System.Drawing.Size(115, 26);
            this.nianyueri_end_tB.TabIndex = 76;
            this.nianyueri_end_tB.Text = "20171218";
            // 
            // nianyueri_start_tB
            // 
            this.nianyueri_start_tB.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nianyueri_start_tB.Location = new System.Drawing.Point(210, 98);
            this.nianyueri_start_tB.Margin = new System.Windows.Forms.Padding(2);
            this.nianyueri_start_tB.Name = "nianyueri_start_tB";
            this.nianyueri_start_tB.Size = new System.Drawing.Size(115, 26);
            this.nianyueri_start_tB.TabIndex = 75;
            this.nianyueri_start_tB.Text = "20171218";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("SimSun", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(221, 66);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 22);
            this.label1.TabIndex = 74;
            this.label1.Text = "年 月 日";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("SimSun", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(30, 141);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(120, 22);
            this.label5.TabIndex = 73;
            this.label5.Text = "停止时间：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("SimSun", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(30, 98);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 22);
            this.label2.TabIndex = 72;
            this.label2.Text = "开始时间：";
            // 
            // variable_predict_select_time
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 271);
            this.Controls.Add(this.cbVariables);
            this.Controls.Add(this.cbParts);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.shifen_end_tB);
            this.Controls.Add(this.shifen_start_tB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.nianyueri_end_tB);
            this.Controls.Add(this.nianyueri_start_tB);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Name = "variable_predict_select_time";
            this.Text = "时间段选择";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbVariables;
        private System.Windows.Forms.ComboBox cbParts;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.TextBox shifen_end_tB;
        private System.Windows.Forms.TextBox shifen_start_tB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox nianyueri_end_tB;
        private System.Windows.Forms.TextBox nianyueri_start_tB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
    }
}