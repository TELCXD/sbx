namespace sbx
{
    partial class AgregaPreciosCliente
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AgregaPreciosCliente));
            panel3 = new Panel();
            btn_add_precio_cliente = new Button();
            button1 = new Button();
            textBox1 = new TextBox();
            lbl_nombre_cliente = new Label();
            btn_busca_cliente = new Button();
            txt_documento_cliente = new TextBox();
            label1 = new Label();
            label2 = new Label();
            lbl_nombre_producto = new Label();
            btn_busca_producto = new Button();
            txt_producto = new TextBox();
            label3 = new Label();
            txt_precio_especial = new TextBox();
            lbl_fechaVencimiento = new Label();
            dtp_fecha_inicio = new DateTimePicker();
            label4 = new Label();
            dtp_fecha_fin = new DateTimePicker();
            errorProvider1 = new ErrorProvider(components);
            panel3.SuspendLayout();
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
            panel3.Size = new Size(731, 56);
            panel3.TabIndex = 108;
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
            btn_add_precio_cliente.Text = "Agregar";
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
            button1.Location = new Point(3453, 3);
            button1.Name = "button1";
            button1.Size = new Size(42, 45);
            button1.TabIndex = 5;
            button1.TextImageRelation = TextImageRelation.ImageBeforeText;
            button1.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            textBox1.Location = new Point(3256, 14);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(191, 23);
            textBox1.TabIndex = 2;
            // 
            // lbl_nombre_cliente
            // 
            lbl_nombre_cliente.AutoSize = true;
            lbl_nombre_cliente.Location = new Point(12, 124);
            lbl_nombre_cliente.Name = "lbl_nombre_cliente";
            lbl_nombre_cliente.Size = new Size(12, 15);
            lbl_nombre_cliente.TabIndex = 111;
            lbl_nombre_cliente.Text = "_";
            // 
            // btn_busca_cliente
            // 
            btn_busca_cliente.Enabled = false;
            btn_busca_cliente.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_busca_cliente.FlatStyle = FlatStyle.Flat;
            btn_busca_cliente.Image = (Image)resources.GetObject("btn_busca_cliente.Image");
            btn_busca_cliente.Location = new Point(319, 96);
            btn_busca_cliente.Name = "btn_busca_cliente";
            btn_busca_cliente.Size = new Size(26, 26);
            btn_busca_cliente.TabIndex = 3;
            btn_busca_cliente.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_busca_cliente.UseVisualStyleBackColor = true;
            btn_busca_cliente.Click += btn_busca_cliente_Click;
            // 
            // txt_documento_cliente
            // 
            txt_documento_cliente.Enabled = false;
            txt_documento_cliente.Location = new Point(12, 98);
            txt_documento_cliente.MaxLength = 200;
            txt_documento_cliente.Name = "txt_documento_cliente";
            txt_documento_cliente.Size = new Size(277, 23);
            txt_documento_cliente.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 80);
            label1.Name = "label1";
            label1.Size = new Size(44, 15);
            label1.TabIndex = 112;
            label1.Text = "Cliente";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(381, 80);
            label2.Name = "label2";
            label2.Size = new Size(56, 15);
            label2.TabIndex = 116;
            label2.Text = "Producto";
            // 
            // lbl_nombre_producto
            // 
            lbl_nombre_producto.AutoSize = true;
            lbl_nombre_producto.Location = new Point(381, 125);
            lbl_nombre_producto.Name = "lbl_nombre_producto";
            lbl_nombre_producto.Size = new Size(12, 15);
            lbl_nombre_producto.TabIndex = 115;
            lbl_nombre_producto.Text = "_";
            // 
            // btn_busca_producto
            // 
            btn_busca_producto.Enabled = false;
            btn_busca_producto.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_busca_producto.FlatStyle = FlatStyle.Flat;
            btn_busca_producto.Image = (Image)resources.GetObject("btn_busca_producto.Image");
            btn_busca_producto.Location = new Point(688, 96);
            btn_busca_producto.Name = "btn_busca_producto";
            btn_busca_producto.Size = new Size(26, 26);
            btn_busca_producto.TabIndex = 4;
            btn_busca_producto.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_busca_producto.UseVisualStyleBackColor = true;
            btn_busca_producto.Click += btn_busca_producto_Click;
            // 
            // txt_producto
            // 
            txt_producto.Enabled = false;
            txt_producto.Location = new Point(381, 98);
            txt_producto.MaxLength = 200;
            txt_producto.Name = "txt_producto";
            txt_producto.Size = new Size(277, 23);
            txt_producto.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 159);
            label3.Name = "label3";
            label3.Size = new Size(85, 15);
            label3.TabIndex = 118;
            label3.Text = "Precio especial";
            // 
            // txt_precio_especial
            // 
            txt_precio_especial.Location = new Point(12, 177);
            txt_precio_especial.MaxLength = 13;
            txt_precio_especial.Name = "txt_precio_especial";
            txt_precio_especial.Size = new Size(193, 23);
            txt_precio_especial.TabIndex = 5;
            txt_precio_especial.KeyPress += txt_precio_especial_KeyPress;
            // 
            // lbl_fechaVencimiento
            // 
            lbl_fechaVencimiento.AutoSize = true;
            lbl_fechaVencimiento.Location = new Point(237, 159);
            lbl_fechaVencimiento.Name = "lbl_fechaVencimiento";
            lbl_fechaVencimiento.Size = new Size(70, 15);
            lbl_fechaVencimiento.TabIndex = 120;
            lbl_fechaVencimiento.Text = "Fecha inicio";
            // 
            // dtp_fecha_inicio
            // 
            dtp_fecha_inicio.Format = DateTimePickerFormat.Short;
            dtp_fecha_inicio.Location = new Point(237, 177);
            dtp_fecha_inicio.Name = "dtp_fecha_inicio";
            dtp_fecha_inicio.Size = new Size(193, 23);
            dtp_fecha_inicio.TabIndex = 6;
            dtp_fecha_inicio.Value = new DateTime(2025, 5, 23, 12, 41, 44, 0);
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(465, 159);
            label4.Name = "label4";
            label4.Size = new Size(55, 15);
            label4.TabIndex = 122;
            label4.Text = "Fecha fin";
            // 
            // dtp_fecha_fin
            // 
            dtp_fecha_fin.Format = DateTimePickerFormat.Short;
            dtp_fecha_fin.Location = new Point(465, 177);
            dtp_fecha_fin.Name = "dtp_fecha_fin";
            dtp_fecha_fin.Size = new Size(193, 23);
            dtp_fecha_fin.TabIndex = 7;
            dtp_fecha_fin.Value = new DateTime(2025, 5, 23, 12, 41, 51, 0);
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // AgregaPreciosCliente
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            ClientSize = new Size(731, 220);
            Controls.Add(label4);
            Controls.Add(dtp_fecha_fin);
            Controls.Add(lbl_fechaVencimiento);
            Controls.Add(dtp_fecha_inicio);
            Controls.Add(label3);
            Controls.Add(txt_precio_especial);
            Controls.Add(label2);
            Controls.Add(lbl_nombre_producto);
            Controls.Add(btn_busca_producto);
            Controls.Add(txt_producto);
            Controls.Add(label1);
            Controls.Add(lbl_nombre_cliente);
            Controls.Add(btn_busca_cliente);
            Controls.Add(txt_documento_cliente);
            Controls.Add(panel3);
            Name = "AgregaPreciosCliente";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AgregaPreciosCliente";
            Load += AgregaPreciosCliente_Load;
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel3;
        private Button btn_add_precio_cliente;
        private Button button1;
        private TextBox textBox1;
        private Label lbl_nombre_cliente;
        private Button btn_busca_cliente;
        private TextBox txt_documento_cliente;
        private Label label1;
        private Label label2;
        private Label lbl_nombre_producto;
        private Button btn_busca_producto;
        private TextBox txt_producto;
        private Label label3;
        private TextBox txt_precio_especial;
        private Label lbl_fechaVencimiento;
        private DateTimePicker dtp_fecha_inicio;
        private Label label4;
        private DateTimePicker dtp_fecha_fin;
        private ErrorProvider errorProvider1;
    }
}