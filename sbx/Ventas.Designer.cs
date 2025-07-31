namespace sbx
{
    partial class Ventas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ventas));
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            panel1 = new Panel();
            dtp_fecha_fin = new DateTimePicker();
            dtp_fecha_inicio = new DateTimePicker();
            label1 = new Label();
            lbl_fechaVencimiento = new Label();
            btn_imprimir = new Button();
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
            lbl_inc_bolsa = new Label();
            label11 = new Label();
            lbl_inc = new Label();
            label7 = new Label();
            lbl_iva = new Label();
            label3 = new Label();
            dtg_ventas = new DataGridView();
            cl_fecha = new DataGridViewTextBoxColumn();
            cl_id_venta = new DataGridViewTextBoxColumn();
            cl_factura = new DataGridViewTextBoxColumn();
            cl_estado = new DataGridViewTextBoxColumn();
            cl_idProducto = new DataGridViewTextBoxColumn();
            cl_sku = new DataGridViewTextBoxColumn();
            cl_codigo_barras = new DataGridViewTextBoxColumn();
            cl_nombre = new DataGridViewTextBoxColumn();
            cl_precio = new DataGridViewTextBoxColumn();
            cl_cantidad = new DataGridViewTextBoxColumn();
            cl_descuento = new DataGridViewTextBoxColumn();
            cl_tributo = new DataGridViewTextBoxColumn();
            cl_impuesto = new DataGridViewTextBoxColumn();
            cl_total_linea = new DataGridViewTextBoxColumn();
            cl_documento_cliente = new DataGridViewTextBoxColumn();
            cl_nombre_cliente = new DataGridViewTextBoxColumn();
            cl_usuario = new DataGridViewTextBoxColumn();
            contextMenuStrip1 = new ContextMenuStrip(components);
            MEmitir = new ToolStripMenuItem();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtg_ventas).BeginInit();
            contextMenuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.Window;
            panel1.BorderStyle = BorderStyle.Fixed3D;
            panel1.Controls.Add(dtp_fecha_fin);
            panel1.Controls.Add(dtp_fecha_inicio);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(lbl_fechaVencimiento);
            panel1.Controls.Add(btn_imprimir);
            panel1.Controls.Add(cbx_client_venta);
            panel1.Controls.Add(cbx_tipo_filtro);
            panel1.Controls.Add(cbx_campo_filtro);
            panel1.Controls.Add(btn_buscar);
            panel1.Controls.Add(txt_buscar);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1223, 58);
            panel1.TabIndex = 0;
            // 
            // dtp_fecha_fin
            // 
            dtp_fecha_fin.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            dtp_fecha_fin.Format = DateTimePickerFormat.Short;
            dtp_fecha_fin.Location = new Point(497, 24);
            dtp_fecha_fin.Name = "dtp_fecha_fin";
            dtp_fecha_fin.Size = new Size(187, 23);
            dtp_fecha_fin.TabIndex = 147;
            // 
            // dtp_fecha_inicio
            // 
            dtp_fecha_inicio.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            dtp_fecha_inicio.Format = DateTimePickerFormat.Short;
            dtp_fecha_inicio.Location = new Point(272, 24);
            dtp_fecha_inicio.Name = "dtp_fecha_inicio";
            dtp_fecha_inicio.Size = new Size(200, 23);
            dtp_fecha_inicio.TabIndex = 146;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Location = new Point(497, 6);
            label1.Name = "label1";
            label1.Size = new Size(55, 15);
            label1.TabIndex = 145;
            label1.Text = "Fecha fin";
            // 
            // lbl_fechaVencimiento
            // 
            lbl_fechaVencimiento.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lbl_fechaVencimiento.AutoSize = true;
            lbl_fechaVencimiento.Location = new Point(272, 6);
            lbl_fechaVencimiento.Name = "lbl_fechaVencimiento";
            lbl_fechaVencimiento.Size = new Size(70, 15);
            lbl_fechaVencimiento.TabIndex = 144;
            lbl_fechaVencimiento.Text = "Fecha inicio";
            // 
            // btn_imprimir
            // 
            btn_imprimir.Enabled = false;
            btn_imprimir.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_imprimir.FlatStyle = FlatStyle.Flat;
            btn_imprimir.Image = (Image)resources.GetObject("btn_imprimir.Image");
            btn_imprimir.Location = new Point(3, 6);
            btn_imprimir.Name = "btn_imprimir";
            btn_imprimir.Size = new Size(101, 45);
            btn_imprimir.TabIndex = 40;
            btn_imprimir.Text = "Imprimir";
            btn_imprimir.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_imprimir.UseVisualStyleBackColor = true;
            btn_imprimir.Click += btn_imprimir_Click;
            // 
            // cbx_client_venta
            // 
            cbx_client_venta.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cbx_client_venta.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_client_venta.FormattingEnabled = true;
            cbx_client_venta.Items.AddRange(new object[] { "Factura", "Cliente", "Producto", "Usuario" });
            cbx_client_venta.Location = new Point(690, 24);
            cbx_client_venta.Name = "cbx_client_venta";
            cbx_client_venta.Size = new Size(87, 23);
            cbx_client_venta.TabIndex = 39;
            cbx_client_venta.SelectedValueChanged += cbx_client_producto_SelectedValueChanged;
            // 
            // cbx_tipo_filtro
            // 
            cbx_tipo_filtro.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cbx_tipo_filtro.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_tipo_filtro.FormattingEnabled = true;
            cbx_tipo_filtro.Items.AddRange(new object[] { "Inicia con", "Igual a", "Contiene" });
            cbx_tipo_filtro.Location = new Point(903, 24);
            cbx_tipo_filtro.Name = "cbx_tipo_filtro";
            cbx_tipo_filtro.Size = new Size(87, 23);
            cbx_tipo_filtro.TabIndex = 38;
            // 
            // cbx_campo_filtro
            // 
            cbx_campo_filtro.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cbx_campo_filtro.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_campo_filtro.FormattingEnabled = true;
            cbx_campo_filtro.Location = new Point(783, 24);
            cbx_campo_filtro.Name = "cbx_campo_filtro";
            cbx_campo_filtro.Size = new Size(114, 23);
            cbx_campo_filtro.TabIndex = 37;
            // 
            // btn_buscar
            // 
            btn_buscar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btn_buscar.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_buscar.FlatStyle = FlatStyle.Flat;
            btn_buscar.Image = (Image)resources.GetObject("btn_buscar.Image");
            btn_buscar.Location = new Point(1188, 22);
            btn_buscar.Name = "btn_buscar";
            btn_buscar.Size = new Size(26, 26);
            btn_buscar.TabIndex = 36;
            btn_buscar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_buscar.UseVisualStyleBackColor = true;
            btn_buscar.Click += btn_buscar_Click;
            // 
            // txt_buscar
            // 
            txt_buscar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txt_buscar.Location = new Point(996, 24);
            txt_buscar.Name = "txt_buscar";
            txt_buscar.Size = new Size(177, 23);
            txt_buscar.TabIndex = 35;
            txt_buscar.KeyPress += txt_buscar_KeyPress;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.Window;
            panel2.BorderStyle = BorderStyle.Fixed3D;
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
            panel2.Location = new Point(0, 402);
            panel2.Name = "panel2";
            panel2.Size = new Size(1223, 170);
            panel2.TabIndex = 4;
            // 
            // lbl_total
            // 
            lbl_total.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lbl_total.AutoSize = true;
            lbl_total.Font = new Font("Segoe UI", 12F);
            lbl_total.ForeColor = SystemColors.ControlDarkDark;
            lbl_total.Location = new Point(828, 136);
            lbl_total.Name = "lbl_total";
            lbl_total.RightToLeft = RightToLeft.No;
            lbl_total.Size = new Size(17, 21);
            lbl_total.TabIndex = 173;
            lbl_total.Text = "_";
            // 
            // label12
            // 
            label12.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label12.AutoSize = true;
            label12.Font = new Font("Segoe UI", 12F);
            label12.ForeColor = SystemColors.ControlDarkDark;
            label12.Location = new Point(743, 136);
            label12.Name = "label12";
            label12.Size = new Size(49, 21);
            label12.TabIndex = 172;
            label12.Text = "Total: ";
            // 
            // lbl_impuesto
            // 
            lbl_impuesto.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lbl_impuesto.AutoSize = true;
            lbl_impuesto.Font = new Font("Segoe UI", 12F);
            lbl_impuesto.ForeColor = SystemColors.ControlDarkDark;
            lbl_impuesto.Location = new Point(828, 104);
            lbl_impuesto.Name = "lbl_impuesto";
            lbl_impuesto.RightToLeft = RightToLeft.No;
            lbl_impuesto.Size = new Size(17, 21);
            lbl_impuesto.TabIndex = 171;
            lbl_impuesto.Text = "_";
            // 
            // label10
            // 
            label10.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 12F);
            label10.ForeColor = SystemColors.ControlDarkDark;
            label10.Location = new Point(743, 104);
            label10.Name = "label10";
            label10.Size = new Size(89, 21);
            label10.TabIndex = 170;
            label10.Text = "Impuestos: ";
            // 
            // lbl_descuento
            // 
            lbl_descuento.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lbl_descuento.AutoSize = true;
            lbl_descuento.Font = new Font("Segoe UI", 12F);
            lbl_descuento.ForeColor = SystemColors.ControlDarkDark;
            lbl_descuento.Location = new Point(828, 71);
            lbl_descuento.Name = "lbl_descuento";
            lbl_descuento.RightToLeft = RightToLeft.No;
            lbl_descuento.Size = new Size(17, 21);
            lbl_descuento.TabIndex = 169;
            lbl_descuento.Text = "_";
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 12F);
            label8.ForeColor = SystemColors.ControlDarkDark;
            label8.Location = new Point(743, 71);
            label8.Name = "label8";
            label8.Size = new Size(90, 21);
            label8.TabIndex = 168;
            label8.Text = "Descuento: ";
            // 
            // lbl_subtotal
            // 
            lbl_subtotal.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lbl_subtotal.AutoSize = true;
            lbl_subtotal.Font = new Font("Segoe UI", 12F);
            lbl_subtotal.ForeColor = SystemColors.ControlDarkDark;
            lbl_subtotal.Location = new Point(828, 40);
            lbl_subtotal.Name = "lbl_subtotal";
            lbl_subtotal.RightToLeft = RightToLeft.No;
            lbl_subtotal.Size = new Size(17, 21);
            lbl_subtotal.TabIndex = 167;
            lbl_subtotal.Text = "_";
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 12F);
            label6.ForeColor = SystemColors.ControlDarkDark;
            label6.Location = new Point(743, 40);
            label6.Name = "label6";
            label6.Size = new Size(75, 21);
            label6.TabIndex = 166;
            label6.Text = "Subtotal: ";
            // 
            // lbl_cantidadProductos
            // 
            lbl_cantidadProductos.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lbl_cantidadProductos.AutoSize = true;
            lbl_cantidadProductos.Font = new Font("Segoe UI", 12F);
            lbl_cantidadProductos.ForeColor = SystemColors.ControlDarkDark;
            lbl_cantidadProductos.Location = new Point(828, 9);
            lbl_cantidadProductos.Name = "lbl_cantidadProductos";
            lbl_cantidadProductos.RightToLeft = RightToLeft.No;
            lbl_cantidadProductos.Size = new Size(17, 21);
            lbl_cantidadProductos.TabIndex = 165;
            lbl_cantidadProductos.Text = "_";
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F);
            label4.ForeColor = SystemColors.ControlDarkDark;
            label4.Location = new Point(743, 9);
            label4.Name = "label4";
            label4.Size = new Size(79, 21);
            label4.TabIndex = 164;
            label4.Text = "Cantidad: ";
            // 
            // panel3
            // 
            panel3.BorderStyle = BorderStyle.FixedSingle;
            panel3.Controls.Add(lbl_inc_bolsa);
            panel3.Controls.Add(label11);
            panel3.Controls.Add(lbl_inc);
            panel3.Controls.Add(label7);
            panel3.Controls.Add(lbl_iva);
            panel3.Controls.Add(label3);
            panel3.Location = new Point(735, 3);
            panel3.Name = "panel3";
            panel3.Size = new Size(481, 160);
            panel3.TabIndex = 174;
            // 
            // lbl_inc_bolsa
            // 
            lbl_inc_bolsa.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lbl_inc_bolsa.AutoSize = true;
            lbl_inc_bolsa.Font = new Font("Segoe UI", 12F);
            lbl_inc_bolsa.ForeColor = SystemColors.ControlDarkDark;
            lbl_inc_bolsa.Location = new Point(351, 67);
            lbl_inc_bolsa.Name = "lbl_inc_bolsa";
            lbl_inc_bolsa.RightToLeft = RightToLeft.No;
            lbl_inc_bolsa.Size = new Size(17, 21);
            lbl_inc_bolsa.TabIndex = 171;
            lbl_inc_bolsa.Text = "_";
            // 
            // label11
            // 
            label11.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 12F);
            label11.ForeColor = SystemColors.ControlDarkDark;
            label11.Location = new Point(279, 67);
            label11.Name = "label11";
            label11.Size = new Size(78, 21);
            label11.TabIndex = 170;
            label11.Text = "Inc Bolsa: ";
            // 
            // lbl_inc
            // 
            lbl_inc.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lbl_inc.AutoSize = true;
            lbl_inc.Font = new Font("Segoe UI", 12F);
            lbl_inc.ForeColor = SystemColors.ControlDarkDark;
            lbl_inc.Location = new Point(351, 36);
            lbl_inc.Name = "lbl_inc";
            lbl_inc.RightToLeft = RightToLeft.No;
            lbl_inc.Size = new Size(17, 21);
            lbl_inc.TabIndex = 169;
            lbl_inc.Text = "_";
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 12F);
            label7.ForeColor = SystemColors.ControlDarkDark;
            label7.Location = new Point(279, 36);
            label7.Name = "label7";
            label7.Size = new Size(37, 21);
            label7.TabIndex = 168;
            label7.Text = "Inc: ";
            // 
            // lbl_iva
            // 
            lbl_iva.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lbl_iva.AutoSize = true;
            lbl_iva.Font = new Font("Segoe UI", 12F);
            lbl_iva.ForeColor = SystemColors.ControlDarkDark;
            lbl_iva.Location = new Point(351, 5);
            lbl_iva.Name = "lbl_iva";
            lbl_iva.RightToLeft = RightToLeft.No;
            lbl_iva.Size = new Size(17, 21);
            lbl_iva.TabIndex = 167;
            lbl_iva.Text = "_";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F);
            label3.ForeColor = SystemColors.ControlDarkDark;
            label3.Location = new Point(279, 5);
            label3.Name = "label3";
            label3.Size = new Size(37, 21);
            label3.TabIndex = 166;
            label3.Text = "Iva: ";
            // 
            // dtg_ventas
            // 
            dtg_ventas.AllowUserToAddRows = false;
            dtg_ventas.AllowUserToDeleteRows = false;
            dtg_ventas.AllowUserToOrderColumns = true;
            dtg_ventas.BackgroundColor = SystemColors.Control;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = SystemColors.Control;
            dataGridViewCellStyle4.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.ScrollBar;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            dtg_ventas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dtg_ventas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dtg_ventas.Columns.AddRange(new DataGridViewColumn[] { cl_fecha, cl_id_venta, cl_factura, cl_estado, cl_idProducto, cl_sku, cl_codigo_barras, cl_nombre, cl_precio, cl_cantidad, cl_descuento, cl_tributo, cl_impuesto, cl_total_linea, cl_documento_cliente, cl_nombre_cliente, cl_usuario });
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = SystemColors.Window;
            dataGridViewCellStyle5.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle5.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = Color.FromArgb(172, 211, 236);
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.ControlText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.False;
            dtg_ventas.DefaultCellStyle = dataGridViewCellStyle5;
            dtg_ventas.Dock = DockStyle.Fill;
            dtg_ventas.Location = new Point(0, 58);
            dtg_ventas.Name = "dtg_ventas";
            dtg_ventas.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = SystemColors.Control;
            dataGridViewCellStyle6.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle6.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = Color.Silver;
            dataGridViewCellStyle6.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.True;
            dtg_ventas.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            dtg_ventas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtg_ventas.Size = new Size(1223, 344);
            dtg_ventas.TabIndex = 5;
            dtg_ventas.DoubleClick += dtg_ventas_DoubleClick;
            dtg_ventas.MouseDown += dtg_ventas_MouseDown;
            // 
            // cl_fecha
            // 
            cl_fecha.HeaderText = "Fecha";
            cl_fecha.Name = "cl_fecha";
            cl_fecha.ReadOnly = true;
            // 
            // cl_id_venta
            // 
            cl_id_venta.HeaderText = "Id venta";
            cl_id_venta.Name = "cl_id_venta";
            cl_id_venta.ReadOnly = true;
            cl_id_venta.Visible = false;
            // 
            // cl_factura
            // 
            cl_factura.HeaderText = "Factura";
            cl_factura.Name = "cl_factura";
            cl_factura.ReadOnly = true;
            // 
            // cl_estado
            // 
            cl_estado.HeaderText = "Estado";
            cl_estado.Name = "cl_estado";
            cl_estado.ReadOnly = true;
            cl_estado.Width = 140;
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
            // cl_tributo
            // 
            cl_tributo.HeaderText = "Tributo";
            cl_tributo.Name = "cl_tributo";
            cl_tributo.ReadOnly = true;
            // 
            // cl_impuesto
            // 
            cl_impuesto.HeaderText = "Impuesto";
            cl_impuesto.Name = "cl_impuesto";
            cl_impuesto.ReadOnly = true;
            cl_impuesto.Width = 80;
            // 
            // cl_total_linea
            // 
            cl_total_linea.HeaderText = "Total linea";
            cl_total_linea.Name = "cl_total_linea";
            cl_total_linea.ReadOnly = true;
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
            // cl_usuario
            // 
            cl_usuario.HeaderText = "Usuario";
            cl_usuario.Name = "cl_usuario";
            cl_usuario.ReadOnly = true;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { MEmitir });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(106, 26);
            // 
            // MEmitir
            // 
            MEmitir.Name = "MEmitir";
            MEmitir.Size = new Size(105, 22);
            MEmitir.Text = "Emitir";
            MEmitir.Click += MEmitir_Click;
            // 
            // Ventas
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1223, 572);
            Controls.Add(dtg_ventas);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Ventas";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Ventas";
            Load += Ventas_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dtg_ventas).EndInit();
            contextMenuStrip1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private ComboBox cbx_client_venta;
        private ComboBox cbx_tipo_filtro;
        private ComboBox cbx_campo_filtro;
        private Button btn_buscar;
        private TextBox txt_buscar;
        private Button btn_imprimir;
        private ErrorProvider errorProvider1;
        private Panel panel2;
        private DataGridView dtg_ventas;
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
        private Label label1;
        private Label lbl_fechaVencimiento;
        private DateTimePicker dtp_fecha_inicio;
        private DateTimePicker dtp_fecha_fin;
        private Panel panel3;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem MEmitir;
        private DataGridViewTextBoxColumn cl_fecha;
        private DataGridViewTextBoxColumn cl_id_venta;
        private DataGridViewTextBoxColumn cl_factura;
        private DataGridViewTextBoxColumn cl_estado;
        private DataGridViewTextBoxColumn cl_idProducto;
        private DataGridViewTextBoxColumn cl_sku;
        private DataGridViewTextBoxColumn cl_codigo_barras;
        private DataGridViewTextBoxColumn cl_nombre;
        private DataGridViewTextBoxColumn cl_precio;
        private DataGridViewTextBoxColumn cl_cantidad;
        private DataGridViewTextBoxColumn cl_descuento;
        private DataGridViewTextBoxColumn cl_tributo;
        private DataGridViewTextBoxColumn cl_impuesto;
        private DataGridViewTextBoxColumn cl_total_linea;
        private DataGridViewTextBoxColumn cl_documento_cliente;
        private DataGridViewTextBoxColumn cl_nombre_cliente;
        private DataGridViewTextBoxColumn cl_usuario;
        private Label lbl_inc_bolsa;
        private Label label11;
        private Label lbl_inc;
        private Label label7;
        private Label lbl_iva;
        private Label label3;
    }
}