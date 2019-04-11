namespace SatelliteSoftwareIF
{
    partial class Logon
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
            this.label3 = new System.Windows.Forms.Label();
            this.btn_fault_dignosis = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.help_btn = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("KaiTi", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.Yellow;
            this.label3.Location = new System.Drawing.Point(587, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1144, 846);
            this.label3.TabIndex = 2;
            this.label3.Text = "气象卫星部件故障监测软件";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // btn_fault_dignosis
            // 
            this.btn_fault_dignosis.BackColor = System.Drawing.SystemColors.Info;
            this.btn_fault_dignosis.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_fault_dignosis.Font = new System.Drawing.Font("Microsoft YaHei", 22.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_fault_dignosis.Location = new System.Drawing.Point(994, 187);
            this.btn_fault_dignosis.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_fault_dignosis.Name = "btn_fault_dignosis";
            this.btn_fault_dignosis.Size = new System.Drawing.Size(295, 91);
            this.btn_fault_dignosis.TabIndex = 22;
            this.btn_fault_dignosis.Text = "实时故障监测";
            this.btn_fault_dignosis.UseVisualStyleBackColor = false;
            this.btn_fault_dignosis.Click += new System.EventHandler(this.btn_fault_dignosis_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.Info;
            this.button1.Font = new System.Drawing.Font("Microsoft YaHei", 22.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(994, 303);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(295, 91);
            this.button1.TabIndex = 24;
            this.button1.Text = "历史数据查看";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // help_btn
            // 
            this.help_btn.BackColor = System.Drawing.SystemColors.Info;
            this.help_btn.Font = new System.Drawing.Font("Microsoft YaHei", 22.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.help_btn.Location = new System.Drawing.Point(994, 442);
            this.help_btn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.help_btn.Name = "help_btn";
            this.help_btn.Size = new System.Drawing.Size(295, 91);
            this.help_btn.TabIndex = 25;
            this.help_btn.Text = "帮助";
            this.help_btn.UseVisualStyleBackColor = false;
            this.help_btn.Click += new System.EventHandler(this.help_btn_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Crimson;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button3.Font = new System.Drawing.Font("Microsoft YaHei", 22.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button3.Location = new System.Drawing.Point(994, 552);
            this.button3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(295, 91);
            this.button3.TabIndex = 26;
            this.button3.Text = "退出";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Logon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::SatelliteSoftwareIF.Properties.Resources.进入界面背景;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1443, 846);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.help_btn);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn_fault_dignosis);
            this.Controls.Add(this.label3);
            this.DoubleBuffered = true;
            this.Name = "Logon";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Logon";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Logon_FormClosing);
            this.Load += new System.EventHandler(this.Logon_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_fault_dignosis;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button help_btn;
        private System.Windows.Forms.Button button3;
    }
}