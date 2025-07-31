namespace sbx
{
    partial class ReporteGeneral
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReporteGeneral));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            panel1 = new Panel();
            btn_buscar = new Button();
            dtp_fecha_fin = new DateTimePicker();
            dtp_fecha_inicio = new DateTimePicker();
            label1 = new Label();
            lbl_fechaVencimiento = new Label();
            panel2 = new Panel();
            panel7 = new Panel();
            btn_salidas = new Button();
            lbl_salidas = new Label();
            label7 = new Label();
            panel5 = new Panel();
            btn_egresos = new Button();
            lbl_gastos = new Label();
            label6 = new Label();
            panel4 = new Panel();
            btn_detalle_venta = new Button();
            label3 = new Label();
            lbl_ventas_totales = new Label();
            panel3 = new Panel();
            btn_detalle_compras = new Button();
            lbl_compras = new Label();
            label4 = new Label();
            panel6 = new Panel();
            lbl_resultado = new Label();
            label2 = new Label();
            chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            panel8 = new Panel();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel7.SuspendLayout();
            panel5.SuspendLayout();
            panel4.SuspendLayout();
            panel3.SuspendLayout();
            panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chart1).BeginInit();
            panel8.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.Window;
            panel1.BorderStyle = BorderStyle.Fixed3D;
            panel1.Controls.Add(btn_buscar);
            panel1.Controls.Add(dtp_fecha_fin);
            panel1.Controls.Add(dtp_fecha_inicio);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(lbl_fechaVencimiento);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1141, 54);
            panel1.TabIndex = 0;
            // 
            // btn_buscar
            // 
            btn_buscar.FlatStyle = FlatStyle.Flat;
            btn_buscar.Location = new Point(724, 20);
            btn_buscar.Margin = new Padding(3, 2, 3, 2);
            btn_buscar.Name = "btn_buscar";
            btn_buscar.Size = new Size(82, 26);
            btn_buscar.TabIndex = 165;
            btn_buscar.Text = "Buscar";
            btn_buscar.UseVisualStyleBackColor = true;
            btn_buscar.Click += btn_buscar_Click;
            // 
            // dtp_fecha_fin
            // 
            dtp_fecha_fin.Anchor = AnchorStyles.Top;
            dtp_fecha_fin.Format = DateTimePickerFormat.Short;
            dtp_fecha_fin.Location = new Point(531, 23);
            dtp_fecha_fin.Name = "dtp_fecha_fin";
            dtp_fecha_fin.Size = new Size(187, 23);
            dtp_fecha_fin.TabIndex = 162;
            // 
            // dtp_fecha_inicio
            // 
            dtp_fecha_inicio.Anchor = AnchorStyles.Top;
            dtp_fecha_inicio.Format = DateTimePickerFormat.Short;
            dtp_fecha_inicio.Location = new Point(323, 23);
            dtp_fecha_inicio.Name = "dtp_fecha_inicio";
            dtp_fecha_inicio.Size = new Size(200, 23);
            dtp_fecha_inicio.TabIndex = 161;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top;
            label1.AutoSize = true;
            label1.Location = new Point(531, 5);
            label1.Name = "label1";
            label1.Size = new Size(55, 15);
            label1.TabIndex = 164;
            label1.Text = "Fecha fin";
            // 
            // lbl_fechaVencimiento
            // 
            lbl_fechaVencimiento.Anchor = AnchorStyles.Top;
            lbl_fechaVencimiento.AutoSize = true;
            lbl_fechaVencimiento.Location = new Point(323, 5);
            lbl_fechaVencimiento.Name = "lbl_fechaVencimiento";
            lbl_fechaVencimiento.Size = new Size(70, 15);
            lbl_fechaVencimiento.TabIndex = 163;
            lbl_fechaVencimiento.Text = "Fecha inicio";
            // 
            // panel2
            // 
            panel2.BackColor = Color.WhiteSmoke;
            panel2.BorderStyle = BorderStyle.Fixed3D;
            panel2.Controls.Add(panel7);
            panel2.Controls.Add(panel5);
            panel2.Controls.Add(panel4);
            panel2.Controls.Add(panel3);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 54);
            panel2.Name = "panel2";
            panel2.Size = new Size(1141, 67);
            panel2.TabIndex = 1;
            // 
            // panel7
            // 
            panel7.BackColor = SystemColors.Window;
            panel7.BorderStyle = BorderStyle.Fixed3D;
            panel7.Controls.Add(btn_salidas);
            panel7.Controls.Add(lbl_salidas);
            panel7.Controls.Add(label7);
            panel7.Dock = DockStyle.Left;
            panel7.Location = new Point(610, 0);
            panel7.Name = "panel7";
            panel7.Size = new Size(255, 63);
            panel7.TabIndex = 3;
            // 
            // btn_salidas
            // 
            btn_salidas.BackColor = Color.Transparent;
            btn_salidas.Dock = DockStyle.Right;
            btn_salidas.FlatAppearance.BorderSize = 0;
            btn_salidas.FlatAppearance.MouseDownBackColor = Color.DimGray;
            btn_salidas.FlatStyle = FlatStyle.Flat;
            btn_salidas.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btn_salidas.ForeColor = SystemColors.ControlText;
            btn_salidas.Image = (Image)resources.GetObject("btn_salidas.Image");
            btn_salidas.Location = new Point(167, 0);
            btn_salidas.Name = "btn_salidas";
            btn_salidas.Size = new Size(84, 59);
            btn_salidas.TabIndex = 10;
            btn_salidas.TextAlign = ContentAlignment.BottomCenter;
            btn_salidas.TextImageRelation = TextImageRelation.ImageAboveText;
            btn_salidas.UseVisualStyleBackColor = false;
            btn_salidas.Click += btn_salidas_Click;
            // 
            // lbl_salidas
            // 
            lbl_salidas.AutoSize = true;
            lbl_salidas.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbl_salidas.Location = new Point(4, 33);
            lbl_salidas.Name = "lbl_salidas";
            lbl_salidas.Size = new Size(17, 21);
            lbl_salidas.TabIndex = 4;
            lbl_salidas.Text = "_";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.Location = new Point(4, 6);
            label7.Name = "label7";
            label7.Size = new Size(84, 21);
            label7.TabIndex = 3;
            label7.Text = "SALIDAS $";
            // 
            // panel5
            // 
            panel5.BackColor = SystemColors.Window;
            panel5.BorderStyle = BorderStyle.Fixed3D;
            panel5.Controls.Add(btn_egresos);
            panel5.Controls.Add(lbl_gastos);
            panel5.Controls.Add(label6);
            panel5.Location = new Point(865, 0);
            panel5.Name = "panel5";
            panel5.Size = new Size(272, 63);
            panel5.TabIndex = 2;
            // 
            // btn_egresos
            // 
            btn_egresos.BackColor = Color.Transparent;
            btn_egresos.Dock = DockStyle.Right;
            btn_egresos.FlatAppearance.BorderSize = 0;
            btn_egresos.FlatAppearance.MouseDownBackColor = Color.DimGray;
            btn_egresos.FlatStyle = FlatStyle.Flat;
            btn_egresos.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btn_egresos.ForeColor = SystemColors.ControlText;
            btn_egresos.Image = (Image)resources.GetObject("btn_egresos.Image");
            btn_egresos.Location = new Point(184, 0);
            btn_egresos.Name = "btn_egresos";
            btn_egresos.Size = new Size(84, 59);
            btn_egresos.TabIndex = 10;
            btn_egresos.TextAlign = ContentAlignment.BottomCenter;
            btn_egresos.TextImageRelation = TextImageRelation.ImageAboveText;
            btn_egresos.UseVisualStyleBackColor = false;
            btn_egresos.Click += btn_egresos_Click;
            // 
            // lbl_gastos
            // 
            lbl_gastos.AutoSize = true;
            lbl_gastos.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbl_gastos.Location = new Point(4, 33);
            lbl_gastos.Name = "lbl_gastos";
            lbl_gastos.Size = new Size(17, 21);
            lbl_gastos.TabIndex = 4;
            lbl_gastos.Text = "_";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.Location = new Point(4, 6);
            label6.Name = "label6";
            label6.Size = new Size(90, 21);
            label6.TabIndex = 3;
            label6.Text = "EGRESOS $";
            // 
            // panel4
            // 
            panel4.BackColor = SystemColors.Window;
            panel4.BorderStyle = BorderStyle.Fixed3D;
            panel4.Controls.Add(btn_detalle_venta);
            panel4.Controls.Add(label3);
            panel4.Controls.Add(lbl_ventas_totales);
            panel4.Dock = DockStyle.Left;
            panel4.Location = new Point(291, 0);
            panel4.Name = "panel4";
            panel4.Size = new Size(319, 63);
            panel4.TabIndex = 1;
            // 
            // btn_detalle_venta
            // 
            btn_detalle_venta.BackColor = Color.Transparent;
            btn_detalle_venta.Dock = DockStyle.Right;
            btn_detalle_venta.FlatAppearance.BorderSize = 0;
            btn_detalle_venta.FlatAppearance.MouseDownBackColor = Color.DimGray;
            btn_detalle_venta.FlatStyle = FlatStyle.Flat;
            btn_detalle_venta.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btn_detalle_venta.ForeColor = SystemColors.ControlText;
            btn_detalle_venta.Image = (Image)resources.GetObject("btn_detalle_venta.Image");
            btn_detalle_venta.Location = new Point(231, 0);
            btn_detalle_venta.Name = "btn_detalle_venta";
            btn_detalle_venta.Size = new Size(84, 59);
            btn_detalle_venta.TabIndex = 9;
            btn_detalle_venta.TextAlign = ContentAlignment.BottomCenter;
            btn_detalle_venta.TextImageRelation = TextImageRelation.ImageAboveText;
            btn_detalle_venta.UseVisualStyleBackColor = false;
            btn_detalle_venta.Click += btn_detalle_venta_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(4, 6);
            label3.Name = "label3";
            label3.Size = new Size(79, 21);
            label3.TabIndex = 3;
            label3.Text = "VENTAS $";
            // 
            // lbl_ventas_totales
            // 
            lbl_ventas_totales.AutoSize = true;
            lbl_ventas_totales.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbl_ventas_totales.Location = new Point(4, 33);
            lbl_ventas_totales.Name = "lbl_ventas_totales";
            lbl_ventas_totales.Size = new Size(17, 21);
            lbl_ventas_totales.TabIndex = 4;
            lbl_ventas_totales.Text = "_";
            // 
            // panel3
            // 
            panel3.BackColor = SystemColors.Window;
            panel3.BorderStyle = BorderStyle.Fixed3D;
            panel3.Controls.Add(btn_detalle_compras);
            panel3.Controls.Add(lbl_compras);
            panel3.Controls.Add(label4);
            panel3.Dock = DockStyle.Left;
            panel3.Location = new Point(0, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(291, 63);
            panel3.TabIndex = 0;
            // 
            // btn_detalle_compras
            // 
            btn_detalle_compras.BackColor = Color.Transparent;
            btn_detalle_compras.Dock = DockStyle.Right;
            btn_detalle_compras.FlatAppearance.BorderSize = 0;
            btn_detalle_compras.FlatAppearance.MouseDownBackColor = Color.DimGray;
            btn_detalle_compras.FlatStyle = FlatStyle.Flat;
            btn_detalle_compras.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btn_detalle_compras.ForeColor = SystemColors.ControlText;
            btn_detalle_compras.Image = (Image)resources.GetObject("btn_detalle_compras.Image");
            btn_detalle_compras.Location = new Point(203, 0);
            btn_detalle_compras.Name = "btn_detalle_compras";
            btn_detalle_compras.Size = new Size(84, 59);
            btn_detalle_compras.TabIndex = 10;
            btn_detalle_compras.TextAlign = ContentAlignment.BottomCenter;
            btn_detalle_compras.TextImageRelation = TextImageRelation.ImageAboveText;
            btn_detalle_compras.UseVisualStyleBackColor = false;
            btn_detalle_compras.Click += btn_detalle_compras_Click;
            // 
            // lbl_compras
            // 
            lbl_compras.AutoSize = true;
            lbl_compras.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbl_compras.Location = new Point(3, 33);
            lbl_compras.Name = "lbl_compras";
            lbl_compras.Size = new Size(17, 21);
            lbl_compras.TabIndex = 4;
            lbl_compras.Text = "_";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(3, 6);
            label4.Name = "label4";
            label4.Size = new Size(97, 21);
            label4.TabIndex = 3;
            label4.Text = "COMPRAS $";
            // 
            // panel6
            // 
            panel6.BackColor = SystemColors.Window;
            panel6.BorderStyle = BorderStyle.Fixed3D;
            panel6.Controls.Add(lbl_resultado);
            panel6.Controls.Add(label2);
            panel6.Dock = DockStyle.Top;
            panel6.Location = new Point(0, 121);
            panel6.Name = "panel6";
            panel6.Size = new Size(1141, 43);
            panel6.TabIndex = 2;
            // 
            // lbl_resultado
            // 
            lbl_resultado.AutoSize = true;
            lbl_resultado.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbl_resultado.Location = new Point(546, 11);
            lbl_resultado.Name = "lbl_resultado";
            lbl_resultado.Size = new Size(17, 21);
            lbl_resultado.TabIndex = 5;
            lbl_resultado.Text = "_";
            lbl_resultado.Visible = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(432, 11);
            label2.Name = "label2";
            label2.Size = new Size(108, 21);
            label2.TabIndex = 4;
            label2.Text = "RESULTADO $";
            label2.Visible = false;
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
            chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SeaGreen;
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            series1.YValuesPerPoint = 6;
            chart1.Series.Add(series1);
            chart1.Size = new Size(1137, 378);
            chart1.TabIndex = 5;
            chart1.Text = "chart1";
            title1.Name = "Title1";
            title1.Text = "Resumen";
            chart1.Titles.Add(title1);
            // 
            // panel8
            // 
            panel8.BorderStyle = BorderStyle.Fixed3D;
            panel8.Controls.Add(chart1);
            panel8.Dock = DockStyle.Fill;
            panel8.Location = new Point(0, 164);
            panel8.Name = "panel8";
            panel8.Size = new Size(1141, 382);
            panel8.TabIndex = 4;
            // 
            // ReporteGeneral
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1141, 546);
            Controls.Add(panel8);
            Controls.Add(panel6);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MaximumSize = new Size(1157, 585);
            MinimumSize = new Size(1157, 585);
            Name = "ReporteGeneral";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ReporteGeneral";
            Load += ReporteGeneral_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel7.ResumeLayout(false);
            panel7.PerformLayout();
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel6.ResumeLayout(false);
            panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)chart1).EndInit();
            panel8.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private Button btn_buscar;
        private DateTimePicker dtp_fecha_fin;
        private DateTimePicker dtp_fecha_inicio;
        private Label label1;
        private Label lbl_fechaVencimiento;
        private Panel panel3;
        private Panel panel5;
        private Panel panel4;
        private Label lbl_ventas_totales;
        private Label label3;
        private Label lbl_gastos;
        private Label label6;
        private Label lbl_compras;
        private Label label4;
        private Panel panel6;
        private Panel panel8;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private Label label2;
        private Label lbl_resultado;
        private Button btn_detalle_venta;
        private Button btn_egresos;
        private Button btn_detalle_compras;
        private Panel panel7;
        private Button btn_salidas;
        private Label lbl_salidas;
        private Label label7;
    }
}