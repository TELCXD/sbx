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
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            panel1 = new Panel();
            label1 = new Label();
            cbx_vendedor = new ComboBox();
            label14 = new Label();
            cbx_lista_precio = new ComboBox();
            btn_ventas_suspendidas = new Button();
            txt_buscar_producto = new TextBox();
            btn_busca_producto = new Button();
            btn_nuevo_producto = new Button();
            dtg_producto = new DataGridView();
            cl_idProducto = new DataGridViewTextBoxColumn();
            cl_sku = new DataGridViewTextBoxColumn();
            cl_codigo_barras = new DataGridViewTextBoxColumn();
            cl_nombre = new DataGridViewTextBoxColumn();
            cl_precio = new DataGridViewTextBoxColumn();
            cl_cantidad = new DataGridViewTextBoxColumn();
            cl_descuento = new DataGridViewTextBoxColumn();
            cl_iva = new DataGridViewTextBoxColumn();
            cl_total = new DataGridViewTextBoxColumn();
            panel2 = new Panel();
            btn_nuevo_cliente = new Button();
            lbl_nombre_cliente = new Label();
            label2 = new Label();
            txt_busca_cliente = new TextBox();
            btn_busca_cliente = new Button();
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
            panel4 = new Panel();
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
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtg_producto).BeginInit();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.Window;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(label1);
            panel1.Controls.Add(cbx_vendedor);
            panel1.Controls.Add(label14);
            panel1.Controls.Add(cbx_lista_precio);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1106, 61);
            panel1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(226, 8);
            label1.Name = "label1";
            label1.Size = new Size(57, 15);
            label1.TabIndex = 134;
            label1.Text = "Vendedor";
            // 
            // cbx_vendedor
            // 
            cbx_vendedor.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_vendedor.FormattingEnabled = true;
            cbx_vendedor.Location = new Point(226, 26);
            cbx_vendedor.Name = "cbx_vendedor";
            cbx_vendedor.Size = new Size(211, 23);
            cbx_vendedor.TabIndex = 8;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(9, 8);
            label14.Name = "label14";
            label14.Size = new Size(72, 15);
            label14.TabIndex = 132;
            label14.Text = "Lista precios";
            // 
            // cbx_lista_precio
            // 
            cbx_lista_precio.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_lista_precio.FormattingEnabled = true;
            cbx_lista_precio.Location = new Point(9, 26);
            cbx_lista_precio.Name = "cbx_lista_precio";
            cbx_lista_precio.Size = new Size(211, 23);
            cbx_lista_precio.TabIndex = 7;
            // 
            // btn_ventas_suspendidas
            // 
            btn_ventas_suspendidas.Enabled = false;
            btn_ventas_suspendidas.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_ventas_suspendidas.FlatStyle = FlatStyle.Flat;
            btn_ventas_suspendidas.Image = (Image)resources.GetObject("btn_ventas_suspendidas.Image");
            btn_ventas_suspendidas.Location = new Point(674, 81);
            btn_ventas_suspendidas.Name = "btn_ventas_suspendidas";
            btn_ventas_suspendidas.Size = new Size(136, 26);
            btn_ventas_suspendidas.TabIndex = 3;
            btn_ventas_suspendidas.Text = "Ventas suspendidas";
            btn_ventas_suspendidas.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_ventas_suspendidas.UseVisualStyleBackColor = true;
            // 
            // txt_buscar_producto
            // 
            txt_buscar_producto.Anchor = AnchorStyles.Top;
            txt_buscar_producto.Enabled = false;
            txt_buscar_producto.Location = new Point(12, 82);
            txt_buscar_producto.Name = "txt_buscar_producto";
            txt_buscar_producto.Size = new Size(312, 23);
            txt_buscar_producto.TabIndex = 0;
            txt_buscar_producto.KeyPress += txt_buscar_producto_KeyPress;
            // 
            // btn_busca_producto
            // 
            btn_busca_producto.Enabled = false;
            btn_busca_producto.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_busca_producto.FlatStyle = FlatStyle.Flat;
            btn_busca_producto.Image = (Image)resources.GetObject("btn_busca_producto.Image");
            btn_busca_producto.Location = new Point(341, 79);
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
            btn_nuevo_producto.Location = new Point(526, 81);
            btn_nuevo_producto.Name = "btn_nuevo_producto";
            btn_nuevo_producto.Size = new Size(136, 26);
            btn_nuevo_producto.TabIndex = 2;
            btn_nuevo_producto.Text = "Nuevo producto";
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
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = SystemColors.Control;
            dataGridViewCellStyle4.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.ScrollBar;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            dtg_producto.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dtg_producto.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dtg_producto.Columns.AddRange(new DataGridViewColumn[] { cl_idProducto, cl_sku, cl_codigo_barras, cl_nombre, cl_precio, cl_cantidad, cl_descuento, cl_iva, cl_total });
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = SystemColors.Window;
            dataGridViewCellStyle5.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle5.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = Color.FromArgb(172, 211, 236);
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.ControlText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.False;
            dtg_producto.DefaultCellStyle = dataGridViewCellStyle5;
            dtg_producto.Location = new Point(11, 113);
            dtg_producto.Name = "dtg_producto";
            dtg_producto.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = SystemColors.Control;
            dataGridViewCellStyle6.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle6.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = Color.Silver;
            dataGridViewCellStyle6.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.True;
            dtg_producto.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            dtg_producto.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtg_producto.Size = new Size(1083, 310);
            dtg_producto.TabIndex = 6;
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
            cl_cantidad.ReadOnly = true;
            // 
            // cl_descuento
            // 
            cl_descuento.HeaderText = "Desc %";
            cl_descuento.Name = "cl_descuento";
            cl_descuento.ReadOnly = true;
            cl_descuento.Width = 60;
            // 
            // cl_iva
            // 
            cl_iva.HeaderText = "Iva %";
            cl_iva.Name = "cl_iva";
            cl_iva.ReadOnly = true;
            cl_iva.Width = 67;
            // 
            // cl_total
            // 
            cl_total.HeaderText = "Total";
            cl_total.Name = "cl_total";
            cl_total.ReadOnly = true;
            cl_total.Width = 142;
            // 
            // panel2
            // 
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(btn_nuevo_cliente);
            panel2.Controls.Add(lbl_nombre_cliente);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(txt_busca_cliente);
            panel2.Controls.Add(btn_busca_cliente);
            panel2.Location = new Point(12, 429);
            panel2.Name = "panel2";
            panel2.Size = new Size(1082, 86);
            panel2.TabIndex = 138;
            // 
            // btn_nuevo_cliente
            // 
            btn_nuevo_cliente.Enabled = false;
            btn_nuevo_cliente.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_nuevo_cliente.FlatStyle = FlatStyle.Flat;
            btn_nuevo_cliente.Image = (Image)resources.GetObject("btn_nuevo_cliente.Image");
            btn_nuevo_cliente.Location = new Point(447, 27);
            btn_nuevo_cliente.Name = "btn_nuevo_cliente";
            btn_nuevo_cliente.Size = new Size(136, 26);
            btn_nuevo_cliente.TabIndex = 11;
            btn_nuevo_cliente.Text = "Nuevo cliente";
            btn_nuevo_cliente.TextAlign = ContentAlignment.BottomCenter;
            btn_nuevo_cliente.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_nuevo_cliente.UseVisualStyleBackColor = true;
            // 
            // lbl_nombre_cliente
            // 
            lbl_nombre_cliente.AutoSize = true;
            lbl_nombre_cliente.Location = new Point(13, 54);
            lbl_nombre_cliente.Name = "lbl_nombre_cliente";
            lbl_nombre_cliente.RightToLeft = RightToLeft.No;
            lbl_nombre_cliente.Size = new Size(12, 15);
            lbl_nombre_cliente.TabIndex = 134;
            lbl_nombre_cliente.Text = "_";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(13, 10);
            label2.Name = "label2";
            label2.Size = new Size(44, 15);
            label2.TabIndex = 133;
            label2.Text = "Cliente";
            // 
            // txt_busca_cliente
            // 
            txt_busca_cliente.Anchor = AnchorStyles.Top;
            txt_busca_cliente.Enabled = false;
            txt_busca_cliente.Location = new Point(13, 30);
            txt_busca_cliente.Name = "txt_busca_cliente";
            txt_busca_cliente.Size = new Size(396, 23);
            txt_busca_cliente.TabIndex = 9;
            // 
            // btn_busca_cliente
            // 
            btn_busca_cliente.Enabled = false;
            btn_busca_cliente.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_busca_cliente.FlatStyle = FlatStyle.Flat;
            btn_busca_cliente.Image = (Image)resources.GetObject("btn_busca_cliente.Image");
            btn_busca_cliente.Location = new Point(415, 27);
            btn_busca_cliente.Name = "btn_busca_cliente";
            btn_busca_cliente.Size = new Size(26, 26);
            btn_busca_cliente.TabIndex = 10;
            btn_busca_cliente.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_busca_cliente.UseVisualStyleBackColor = true;
            btn_busca_cliente.Click += btn_busca_cliente_Click;
            // 
            // btn_suspender
            // 
            btn_suspender.Enabled = false;
            btn_suspender.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_suspender.FlatStyle = FlatStyle.Flat;
            btn_suspender.Image = (Image)resources.GetObject("btn_suspender.Image");
            btn_suspender.Location = new Point(816, 81);
            btn_suspender.Name = "btn_suspender";
            btn_suspender.Size = new Size(136, 26);
            btn_suspender.TabIndex = 4;
            btn_suspender.Text = "Suspender";
            btn_suspender.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_suspender.UseVisualStyleBackColor = true;
            // 
            // btn_cancelar
            // 
            btn_cancelar.Enabled = false;
            btn_cancelar.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_cancelar.FlatStyle = FlatStyle.Flat;
            btn_cancelar.Image = (Image)resources.GetObject("btn_cancelar.Image");
            btn_cancelar.Location = new Point(958, 81);
            btn_cancelar.Name = "btn_cancelar";
            btn_cancelar.Size = new Size(136, 26);
            btn_cancelar.TabIndex = 5;
            btn_cancelar.Text = "Cancelar";
            btn_cancelar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_cancelar.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 64);
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
            panel3.Location = new Point(637, 521);
            panel3.Name = "panel3";
            panel3.Size = new Size(457, 217);
            panel3.TabIndex = 141;
            // 
            // lbl_total
            // 
            lbl_total.AutoSize = true;
            lbl_total.Font = new Font("Segoe UI", 14.25F);
            lbl_total.Location = new Point(224, 162);
            lbl_total.Name = "lbl_total";
            lbl_total.RightToLeft = RightToLeft.No;
            lbl_total.Size = new Size(20, 25);
            lbl_total.TabIndex = 143;
            lbl_total.Text = "_";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Segoe UI", 14.25F);
            label12.Location = new Point(12, 162);
            label12.Name = "label12";
            label12.Size = new Size(61, 25);
            label12.TabIndex = 142;
            label12.Text = "Total: ";
            // 
            // lbl_impuesto
            // 
            lbl_impuesto.AutoSize = true;
            lbl_impuesto.Font = new Font("Segoe UI", 14.25F);
            lbl_impuesto.Location = new Point(224, 128);
            lbl_impuesto.Name = "lbl_impuesto";
            lbl_impuesto.RightToLeft = RightToLeft.No;
            lbl_impuesto.Size = new Size(20, 25);
            lbl_impuesto.TabIndex = 141;
            lbl_impuesto.Text = "_";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 14.25F);
            label10.Location = new Point(12, 128);
            label10.Name = "label10";
            label10.Size = new Size(99, 25);
            label10.TabIndex = 140;
            label10.Text = "Impuesto: ";
            // 
            // lbl_descuento
            // 
            lbl_descuento.AutoSize = true;
            lbl_descuento.Font = new Font("Segoe UI", 14.25F);
            lbl_descuento.Location = new Point(224, 94);
            lbl_descuento.Name = "lbl_descuento";
            lbl_descuento.RightToLeft = RightToLeft.No;
            lbl_descuento.Size = new Size(20, 25);
            lbl_descuento.TabIndex = 139;
            lbl_descuento.Text = "_";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 14.25F);
            label8.Location = new Point(12, 94);
            label8.Name = "label8";
            label8.Size = new Size(110, 25);
            label8.TabIndex = 138;
            label8.Text = "Descuento: ";
            // 
            // lbl_subtotal
            // 
            lbl_subtotal.AutoSize = true;
            lbl_subtotal.Font = new Font("Segoe UI", 14.25F);
            lbl_subtotal.Location = new Point(224, 60);
            lbl_subtotal.Name = "lbl_subtotal";
            lbl_subtotal.RightToLeft = RightToLeft.No;
            lbl_subtotal.Size = new Size(20, 25);
            lbl_subtotal.TabIndex = 137;
            lbl_subtotal.Text = "_";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 14.25F);
            label6.Location = new Point(12, 60);
            label6.Name = "label6";
            label6.Size = new Size(91, 25);
            label6.TabIndex = 136;
            label6.Text = "Subtotal: ";
            // 
            // lbl_cantidadProductos
            // 
            lbl_cantidadProductos.AutoSize = true;
            lbl_cantidadProductos.Font = new Font("Segoe UI", 14.25F);
            lbl_cantidadProductos.Location = new Point(224, 26);
            lbl_cantidadProductos.Name = "lbl_cantidadProductos";
            lbl_cantidadProductos.RightToLeft = RightToLeft.No;
            lbl_cantidadProductos.Size = new Size(20, 25);
            lbl_cantidadProductos.TabIndex = 135;
            lbl_cantidadProductos.Text = "_";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 14.25F);
            label4.Location = new Point(12, 26);
            label4.Name = "label4";
            label4.Size = new Size(187, 25);
            label4.TabIndex = 134;
            label4.Text = "Cantidad productos: ";
            // 
            // panel4
            // 
            panel4.BorderStyle = BorderStyle.FixedSingle;
            panel4.Controls.Add(btn_completar_venta);
            panel4.Controls.Add(lbl_cambio);
            panel4.Controls.Add(label18);
            panel4.Controls.Add(lbl_metodo_pago);
            panel4.Controls.Add(txt_valor_pago);
            panel4.Controls.Add(label17);
            panel4.Controls.Add(label16);
            panel4.Controls.Add(txt_referencia_pago);
            panel4.Controls.Add(label15);
            panel4.Controls.Add(cbx_banco);
            panel4.Controls.Add(label13);
            panel4.Controls.Add(cbx_medio_pago);
            panel4.Location = new Point(12, 521);
            panel4.Name = "panel4";
            panel4.Size = new Size(619, 217);
            panel4.TabIndex = 142;
            // 
            // btn_completar_venta
            // 
            btn_completar_venta.Enabled = false;
            btn_completar_venta.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_completar_venta.FlatStyle = FlatStyle.Flat;
            btn_completar_venta.Image = (Image)resources.GetObject("btn_completar_venta.Image");
            btn_completar_venta.Location = new Point(13, 165);
            btn_completar_venta.Name = "btn_completar_venta";
            btn_completar_venta.Size = new Size(134, 45);
            btn_completar_venta.TabIndex = 16;
            btn_completar_venta.Text = "Completar venta";
            btn_completar_venta.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_completar_venta.UseVisualStyleBackColor = true;
            // 
            // lbl_cambio
            // 
            lbl_cambio.AutoSize = true;
            lbl_cambio.Font = new Font("Segoe UI", 12F);
            lbl_cambio.Location = new Point(123, 129);
            lbl_cambio.Name = "lbl_cambio";
            lbl_cambio.RightToLeft = RightToLeft.No;
            lbl_cambio.Size = new Size(17, 21);
            lbl_cambio.TabIndex = 144;
            lbl_cambio.Text = "_";
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Font = new Font("Segoe UI", 12F);
            label18.Location = new Point(13, 129);
            label18.Name = "label18";
            label18.Size = new Size(104, 21);
            label18.TabIndex = 142;
            label18.Text = "Valor cambio:";
            // 
            // lbl_metodo_pago
            // 
            lbl_metodo_pago.AutoSize = true;
            lbl_metodo_pago.Font = new Font("Segoe UI", 12F);
            lbl_metodo_pago.Location = new Point(148, 64);
            lbl_metodo_pago.Name = "lbl_metodo_pago";
            lbl_metodo_pago.RightToLeft = RightToLeft.No;
            lbl_metodo_pago.Size = new Size(17, 21);
            lbl_metodo_pago.TabIndex = 141;
            lbl_metodo_pago.Text = "_";
            // 
            // txt_valor_pago
            // 
            txt_valor_pago.Anchor = AnchorStyles.Top;
            txt_valor_pago.Enabled = false;
            txt_valor_pago.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txt_valor_pago.Location = new Point(13, 88);
            txt_valor_pago.Name = "txt_valor_pago";
            txt_valor_pago.Size = new Size(413, 29);
            txt_valor_pago.TabIndex = 15;
            txt_valor_pago.KeyPress += txt_valor_pago_KeyPress;
            txt_valor_pago.KeyUp += txt_valor_pago_KeyUp;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Font = new Font("Segoe UI", 12F);
            label17.Location = new Point(12, 64);
            label17.Name = "label17";
            label17.Size = new Size(139, 21);
            label17.TabIndex = 139;
            label17.Text = "Valor del pago con";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(230, 8);
            label16.Name = "label16";
            label16.Size = new Size(92, 15);
            label16.TabIndex = 138;
            label16.Text = "Referencia pago";
            label16.Visible = false;
            // 
            // txt_referencia_pago
            // 
            txt_referencia_pago.Anchor = AnchorStyles.Top;
            txt_referencia_pago.Enabled = false;
            txt_referencia_pago.Location = new Point(229, 26);
            txt_referencia_pago.Name = "txt_referencia_pago";
            txt_referencia_pago.Size = new Size(197, 23);
            txt_referencia_pago.TabIndex = 13;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(433, 8);
            label15.Name = "label15";
            label15.Size = new Size(40, 15);
            label15.TabIndex = 136;
            label15.Text = "Banco";
            label15.Visible = false;
            // 
            // cbx_banco
            // 
            cbx_banco.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_banco.FormattingEnabled = true;
            cbx_banco.Location = new Point(433, 26);
            cbx_banco.Name = "cbx_banco";
            cbx_banco.Size = new Size(176, 23);
            cbx_banco.TabIndex = 14;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(12, 8);
            label13.Name = "label13";
            label13.Size = new Size(87, 15);
            label13.TabIndex = 134;
            label13.Text = "Medio de pago";
            // 
            // cbx_medio_pago
            // 
            cbx_medio_pago.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_medio_pago.FormattingEnabled = true;
            cbx_medio_pago.Location = new Point(12, 26);
            cbx_medio_pago.Name = "cbx_medio_pago";
            cbx_medio_pago.Size = new Size(211, 23);
            cbx_medio_pago.TabIndex = 12;
            cbx_medio_pago.SelectedValueChanged += cbx_medio_pago_SelectedValueChanged;
            // 
            // cbx_busca_por
            // 
            cbx_busca_por.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_busca_por.FormattingEnabled = true;
            cbx_busca_por.Items.AddRange(new object[] { "Id", "Sku", "Codigo barras" });
            cbx_busca_por.Location = new Point(373, 82);
            cbx_busca_por.Name = "cbx_busca_por";
            cbx_busca_por.Size = new Size(147, 23);
            cbx_busca_por.TabIndex = 143;
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Location = new Point(373, 64);
            label20.Name = "label20";
            label20.Size = new Size(59, 15);
            label20.TabIndex = 144;
            label20.Text = "Busca por";
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // AgregarVentas
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            ClientSize = new Size(1106, 745);
            Controls.Add(label20);
            Controls.Add(cbx_busca_por);
            Controls.Add(btn_ventas_suspendidas);
            Controls.Add(panel4);
            Controls.Add(panel3);
            Controls.Add(label3);
            Controls.Add(btn_cancelar);
            Controls.Add(btn_suspender);
            Controls.Add(panel2);
            Controls.Add(dtg_producto);
            Controls.Add(btn_nuevo_producto);
            Controls.Add(panel1);
            Controls.Add(txt_buscar_producto);
            Controls.Add(btn_busca_producto);
            MaximizeBox = false;
            MaximumSize = new Size(1122, 784);
            MinimumSize = new Size(1122, 784);
            Name = "AgregarVentas";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AgregarVentas";
            Load += AgregarVentas_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dtg_producto).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
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
        private Panel panel2;
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
        private Panel panel4;
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
        private DataGridViewTextBoxColumn cl_idProducto;
        private DataGridViewTextBoxColumn cl_sku;
        private DataGridViewTextBoxColumn cl_codigo_barras;
        private DataGridViewTextBoxColumn cl_nombre;
        private DataGridViewTextBoxColumn cl_precio;
        private DataGridViewTextBoxColumn cl_cantidad;
        private DataGridViewTextBoxColumn cl_descuento;
        private DataGridViewTextBoxColumn cl_iva;
        private DataGridViewTextBoxColumn cl_total;
        private ErrorProvider errorProvider1;
    }
}