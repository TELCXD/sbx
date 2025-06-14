namespace sbx
{
    partial class AddProductoListaPrecio
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddProductoListaPrecio));
            panel3 = new Panel();
            btn_add_precio_producto_lista = new Button();
            button1 = new Button();
            textBox1 = new TextBox();
            label2 = new Label();
            lbl_nombre_producto = new Label();
            btn_busca_producto = new Button();
            txt_producto = new TextBox();
            label3 = new Label();
            txt_precio = new TextBox();
            errorProvider1 = new ErrorProvider(components);
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // panel3
            // 
            panel3.BackColor = SystemColors.Window;
            panel3.BorderStyle = BorderStyle.Fixed3D;
            panel3.Controls.Add(btn_add_precio_producto_lista);
            panel3.Controls.Add(button1);
            panel3.Controls.Add(textBox1);
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(0, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(347, 56);
            panel3.TabIndex = 109;
            // 
            // btn_add_precio_producto_lista
            // 
            btn_add_precio_producto_lista.Enabled = false;
            btn_add_precio_producto_lista.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_add_precio_producto_lista.FlatStyle = FlatStyle.Flat;
            btn_add_precio_producto_lista.Image = (Image)resources.GetObject("btn_add_precio_producto_lista.Image");
            btn_add_precio_producto_lista.Location = new Point(3, 3);
            btn_add_precio_producto_lista.Name = "btn_add_precio_producto_lista";
            btn_add_precio_producto_lista.Size = new Size(101, 45);
            btn_add_precio_producto_lista.TabIndex = 0;
            btn_add_precio_producto_lista.Text = "Agregar";
            btn_add_precio_producto_lista.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_add_precio_producto_lista.UseVisualStyleBackColor = true;
            btn_add_precio_producto_lista.Click += btn_add_precio_producto_lista_Click;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatAppearance.MouseDownBackColor = Color.Gray;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Image = (Image)resources.GetObject("button1.Image");
            button1.Location = new Point(3596, 3);
            button1.Name = "button1";
            button1.Size = new Size(42, 45);
            button1.TabIndex = 5;
            button1.TextImageRelation = TextImageRelation.ImageBeforeText;
            button1.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            textBox1.Location = new Point(3399, 14);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(191, 23);
            textBox1.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 64);
            label2.Name = "label2";
            label2.Size = new Size(56, 15);
            label2.TabIndex = 120;
            label2.Text = "Producto";
            // 
            // lbl_nombre_producto
            // 
            lbl_nombre_producto.AutoSize = true;
            lbl_nombre_producto.Location = new Point(12, 109);
            lbl_nombre_producto.Name = "lbl_nombre_producto";
            lbl_nombre_producto.Size = new Size(12, 15);
            lbl_nombre_producto.TabIndex = 119;
            lbl_nombre_producto.Text = "_";
            // 
            // btn_busca_producto
            // 
            btn_busca_producto.Enabled = false;
            btn_busca_producto.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_busca_producto.FlatStyle = FlatStyle.Flat;
            btn_busca_producto.Image = (Image)resources.GetObject("btn_busca_producto.Image");
            btn_busca_producto.Location = new Point(313, 80);
            btn_busca_producto.Name = "btn_busca_producto";
            btn_busca_producto.Size = new Size(26, 26);
            btn_busca_producto.TabIndex = 118;
            btn_busca_producto.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_busca_producto.UseVisualStyleBackColor = true;
            btn_busca_producto.Click += btn_busca_producto_Click;
            // 
            // txt_producto
            // 
            txt_producto.Enabled = false;
            txt_producto.Location = new Point(12, 82);
            txt_producto.MaxLength = 200;
            txt_producto.Name = "txt_producto";
            txt_producto.Size = new Size(277, 23);
            txt_producto.TabIndex = 117;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 141);
            label3.Name = "label3";
            label3.Size = new Size(85, 15);
            label3.TabIndex = 122;
            label3.Text = "Precio especial";
            // 
            // txt_precio
            // 
            txt_precio.Location = new Point(12, 159);
            txt_precio.MaxLength = 13;
            txt_precio.Name = "txt_precio";
            txt_precio.Size = new Size(277, 23);
            txt_precio.TabIndex = 121;
            txt_precio.KeyPress += txt_precio_KeyPress;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // AddProductoListaPrecio
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(347, 200);
            Controls.Add(label3);
            Controls.Add(txt_precio);
            Controls.Add(label2);
            Controls.Add(lbl_nombre_producto);
            Controls.Add(btn_busca_producto);
            Controls.Add(txt_producto);
            Controls.Add(panel3);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "AddProductoListaPrecio";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AddProductoListaPrecio";
            Load += AddProductoListaPrecio_Load;
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel3;
        private Button btn_add_precio_producto_lista;
        private Button button1;
        private TextBox textBox1;
        private Label label2;
        private Label lbl_nombre_producto;
        private Button btn_busca_producto;
        private TextBox txt_producto;
        private Label label3;
        private TextBox txt_precio;
        private ErrorProvider errorProvider1;
    }
}