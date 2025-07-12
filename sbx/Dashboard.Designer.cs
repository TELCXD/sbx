namespace sbx
{
    partial class Dashboard
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend5 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title5 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend6 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title6 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea7 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend7 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title7 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea8 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend8 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series8 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title8 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Dashboard));
            panel1 = new Panel();
            btn_detalle = new Button();
            btn_buscar = new Button();
            dtp_fecha_fin = new DateTimePicker();
            dtp_fecha_inicio = new DateTimePicker();
            label1 = new Label();
            lbl_fechaVencimiento = new Label();
            panel4 = new Panel();
            panel2 = new Panel();
            chart4 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            panel3 = new Panel();
            panel10 = new Panel();
            chart3 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            panel9 = new Panel();
            chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            panel8 = new Panel();
            chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            panel5 = new Panel();
            panel11 = new Panel();
            pictureBox3 = new PictureBox();
            lbl_costos_totales = new Label();
            label6 = new Label();
            panel7 = new Panel();
            pictureBox2 = new PictureBox();
            lbl_ganancias_totales = new Label();
            label4 = new Label();
            panel6 = new Panel();
            pictureBox4 = new PictureBox();
            lbl_ventas_totales = new Label();
            label3 = new Label();
            panel1.SuspendLayout();
            panel4.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chart4).BeginInit();
            panel3.SuspendLayout();
            panel10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chart3).BeginInit();
            panel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chart2).BeginInit();
            panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chart1).BeginInit();
            panel5.SuspendLayout();
            panel11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
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
            panel1.Size = new Size(1264, 56);
            panel1.TabIndex = 0;
            // 
            // btn_detalle
            // 
            btn_detalle.FlatStyle = FlatStyle.Flat;
            btn_detalle.Location = new Point(852, 20);
            btn_detalle.Margin = new Padding(3, 2, 3, 2);
            btn_detalle.Name = "btn_detalle";
            btn_detalle.Size = new Size(82, 26);
            btn_detalle.TabIndex = 161;
            btn_detalle.Text = "Ver detalle";
            btn_detalle.UseVisualStyleBackColor = true;
            btn_detalle.Click += btn_detalle_Click;
            // 
            // btn_buscar
            // 
            btn_buscar.FlatStyle = FlatStyle.Flat;
            btn_buscar.Location = new Point(765, 20);
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
            dtp_fecha_fin.Location = new Point(574, 22);
            dtp_fecha_fin.Name = "dtp_fecha_fin";
            dtp_fecha_fin.Size = new Size(187, 23);
            dtp_fecha_fin.TabIndex = 157;
            // 
            // dtp_fecha_inicio
            // 
            dtp_fecha_inicio.Anchor = AnchorStyles.Top;
            dtp_fecha_inicio.Format = DateTimePickerFormat.Short;
            dtp_fecha_inicio.Location = new Point(360, 22);
            dtp_fecha_inicio.Name = "dtp_fecha_inicio";
            dtp_fecha_inicio.Size = new Size(200, 23);
            dtp_fecha_inicio.TabIndex = 156;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top;
            label1.AutoSize = true;
            label1.Location = new Point(574, 4);
            label1.Name = "label1";
            label1.Size = new Size(55, 15);
            label1.TabIndex = 159;
            label1.Text = "Fecha fin";
            // 
            // lbl_fechaVencimiento
            // 
            lbl_fechaVencimiento.Anchor = AnchorStyles.Top;
            lbl_fechaVencimiento.AutoSize = true;
            lbl_fechaVencimiento.Location = new Point(360, 4);
            lbl_fechaVencimiento.Name = "lbl_fechaVencimiento";
            lbl_fechaVencimiento.Size = new Size(70, 15);
            lbl_fechaVencimiento.TabIndex = 158;
            lbl_fechaVencimiento.Text = "Fecha inicio";
            // 
            // panel4
            // 
            panel4.Controls.Add(panel2);
            panel4.Controls.Add(panel3);
            panel4.Controls.Add(panel5);
            panel4.Dock = DockStyle.Fill;
            panel4.Location = new Point(0, 56);
            panel4.Margin = new Padding(3, 2, 3, 2);
            panel4.Name = "panel4";
            panel4.Size = new Size(1264, 617);
            panel4.TabIndex = 3;
            // 
            // panel2
            // 
            panel2.BorderStyle = BorderStyle.Fixed3D;
            panel2.Controls.Add(chart4);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 362);
            panel2.Margin = new Padding(3, 2, 3, 2);
            panel2.Name = "panel2";
            panel2.Size = new Size(1264, 255);
            panel2.TabIndex = 4;
            // 
            // chart4
            // 
            chartArea5.Name = "ChartArea1";
            chart4.ChartAreas.Add(chartArea5);
            chart4.Dock = DockStyle.Fill;
            legend5.Name = "Legend1";
            chart4.Legends.Add(legend5);
            chart4.Location = new Point(0, 0);
            chart4.Margin = new Padding(3, 2, 3, 2);
            chart4.Name = "chart4";
            chart4.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SeaGreen;
            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series5.Legend = "Legend1";
            series5.Name = "Series1";
            series5.YValuesPerPoint = 6;
            chart4.Series.Add(series5);
            chart4.Size = new Size(1260, 251);
            chart4.TabIndex = 4;
            chart4.Text = "chart4";
            title5.Name = "Title1";
            title5.Text = "Ventas diarias";
            chart4.Titles.Add(title5);
            // 
            // panel3
            // 
            panel3.Controls.Add(panel10);
            panel3.Controls.Add(panel9);
            panel3.Controls.Add(panel8);
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(0, 78);
            panel3.Margin = new Padding(3, 2, 3, 2);
            panel3.Name = "panel3";
            panel3.Size = new Size(1264, 284);
            panel3.TabIndex = 3;
            // 
            // panel10
            // 
            panel10.BorderStyle = BorderStyle.Fixed3D;
            panel10.Controls.Add(chart3);
            panel10.Dock = DockStyle.Fill;
            panel10.Location = new Point(867, 0);
            panel10.Margin = new Padding(3, 2, 3, 2);
            panel10.Name = "panel10";
            panel10.Size = new Size(397, 284);
            panel10.TabIndex = 2;
            // 
            // chart3
            // 
            chartArea6.Name = "ChartArea1";
            chart3.ChartAreas.Add(chartArea6);
            chart3.Dock = DockStyle.Fill;
            legend6.Name = "Legend1";
            chart3.Legends.Add(legend6);
            chart3.Location = new Point(0, 0);
            chart3.Margin = new Padding(3, 2, 3, 2);
            chart3.Name = "chart3";
            series6.ChartArea = "ChartArea1";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Doughnut;
            series6.Legend = "Legend1";
            series6.Name = "Series1";
            series6.YValuesPerPoint = 6;
            chart3.Series.Add(series6);
            chart3.Size = new Size(393, 280);
            chart3.TabIndex = 3;
            chart3.Text = "chart3";
            title6.Name = "Title1";
            title6.Text = "Medios de pago";
            chart3.Titles.Add(title6);
            // 
            // panel9
            // 
            panel9.BorderStyle = BorderStyle.Fixed3D;
            panel9.Controls.Add(chart2);
            panel9.Dock = DockStyle.Left;
            panel9.Location = new Point(420, 0);
            panel9.Margin = new Padding(3, 2, 3, 2);
            panel9.Name = "panel9";
            panel9.Size = new Size(447, 284);
            panel9.TabIndex = 1;
            // 
            // chart2
            // 
            chartArea7.Name = "ChartArea1";
            chart2.ChartAreas.Add(chartArea7);
            chart2.Dock = DockStyle.Fill;
            legend7.Name = "Legend1";
            chart2.Legends.Add(legend7);
            chart2.Location = new Point(0, 0);
            chart2.Margin = new Padding(3, 2, 3, 2);
            chart2.Name = "chart2";
            chart2.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SeaGreen;
            series7.ChartArea = "ChartArea1";
            series7.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar;
            series7.Legend = "Legend1";
            series7.Name = "Series1";
            series7.YValuesPerPoint = 6;
            chart2.Series.Add(series7);
            chart2.Size = new Size(443, 280);
            chart2.TabIndex = 3;
            chart2.Text = "chart2";
            title7.Name = "Title1";
            title7.Text = "Productos más vendidos";
            chart2.Titles.Add(title7);
            // 
            // panel8
            // 
            panel8.BorderStyle = BorderStyle.Fixed3D;
            panel8.Controls.Add(chart1);
            panel8.Dock = DockStyle.Left;
            panel8.Location = new Point(0, 0);
            panel8.Margin = new Padding(3, 2, 3, 2);
            panel8.Name = "panel8";
            panel8.Size = new Size(420, 284);
            panel8.TabIndex = 0;
            // 
            // chart1
            // 
            chartArea8.Name = "ChartArea1";
            chart1.ChartAreas.Add(chartArea8);
            chart1.Dock = DockStyle.Fill;
            legend8.Name = "Legend1";
            chart1.Legends.Add(legend8);
            chart1.Location = new Point(0, 0);
            chart1.Margin = new Padding(3, 2, 3, 2);
            chart1.Name = "chart1";
            chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.EarthTones;
            series8.ChartArea = "ChartArea1";
            series8.Legend = "Legend1";
            series8.Name = "Series1";
            series8.YValuesPerPoint = 6;
            chart1.Series.Add(series8);
            chart1.Size = new Size(416, 280);
            chart1.TabIndex = 4;
            chart1.Text = "chart1";
            title8.Name = "Title1";
            title8.Text = "Ventas por mes";
            chart1.Titles.Add(title8);
            // 
            // panel5
            // 
            panel5.BackColor = SystemColors.Window;
            panel5.BorderStyle = BorderStyle.FixedSingle;
            panel5.Controls.Add(panel11);
            panel5.Controls.Add(panel7);
            panel5.Controls.Add(panel6);
            panel5.Dock = DockStyle.Top;
            panel5.Location = new Point(0, 0);
            panel5.Margin = new Padding(3, 2, 3, 2);
            panel5.Name = "panel5";
            panel5.Size = new Size(1264, 78);
            panel5.TabIndex = 0;
            // 
            // panel11
            // 
            panel11.BackColor = SystemColors.Window;
            panel11.BorderStyle = BorderStyle.Fixed3D;
            panel11.Controls.Add(pictureBox3);
            panel11.Controls.Add(lbl_costos_totales);
            panel11.Controls.Add(label6);
            panel11.Dock = DockStyle.Fill;
            panel11.Location = new Point(867, 0);
            panel11.Margin = new Padding(3, 2, 3, 2);
            panel11.Name = "panel11";
            panel11.Size = new Size(395, 76);
            panel11.TabIndex = 10;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(338, 20);
            pictureBox3.Margin = new Padding(3, 2, 3, 2);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(50, 36);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 3;
            pictureBox3.TabStop = false;
            // 
            // lbl_costos_totales
            // 
            lbl_costos_totales.AutoSize = true;
            lbl_costos_totales.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbl_costos_totales.Location = new Point(10, 35);
            lbl_costos_totales.Name = "lbl_costos_totales";
            lbl_costos_totales.Size = new Size(17, 21);
            lbl_costos_totales.TabIndex = 2;
            lbl_costos_totales.Text = "_";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.Location = new Point(10, 8);
            label6.Name = "label6";
            label6.Size = new Size(146, 21);
            label6.TabIndex = 1;
            label6.Text = "COSTOS TOTALES $";
            // 
            // panel7
            // 
            panel7.BackColor = SystemColors.Window;
            panel7.BorderStyle = BorderStyle.Fixed3D;
            panel7.Controls.Add(pictureBox2);
            panel7.Controls.Add(lbl_ganancias_totales);
            panel7.Controls.Add(label4);
            panel7.Dock = DockStyle.Left;
            panel7.Location = new Point(420, 0);
            panel7.Margin = new Padding(3, 2, 3, 2);
            panel7.Name = "panel7";
            panel7.Size = new Size(447, 76);
            panel7.TabIndex = 9;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(389, 20);
            pictureBox2.Margin = new Padding(3, 2, 3, 2);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(50, 36);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 3;
            pictureBox2.TabStop = false;
            // 
            // lbl_ganancias_totales
            // 
            lbl_ganancias_totales.AutoSize = true;
            lbl_ganancias_totales.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbl_ganancias_totales.Location = new Point(10, 35);
            lbl_ganancias_totales.Name = "lbl_ganancias_totales";
            lbl_ganancias_totales.Size = new Size(17, 21);
            lbl_ganancias_totales.TabIndex = 2;
            lbl_ganancias_totales.Text = "_";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(10, 8);
            label4.Name = "label4";
            label4.Size = new Size(175, 21);
            label4.TabIndex = 1;
            label4.Text = "GANANCIAS TOTALES $";
            // 
            // panel6
            // 
            panel6.BackColor = SystemColors.Window;
            panel6.BorderStyle = BorderStyle.Fixed3D;
            panel6.Controls.Add(pictureBox4);
            panel6.Controls.Add(lbl_ventas_totales);
            panel6.Controls.Add(label3);
            panel6.Dock = DockStyle.Left;
            panel6.Location = new Point(0, 0);
            panel6.Margin = new Padding(3, 2, 3, 2);
            panel6.Name = "panel6";
            panel6.Size = new Size(420, 76);
            panel6.TabIndex = 8;
            // 
            // pictureBox4
            // 
            pictureBox4.Image = (Image)resources.GetObject("pictureBox4.Image");
            pictureBox4.Location = new Point(361, 20);
            pictureBox4.Margin = new Padding(3, 2, 3, 2);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(50, 36);
            pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox4.TabIndex = 3;
            pictureBox4.TabStop = false;
            // 
            // lbl_ventas_totales
            // 
            lbl_ventas_totales.AutoSize = true;
            lbl_ventas_totales.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbl_ventas_totales.Location = new Point(10, 35);
            lbl_ventas_totales.Name = "lbl_ventas_totales";
            lbl_ventas_totales.Size = new Size(17, 21);
            lbl_ventas_totales.TabIndex = 2;
            lbl_ventas_totales.Text = "_";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(10, 8);
            label3.Name = "label3";
            label3.Size = new Size(143, 21);
            label3.TabIndex = 1;
            label3.Text = "VENTAS TOTALES $";
            // 
            // Dashboard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1264, 673);
            Controls.Add(panel4);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 2, 3, 2);
            MaximizeBox = false;
            MaximumSize = new Size(1280, 712);
            MinimumSize = new Size(1280, 712);
            Name = "Dashboard";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Dashboard";
            Load += Dashboard_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel4.ResumeLayout(false);
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)chart4).EndInit();
            panel3.ResumeLayout(false);
            panel10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)chart3).EndInit();
            panel9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)chart2).EndInit();
            panel8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)chart1).EndInit();
            panel5.ResumeLayout(false);
            panel11.ResumeLayout(false);
            panel11.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            panel7.ResumeLayout(false);
            panel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            panel6.ResumeLayout(false);
            panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel4;
        private Panel panel5;
        private Panel panel3;
        private Panel panel10;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart3;
        private Panel panel9;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart2;
        private Panel panel8;
        private Panel panel2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart4;
        private Panel panel6;
        private Label lbl_ventas_totales;
        private Label label3;
        private Panel panel7;
        private Label lbl_ganancias_totales;
        private Label label4;
        private Panel panel11;
        private Label lbl_costos_totales;
        private Label label6;
        private PictureBox pictureBox3;
        private PictureBox pictureBox2;
        private PictureBox pictureBox4;
        private DateTimePicker dtp_fecha_fin;
        private DateTimePicker dtp_fecha_inicio;
        private Label label1;
        private Label lbl_fechaVencimiento;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private Button btn_buscar;
        private Button btn_detalle;
    }
}