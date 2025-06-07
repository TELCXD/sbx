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
            cbx_client_venta = new ComboBox();
            cbx_tipo_filtro = new ComboBox();
            cbx_campo_filtro = new ComboBox();
            txt_buscar = new TextBox();
            dtg_reportes = new DataGridView();
            panel1.SuspendLayout();
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
            panel1.Controls.Add(cbx_client_venta);
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
            btn_buscar.Enabled = false;
            btn_buscar.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_buscar.FlatStyle = FlatStyle.Flat;
            btn_buscar.Image = (Image)resources.GetObject("btn_buscar.Image");
            btn_buscar.Location = new Point(1160, 20);
            btn_buscar.Name = "btn_buscar";
            btn_buscar.Size = new Size(26, 26);
            btn_buscar.TabIndex = 159;
            btn_buscar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_buscar.UseVisualStyleBackColor = true;
            btn_buscar.Click += btn_buscar_Click;
            // 
            // btn_exportar_excel
            // 
            btn_exportar_excel.Enabled = false;
            btn_exportar_excel.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_exportar_excel.FlatStyle = FlatStyle.Flat;
            btn_exportar_excel.Image = (Image)resources.GetObject("btn_exportar_excel.Image");
            btn_exportar_excel.Location = new Point(110, 5);
            btn_exportar_excel.Name = "btn_exportar_excel";
            btn_exportar_excel.Size = new Size(101, 45);
            btn_exportar_excel.TabIndex = 158;
            btn_exportar_excel.Text = "Excel";
            btn_exportar_excel.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_exportar_excel.UseVisualStyleBackColor = true;
            // 
            // dtp_fecha_fin
            // 
            dtp_fecha_fin.Format = DateTimePickerFormat.Short;
            dtp_fecha_fin.Location = new Point(479, 23);
            dtp_fecha_fin.Name = "dtp_fecha_fin";
            dtp_fecha_fin.Size = new Size(187, 23);
            dtp_fecha_fin.TabIndex = 157;
            // 
            // dtp_fecha_inicio
            // 
            dtp_fecha_inicio.Format = DateTimePickerFormat.Short;
            dtp_fecha_inicio.Location = new Point(254, 23);
            dtp_fecha_inicio.Name = "dtp_fecha_inicio";
            dtp_fecha_inicio.Size = new Size(200, 23);
            dtp_fecha_inicio.TabIndex = 156;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(479, 5);
            label1.Name = "label1";
            label1.Size = new Size(55, 15);
            label1.TabIndex = 155;
            label1.Text = "Fecha fin";
            // 
            // lbl_fechaVencimiento
            // 
            lbl_fechaVencimiento.AutoSize = true;
            lbl_fechaVencimiento.Location = new Point(254, 5);
            lbl_fechaVencimiento.Name = "lbl_fechaVencimiento";
            lbl_fechaVencimiento.Size = new Size(70, 15);
            lbl_fechaVencimiento.TabIndex = 154;
            lbl_fechaVencimiento.Text = "Fecha inicio";
            // 
            // btn_imprimir_pdf
            // 
            btn_imprimir_pdf.Enabled = false;
            btn_imprimir_pdf.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_imprimir_pdf.FlatStyle = FlatStyle.Flat;
            btn_imprimir_pdf.Image = (Image)resources.GetObject("btn_imprimir_pdf.Image");
            btn_imprimir_pdf.Location = new Point(3, 5);
            btn_imprimir_pdf.Name = "btn_imprimir_pdf";
            btn_imprimir_pdf.Size = new Size(101, 45);
            btn_imprimir_pdf.TabIndex = 153;
            btn_imprimir_pdf.Text = "Pdf";
            btn_imprimir_pdf.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_imprimir_pdf.UseVisualStyleBackColor = true;
            // 
            // cbx_client_venta
            // 
            cbx_client_venta.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cbx_client_venta.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_client_venta.FormattingEnabled = true;
            cbx_client_venta.Items.AddRange(new object[] { "Resumen - Ganancias y perdidas", "Detallado -  Ganancias y perdidas" });
            cbx_client_venta.Location = new Point(671, 23);
            cbx_client_venta.Name = "cbx_client_venta";
            cbx_client_venta.Size = new Size(87, 23);
            cbx_client_venta.TabIndex = 152;
            cbx_client_venta.SelectedValueChanged += cbx_client_venta_SelectedValueChanged;
            // 
            // cbx_tipo_filtro
            // 
            cbx_tipo_filtro.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cbx_tipo_filtro.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_tipo_filtro.FormattingEnabled = true;
            cbx_tipo_filtro.Items.AddRange(new object[] { "Inicia con", "Igual a", "Contiene" });
            cbx_tipo_filtro.Location = new Point(884, 23);
            cbx_tipo_filtro.Name = "cbx_tipo_filtro";
            cbx_tipo_filtro.Size = new Size(87, 23);
            cbx_tipo_filtro.TabIndex = 151;
            // 
            // cbx_campo_filtro
            // 
            cbx_campo_filtro.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cbx_campo_filtro.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_campo_filtro.FormattingEnabled = true;
            cbx_campo_filtro.Location = new Point(764, 23);
            cbx_campo_filtro.Name = "cbx_campo_filtro";
            cbx_campo_filtro.Size = new Size(114, 23);
            cbx_campo_filtro.TabIndex = 150;
            // 
            // txt_buscar
            // 
            txt_buscar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txt_buscar.Location = new Point(977, 23);
            txt_buscar.Name = "txt_buscar";
            txt_buscar.Size = new Size(177, 23);
            txt_buscar.TabIndex = 148;
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
            dtg_reportes.Size = new Size(1242, 394);
            dtg_reportes.TabIndex = 6;
            // 
            // Reportes
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1242, 450);
            Controls.Add(dtg_reportes);
            Controls.Add(panel1);
            Name = "Reportes";
            Text = "Reportes";
            Load += Reportes_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
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
        private ComboBox cbx_client_venta;
        private ComboBox cbx_tipo_filtro;
        private ComboBox cbx_campo_filtro;
        private TextBox txt_buscar;
        private Button btn_exportar_excel;
        private Button btn_buscar;
        private DataGridView dtg_reportes;
    }
}