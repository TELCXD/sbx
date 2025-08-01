namespace sbx
{
    partial class Proveedores
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Proveedores));
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            panel1 = new Panel();
            btn_exportar = new Button();
            cbx_tipo_filtro = new ComboBox();
            cbx_campo_filtro = new ComboBox();
            btn_buscar = new Button();
            txt_buscar = new TextBox();
            btn_eliminar = new Button();
            btn_editar = new Button();
            btn_agregar = new Button();
            dtg_proveedor = new DataGridView();
            cl_IdProveedor = new DataGridViewTextBoxColumn();
            cl_tipo_documento = new DataGridViewTextBoxColumn();
            cl_numero_documento = new DataGridViewTextBoxColumn();
            cl_nombre = new DataGridViewTextBoxColumn();
            cl_ciudad = new DataGridViewTextBoxColumn();
            cl_direccion = new DataGridViewTextBoxColumn();
            cl_telefono = new DataGridViewTextBoxColumn();
            cl_email = new DataGridViewTextBoxColumn();
            cl_estado = new DataGridViewTextBoxColumn();
            cl_tipo_contribuyente = new DataGridViewTextBoxColumn();
            cl_tipo_responsabilidad = new DataGridViewTextBoxColumn();
            cl_tipo_responsabilidad_tributaria = new DataGridViewTextBoxColumn();
            errorProvider1 = new ErrorProvider(components);
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtg_proveedor).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.Window;
            panel1.BorderStyle = BorderStyle.Fixed3D;
            panel1.Controls.Add(btn_exportar);
            panel1.Controls.Add(cbx_tipo_filtro);
            panel1.Controls.Add(cbx_campo_filtro);
            panel1.Controls.Add(btn_buscar);
            panel1.Controls.Add(txt_buscar);
            panel1.Controls.Add(btn_eliminar);
            panel1.Controls.Add(btn_editar);
            panel1.Controls.Add(btn_agregar);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(3, 4, 3, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(1045, 76);
            panel1.TabIndex = 0;
            // 
            // btn_exportar
            // 
            btn_exportar.Enabled = false;
            btn_exportar.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_exportar.FlatStyle = FlatStyle.Flat;
            btn_exportar.Image = (Image)resources.GetObject("btn_exportar.Image");
            btn_exportar.Location = new Point(373, 7);
            btn_exportar.Margin = new Padding(3, 4, 3, 4);
            btn_exportar.Name = "btn_exportar";
            btn_exportar.Size = new Size(53, 60);
            btn_exportar.TabIndex = 32;
            btn_exportar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_exportar.UseVisualStyleBackColor = true;
            btn_exportar.Click += btn_exportar_Click;
            // 
            // cbx_tipo_filtro
            // 
            cbx_tipo_filtro.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cbx_tipo_filtro.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_tipo_filtro.FormattingEnabled = true;
            cbx_tipo_filtro.Items.AddRange(new object[] { "Inicia con", "Igual a", "Contiene" });
            cbx_tipo_filtro.Location = new Point(674, 21);
            cbx_tipo_filtro.Margin = new Padding(3, 4, 3, 4);
            cbx_tipo_filtro.Name = "cbx_tipo_filtro";
            cbx_tipo_filtro.Size = new Size(99, 28);
            cbx_tipo_filtro.TabIndex = 21;
            // 
            // cbx_campo_filtro
            // 
            cbx_campo_filtro.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cbx_campo_filtro.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_campo_filtro.FormattingEnabled = true;
            cbx_campo_filtro.Items.AddRange(new object[] { "Nombre", "Num Doc" });
            cbx_campo_filtro.Location = new Point(537, 21);
            cbx_campo_filtro.Margin = new Padding(3, 4, 3, 4);
            cbx_campo_filtro.Name = "cbx_campo_filtro";
            cbx_campo_filtro.Size = new Size(130, 28);
            cbx_campo_filtro.TabIndex = 20;
            // 
            // btn_buscar
            // 
            btn_buscar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btn_buscar.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_buscar.FlatStyle = FlatStyle.Flat;
            btn_buscar.Image = (Image)resources.GetObject("btn_buscar.Image");
            btn_buscar.Location = new Point(1006, 19);
            btn_buscar.Margin = new Padding(3, 4, 3, 4);
            btn_buscar.Name = "btn_buscar";
            btn_buscar.Size = new Size(30, 35);
            btn_buscar.TabIndex = 19;
            btn_buscar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_buscar.UseVisualStyleBackColor = true;
            btn_buscar.Click += btn_buscar_Click;
            // 
            // txt_buscar
            // 
            txt_buscar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txt_buscar.Location = new Point(780, 21);
            txt_buscar.Margin = new Padding(3, 4, 3, 4);
            txt_buscar.Name = "txt_buscar";
            txt_buscar.Size = new Size(202, 27);
            txt_buscar.TabIndex = 18;
            txt_buscar.KeyPress += txt_buscar_KeyPress;
            // 
            // btn_eliminar
            // 
            btn_eliminar.Enabled = false;
            btn_eliminar.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_eliminar.FlatStyle = FlatStyle.Flat;
            btn_eliminar.Image = (Image)resources.GetObject("btn_eliminar.Image");
            btn_eliminar.Location = new Point(250, 7);
            btn_eliminar.Margin = new Padding(3, 4, 3, 4);
            btn_eliminar.Name = "btn_eliminar";
            btn_eliminar.Size = new Size(115, 60);
            btn_eliminar.TabIndex = 13;
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
            btn_editar.Location = new Point(128, 7);
            btn_editar.Margin = new Padding(3, 4, 3, 4);
            btn_editar.Name = "btn_editar";
            btn_editar.Size = new Size(115, 60);
            btn_editar.TabIndex = 12;
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
            btn_agregar.Location = new Point(6, 7);
            btn_agregar.Margin = new Padding(3, 4, 3, 4);
            btn_agregar.Name = "btn_agregar";
            btn_agregar.Size = new Size(115, 60);
            btn_agregar.TabIndex = 11;
            btn_agregar.Text = "Agregar";
            btn_agregar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_agregar.UseVisualStyleBackColor = true;
            btn_agregar.Click += btn_agregar_Click;
            // 
            // dtg_proveedor
            // 
            dtg_proveedor.AllowUserToAddRows = false;
            dtg_proveedor.AllowUserToDeleteRows = false;
            dtg_proveedor.AllowUserToOrderColumns = true;
            dtg_proveedor.BackgroundColor = SystemColors.Control;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.ScrollBar;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dtg_proveedor.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dtg_proveedor.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dtg_proveedor.Columns.AddRange(new DataGridViewColumn[] { cl_IdProveedor, cl_tipo_documento, cl_numero_documento, cl_nombre, cl_ciudad, cl_direccion, cl_telefono, cl_email, cl_estado, cl_tipo_contribuyente, cl_tipo_responsabilidad, cl_tipo_responsabilidad_tributaria });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(172, 211, 236);
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dtg_proveedor.DefaultCellStyle = dataGridViewCellStyle2;
            dtg_proveedor.Dock = DockStyle.Fill;
            dtg_proveedor.Location = new Point(0, 76);
            dtg_proveedor.Margin = new Padding(3, 4, 3, 4);
            dtg_proveedor.Name = "dtg_proveedor";
            dtg_proveedor.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = Color.Silver;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dtg_proveedor.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dtg_proveedor.RowHeadersWidth = 51;
            dtg_proveedor.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtg_proveedor.Size = new Size(1045, 524);
            dtg_proveedor.TabIndex = 3;
            // 
            // cl_IdProveedor
            // 
            cl_IdProveedor.HeaderText = "Id";
            cl_IdProveedor.MinimumWidth = 6;
            cl_IdProveedor.Name = "cl_IdProveedor";
            cl_IdProveedor.ReadOnly = true;
            cl_IdProveedor.Visible = false;
            cl_IdProveedor.Width = 125;
            // 
            // cl_tipo_documento
            // 
            cl_tipo_documento.HeaderText = "Tipo documento";
            cl_tipo_documento.MinimumWidth = 6;
            cl_tipo_documento.Name = "cl_tipo_documento";
            cl_tipo_documento.ReadOnly = true;
            cl_tipo_documento.Width = 200;
            // 
            // cl_numero_documento
            // 
            cl_numero_documento.HeaderText = "Numero documento";
            cl_numero_documento.MinimumWidth = 6;
            cl_numero_documento.Name = "cl_numero_documento";
            cl_numero_documento.ReadOnly = true;
            cl_numero_documento.Width = 170;
            // 
            // cl_nombre
            // 
            cl_nombre.HeaderText = "Nombre";
            cl_nombre.MinimumWidth = 6;
            cl_nombre.Name = "cl_nombre";
            cl_nombre.ReadOnly = true;
            cl_nombre.Width = 125;
            // 
            // cl_ciudad
            // 
            cl_ciudad.HeaderText = "Ciudad";
            cl_ciudad.MinimumWidth = 6;
            cl_ciudad.Name = "cl_ciudad";
            cl_ciudad.ReadOnly = true;
            cl_ciudad.Width = 125;
            // 
            // cl_direccion
            // 
            cl_direccion.HeaderText = "Direccion";
            cl_direccion.MinimumWidth = 6;
            cl_direccion.Name = "cl_direccion";
            cl_direccion.ReadOnly = true;
            cl_direccion.Width = 125;
            // 
            // cl_telefono
            // 
            cl_telefono.HeaderText = "Telefono";
            cl_telefono.MinimumWidth = 6;
            cl_telefono.Name = "cl_telefono";
            cl_telefono.ReadOnly = true;
            cl_telefono.Width = 125;
            // 
            // cl_email
            // 
            cl_email.HeaderText = "Email";
            cl_email.MinimumWidth = 6;
            cl_email.Name = "cl_email";
            cl_email.ReadOnly = true;
            cl_email.Width = 125;
            // 
            // cl_estado
            // 
            cl_estado.HeaderText = "Estado";
            cl_estado.MinimumWidth = 6;
            cl_estado.Name = "cl_estado";
            cl_estado.ReadOnly = true;
            cl_estado.Width = 125;
            // 
            // cl_tipo_contribuyente
            // 
            cl_tipo_contribuyente.HeaderText = "Tipo contribuyente";
            cl_tipo_contribuyente.MinimumWidth = 6;
            cl_tipo_contribuyente.Name = "cl_tipo_contribuyente";
            cl_tipo_contribuyente.ReadOnly = true;
            cl_tipo_contribuyente.Width = 125;
            // 
            // cl_tipo_responsabilidad
            // 
            cl_tipo_responsabilidad.HeaderText = "Tipo responsabilidad";
            cl_tipo_responsabilidad.MinimumWidth = 6;
            cl_tipo_responsabilidad.Name = "cl_tipo_responsabilidad";
            cl_tipo_responsabilidad.ReadOnly = true;
            cl_tipo_responsabilidad.Width = 125;
            // 
            // cl_tipo_responsabilidad_tributaria
            // 
            cl_tipo_responsabilidad_tributaria.HeaderText = "Tipo responsabilidad tributaria";
            cl_tipo_responsabilidad_tributaria.MinimumWidth = 6;
            cl_tipo_responsabilidad_tributaria.Name = "cl_tipo_responsabilidad_tributaria";
            cl_tipo_responsabilidad_tributaria.ReadOnly = true;
            cl_tipo_responsabilidad_tributaria.Width = 125;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // Proveedores
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1045, 600);
            Controls.Add(dtg_proveedor);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MinimumSize = new Size(1060, 636);
            Name = "Proveedores";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Proveedores";
            Load += Proveedores_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dtg_proveedor).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button btn_eliminar;
        private Button btn_editar;
        private Button btn_agregar;
        private ComboBox cbx_tipo_filtro;
        private ComboBox cbx_campo_filtro;
        private Button btn_buscar;
        private TextBox txt_buscar;
        private DataGridView dtg_proveedor;
        private ErrorProvider errorProvider1;
        private Button btn_exportar;
        private DataGridViewTextBoxColumn cl_IdProveedor;
        private DataGridViewTextBoxColumn cl_tipo_documento;
        private DataGridViewTextBoxColumn cl_numero_documento;
        private DataGridViewTextBoxColumn cl_nombre;
        private DataGridViewTextBoxColumn cl_ciudad;
        private DataGridViewTextBoxColumn cl_direccion;
        private DataGridViewTextBoxColumn cl_telefono;
        private DataGridViewTextBoxColumn cl_email;
        private DataGridViewTextBoxColumn cl_estado;
        private DataGridViewTextBoxColumn cl_tipo_contribuyente;
        private DataGridViewTextBoxColumn cl_tipo_responsabilidad;
        private DataGridViewTextBoxColumn cl_tipo_responsabilidad_tributaria;
    }
}