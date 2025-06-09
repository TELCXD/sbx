namespace sbx
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
            btn_buscar.TabIndex = 159;
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
            btn_exportar_excel.TabIndex = 158;
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
            dtp_fecha_fin.TabIndex = 157;
            // 
            // dtp_fecha_inicio
            // 
            dtp_fecha_inicio.Format = DateTimePickerFormat.Short;
            dtp_fecha_inicio.Location = new Point(162, 23);
            dtp_fecha_inicio.Name = "dtp_fecha_inicio";
            dtp_fecha_inicio.Size = new Size(200, 23);
            dtp_fecha_inicio.TabIndex = 156;
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
            btn_imprimir_pdf.TabIndex = 153;
            btn_imprimir_pdf.Text = "Pdf";
            btn_imprimir_pdf.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_imprimir_pdf.UseVisualStyleBackColor = true;
            btn_imprimir_pdf.Visible = false;
            // 
            // cbx_tipo_reporte
            // 
            cbx_tipo_reporte.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cbx_tipo_reporte.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_tipo_reporte.FormattingEnabled = true;
            cbx_tipo_reporte.Items.AddRange(new object[] { "Resumen - Ganancias y perdidas", "Detallado -  Ganancias y perdidas" });
            cbx_tipo_reporte.Location = new Point(592, 23);
            cbx_tipo_reporte.Name = "cbx_tipo_reporte";
            cbx_tipo_reporte.Size = new Size(213, 23);
            cbx_tipo_reporte.TabIndex = 152;
            cbx_tipo_reporte.SelectedValueChanged += cbx_client_venta_SelectedValueChanged;
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
            cbx_tipo_filtro.TabIndex = 151;
            // 
            // cbx_campo_filtro
            // 
            cbx_campo_filtro.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cbx_campo_filtro.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_campo_filtro.FormattingEnabled = true;
            cbx_campo_filtro.Location = new Point(811, 23);
            cbx_campo_filtro.Name = "cbx_campo_filtro";
            cbx_campo_filtro.Size = new Size(114, 23);
            cbx_campo_filtro.TabIndex = 150;
            // 
            // txt_buscar
            // 
            txt_buscar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txt_buscar.Location = new Point(1024, 23);
            txt_buscar.Name = "txt_buscar";
            txt_buscar.Size = new Size(177, 23);
            txt_buscar.TabIndex = 148;
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.Window;
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
            panel2.Location = new Point(0, 478);
            panel2.Name = "panel2";
            panel2.Size = new Size(1242, 169);
            panel2.TabIndex = 7;
            // 
            // lbl_total
            // 
            lbl_total.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lbl_total.AutoSize = true;
            lbl_total.Font = new Font("Segoe UI", 12F);
            lbl_total.ForeColor = SystemColors.ControlDarkDark;
            lbl_total.Location = new Point(912, 138);
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
            label12.Location = new Point(827, 138);
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
            lbl_impuesto.Location = new Point(912, 106);
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
            label10.Location = new Point(827, 106);
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
            lbl_descuento.Location = new Point(912, 73);
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
            label8.Location = new Point(827, 73);
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
            lbl_subtotal.Location = new Point(912, 42);
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
            label6.Location = new Point(827, 42);
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
            lbl_cantidadProductos.Location = new Point(912, 11);
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
            label4.Location = new Point(827, 11);
            label4.Name = "label4";
            label4.Size = new Size(79, 21);
            label4.TabIndex = 175;
            label4.Text = "Cantidad: ";
            // 
            // panel3
            // 
            panel3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            panel3.BorderStyle = BorderStyle.FixedSingle;
            panel3.Location = new Point(824, 5);
            panel3.Name = "panel3";
            panel3.Size = new Size(414, 160);
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
            dtg_reportes.Size = new Size(1242, 422);
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
            Name = "Reportes";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Reportes";
            Load += Reportes_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
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
    }
}