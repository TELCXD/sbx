namespace sbx
{
    partial class Entradas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Entradas));
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            panel3 = new Panel();
            btn_guardar = new Button();
            panel1 = new Panel();
            label3 = new Label();
            txt_num_factura = new TextBox();
            lbl_nombre_proveedor = new Label();
            label10 = new Label();
            txt_comentario = new TextBox();
            label2 = new Label();
            txt_orden_compra = new TextBox();
            btn_busca_pv = new Button();
            label1 = new Label();
            txt_documento_proveedor = new TextBox();
            cbx_tipo_entrada = new ComboBox();
            label6 = new Label();
            panel2 = new Panel();
            btn_quitar = new Button();
            btn_add_producto = new Button();
            panel4 = new Panel();
            lbl_total = new Label();
            label4 = new Label();
            dtg_detalle_entrada = new DataGridView();
            cl_id_producto = new DataGridViewTextBoxColumn();
            cl_sku = new DataGridViewTextBoxColumn();
            cl_codigo_barras = new DataGridViewTextBoxColumn();
            cl_Nombre = new DataGridViewTextBoxColumn();
            cl_lote = new DataGridViewTextBoxColumn();
            cl_fecha_vencimiento = new DataGridViewTextBoxColumn();
            cl_cantidad = new DataGridViewTextBoxColumn();
            cl_costo_unitario = new DataGridViewTextBoxColumn();
            cl_descuento = new DataGridViewTextBoxColumn();
            cl_impuesto = new DataGridViewTextBoxColumn();
            cl_total = new DataGridViewTextBoxColumn();
            panel3.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtg_detalle_entrada).BeginInit();
            SuspendLayout();
            // 
            // panel3
            // 
            panel3.BackColor = Color.WhiteSmoke;
            panel3.BorderStyle = BorderStyle.Fixed3D;
            panel3.Controls.Add(btn_guardar);
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(0, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(1138, 56);
            panel3.TabIndex = 64;
            // 
            // btn_guardar
            // 
            btn_guardar.Enabled = false;
            btn_guardar.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_guardar.FlatStyle = FlatStyle.Flat;
            btn_guardar.Image = (Image)resources.GetObject("btn_guardar.Image");
            btn_guardar.Location = new Point(3, 3);
            btn_guardar.Name = "btn_guardar";
            btn_guardar.Size = new Size(96, 45);
            btn_guardar.TabIndex = 0;
            btn_guardar.Text = "Guardar";
            btn_guardar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_guardar.UseVisualStyleBackColor = true;
            btn_guardar.Click += btn_guardar_Click;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.Window;
            panel1.BorderStyle = BorderStyle.Fixed3D;
            panel1.Controls.Add(label3);
            panel1.Controls.Add(txt_num_factura);
            panel1.Controls.Add(lbl_nombre_proveedor);
            panel1.Controls.Add(label10);
            panel1.Controls.Add(txt_comentario);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(txt_orden_compra);
            panel1.Controls.Add(btn_busca_pv);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(txt_documento_proveedor);
            panel1.Controls.Add(cbx_tipo_entrada);
            panel1.Controls.Add(label6);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 56);
            panel1.Name = "panel1";
            panel1.Size = new Size(1138, 121);
            panel1.TabIndex = 65;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(435, 11);
            label3.Name = "label3";
            label3.Size = new Size(68, 15);
            label3.TabIndex = 110;
            label3.Text = "No. Factura";
            // 
            // txt_num_factura
            // 
            txt_num_factura.Location = new Point(435, 29);
            txt_num_factura.MaxLength = 200;
            txt_num_factura.Name = "txt_num_factura";
            txt_num_factura.Size = new Size(190, 23);
            txt_num_factura.TabIndex = 3;
            // 
            // lbl_nombre_proveedor
            // 
            lbl_nombre_proveedor.AutoSize = true;
            lbl_nombre_proveedor.Location = new Point(644, 55);
            lbl_nombre_proveedor.Name = "lbl_nombre_proveedor";
            lbl_nombre_proveedor.Size = new Size(12, 15);
            lbl_nombre_proveedor.TabIndex = 108;
            lbl_nombre_proveedor.Text = "_";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(12, 60);
            label10.Name = "label10";
            label10.Size = new Size(70, 15);
            label10.TabIndex = 107;
            label10.Text = "Comentario";
            // 
            // txt_comentario
            // 
            txt_comentario.Location = new Point(12, 80);
            txt_comentario.MaxLength = 150;
            txt_comentario.Name = "txt_comentario";
            txt_comentario.Size = new Size(825, 23);
            txt_comentario.TabIndex = 6;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(223, 11);
            label2.Name = "label2";
            label2.Size = new Size(100, 15);
            label2.TabIndex = 105;
            label2.Text = "Orden de compra";
            // 
            // txt_orden_compra
            // 
            txt_orden_compra.Location = new Point(223, 29);
            txt_orden_compra.MaxLength = 200;
            txt_orden_compra.Name = "txt_orden_compra";
            txt_orden_compra.Size = new Size(190, 23);
            txt_orden_compra.TabIndex = 2;
            // 
            // btn_busca_pv
            // 
            btn_busca_pv.Enabled = false;
            btn_busca_pv.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_busca_pv.FlatStyle = FlatStyle.Flat;
            btn_busca_pv.Image = (Image)resources.GetObject("btn_busca_pv.Image");
            btn_busca_pv.Location = new Point(843, 27);
            btn_busca_pv.Name = "btn_busca_pv";
            btn_busca_pv.Size = new Size(26, 26);
            btn_busca_pv.TabIndex = 5;
            btn_busca_pv.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_busca_pv.UseVisualStyleBackColor = true;
            btn_busca_pv.Click += btn_busca_pv_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(644, 11);
            label1.Name = "label1";
            label1.Size = new Size(61, 15);
            label1.TabIndex = 102;
            label1.Text = "Proveedor";
            // 
            // txt_documento_proveedor
            // 
            txt_documento_proveedor.Enabled = false;
            txt_documento_proveedor.Location = new Point(644, 29);
            txt_documento_proveedor.MaxLength = 200;
            txt_documento_proveedor.Name = "txt_documento_proveedor";
            txt_documento_proveedor.Size = new Size(193, 23);
            txt_documento_proveedor.TabIndex = 4;
            // 
            // cbx_tipo_entrada
            // 
            cbx_tipo_entrada.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_tipo_entrada.FormattingEnabled = true;
            cbx_tipo_entrada.Location = new Point(12, 29);
            cbx_tipo_entrada.Name = "cbx_tipo_entrada";
            cbx_tipo_entrada.Size = new Size(193, 23);
            cbx_tipo_entrada.TabIndex = 1;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 11);
            label6.Name = "label6";
            label6.Size = new Size(73, 15);
            label6.TabIndex = 100;
            label6.Text = "Tipo entrada";
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.Window;
            panel2.BorderStyle = BorderStyle.Fixed3D;
            panel2.Controls.Add(btn_quitar);
            panel2.Controls.Add(btn_add_producto);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 177);
            panel2.Name = "panel2";
            panel2.Size = new Size(1138, 56);
            panel2.TabIndex = 66;
            // 
            // btn_quitar
            // 
            btn_quitar.Enabled = false;
            btn_quitar.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_quitar.FlatStyle = FlatStyle.Flat;
            btn_quitar.Image = (Image)resources.GetObject("btn_quitar.Image");
            btn_quitar.Location = new Point(112, 5);
            btn_quitar.Name = "btn_quitar";
            btn_quitar.Size = new Size(101, 45);
            btn_quitar.TabIndex = 8;
            btn_quitar.Text = "Quitar";
            btn_quitar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_quitar.UseVisualStyleBackColor = true;
            btn_quitar.Click += btn_quitar_Click;
            // 
            // btn_add_producto
            // 
            btn_add_producto.Enabled = false;
            btn_add_producto.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_add_producto.FlatStyle = FlatStyle.Flat;
            btn_add_producto.Image = (Image)resources.GetObject("btn_add_producto.Image");
            btn_add_producto.Location = new Point(5, 5);
            btn_add_producto.Name = "btn_add_producto";
            btn_add_producto.Size = new Size(101, 45);
            btn_add_producto.TabIndex = 7;
            btn_add_producto.Text = "Agregar";
            btn_add_producto.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_add_producto.UseVisualStyleBackColor = true;
            btn_add_producto.Click += btn_add_producto_Click;
            // 
            // panel4
            // 
            panel4.BackColor = Color.WhiteSmoke;
            panel4.BorderStyle = BorderStyle.Fixed3D;
            panel4.Controls.Add(lbl_total);
            panel4.Controls.Add(label4);
            panel4.Dock = DockStyle.Bottom;
            panel4.Location = new Point(0, 608);
            panel4.Name = "panel4";
            panel4.Size = new Size(1138, 39);
            panel4.TabIndex = 68;
            // 
            // lbl_total
            // 
            lbl_total.AutoSize = true;
            lbl_total.Font = new Font("Segoe UI", 14F);
            lbl_total.Location = new Point(922, 5);
            lbl_total.Name = "lbl_total";
            lbl_total.Size = new Size(20, 25);
            lbl_total.TabIndex = 31;
            lbl_total.Text = "_";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 14F);
            label4.Location = new Point(863, 5);
            label4.Name = "label4";
            label4.Size = new Size(52, 25);
            label4.TabIndex = 30;
            label4.Text = "Total";
            // 
            // dtg_detalle_entrada
            // 
            dtg_detalle_entrada.AllowUserToAddRows = false;
            dtg_detalle_entrada.AllowUserToDeleteRows = false;
            dtg_detalle_entrada.AllowUserToOrderColumns = true;
            dtg_detalle_entrada.BackgroundColor = SystemColors.Control;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.ScrollBar;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dtg_detalle_entrada.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dtg_detalle_entrada.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dtg_detalle_entrada.Columns.AddRange(new DataGridViewColumn[] { cl_id_producto, cl_sku, cl_codigo_barras, cl_Nombre, cl_lote, cl_fecha_vencimiento, cl_cantidad, cl_costo_unitario, cl_descuento, cl_impuesto, cl_total });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(172, 211, 236);
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dtg_detalle_entrada.DefaultCellStyle = dataGridViewCellStyle2;
            dtg_detalle_entrada.Dock = DockStyle.Fill;
            dtg_detalle_entrada.Location = new Point(0, 233);
            dtg_detalle_entrada.Name = "dtg_detalle_entrada";
            dtg_detalle_entrada.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = Color.Silver;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dtg_detalle_entrada.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dtg_detalle_entrada.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtg_detalle_entrada.Size = new Size(1138, 375);
            dtg_detalle_entrada.TabIndex = 69;
            // 
            // cl_id_producto
            // 
            cl_id_producto.HeaderText = "Id";
            cl_id_producto.Name = "cl_id_producto";
            cl_id_producto.ReadOnly = true;
            cl_id_producto.Width = 50;
            // 
            // cl_sku
            // 
            cl_sku.HeaderText = "Sku";
            cl_sku.Name = "cl_sku";
            cl_sku.ReadOnly = true;
            // 
            // cl_codigo_barras
            // 
            cl_codigo_barras.HeaderText = "Codigo b.";
            cl_codigo_barras.Name = "cl_codigo_barras";
            cl_codigo_barras.ReadOnly = true;
            cl_codigo_barras.Width = 150;
            // 
            // cl_Nombre
            // 
            cl_Nombre.HeaderText = "Nombre";
            cl_Nombre.Name = "cl_Nombre";
            cl_Nombre.ReadOnly = true;
            cl_Nombre.Width = 130;
            // 
            // cl_lote
            // 
            cl_lote.HeaderText = "Lote";
            cl_lote.Name = "cl_lote";
            cl_lote.ReadOnly = true;
            // 
            // cl_fecha_vencimiento
            // 
            cl_fecha_vencimiento.HeaderText = "Fecha venc";
            cl_fecha_vencimiento.Name = "cl_fecha_vencimiento";
            cl_fecha_vencimiento.ReadOnly = true;
            cl_fecha_vencimiento.Width = 110;
            // 
            // cl_cantidad
            // 
            cl_cantidad.HeaderText = "Cantidad";
            cl_cantidad.Name = "cl_cantidad";
            cl_cantidad.ReadOnly = true;
            // 
            // cl_costo_unitario
            // 
            cl_costo_unitario.HeaderText = "Costo und";
            cl_costo_unitario.Name = "cl_costo_unitario";
            cl_costo_unitario.ReadOnly = true;
            // 
            // cl_descuento
            // 
            cl_descuento.HeaderText = "%Desc";
            cl_descuento.Name = "cl_descuento";
            cl_descuento.ReadOnly = true;
            cl_descuento.Width = 60;
            // 
            // cl_impuesto
            // 
            cl_impuesto.HeaderText = "Impuesto";
            cl_impuesto.Name = "cl_impuesto";
            cl_impuesto.ReadOnly = true;
            cl_impuesto.Width = 65;
            // 
            // cl_total
            // 
            cl_total.HeaderText = "Total";
            cl_total.Name = "cl_total";
            cl_total.ReadOnly = true;
            cl_total.Width = 130;
            // 
            // Entradas
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1138, 647);
            Controls.Add(dtg_detalle_entrada);
            Controls.Add(panel4);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(panel3);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MaximumSize = new Size(1154, 686);
            MinimumSize = new Size(1154, 686);
            Name = "Entradas";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Entradas";
            Load += Entradas_Load;
            panel3.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dtg_detalle_entrada).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel3;
        private Button btn_guardar;
        private Panel panel1;
        private Panel panel2;
        private ComboBox cbx_tipo_entrada;
        private Label label6;
        private Label label1;
        private TextBox txt_documento_proveedor;
        private Button btn_busca_pv;
        private Label label2;
        private TextBox txt_orden_compra;
        private Label label10;
        private TextBox txt_comentario;
        private Button btn_add_producto;
        private Label lbl_nombre_proveedor;
        private Label label3;
        private TextBox txt_num_factura;
        private Panel panel4;
        private DataGridView dtg_detalle_entrada;
        private Label label4;
        private Button btn_quitar;
        private Label lbl_total;
        private DataGridViewTextBoxColumn cl_id_producto;
        private DataGridViewTextBoxColumn cl_sku;
        private DataGridViewTextBoxColumn cl_codigo_barras;
        private DataGridViewTextBoxColumn cl_Nombre;
        private DataGridViewTextBoxColumn cl_lote;
        private DataGridViewTextBoxColumn cl_fecha_vencimiento;
        private DataGridViewTextBoxColumn cl_cantidad;
        private DataGridViewTextBoxColumn cl_costo_unitario;
        private DataGridViewTextBoxColumn cl_descuento;
        private DataGridViewTextBoxColumn cl_impuesto;
        private DataGridViewTextBoxColumn cl_total;
    }
}