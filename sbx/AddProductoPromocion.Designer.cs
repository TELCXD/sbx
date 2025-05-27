namespace sbx
{
    partial class AddProductoPromocion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddProductoPromocion));
            panel3 = new Panel();
            btn_add_producto = new Button();
            button1 = new Button();
            textBox1 = new TextBox();
            label2 = new Label();
            lbl_nombre_producto = new Label();
            btn_busca_producto = new Button();
            txt_producto = new TextBox();
            errorProvider1 = new ErrorProvider(components);
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // panel3
            // 
            panel3.BackColor = SystemColors.Window;
            panel3.BorderStyle = BorderStyle.Fixed3D;
            panel3.Controls.Add(btn_add_producto);
            panel3.Controls.Add(button1);
            panel3.Controls.Add(textBox1);
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(0, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(351, 56);
            panel3.TabIndex = 110;
            // 
            // btn_add_producto
            // 
            btn_add_producto.Enabled = false;
            btn_add_producto.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_add_producto.FlatStyle = FlatStyle.Flat;
            btn_add_producto.Image = (Image)resources.GetObject("btn_add_producto.Image");
            btn_add_producto.Location = new Point(3, 3);
            btn_add_producto.Name = "btn_add_producto";
            btn_add_producto.Size = new Size(101, 45);
            btn_add_producto.TabIndex = 0;
            btn_add_producto.Text = "Agregar";
            btn_add_producto.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_add_producto.UseVisualStyleBackColor = true;
            btn_add_producto.Click += btn_add_producto_Click;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatAppearance.MouseDownBackColor = Color.Gray;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Image = (Image)resources.GetObject("button1.Image");
            button1.Location = new Point(3743, 3);
            button1.Name = "button1";
            button1.Size = new Size(42, 45);
            button1.TabIndex = 5;
            button1.TextImageRelation = TextImageRelation.ImageBeforeText;
            button1.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            textBox1.Location = new Point(3546, 14);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(191, 23);
            textBox1.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 63);
            label2.Name = "label2";
            label2.Size = new Size(56, 15);
            label2.TabIndex = 124;
            label2.Text = "Producto";
            // 
            // lbl_nombre_producto
            // 
            lbl_nombre_producto.AutoSize = true;
            lbl_nombre_producto.Location = new Point(12, 108);
            lbl_nombre_producto.Name = "lbl_nombre_producto";
            lbl_nombre_producto.Size = new Size(12, 15);
            lbl_nombre_producto.TabIndex = 123;
            lbl_nombre_producto.Text = "_";
            // 
            // btn_busca_producto
            // 
            btn_busca_producto.Enabled = false;
            btn_busca_producto.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_busca_producto.FlatStyle = FlatStyle.Flat;
            btn_busca_producto.Image = (Image)resources.GetObject("btn_busca_producto.Image");
            btn_busca_producto.Location = new Point(313, 79);
            btn_busca_producto.Name = "btn_busca_producto";
            btn_busca_producto.Size = new Size(26, 26);
            btn_busca_producto.TabIndex = 122;
            btn_busca_producto.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_busca_producto.UseVisualStyleBackColor = true;
            btn_busca_producto.Click += btn_busca_producto_Click;
            // 
            // txt_producto
            // 
            txt_producto.Enabled = false;
            txt_producto.Location = new Point(12, 81);
            txt_producto.MaxLength = 200;
            txt_producto.Name = "txt_producto";
            txt_producto.Size = new Size(277, 23);
            txt_producto.TabIndex = 121;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // AddProductoPromocion
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(351, 149);
            Controls.Add(label2);
            Controls.Add(lbl_nombre_producto);
            Controls.Add(btn_busca_producto);
            Controls.Add(txt_producto);
            Controls.Add(panel3);
            MaximizeBox = false;
            MaximumSize = new Size(367, 188);
            MinimumSize = new Size(367, 188);
            Name = "AddProductoPromocion";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AddProductoPromocion";
            Load += AddProductoPromocion_Load;
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel3;
        private Button btn_add_producto;
        private Button button1;
        private TextBox textBox1;
        private Label label2;
        private Label lbl_nombre_producto;
        private Button btn_busca_producto;
        private TextBox txt_producto;
        private ErrorProvider errorProvider1;
    }
}