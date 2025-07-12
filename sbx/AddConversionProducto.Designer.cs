namespace sbx
{
    partial class AddConversionProducto
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddConversionProducto));
            label3 = new Label();
            btn_busca_producto_padre = new Button();
            label1 = new Label();
            btn_busca_producto_hijo = new Button();
            label4 = new Label();
            txt_cantidad = new TextBox();
            txt_producto_padre = new TextBox();
            txt_producto_hijo = new TextBox();
            btn_guardar_conversion = new Button();
            lbl_nombre_producto_padre = new Label();
            lbl_nombre_producto_hijo = new Label();
            panel1 = new Panel();
            errorProvider1 = new ErrorProvider(components);
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 64);
            label3.Name = "label3";
            label3.Size = new Size(89, 15);
            label3.TabIndex = 143;
            label3.Text = "Producto padre";
            // 
            // btn_busca_producto_padre
            // 
            btn_busca_producto_padre.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_busca_producto_padre.FlatStyle = FlatStyle.Flat;
            btn_busca_producto_padre.Image = (Image)resources.GetObject("btn_busca_producto_padre.Image");
            btn_busca_producto_padre.Location = new Point(523, 80);
            btn_busca_producto_padre.Name = "btn_busca_producto_padre";
            btn_busca_producto_padre.Size = new Size(26, 26);
            btn_busca_producto_padre.TabIndex = 1;
            btn_busca_producto_padre.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_busca_producto_padre.UseVisualStyleBackColor = true;
            btn_busca_producto_padre.Click += btn_busca_producto_padre_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 142);
            label1.Name = "label1";
            label1.Size = new Size(79, 15);
            label1.TabIndex = 146;
            label1.Text = "Producto hijo";
            // 
            // btn_busca_producto_hijo
            // 
            btn_busca_producto_hijo.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_busca_producto_hijo.FlatStyle = FlatStyle.Flat;
            btn_busca_producto_hijo.Image = (Image)resources.GetObject("btn_busca_producto_hijo.Image");
            btn_busca_producto_hijo.Location = new Point(523, 158);
            btn_busca_producto_hijo.Name = "btn_busca_producto_hijo";
            btn_busca_producto_hijo.Size = new Size(26, 26);
            btn_busca_producto_hijo.TabIndex = 3;
            btn_busca_producto_hijo.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_busca_producto_hijo.UseVisualStyleBackColor = true;
            btn_busca_producto_hijo.Click += btn_busca_producto_hijo_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 217);
            label4.Name = "label4";
            label4.Size = new Size(146, 15);
            label4.TabIndex = 6;
            label4.Text = "Cantidad de producto hijo";
            // 
            // txt_cantidad
            // 
            txt_cantidad.Location = new Point(12, 235);
            txt_cantidad.MaxLength = 13;
            txt_cantidad.Name = "txt_cantidad";
            txt_cantidad.Size = new Size(209, 23);
            txt_cantidad.TabIndex = 7;
            txt_cantidad.KeyPress += txt_cantidad_KeyPress;
            // 
            // txt_producto_padre
            // 
            txt_producto_padre.Enabled = false;
            txt_producto_padre.Location = new Point(12, 83);
            txt_producto_padre.MaxLength = 13;
            txt_producto_padre.Name = "txt_producto_padre";
            txt_producto_padre.Size = new Size(486, 23);
            txt_producto_padre.TabIndex = 0;
            // 
            // txt_producto_hijo
            // 
            txt_producto_hijo.Enabled = false;
            txt_producto_hijo.Location = new Point(12, 161);
            txt_producto_hijo.MaxLength = 13;
            txt_producto_hijo.Name = "txt_producto_hijo";
            txt_producto_hijo.Size = new Size(486, 23);
            txt_producto_hijo.TabIndex = 2;
            // 
            // btn_guardar_conversion
            // 
            btn_guardar_conversion.Enabled = false;
            btn_guardar_conversion.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_guardar_conversion.FlatStyle = FlatStyle.Flat;
            btn_guardar_conversion.Image = (Image)resources.GetObject("btn_guardar_conversion.Image");
            btn_guardar_conversion.Location = new Point(3, 3);
            btn_guardar_conversion.Name = "btn_guardar_conversion";
            btn_guardar_conversion.Size = new Size(101, 45);
            btn_guardar_conversion.TabIndex = 8;
            btn_guardar_conversion.Text = "Guardar";
            btn_guardar_conversion.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_guardar_conversion.UseVisualStyleBackColor = true;
            btn_guardar_conversion.Click += btn_guardar_agrupacion_Click;
            // 
            // lbl_nombre_producto_padre
            // 
            lbl_nombre_producto_padre.AutoSize = true;
            lbl_nombre_producto_padre.Location = new Point(12, 109);
            lbl_nombre_producto_padre.Name = "lbl_nombre_producto_padre";
            lbl_nombre_producto_padre.Size = new Size(12, 15);
            lbl_nombre_producto_padre.TabIndex = 4;
            lbl_nombre_producto_padre.Text = "_";
            // 
            // lbl_nombre_producto_hijo
            // 
            lbl_nombre_producto_hijo.AutoSize = true;
            lbl_nombre_producto_hijo.Location = new Point(12, 187);
            lbl_nombre_producto_hijo.Name = "lbl_nombre_producto_hijo";
            lbl_nombre_producto_hijo.Size = new Size(12, 15);
            lbl_nombre_producto_hijo.TabIndex = 5;
            lbl_nombre_producto_hijo.Text = "_";
            // 
            // panel1
            // 
            panel1.BackColor = Color.WhiteSmoke;
            panel1.BorderStyle = BorderStyle.Fixed3D;
            panel1.Controls.Add(btn_guardar_conversion);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(561, 56);
            panel1.TabIndex = 147;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // AddConversionProducto
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            ClientSize = new Size(561, 265);
            Controls.Add(panel1);
            Controls.Add(lbl_nombre_producto_hijo);
            Controls.Add(lbl_nombre_producto_padre);
            Controls.Add(txt_producto_hijo);
            Controls.Add(txt_producto_padre);
            Controls.Add(label4);
            Controls.Add(txt_cantidad);
            Controls.Add(label1);
            Controls.Add(btn_busca_producto_hijo);
            Controls.Add(label3);
            Controls.Add(btn_busca_producto_padre);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MaximumSize = new Size(577, 304);
            MinimizeBox = false;
            MinimumSize = new Size(577, 304);
            Name = "AddConversionProducto";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AddAgrupacionProducto";
            Load += AddAgrupacionProducto_Load;
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label3;
        private Button btn_busca_producto_padre;
        private Label label1;
        private Button btn_busca_producto_hijo;
        private Label label4;
        private TextBox txt_cantidad;
        private TextBox txt_producto_padre;
        private TextBox txt_producto_hijo;
        private Button btn_guardar_conversion;
        private Label lbl_nombre_producto_padre;
        private Label lbl_nombre_producto_hijo;
        private Panel panel1;
        private ErrorProvider errorProvider1;
    }
}