﻿namespace sbx
{
    partial class Reportes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Reportes));
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            panel1 = new Panel();
            btn_buscar = new Button();
            btn_exportar_excel = new Button();
            dtp_fecha_fin = new DateTimePicker();
            dtp_fecha_inicio = new DateTimePicker();
            label1 = new Label();
            lbl_fechaVencimiento = new Label();
            btn_imprimir_pdf = new Button();
            cbx_tipo_reporte = new ComboBox();
            cbx_tipo_filtro = new ComboBox();
            cbx_campo_filtro = new ComboBox();
            txt_buscar = new TextBox();
            panel2 = new Panel();
            panel4 = new Panel();
            lbl_cantidad_resumen = new Label();
            label7 = new Label();
            lbl_ventas = new Label();
            label9 = new Label();
            lbl_costos = new Label();
            label5 = new Label();
            lbl_ganancia = new Label();
            label3 = new Label();
            lbl_total = new Label();
            label12 = new Label();
            lbl_impuesto = new Label();
            label10 = new Label();
            lbl_descuento = new Label();
            label8 = new Label();
            lbl_subtotal = new Label();
            label6 = new Label();
            lbl_cantidadProductos = new Label();
            label4 = new Label();
            panel3 = new Panel();
            dtg_reportes = new DataGridView();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtg_reportes).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.Window;
            panel1.BorderStyle = BorderStyle.Fixed3D;
            panel1.Controls.Add(btn_buscar);
            panel1.Controls.Add(btn_exportar_excel);
            panel1.Controls.Add(dtp_fecha_fin);
            panel1.Controls.Add(dtp_fecha_inicio);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(lbl_fechaVencimiento);
            panel1.Controls.Add(btn_imprimir_pdf);
            panel1.Controls.Add(cbx_tipo_reporte);
            panel1.Controls.Add(cbx_tipo_filtro);
            panel1.Controls.Add(cbx_campo_filtro);
            panel1.Controls.Add(txt_buscar);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1242, 56);
            panel1.TabIndex = 0;
            // 
            // btn_buscar
            // 
            btn_buscar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btn_buscar.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_buscar.FlatStyle = FlatStyle.Flat;
            btn_buscar.Image = (Image)resources.GetObject("btn_buscar.Image");
            btn_buscar.Location = new Point(1207, 20);
            btn_buscar.Name = "btn_buscar";
            btn_buscar.Size = new Size(26, 26);
            btn_buscar.TabIndex = 9;
            btn_buscar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_buscar.UseVisualStyleBackColor = true;
            btn_buscar.Click += btn_buscar_Click;
            // 
            // btn_exportar_excel
            // 
            btn_exportar_excel.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_exportar_excel.FlatStyle = FlatStyle.Flat;
            btn_exportar_excel.Image = (Image)resources.GetObject("btn_exportar_excel.Image");
            btn_exportar_excel.Location = new Point(74, 5);
            btn_exportar_excel.Name = "btn_exportar_excel";
            btn_exportar_excel.Size = new Size(70, 45);
            btn_exportar_excel.TabIndex = 2;
            btn_exportar_excel.Text = "Excel";
            btn_exportar_excel.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_exportar_excel.UseVisualStyleBackColor = true;
            btn_exportar_excel.Visible = false;
            // 
            // dtp_fecha_fin
            // 
            dtp_fecha_fin.Format = DateTimePickerFormat.Short;
            dtp_fecha_fin.Location = new Point(376, 23);
            dtp_fecha_fin.Name = "dtp_fecha_fin";
            dtp_fecha_fin.Size = new Size(187, 23);
            dtp_fecha_fin.TabIndex = 4;
            // 
            // dtp_fecha_inicio
            // 
            dtp_fecha_inicio.Format = DateTimePickerFormat.Short;
            dtp_fecha_inicio.Location = new Point(162, 23);
            dtp_fecha_inicio.Name = "dtp_fecha_inicio";
            dtp_fecha_inicio.Size = new Size(200, 23);
            dtp_fecha_inicio.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(376, 5);
            label1.Name = "label1";
            label1.Size = new Size(55, 15);
            label1.TabIndex = 155;
            label1.Text = "Fecha fin";
            // 
            // lbl_fechaVencimiento
            // 
            lbl_fechaVencimiento.AutoSize = true;
            lbl_fechaVencimiento.Location = new Point(162, 5);
            lbl_fechaVencimiento.Name = "lbl_fechaVencimiento";
            lbl_fechaVencimiento.Size = new Size(70, 15);
            lbl_fechaVencimiento.TabIndex = 154;
            lbl_fechaVencimiento.Text = "Fecha inicio";
            // 
            // btn_imprimir_pdf
            // 
            btn_imprimir_pdf.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_imprimir_pdf.FlatStyle = FlatStyle.Flat;
            btn_imprimir_pdf.Image = (Image)resources.GetObject("btn_imprimir_pdf.Image");
            btn_imprimir_pdf.Location = new Point(3, 5);
            btn_imprimir_pdf.Name = "btn_imprimir_pdf";
            btn_imprimir_pdf.Size = new Size(65, 45);
            btn_imprimir_pdf.TabIndex = 1;
            btn_imprimir_pdf.Text = "Pdf";
            btn_imprimir_pdf.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_imprimir_pdf.UseVisualStyleBackColor = true;
            btn_imprimir_pdf.Click += btn_imprimir_pdf_Click;
            // 
            // cbx_tipo_reporte
            // 
            cbx_tipo_reporte.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cbx_tipo_reporte.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_tipo_reporte.FormattingEnabled = true;
            cbx_tipo_reporte.Items.AddRange(new object[] { "Resumen por factura - Ganancias y perdidas", "Resumen - Ganancias y perdidas", "Detallado -  Ganancias y perdidas" });
            cbx_tipo_reporte.Location = new Point(592, 23);
            cbx_tipo_reporte.Name = "cbx_tipo_reporte";
            cbx_tipo_reporte.Size = new Size(213, 23);
            cbx_tipo_reporte.TabIndex = 5;
            cbx_tipo_reporte.SelectedValueChanged += cbx_tipo_reporte_SelectedValueChanged;
            // 
            // cbx_tipo_filtro
            // 
            cbx_tipo_filtro.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cbx_tipo_filtro.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_tipo_filtro.FormattingEnabled = true;
            cbx_tipo_filtro.Items.AddRange(new object[] { "Inicia con", "Igual a", "Contiene" });
            cbx_tipo_filtro.Location = new Point(931, 23);
            cbx_tipo_filtro.Name = "cbx_tipo_filtro";
            cbx_tipo_filtro.Size = new Size(87, 23);
            cbx_tipo_filtro.TabIndex = 7;
            // 
            // cbx_campo_filtro
            // 
            cbx_campo_filtro.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cbx_campo_filtro.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_campo_filtro.FormattingEnabled = true;
            cbx_campo_filtro.Location = new Point(811, 23);
            cbx_campo_filtro.Name = "cbx_campo_filtro";
            cbx_campo_filtro.Size = new Size(114, 23);
            cbx_campo_filtro.TabIndex = 6;
            // 
            // txt_buscar
            // 
            txt_buscar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txt_buscar.Location = new Point(1024, 23);
            txt_buscar.Name = "txt_buscar";
            txt_buscar.Size = new Size(177, 23);
            txt_buscar.TabIndex = 8;
            txt_buscar.KeyPress += txt_buscar_KeyPress;
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.Window;
            panel2.Controls.Add(panel4);
            panel2.Controls.Add(lbl_total);
            panel2.Controls.Add(label12);
            panel2.Controls.Add(lbl_impuesto);
            panel2.Controls.Add(label10);
            panel2.Controls.Add(lbl_descuento);
            panel2.Controls.Add(label8);
            panel2.Controls.Add(lbl_subtotal);
            panel2.Controls.Add(label6);
            panel2.Controls.Add(lbl_cantidadProductos);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(panel3);
            panel2.Dock = DockStyle.Bottom;
            panel2.Location = new Point(0, 466);
            panel2.Name = "panel2";
            panel2.Size = new Size(1242, 181);
            panel2.TabIndex = 7;
            // 
            // panel4
            // 
            panel4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            panel4.BorderStyle = BorderStyle.FixedSingle;
            panel4.Controls.Add(lbl_cantidad_resumen);
            panel4.Controls.Add(label7);
            panel4.Controls.Add(lbl_ventas);
            panel4.Controls.Add(label9);
            panel4.Controls.Add(lbl_costos);
            panel4.Controls.Add(label5);
            panel4.Controls.Add(lbl_ganancia);
            panel4.Controls.Add(label3);
            panel4.Location = new Point(821, 9);
            panel4.Name = "panel4";
            panel4.Size = new Size(414, 165);
            panel4.TabIndex = 186;
            // 
            // lbl_cantidad_resumen
            // 
            lbl_cantidad_resumen.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lbl_cantidad_resumen.AutoSize = true;
            lbl_cantidad_resumen.Font = new Font("Segoe UI", 12F);
            lbl_cantidad_resumen.ForeColor = SystemColors.ControlDarkDark;
            lbl_cantidad_resumen.Location = new Point(113, 36);
            lbl_cantidad_resumen.Name = "lbl_cantidad_resumen";
            lbl_cantidad_resumen.RightToLeft = RightToLeft.No;
            lbl_cantidad_resumen.Size = new Size(17, 21);
            lbl_cantidad_resumen.TabIndex = 192;
            lbl_cantidad_resumen.Text = "_";
            lbl_cantidad_resumen.Visible = false;
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 12F);
            label7.ForeColor = SystemColors.ControlDarkDark;
            label7.Location = new Point(3, 36);
            label7.Name = "label7";
            label7.Size = new Size(112, 21);
            label7.TabIndex = 191;
            label7.Text = "Total cantidad: ";
            label7.Visible = false;
            // 
            // lbl_ventas
            // 
            lbl_ventas.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lbl_ventas.AutoSize = true;
            lbl_ventas.Font = new Font("Segoe UI", 12F);
            lbl_ventas.ForeColor = SystemColors.ControlDarkDark;
            lbl_ventas.Location = new Point(113, 67);
            lbl_ventas.Name = "lbl_ventas";
            lbl_ventas.RightToLeft = RightToLeft.No;
            lbl_ventas.Size = new Size(17, 21);
            lbl_ventas.TabIndex = 190;
            lbl_ventas.Text = "_";
            // 
            // label9
            // 
            label9.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 12F);
            label9.ForeColor = SystemColors.ControlDarkDark;
            label9.Location = new Point(3, 67);
            label9.Name = "label9";
            label9.Size = new Size(94, 21);
            label9.TabIndex = 189;
            label9.Text = "Total ventas:";
            // 
            // lbl_costos
            // 
            lbl_costos.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lbl_costos.AutoSize = true;
            lbl_costos.Font = new Font("Segoe UI", 12F);
            lbl_costos.ForeColor = SystemColors.ControlDarkDark;
            lbl_costos.Location = new Point(113, 100);
            lbl_costos.Name = "lbl_costos";
            lbl_costos.RightToLeft = RightToLeft.No;
            lbl_costos.Size = new Size(17, 21);
            lbl_costos.TabIndex = 188;
            lbl_costos.Text = "_";
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F);
            label5.ForeColor = SystemColors.ControlDarkDark;
            label5.Location = new Point(3, 100);
            label5.Name = "label5";
            label5.Size = new Size(93, 21);
            label5.TabIndex = 187;
            label5.Text = "Total costos:";
            // 
            // lbl_ganancia
            // 
            lbl_ganancia.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lbl_ganancia.AutoSize = true;
            lbl_ganancia.Font = new Font("Segoe UI", 12F);
            lbl_ganancia.ForeColor = SystemColors.ControlDarkDark;
            lbl_ganancia.Location = new Point(113, 132);
            lbl_ganancia.Name = "lbl_ganancia";
            lbl_ganancia.RightToLeft = RightToLeft.No;
            lbl_ganancia.Size = new Size(17, 21);
            lbl_ganancia.TabIndex = 186;
            lbl_ganancia.Text = "_";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F);
            label3.ForeColor = SystemColors.ControlDarkDark;
            label3.Location = new Point(3, 132);
            label3.Name = "label3";
            label3.Size = new Size(111, 21);
            label3.TabIndex = 185;
            label3.Text = "Total ganancia:";
            // 
            // lbl_total
            // 
            lbl_total.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lbl_total.AutoSize = true;
            lbl_total.Font = new Font("Segoe UI", 12F);
            lbl_total.ForeColor = SystemColors.ControlDarkDark;
            lbl_total.Location = new Point(493, 142);
            lbl_total.Name = "lbl_total";
            lbl_total.RightToLeft = RightToLeft.No;
            lbl_total.Size = new Size(17, 21);
            lbl_total.TabIndex = 184;
            lbl_total.Text = "_";
            // 
            // label12
            // 
            label12.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label12.AutoSize = true;
            label12.Font = new Font("Segoe UI", 12F);
            label12.ForeColor = SystemColors.ControlDarkDark;
            label12.Location = new Point(408, 142);
            label12.Name = "label12";
            label12.Size = new Size(49, 21);
            label12.TabIndex = 183;
            label12.Text = "Total: ";
            // 
            // lbl_impuesto
            // 
            lbl_impuesto.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lbl_impuesto.AutoSize = true;
            lbl_impuesto.Font = new Font("Segoe UI", 12F);
            lbl_impuesto.ForeColor = SystemColors.ControlDarkDark;
            lbl_impuesto.Location = new Point(493, 110);
            lbl_impuesto.Name = "lbl_impuesto";
            lbl_impuesto.RightToLeft = RightToLeft.No;
            lbl_impuesto.Size = new Size(17, 21);
            lbl_impuesto.TabIndex = 182;
            lbl_impuesto.Text = "_";
            // 
            // label10
            // 
            label10.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 12F);
            label10.ForeColor = SystemColors.ControlDarkDark;
            label10.Location = new Point(408, 110);
            label10.Name = "label10";
            label10.Size = new Size(82, 21);
            label10.TabIndex = 181;
            label10.Text = "Impuesto: ";
            // 
            // lbl_descuento
            // 
            lbl_descuento.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lbl_descuento.AutoSize = true;
            lbl_descuento.Font = new Font("Segoe UI", 12F);
            lbl_descuento.ForeColor = SystemColors.ControlDarkDark;
            lbl_descuento.Location = new Point(493, 77);
            lbl_descuento.Name = "lbl_descuento";
            lbl_descuento.RightToLeft = RightToLeft.No;
            lbl_descuento.Size = new Size(17, 21);
            lbl_descuento.TabIndex = 180;
            lbl_descuento.Text = "_";
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 12F);
            label8.ForeColor = SystemColors.ControlDarkDark;
            label8.Location = new Point(408, 77);
            label8.Name = "label8";
            label8.Size = new Size(90, 21);
            label8.TabIndex = 179;
            label8.Text = "Descuento: ";
            // 
            // lbl_subtotal
            // 
            lbl_subtotal.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lbl_subtotal.AutoSize = true;
            lbl_subtotal.Font = new Font("Segoe UI", 12F);
            lbl_subtotal.ForeColor = SystemColors.ControlDarkDark;
            lbl_subtotal.Location = new Point(493, 46);
            lbl_subtotal.Name = "lbl_subtotal";
            lbl_subtotal.RightToLeft = RightToLeft.No;
            lbl_subtotal.Size = new Size(17, 21);
            lbl_subtotal.TabIndex = 178;
            lbl_subtotal.Text = "_";
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 12F);
            label6.ForeColor = SystemColors.ControlDarkDark;
            label6.Location = new Point(408, 46);
            label6.Name = "label6";
            label6.Size = new Size(75, 21);
            label6.TabIndex = 177;
            label6.Text = "Subtotal: ";
            // 
            // lbl_cantidadProductos
            // 
            lbl_cantidadProductos.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lbl_cantidadProductos.AutoSize = true;
            lbl_cantidadProductos.Font = new Font("Segoe UI", 12F);
            lbl_cantidadProductos.ForeColor = SystemColors.ControlDarkDark;
            lbl_cantidadProductos.Location = new Point(493, 15);
            lbl_cantidadProductos.Name = "lbl_cantidadProductos";
            lbl_cantidadProductos.RightToLeft = RightToLeft.No;
            lbl_cantidadProductos.Size = new Size(17, 21);
            lbl_cantidadProductos.TabIndex = 176;
            lbl_cantidadProductos.Text = "_";
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F);
            label4.ForeColor = SystemColors.ControlDarkDark;
            label4.Location = new Point(408, 15);
            label4.Name = "label4";
            label4.Size = new Size(79, 21);
            label4.TabIndex = 175;
            label4.Text = "Cantidad: ";
            // 
            // panel3
            // 
            panel3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            panel3.BorderStyle = BorderStyle.FixedSingle;
            panel3.Location = new Point(405, 9);
            panel3.Name = "panel3";
            panel3.Size = new Size(414, 165);
            panel3.TabIndex = 185;
            // 
            // dtg_reportes
            // 
            dtg_reportes.AllowUserToAddRows = false;
            dtg_reportes.AllowUserToDeleteRows = false;
            dtg_reportes.AllowUserToOrderColumns = true;
            dtg_reportes.BackgroundColor = SystemColors.Control;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.ScrollBar;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dtg_reportes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dtg_reportes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(172, 211, 236);
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dtg_reportes.DefaultCellStyle = dataGridViewCellStyle2;
            dtg_reportes.Dock = DockStyle.Fill;
            dtg_reportes.Location = new Point(0, 56);
            dtg_reportes.Name = "dtg_reportes";
            dtg_reportes.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = Color.Silver;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dtg_reportes.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dtg_reportes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtg_reportes.Size = new Size(1242, 410);
            dtg_reportes.TabIndex = 8;
            // 
            // Reportes
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1242, 647);
            Controls.Add(dtg_reportes);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Reportes";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Reportes";
            Load += Reportes_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dtg_reportes).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private DateTimePicker dtp_fecha_fin;
        private DateTimePicker dtp_fecha_inicio;
        private Label label1;
        private Label lbl_fechaVencimiento;
        private Button btn_imprimir_pdf;
        private ComboBox cbx_tipo_reporte;
        private ComboBox cbx_tipo_filtro;
        private ComboBox cbx_campo_filtro;
        private TextBox txt_buscar;
        private Button btn_exportar_excel;
        private Button btn_buscar;
        private Panel panel2;
        private DataGridView dtg_reportes;
        private Label lbl_total;
        private Label label12;
        private Label lbl_impuesto;
        private Label label10;
        private Label lbl_descuento;
        private Label label8;
        private Label lbl_subtotal;
        private Label label6;
        private Label lbl_cantidadProductos;
        private Label label4;
        private Panel panel3;
        private Label lbl_ganancia;
        private Label label3;
        private Panel panel4;
        private Label lbl_costos;
        private Label label5;
        private Label lbl_ventas;
        private Label label9;
        private Label lbl_cantidad_resumen;
        private Label label7;
    }
}