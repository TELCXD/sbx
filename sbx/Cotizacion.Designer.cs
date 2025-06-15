namespace sbx
{
    partial class Cotizacion
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Cotizacion));
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            panel1 = new Panel();
            btn_imprimir = new Button();
            dtp_fecha_fin = new DateTimePicker();
            dtp_fecha_inicio = new DateTimePicker();
            label1 = new Label();
            lbl_fechaVencimiento = new Label();
            cbx_client_venta = new ComboBox();
            cbx_tipo_filtro = new ComboBox();
            cbx_campo_filtro = new ComboBox();
            btn_buscar = new Button();
            txt_buscar = new TextBox();
            errorProvider1 = new ErrorProvider(components);
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
            dtg_cotizaciones = new DataGridView();
            cl_id_cotizacion = new DataGridViewTextBoxColumn();
            cl_fecha = new DataGridViewTextBoxColumn();
            cl_cotizacion = new DataGridViewTextBoxColumn();
            cl_estado = new DataGridViewTextBoxColumn();
            cl_documento_cliente = new DataGridViewTextBoxColumn();
            cl_nombre_cliente = new DataGridViewTextBoxColumn();
            cl_idProducto = new DataGridViewTextBoxColumn();
            cl_sku = new DataGridViewTextBoxColumn();
            cl_codigo_barras = new DataGridViewTextBoxColumn();
            cl_nombre = new DataGridViewTextBoxColumn();
            cl_precio = new DataGridViewTextBoxColumn();
            cl_cantidad = new DataGridViewTextBoxColumn();
            cl_descuento = new DataGridViewTextBoxColumn();
            cl_iva = new DataGridViewTextBoxColumn();
            cl_total_linea = new DataGridViewTextBoxColumn();
            cl_usuario = new DataGridViewTextBoxColumn();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtg_cotizaciones).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.Window;
            panel1.BorderStyle = BorderStyle.Fixed3D;
            panel1.Controls.Add(btn_imprimir);
            panel1.Controls.Add(dtp_fecha_fin);
            panel1.Controls.Add(dtp_fecha_inicio);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(lbl_fechaVencimiento);
            panel1.Controls.Add(cbx_client_venta);
            panel1.Controls.Add(cbx_tipo_filtro);
            panel1.Controls.Add(cbx_campo_filtro);
            panel1.Controls.Add(btn_buscar);
            panel1.Controls.Add(txt_buscar);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1197, 56);
            panel1.TabIndex = 0;
            // 
            // btn_imprimir
            // 
            btn_imprimir.Enabled = false;
            btn_imprimir.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_imprimir.FlatStyle = FlatStyle.Flat;
            btn_imprimir.Image = (Image)resources.GetObject("btn_imprimir.Image");
            btn_imprimir.Location = new Point(3, 4);
            btn_imprimir.Name = "btn_imprimir";
            btn_imprimir.Size = new Size(101, 45);
            btn_imprimir.TabIndex = 157;
            btn_imprimir.Text = "Imprimir";
            btn_imprimir.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_imprimir.UseVisualStyleBackColor = true;
            btn_imprimir.Click += btn_imprimir_Click;
            // 
            // dtp_fecha_fin
            // 
            dtp_fecha_fin.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            dtp_fecha_fin.Format = DateTimePickerFormat.Short;
            dtp_fecha_fin.Location = new Point(470, 24);
            dtp_fecha_fin.Name = "dtp_fecha_fin";
            dtp_fecha_fin.Size = new Size(187, 23);
            dtp_fecha_fin.TabIndex = 156;
            // 
            // dtp_fecha_inicio
            // 
            dtp_fecha_inicio.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            dtp_fecha_inicio.Format = DateTimePickerFormat.Short;
            dtp_fecha_inicio.Location = new Point(264, 24);
            dtp_fecha_inicio.Name = "dtp_fecha_inicio";
            dtp_fecha_inicio.Size = new Size(200, 23);
            dtp_fecha_inicio.TabIndex = 155;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Location = new Point(470, 6);
            label1.Name = "label1";
            label1.Size = new Size(55, 15);
            label1.TabIndex = 154;
            label1.Text = "Fecha fin";
            // 
            // lbl_fechaVencimiento
            // 
            lbl_fechaVencimiento.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lbl_fechaVencimiento.AutoSize = true;
            lbl_fechaVencimiento.Location = new Point(264, 6);
            lbl_fechaVencimiento.Name = "lbl_fechaVencimiento";
            lbl_fechaVencimiento.Size = new Size(70, 15);
            lbl_fechaVencimiento.TabIndex = 153;
            lbl_fechaVencimiento.Text = "Fecha inicio";
            // 
            // cbx_client_venta
            // 
            cbx_client_venta.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cbx_client_venta.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_client_venta.FormattingEnabled = true;
            cbx_client_venta.Items.AddRange(new object[] { "Cotizacion", "Cliente", "Producto", "Usuario" });
            cbx_client_venta.Location = new Point(663, 24);
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
            cbx_tipo_filtro.Location = new Point(876, 24);
            cbx_tipo_filtro.Name = "cbx_tipo_filtro";
            cbx_tipo_filtro.Size = new Size(87, 23);
            cbx_tipo_filtro.TabIndex = 151;
            // 
            // cbx_campo_filtro
            // 
            cbx_campo_filtro.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cbx_campo_filtro.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_campo_filtro.FormattingEnabled = true;
            cbx_campo_filtro.Location = new Point(756, 24);
            cbx_campo_filtro.Name = "cbx_campo_filtro";
            cbx_campo_filtro.Size = new Size(114, 23);
            cbx_campo_filtro.TabIndex = 150;
            // 
            // btn_buscar
            // 
            btn_buscar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btn_buscar.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_buscar.FlatStyle = FlatStyle.Flat;
            btn_buscar.Image = (Image)resources.GetObject("btn_buscar.Image");
            btn_buscar.Location = new Point(1161, 22);
            btn_buscar.Name = "btn_buscar";
            btn_buscar.Size = new Size(26, 26);
            btn_buscar.TabIndex = 149;
            btn_buscar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_buscar.UseVisualStyleBackColor = true;
            btn_buscar.Click += btn_buscar_Click;
            // 
            // txt_buscar
            // 
            txt_buscar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txt_buscar.Location = new Point(969, 24);
            txt_buscar.Name = "txt_buscar";
            txt_buscar.Size = new Size(177, 23);
            txt_buscar.TabIndex = 148;
            txt_buscar.KeyPress += txt_buscar_KeyPress;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
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
            panel2.Location = new Point(0, 470);
            panel2.Name = "panel2";
            panel2.Size = new Size(1197, 170);
            panel2.TabIndex = 1;
            // 
            // lbl_total
            // 
            lbl_total.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lbl_total.AutoSize = true;
            lbl_total.Font = new Font("Segoe UI", 12F);
            lbl_total.ForeColor = SystemColors.ControlDarkDark;
            lbl_total.Location = new Point(873, 136);
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
            label12.Location = new Point(788, 136);
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
            lbl_impuesto.Location = new Point(873, 104);
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
            label10.Location = new Point(788, 104);
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
            lbl_descuento.Location = new Point(873, 71);
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
            label8.Location = new Point(788, 71);
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
            lbl_subtotal.Location = new Point(873, 40);
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
            label6.Location = new Point(788, 40);
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
            lbl_cantidadProductos.Location = new Point(873, 9);
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
            label4.Location = new Point(788, 9);
            label4.Name = "label4";
            label4.Size = new Size(79, 21);
            label4.TabIndex = 175;
            label4.Text = "Cantidad: ";
            // 
            // panel3
            // 
            panel3.BorderStyle = BorderStyle.FixedSingle;
            panel3.Location = new Point(780, 3);
            panel3.Name = "panel3";
            panel3.Size = new Size(414, 160);
            panel3.TabIndex = 185;
            // 
            // dtg_cotizaciones
            // 
            dtg_cotizaciones.AllowUserToAddRows = false;
            dtg_cotizaciones.AllowUserToDeleteRows = false;
            dtg_cotizaciones.AllowUserToOrderColumns = true;
            dtg_cotizaciones.BackgroundColor = SystemColors.Control;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.ScrollBar;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dtg_cotizaciones.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dtg_cotizaciones.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dtg_cotizaciones.Columns.AddRange(new DataGridViewColumn[] { cl_id_cotizacion, cl_fecha, cl_cotizacion, cl_estado, cl_documento_cliente, cl_nombre_cliente, cl_idProducto, cl_sku, cl_codigo_barras, cl_nombre, cl_precio, cl_cantidad, cl_descuento, cl_iva, cl_total_linea, cl_usuario });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(172, 211, 236);
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dtg_cotizaciones.DefaultCellStyle = dataGridViewCellStyle2;
            dtg_cotizaciones.Dock = DockStyle.Fill;
            dtg_cotizaciones.Location = new Point(0, 56);
            dtg_cotizaciones.Name = "dtg_cotizaciones";
            dtg_cotizaciones.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = Color.Silver;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dtg_cotizaciones.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dtg_cotizaciones.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtg_cotizaciones.Size = new Size(1197, 414);
            dtg_cotizaciones.TabIndex = 7;
            dtg_cotizaciones.DoubleClick += dtg_cotizaciones_DoubleClick;
            // 
            // cl_id_cotizacion
            // 
            cl_id_cotizacion.HeaderText = "Id cotizacion";
            cl_id_cotizacion.Name = "cl_id_cotizacion";
            cl_id_cotizacion.ReadOnly = true;
            cl_id_cotizacion.Visible = false;
            // 
            // cl_fecha
            // 
            cl_fecha.HeaderText = "Fecha";
            cl_fecha.Name = "cl_fecha";
            cl_fecha.ReadOnly = true;
            // 
            // cl_cotizacion
            // 
            cl_cotizacion.HeaderText = "Cotizacion";
            cl_cotizacion.Name = "cl_cotizacion";
            cl_cotizacion.ReadOnly = true;
            // 
            // cl_estado
            // 
            cl_estado.HeaderText = "Estado";
            cl_estado.Name = "cl_estado";
            cl_estado.ReadOnly = true;
            // 
            // cl_documento_cliente
            // 
            cl_documento_cliente.HeaderText = "Doc Cliente";
            cl_documento_cliente.Name = "cl_documento_cliente";
            cl_documento_cliente.ReadOnly = true;
            cl_documento_cliente.Width = 150;
            // 
            // cl_nombre_cliente
            // 
            cl_nombre_cliente.HeaderText = "Nombre cliente";
            cl_nombre_cliente.Name = "cl_nombre_cliente";
            cl_nombre_cliente.ReadOnly = true;
            cl_nombre_cliente.Width = 200;
            // 
            // cl_idProducto
            // 
            cl_idProducto.HeaderText = "Id";
            cl_idProducto.Name = "cl_idProducto";
            cl_idProducto.ReadOnly = true;
            // 
            // cl_sku
            // 
            cl_sku.HeaderText = "sku";
            cl_sku.Name = "cl_sku";
            cl_sku.ReadOnly = true;
            // 
            // cl_codigo_barras
            // 
            cl_codigo_barras.HeaderText = "Codigo b";
            cl_codigo_barras.Name = "cl_codigo_barras";
            cl_codigo_barras.ReadOnly = true;
            cl_codigo_barras.Visible = false;
            // 
            // cl_nombre
            // 
            cl_nombre.HeaderText = "Nombre";
            cl_nombre.Name = "cl_nombre";
            cl_nombre.ReadOnly = true;
            cl_nombre.Width = 200;
            // 
            // cl_precio
            // 
            cl_precio.HeaderText = "Precio";
            cl_precio.Name = "cl_precio";
            cl_precio.ReadOnly = true;
            // 
            // cl_cantidad
            // 
            cl_cantidad.HeaderText = "Cantidad";
            cl_cantidad.Name = "cl_cantidad";
            cl_cantidad.ReadOnly = true;
            // 
            // cl_descuento
            // 
            cl_descuento.HeaderText = "Desc (%)";
            cl_descuento.Name = "cl_descuento";
            cl_descuento.ReadOnly = true;
            // 
            // cl_iva
            // 
            cl_iva.HeaderText = "Iva (%)";
            cl_iva.Name = "cl_iva";
            cl_iva.ReadOnly = true;
            cl_iva.Width = 80;
            // 
            // cl_total_linea
            // 
            cl_total_linea.HeaderText = "Total linea";
            cl_total_linea.Name = "cl_total_linea";
            cl_total_linea.ReadOnly = true;
            // 
            // cl_usuario
            // 
            cl_usuario.HeaderText = "Usuario";
            cl_usuario.Name = "cl_usuario";
            cl_usuario.ReadOnly = true;
            // 
            // Cotizacion
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1197, 640);
            Controls.Add(dtg_cotizaciones);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MaximumSize = new Size(1213, 679);
            MinimumSize = new Size(1213, 679);
            Name = "Cotizacion";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Cotizacion";
            Load += Cotizacion_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dtg_cotizaciones).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private DateTimePicker dtp_fecha_fin;
        private DateTimePicker dtp_fecha_inicio;
        private Label label1;
        private Label lbl_fechaVencimiento;
        private ComboBox cbx_client_venta;
        private ComboBox cbx_tipo_filtro;
        private ComboBox cbx_campo_filtro;
        private Button btn_buscar;
        private TextBox txt_buscar;
        private Button btn_imprimir;
        private ErrorProvider errorProvider1;
        private DataGridView dtg_cotizaciones;
        private Panel panel2;
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
        private DataGridViewTextBoxColumn cl_id_cotizacion;
        private DataGridViewTextBoxColumn cl_fecha;
        private DataGridViewTextBoxColumn cl_cotizacion;
        private DataGridViewTextBoxColumn cl_estado;
        private DataGridViewTextBoxColumn cl_documento_cliente;
        private DataGridViewTextBoxColumn cl_nombre_cliente;
        private DataGridViewTextBoxColumn cl_idProducto;
        private DataGridViewTextBoxColumn cl_sku;
        private DataGridViewTextBoxColumn cl_codigo_barras;
        private DataGridViewTextBoxColumn cl_nombre;
        private DataGridViewTextBoxColumn cl_precio;
        private DataGridViewTextBoxColumn cl_cantidad;
        private DataGridViewTextBoxColumn cl_descuento;
        private DataGridViewTextBoxColumn cl_iva;
        private DataGridViewTextBoxColumn cl_total_linea;
        private DataGridViewTextBoxColumn cl_usuario;
    }
}