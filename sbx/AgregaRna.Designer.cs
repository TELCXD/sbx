namespace sbx
{
    partial class AgregaRna
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AgregaRna));
            panel3 = new Panel();
            button1 = new Button();
            textBox3 = new TextBox();
            btn_guardar = new Button();
            panel4 = new Panel();
            cbx_numeracion_autorizada = new ComboBox();
            dtpk_fecha_vencimiento = new DateTimePicker();
            cbx_tipo_documento = new ComboBox();
            label14 = new Label();
            label13 = new Label();
            txt_prefijo = new TextBox();
            label9 = new Label();
            label8 = new Label();
            label7 = new Label();
            txt_numero_hasta = new TextBox();
            cbx_estado = new ComboBox();
            label5 = new Label();
            label2 = new Label();
            txt_numero_autorizacion = new TextBox();
            label1 = new Label();
            txt_numero_desde = new TextBox();
            errorProvider1 = new ErrorProvider(components);
            panel3.SuspendLayout();
            panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // panel3
            // 
            panel3.BackColor = Color.WhiteSmoke;
            panel3.BorderStyle = BorderStyle.Fixed3D;
            panel3.Controls.Add(button1);
            panel3.Controls.Add(textBox3);
            panel3.Controls.Add(btn_guardar);
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(0, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(723, 56);
            panel3.TabIndex = 3;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatAppearance.MouseDownBackColor = Color.Gray;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Image = (Image)resources.GetObject("button1.Image");
            button1.Location = new Point(2483, 3);
            button1.Name = "button1";
            button1.Size = new Size(42, 45);
            button1.TabIndex = 5;
            button1.TextImageRelation = TextImageRelation.ImageBeforeText;
            button1.UseVisualStyleBackColor = true;
            // 
            // textBox3
            // 
            textBox3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            textBox3.Location = new Point(2286, 14);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(191, 23);
            textBox3.TabIndex = 2;
            // 
            // btn_guardar
            // 
            btn_guardar.Enabled = false;
            btn_guardar.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_guardar.FlatStyle = FlatStyle.Flat;
            btn_guardar.Image = (Image)resources.GetObject("btn_guardar.Image");
            btn_guardar.Location = new Point(3, 3);
            btn_guardar.Name = "btn_guardar";
            btn_guardar.Size = new Size(96, 45);
            btn_guardar.TabIndex = 0;
            btn_guardar.Text = "Guardar";
            btn_guardar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_guardar.UseVisualStyleBackColor = true;
            btn_guardar.Click += btn_guardar_Click;
            // 
            // panel4
            // 
            panel4.BackColor = SystemColors.Window;
            panel4.BorderStyle = BorderStyle.Fixed3D;
            panel4.Controls.Add(cbx_numeracion_autorizada);
            panel4.Controls.Add(dtpk_fecha_vencimiento);
            panel4.Controls.Add(cbx_tipo_documento);
            panel4.Controls.Add(label14);
            panel4.Controls.Add(label13);
            panel4.Controls.Add(txt_prefijo);
            panel4.Controls.Add(label9);
            panel4.Controls.Add(label8);
            panel4.Controls.Add(label7);
            panel4.Controls.Add(txt_numero_hasta);
            panel4.Controls.Add(cbx_estado);
            panel4.Controls.Add(label5);
            panel4.Controls.Add(label2);
            panel4.Controls.Add(txt_numero_autorizacion);
            panel4.Controls.Add(label1);
            panel4.Controls.Add(txt_numero_desde);
            panel4.Dock = DockStyle.Fill;
            panel4.Location = new Point(0, 56);
            panel4.Name = "panel4";
            panel4.Size = new Size(723, 260);
            panel4.TabIndex = 4;
            // 
            // cbx_numeracion_autorizada
            // 
            cbx_numeracion_autorizada.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_numeracion_autorizada.FormattingEnabled = true;
            cbx_numeracion_autorizada.Items.AddRange(new object[] { "Si", "No" });
            cbx_numeracion_autorizada.Location = new Point(374, 200);
            cbx_numeracion_autorizada.Name = "cbx_numeracion_autorizada";
            cbx_numeracion_autorizada.Size = new Size(303, 23);
            cbx_numeracion_autorizada.TabIndex = 8;
            // 
            // dtpk_fecha_vencimiento
            // 
            dtpk_fecha_vencimiento.Format = DateTimePickerFormat.Short;
            dtpk_fecha_vencimiento.Location = new Point(374, 145);
            dtpk_fecha_vencimiento.MinDate = new DateTime(2025, 4, 17, 0, 0, 0, 0);
            dtpk_fecha_vencimiento.Name = "dtpk_fecha_vencimiento";
            dtpk_fecha_vencimiento.Size = new Size(303, 23);
            dtpk_fecha_vencimiento.TabIndex = 6;
            // 
            // cbx_tipo_documento
            // 
            cbx_tipo_documento.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_tipo_documento.FormattingEnabled = true;
            cbx_tipo_documento.Location = new Point(41, 39);
            cbx_tipo_documento.Name = "cbx_tipo_documento";
            cbx_tipo_documento.Size = new Size(287, 23);
            cbx_tipo_documento.TabIndex = 1;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(41, 19);
            label14.Name = "label14";
            label14.Size = new Size(103, 15);
            label14.TabIndex = 30;
            label14.Text = "Tipo documento *";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(374, 19);
            label13.Name = "label13";
            label13.Size = new Size(49, 15);
            label13.TabIndex = 26;
            label13.Text = "Prefijo *";
            // 
            // txt_prefijo
            // 
            txt_prefijo.Location = new Point(374, 39);
            txt_prefijo.MaxLength = 10;
            txt_prefijo.Name = "txt_prefijo";
            txt_prefijo.Size = new Size(303, 23);
            txt_prefijo.TabIndex = 2;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(374, 180);
            label9.Name = "label9";
            label9.Size = new Size(139, 15);
            label9.TabIndex = 17;
            label9.Text = "Numeración autorizada *";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(374, 123);
            label8.Name = "label8";
            label8.Size = new Size(107, 15);
            label8.TabIndex = 15;
            label8.Text = "Fecha vencimiento";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(374, 72);
            label7.Name = "label7";
            label7.Size = new Size(90, 15);
            label7.TabIndex = 14;
            label7.Text = "Número hasta *";
            // 
            // txt_numero_hasta
            // 
            txt_numero_hasta.Location = new Point(374, 92);
            txt_numero_hasta.MaxLength = 50;
            txt_numero_hasta.Name = "txt_numero_hasta";
            txt_numero_hasta.Size = new Size(303, 23);
            txt_numero_hasta.TabIndex = 4;
            txt_numero_hasta.KeyPress += txt_numero_hasta_KeyPress;
            // 
            // cbx_estado
            // 
            cbx_estado.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_estado.FormattingEnabled = true;
            cbx_estado.Items.AddRange(new object[] { "Activo", "Inactivo" });
            cbx_estado.Location = new Point(41, 200);
            cbx_estado.Name = "cbx_estado";
            cbx_estado.Size = new Size(287, 23);
            cbx_estado.TabIndex = 7;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(41, 180);
            label5.Name = "label5";
            label5.Size = new Size(50, 15);
            label5.TabIndex = 9;
            label5.Text = "Estado *";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(41, 125);
            label2.Name = "label2";
            label2.Size = new Size(119, 15);
            label2.TabIndex = 3;
            label2.Text = "Número autorización";
            // 
            // txt_numero_autorizacion
            // 
            txt_numero_autorizacion.Location = new Point(41, 145);
            txt_numero_autorizacion.MaxLength = 50;
            txt_numero_autorizacion.Name = "txt_numero_autorizacion";
            txt_numero_autorizacion.Size = new Size(287, 23);
            txt_numero_autorizacion.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(41, 72);
            label1.Name = "label1";
            label1.Size = new Size(93, 15);
            label1.TabIndex = 1;
            label1.Text = "Número desde *";
            // 
            // txt_numero_desde
            // 
            txt_numero_desde.Location = new Point(41, 92);
            txt_numero_desde.MaxLength = 50;
            txt_numero_desde.Name = "txt_numero_desde";
            txt_numero_desde.Size = new Size(287, 23);
            txt_numero_desde.TabIndex = 3;
            txt_numero_desde.KeyPress += txt_numero_desde_KeyPress;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // AgregaRna
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(723, 316);
            Controls.Add(panel4);
            Controls.Add(panel3);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MaximumSize = new Size(739, 355);
            MinimumSize = new Size(739, 355);
            Name = "AgregaRna";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AgregaRna";
            Load += AgregaRna_Load;
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel3;
        private Button button1;
        private TextBox textBox3;
        private Button btn_guardar;
        private Panel panel4;
        private ComboBox cbx_tipo_documento;
        private Label label14;
        private Label label13;
        private TextBox txt_prefijo;
        private Label label9;
        private Label label8;
        private Label label7;
        private TextBox txt_numero_hasta;
        private ComboBox cbx_estado;
        private Label label5;
        private Label label2;
        private TextBox txt_numero_autorizacion;
        private Label label1;
        private TextBox txt_numero_desde;
        private DateTimePicker dtpk_fecha_vencimiento;
        private ComboBox cbx_numeracion_autorizada;
        private ErrorProvider errorProvider1;
    }
}