namespace sbx
{
    partial class ConversionProducto
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConversionProducto));
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            panel1 = new Panel();
            cbx_padre_hijo = new ComboBox();
            cbx_tipo_filtro = new ComboBox();
            cbx_campo_filtro = new ComboBox();
            btn_buscar = new Button();
            txt_buscar = new TextBox();
            btn_eliminar = new Button();
            btn_editar = new Button();
            btn_agregar = new Button();
            dtg_producto = new DataGridView();
            cl_id_padre = new DataGridViewTextBoxColumn();
            cl_sku_padre = new DataGridViewTextBoxColumn();
            cl_codigoBarras_padre = new DataGridViewTextBoxColumn();
            cl_nombre_padre = new DataGridViewTextBoxColumn();
            cl_id_Hijo = new DataGridViewTextBoxColumn();
            cl_sku_hijo = new DataGridViewTextBoxColumn();
            cl_codigoBarras_hijo = new DataGridViewTextBoxColumn();
            cl_nombre_hijo = new DataGridViewTextBoxColumn();
            cl_cantidad_hijo = new DataGridViewTextBoxColumn();
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
            panel1.Controls.Add(cbx_padre_hijo);
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
            panel1.Size = new Size(1045, 56);
            panel1.TabIndex = 0;
            // 
            // cbx_padre_hijo
            // 
            cbx_padre_hijo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cbx_padre_hijo.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_padre_hijo.FormattingEnabled = true;
            cbx_padre_hijo.Items.AddRange(new object[] { "Padre", "Hijo" });
            cbx_padre_hijo.Location = new Point(496, 15);
            cbx_padre_hijo.Name = "cbx_padre_hijo";
            cbx_padre_hijo.Size = new Size(114, 23);
            cbx_padre_hijo.TabIndex = 15;
            // 
            // cbx_tipo_filtro
            // 
            cbx_tipo_filtro.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cbx_tipo_filtro.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_tipo_filtro.FormattingEnabled = true;
            cbx_tipo_filtro.Items.AddRange(new object[] { "Inicia con", "Igual a", "Contiene" });
            cbx_tipo_filtro.Location = new Point(736, 15);
            cbx_tipo_filtro.Name = "cbx_tipo_filtro";
            cbx_tipo_filtro.Size = new Size(87, 23);
            cbx_tipo_filtro.TabIndex = 14;
            // 
            // cbx_campo_filtro
            // 
            cbx_campo_filtro.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cbx_campo_filtro.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_campo_filtro.FormattingEnabled = true;
            cbx_campo_filtro.Items.AddRange(new object[] { "Nombre", "Id", "Sku", "Codigo barras" });
            cbx_campo_filtro.Location = new Point(616, 15);
            cbx_campo_filtro.Name = "cbx_campo_filtro";
            cbx_campo_filtro.Size = new Size(114, 23);
            cbx_campo_filtro.TabIndex = 13;
            // 
            // btn_buscar
            // 
            btn_buscar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btn_buscar.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_buscar.FlatStyle = FlatStyle.Flat;
            btn_buscar.Image = (Image)resources.GetObject("btn_buscar.Image");
            btn_buscar.Location = new Point(1012, 13);
            btn_buscar.Name = "btn_buscar";
            btn_buscar.Size = new Size(26, 26);
            btn_buscar.TabIndex = 12;
            btn_buscar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_buscar.UseVisualStyleBackColor = true;
            btn_buscar.Click += btn_buscar_Click;
            // 
            // txt_buscar
            // 
            txt_buscar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txt_buscar.Location = new Point(829, 15);
            txt_buscar.Name = "txt_buscar";
            txt_buscar.Size = new Size(177, 23);
            txt_buscar.TabIndex = 11;
            txt_buscar.KeyPress += txt_buscar_KeyPress;
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
            btn_eliminar.TabIndex = 5;
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
            btn_editar.TabIndex = 3;
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
            btn_agregar.TabIndex = 2;
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
            dtg_producto.Columns.AddRange(new DataGridViewColumn[] { cl_id_padre, cl_sku_padre, cl_codigoBarras_padre, cl_nombre_padre, cl_id_Hijo, cl_sku_hijo, cl_codigoBarras_hijo, cl_nombre_hijo, cl_cantidad_hijo });
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
            dtg_producto.Size = new Size(1045, 456);
            dtg_producto.TabIndex = 3;
            // 
            // cl_id_padre
            // 
            cl_id_padre.HeaderText = "Id padre";
            cl_id_padre.Name = "cl_id_padre";
            cl_id_padre.ReadOnly = true;
            // 
            // cl_sku_padre
            // 
            cl_sku_padre.HeaderText = "Sku padre";
            cl_sku_padre.Name = "cl_sku_padre";
            cl_sku_padre.ReadOnly = true;
            // 
            // cl_codigoBarras_padre
            // 
            cl_codigoBarras_padre.HeaderText = "Codigo barras padre";
            cl_codigoBarras_padre.Name = "cl_codigoBarras_padre";
            cl_codigoBarras_padre.ReadOnly = true;
            cl_codigoBarras_padre.Visible = false;
            // 
            // cl_nombre_padre
            // 
            cl_nombre_padre.HeaderText = "Nombre padre";
            cl_nombre_padre.Name = "cl_nombre_padre";
            cl_nombre_padre.ReadOnly = true;
            cl_nombre_padre.Width = 250;
            // 
            // cl_id_Hijo
            // 
            cl_id_Hijo.HeaderText = "Id Hijo";
            cl_id_Hijo.Name = "cl_id_Hijo";
            cl_id_Hijo.ReadOnly = true;
            // 
            // cl_sku_hijo
            // 
            cl_sku_hijo.HeaderText = "Sku hijo";
            cl_sku_hijo.Name = "cl_sku_hijo";
            cl_sku_hijo.ReadOnly = true;
            // 
            // cl_codigoBarras_hijo
            // 
            cl_codigoBarras_hijo.HeaderText = "Codigo barras hijo";
            cl_codigoBarras_hijo.Name = "cl_codigoBarras_hijo";
            cl_codigoBarras_hijo.ReadOnly = true;
            cl_codigoBarras_hijo.Visible = false;
            // 
            // cl_nombre_hijo
            // 
            cl_nombre_hijo.HeaderText = "Nombre hijo";
            cl_nombre_hijo.Name = "cl_nombre_hijo";
            cl_nombre_hijo.ReadOnly = true;
            cl_nombre_hijo.Width = 250;
            // 
            // cl_cantidad_hijo
            // 
            cl_cantidad_hijo.HeaderText = "Cant. hijo";
            cl_cantidad_hijo.Name = "cl_cantidad_hijo";
            cl_cantidad_hijo.ReadOnly = true;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // ConversionProducto
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1045, 512);
            Controls.Add(dtg_producto);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximumSize = new Size(1061, 551);
            MinimumSize = new Size(1061, 551);
            Name = "ConversionProducto";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ConversionProducto";
            Load += ConversionProducto_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dtg_producto).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button btn_editar;
        private Button btn_agregar;
        private Button btn_eliminar;
        private ComboBox cbx_tipo_filtro;
        private ComboBox cbx_campo_filtro;
        private Button btn_buscar;
        private TextBox txt_buscar;
        private DataGridView dtg_producto;
        private DataGridViewTextBoxColumn cl_id_padre;
        private DataGridViewTextBoxColumn cl_sku_padre;
        private DataGridViewTextBoxColumn cl_codigoBarras_padre;
        private DataGridViewTextBoxColumn cl_nombre_padre;
        private DataGridViewTextBoxColumn cl_id_Hijo;
        private DataGridViewTextBoxColumn cl_sku_hijo;
        private DataGridViewTextBoxColumn cl_codigoBarras_hijo;
        private DataGridViewTextBoxColumn cl_nombre_hijo;
        private DataGridViewTextBoxColumn cl_cantidad_hijo;
        private ErrorProvider errorProvider1;
        private ComboBox cbx_padre_hijo;
    }
}