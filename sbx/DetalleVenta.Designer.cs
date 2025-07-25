namespace sbx
{
    partial class DetalleVenta
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DetalleVenta));
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            label1 = new Label();
            lbl_factura = new Label();
            label2 = new Label();
            lbl_cliente = new Label();
            label3 = new Label();
            lbl_medio_pago = new Label();
            lbl_vendedor = new Label();
            label5 = new Label();
            panel1 = new Panel();
            btn_imprimir = new Button();
            dtg_ventas = new DataGridView();
            cl_idProducto = new DataGridViewTextBoxColumn();
            cl_sku = new DataGridViewTextBoxColumn();
            cl_codigo_barras = new DataGridViewTextBoxColumn();
            cl_nombre = new DataGridViewTextBoxColumn();
            cl_precio = new DataGridViewTextBoxColumn();
            cl_cantidad = new DataGridViewTextBoxColumn();
            cl_descuento = new DataGridViewTextBoxColumn();
            cl_iva = new DataGridViewTextBoxColumn();
            cl_total = new DataGridViewTextBoxColumn();
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
            lbl_banco = new Label();
            label9 = new Label();
            lbl_referencia = new Label();
            label11 = new Label();
            lbl_usuario = new Label();
            label13 = new Label();
            panel2 = new Panel();
            btn_ver_productos = new Button();
            lbl_nota_credito = new Label();
            label15 = new Label();
            lbl_estado = new Label();
            label18 = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtg_ventas).BeginInit();
            panel3.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9.75F);
            label1.Location = new Point(12, 68);
            label1.Name = "label1";
            label1.Size = new Size(53, 17);
            label1.TabIndex = 0;
            label1.Text = "Factura:";
            // 
            // lbl_factura
            // 
            lbl_factura.AutoSize = true;
            lbl_factura.Font = new Font("Segoe UI", 9.75F);
            lbl_factura.Location = new Point(76, 68);
            lbl_factura.Name = "lbl_factura";
            lbl_factura.Size = new Size(13, 17);
            lbl_factura.TabIndex = 1;
            lbl_factura.Text = "_";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9.75F);
            label2.Location = new Point(12, 94);
            label2.Name = "label2";
            label2.Size = new Size(50, 17);
            label2.TabIndex = 2;
            label2.Text = "Cliente:";
            // 
            // lbl_cliente
            // 
            lbl_cliente.AutoSize = true;
            lbl_cliente.Font = new Font("Segoe UI", 9.75F);
            lbl_cliente.Location = new Point(76, 94);
            lbl_cliente.Name = "lbl_cliente";
            lbl_cliente.Size = new Size(13, 17);
            lbl_cliente.TabIndex = 3;
            lbl_cliente.Text = "_";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9.75F);
            label3.Location = new Point(481, 68);
            label3.Name = "label3";
            label3.Size = new Size(103, 17);
            label3.TabIndex = 4;
            label3.Text = "Medio de pago:";
            // 
            // lbl_medio_pago
            // 
            lbl_medio_pago.AutoSize = true;
            lbl_medio_pago.Font = new Font("Segoe UI", 9.75F);
            lbl_medio_pago.Location = new Point(584, 68);
            lbl_medio_pago.Name = "lbl_medio_pago";
            lbl_medio_pago.Size = new Size(13, 17);
            lbl_medio_pago.TabIndex = 5;
            lbl_medio_pago.Text = "_";
            // 
            // lbl_vendedor
            // 
            lbl_vendedor.AutoSize = true;
            lbl_vendedor.Font = new Font("Segoe UI", 9.75F);
            lbl_vendedor.Location = new Point(76, 122);
            lbl_vendedor.Name = "lbl_vendedor";
            lbl_vendedor.Size = new Size(13, 17);
            lbl_vendedor.TabIndex = 7;
            lbl_vendedor.Text = "_";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9.75F);
            label5.Location = new Point(12, 122);
            label5.Name = "label5";
            label5.Size = new Size(68, 17);
            label5.TabIndex = 6;
            label5.Text = "Vendedor:";
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.Window;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(btn_imprimir);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1111, 55);
            panel1.TabIndex = 8;
            // 
            // btn_imprimir
            // 
            btn_imprimir.Enabled = false;
            btn_imprimir.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_imprimir.FlatStyle = FlatStyle.Flat;
            btn_imprimir.Image = (Image)resources.GetObject("btn_imprimir.Image");
            btn_imprimir.Location = new Point(3, 3);
            btn_imprimir.Name = "btn_imprimir";
            btn_imprimir.Size = new Size(101, 45);
            btn_imprimir.TabIndex = 41;
            btn_imprimir.Text = "Imprimir";
            btn_imprimir.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_imprimir.UseVisualStyleBackColor = true;
            btn_imprimir.Click += btn_imprimir_Click;
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
            dtg_ventas.Columns.AddRange(new DataGridViewColumn[] { cl_idProducto, cl_sku, cl_codigo_barras, cl_nombre, cl_precio, cl_cantidad, cl_descuento, cl_iva, cl_total });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(172, 211, 236);
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dtg_ventas.DefaultCellStyle = dataGridViewCellStyle2;
            dtg_ventas.Location = new Point(4, 142);
            dtg_ventas.Name = "dtg_ventas";
            dtg_ventas.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = Color.Silver;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dtg_ventas.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dtg_ventas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtg_ventas.Size = new Size(1104, 268);
            dtg_ventas.TabIndex = 9;
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
            panel3.Location = new Point(749, 416);
            panel3.Name = "panel3";
            panel3.Size = new Size(359, 140);
            panel3.TabIndex = 142;
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
            // lbl_banco
            // 
            lbl_banco.AutoSize = true;
            lbl_banco.Font = new Font("Segoe UI", 9.75F);
            lbl_banco.Location = new Point(584, 120);
            lbl_banco.Name = "lbl_banco";
            lbl_banco.Size = new Size(13, 17);
            lbl_banco.TabIndex = 144;
            lbl_banco.Text = "_";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 9.75F);
            label9.Location = new Point(481, 120);
            label9.Name = "label9";
            label9.Size = new Size(46, 17);
            label9.TabIndex = 143;
            label9.Text = "Banco:";
            // 
            // lbl_referencia
            // 
            lbl_referencia.AutoSize = true;
            lbl_referencia.Font = new Font("Segoe UI", 9.75F);
            lbl_referencia.Location = new Point(584, 94);
            lbl_referencia.Name = "lbl_referencia";
            lbl_referencia.Size = new Size(13, 17);
            lbl_referencia.TabIndex = 146;
            lbl_referencia.Text = "_";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 9.75F);
            label11.Location = new Point(481, 94);
            label11.Name = "label11";
            label11.Size = new Size(72, 17);
            label11.TabIndex = 145;
            label11.Text = "Referencia:";
            // 
            // lbl_usuario
            // 
            lbl_usuario.AutoSize = true;
            lbl_usuario.Font = new Font("Segoe UI", 9.75F);
            lbl_usuario.Location = new Point(848, 120);
            lbl_usuario.Name = "lbl_usuario";
            lbl_usuario.Size = new Size(13, 17);
            lbl_usuario.TabIndex = 148;
            lbl_usuario.Text = "_";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Segoe UI", 9.75F);
            label13.Location = new Point(792, 120);
            label13.Name = "label13";
            label13.Size = new Size(56, 17);
            label13.TabIndex = 147;
            label13.Text = "Usuario:";
            // 
            // panel2
            // 
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(btn_ver_productos);
            panel2.Controls.Add(lbl_nota_credito);
            panel2.Controls.Add(label15);
            panel2.Location = new Point(4, 416);
            panel2.Name = "panel2";
            panel2.Size = new Size(739, 140);
            panel2.TabIndex = 149;
            // 
            // btn_ver_productos
            // 
            btn_ver_productos.Enabled = false;
            btn_ver_productos.FlatAppearance.BorderSize = 0;
            btn_ver_productos.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_ver_productos.FlatStyle = FlatStyle.Flat;
            btn_ver_productos.Image = (Image)resources.GetObject("btn_ver_productos.Image");
            btn_ver_productos.Location = new Point(92, 3);
            btn_ver_productos.Name = "btn_ver_productos";
            btn_ver_productos.Size = new Size(26, 26);
            btn_ver_productos.TabIndex = 181;
            btn_ver_productos.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_ver_productos.UseVisualStyleBackColor = true;
            btn_ver_productos.Click += btn_ver_productos_Click;
            // 
            // lbl_nota_credito
            // 
            lbl_nota_credito.AutoSize = true;
            lbl_nota_credito.Font = new Font("Segoe UI", 9.75F);
            lbl_nota_credito.ForeColor = SystemColors.InfoText;
            lbl_nota_credito.Location = new Point(124, 8);
            lbl_nota_credito.Name = "lbl_nota_credito";
            lbl_nota_credito.RightToLeft = RightToLeft.No;
            lbl_nota_credito.Size = new Size(13, 17);
            lbl_nota_credito.TabIndex = 180;
            lbl_nota_credito.Text = "_";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("Segoe UI", 9.75F);
            label15.ForeColor = SystemColors.InfoText;
            label15.Location = new Point(7, 5);
            label15.Name = "label15";
            label15.Size = new Size(89, 17);
            label15.TabIndex = 179;
            label15.Text = "Nota credito: ";
            // 
            // lbl_estado
            // 
            lbl_estado.AutoSize = true;
            lbl_estado.Font = new Font("Segoe UI", 9.75F);
            lbl_estado.Location = new Point(848, 94);
            lbl_estado.Name = "lbl_estado";
            lbl_estado.Size = new Size(13, 17);
            lbl_estado.TabIndex = 151;
            lbl_estado.Text = "_";
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Font = new Font("Segoe UI", 9.75F);
            label18.Location = new Point(792, 94);
            label18.Name = "label18";
            label18.Size = new Size(51, 17);
            label18.TabIndex = 150;
            label18.Text = "Estado:";
            // 
            // DetalleVenta
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            ClientSize = new Size(1111, 562);
            Controls.Add(lbl_estado);
            Controls.Add(label18);
            Controls.Add(panel2);
            Controls.Add(lbl_usuario);
            Controls.Add(label13);
            Controls.Add(lbl_referencia);
            Controls.Add(label11);
            Controls.Add(lbl_banco);
            Controls.Add(label9);
            Controls.Add(panel3);
            Controls.Add(dtg_ventas);
            Controls.Add(panel1);
            Controls.Add(lbl_vendedor);
            Controls.Add(label5);
            Controls.Add(lbl_medio_pago);
            Controls.Add(label3);
            Controls.Add(lbl_cliente);
            Controls.Add(label2);
            Controls.Add(lbl_factura);
            Controls.Add(label1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MaximumSize = new Size(1127, 601);
            MinimumSize = new Size(1127, 601);
            Name = "DetalleVenta";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "DetalleVenta";
            Load += DetalleVenta_Load;
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dtg_ventas).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label lbl_factura;
        private Label label2;
        private Label lbl_cliente;
        private Label label3;
        private Label lbl_medio_pago;
        private Label lbl_vendedor;
        private Label label5;
        private Panel panel1;
        private Button btn_imprimir;
        private DataGridView dtg_ventas;
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
        private DataGridViewTextBoxColumn cl_idProducto;
        private DataGridViewTextBoxColumn cl_sku;
        private DataGridViewTextBoxColumn cl_codigo_barras;
        private DataGridViewTextBoxColumn cl_nombre;
        private DataGridViewTextBoxColumn cl_precio;
        private DataGridViewTextBoxColumn cl_cantidad;
        private DataGridViewTextBoxColumn cl_descuento;
        private DataGridViewTextBoxColumn cl_iva;
        private DataGridViewTextBoxColumn cl_total;
        private Label lbl_banco;
        private Label label9;
        private Label lbl_referencia;
        private Label label11;
        private Label lbl_usuario;
        private Label label13;
        private Panel panel2;
        private Label lbl_nota_credito;
        private Label label15;
        private Button btn_ver_productos;
        private Label lbl_estado;
        private Label label18;
    }
}