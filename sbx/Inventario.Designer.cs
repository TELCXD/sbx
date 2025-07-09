namespace sbx
{
    partial class Inventario
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Inventario));
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            panel1 = new Panel();
            btn_eliminar = new Button();
            btn_agrupar_productos = new Button();
            cbx_tipo_filtro = new ComboBox();
            cbx_campo_filtro = new ComboBox();
            btn_buscar = new Button();
            txt_buscar = new TextBox();
            btn_salida = new Button();
            btn_entrada = new Button();
            panel2 = new Panel();
            label1 = new Label();
            txt_total_stock = new TextBox();
            dtg_inventario = new DataGridView();
            cl_id_documento = new DataGridViewTextBoxColumn();
            cl_fecha = new DataGridViewTextBoxColumn();
            cl_documento = new DataGridViewTextBoxColumn();
            cl_movimiento = new DataGridViewTextBoxColumn();
            cl_cantidad = new DataGridViewTextBoxColumn();
            cl_id_producto = new DataGridViewTextBoxColumn();
            cl_nombre = new DataGridViewTextBoxColumn();
            cl_sku = new DataGridViewTextBoxColumn();
            cl_comentario = new DataGridViewTextBoxColumn();
            cl_tipo = new DataGridViewTextBoxColumn();
            cl_codigo_barras = new DataGridViewTextBoxColumn();
            cl_codigo_lote = new DataGridViewTextBoxColumn();
            cl_fecha_vencimiento = new DataGridViewTextBoxColumn();
            cl_usuario = new DataGridViewTextBoxColumn();
            errorProvider1 = new ErrorProvider(components);
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtg_inventario).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.Window;
            panel1.Controls.Add(btn_eliminar);
            panel1.Controls.Add(btn_agrupar_productos);
            panel1.Controls.Add(cbx_tipo_filtro);
            panel1.Controls.Add(cbx_campo_filtro);
            panel1.Controls.Add(btn_buscar);
            panel1.Controls.Add(txt_buscar);
            panel1.Controls.Add(btn_salida);
            panel1.Controls.Add(btn_entrada);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(3, 4, 3, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(1288, 75);
            panel1.TabIndex = 0;
            // 
            // btn_eliminar
            // 
            btn_eliminar.Enabled = false;
            btn_eliminar.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_eliminar.FlatStyle = FlatStyle.Flat;
            btn_eliminar.Image = (Image)resources.GetObject("btn_eliminar.Image");
            btn_eliminar.Location = new Point(454, 6);
            btn_eliminar.Margin = new Padding(3, 4, 3, 4);
            btn_eliminar.Name = "btn_eliminar";
            btn_eliminar.Size = new Size(115, 60);
            btn_eliminar.TabIndex = 22;
            btn_eliminar.Text = "Eliminar";
            btn_eliminar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_eliminar.UseVisualStyleBackColor = true;
            btn_eliminar.Visible = false;
            btn_eliminar.Click += btn_eliminar_Click;
            // 
            // btn_agrupar_productos
            // 
            btn_agrupar_productos.Enabled = false;
            btn_agrupar_productos.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_agrupar_productos.FlatStyle = FlatStyle.Flat;
            btn_agrupar_productos.Image = (Image)resources.GetObject("btn_agrupar_productos.Image");
            btn_agrupar_productos.Location = new Point(257, 7);
            btn_agrupar_productos.Margin = new Padding(3, 4, 3, 4);
            btn_agrupar_productos.Name = "btn_agrupar_productos";
            btn_agrupar_productos.Size = new Size(191, 60);
            btn_agrupar_productos.TabIndex = 21;
            btn_agrupar_productos.Text = "Conversion productos";
            btn_agrupar_productos.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_agrupar_productos.UseVisualStyleBackColor = true;
            btn_agrupar_productos.Click += btn_agrupar_productos_Click;
            // 
            // cbx_tipo_filtro
            // 
            cbx_tipo_filtro.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cbx_tipo_filtro.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_tipo_filtro.FormattingEnabled = true;
            cbx_tipo_filtro.Items.AddRange(new object[] { "Inicia con", "Igual a", "Contiene" });
            cbx_tipo_filtro.Location = new Point(919, 23);
            cbx_tipo_filtro.Margin = new Padding(3, 4, 3, 4);
            cbx_tipo_filtro.Name = "cbx_tipo_filtro";
            cbx_tipo_filtro.Size = new Size(99, 28);
            cbx_tipo_filtro.TabIndex = 20;
            // 
            // cbx_campo_filtro
            // 
            cbx_campo_filtro.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cbx_campo_filtro.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_campo_filtro.FormattingEnabled = true;
            cbx_campo_filtro.Items.AddRange(new object[] { "Nombre", "Id", "Sku", "Codigo barras" });
            cbx_campo_filtro.Location = new Point(782, 23);
            cbx_campo_filtro.Margin = new Padding(3, 4, 3, 4);
            cbx_campo_filtro.Name = "cbx_campo_filtro";
            cbx_campo_filtro.Size = new Size(130, 28);
            cbx_campo_filtro.TabIndex = 19;
            // 
            // btn_buscar
            // 
            btn_buscar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btn_buscar.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_buscar.FlatStyle = FlatStyle.Flat;
            btn_buscar.Image = (Image)resources.GetObject("btn_buscar.Image");
            btn_buscar.Location = new Point(1250, 20);
            btn_buscar.Margin = new Padding(3, 4, 3, 4);
            btn_buscar.Name = "btn_buscar";
            btn_buscar.Size = new Size(30, 35);
            btn_buscar.TabIndex = 18;
            btn_buscar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_buscar.UseVisualStyleBackColor = true;
            btn_buscar.Click += btn_buscar_Click;
            // 
            // txt_buscar
            // 
            txt_buscar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txt_buscar.Location = new Point(1025, 23);
            txt_buscar.Margin = new Padding(3, 4, 3, 4);
            txt_buscar.Name = "txt_buscar";
            txt_buscar.Size = new Size(202, 27);
            txt_buscar.TabIndex = 17;
            txt_buscar.KeyPress += txt_buscar_KeyPress;
            // 
            // btn_salida
            // 
            btn_salida.Enabled = false;
            btn_salida.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_salida.FlatStyle = FlatStyle.Flat;
            btn_salida.Image = (Image)resources.GetObject("btn_salida.Image");
            btn_salida.Location = new Point(135, 7);
            btn_salida.Margin = new Padding(3, 4, 3, 4);
            btn_salida.Name = "btn_salida";
            btn_salida.Size = new Size(115, 60);
            btn_salida.TabIndex = 16;
            btn_salida.Text = "Salida";
            btn_salida.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_salida.UseVisualStyleBackColor = true;
            btn_salida.Click += btn_salida_Click;
            // 
            // btn_entrada
            // 
            btn_entrada.Enabled = false;
            btn_entrada.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_entrada.FlatStyle = FlatStyle.Flat;
            btn_entrada.Image = (Image)resources.GetObject("btn_entrada.Image");
            btn_entrada.Location = new Point(13, 7);
            btn_entrada.Margin = new Padding(3, 4, 3, 4);
            btn_entrada.Name = "btn_entrada";
            btn_entrada.Size = new Size(115, 60);
            btn_entrada.TabIndex = 15;
            btn_entrada.Text = "Entrada";
            btn_entrada.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_entrada.UseVisualStyleBackColor = true;
            btn_entrada.Click += btn_entrada_Click;
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.Window;
            panel2.Controls.Add(label1);
            panel2.Controls.Add(txt_total_stock);
            panel2.Dock = DockStyle.Bottom;
            panel2.Location = new Point(0, 632);
            panel2.Margin = new Padding(3, 4, 3, 4);
            panel2.Name = "panel2";
            panel2.Size = new Size(1288, 61);
            panel2.TabIndex = 1;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(943, 12);
            label1.Name = "label1";
            label1.Size = new Size(80, 37);
            label1.TabIndex = 28;
            label1.Text = "Stock";
            // 
            // txt_total_stock
            // 
            txt_total_stock.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txt_total_stock.Enabled = false;
            txt_total_stock.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txt_total_stock.Location = new Point(1021, 8);
            txt_total_stock.Margin = new Padding(3, 4, 3, 4);
            txt_total_stock.Name = "txt_total_stock";
            txt_total_stock.Size = new Size(258, 42);
            txt_total_stock.TabIndex = 27;
            // 
            // dtg_inventario
            // 
            dtg_inventario.AllowUserToAddRows = false;
            dtg_inventario.AllowUserToDeleteRows = false;
            dtg_inventario.AllowUserToOrderColumns = true;
            dtg_inventario.BackgroundColor = SystemColors.Control;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.ScrollBar;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dtg_inventario.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dtg_inventario.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dtg_inventario.Columns.AddRange(new DataGridViewColumn[] { cl_id_documento, cl_fecha, cl_documento, cl_movimiento, cl_cantidad, cl_id_producto, cl_nombre, cl_sku, cl_comentario, cl_tipo, cl_codigo_barras, cl_codigo_lote, cl_fecha_vencimiento, cl_usuario });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(172, 211, 236);
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dtg_inventario.DefaultCellStyle = dataGridViewCellStyle2;
            dtg_inventario.Dock = DockStyle.Fill;
            dtg_inventario.Location = new Point(0, 75);
            dtg_inventario.Margin = new Padding(3, 4, 3, 4);
            dtg_inventario.Name = "dtg_inventario";
            dtg_inventario.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = Color.Silver;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dtg_inventario.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dtg_inventario.RowHeadersWidth = 51;
            dtg_inventario.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtg_inventario.Size = new Size(1288, 557);
            dtg_inventario.TabIndex = 6;
            // 
            // cl_id_documento
            // 
            cl_id_documento.HeaderText = "Id Documento";
            cl_id_documento.MinimumWidth = 6;
            cl_id_documento.Name = "cl_id_documento";
            cl_id_documento.ReadOnly = true;
            cl_id_documento.Visible = false;
            cl_id_documento.Width = 125;
            // 
            // cl_fecha
            // 
            cl_fecha.HeaderText = "Fecha";
            cl_fecha.MinimumWidth = 6;
            cl_fecha.Name = "cl_fecha";
            cl_fecha.ReadOnly = true;
            cl_fecha.Width = 150;
            // 
            // cl_documento
            // 
            cl_documento.HeaderText = "Doc";
            cl_documento.MinimumWidth = 6;
            cl_documento.Name = "cl_documento";
            cl_documento.ReadOnly = true;
            cl_documento.Width = 125;
            // 
            // cl_movimiento
            // 
            cl_movimiento.HeaderText = "Movimiento";
            cl_movimiento.MinimumWidth = 6;
            cl_movimiento.Name = "cl_movimiento";
            cl_movimiento.ReadOnly = true;
            cl_movimiento.Width = 150;
            // 
            // cl_cantidad
            // 
            cl_cantidad.HeaderText = "Cantidad";
            cl_cantidad.MinimumWidth = 6;
            cl_cantidad.Name = "cl_cantidad";
            cl_cantidad.ReadOnly = true;
            cl_cantidad.Width = 125;
            // 
            // cl_id_producto
            // 
            cl_id_producto.HeaderText = "Id";
            cl_id_producto.MinimumWidth = 6;
            cl_id_producto.Name = "cl_id_producto";
            cl_id_producto.ReadOnly = true;
            cl_id_producto.Width = 60;
            // 
            // cl_nombre
            // 
            cl_nombre.HeaderText = "Nombre";
            cl_nombre.MinimumWidth = 6;
            cl_nombre.Name = "cl_nombre";
            cl_nombre.ReadOnly = true;
            cl_nombre.Width = 200;
            // 
            // cl_sku
            // 
            cl_sku.HeaderText = "Sku";
            cl_sku.MinimumWidth = 6;
            cl_sku.Name = "cl_sku";
            cl_sku.ReadOnly = true;
            cl_sku.Width = 125;
            // 
            // cl_comentario
            // 
            cl_comentario.HeaderText = "Comentario";
            cl_comentario.MinimumWidth = 6;
            cl_comentario.Name = "cl_comentario";
            cl_comentario.ReadOnly = true;
            cl_comentario.Width = 125;
            // 
            // cl_tipo
            // 
            cl_tipo.HeaderText = "Tipo";
            cl_tipo.MinimumWidth = 6;
            cl_tipo.Name = "cl_tipo";
            cl_tipo.ReadOnly = true;
            cl_tipo.Width = 150;
            // 
            // cl_codigo_barras
            // 
            cl_codigo_barras.HeaderText = "Codigo Barras";
            cl_codigo_barras.MinimumWidth = 6;
            cl_codigo_barras.Name = "cl_codigo_barras";
            cl_codigo_barras.ReadOnly = true;
            cl_codigo_barras.Width = 150;
            // 
            // cl_codigo_lote
            // 
            cl_codigo_lote.HeaderText = "lote";
            cl_codigo_lote.MinimumWidth = 6;
            cl_codigo_lote.Name = "cl_codigo_lote";
            cl_codigo_lote.ReadOnly = true;
            cl_codigo_lote.Width = 125;
            // 
            // cl_fecha_vencimiento
            // 
            cl_fecha_vencimiento.HeaderText = "Fecha vencimiento";
            cl_fecha_vencimiento.MinimumWidth = 6;
            cl_fecha_vencimiento.Name = "cl_fecha_vencimiento";
            cl_fecha_vencimiento.ReadOnly = true;
            cl_fecha_vencimiento.Width = 155;
            // 
            // cl_usuario
            // 
            cl_usuario.HeaderText = "Usuario";
            cl_usuario.MinimumWidth = 6;
            cl_usuario.Name = "cl_usuario";
            cl_usuario.ReadOnly = true;
            cl_usuario.Width = 125;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // Inventario
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1288, 693);
            Controls.Add(dtg_inventario);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            Name = "Inventario";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Inventario";
            Load += Inventario_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dtg_inventario).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button btn_entrada;
        private Button btn_salida;
        private Panel panel2;
        private DataGridView dtg_cliente;
        private TextBox txt_total_stock;
        private Label label1;
        private DataGridView dtg_inventario;
        private ComboBox cbx_tipo_filtro;
        private ComboBox cbx_campo_filtro;
        private Button btn_buscar;
        private TextBox txt_buscar;
        private ErrorProvider errorProvider1;
        private Button btn_agrupar_productos;
        private DataGridViewTextBoxColumn cl_id_documento;
        private DataGridViewTextBoxColumn cl_fecha;
        private DataGridViewTextBoxColumn cl_documento;
        private DataGridViewTextBoxColumn cl_movimiento;
        private DataGridViewTextBoxColumn cl_cantidad;
        private DataGridViewTextBoxColumn cl_id_producto;
        private DataGridViewTextBoxColumn cl_nombre;
        private DataGridViewTextBoxColumn cl_sku;
        private DataGridViewTextBoxColumn cl_comentario;
        private DataGridViewTextBoxColumn cl_tipo;
        private DataGridViewTextBoxColumn cl_codigo_barras;
        private DataGridViewTextBoxColumn cl_codigo_lote;
        private DataGridViewTextBoxColumn cl_fecha_vencimiento;
        private DataGridViewTextBoxColumn cl_usuario;
        private Button btn_eliminar;
    }
}