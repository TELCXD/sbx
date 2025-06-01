namespace sbx
{
    partial class Productos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Productos));
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            panel1 = new Panel();
            btn_promociones = new Button();
            btn_lista_precios = new Button();
            cbx_tipo_filtro = new ComboBox();
            cbx_campo_filtro = new ComboBox();
            btn_buscar = new Button();
            txt_buscar = new TextBox();
            btn_buscar_ra = new Button();
            btn_eliminar = new Button();
            textBox1 = new TextBox();
            btn_editar = new Button();
            btn_agregar = new Button();
            dtg_producto = new DataGridView();
            errorProvider1 = new ErrorProvider(components);
            cl_idProducto = new DataGridViewTextBoxColumn();
            cl_sku = new DataGridViewTextBoxColumn();
            cl_codigo_barras = new DataGridViewTextBoxColumn();
            cl_nombre = new DataGridViewTextBoxColumn();
            cl_stock = new DataGridViewTextBoxColumn();
            cl_costo = new DataGridViewTextBoxColumn();
            cl_precio = new DataGridViewTextBoxColumn();
            cl_iva = new DataGridViewTextBoxColumn();
            cl_esInventariable = new DataGridViewTextBoxColumn();
            cl_unidadMedida = new DataGridViewTextBoxColumn();
            cl_marca = new DataGridViewTextBoxColumn();
            cl_categoria = new DataGridViewTextBoxColumn();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtg_producto).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.Window;
            panel1.BorderStyle = BorderStyle.Fixed3D;
            panel1.Controls.Add(btn_promociones);
            panel1.Controls.Add(btn_lista_precios);
            panel1.Controls.Add(cbx_tipo_filtro);
            panel1.Controls.Add(cbx_campo_filtro);
            panel1.Controls.Add(btn_buscar);
            panel1.Controls.Add(txt_buscar);
            panel1.Controls.Add(btn_buscar_ra);
            panel1.Controls.Add(btn_eliminar);
            panel1.Controls.Add(textBox1);
            panel1.Controls.Add(btn_editar);
            panel1.Controls.Add(btn_agregar);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1148, 56);
            panel1.TabIndex = 1;
            // 
            // btn_promociones
            // 
            btn_promociones.Enabled = false;
            btn_promociones.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_promociones.FlatStyle = FlatStyle.Flat;
            btn_promociones.Image = (Image)resources.GetObject("btn_promociones.Image");
            btn_promociones.Location = new Point(436, 3);
            btn_promociones.Name = "btn_promociones";
            btn_promociones.Size = new Size(111, 45);
            btn_promociones.TabIndex = 12;
            btn_promociones.Text = "Promociones";
            btn_promociones.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_promociones.UseVisualStyleBackColor = true;
            btn_promociones.Click += btn_promociones_Click;
            // 
            // btn_lista_precios
            // 
            btn_lista_precios.Enabled = false;
            btn_lista_precios.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_lista_precios.FlatStyle = FlatStyle.Flat;
            btn_lista_precios.Image = (Image)resources.GetObject("btn_lista_precios.Image");
            btn_lista_precios.Location = new Point(324, 3);
            btn_lista_precios.Name = "btn_lista_precios";
            btn_lista_precios.Size = new Size(106, 45);
            btn_lista_precios.TabIndex = 11;
            btn_lista_precios.Text = "Lista precios";
            btn_lista_precios.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_lista_precios.UseVisualStyleBackColor = true;
            btn_lista_precios.Click += btn_lista_precios_Click;
            // 
            // cbx_tipo_filtro
            // 
            cbx_tipo_filtro.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cbx_tipo_filtro.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_tipo_filtro.FormattingEnabled = true;
            cbx_tipo_filtro.Items.AddRange(new object[] { "Inicia con", "Igual a", "Contiene" });
            cbx_tipo_filtro.Location = new Point(807, 15);
            cbx_tipo_filtro.Name = "cbx_tipo_filtro";
            cbx_tipo_filtro.Size = new Size(87, 23);
            cbx_tipo_filtro.TabIndex = 10;
            // 
            // cbx_campo_filtro
            // 
            cbx_campo_filtro.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cbx_campo_filtro.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_campo_filtro.FormattingEnabled = true;
            cbx_campo_filtro.Items.AddRange(new object[] { "Nombre", "Id", "Sku", "Codigo barras" });
            cbx_campo_filtro.Location = new Point(687, 15);
            cbx_campo_filtro.Name = "cbx_campo_filtro";
            cbx_campo_filtro.Size = new Size(114, 23);
            cbx_campo_filtro.TabIndex = 9;
            // 
            // btn_buscar
            // 
            btn_buscar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btn_buscar.FlatAppearance.BorderSize = 0;
            btn_buscar.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_buscar.FlatStyle = FlatStyle.Flat;
            btn_buscar.Image = (Image)resources.GetObject("btn_buscar.Image");
            btn_buscar.Location = new Point(1097, 4);
            btn_buscar.Name = "btn_buscar";
            btn_buscar.Size = new Size(42, 45);
            btn_buscar.TabIndex = 7;
            btn_buscar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_buscar.UseVisualStyleBackColor = true;
            btn_buscar.Click += btn_buscar_Click;
            // 
            // txt_buscar
            // 
            txt_buscar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txt_buscar.Location = new Point(900, 15);
            txt_buscar.Name = "txt_buscar";
            txt_buscar.Size = new Size(177, 23);
            txt_buscar.TabIndex = 6;
            // 
            // btn_buscar_ra
            // 
            btn_buscar_ra.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btn_buscar_ra.FlatAppearance.BorderSize = 0;
            btn_buscar_ra.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_buscar_ra.FlatStyle = FlatStyle.Flat;
            btn_buscar_ra.Image = (Image)resources.GetObject("btn_buscar_ra.Image");
            btn_buscar_ra.Location = new Point(1738, 3);
            btn_buscar_ra.Name = "btn_buscar_ra";
            btn_buscar_ra.Size = new Size(42, 45);
            btn_buscar_ra.TabIndex = 5;
            btn_buscar_ra.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_buscar_ra.UseVisualStyleBackColor = true;
            // 
            // btn_eliminar
            // 
            btn_eliminar.Enabled = false;
            btn_eliminar.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_eliminar.FlatStyle = FlatStyle.Flat;
            btn_eliminar.Image = (Image)resources.GetObject("btn_eliminar.Image");
            btn_eliminar.Location = new Point(217, 3);
            btn_eliminar.Name = "btn_eliminar";
            btn_eliminar.Size = new Size(101, 45);
            btn_eliminar.TabIndex = 4;
            btn_eliminar.Text = "Eliminar";
            btn_eliminar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_eliminar.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            textBox1.Location = new Point(1541, 14);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(191, 23);
            textBox1.TabIndex = 2;
            // 
            // btn_editar
            // 
            btn_editar.Enabled = false;
            btn_editar.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_editar.FlatStyle = FlatStyle.Flat;
            btn_editar.Image = (Image)resources.GetObject("btn_editar.Image");
            btn_editar.Location = new Point(110, 3);
            btn_editar.Name = "btn_editar";
            btn_editar.Size = new Size(101, 45);
            btn_editar.TabIndex = 1;
            btn_editar.Text = "Editar";
            btn_editar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_editar.UseVisualStyleBackColor = true;
            btn_editar.Click += btn_editar_Click;
            // 
            // btn_agregar
            // 
            btn_agregar.Enabled = false;
            btn_agregar.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_agregar.FlatStyle = FlatStyle.Flat;
            btn_agregar.Image = (Image)resources.GetObject("btn_agregar.Image");
            btn_agregar.Location = new Point(3, 3);
            btn_agregar.Name = "btn_agregar";
            btn_agregar.Size = new Size(101, 45);
            btn_agregar.TabIndex = 0;
            btn_agregar.Text = "Agregar";
            btn_agregar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_agregar.UseVisualStyleBackColor = true;
            btn_agregar.Click += btn_agregar_Click;
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
            dtg_producto.Columns.AddRange(new DataGridViewColumn[] { cl_idProducto, cl_sku, cl_codigo_barras, cl_nombre, cl_stock, cl_costo, cl_precio, cl_iva, cl_esInventariable, cl_unidadMedida, cl_marca, cl_categoria });
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = SystemColors.Window;
            dataGridViewCellStyle5.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle5.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = Color.FromArgb(172, 211, 236);
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.ControlText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.False;
            dtg_producto.DefaultCellStyle = dataGridViewCellStyle5;
            dtg_producto.Dock = DockStyle.Fill;
            dtg_producto.Location = new Point(0, 56);
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
            dtg_producto.Size = new Size(1148, 496);
            dtg_producto.TabIndex = 2;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
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
            // cl_stock
            // 
            cl_stock.HeaderText = "Stock";
            cl_stock.Name = "cl_stock";
            cl_stock.ReadOnly = true;
            // 
            // cl_costo
            // 
            cl_costo.HeaderText = "Costo";
            cl_costo.Name = "cl_costo";
            cl_costo.ReadOnly = true;
            // 
            // cl_precio
            // 
            cl_precio.HeaderText = "Precio";
            cl_precio.Name = "cl_precio";
            cl_precio.ReadOnly = true;
            // 
            // cl_iva
            // 
            cl_iva.HeaderText = "Iva (%)";
            cl_iva.Name = "cl_iva";
            cl_iva.ReadOnly = true;
            // 
            // cl_esInventariable
            // 
            cl_esInventariable.HeaderText = "Inventariable";
            cl_esInventariable.Name = "cl_esInventariable";
            cl_esInventariable.ReadOnly = true;
            // 
            // cl_unidadMedida
            // 
            cl_unidadMedida.HeaderText = "Unidad m";
            cl_unidadMedida.Name = "cl_unidadMedida";
            cl_unidadMedida.ReadOnly = true;
            // 
            // cl_marca
            // 
            cl_marca.HeaderText = "Marca";
            cl_marca.Name = "cl_marca";
            cl_marca.ReadOnly = true;
            // 
            // cl_categoria
            // 
            cl_categoria.HeaderText = "Categoria";
            cl_categoria.Name = "cl_categoria";
            cl_categoria.ReadOnly = true;
            // 
            // Productos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1148, 552);
            Controls.Add(dtg_producto);
            Controls.Add(panel1);
            MaximizeBox = false;
            MaximumSize = new Size(1164, 591);
            MinimumSize = new Size(1164, 591);
            Name = "Productos";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Productos";
            Load += Productos_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dtg_producto).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button btn_buscar_ra;
        private Button btn_eliminar;
        private TextBox textBox1;
        private Button btn_editar;
        private Button btn_agregar;
        private DataGridView dtg_producto;
        private Button btn_buscar;
        private TextBox txt_buscar;
        private ComboBox cbx_campo_filtro;
        private ComboBox cbx_tipo_filtro;
        private ErrorProvider errorProvider1;
        private Button btn_lista_precios;
        private Button btn_promociones;
        private DataGridViewTextBoxColumn cl_idProducto;
        private DataGridViewTextBoxColumn cl_sku;
        private DataGridViewTextBoxColumn cl_codigo_barras;
        private DataGridViewTextBoxColumn cl_nombre;
        private DataGridViewTextBoxColumn cl_stock;
        private DataGridViewTextBoxColumn cl_costo;
        private DataGridViewTextBoxColumn cl_precio;
        private DataGridViewTextBoxColumn cl_iva;
        private DataGridViewTextBoxColumn cl_esInventariable;
        private DataGridViewTextBoxColumn cl_unidadMedida;
        private DataGridViewTextBoxColumn cl_marca;
        private DataGridViewTextBoxColumn cl_categoria;
    }
}