namespace sbx
{
    partial class AgregarVentas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AgregarVentas));
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            panel1 = new Panel();
            btn_devolucion = new Button();
            btn_ver_ventas = new Button();
            btn_nuevo_cliente = new Button();
            label1 = new Label();
            lbl_nombre_cliente = new Label();
            cbx_vendedor = new ComboBox();
            label2 = new Label();
            label14 = new Label();
            txt_busca_cliente = new TextBox();
            btn_busca_cliente = new Button();
            cbx_lista_precio = new ComboBox();
            btn_ventas_suspendidas = new Button();
            txt_buscar_producto = new TextBox();
            btn_busca_producto = new Button();
            btn_nuevo_producto = new Button();
            dtg_producto = new DataGridView();
            btn_suspender = new Button();
            btn_cancelar = new Button();
            label3 = new Label();
            panel3 = new Panel();
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
            pnl_pagos = new Panel();
            btn_completar_venta = new Button();
            lbl_cambio = new Label();
            label18 = new Label();
            lbl_metodo_pago = new Label();
            txt_valor_pago = new TextBox();
            label17 = new Label();
            label16 = new Label();
            txt_referencia_pago = new TextBox();
            label15 = new Label();
            cbx_banco = new ComboBox();
            label13 = new Label();
            cbx_medio_pago = new ComboBox();
            cbx_busca_por = new ComboBox();
            label20 = new Label();
            errorProvider1 = new ErrorProvider(components);
            btn_pagos_en_efectivo = new Button();
            btn_cotizacion = new Button();
            btn_lista_cotizaciones = new Button();
            btn_quitar1 = new Button();
            cl_idProducto = new DataGridViewTextBoxColumn();
            cl_sku = new DataGridViewTextBoxColumn();
            cl_codigo_barras = new DataGridViewTextBoxColumn();
            cl_nombre = new DataGridViewTextBoxColumn();
            cl_precio = new DataGridViewTextBoxColumn();
            cl_cantidad = new DataGridViewTextBoxColumn();
            cl_descuento = new DataGridViewTextBoxColumn();
            cl_impuesto = new DataGridViewTextBoxColumn();
            cl_total = new DataGridViewTextBoxColumn();
            cl_unidad_medida = new DataGridViewTextBoxColumn();
            cl_costo = new DataGridViewTextBoxColumn();
            cl_tributo = new DataGridViewTextBoxColumn();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtg_producto).BeginInit();
            panel3.SuspendLayout();
            pnl_pagos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.Window;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(btn_devolucion);
            panel1.Controls.Add(btn_ver_ventas);
            panel1.Controls.Add(btn_nuevo_cliente);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(lbl_nombre_cliente);
            panel1.Controls.Add(cbx_vendedor);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label14);
            panel1.Controls.Add(txt_busca_cliente);
            panel1.Controls.Add(btn_busca_cliente);
            panel1.Controls.Add(cbx_lista_precio);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1123, 73);
            panel1.TabIndex = 0;
            // 
            // btn_devolucion
            // 
            btn_devolucion.Enabled = false;
            btn_devolucion.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_devolucion.FlatStyle = FlatStyle.Flat;
            btn_devolucion.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btn_devolucion.Image = (Image)resources.GetObject("btn_devolucion.Image");
            btn_devolucion.Location = new Point(979, 22);
            btn_devolucion.Name = "btn_devolucion";
            btn_devolucion.Size = new Size(136, 26);
            btn_devolucion.TabIndex = 136;
            btn_devolucion.Text = "Devolucion";
            btn_devolucion.TextAlign = ContentAlignment.BottomCenter;
            btn_devolucion.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_devolucion.UseVisualStyleBackColor = true;
            btn_devolucion.Click += btn_devolucion_Click;
            // 
            // btn_ver_ventas
            // 
            btn_ver_ventas.Enabled = false;
            btn_ver_ventas.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_ver_ventas.FlatStyle = FlatStyle.Flat;
            btn_ver_ventas.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btn_ver_ventas.Image = (Image)resources.GetObject("btn_ver_ventas.Image");
            btn_ver_ventas.Location = new Point(837, 22);
            btn_ver_ventas.Name = "btn_ver_ventas";
            btn_ver_ventas.Size = new Size(136, 26);
            btn_ver_ventas.TabIndex = 135;
            btn_ver_ventas.Text = "Ver ventas";
            btn_ver_ventas.TextAlign = ContentAlignment.BottomCenter;
            btn_ver_ventas.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_ver_ventas.UseVisualStyleBackColor = true;
            btn_ver_ventas.Click += btn_ver_ventas_Click;
            // 
            // btn_nuevo_cliente
            // 
            btn_nuevo_cliente.Enabled = false;
            btn_nuevo_cliente.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_nuevo_cliente.FlatStyle = FlatStyle.Flat;
            btn_nuevo_cliente.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btn_nuevo_cliente.Image = (Image)resources.GetObject("btn_nuevo_cliente.Image");
            btn_nuevo_cliente.Location = new Point(695, 22);
            btn_nuevo_cliente.Name = "btn_nuevo_cliente";
            btn_nuevo_cliente.Size = new Size(136, 26);
            btn_nuevo_cliente.TabIndex = 11;
            btn_nuevo_cliente.Text = "Nuevo cliente";
            btn_nuevo_cliente.TextAlign = ContentAlignment.BottomCenter;
            btn_nuevo_cliente.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_nuevo_cliente.UseVisualStyleBackColor = true;
            btn_nuevo_cliente.Click += btn_nuevo_cliente_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(227, 2);
            label1.Name = "label1";
            label1.Size = new Size(65, 17);
            label1.TabIndex = 134;
            label1.Text = "Vendedor";
            // 
            // lbl_nombre_cliente
            // 
            lbl_nombre_cliente.AutoSize = true;
            lbl_nombre_cliente.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbl_nombre_cliente.Location = new Point(453, 51);
            lbl_nombre_cliente.Name = "lbl_nombre_cliente";
            lbl_nombre_cliente.RightToLeft = RightToLeft.No;
            lbl_nombre_cliente.Size = new Size(13, 17);
            lbl_nombre_cliente.TabIndex = 134;
            lbl_nombre_cliente.Text = "_";
            // 
            // cbx_vendedor
            // 
            cbx_vendedor.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_vendedor.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cbx_vendedor.FormattingEnabled = true;
            cbx_vendedor.Location = new Point(227, 22);
            cbx_vendedor.Name = "cbx_vendedor";
            cbx_vendedor.Size = new Size(211, 25);
            cbx_vendedor.TabIndex = 8;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(453, 4);
            label2.Name = "label2";
            label2.Size = new Size(47, 17);
            label2.TabIndex = 133;
            label2.Text = "Cliente";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label14.Location = new Point(11, 2);
            label14.Name = "label14";
            label14.Size = new Size(81, 17);
            label14.TabIndex = 132;
            label14.Text = "Lista precios";
            // 
            // txt_busca_cliente
            // 
            txt_busca_cliente.Anchor = AnchorStyles.Top;
            txt_busca_cliente.Enabled = false;
            txt_busca_cliente.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txt_busca_cliente.Location = new Point(453, 22);
            txt_busca_cliente.Name = "txt_busca_cliente";
            txt_busca_cliente.Size = new Size(184, 25);
            txt_busca_cliente.TabIndex = 9;
            txt_busca_cliente.KeyPress += txt_busca_cliente_KeyPress;
            // 
            // btn_busca_cliente
            // 
            btn_busca_cliente.Enabled = false;
            btn_busca_cliente.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_busca_cliente.FlatStyle = FlatStyle.Flat;
            btn_busca_cliente.Image = (Image)resources.GetObject("btn_busca_cliente.Image");
            btn_busca_cliente.Location = new Point(657, 22);
            btn_busca_cliente.Name = "btn_busca_cliente";
            btn_busca_cliente.Size = new Size(26, 26);
            btn_busca_cliente.TabIndex = 10;
            btn_busca_cliente.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_busca_cliente.UseVisualStyleBackColor = true;
            btn_busca_cliente.Click += btn_busca_cliente_Click;
            // 
            // cbx_lista_precio
            // 
            cbx_lista_precio.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_lista_precio.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cbx_lista_precio.FormattingEnabled = true;
            cbx_lista_precio.Location = new Point(11, 22);
            cbx_lista_precio.Name = "cbx_lista_precio";
            cbx_lista_precio.Size = new Size(211, 25);
            cbx_lista_precio.TabIndex = 7;
            cbx_lista_precio.SelectedValueChanged += cbx_lista_precio_SelectedValueChanged;
            // 
            // btn_ventas_suspendidas
            // 
            btn_ventas_suspendidas.Enabled = false;
            btn_ventas_suspendidas.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_ventas_suspendidas.FlatStyle = FlatStyle.Flat;
            btn_ventas_suspendidas.Image = (Image)resources.GetObject("btn_ventas_suspendidas.Image");
            btn_ventas_suspendidas.Location = new Point(795, 95);
            btn_ventas_suspendidas.Name = "btn_ventas_suspendidas";
            btn_ventas_suspendidas.Size = new Size(32, 26);
            btn_ventas_suspendidas.TabIndex = 3;
            btn_ventas_suspendidas.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_ventas_suspendidas.UseVisualStyleBackColor = true;
            btn_ventas_suspendidas.Click += btn_ventas_suspendidas_Click;
            // 
            // txt_buscar_producto
            // 
            txt_buscar_producto.Anchor = AnchorStyles.Top;
            txt_buscar_producto.Enabled = false;
            txt_buscar_producto.Location = new Point(11, 96);
            txt_buscar_producto.Name = "txt_buscar_producto";
            txt_buscar_producto.Size = new Size(321, 23);
            txt_buscar_producto.TabIndex = 0;
            txt_buscar_producto.KeyPress += txt_buscar_producto_KeyPress;
            // 
            // btn_busca_producto
            // 
            btn_busca_producto.Enabled = false;
            btn_busca_producto.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_busca_producto.FlatStyle = FlatStyle.Flat;
            btn_busca_producto.Image = (Image)resources.GetObject("btn_busca_producto.Image");
            btn_busca_producto.Location = new Point(351, 93);
            btn_busca_producto.Name = "btn_busca_producto";
            btn_busca_producto.Size = new Size(26, 26);
            btn_busca_producto.TabIndex = 1;
            btn_busca_producto.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_busca_producto.UseVisualStyleBackColor = true;
            btn_busca_producto.Click += btn_busca_producto_Click;
            // 
            // btn_nuevo_producto
            // 
            btn_nuevo_producto.Enabled = false;
            btn_nuevo_producto.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_nuevo_producto.FlatStyle = FlatStyle.Flat;
            btn_nuevo_producto.Image = (Image)resources.GetObject("btn_nuevo_producto.Image");
            btn_nuevo_producto.Location = new Point(536, 95);
            btn_nuevo_producto.Name = "btn_nuevo_producto";
            btn_nuevo_producto.Size = new Size(86, 26);
            btn_nuevo_producto.TabIndex = 2;
            btn_nuevo_producto.Text = "Producto";
            btn_nuevo_producto.TextAlign = ContentAlignment.BottomCenter;
            btn_nuevo_producto.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_nuevo_producto.UseVisualStyleBackColor = true;
            btn_nuevo_producto.Click += btn_nuevo_producto_Click;
            // 
            // dtg_producto
            // 
            dtg_producto.AllowUserToAddRows = false;
            dtg_producto.AllowUserToDeleteRows = false;
            dtg_producto.AllowUserToOrderColumns = true;
            dtg_producto.BackgroundColor = SystemColors.Control;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.ScrollBar;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dtg_producto.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dtg_producto.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dtg_producto.Columns.AddRange(new DataGridViewColumn[] { cl_idProducto, cl_sku, cl_codigo_barras, cl_nombre, cl_precio, cl_cantidad, cl_descuento, cl_impuesto, cl_total, cl_unidad_medida, cl_costo, cl_tributo });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(172, 211, 236);
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dtg_producto.DefaultCellStyle = dataGridViewCellStyle2;
            dtg_producto.Location = new Point(11, 127);
            dtg_producto.Name = "dtg_producto";
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = Color.Silver;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dtg_producto.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dtg_producto.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtg_producto.Size = new Size(1107, 298);
            dtg_producto.TabIndex = 6;
            dtg_producto.CellBeginEdit += dtg_producto_CellBeginEdit;
            dtg_producto.CellEndEdit += dtg_producto_CellEndEdit;
            dtg_producto.EditingControlShowing += dtg_producto_EditingControlShowing;
            dtg_producto.KeyPress += dtg_producto_KeyPress;
            // 
            // btn_suspender
            // 
            btn_suspender.Enabled = false;
            btn_suspender.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_suspender.FlatStyle = FlatStyle.Flat;
            btn_suspender.Image = (Image)resources.GetObject("btn_suspender.Image");
            btn_suspender.Location = new Point(830, 95);
            btn_suspender.Name = "btn_suspender";
            btn_suspender.Size = new Size(35, 26);
            btn_suspender.TabIndex = 4;
            btn_suspender.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_suspender.UseVisualStyleBackColor = true;
            btn_suspender.Click += btn_suspender_Click;
            // 
            // btn_cancelar
            // 
            btn_cancelar.Enabled = false;
            btn_cancelar.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_cancelar.FlatStyle = FlatStyle.Flat;
            btn_cancelar.Image = (Image)resources.GetObject("btn_cancelar.Image");
            btn_cancelar.Location = new Point(1036, 95);
            btn_cancelar.Name = "btn_cancelar";
            btn_cancelar.Size = new Size(81, 26);
            btn_cancelar.TabIndex = 5;
            btn_cancelar.Text = "Cancelar";
            btn_cancelar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_cancelar.UseVisualStyleBackColor = true;
            btn_cancelar.Click += btn_cancelar_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(11, 78);
            label3.Name = "label3";
            label3.Size = new Size(56, 15);
            label3.TabIndex = 140;
            label3.Text = "Producto";
            // 
            // panel3
            // 
            panel3.BorderStyle = BorderStyle.FixedSingle;
            panel3.Controls.Add(lbl_total);
            panel3.Controls.Add(label12);
            panel3.Controls.Add(lbl_impuesto);
            panel3.Controls.Add(label10);
            panel3.Controls.Add(lbl_descuento);
            panel3.Controls.Add(label8);
            panel3.Controls.Add(lbl_subtotal);
            panel3.Controls.Add(label6);
            panel3.Controls.Add(lbl_cantidadProductos);
            panel3.Controls.Add(label4);
            panel3.Location = new Point(637, 429);
            panel3.Name = "panel3";
            panel3.Size = new Size(481, 244);
            panel3.TabIndex = 141;
            // 
            // lbl_total
            // 
            lbl_total.AutoSize = true;
            lbl_total.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbl_total.ForeColor = SystemColors.ControlDarkDark;
            lbl_total.Location = new Point(134, 186);
            lbl_total.Name = "lbl_total";
            lbl_total.RightToLeft = RightToLeft.No;
            lbl_total.Size = new Size(24, 32);
            lbl_total.TabIndex = 143;
            lbl_total.Text = "_";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label12.ForeColor = SystemColors.ControlDarkDark;
            label12.Location = new Point(5, 186);
            label12.Name = "label12";
            label12.Size = new Size(77, 32);
            label12.TabIndex = 142;
            label12.Text = "Total: ";
            // 
            // lbl_impuesto
            // 
            lbl_impuesto.AutoSize = true;
            lbl_impuesto.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbl_impuesto.ForeColor = SystemColors.ControlDarkDark;
            lbl_impuesto.Location = new Point(134, 144);
            lbl_impuesto.Name = "lbl_impuesto";
            lbl_impuesto.RightToLeft = RightToLeft.No;
            lbl_impuesto.Size = new Size(24, 32);
            lbl_impuesto.TabIndex = 141;
            lbl_impuesto.Text = "_";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label10.ForeColor = SystemColors.ControlDarkDark;
            label10.Location = new Point(5, 144);
            label10.Name = "label10";
            label10.Size = new Size(126, 32);
            label10.TabIndex = 140;
            label10.Text = "Impuesto: ";
            // 
            // lbl_descuento
            // 
            lbl_descuento.AutoSize = true;
            lbl_descuento.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbl_descuento.ForeColor = SystemColors.ControlDarkDark;
            lbl_descuento.Location = new Point(134, 103);
            lbl_descuento.Name = "lbl_descuento";
            lbl_descuento.RightToLeft = RightToLeft.No;
            lbl_descuento.Size = new Size(24, 32);
            lbl_descuento.TabIndex = 139;
            lbl_descuento.Text = "_";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label8.ForeColor = SystemColors.ControlDarkDark;
            label8.Location = new Point(5, 103);
            label8.Name = "label8";
            label8.Size = new Size(140, 32);
            label8.TabIndex = 138;
            label8.Text = "Descuento: ";
            // 
            // lbl_subtotal
            // 
            lbl_subtotal.AutoSize = true;
            lbl_subtotal.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbl_subtotal.ForeColor = SystemColors.ControlDarkDark;
            lbl_subtotal.Location = new Point(134, 62);
            lbl_subtotal.Name = "lbl_subtotal";
            lbl_subtotal.RightToLeft = RightToLeft.No;
            lbl_subtotal.Size = new Size(24, 32);
            lbl_subtotal.TabIndex = 137;
            lbl_subtotal.Text = "_";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.ForeColor = SystemColors.ControlDarkDark;
            label6.Location = new Point(5, 62);
            label6.Name = "label6";
            label6.Size = new Size(115, 32);
            label6.TabIndex = 136;
            label6.Text = "Subtotal: ";
            // 
            // lbl_cantidadProductos
            // 
            lbl_cantidadProductos.AutoSize = true;
            lbl_cantidadProductos.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbl_cantidadProductos.ForeColor = SystemColors.ControlDarkDark;
            lbl_cantidadProductos.Location = new Point(134, 21);
            lbl_cantidadProductos.Name = "lbl_cantidadProductos";
            lbl_cantidadProductos.RightToLeft = RightToLeft.No;
            lbl_cantidadProductos.Size = new Size(24, 32);
            lbl_cantidadProductos.TabIndex = 135;
            lbl_cantidadProductos.Text = "_";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.ForeColor = SystemColors.ControlDarkDark;
            label4.Location = new Point(5, 21);
            label4.Name = "label4";
            label4.Size = new Size(121, 32);
            label4.TabIndex = 134;
            label4.Text = "Cantidad: ";
            // 
            // pnl_pagos
            // 
            pnl_pagos.BorderStyle = BorderStyle.FixedSingle;
            pnl_pagos.Controls.Add(btn_completar_venta);
            pnl_pagos.Controls.Add(lbl_cambio);
            pnl_pagos.Controls.Add(label18);
            pnl_pagos.Controls.Add(lbl_metodo_pago);
            pnl_pagos.Controls.Add(txt_valor_pago);
            pnl_pagos.Controls.Add(label17);
            pnl_pagos.Controls.Add(label16);
            pnl_pagos.Controls.Add(txt_referencia_pago);
            pnl_pagos.Controls.Add(label15);
            pnl_pagos.Controls.Add(cbx_banco);
            pnl_pagos.Controls.Add(label13);
            pnl_pagos.Controls.Add(cbx_medio_pago);
            pnl_pagos.Location = new Point(12, 429);
            pnl_pagos.Name = "pnl_pagos";
            pnl_pagos.Size = new Size(619, 244);
            pnl_pagos.TabIndex = 142;
            // 
            // btn_completar_venta
            // 
            btn_completar_venta.Enabled = false;
            btn_completar_venta.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_completar_venta.FlatStyle = FlatStyle.Flat;
            btn_completar_venta.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btn_completar_venta.Image = (Image)resources.GetObject("btn_completar_venta.Image");
            btn_completar_venta.ImageAlign = ContentAlignment.MiddleRight;
            btn_completar_venta.Location = new Point(12, 194);
            btn_completar_venta.Name = "btn_completar_venta";
            btn_completar_venta.Size = new Size(597, 45);
            btn_completar_venta.TabIndex = 16;
            btn_completar_venta.Text = "Guardar venta";
            btn_completar_venta.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_completar_venta.UseVisualStyleBackColor = true;
            btn_completar_venta.Click += btn_completar_venta_Click;
            // 
            // lbl_cambio
            // 
            lbl_cambio.AutoSize = true;
            lbl_cambio.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbl_cambio.ForeColor = SystemColors.ControlDarkDark;
            lbl_cambio.Location = new Point(175, 153);
            lbl_cambio.Name = "lbl_cambio";
            lbl_cambio.RightToLeft = RightToLeft.No;
            lbl_cambio.Size = new Size(24, 32);
            lbl_cambio.TabIndex = 144;
            lbl_cambio.Text = "_";
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label18.ForeColor = SystemColors.ControlDarkDark;
            label18.Location = new Point(12, 153);
            label18.Name = "label18";
            label18.Size = new Size(157, 32);
            label18.TabIndex = 142;
            label18.Text = "Valor cambio:";
            // 
            // lbl_metodo_pago
            // 
            lbl_metodo_pago.AutoSize = true;
            lbl_metodo_pago.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbl_metodo_pago.ForeColor = SystemColors.ControlDarkDark;
            lbl_metodo_pago.Location = new Point(219, 64);
            lbl_metodo_pago.Name = "lbl_metodo_pago";
            lbl_metodo_pago.RightToLeft = RightToLeft.No;
            lbl_metodo_pago.Size = new Size(24, 32);
            lbl_metodo_pago.TabIndex = 141;
            lbl_metodo_pago.Text = "_";
            // 
            // txt_valor_pago
            // 
            txt_valor_pago.Anchor = AnchorStyles.Top;
            txt_valor_pago.Enabled = false;
            txt_valor_pago.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txt_valor_pago.Location = new Point(13, 103);
            txt_valor_pago.Name = "txt_valor_pago";
            txt_valor_pago.Size = new Size(596, 39);
            txt_valor_pago.TabIndex = 15;
            txt_valor_pago.KeyPress += txt_valor_pago_KeyPress;
            txt_valor_pago.KeyUp += txt_valor_pago_KeyUp;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label17.ForeColor = SystemColors.ControlDarkDark;
            label17.Location = new Point(12, 64);
            label17.Name = "label17";
            label17.Size = new Size(214, 32);
            label17.TabIndex = 139;
            label17.Text = "Valor del pago con";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new Font("Segoe UI", 9.75F);
            label16.Location = new Point(230, 8);
            label16.Name = "label16";
            label16.Size = new Size(104, 17);
            label16.TabIndex = 138;
            label16.Text = "Referencia pago";
            label16.Visible = false;
            // 
            // txt_referencia_pago
            // 
            txt_referencia_pago.Anchor = AnchorStyles.Top;
            txt_referencia_pago.Enabled = false;
            txt_referencia_pago.Font = new Font("Segoe UI", 9.75F);
            txt_referencia_pago.Location = new Point(229, 28);
            txt_referencia_pago.Name = "txt_referencia_pago";
            txt_referencia_pago.Size = new Size(197, 25);
            txt_referencia_pago.TabIndex = 13;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("Segoe UI", 9.75F);
            label15.Location = new Point(433, 8);
            label15.Name = "label15";
            label15.Size = new Size(43, 17);
            label15.TabIndex = 136;
            label15.Text = "Banco";
            label15.Visible = false;
            // 
            // cbx_banco
            // 
            cbx_banco.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_banco.Font = new Font("Segoe UI", 9.75F);
            cbx_banco.FormattingEnabled = true;
            cbx_banco.Location = new Point(433, 28);
            cbx_banco.Name = "cbx_banco";
            cbx_banco.Size = new Size(176, 25);
            cbx_banco.TabIndex = 14;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Segoe UI", 9.75F);
            label13.Location = new Point(12, 8);
            label13.Name = "label13";
            label13.Size = new Size(100, 17);
            label13.TabIndex = 134;
            label13.Text = "Medio de pago";
            // 
            // cbx_medio_pago
            // 
            cbx_medio_pago.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_medio_pago.Font = new Font("Segoe UI", 9.75F);
            cbx_medio_pago.FormattingEnabled = true;
            cbx_medio_pago.Location = new Point(12, 28);
            cbx_medio_pago.Name = "cbx_medio_pago";
            cbx_medio_pago.Size = new Size(211, 25);
            cbx_medio_pago.TabIndex = 12;
            cbx_medio_pago.SelectedValueChanged += cbx_medio_pago_SelectedValueChanged;
            // 
            // cbx_busca_por
            // 
            cbx_busca_por.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_busca_por.FormattingEnabled = true;
            cbx_busca_por.Items.AddRange(new object[] { "Id", "Sku", "Codigo barras" });
            cbx_busca_por.Location = new Point(383, 96);
            cbx_busca_por.Name = "cbx_busca_por";
            cbx_busca_por.Size = new Size(147, 23);
            cbx_busca_por.TabIndex = 143;
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Location = new Point(383, 78);
            label20.Name = "label20";
            label20.Size = new Size(59, 15);
            label20.TabIndex = 144;
            label20.Text = "Busca por";
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // btn_pagos_en_efectivo
            // 
            btn_pagos_en_efectivo.Enabled = false;
            btn_pagos_en_efectivo.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_pagos_en_efectivo.FlatStyle = FlatStyle.Flat;
            btn_pagos_en_efectivo.Image = (Image)resources.GetObject("btn_pagos_en_efectivo.Image");
            btn_pagos_en_efectivo.Location = new Point(868, 95);
            btn_pagos_en_efectivo.Name = "btn_pagos_en_efectivo";
            btn_pagos_en_efectivo.Size = new Size(130, 26);
            btn_pagos_en_efectivo.TabIndex = 145;
            btn_pagos_en_efectivo.Text = "Pagos en efectivo";
            btn_pagos_en_efectivo.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_pagos_en_efectivo.UseVisualStyleBackColor = true;
            btn_pagos_en_efectivo.Click += btn_pagos_en_efectivo_Click;
            // 
            // btn_cotizacion
            // 
            btn_cotizacion.Enabled = false;
            btn_cotizacion.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_cotizacion.FlatStyle = FlatStyle.Flat;
            btn_cotizacion.Image = (Image)resources.GetObject("btn_cotizacion.Image");
            btn_cotizacion.Location = new Point(674, 95);
            btn_cotizacion.Name = "btn_cotizacion";
            btn_cotizacion.Size = new Size(118, 26);
            btn_cotizacion.TabIndex = 147;
            btn_cotizacion.Text = "Crear cotizacion";
            btn_cotizacion.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_cotizacion.UseVisualStyleBackColor = true;
            btn_cotizacion.Click += btn_cotizacion_Click;
            // 
            // btn_lista_cotizaciones
            // 
            btn_lista_cotizaciones.Enabled = false;
            btn_lista_cotizaciones.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_lista_cotizaciones.FlatStyle = FlatStyle.Flat;
            btn_lista_cotizaciones.Image = (Image)resources.GetObject("btn_lista_cotizaciones.Image");
            btn_lista_cotizaciones.Location = new Point(639, 95);
            btn_lista_cotizaciones.Name = "btn_lista_cotizaciones";
            btn_lista_cotizaciones.Size = new Size(32, 26);
            btn_lista_cotizaciones.TabIndex = 148;
            btn_lista_cotizaciones.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_lista_cotizaciones.UseVisualStyleBackColor = true;
            btn_lista_cotizaciones.Click += btn_lista_cotizaciones_Click;
            // 
            // btn_quitar1
            // 
            btn_quitar1.Enabled = false;
            btn_quitar1.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_quitar1.FlatStyle = FlatStyle.Flat;
            btn_quitar1.Image = (Image)resources.GetObject("btn_quitar1.Image");
            btn_quitar1.Location = new Point(1001, 95);
            btn_quitar1.Name = "btn_quitar1";
            btn_quitar1.Size = new Size(32, 26);
            btn_quitar1.TabIndex = 149;
            btn_quitar1.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_quitar1.UseVisualStyleBackColor = true;
            btn_quitar1.Click += btn_quitar1_Click;
            // 
            // cl_idProducto
            // 
            cl_idProducto.HeaderText = "Id";
            cl_idProducto.Name = "cl_idProducto";
            cl_idProducto.ReadOnly = true;
            cl_idProducto.Width = 50;
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
            cl_codigo_barras.Width = 150;
            // 
            // cl_nombre
            // 
            cl_nombre.HeaderText = "Nombre";
            cl_nombre.Name = "cl_nombre";
            cl_nombre.ReadOnly = true;
            cl_nombre.Width = 220;
            // 
            // cl_precio
            // 
            cl_precio.HeaderText = "Precio";
            cl_precio.Name = "cl_precio";
            cl_precio.ReadOnly = true;
            cl_precio.Width = 150;
            // 
            // cl_cantidad
            // 
            cl_cantidad.HeaderText = "Cantidad";
            cl_cantidad.Name = "cl_cantidad";
            // 
            // cl_descuento
            // 
            cl_descuento.HeaderText = "Desc %";
            cl_descuento.Name = "cl_descuento";
            cl_descuento.Width = 76;
            // 
            // cl_impuesto
            // 
            cl_impuesto.HeaderText = "Impuesto";
            cl_impuesto.Name = "cl_impuesto";
            cl_impuesto.ReadOnly = true;
            cl_impuesto.Width = 75;
            // 
            // cl_total
            // 
            cl_total.HeaderText = "Total";
            cl_total.Name = "cl_total";
            cl_total.ReadOnly = true;
            cl_total.Width = 142;
            // 
            // cl_unidad_medida
            // 
            cl_unidad_medida.HeaderText = "UM";
            cl_unidad_medida.Name = "cl_unidad_medida";
            cl_unidad_medida.Visible = false;
            // 
            // cl_costo
            // 
            cl_costo.HeaderText = "Costo";
            cl_costo.Name = "cl_costo";
            cl_costo.Visible = false;
            // 
            // cl_tributo
            // 
            cl_tributo.HeaderText = "Tributo";
            cl_tributo.Name = "cl_tributo";
            cl_tributo.Visible = false;
            // 
            // AgregarVentas
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            ClientSize = new Size(1123, 678);
            Controls.Add(btn_quitar1);
            Controls.Add(btn_lista_cotizaciones);
            Controls.Add(btn_cotizacion);
            Controls.Add(btn_pagos_en_efectivo);
            Controls.Add(label20);
            Controls.Add(cbx_busca_por);
            Controls.Add(btn_ventas_suspendidas);
            Controls.Add(pnl_pagos);
            Controls.Add(panel3);
            Controls.Add(label3);
            Controls.Add(btn_cancelar);
            Controls.Add(btn_suspender);
            Controls.Add(dtg_producto);
            Controls.Add(btn_nuevo_producto);
            Controls.Add(panel1);
            Controls.Add(txt_buscar_producto);
            Controls.Add(btn_busca_producto);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "AgregarVentas";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AgregarVentas";
            Load += AgregarVentas_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dtg_producto).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            pnl_pagos.ResumeLayout(false);
            pnl_pagos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private TextBox txt_buscar_producto;
        private Button btn_busca_producto;
        private ComboBox cbx_lista_precio;
        private Label label14;
        private Label label1;
        private ComboBox cbx_vendedor;
        private Button btn_ventas_suspendidas;
        private Button btn_nuevo_producto;
        private DataGridView dtg_producto;
        private Button btn_suspender;
        private Button btn_cancelar;
        private TextBox txt_busca_cliente;
        private Button btn_busca_cliente;
        private Label label2;
        private Label label3;
        private Label lbl_nombre_cliente;
        private Button btn_nuevo_cliente;
        private Panel panel3;
        private Label label4;
        private Label lbl_cantidadProductos;
        private Label lbl_subtotal;
        private Label label6;
        private Label lbl_descuento;
        private Label label8;
        private Label lbl_impuesto;
        private Label label10;
        private Label lbl_total;
        private Label label12;
        private Panel pnl_pagos;
        private Label label13;
        private ComboBox cbx_medio_pago;
        private Label label15;
        private ComboBox cbx_banco;
        private Label label16;
        private TextBox txt_referencia_pago;
        private Label label17;
        private TextBox txt_valor_pago;
        private Label lbl_metodo_pago;
        private Label label18;
        private Label lbl_cambio;
        private Button btn_completar_venta;
        private ComboBox cbx_busca_por;
        private Label label20;
        private ErrorProvider errorProvider1;
        private Button btn_ver_ventas;
        private Button btn_devolucion;
        private Button btn_pagos_en_efectivo;
        private Button btn_cotizacion;
        private Button btn_lista_cotizaciones;
        private Button btn_quitar1;
        private DataGridViewTextBoxColumn cl_idProducto;
        private DataGridViewTextBoxColumn cl_sku;
        private DataGridViewTextBoxColumn cl_codigo_barras;
        private DataGridViewTextBoxColumn cl_nombre;
        private DataGridViewTextBoxColumn cl_precio;
        private DataGridViewTextBoxColumn cl_cantidad;
        private DataGridViewTextBoxColumn cl_descuento;
        private DataGridViewTextBoxColumn cl_impuesto;
        private DataGridViewTextBoxColumn cl_total;
        private DataGridViewTextBoxColumn cl_unidad_medida;
        private DataGridViewTextBoxColumn cl_costo;
        private DataGridViewTextBoxColumn cl_tributo;
    }
}