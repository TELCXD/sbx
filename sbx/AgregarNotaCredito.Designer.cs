namespace sbx
{
    partial class AgregarNotaCredito
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AgregarNotaCredito));
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            label2 = new Label();
            txt_busca_factura = new TextBox();
            btn_busca_factura = new Button();
            lbl_usuario = new Label();
            label13 = new Label();
            lbl_referencia = new Label();
            label11 = new Label();
            lbl_banco = new Label();
            label9 = new Label();
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
            dtg_ventas = new DataGridView();
            lbl_vendedor = new Label();
            label5 = new Label();
            lbl_medio_pago = new Label();
            label3 = new Label();
            lbl_cliente = new Label();
            label1 = new Label();
            lbl_factura = new Label();
            label7 = new Label();
            btn_devolucion = new Button();
            label14 = new Label();
            txt_motivo_devolucion = new TextBox();
            lbl_total_devolucion = new Label();
            label16 = new Label();
            chk_marcar_todo = new CheckBox();
            panel1 = new Panel();
            panel2 = new Panel();
            lbl_cantidad_devolucion = new Label();
            label17 = new Label();
            cl_id_detalle_venta = new DataGridViewTextBoxColumn();
            cl_seleccionado = new DataGridViewCheckBoxColumn();
            cl_idProducto = new DataGridViewTextBoxColumn();
            cl_sku = new DataGridViewTextBoxColumn();
            cl_codigo_barras = new DataGridViewTextBoxColumn();
            cl_nombre = new DataGridViewTextBoxColumn();
            cl_unidadMedida = new DataGridViewTextBoxColumn();
            cl_precio = new DataGridViewTextBoxColumn();
            cl_cantidad = new DataGridViewTextBoxColumn();
            cl_cantidad_devolver = new DataGridViewTextBoxColumn();
            cl_descuento = new DataGridViewTextBoxColumn();
            cl_iva = new DataGridViewTextBoxColumn();
            cl_total = new DataGridViewTextBoxColumn();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtg_ventas).BeginInit();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(4, 3);
            label2.Name = "label2";
            label2.Size = new Size(49, 15);
            label2.TabIndex = 136;
            label2.Text = "Factura:";
            // 
            // txt_busca_factura
            // 
            txt_busca_factura.Anchor = AnchorStyles.Top;
            txt_busca_factura.Enabled = false;
            txt_busca_factura.Location = new Point(4, 21);
            txt_busca_factura.Name = "txt_busca_factura";
            txt_busca_factura.Size = new Size(184, 23);
            txt_busca_factura.TabIndex = 0;
            // 
            // btn_busca_factura
            // 
            btn_busca_factura.Enabled = false;
            btn_busca_factura.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_busca_factura.FlatStyle = FlatStyle.Flat;
            btn_busca_factura.Image = (Image)resources.GetObject("btn_busca_factura.Image");
            btn_busca_factura.Location = new Point(194, 18);
            btn_busca_factura.Name = "btn_busca_factura";
            btn_busca_factura.Size = new Size(26, 26);
            btn_busca_factura.TabIndex = 1;
            btn_busca_factura.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_busca_factura.UseVisualStyleBackColor = true;
            btn_busca_factura.Click += btn_busca_factura_Click;
            // 
            // lbl_usuario
            // 
            lbl_usuario.AutoSize = true;
            lbl_usuario.Font = new Font("Segoe UI", 9.75F);
            lbl_usuario.Location = new Point(848, 110);
            lbl_usuario.Name = "lbl_usuario";
            lbl_usuario.Size = new Size(13, 17);
            lbl_usuario.TabIndex = 164;
            lbl_usuario.Text = "_";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Segoe UI", 9.75F);
            label13.Location = new Point(792, 110);
            label13.Name = "label13";
            label13.Size = new Size(56, 17);
            label13.TabIndex = 163;
            label13.Text = "Usuario:";
            // 
            // lbl_referencia
            // 
            lbl_referencia.AutoSize = true;
            lbl_referencia.Font = new Font("Segoe UI", 9.75F);
            lbl_referencia.Location = new Point(584, 84);
            lbl_referencia.Name = "lbl_referencia";
            lbl_referencia.Size = new Size(13, 17);
            lbl_referencia.TabIndex = 162;
            lbl_referencia.Text = "_";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 9.75F);
            label11.Location = new Point(481, 84);
            label11.Name = "label11";
            label11.Size = new Size(72, 17);
            label11.TabIndex = 161;
            label11.Text = "Referencia:";
            // 
            // lbl_banco
            // 
            lbl_banco.AutoSize = true;
            lbl_banco.Font = new Font("Segoe UI", 9.75F);
            lbl_banco.Location = new Point(584, 110);
            lbl_banco.Name = "lbl_banco";
            lbl_banco.Size = new Size(13, 17);
            lbl_banco.TabIndex = 160;
            lbl_banco.Text = "_";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 9.75F);
            label9.Location = new Point(481, 110);
            label9.Name = "label9";
            label9.Size = new Size(46, 17);
            label9.TabIndex = 159;
            label9.Text = "Banco:";
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
            panel3.Location = new Point(800, 387);
            panel3.Name = "panel3";
            panel3.Size = new Size(359, 159);
            panel3.TabIndex = 158;
            // 
            // lbl_total
            // 
            lbl_total.AutoSize = true;
            lbl_total.Font = new Font("Segoe UI", 9.75F);
            lbl_total.ForeColor = SystemColors.ControlDarkDark;
            lbl_total.Location = new Point(76, 110);
            lbl_total.Name = "lbl_total";
            lbl_total.RightToLeft = RightToLeft.No;
            lbl_total.Size = new Size(13, 17);
            lbl_total.TabIndex = 143;
            lbl_total.Text = "_";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Segoe UI", 9.75F);
            label12.ForeColor = SystemColors.ControlDarkDark;
            label12.Location = new Point(5, 110);
            label12.Name = "label12";
            label12.Size = new Size(43, 17);
            label12.TabIndex = 142;
            label12.Text = "Total: ";
            // 
            // lbl_impuesto
            // 
            lbl_impuesto.AutoSize = true;
            lbl_impuesto.Font = new Font("Segoe UI", 9.75F);
            lbl_impuesto.ForeColor = SystemColors.ControlDarkDark;
            lbl_impuesto.Location = new Point(76, 83);
            lbl_impuesto.Name = "lbl_impuesto";
            lbl_impuesto.RightToLeft = RightToLeft.No;
            lbl_impuesto.Size = new Size(13, 17);
            lbl_impuesto.TabIndex = 141;
            lbl_impuesto.Text = "_";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 9.75F);
            label10.ForeColor = SystemColors.ControlDarkDark;
            label10.Location = new Point(5, 83);
            label10.Name = "label10";
            label10.Size = new Size(69, 17);
            label10.TabIndex = 140;
            label10.Text = "Impuesto: ";
            // 
            // lbl_descuento
            // 
            lbl_descuento.AutoSize = true;
            lbl_descuento.Font = new Font("Segoe UI", 9.75F);
            lbl_descuento.ForeColor = SystemColors.ControlDarkDark;
            lbl_descuento.Location = new Point(76, 57);
            lbl_descuento.Name = "lbl_descuento";
            lbl_descuento.RightToLeft = RightToLeft.No;
            lbl_descuento.Size = new Size(13, 17);
            lbl_descuento.TabIndex = 139;
            lbl_descuento.Text = "_";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 9.75F);
            label8.ForeColor = SystemColors.ControlDarkDark;
            label8.Location = new Point(5, 57);
            label8.Name = "label8";
            label8.Size = new Size(76, 17);
            label8.TabIndex = 138;
            label8.Text = "Descuento: ";
            // 
            // lbl_subtotal
            // 
            lbl_subtotal.AutoSize = true;
            lbl_subtotal.Font = new Font("Segoe UI", 9.75F);
            lbl_subtotal.ForeColor = SystemColors.ControlDarkDark;
            lbl_subtotal.Location = new Point(76, 31);
            lbl_subtotal.Name = "lbl_subtotal";
            lbl_subtotal.RightToLeft = RightToLeft.No;
            lbl_subtotal.Size = new Size(13, 17);
            lbl_subtotal.TabIndex = 137;
            lbl_subtotal.Text = "_";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 9.75F);
            label6.ForeColor = SystemColors.ControlDarkDark;
            label6.Location = new Point(7, 31);
            label6.Name = "label6";
            label6.Size = new Size(63, 17);
            label6.TabIndex = 136;
            label6.Text = "Subtotal: ";
            // 
            // lbl_cantidadProductos
            // 
            lbl_cantidadProductos.AutoSize = true;
            lbl_cantidadProductos.Font = new Font("Segoe UI", 9.75F);
            lbl_cantidadProductos.ForeColor = SystemColors.ControlDarkDark;
            lbl_cantidadProductos.Location = new Point(76, 5);
            lbl_cantidadProductos.Name = "lbl_cantidadProductos";
            lbl_cantidadProductos.RightToLeft = RightToLeft.No;
            lbl_cantidadProductos.Size = new Size(13, 17);
            lbl_cantidadProductos.TabIndex = 135;
            lbl_cantidadProductos.Text = "_";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9.75F);
            label4.ForeColor = SystemColors.ControlDarkDark;
            label4.Location = new Point(5, 5);
            label4.Name = "label4";
            label4.Size = new Size(67, 17);
            label4.TabIndex = 134;
            label4.Text = "Cantidad: ";
            // 
            // dtg_ventas
            // 
            dtg_ventas.AllowUserToAddRows = false;
            dtg_ventas.AllowUserToDeleteRows = false;
            dtg_ventas.AllowUserToOrderColumns = true;
            dtg_ventas.BackgroundColor = SystemColors.Control;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.ScrollBar;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dtg_ventas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dtg_ventas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dtg_ventas.Columns.AddRange(new DataGridViewColumn[] { cl_id_detalle_venta, cl_seleccionado, cl_idProducto, cl_sku, cl_codigo_barras, cl_nombre, cl_unidadMedida, cl_precio, cl_cantidad, cl_cantidad_devolver, cl_descuento, cl_iva, cl_total });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(172, 211, 236);
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dtg_ventas.DefaultCellStyle = dataGridViewCellStyle2;
            dtg_ventas.Location = new Point(4, 165);
            dtg_ventas.Name = "dtg_ventas";
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = Color.Silver;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dtg_ventas.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dtg_ventas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtg_ventas.Size = new Size(1155, 216);
            dtg_ventas.TabIndex = 157;
            dtg_ventas.CellContentClick += dtg_ventas_CellContentClick;
            dtg_ventas.CellEndEdit += dtg_ventas_CellEndEdit;
            dtg_ventas.EditingControlShowing += dtg_ventas_EditingControlShowing;
            dtg_ventas.KeyPress += dtg_ventas_KeyPress;
            // 
            // lbl_vendedor
            // 
            lbl_vendedor.AutoSize = true;
            lbl_vendedor.Font = new Font("Segoe UI", 9.75F);
            lbl_vendedor.Location = new Point(77, 61);
            lbl_vendedor.Name = "lbl_vendedor";
            lbl_vendedor.Size = new Size(13, 17);
            lbl_vendedor.TabIndex = 156;
            lbl_vendedor.Text = "_";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9.75F);
            label5.Location = new Point(10, 112);
            label5.Name = "label5";
            label5.Size = new Size(68, 17);
            label5.TabIndex = 155;
            label5.Text = "Vendedor:";
            // 
            // lbl_medio_pago
            // 
            lbl_medio_pago.AutoSize = true;
            lbl_medio_pago.Font = new Font("Segoe UI", 9.75F);
            lbl_medio_pago.Location = new Point(584, 58);
            lbl_medio_pago.Name = "lbl_medio_pago";
            lbl_medio_pago.Size = new Size(13, 17);
            lbl_medio_pago.TabIndex = 154;
            lbl_medio_pago.Text = "_";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9.75F);
            label3.Location = new Point(481, 58);
            label3.Name = "label3";
            label3.Size = new Size(103, 17);
            label3.TabIndex = 153;
            label3.Text = "Medio de pago:";
            // 
            // lbl_cliente
            // 
            lbl_cliente.AutoSize = true;
            lbl_cliente.Font = new Font("Segoe UI", 9.75F);
            lbl_cliente.Location = new Point(77, 33);
            lbl_cliente.Name = "lbl_cliente";
            lbl_cliente.Size = new Size(13, 17);
            lbl_cliente.TabIndex = 152;
            lbl_cliente.Text = "_";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9.75F);
            label1.Location = new Point(10, 84);
            label1.Name = "label1";
            label1.Size = new Size(50, 17);
            label1.TabIndex = 151;
            label1.Text = "Cliente:";
            // 
            // lbl_factura
            // 
            lbl_factura.AutoSize = true;
            lbl_factura.Font = new Font("Segoe UI", 9.75F);
            lbl_factura.Location = new Point(77, 7);
            lbl_factura.Name = "lbl_factura";
            lbl_factura.Size = new Size(13, 17);
            lbl_factura.TabIndex = 150;
            lbl_factura.Text = "_";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 9.75F);
            label7.Location = new Point(10, 58);
            label7.Name = "label7";
            label7.Size = new Size(53, 17);
            label7.TabIndex = 149;
            label7.Text = "Factura:";
            // 
            // btn_devolucion
            // 
            btn_devolucion.Enabled = false;
            btn_devolucion.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_devolucion.FlatStyle = FlatStyle.Flat;
            btn_devolucion.Image = (Image)resources.GetObject("btn_devolucion.Image");
            btn_devolucion.Location = new Point(12, 493);
            btn_devolucion.Name = "btn_devolucion";
            btn_devolucion.Size = new Size(150, 47);
            btn_devolucion.TabIndex = 5;
            btn_devolucion.Text = "Guardar Devolucion";
            btn_devolucion.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_devolucion.UseVisualStyleBackColor = true;
            btn_devolucion.Click += btn_devolucion_parcial_Click;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label14.Location = new Point(5, 55);
            label14.Name = "label14";
            label14.Size = new Size(119, 17);
            label14.TabIndex = 168;
            label14.Text = "Motivo devolucion:";
            // 
            // txt_motivo_devolucion
            // 
            txt_motivo_devolucion.Anchor = AnchorStyles.Top;
            txt_motivo_devolucion.Location = new Point(5, 75);
            txt_motivo_devolucion.Name = "txt_motivo_devolucion";
            txt_motivo_devolucion.Size = new Size(769, 23);
            txt_motivo_devolucion.TabIndex = 4;
            // 
            // lbl_total_devolucion
            // 
            lbl_total_devolucion.AutoSize = true;
            lbl_total_devolucion.Font = new Font("Segoe UI", 9.75F);
            lbl_total_devolucion.ForeColor = SystemColors.InfoText;
            lbl_total_devolucion.Location = new Point(144, 9);
            lbl_total_devolucion.Name = "lbl_total_devolucion";
            lbl_total_devolucion.RightToLeft = RightToLeft.No;
            lbl_total_devolucion.Size = new Size(13, 17);
            lbl_total_devolucion.TabIndex = 170;
            lbl_total_devolucion.Text = "_";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new Font("Segoe UI", 9.75F);
            label16.ForeColor = SystemColors.InfoText;
            label16.Location = new Point(5, 9);
            label16.Name = "label16";
            label16.Size = new Size(110, 17);
            label16.TabIndex = 169;
            label16.Text = "Total devolucion: ";
            // 
            // chk_marcar_todo
            // 
            chk_marcar_todo.AutoSize = true;
            chk_marcar_todo.Enabled = false;
            chk_marcar_todo.Location = new Point(4, 140);
            chk_marcar_todo.Name = "chk_marcar_todo";
            chk_marcar_todo.Size = new Size(91, 19);
            chk_marcar_todo.TabIndex = 3;
            chk_marcar_todo.Text = "Marcar todo";
            chk_marcar_todo.UseVisualStyleBackColor = true;
            chk_marcar_todo.CheckedChanged += chk_marcar_todo_CheckedChanged;
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(lbl_cliente);
            panel1.Controls.Add(lbl_factura);
            panel1.Controls.Add(lbl_vendedor);
            panel1.Location = new Point(4, 50);
            panel1.Name = "panel1";
            panel1.Size = new Size(1155, 84);
            panel1.TabIndex = 172;
            // 
            // panel2
            // 
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(lbl_cantidad_devolucion);
            panel2.Controls.Add(lbl_total_devolucion);
            panel2.Controls.Add(label17);
            panel2.Controls.Add(label16);
            panel2.Controls.Add(txt_motivo_devolucion);
            panel2.Controls.Add(label14);
            panel2.Location = new Point(4, 387);
            panel2.Name = "panel2";
            panel2.Size = new Size(790, 159);
            panel2.TabIndex = 173;
            // 
            // lbl_cantidad_devolucion
            // 
            lbl_cantidad_devolucion.AutoSize = true;
            lbl_cantidad_devolucion.Font = new Font("Segoe UI", 9.75F);
            lbl_cantidad_devolucion.ForeColor = SystemColors.InfoText;
            lbl_cantidad_devolucion.Location = new Point(144, 33);
            lbl_cantidad_devolucion.Name = "lbl_cantidad_devolucion";
            lbl_cantidad_devolucion.RightToLeft = RightToLeft.No;
            lbl_cantidad_devolucion.Size = new Size(13, 17);
            lbl_cantidad_devolucion.TabIndex = 172;
            lbl_cantidad_devolucion.Text = "_";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Font = new Font("Segoe UI", 9.75F);
            label17.ForeColor = SystemColors.InfoText;
            label17.Location = new Point(5, 33);
            label17.Name = "label17";
            label17.Size = new Size(134, 17);
            label17.TabIndex = 171;
            label17.Text = "Cantidad devolucion: ";
            // 
            // cl_id_detalle_venta
            // 
            cl_id_detalle_venta.HeaderText = "Id Detalle venta";
            cl_id_detalle_venta.Name = "cl_id_detalle_venta";
            cl_id_detalle_venta.ReadOnly = true;
            cl_id_detalle_venta.Visible = false;
            // 
            // cl_seleccionado
            // 
            cl_seleccionado.HeaderText = "";
            cl_seleccionado.Name = "cl_seleccionado";
            cl_seleccionado.ReadOnly = true;
            cl_seleccionado.Resizable = DataGridViewTriState.True;
            cl_seleccionado.SortMode = DataGridViewColumnSortMode.Automatic;
            cl_seleccionado.Width = 50;
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
            cl_codigo_barras.Width = 120;
            // 
            // cl_nombre
            // 
            cl_nombre.HeaderText = "Nombre";
            cl_nombre.Name = "cl_nombre";
            cl_nombre.ReadOnly = true;
            cl_nombre.Width = 150;
            // 
            // cl_unidadMedida
            // 
            cl_unidadMedida.HeaderText = "UM";
            cl_unidadMedida.Name = "cl_unidadMedida";
            cl_unidadMedida.ReadOnly = true;
            cl_unidadMedida.Visible = false;
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
            // cl_cantidad_devolver
            // 
            cl_cantidad_devolver.HeaderText = "Cant. dev";
            cl_cantidad_devolver.Name = "cl_cantidad_devolver";
            cl_cantidad_devolver.ReadOnly = true;
            // 
            // cl_descuento
            // 
            cl_descuento.HeaderText = "Desc %";
            cl_descuento.Name = "cl_descuento";
            cl_descuento.ReadOnly = true;
            cl_descuento.Width = 80;
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
            // AgregarNotaCredito
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            ClientSize = new Size(1166, 550);
            Controls.Add(chk_marcar_todo);
            Controls.Add(btn_devolucion);
            Controls.Add(lbl_usuario);
            Controls.Add(label13);
            Controls.Add(lbl_referencia);
            Controls.Add(label11);
            Controls.Add(lbl_banco);
            Controls.Add(label9);
            Controls.Add(panel3);
            Controls.Add(dtg_ventas);
            Controls.Add(label5);
            Controls.Add(lbl_medio_pago);
            Controls.Add(label3);
            Controls.Add(label1);
            Controls.Add(label7);
            Controls.Add(label2);
            Controls.Add(txt_busca_factura);
            Controls.Add(btn_busca_factura);
            Controls.Add(panel1);
            Controls.Add(panel2);
            MaximizeBox = false;
            MaximumSize = new Size(1182, 589);
            MinimumSize = new Size(1182, 589);
            Name = "AgregarNotaCredito";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AgregarNotaCredito";
            Load += AgregarNotaCredito_Load;
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dtg_ventas).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label2;
        private TextBox txt_busca_factura;
        private Button btn_busca_factura;
        private Label lbl_usuario;
        private Label label13;
        private Label lbl_referencia;
        private Label label11;
        private Label lbl_banco;
        private Label label9;
        private Panel panel3;
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
        private DataGridView dtg_ventas;
        private Label lbl_vendedor;
        private Label label5;
        private Label lbl_medio_pago;
        private Label label3;
        private Label lbl_cliente;
        private Label label1;
        private Label lbl_factura;
        private Label label7;
        private Button btn_devolucion;
        private Label label14;
        private TextBox txt_motivo_devolucion;
        private Label lbl_total_devolucion;
        private Label label16;
        private CheckBox chk_marcar_todo;
        private Panel panel1;
        private Panel panel2;
        private Label lbl_cantidad_devolucion;
        private Label label17;
        private DataGridViewTextBoxColumn cl_id_detalle_venta;
        private DataGridViewCheckBoxColumn cl_seleccionado;
        private DataGridViewTextBoxColumn cl_idProducto;
        private DataGridViewTextBoxColumn cl_sku;
        private DataGridViewTextBoxColumn cl_codigo_barras;
        private DataGridViewTextBoxColumn cl_nombre;
        private DataGridViewTextBoxColumn cl_unidadMedida;
        private DataGridViewTextBoxColumn cl_precio;
        private DataGridViewTextBoxColumn cl_cantidad;
        private DataGridViewTextBoxColumn cl_cantidad_devolver;
        private DataGridViewTextBoxColumn cl_descuento;
        private DataGridViewTextBoxColumn cl_iva;
        private DataGridViewTextBoxColumn cl_total;
    }
}