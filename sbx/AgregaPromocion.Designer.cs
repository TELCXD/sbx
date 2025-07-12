namespace sbx
{
    partial class AgregaPromocion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AgregaPromocion));
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            panel3 = new Panel();
            btn_add_promocion = new Button();
            button1 = new Button();
            textBox1 = new TextBox();
            panel1 = new Panel();
            dtg_producto = new DataGridView();
            cl_idProducto = new DataGridViewTextBoxColumn();
            cl_sku = new DataGridViewTextBoxColumn();
            cl_codigo_barras = new DataGridViewTextBoxColumn();
            cl_nombre = new DataGridViewTextBoxColumn();
            label2 = new Label();
            txt_nombre_promocion = new TextBox();
            btn_quitar_producto = new Button();
            btn_agregar_producto = new Button();
            cbx_tipo_promocion = new ComboBox();
            label1 = new Label();
            label4 = new Label();
            lbl_fechaVencimiento = new Label();
            txt_porcj_desc = new TextBox();
            label3 = new Label();
            errorProvider1 = new ErrorProvider(components);
            dtp_fecha_inicio = new DateTimePicker();
            dtp_fecha_fin = new DateTimePicker();
            panel3.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtg_producto).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // panel3
            // 
            panel3.BackColor = Color.WhiteSmoke;
            panel3.BorderStyle = BorderStyle.Fixed3D;
            panel3.Controls.Add(btn_add_promocion);
            panel3.Controls.Add(button1);
            panel3.Controls.Add(textBox1);
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(0, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(667, 56);
            panel3.TabIndex = 110;
            // 
            // btn_add_promocion
            // 
            btn_add_promocion.Enabled = false;
            btn_add_promocion.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_add_promocion.FlatStyle = FlatStyle.Flat;
            btn_add_promocion.Image = (Image)resources.GetObject("btn_add_promocion.Image");
            btn_add_promocion.Location = new Point(3, 3);
            btn_add_promocion.Name = "btn_add_promocion";
            btn_add_promocion.Size = new Size(101, 45);
            btn_add_promocion.TabIndex = 0;
            btn_add_promocion.Text = "Guardar";
            btn_add_promocion.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_add_promocion.UseVisualStyleBackColor = true;
            btn_add_promocion.Click += btn_add_promocion_Click;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatAppearance.MouseDownBackColor = Color.Gray;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Image = (Image)resources.GetObject("button1.Image");
            button1.Location = new Point(4435, 3);
            button1.Name = "button1";
            button1.Size = new Size(42, 45);
            button1.TabIndex = 5;
            button1.TextImageRelation = TextImageRelation.ImageBeforeText;
            button1.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            textBox1.Location = new Point(4238, 14);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(191, 23);
            textBox1.TabIndex = 2;
            // 
            // panel1
            // 
            panel1.Controls.Add(dtg_producto);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 198);
            panel1.Name = "panel1";
            panel1.Size = new Size(667, 256);
            panel1.TabIndex = 111;
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
            dtg_producto.Columns.AddRange(new DataGridViewColumn[] { cl_idProducto, cl_sku, cl_codigo_barras, cl_nombre });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(172, 211, 236);
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dtg_producto.DefaultCellStyle = dataGridViewCellStyle2;
            dtg_producto.Dock = DockStyle.Fill;
            dtg_producto.Location = new Point(0, 0);
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
            dtg_producto.Size = new Size(667, 256);
            dtg_producto.TabIndex = 4;
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
            cl_nombre.Width = 240;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 61);
            label2.Name = "label2";
            label2.Size = new Size(51, 15);
            label2.TabIndex = 147;
            label2.Text = "Nombre";
            // 
            // txt_nombre_promocion
            // 
            txt_nombre_promocion.Location = new Point(12, 79);
            txt_nombre_promocion.MaxLength = 100;
            txt_nombre_promocion.Name = "txt_nombre_promocion";
            txt_nombre_promocion.Size = new Size(419, 23);
            txt_nombre_promocion.TabIndex = 146;
            // 
            // btn_quitar_producto
            // 
            btn_quitar_producto.Enabled = false;
            btn_quitar_producto.FlatAppearance.BorderSize = 0;
            btn_quitar_producto.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_quitar_producto.FlatStyle = FlatStyle.Flat;
            btn_quitar_producto.Image = (Image)resources.GetObject("btn_quitar_producto.Image");
            btn_quitar_producto.Location = new Point(54, 163);
            btn_quitar_producto.Name = "btn_quitar_producto";
            btn_quitar_producto.Size = new Size(26, 26);
            btn_quitar_producto.TabIndex = 145;
            btn_quitar_producto.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_quitar_producto.UseVisualStyleBackColor = true;
            btn_quitar_producto.Click += btn_quitar_producto_Click;
            // 
            // btn_agregar_producto
            // 
            btn_agregar_producto.Enabled = false;
            btn_agregar_producto.FlatAppearance.BorderSize = 0;
            btn_agregar_producto.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_agregar_producto.FlatStyle = FlatStyle.Flat;
            btn_agregar_producto.Image = (Image)resources.GetObject("btn_agregar_producto.Image");
            btn_agregar_producto.Location = new Point(12, 163);
            btn_agregar_producto.Name = "btn_agregar_producto";
            btn_agregar_producto.Size = new Size(26, 26);
            btn_agregar_producto.TabIndex = 144;
            btn_agregar_producto.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_agregar_producto.UseVisualStyleBackColor = true;
            btn_agregar_producto.Click += btn_agregar_producto_Click;
            // 
            // cbx_tipo_promocion
            // 
            cbx_tipo_promocion.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_tipo_promocion.FormattingEnabled = true;
            cbx_tipo_promocion.Location = new Point(12, 133);
            cbx_tipo_promocion.Name = "cbx_tipo_promocion";
            cbx_tipo_promocion.Size = new Size(193, 23);
            cbx_tipo_promocion.TabIndex = 142;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 115);
            label1.Name = "label1";
            label1.Size = new Size(92, 15);
            label1.TabIndex = 143;
            label1.Text = "Tipo promocion";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(463, 115);
            label4.Name = "label4";
            label4.Size = new Size(55, 15);
            label4.TabIndex = 141;
            label4.Text = "Fecha fin";
            // 
            // lbl_fechaVencimiento
            // 
            lbl_fechaVencimiento.AutoSize = true;
            lbl_fechaVencimiento.Location = new Point(238, 115);
            lbl_fechaVencimiento.Name = "lbl_fechaVencimiento";
            lbl_fechaVencimiento.Size = new Size(70, 15);
            lbl_fechaVencimiento.TabIndex = 140;
            lbl_fechaVencimiento.Text = "Fecha inicio";
            // 
            // txt_porcj_desc
            // 
            txt_porcj_desc.Location = new Point(463, 79);
            txt_porcj_desc.Name = "txt_porcj_desc";
            txt_porcj_desc.Size = new Size(193, 23);
            txt_porcj_desc.TabIndex = 148;
            txt_porcj_desc.KeyPress += txt_porcj_desc_KeyPress;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(463, 61);
            label3.Name = "label3";
            label3.Size = new Size(76, 15);
            label3.TabIndex = 149;
            label3.Text = "% Descuento";
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // dtp_fecha_inicio
            // 
            dtp_fecha_inicio.Format = DateTimePickerFormat.Short;
            dtp_fecha_inicio.Location = new Point(238, 133);
            dtp_fecha_inicio.Name = "dtp_fecha_inicio";
            dtp_fecha_inicio.Size = new Size(193, 23);
            dtp_fecha_inicio.TabIndex = 150;
            // 
            // dtp_fecha_fin
            // 
            dtp_fecha_fin.Format = DateTimePickerFormat.Short;
            dtp_fecha_fin.Location = new Point(463, 133);
            dtp_fecha_fin.Name = "dtp_fecha_fin";
            dtp_fecha_fin.Size = new Size(192, 23);
            dtp_fecha_fin.TabIndex = 151;
            // 
            // AgregaPromocion
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            ClientSize = new Size(667, 454);
            Controls.Add(dtp_fecha_fin);
            Controls.Add(dtp_fecha_inicio);
            Controls.Add(label3);
            Controls.Add(txt_porcj_desc);
            Controls.Add(label2);
            Controls.Add(txt_nombre_promocion);
            Controls.Add(btn_quitar_producto);
            Controls.Add(btn_agregar_producto);
            Controls.Add(cbx_tipo_promocion);
            Controls.Add(label1);
            Controls.Add(label4);
            Controls.Add(lbl_fechaVencimiento);
            Controls.Add(panel1);
            Controls.Add(panel3);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MaximumSize = new Size(683, 493);
            MinimumSize = new Size(683, 493);
            Name = "AgregaPromocion";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AgregaPromocion";
            Load += AgregaPromocion_Load;
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
        private Button btn_add_promocion;
        private Button button1;
        private TextBox textBox1;
        private Panel panel1;
        private DataGridView dtg_producto;
        private Label label2;
        private TextBox txt_nombre_promocion;
        private Button btn_quitar_producto;
        private Button btn_agregar_producto;
        private ComboBox cbx_tipo_promocion;
        private Label label1;
        private Label label4;
        private Label lbl_fechaVencimiento;
        private TextBox txt_porcj_desc;
        private Label label3;
        private DataGridViewTextBoxColumn cl_idProducto;
        private DataGridViewTextBoxColumn cl_sku;
        private DataGridViewTextBoxColumn cl_codigo_barras;
        private DataGridViewTextBoxColumn cl_nombre;
        private ErrorProvider errorProvider1;
        private DateTimePicker dtp_fecha_inicio;
        private DateTimePicker dtp_fecha_fin;
    }
}