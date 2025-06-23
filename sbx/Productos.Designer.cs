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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            panel1 = new Panel();
            btn_exportar = new Button();
            btn_importar = new Button();
            btn_promociones = new Button();
            btn_lista_precios = new Button();
            cbx_tipo_filtro = new ComboBox();
            cbx_campo_filtro = new ComboBox();
            btn_buscar = new Button();
            txt_buscar = new TextBox();
            btn_eliminar = new Button();
            btn_editar = new Button();
            btn_agregar = new Button();
            dtg_producto = new DataGridView();
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
            errorProvider1 = new ErrorProvider(components);
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtg_producto).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.Window;
            panel1.BorderStyle = BorderStyle.Fixed3D;
            panel1.Controls.Add(btn_exportar);
            panel1.Controls.Add(btn_importar);
            panel1.Controls.Add(btn_promociones);
            panel1.Controls.Add(btn_lista_precios);
            panel1.Controls.Add(cbx_tipo_filtro);
            panel1.Controls.Add(cbx_campo_filtro);
            panel1.Controls.Add(btn_buscar);
            panel1.Controls.Add(txt_buscar);
            panel1.Controls.Add(btn_eliminar);
            panel1.Controls.Add(btn_editar);
            panel1.Controls.Add(btn_agregar);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1148, 56);
            panel1.TabIndex = 1;
            // 
            // btn_exportar
            // 
            btn_exportar.Enabled = false;
            btn_exportar.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_exportar.FlatStyle = FlatStyle.Flat;
            btn_exportar.Image = (Image)resources.GetObject("btn_exportar.Image");
            btn_exportar.Location = new Point(605, 3);
            btn_exportar.Name = "btn_exportar";
            btn_exportar.Size = new Size(46, 45);
            btn_exportar.TabIndex = 15;
            btn_exportar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_exportar.UseVisualStyleBackColor = true;
            btn_exportar.Click += btn_exportar_Click;
            // 
            // btn_importar
            // 
            btn_importar.Enabled = false;
            btn_importar.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_importar.FlatStyle = FlatStyle.Flat;
            btn_importar.Image = (Image)resources.GetObject("btn_importar.Image");
            btn_importar.Location = new Point(553, 3);
            btn_importar.Name = "btn_importar";
            btn_importar.Size = new Size(46, 45);
            btn_importar.TabIndex = 13;
            btn_importar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_importar.UseVisualStyleBackColor = true;
            btn_importar.Click += btn_importar_Click;
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
            cbx_tipo_filtro.Location = new Point(838, 15);
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
            cbx_campo_filtro.Location = new Point(718, 15);
            cbx_campo_filtro.Name = "cbx_campo_filtro";
            cbx_campo_filtro.Size = new Size(114, 23);
            cbx_campo_filtro.TabIndex = 9;
            // 
            // btn_buscar
            // 
            btn_buscar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btn_buscar.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_buscar.FlatStyle = FlatStyle.Flat;
            btn_buscar.Image = (Image)resources.GetObject("btn_buscar.Image");
            btn_buscar.Location = new Point(1114, 13);
            btn_buscar.Name = "btn_buscar";
            btn_buscar.Size = new Size(26, 26);
            btn_buscar.TabIndex = 7;
            btn_buscar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_buscar.UseVisualStyleBackColor = true;
            btn_buscar.Click += btn_buscar_Click;
            // 
            // txt_buscar
            // 
            txt_buscar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txt_buscar.Location = new Point(931, 15);
            txt_buscar.Name = "txt_buscar";
            txt_buscar.Size = new Size(177, 23);
            txt_buscar.TabIndex = 6;
            txt_buscar.KeyPress += txt_buscar_KeyPress;
            txt_buscar.KeyUp += txt_buscar_KeyUp;
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
            btn_eliminar.Click += btn_eliminar_Click;
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
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.ScrollBar;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dtg_producto.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dtg_producto.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dtg_producto.Columns.AddRange(new DataGridViewColumn[] { cl_idProducto, cl_sku, cl_codigo_barras, cl_nombre, cl_stock, cl_costo, cl_precio, cl_iva, cl_esInventariable, cl_unidadMedida, cl_marca, cl_categoria });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(172, 211, 236);
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dtg_producto.DefaultCellStyle = dataGridViewCellStyle2;
            dtg_producto.Dock = DockStyle.Fill;
            dtg_producto.Location = new Point(0, 56);
            dtg_producto.Name = "dtg_producto";
            dtg_producto.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = Color.Silver;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dtg_producto.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dtg_producto.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtg_producto.Size = new Size(1148, 496);
            dtg_producto.TabIndex = 2;
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
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // Productos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1148, 552);
            Controls.Add(dtg_producto);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
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
        private Button btn_eliminar;
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
        private Button btn_importar;
        private Button btn_exportar;
    }
}