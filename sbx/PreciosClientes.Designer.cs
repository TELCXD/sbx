namespace sbx
{
    partial class PreciosClientes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PreciosClientes));
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            panel1 = new Panel();
            cbx_client_producto = new ComboBox();
            cbx_tipo_filtro = new ComboBox();
            cbx_campo_filtro = new ComboBox();
            btn_buscar = new Button();
            txt_buscar = new TextBox();
            btn_eliminar = new Button();
            btn_editar = new Button();
            btn_agregar = new Button();
            dtg_cliente_precio = new DataGridView();
            cl_id_precio_cliente = new DataGridViewTextBoxColumn();
            cl_IdCliente = new DataGridViewTextBoxColumn();
            cl_numero_documento = new DataGridViewTextBoxColumn();
            cl_nombre = new DataGridViewTextBoxColumn();
            cl_tipo_cliente = new DataGridViewTextBoxColumn();
            cl_producto = new DataGridViewTextBoxColumn();
            cl_sku = new DataGridViewTextBoxColumn();
            cl_codigoBarras = new DataGridViewTextBoxColumn();
            cl_nombre_producto = new DataGridViewTextBoxColumn();
            cl_precioEspacial = new DataGridViewTextBoxColumn();
            cl_fecha_inicial = new DataGridViewTextBoxColumn();
            cl_fecha_final = new DataGridViewTextBoxColumn();
            errorProvider1 = new ErrorProvider(components);
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtg_cliente_precio).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.Window;
            panel1.BorderStyle = BorderStyle.Fixed3D;
            panel1.Controls.Add(cbx_client_producto);
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
            panel1.Size = new Size(1134, 58);
            panel1.TabIndex = 0;
            // 
            // cbx_client_producto
            // 
            cbx_client_producto.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cbx_client_producto.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_client_producto.FormattingEnabled = true;
            cbx_client_producto.Items.AddRange(new object[] { "Cliente", "Producto" });
            cbx_client_producto.Location = new Point(570, 16);
            cbx_client_producto.Name = "cbx_client_producto";
            cbx_client_producto.Size = new Size(87, 23);
            cbx_client_producto.TabIndex = 34;
            cbx_client_producto.SelectedValueChanged += cbx_client_producto_SelectedValueChanged;
            // 
            // cbx_tipo_filtro
            // 
            cbx_tipo_filtro.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cbx_tipo_filtro.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_tipo_filtro.FormattingEnabled = true;
            cbx_tipo_filtro.Items.AddRange(new object[] { "Inicia con", "Igual a", "Contiene" });
            cbx_tipo_filtro.Location = new Point(783, 16);
            cbx_tipo_filtro.Name = "cbx_tipo_filtro";
            cbx_tipo_filtro.Size = new Size(87, 23);
            cbx_tipo_filtro.TabIndex = 33;
            // 
            // cbx_campo_filtro
            // 
            cbx_campo_filtro.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cbx_campo_filtro.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_campo_filtro.FormattingEnabled = true;
            cbx_campo_filtro.Location = new Point(663, 16);
            cbx_campo_filtro.Name = "cbx_campo_filtro";
            cbx_campo_filtro.Size = new Size(114, 23);
            cbx_campo_filtro.TabIndex = 32;
            // 
            // btn_buscar
            // 
            btn_buscar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btn_buscar.FlatAppearance.BorderSize = 0;
            btn_buscar.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_buscar.FlatStyle = FlatStyle.Flat;
            btn_buscar.Image = (Image)resources.GetObject("btn_buscar.Image");
            btn_buscar.Location = new Point(1073, 5);
            btn_buscar.Name = "btn_buscar";
            btn_buscar.Size = new Size(42, 45);
            btn_buscar.TabIndex = 31;
            btn_buscar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_buscar.UseVisualStyleBackColor = true;
            btn_buscar.Click += btn_buscar_Click;
            // 
            // txt_buscar
            // 
            txt_buscar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txt_buscar.Location = new Point(876, 16);
            txt_buscar.Name = "txt_buscar";
            txt_buscar.Size = new Size(177, 23);
            txt_buscar.TabIndex = 30;
            // 
            // btn_eliminar
            // 
            btn_eliminar.Enabled = false;
            btn_eliminar.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_eliminar.FlatStyle = FlatStyle.Flat;
            btn_eliminar.Image = (Image)resources.GetObject("btn_eliminar.Image");
            btn_eliminar.Location = new Point(219, 4);
            btn_eliminar.Name = "btn_eliminar";
            btn_eliminar.Size = new Size(101, 45);
            btn_eliminar.TabIndex = 17;
            btn_eliminar.Text = "Eliminar";
            btn_eliminar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_eliminar.UseVisualStyleBackColor = true;
            // 
            // btn_editar
            // 
            btn_editar.Enabled = false;
            btn_editar.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_editar.FlatStyle = FlatStyle.Flat;
            btn_editar.Image = (Image)resources.GetObject("btn_editar.Image");
            btn_editar.Location = new Point(112, 4);
            btn_editar.Name = "btn_editar";
            btn_editar.Size = new Size(101, 45);
            btn_editar.TabIndex = 16;
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
            btn_agregar.Location = new Point(5, 4);
            btn_agregar.Name = "btn_agregar";
            btn_agregar.Size = new Size(101, 45);
            btn_agregar.TabIndex = 15;
            btn_agregar.Text = "Agregar";
            btn_agregar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_agregar.UseVisualStyleBackColor = true;
            btn_agregar.Click += btn_agregar_Click;
            // 
            // dtg_cliente_precio
            // 
            dtg_cliente_precio.AllowUserToAddRows = false;
            dtg_cliente_precio.AllowUserToDeleteRows = false;
            dtg_cliente_precio.AllowUserToOrderColumns = true;
            dtg_cliente_precio.BackgroundColor = SystemColors.Control;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.ScrollBar;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dtg_cliente_precio.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dtg_cliente_precio.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dtg_cliente_precio.Columns.AddRange(new DataGridViewColumn[] { cl_id_precio_cliente, cl_IdCliente, cl_numero_documento, cl_nombre, cl_tipo_cliente, cl_producto, cl_sku, cl_codigoBarras, cl_nombre_producto, cl_precioEspacial, cl_fecha_inicial, cl_fecha_final });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(172, 211, 236);
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dtg_cliente_precio.DefaultCellStyle = dataGridViewCellStyle2;
            dtg_cliente_precio.Dock = DockStyle.Fill;
            dtg_cliente_precio.Location = new Point(0, 58);
            dtg_cliente_precio.Name = "dtg_cliente_precio";
            dtg_cliente_precio.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = Color.Silver;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dtg_cliente_precio.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dtg_cliente_precio.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtg_cliente_precio.Size = new Size(1134, 392);
            dtg_cliente_precio.TabIndex = 5;
            // 
            // cl_id_precio_cliente
            // 
            cl_id_precio_cliente.HeaderText = "Id";
            cl_id_precio_cliente.Name = "cl_id_precio_cliente";
            cl_id_precio_cliente.ReadOnly = true;
            cl_id_precio_cliente.Visible = false;
            // 
            // cl_IdCliente
            // 
            cl_IdCliente.HeaderText = "Id";
            cl_IdCliente.Name = "cl_IdCliente";
            cl_IdCliente.ReadOnly = true;
            cl_IdCliente.Visible = false;
            // 
            // cl_numero_documento
            // 
            cl_numero_documento.HeaderText = "Numero documento";
            cl_numero_documento.Name = "cl_numero_documento";
            cl_numero_documento.ReadOnly = true;
            cl_numero_documento.Width = 165;
            // 
            // cl_nombre
            // 
            cl_nombre.HeaderText = "Cliente";
            cl_nombre.Name = "cl_nombre";
            cl_nombre.ReadOnly = true;
            // 
            // cl_tipo_cliente
            // 
            cl_tipo_cliente.HeaderText = "Tipo cliente";
            cl_tipo_cliente.Name = "cl_tipo_cliente";
            cl_tipo_cliente.ReadOnly = true;
            cl_tipo_cliente.Width = 120;
            // 
            // cl_producto
            // 
            cl_producto.HeaderText = "Id";
            cl_producto.Name = "cl_producto";
            cl_producto.ReadOnly = true;
            cl_producto.Width = 60;
            // 
            // cl_sku
            // 
            cl_sku.HeaderText = "Sku";
            cl_sku.Name = "cl_sku";
            cl_sku.ReadOnly = true;
            // 
            // cl_codigoBarras
            // 
            cl_codigoBarras.HeaderText = "Codigo b";
            cl_codigoBarras.Name = "cl_codigoBarras";
            cl_codigoBarras.ReadOnly = true;
            // 
            // cl_nombre_producto
            // 
            cl_nombre_producto.HeaderText = "Producto";
            cl_nombre_producto.Name = "cl_nombre_producto";
            cl_nombre_producto.ReadOnly = true;
            // 
            // cl_precioEspacial
            // 
            cl_precioEspacial.HeaderText = "Precio";
            cl_precioEspacial.Name = "cl_precioEspacial";
            cl_precioEspacial.ReadOnly = true;
            // 
            // cl_fecha_inicial
            // 
            cl_fecha_inicial.HeaderText = "Fecha inicial";
            cl_fecha_inicial.Name = "cl_fecha_inicial";
            cl_fecha_inicial.ReadOnly = true;
            cl_fecha_inicial.Width = 125;
            // 
            // cl_fecha_final
            // 
            cl_fecha_final.HeaderText = "Fecha final";
            cl_fecha_final.Name = "cl_fecha_final";
            cl_fecha_final.ReadOnly = true;
            cl_fecha_final.Width = 120;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // PreciosClientes
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1134, 450);
            Controls.Add(dtg_cliente_precio);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MaximumSize = new Size(1150, 489);
            MinimumSize = new Size(1150, 489);
            Name = "PreciosClientes";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "PreciosClientes";
            Load += PreciosClientes_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dtg_cliente_precio).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button btn_agregar;
        private Button btn_editar;
        private Button btn_eliminar;
        private ComboBox cbx_tipo_filtro;
        private ComboBox cbx_campo_filtro;
        private Button btn_buscar;
        private TextBox txt_buscar;
        private ComboBox cbx_client_producto;
        private DataGridView dtg_cliente_precio;
        private ErrorProvider errorProvider1;
        private DataGridViewTextBoxColumn cl_id_precio_cliente;
        private DataGridViewTextBoxColumn cl_IdCliente;
        private DataGridViewTextBoxColumn cl_numero_documento;
        private DataGridViewTextBoxColumn cl_nombre;
        private DataGridViewTextBoxColumn cl_tipo_cliente;
        private DataGridViewTextBoxColumn cl_producto;
        private DataGridViewTextBoxColumn cl_sku;
        private DataGridViewTextBoxColumn cl_codigoBarras;
        private DataGridViewTextBoxColumn cl_nombre_producto;
        private DataGridViewTextBoxColumn cl_precioEspacial;
        private DataGridViewTextBoxColumn cl_fecha_inicial;
        private DataGridViewTextBoxColumn cl_fecha_final;
    }
}