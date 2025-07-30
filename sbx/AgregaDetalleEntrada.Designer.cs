namespace sbx
{
    partial class AgregaDetalleEntrada
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AgregaDetalleEntrada));
            btn_busca_pr = new Button();
            label1 = new Label();
            txt_producto = new TextBox();
            panel3 = new Panel();
            btn_add_producto = new Button();
            button1 = new Button();
            textBox1 = new TextBox();
            label2 = new Label();
            txt_lote = new TextBox();
            label3 = new Label();
            txt_costo = new TextBox();
            label4 = new Label();
            txt_cantidad = new TextBox();
            dtp_fecha_vencimiento = new DateTimePicker();
            lbl_fechaVencimiento = new Label();
            label6 = new Label();
            txt_descuento = new TextBox();
            lbl_impuesto = new Label();
            txt_impuesto = new TextBox();
            label8 = new Label();
            txt_subtotal = new TextBox();
            label9 = new Label();
            txt_total = new TextBox();
            lbl_nombre_producto = new Label();
            errorProvider1 = new ErrorProvider(components);
            chek_fecha_vencimiento = new CheckBox();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // btn_busca_pr
            // 
            btn_busca_pr.Enabled = false;
            btn_busca_pr.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_busca_pr.FlatStyle = FlatStyle.Flat;
            btn_busca_pr.Image = (Image)resources.GetObject("btn_busca_pr.Image");
            btn_busca_pr.Location = new Point(441, 86);
            btn_busca_pr.Name = "btn_busca_pr";
            btn_busca_pr.Size = new Size(26, 26);
            btn_busca_pr.TabIndex = 3;
            btn_busca_pr.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_busca_pr.UseVisualStyleBackColor = true;
            btn_busca_pr.Click += btn_busca_pr_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(5, 70);
            label1.Name = "label1";
            label1.Size = new Size(56, 15);
            label1.TabIndex = 105;
            label1.Text = "Producto";
            // 
            // txt_producto
            // 
            txt_producto.Enabled = false;
            txt_producto.Location = new Point(5, 88);
            txt_producto.MaxLength = 200;
            txt_producto.Name = "txt_producto";
            txt_producto.Size = new Size(414, 23);
            txt_producto.TabIndex = 1;
            // 
            // panel3
            // 
            panel3.BackColor = Color.WhiteSmoke;
            panel3.BorderStyle = BorderStyle.Fixed3D;
            panel3.Controls.Add(btn_add_producto);
            panel3.Controls.Add(button1);
            panel3.Controls.Add(textBox1);
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(0, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(717, 56);
            panel3.TabIndex = 107;
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
            button1.Location = new Point(2975, 3);
            button1.Name = "button1";
            button1.Size = new Size(42, 45);
            button1.TabIndex = 5;
            button1.TextImageRelation = TextImageRelation.ImageBeforeText;
            button1.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            textBox1.Location = new Point(2778, 14);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(191, 23);
            textBox1.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 151);
            label2.Name = "label2";
            label2.Size = new Size(30, 15);
            label2.TabIndex = 109;
            label2.Text = "Lote";
            // 
            // txt_lote
            // 
            txt_lote.Location = new Point(6, 169);
            txt_lote.MaxLength = 200;
            txt_lote.Name = "txt_lote";
            txt_lote.Size = new Size(193, 23);
            txt_lote.TabIndex = 4;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(226, 205);
            label3.Name = "label3";
            label3.Size = new Size(222, 15);
            label3.TabIndex = 111;
            label3.Text = "Costo unitario (con impuesto, si aplica) *";
            // 
            // txt_costo
            // 
            txt_costo.Location = new Point(226, 223);
            txt_costo.MaxLength = 13;
            txt_costo.Name = "txt_costo";
            txt_costo.Size = new Size(222, 23);
            txt_costo.TabIndex = 7;
            txt_costo.Text = "0";
            txt_costo.KeyPress += txt_costo_KeyPress;
            txt_costo.KeyUp += txt_costo_KeyUp;
            txt_costo.Leave += txt_costo_Leave;
            txt_costo.Validating += txt_costo_Validating;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 205);
            label4.Name = "label4";
            label4.Size = new Size(55, 15);
            label4.TabIndex = 113;
            label4.Text = "Cantidad";
            // 
            // txt_cantidad
            // 
            txt_cantidad.Location = new Point(6, 223);
            txt_cantidad.MaxLength = 13;
            txt_cantidad.Name = "txt_cantidad";
            txt_cantidad.Size = new Size(193, 23);
            txt_cantidad.TabIndex = 6;
            txt_cantidad.KeyPress += txt_cantidad_KeyPress;
            txt_cantidad.KeyUp += txt_cantidad_KeyUp;
            txt_cantidad.Validating += txt_cantidad_Validating;
            // 
            // dtp_fecha_vencimiento
            // 
            dtp_fecha_vencimiento.Format = DateTimePickerFormat.Short;
            dtp_fecha_vencimiento.Location = new Point(484, 169);
            dtp_fecha_vencimiento.MinDate = new DateTime(2025, 7, 24, 0, 0, 0, 0);
            dtp_fecha_vencimiento.Name = "dtp_fecha_vencimiento";
            dtp_fecha_vencimiento.Size = new Size(209, 23);
            dtp_fecha_vencimiento.TabIndex = 5;
            dtp_fecha_vencimiento.Value = new DateTime(2025, 7, 24, 0, 0, 0, 0);
            dtp_fecha_vencimiento.Visible = false;
            // 
            // lbl_fechaVencimiento
            // 
            lbl_fechaVencimiento.AutoSize = true;
            lbl_fechaVencimiento.Location = new Point(484, 151);
            lbl_fechaVencimiento.Name = "lbl_fechaVencimiento";
            lbl_fechaVencimiento.Size = new Size(107, 15);
            lbl_fechaVencimiento.TabIndex = 115;
            lbl_fechaVencimiento.Text = "Fecha vencimiento";
            lbl_fechaVencimiento.Visible = false;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(6, 256);
            label6.Name = "label6";
            label6.Size = new Size(76, 15);
            label6.TabIndex = 117;
            label6.Text = "% Descuento";
            // 
            // txt_descuento
            // 
            txt_descuento.Location = new Point(6, 274);
            txt_descuento.MaxLength = 13;
            txt_descuento.Name = "txt_descuento";
            txt_descuento.Size = new Size(193, 23);
            txt_descuento.TabIndex = 9;
            txt_descuento.Text = "0";
            txt_descuento.KeyPress += txt_descuento_KeyPress;
            txt_descuento.KeyUp += txt_descuento_KeyUp;
            txt_descuento.Validating += txt_descuento_Validating;
            // 
            // lbl_impuesto
            // 
            lbl_impuesto.AutoSize = true;
            lbl_impuesto.Location = new Point(226, 256);
            lbl_impuesto.Name = "lbl_impuesto";
            lbl_impuesto.Size = new Size(70, 15);
            lbl_impuesto.TabIndex = 119;
            lbl_impuesto.Text = "% Impuesto";
            // 
            // txt_impuesto
            // 
            txt_impuesto.Enabled = false;
            txt_impuesto.Location = new Point(226, 274);
            txt_impuesto.MaxLength = 13;
            txt_impuesto.Name = "txt_impuesto";
            txt_impuesto.Size = new Size(222, 23);
            txt_impuesto.TabIndex = 10;
            txt_impuesto.Text = "0";
            txt_impuesto.KeyPress += txt_iva_KeyPress;
            txt_impuesto.KeyUp += txt_iva_KeyUp;
            txt_impuesto.Validating += txt_iva_Validating;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(484, 205);
            label8.Name = "label8";
            label8.Size = new Size(54, 15);
            label8.TabIndex = 121;
            label8.Text = "Sub total";
            // 
            // txt_subtotal
            // 
            txt_subtotal.Enabled = false;
            txt_subtotal.Location = new Point(484, 223);
            txt_subtotal.MaxLength = 13;
            txt_subtotal.Name = "txt_subtotal";
            txt_subtotal.Size = new Size(209, 23);
            txt_subtotal.TabIndex = 8;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(484, 256);
            label9.Name = "label9";
            label9.Size = new Size(32, 15);
            label9.TabIndex = 123;
            label9.Text = "Total";
            // 
            // txt_total
            // 
            txt_total.Enabled = false;
            txt_total.Location = new Point(484, 274);
            txt_total.MaxLength = 13;
            txt_total.Name = "txt_total";
            txt_total.Size = new Size(209, 23);
            txt_total.TabIndex = 11;
            // 
            // lbl_nombre_producto
            // 
            lbl_nombre_producto.AutoSize = true;
            lbl_nombre_producto.Location = new Point(5, 114);
            lbl_nombre_producto.Name = "lbl_nombre_producto";
            lbl_nombre_producto.Size = new Size(12, 15);
            lbl_nombre_producto.TabIndex = 124;
            lbl_nombre_producto.Text = "_";
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // chek_fecha_vencimiento
            // 
            chek_fecha_vencimiento.AutoSize = true;
            chek_fecha_vencimiento.Location = new Point(226, 169);
            chek_fecha_vencimiento.Name = "chek_fecha_vencimiento";
            chek_fecha_vencimiento.Size = new Size(126, 19);
            chek_fecha_vencimiento.TabIndex = 125;
            chek_fecha_vencimiento.Text = "Fecha vencimiento";
            chek_fecha_vencimiento.UseVisualStyleBackColor = true;
            chek_fecha_vencimiento.CheckedChanged += chek_fecha_vencimiento_CheckedChanged;
            // 
            // AgregaDetalleEntrada
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            ClientSize = new Size(717, 308);
            Controls.Add(chek_fecha_vencimiento);
            Controls.Add(lbl_nombre_producto);
            Controls.Add(label9);
            Controls.Add(txt_total);
            Controls.Add(label8);
            Controls.Add(txt_subtotal);
            Controls.Add(lbl_impuesto);
            Controls.Add(txt_impuesto);
            Controls.Add(label6);
            Controls.Add(txt_descuento);
            Controls.Add(lbl_fechaVencimiento);
            Controls.Add(dtp_fecha_vencimiento);
            Controls.Add(label4);
            Controls.Add(txt_cantidad);
            Controls.Add(label3);
            Controls.Add(txt_costo);
            Controls.Add(label2);
            Controls.Add(txt_lote);
            Controls.Add(panel3);
            Controls.Add(btn_busca_pr);
            Controls.Add(label1);
            Controls.Add(txt_producto);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MaximumSize = new Size(733, 347);
            MinimumSize = new Size(733, 347);
            Name = "AgregaDetalleEntrada";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AgregaDetalleEntrada";
            Load += AgregaDetalleEntrada_Load;
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btn_busca_pr;
        private Label label1;
        private TextBox txt_producto;
        private Panel panel3;
        private Button button1;
        private TextBox textBox1;
        private Button btn_add_producto;
        private Label label2;
        private TextBox txt_lote;
        private Label label3;
        private TextBox txt_costo;
        private Label label4;
        private TextBox txt_cantidad;
        private DateTimePicker dtp_fecha_vencimiento;
        private Label lbl_fechaVencimiento;
        private Label label6;
        private TextBox txt_descuento;
        private Label lbl_impuesto;
        private TextBox txt_impuesto;
        private Label label8;
        private TextBox txt_subtotal;
        private Label label9;
        private TextBox txt_total;
        private Label lbl_nombre_producto;
        private ErrorProvider errorProvider1;
        private CheckBox chek_fecha_vencimiento;
    }
}