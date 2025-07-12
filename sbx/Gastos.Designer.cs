namespace sbx
{
    partial class Gastos
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
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title3 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Gastos));
            panel1 = new Panel();
            btn_detalle = new Button();
            btn_buscar = new Button();
            dtp_fecha_fin = new DateTimePicker();
            dtp_fecha_inicio = new DateTimePicker();
            label1 = new Label();
            lbl_fechaVencimiento = new Label();
            panel2 = new Panel();
            lbl_gastos_totales = new Label();
            label3 = new Label();
            panel3 = new Panel();
            chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            panel4 = new Panel();
            chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            panel5 = new Panel();
            chart3 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chart1).BeginInit();
            panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chart2).BeginInit();
            panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chart3).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.Window;
            panel1.BorderStyle = BorderStyle.Fixed3D;
            panel1.Controls.Add(btn_detalle);
            panel1.Controls.Add(btn_buscar);
            panel1.Controls.Add(dtp_fecha_fin);
            panel1.Controls.Add(dtp_fecha_inicio);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(lbl_fechaVencimiento);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(1104, 56);
            panel1.TabIndex = 1;
            // 
            // btn_detalle
            // 
            btn_detalle.FlatStyle = FlatStyle.Flat;
            btn_detalle.Location = new Point(740, 18);
            btn_detalle.Margin = new Padding(3, 2, 3, 2);
            btn_detalle.Name = "btn_detalle";
            btn_detalle.Size = new Size(82, 26);
            btn_detalle.TabIndex = 162;
            btn_detalle.Text = "Ver detalle";
            btn_detalle.UseVisualStyleBackColor = true;
            btn_detalle.Click += btn_detalle_Click;
            // 
            // btn_buscar
            // 
            btn_buscar.FlatStyle = FlatStyle.Flat;
            btn_buscar.Location = new Point(652, 18);
            btn_buscar.Margin = new Padding(3, 2, 3, 2);
            btn_buscar.Name = "btn_buscar";
            btn_buscar.Size = new Size(82, 26);
            btn_buscar.TabIndex = 160;
            btn_buscar.Text = "Buscar";
            btn_buscar.UseVisualStyleBackColor = true;
            btn_buscar.Click += btn_buscar_Click;
            // 
            // dtp_fecha_fin
            // 
            dtp_fecha_fin.Anchor = AnchorStyles.Top;
            dtp_fecha_fin.Format = DateTimePickerFormat.Short;
            dtp_fecha_fin.Location = new Point(452, 21);
            dtp_fecha_fin.Name = "dtp_fecha_fin";
            dtp_fecha_fin.Size = new Size(187, 23);
            dtp_fecha_fin.TabIndex = 157;
            // 
            // dtp_fecha_inicio
            // 
            dtp_fecha_inicio.Anchor = AnchorStyles.Top;
            dtp_fecha_inicio.Format = DateTimePickerFormat.Short;
            dtp_fecha_inicio.Location = new Point(244, 21);
            dtp_fecha_inicio.Name = "dtp_fecha_inicio";
            dtp_fecha_inicio.Size = new Size(200, 23);
            dtp_fecha_inicio.TabIndex = 156;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top;
            label1.AutoSize = true;
            label1.Location = new Point(452, 3);
            label1.Name = "label1";
            label1.Size = new Size(55, 15);
            label1.TabIndex = 159;
            label1.Text = "Fecha fin";
            // 
            // lbl_fechaVencimiento
            // 
            lbl_fechaVencimiento.Anchor = AnchorStyles.Top;
            lbl_fechaVencimiento.AutoSize = true;
            lbl_fechaVencimiento.Location = new Point(244, 3);
            lbl_fechaVencimiento.Name = "lbl_fechaVencimiento";
            lbl_fechaVencimiento.Size = new Size(70, 15);
            lbl_fechaVencimiento.TabIndex = 158;
            lbl_fechaVencimiento.Text = "Fecha inicio";
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.Window;
            panel2.BorderStyle = BorderStyle.Fixed3D;
            panel2.Controls.Add(lbl_gastos_totales);
            panel2.Controls.Add(label3);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 56);
            panel2.Name = "panel2";
            panel2.Size = new Size(1104, 66);
            panel2.TabIndex = 2;
            // 
            // lbl_gastos_totales
            // 
            lbl_gastos_totales.AutoSize = true;
            lbl_gastos_totales.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbl_gastos_totales.Location = new Point(10, 33);
            lbl_gastos_totales.Name = "lbl_gastos_totales";
            lbl_gastos_totales.Size = new Size(17, 21);
            lbl_gastos_totales.TabIndex = 5;
            lbl_gastos_totales.Text = "_";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(10, 6);
            label3.Name = "label3";
            label3.Size = new Size(154, 21);
            label3.TabIndex = 4;
            label3.Text = "EGRESOS TOTALES $";
            // 
            // panel3
            // 
            panel3.BorderStyle = BorderStyle.Fixed3D;
            panel3.Controls.Add(chart1);
            panel3.Location = new Point(0, 122);
            panel3.Name = "panel3";
            panel3.Size = new Size(664, 288);
            panel3.TabIndex = 3;
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            chart1.ChartAreas.Add(chartArea1);
            chart1.Dock = DockStyle.Fill;
            legend1.Name = "Legend1";
            chart1.Legends.Add(legend1);
            chart1.Location = new Point(0, 0);
            chart1.Margin = new Padding(3, 2, 3, 2);
            chart1.Name = "chart1";
            chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.EarthTones;
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            series1.YValuesPerPoint = 6;
            chart1.Series.Add(series1);
            chart1.Size = new Size(660, 284);
            chart1.TabIndex = 5;
            chart1.Text = "chart1";
            title1.Name = "Title1";
            title1.Text = "Egresos por mes";
            chart1.Titles.Add(title1);
            // 
            // panel4
            // 
            panel4.BorderStyle = BorderStyle.Fixed3D;
            panel4.Controls.Add(chart2);
            panel4.Location = new Point(670, 122);
            panel4.Name = "panel4";
            panel4.Size = new Size(434, 288);
            panel4.TabIndex = 4;
            // 
            // chart2
            // 
            chartArea2.Name = "ChartArea1";
            chart2.ChartAreas.Add(chartArea2);
            chart2.Dock = DockStyle.Fill;
            legend2.Name = "Legend1";
            chart2.Legends.Add(legend2);
            chart2.Location = new Point(0, 0);
            chart2.Margin = new Padding(3, 2, 3, 2);
            chart2.Name = "chart2";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Doughnut;
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            series2.YValuesPerPoint = 6;
            chart2.Series.Add(series2);
            chart2.Size = new Size(430, 284);
            chart2.TabIndex = 4;
            chart2.Text = "chart2";
            title2.Name = "Title1";
            title2.Text = "Egresos";
            chart2.Titles.Add(title2);
            // 
            // panel5
            // 
            panel5.Controls.Add(chart3);
            panel5.Dock = DockStyle.Bottom;
            panel5.Location = new Point(0, 416);
            panel5.Name = "panel5";
            panel5.Size = new Size(1104, 233);
            panel5.TabIndex = 5;
            // 
            // chart3
            // 
            chartArea3.Name = "ChartArea1";
            chart3.ChartAreas.Add(chartArea3);
            chart3.Dock = DockStyle.Fill;
            legend3.Name = "Legend1";
            chart3.Legends.Add(legend3);
            chart3.Location = new Point(0, 0);
            chart3.Margin = new Padding(3, 2, 3, 2);
            chart3.Name = "chart3";
            chart3.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Pastel;
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Area;
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            series3.YValuesPerPoint = 6;
            chart3.Series.Add(series3);
            chart3.Size = new Size(1104, 233);
            chart3.TabIndex = 5;
            chart3.Text = "chart3";
            title3.Name = "Title1";
            title3.Text = "Egresos";
            chart3.Titles.Add(title3);
            // 
            // Gastos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            ClientSize = new Size(1104, 649);
            Controls.Add(panel5);
            Controls.Add(panel4);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MaximumSize = new Size(1120, 688);
            MinimumSize = new Size(1120, 688);
            Name = "Gastos";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Gastos";
            Load += Gastos_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)chart1).EndInit();
            panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)chart2).EndInit();
            panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)chart3).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button btn_buscar;
        private DateTimePicker dtp_fecha_fin;
        private DateTimePicker dtp_fecha_inicio;
        private Label label1;
        private Label lbl_fechaVencimiento;
        private Panel panel2;
        private Label lbl_gastos_totales;
        private Label label3;
        private Panel panel3;
        private Panel panel4;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart2;
        private Button btn_detalle;
        private Panel panel5;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart3;
    }
}