namespace SatelliteSoftwareIF
{
    partial class VariablePrediction
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chartPrediction = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.chartPrediction)).BeginInit();
            this.SuspendLayout();
            // 
            // chartPrediction
            // 
            this.chartPrediction.BackColor = System.Drawing.Color.Transparent;
            chartArea1.Name = "ChartArea1";
            this.chartPrediction.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartPrediction.Legends.Add(legend1);
            this.chartPrediction.Location = new System.Drawing.Point(25, 23);
            this.chartPrediction.Margin = new System.Windows.Forms.Padding(2);
            this.chartPrediction.Name = "chartPrediction";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartPrediction.Series.Add(series1);
            this.chartPrediction.Size = new System.Drawing.Size(963, 364);
            this.chartPrediction.TabIndex = 1;
            this.chartPrediction.Text = "chart1";
            // 
            // VariablePrediction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(999, 401);
            this.Controls.Add(this.chartPrediction);
            this.Name = "VariablePrediction";
            this.Text = "VariablePrediction";
            ((System.ComponentModel.ISupportInitialize)(this.chartPrediction)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartPrediction;
    }
}