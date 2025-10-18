namespace sbx
{
    partial class AgregarProducto
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AgregarProducto));
            cbx_unidad_medida = new ComboBox();
            label9 = new Label();
            cbx_marca = new ComboBox();
            label8 = new Label();
            cbx_categoria = new ComboBox();
            label6 = new Label();
            lbl_tributo = new Label();
            txt_impuesto = new TextBox();
            cbx_es_inventariable = new ComboBox();
            label14 = new Label();
            label4 = new Label();
            txt_precio = new TextBox();
            label3 = new Label();
            txt_costo = new TextBox();
            label2 = new Label();
            txt_nombre = new TextBox();
            label1 = new Label();
            txt_codigo_interno = new TextBox();
            label7 = new Label();
            txt_sku = new TextBox();
            panel3 = new Panel();
            btn_guardar = new Button();
            label10 = new Label();
            txt_codigo_barras = new TextBox();
            errorProvider1 = new ErrorProvider(components);
            cbx_tipo_tributo = new ComboBox();
            label11 = new Label();
            btn_agregaMarca = new Button();
            cbx_tipo_producto = new ComboBox();
            label5 = new Label();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // cbx_unidad_medida
            // 
            cbx_unidad_medida.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_unidad_medida.FormattingEnabled = true;
            cbx_unidad_medida.Location = new Point(363, 313);
            cbx_unidad_medida.Name = "cbx_unidad_medida";
            cbx_unidad_medida.Size = new Size(360, 23);
            cbx_unidad_medida.TabIndex = 10;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(363, 295);
            label9.Name = "label9";
            label9.Size = new Size(88, 15);
            label9.TabIndex = 60;
            label9.Text = "Unidad medida";
            // 
            // cbx_marca
            // 
            cbx_marca.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_marca.FormattingEnabled = true;
            cbx_marca.Location = new Point(363, 258);
            cbx_marca.Name = "cbx_marca";
            cbx_marca.Size = new Size(360, 23);
            cbx_marca.TabIndex = 9;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(363, 240);
            label8.Name = "label8";
            label8.Size = new Size(40, 15);
            label8.TabIndex = 58;
            label8.Text = "Marca";
            // 
            // cbx_categoria
            // 
            cbx_categoria.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_categoria.FormattingEnabled = true;
            cbx_categoria.Location = new Point(363, 203);
            cbx_categoria.Name = "cbx_categoria";
            cbx_categoria.Size = new Size(360, 23);
            cbx_categoria.TabIndex = 8;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(363, 185);
            label6.Name = "label6";
            label6.Size = new Size(58, 15);
            label6.TabIndex = 56;
            label6.Text = "Categoria";
            // 
            // lbl_tributo
            // 
            lbl_tributo.AutoSize = true;
            lbl_tributo.Location = new Point(363, 128);
            lbl_tributo.Name = "lbl_tributo";
            lbl_tributo.Size = new Size(86, 15);
            lbl_tributo.TabIndex = 54;
            lbl_tributo.Text = "Impuesto (%) *";
            // 
            // txt_impuesto
            // 
            txt_impuesto.Location = new Point(363, 148);
            txt_impuesto.MaxLength = 13;
            txt_impuesto.Name = "txt_impuesto";
            txt_impuesto.Size = new Size(360, 23);
            txt_impuesto.TabIndex = 7;
            txt_impuesto.Text = "0";
            txt_impuesto.KeyPress += txt_iva_KeyPress;
            txt_impuesto.Validating += txt_iva_Validating;
            // 
            // cbx_es_inventariable
            // 
            cbx_es_inventariable.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_es_inventariable.Enabled = false;
            cbx_es_inventariable.FormattingEnabled = true;
            cbx_es_inventariable.Items.AddRange(new object[] { "SI", "NO" });
            cbx_es_inventariable.Location = new Point(363, 369);
            cbx_es_inventariable.Name = "cbx_es_inventariable";
            cbx_es_inventariable.Size = new Size(360, 23);
            cbx_es_inventariable.TabIndex = 11;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(363, 351);
            label14.Name = "label14";
            label14.Size = new Size(89, 15);
            label14.TabIndex = 52;
            label14.Text = "Es Inventariable";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 349);
            label4.Name = "label4";
            label4.Size = new Size(224, 15);
            label4.TabIndex = 50;
            label4.Text = "Precio unitario (con impuesto, si aplica) *";
            // 
            // txt_precio
            // 
            txt_precio.Location = new Point(12, 369);
            txt_precio.MaxLength = 13;
            txt_precio.Name = "txt_precio";
            txt_precio.Size = new Size(303, 23);
            txt_precio.TabIndex = 6;
            txt_precio.KeyPress += txt_precio_KeyPress;
            txt_precio.Leave += txt_precio_Leave;
            txt_precio.Validating += txt_precio_Validating;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 293);
            label3.Name = "label3";
            label3.Size = new Size(222, 15);
            label3.TabIndex = 48;
            label3.Text = "Costo unitario (con impuesto, si aplica) *";
            // 
            // txt_costo
            // 
            txt_costo.Location = new Point(12, 313);
            txt_costo.MaxLength = 13;
            txt_costo.Name = "txt_costo";
            txt_costo.Size = new Size(303, 23);
            txt_costo.TabIndex = 5;
            txt_costo.KeyPress += txt_costo_KeyPress;
            txt_costo.Leave += txt_costo_Leave;
            txt_costo.Validating += txt_costo_Validating;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 238);
            label2.Name = "label2";
            label2.Size = new Size(59, 15);
            label2.TabIndex = 46;
            label2.Text = "Nombre *";
            // 
            // txt_nombre
            // 
            txt_nombre.Location = new Point(12, 258);
            txt_nombre.MaxLength = 100;
            txt_nombre.Name = "txt_nombre";
            txt_nombre.Size = new Size(303, 23);
            txt_nombre.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 72);
            label1.Name = "label1";
            label1.Size = new Size(87, 15);
            label1.TabIndex = 44;
            label1.Text = "Codigo interno";
            // 
            // txt_codigo_interno
            // 
            txt_codigo_interno.Enabled = false;
            txt_codigo_interno.Location = new Point(12, 92);
            txt_codigo_interno.MaxLength = 200;
            txt_codigo_interno.Name = "txt_codigo_interno";
            txt_codigo_interno.Size = new Size(303, 23);
            txt_codigo_interno.TabIndex = 1;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(12, 130);
            label7.Name = "label7";
            label7.Size = new Size(28, 15);
            label7.TabIndex = 42;
            label7.Text = "SKU";
            // 
            // txt_sku
            // 
            txt_sku.Location = new Point(12, 148);
            txt_sku.MaxLength = 50;
            txt_sku.Name = "txt_sku";
            txt_sku.Size = new Size(303, 23);
            txt_sku.TabIndex = 2;
            // 
            // panel3
            // 
            panel3.BackColor = Color.WhiteSmoke;
            panel3.BorderStyle = BorderStyle.Fixed3D;
            panel3.Controls.Add(btn_guardar);
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(0, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(769, 56);
            panel3.TabIndex = 61;
            // 
            // btn_guardar
            // 
            btn_guardar.Enabled = false;
            btn_guardar.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_guardar.FlatStyle = FlatStyle.Flat;
            btn_guardar.Image = (Image)resources.GetObject("btn_guardar.Image");
            btn_guardar.Location = new Point(4, 3);
            btn_guardar.Name = "btn_guardar";
            btn_guardar.Size = new Size(96, 45);
            btn_guardar.TabIndex = 0;
            btn_guardar.Text = "Guardar";
            btn_guardar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_guardar.UseVisualStyleBackColor = true;
            btn_guardar.Click += btn_guardar_Click;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(12, 185);
            label10.Name = "label10";
            label10.Size = new Size(81, 15);
            label10.TabIndex = 63;
            label10.Text = "Codigo barras";
            // 
            // txt_codigo_barras
            // 
            txt_codigo_barras.Location = new Point(12, 203);
            txt_codigo_barras.MaxLength = 50;
            txt_codigo_barras.Name = "txt_codigo_barras";
            txt_codigo_barras.Size = new Size(303, 23);
            txt_codigo_barras.TabIndex = 3;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // cbx_tipo_tributo
            // 
            cbx_tipo_tributo.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_tipo_tributo.FormattingEnabled = true;
            cbx_tipo_tributo.Items.AddRange(new object[] { "IVA (Impuesto sobre la Ventas)", "INC (Impuesto Nacional al Consumo)", "INC Bolsas (Impuesto Nacional al Consumo de Bolsa Plástica)" });
            cbx_tipo_tributo.Location = new Point(363, 92);
            cbx_tipo_tributo.Name = "cbx_tipo_tributo";
            cbx_tipo_tributo.Size = new Size(360, 23);
            cbx_tipo_tributo.TabIndex = 64;
            cbx_tipo_tributo.SelectedIndexChanged += cbx_tipo_tributo_SelectedIndexChanged;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(363, 74);
            label11.Name = "label11";
            label11.Size = new Size(72, 15);
            label11.TabIndex = 65;
            label11.Text = "Tipo Tributo";
            // 
            // btn_agregaMarca
            // 
            btn_agregaMarca.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btn_agregaMarca.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_agregaMarca.FlatStyle = FlatStyle.Flat;
            btn_agregaMarca.Image = (Image)resources.GetObject("btn_agregaMarca.Image");
            btn_agregaMarca.Location = new Point(733, 257);
            btn_agregaMarca.Name = "btn_agregaMarca";
            btn_agregaMarca.Size = new Size(26, 26);
            btn_agregaMarca.TabIndex = 66;
            btn_agregaMarca.UseVisualStyleBackColor = true;
            btn_agregaMarca.Click += btn_agregaMarca_Click;
            // 
            // cbx_tipo_producto
            // 
            cbx_tipo_producto.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_tipo_producto.FormattingEnabled = true;
            cbx_tipo_producto.Items.AddRange(new object[] { "Individual", "Grupo" });
            cbx_tipo_producto.Location = new Point(12, 422);
            cbx_tipo_producto.Name = "cbx_tipo_producto";
            cbx_tipo_producto.Size = new Size(303, 23);
            cbx_tipo_producto.TabIndex = 67;
            cbx_tipo_producto.SelectedIndexChanged += cbx_tipo_producto_SelectedIndexChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 404);
            label5.Name = "label5";
            label5.Size = new Size(83, 15);
            label5.TabIndex = 68;
            label5.Text = "Tipo producto";
            // 
            // AgregarProducto
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            ClientSize = new Size(769, 454);
            Controls.Add(cbx_tipo_producto);
            Controls.Add(label5);
            Controls.Add(btn_agregaMarca);
            Controls.Add(cbx_tipo_tributo);
            Controls.Add(label11);
            Controls.Add(label10);
            Controls.Add(txt_codigo_barras);
            Controls.Add(panel3);
            Controls.Add(cbx_unidad_medida);
            Controls.Add(label9);
            Controls.Add(cbx_marca);
            Controls.Add(label8);
            Controls.Add(cbx_categoria);
            Controls.Add(label6);
            Controls.Add(lbl_tributo);
            Controls.Add(txt_impuesto);
            Controls.Add(cbx_es_inventariable);
            Controls.Add(label14);
            Controls.Add(label4);
            Controls.Add(txt_precio);
            Controls.Add(label3);
            Controls.Add(txt_costo);
            Controls.Add(label2);
            Controls.Add(txt_nombre);
            Controls.Add(label1);
            Controls.Add(txt_codigo_interno);
            Controls.Add(label7);
            Controls.Add(txt_sku);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MaximumSize = new Size(785, 493);
            MinimumSize = new Size(785, 493);
            Name = "AgregarProducto";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AgregarProducto";
            Load += AgregarProducto_Load;
            panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cbx_unidad_medida;
        private Label label9;
        private ComboBox cbx_marca;
        private Label label8;
        private ComboBox cbx_categoria;
        private Label label6;
        private Label lbl_tributo;
        private TextBox txt_impuesto;
        private ComboBox cbx_es_inventariable;
        private Label label14;
        private Label label4;
        private TextBox txt_precio;
        private Label label3;
        private TextBox txt_costo;
        private Label label2;
        private TextBox txt_nombre;
        private Label label1;
        private TextBox txt_codigo_interno;
        private Label label7;
        private TextBox txt_sku;
        private Panel panel3;
        private Button button1;
        private TextBox textBox1;
        private Button btn_guardar;
        private Label label10;
        private TextBox txt_codigo_barras;
        private ErrorProvider errorProvider1;
        private ComboBox cbx_tipo_tributo;
        private Label label11;
        private Button btn_agregaMarca;
        private ComboBox cbx_tipo_producto;
        private Label label5;
    }
}