namespace sbx
{
    partial class AgregaDetalleSalida
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AgregaDetalleSalida));
            panel3 = new Panel();
            btn_add_producto = new Button();
            button1 = new Button();
            textBox1 = new TextBox();
            chek_fecha_vencimiento = new CheckBox();
            lbl_nombre_producto = new Label();
            label8 = new Label();
            txt_total = new TextBox();
            lbl_fechaVencimiento = new Label();
            dtp_fecha_vencimiento = new DateTimePicker();
            label4 = new Label();
            txt_cantidad = new TextBox();
            label3 = new Label();
            txt_costo = new TextBox();
            label2 = new Label();
            txt_lote = new TextBox();
            btn_busca_pr = new Button();
            label1 = new Label();
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
            panel3.Size = new Size(673, 56);
            panel3.TabIndex = 108;
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
            button1.Location = new Point(3395, 3);
            button1.Name = "button1";
            button1.Size = new Size(42, 45);
            button1.TabIndex = 5;
            button1.TextImageRelation = TextImageRelation.ImageBeforeText;
            button1.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            textBox1.Location = new Point(3198, 14);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(191, 23);
            textBox1.TabIndex = 2;
            // 
            // chek_fecha_vencimiento
            // 
            chek_fecha_vencimiento.AutoSize = true;
            chek_fecha_vencimiento.Location = new Point(233, 164);
            chek_fecha_vencimiento.Name = "chek_fecha_vencimiento";
            chek_fecha_vencimiento.Size = new Size(126, 19);
            chek_fecha_vencimiento.TabIndex = 146;
            chek_fecha_vencimiento.Text = "Fecha vencimiento";
            chek_fecha_vencimiento.UseVisualStyleBackColor = true;
            // 
            // lbl_nombre_producto
            // 
            lbl_nombre_producto.AutoSize = true;
            lbl_nombre_producto.Location = new Point(12, 109);
            lbl_nombre_producto.Name = "lbl_nombre_producto";
            lbl_nombre_producto.Size = new Size(12, 15);
            lbl_nombre_producto.TabIndex = 145;
            lbl_nombre_producto.Text = "_";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(455, 200);
            label8.Name = "label8";
            label8.Size = new Size(32, 15);
            label8.TabIndex = 143;
            label8.Text = "Total";
            // 
            // txt_total
            // 
            txt_total.Enabled = false;
            txt_total.Location = new Point(455, 218);
            txt_total.MaxLength = 13;
            txt_total.Name = "txt_total";
            txt_total.Size = new Size(193, 23);
            txt_total.TabIndex = 132;
            // 
            // lbl_fechaVencimiento
            // 
            lbl_fechaVencimiento.AutoSize = true;
            lbl_fechaVencimiento.Location = new Point(455, 146);
            lbl_fechaVencimiento.Name = "lbl_fechaVencimiento";
            lbl_fechaVencimiento.Size = new Size(107, 15);
            lbl_fechaVencimiento.TabIndex = 140;
            lbl_fechaVencimiento.Text = "Fecha vencimiento";
            lbl_fechaVencimiento.Visible = false;
            // 
            // dtp_fecha_vencimiento
            // 
            dtp_fecha_vencimiento.Format = DateTimePickerFormat.Short;
            dtp_fecha_vencimiento.Location = new Point(455, 164);
            dtp_fecha_vencimiento.MinDate = new DateTime(2025, 5, 12, 0, 0, 0, 0);
            dtp_fecha_vencimiento.Name = "dtp_fecha_vencimiento";
            dtp_fecha_vencimiento.Size = new Size(193, 23);
            dtp_fecha_vencimiento.TabIndex = 129;
            dtp_fecha_vencimiento.Value = new DateTime(2025, 5, 12, 20, 35, 24, 0);
            dtp_fecha_vencimiento.Visible = false;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(13, 200);
            label4.Name = "label4";
            label4.Size = new Size(55, 15);
            label4.TabIndex = 139;
            label4.Text = "Cantidad";
            // 
            // txt_cantidad
            // 
            txt_cantidad.Location = new Point(13, 218);
            txt_cantidad.MaxLength = 13;
            txt_cantidad.Name = "txt_cantidad";
            txt_cantidad.Size = new Size(193, 23);
            txt_cantidad.TabIndex = 130;
            txt_cantidad.KeyPress += txt_cantidad_KeyPress;
            txt_cantidad.KeyUp += txt_cantidad_KeyUp;
            txt_cantidad.Validating += txt_cantidad_Validating;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(233, 200);
            label3.Name = "label3";
            label3.Size = new Size(82, 15);
            label3.TabIndex = 138;
            label3.Text = "Costo unitario";
            // 
            // txt_costo
            // 
            txt_costo.Location = new Point(233, 218);
            txt_costo.MaxLength = 13;
            txt_costo.Name = "txt_costo";
            txt_costo.Size = new Size(193, 23);
            txt_costo.TabIndex = 131;
            txt_costo.KeyPress += txt_costo_KeyPress;
            txt_costo.KeyUp += txt_costo_KeyUp;
            txt_costo.Validating += txt_costo_Validating;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(13, 146);
            label2.Name = "label2";
            label2.Size = new Size(30, 15);
            label2.TabIndex = 137;
            label2.Text = "Lote";
            // 
            // txt_lote
            // 
            txt_lote.Location = new Point(13, 164);
            txt_lote.MaxLength = 200;
            txt_lote.Name = "txt_lote";
            txt_lote.Size = new Size(193, 23);
            txt_lote.TabIndex = 128;
            // 
            // btn_busca_pr
            // 
            btn_busca_pr.Enabled = false;
            btn_busca_pr.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_busca_pr.FlatStyle = FlatStyle.Flat;
            btn_busca_pr.Image = (Image)resources.GetObject("btn_busca_pr.Image");
            btn_busca_pr.Location = new Point(448, 81);
            btn_busca_pr.Name = "btn_busca_pr";
            btn_busca_pr.Size = new Size(26, 26);
            btn_busca_pr.TabIndex = 127;
            btn_busca_pr.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_busca_pr.UseVisualStyleBackColor = true;
            btn_busca_pr.Click += btn_busca_pr_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 65);
            label1.Name = "label1";
            label1.Size = new Size(56, 15);
            label1.TabIndex = 136;
            label1.Text = "Producto";
            // 
            // txt_producto
            // 
            txt_producto.Enabled = false;
            txt_producto.Location = new Point(12, 83);
            txt_producto.MaxLength = 200;
            txt_producto.Name = "txt_producto";
            txt_producto.Size = new Size(414, 23);
            txt_producto.TabIndex = 126;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // AgregaDetalleSalida
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            ClientSize = new Size(673, 260);
            Controls.Add(chek_fecha_vencimiento);
            Controls.Add(lbl_nombre_producto);
            Controls.Add(label8);
            Controls.Add(txt_total);
            Controls.Add(lbl_fechaVencimiento);
            Controls.Add(dtp_fecha_vencimiento);
            Controls.Add(label4);
            Controls.Add(txt_cantidad);
            Controls.Add(label3);
            Controls.Add(txt_costo);
            Controls.Add(label2);
            Controls.Add(txt_lote);
            Controls.Add(btn_busca_pr);
            Controls.Add(label1);
            Controls.Add(txt_producto);
            Controls.Add(panel3);
            MaximizeBox = false;
            MaximumSize = new Size(689, 299);
            MinimumSize = new Size(689, 299);
            Name = "AgregaDetalleSalida";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AgregaDetalleSalida";
            Load += AgregaDetalleSalida_Load;
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
        private CheckBox chek_fecha_vencimiento;
        private Label lbl_nombre_producto;
        private Label label8;
        private TextBox txt_total;
        private Label lbl_fechaVencimiento;
        private DateTimePicker dtp_fecha_vencimiento;
        private Label label4;
        private TextBox txt_cantidad;
        private Label label3;
        private TextBox txt_costo;
        private Label label2;
        private TextBox txt_lote;
        private Button btn_busca_pr;
        private Label label1;
        private TextBox txt_producto;
        private ErrorProvider errorProvider1;
    }
}