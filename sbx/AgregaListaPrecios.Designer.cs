namespace sbx
{
    partial class AgregaListaPrecios
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AgregaListaPrecios));
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            panel3 = new Panel();
            btn_add_precio_cliente = new Button();
            button1 = new Button();
            textBox1 = new TextBox();
            label4 = new Label();
            lbl_fechaVencimiento = new Label();
            cbx_tipo_cliente = new ComboBox();
            label1 = new Label();
            btn_agregar_producto = new Button();
            panel1 = new Panel();
            dtg_producto = new DataGridView();
            cl_idProducto = new DataGridViewTextBoxColumn();
            cl_sku = new DataGridViewTextBoxColumn();
            cl_codigo_barras = new DataGridViewTextBoxColumn();
            cl_nombre = new DataGridViewTextBoxColumn();
            cl_precio = new DataGridViewTextBoxColumn();
            btn_quitar_producto = new Button();
            label2 = new Label();
            txt_nombre_lista_precios = new TextBox();
            errorProvider1 = new ErrorProvider(components);
            dtp_fecha_fin = new DateTimePicker();
            dtp_fecha_inicio = new DateTimePicker();
            panel3.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtg_producto).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // panel3
            // 
            panel3.BackColor = SystemColors.Window;
            panel3.BorderStyle = BorderStyle.Fixed3D;
            panel3.Controls.Add(btn_add_precio_cliente);
            panel3.Controls.Add(button1);
            panel3.Controls.Add(textBox1);
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(0, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(723, 56);
            panel3.TabIndex = 109;
            // 
            // btn_add_precio_cliente
            // 
            btn_add_precio_cliente.Enabled = false;
            btn_add_precio_cliente.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_add_precio_cliente.FlatStyle = FlatStyle.Flat;
            btn_add_precio_cliente.Image = (Image)resources.GetObject("btn_add_precio_cliente.Image");
            btn_add_precio_cliente.Location = new Point(3, 3);
            btn_add_precio_cliente.Name = "btn_add_precio_cliente";
            btn_add_precio_cliente.Size = new Size(101, 45);
            btn_add_precio_cliente.TabIndex = 0;
            btn_add_precio_cliente.Text = "Guardar";
            btn_add_precio_cliente.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_add_precio_cliente.UseVisualStyleBackColor = true;
            btn_add_precio_cliente.Click += btn_add_precio_cliente_Click;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatAppearance.MouseDownBackColor = Color.Gray;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Image = (Image)resources.GetObject("button1.Image");
            button1.Location = new Point(3972, 3);
            button1.Name = "button1";
            button1.Size = new Size(42, 45);
            button1.TabIndex = 5;
            button1.TextImageRelation = TextImageRelation.ImageBeforeText;
            button1.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            textBox1.Location = new Point(3775, 14);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(191, 23);
            textBox1.TabIndex = 2;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(463, 113);
            label4.Name = "label4";
            label4.Size = new Size(55, 15);
            label4.TabIndex = 128;
            label4.Text = "Fecha fin";
            // 
            // lbl_fechaVencimiento
            // 
            lbl_fechaVencimiento.AutoSize = true;
            lbl_fechaVencimiento.Location = new Point(238, 113);
            lbl_fechaVencimiento.Name = "lbl_fechaVencimiento";
            lbl_fechaVencimiento.Size = new Size(70, 15);
            lbl_fechaVencimiento.TabIndex = 127;
            lbl_fechaVencimiento.Text = "Fecha inicio";
            // 
            // cbx_tipo_cliente
            // 
            cbx_tipo_cliente.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_tipo_cliente.FormattingEnabled = true;
            cbx_tipo_cliente.Location = new Point(12, 131);
            cbx_tipo_cliente.Name = "cbx_tipo_cliente";
            cbx_tipo_cliente.Size = new Size(193, 23);
            cbx_tipo_cliente.TabIndex = 129;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 113);
            label1.Name = "label1";
            label1.Size = new Size(68, 15);
            label1.TabIndex = 130;
            label1.Text = "Tipo cliente";
            // 
            // btn_agregar_producto
            // 
            btn_agregar_producto.Enabled = false;
            btn_agregar_producto.FlatAppearance.BorderSize = 0;
            btn_agregar_producto.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_agregar_producto.FlatStyle = FlatStyle.Flat;
            btn_agregar_producto.Image = (Image)resources.GetObject("btn_agregar_producto.Image");
            btn_agregar_producto.Location = new Point(12, 161);
            btn_agregar_producto.Name = "btn_agregar_producto";
            btn_agregar_producto.Size = new Size(26, 26);
            btn_agregar_producto.TabIndex = 132;
            btn_agregar_producto.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_agregar_producto.UseVisualStyleBackColor = true;
            btn_agregar_producto.Click += btn_agregar_producto_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(dtg_producto);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 193);
            panel1.Name = "panel1";
            panel1.Size = new Size(723, 257);
            panel1.TabIndex = 134;
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
            dtg_producto.Columns.AddRange(new DataGridViewColumn[] { cl_idProducto, cl_sku, cl_codigo_barras, cl_nombre, cl_precio });
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = SystemColors.Window;
            dataGridViewCellStyle5.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle5.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = Color.FromArgb(172, 211, 236);
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.ControlText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.False;
            dtg_producto.DefaultCellStyle = dataGridViewCellStyle5;
            dtg_producto.Dock = DockStyle.Fill;
            dtg_producto.Location = new Point(0, 0);
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
            dtg_producto.Size = new Size(723, 257);
            dtg_producto.TabIndex = 3;
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
            cl_sku.Width = 130;
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
            cl_nombre.Width = 150;
            // 
            // cl_precio
            // 
            cl_precio.HeaderText = "Precio";
            cl_precio.Name = "cl_precio";
            cl_precio.ReadOnly = true;
            cl_precio.Width = 150;
            // 
            // btn_quitar_producto
            // 
            btn_quitar_producto.Enabled = false;
            btn_quitar_producto.FlatAppearance.BorderSize = 0;
            btn_quitar_producto.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_quitar_producto.FlatStyle = FlatStyle.Flat;
            btn_quitar_producto.Image = (Image)resources.GetObject("btn_quitar_producto.Image");
            btn_quitar_producto.Location = new Point(54, 161);
            btn_quitar_producto.Name = "btn_quitar_producto";
            btn_quitar_producto.Size = new Size(26, 26);
            btn_quitar_producto.TabIndex = 135;
            btn_quitar_producto.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_quitar_producto.UseVisualStyleBackColor = true;
            btn_quitar_producto.Click += btn_quitar_producto_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 59);
            label2.Name = "label2";
            label2.Size = new Size(51, 15);
            label2.TabIndex = 137;
            label2.Text = "Nombre";
            // 
            // txt_nombre_lista_precios
            // 
            txt_nombre_lista_precios.Location = new Point(12, 77);
            txt_nombre_lista_precios.MaxLength = 100;
            txt_nombre_lista_precios.Name = "txt_nombre_lista_precios";
            txt_nombre_lista_precios.Size = new Size(419, 23);
            txt_nombre_lista_precios.TabIndex = 136;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // dtp_fecha_fin
            // 
            dtp_fecha_fin.Format = DateTimePickerFormat.Short;
            dtp_fecha_fin.Location = new Point(463, 131);
            dtp_fecha_fin.Name = "dtp_fecha_fin";
            dtp_fecha_fin.Size = new Size(187, 23);
            dtp_fecha_fin.TabIndex = 139;
            // 
            // dtp_fecha_inicio
            // 
            dtp_fecha_inicio.Format = DateTimePickerFormat.Short;
            dtp_fecha_inicio.Location = new Point(238, 131);
            dtp_fecha_inicio.Name = "dtp_fecha_inicio";
            dtp_fecha_inicio.Size = new Size(187, 23);
            dtp_fecha_inicio.TabIndex = 140;
            // 
            // AgregaListaPrecios
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            ClientSize = new Size(723, 450);
            Controls.Add(dtp_fecha_inicio);
            Controls.Add(dtp_fecha_fin);
            Controls.Add(label2);
            Controls.Add(txt_nombre_lista_precios);
            Controls.Add(btn_quitar_producto);
            Controls.Add(panel1);
            Controls.Add(btn_agregar_producto);
            Controls.Add(cbx_tipo_cliente);
            Controls.Add(label1);
            Controls.Add(label4);
            Controls.Add(lbl_fechaVencimiento);
            Controls.Add(panel3);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "AgregaListaPrecios";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AgregaListaPrecios";
            Load += AgregaListaPrecios_Load;
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dtg_producto).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel3;
        private Button btn_add_precio_cliente;
        private Button button1;
        private TextBox textBox1;
        private Label label4;
        private Label lbl_fechaVencimiento;
        private DateTimePicker dtp_fecha_fin;
        private ComboBox cbx_tipo_cliente;
        private Label label1;
        private Button btn_agregar_producto;
        private Panel panel1;
        private DataGridView dtg_producto;
        private Button btn_quitar_producto;
        private Label label2;
        private TextBox txt_nombre_lista_precios;
        private DataGridViewTextBoxColumn cl_idProducto;
        private DataGridViewTextBoxColumn cl_sku;
        private DataGridViewTextBoxColumn cl_codigo_barras;
        private DataGridViewTextBoxColumn cl_nombre;
        private DataGridViewTextBoxColumn cl_precio;
        private ErrorProvider errorProvider1;
        private DateTimePicker dtp_fecha_inicio;
    }
}