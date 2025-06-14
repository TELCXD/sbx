namespace sbx
{
    partial class Clientes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Clientes));
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            panel1 = new Panel();
            btn_mejor_precio = new Button();
            cbx_tipo_filtro = new ComboBox();
            cbx_campo_filtro = new ComboBox();
            btn_buscar = new Button();
            txt_buscar = new TextBox();
            btn_eliminar = new Button();
            btn_editar = new Button();
            btn_agregar = new Button();
            dtg_cliente = new DataGridView();
            cl_IdCliente = new DataGridViewTextBoxColumn();
            cl_tipo_documento = new DataGridViewTextBoxColumn();
            cl_numero_documento = new DataGridViewTextBoxColumn();
            cl_nombre = new DataGridViewTextBoxColumn();
            cl_tipo_cliente = new DataGridViewTextBoxColumn();
            cl_direccion = new DataGridViewTextBoxColumn();
            cl_telefono = new DataGridViewTextBoxColumn();
            cl_email = new DataGridViewTextBoxColumn();
            cl_estado = new DataGridViewTextBoxColumn();
            errorProvider1 = new ErrorProvider(components);
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtg_cliente).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.Window;
            panel1.BorderStyle = BorderStyle.Fixed3D;
            panel1.Controls.Add(btn_mejor_precio);
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
            panel1.Size = new Size(1033, 68);
            panel1.TabIndex = 0;
            // 
            // btn_mejor_precio
            // 
            btn_mejor_precio.Enabled = false;
            btn_mejor_precio.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_mejor_precio.FlatStyle = FlatStyle.Flat;
            btn_mejor_precio.Image = (Image)resources.GetObject("btn_mejor_precio.Image");
            btn_mejor_precio.Location = new Point(331, 10);
            btn_mejor_precio.Name = "btn_mejor_precio";
            btn_mejor_precio.Size = new Size(109, 45);
            btn_mejor_precio.TabIndex = 30;
            btn_mejor_precio.Text = "Mejor precio";
            btn_mejor_precio.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_mejor_precio.UseVisualStyleBackColor = true;
            btn_mejor_precio.Click += btn_mejor_precio_Click;
            // 
            // cbx_tipo_filtro
            // 
            cbx_tipo_filtro.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cbx_tipo_filtro.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_tipo_filtro.FormattingEnabled = true;
            cbx_tipo_filtro.Items.AddRange(new object[] { "Inicia con", "Igual a", "Contiene" });
            cbx_tipo_filtro.Location = new Point(715, 22);
            cbx_tipo_filtro.Name = "cbx_tipo_filtro";
            cbx_tipo_filtro.Size = new Size(87, 23);
            cbx_tipo_filtro.TabIndex = 29;
            // 
            // cbx_campo_filtro
            // 
            cbx_campo_filtro.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cbx_campo_filtro.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_campo_filtro.FormattingEnabled = true;
            cbx_campo_filtro.Items.AddRange(new object[] { "Nombre", "Num Doc" });
            cbx_campo_filtro.Location = new Point(595, 22);
            cbx_campo_filtro.Name = "cbx_campo_filtro";
            cbx_campo_filtro.Size = new Size(114, 23);
            cbx_campo_filtro.TabIndex = 28;
            // 
            // btn_buscar
            // 
            btn_buscar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btn_buscar.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_buscar.FlatStyle = FlatStyle.Flat;
            btn_buscar.Image = (Image)resources.GetObject("btn_buscar.Image");
            btn_buscar.Location = new Point(999, 20);
            btn_buscar.Name = "btn_buscar";
            btn_buscar.Size = new Size(26, 26);
            btn_buscar.TabIndex = 27;
            btn_buscar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_buscar.UseVisualStyleBackColor = true;
            btn_buscar.Click += btn_buscar_Click;
            // 
            // txt_buscar
            // 
            txt_buscar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txt_buscar.Location = new Point(808, 22);
            txt_buscar.Name = "txt_buscar";
            txt_buscar.Size = new Size(177, 23);
            txt_buscar.TabIndex = 26;
            // 
            // btn_eliminar
            // 
            btn_eliminar.Enabled = false;
            btn_eliminar.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_eliminar.FlatStyle = FlatStyle.Flat;
            btn_eliminar.Image = (Image)resources.GetObject("btn_eliminar.Image");
            btn_eliminar.Location = new Point(224, 10);
            btn_eliminar.Name = "btn_eliminar";
            btn_eliminar.Size = new Size(101, 45);
            btn_eliminar.TabIndex = 16;
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
            btn_editar.Location = new Point(117, 10);
            btn_editar.Name = "btn_editar";
            btn_editar.Size = new Size(101, 45);
            btn_editar.TabIndex = 15;
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
            btn_agregar.Location = new Point(10, 10);
            btn_agregar.Name = "btn_agregar";
            btn_agregar.Size = new Size(101, 45);
            btn_agregar.TabIndex = 14;
            btn_agregar.Text = "Agregar";
            btn_agregar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_agregar.UseVisualStyleBackColor = true;
            btn_agregar.Click += btn_agregar_Click;
            // 
            // dtg_cliente
            // 
            dtg_cliente.AllowUserToAddRows = false;
            dtg_cliente.AllowUserToDeleteRows = false;
            dtg_cliente.AllowUserToOrderColumns = true;
            dtg_cliente.BackgroundColor = SystemColors.Control;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.ScrollBar;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dtg_cliente.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dtg_cliente.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dtg_cliente.Columns.AddRange(new DataGridViewColumn[] { cl_IdCliente, cl_tipo_documento, cl_numero_documento, cl_nombre, cl_tipo_cliente, cl_direccion, cl_telefono, cl_email, cl_estado });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(172, 211, 236);
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dtg_cliente.DefaultCellStyle = dataGridViewCellStyle2;
            dtg_cliente.Dock = DockStyle.Fill;
            dtg_cliente.Location = new Point(0, 68);
            dtg_cliente.Name = "dtg_cliente";
            dtg_cliente.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = Color.Silver;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dtg_cliente.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dtg_cliente.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtg_cliente.Size = new Size(1033, 382);
            dtg_cliente.TabIndex = 4;
            // 
            // cl_IdCliente
            // 
            cl_IdCliente.HeaderText = "Id";
            cl_IdCliente.Name = "cl_IdCliente";
            cl_IdCliente.ReadOnly = true;
            cl_IdCliente.Visible = false;
            // 
            // cl_tipo_documento
            // 
            cl_tipo_documento.HeaderText = "Tipo documento";
            cl_tipo_documento.Name = "cl_tipo_documento";
            cl_tipo_documento.ReadOnly = true;
            cl_tipo_documento.Width = 200;
            // 
            // cl_numero_documento
            // 
            cl_numero_documento.HeaderText = "Numero documento";
            cl_numero_documento.Name = "cl_numero_documento";
            cl_numero_documento.ReadOnly = true;
            cl_numero_documento.Width = 170;
            // 
            // cl_nombre
            // 
            cl_nombre.HeaderText = "Nombre";
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
            // cl_direccion
            // 
            cl_direccion.HeaderText = "Direccion";
            cl_direccion.Name = "cl_direccion";
            cl_direccion.ReadOnly = true;
            // 
            // cl_telefono
            // 
            cl_telefono.HeaderText = "Telefono";
            cl_telefono.Name = "cl_telefono";
            cl_telefono.ReadOnly = true;
            // 
            // cl_email
            // 
            cl_email.HeaderText = "Email";
            cl_email.Name = "cl_email";
            cl_email.ReadOnly = true;
            // 
            // cl_estado
            // 
            cl_estado.HeaderText = "Estado";
            cl_estado.Name = "cl_estado";
            cl_estado.ReadOnly = true;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // Clientes
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1033, 450);
            Controls.Add(dtg_cliente);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MaximumSize = new Size(1049, 489);
            MinimumSize = new Size(1049, 489);
            Name = "Clientes";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Clientes";
            Load += Clientes_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dtg_cliente).EndInit();
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
        private DataGridView dtg_cliente;
        private ErrorProvider errorProvider1;
        private DataGridViewTextBoxColumn cl_IdCliente;
        private DataGridViewTextBoxColumn cl_tipo_documento;
        private DataGridViewTextBoxColumn cl_numero_documento;
        private DataGridViewTextBoxColumn cl_nombre;
        private DataGridViewTextBoxColumn cl_tipo_cliente;
        private DataGridViewTextBoxColumn cl_direccion;
        private DataGridViewTextBoxColumn cl_telefono;
        private DataGridViewTextBoxColumn cl_email;
        private DataGridViewTextBoxColumn cl_estado;
        private Button btn_mejor_precio;
    }
}