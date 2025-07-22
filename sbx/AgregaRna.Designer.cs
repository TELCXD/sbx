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
            cbx_expired = new ComboBox();
            label11 = new Label();
            label10 = new Label();
            txt_clave_Tecnica = new TextBox();
            dtpFechaExpedicion = new DateTimePicker();
            label6 = new Label();
            label4 = new Label();
            textBox2 = new TextBox();
            label3 = new Label();
            txt_id_dian = new TextBox();
            cbx_en_uso = new ComboBox();
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
            panel3.Size = new Size(660, 56);
            panel3.TabIndex = 3;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatAppearance.MouseDownBackColor = Color.Gray;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Image = (Image)resources.GetObject("button1.Image");
            button1.Location = new Point(2420, 3);
            button1.Name = "button1";
            button1.Size = new Size(42, 45);
            button1.TabIndex = 5;
            button1.TextImageRelation = TextImageRelation.ImageBeforeText;
            button1.UseVisualStyleBackColor = true;
            // 
            // textBox3
            // 
            textBox3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            textBox3.Location = new Point(2223, 14);
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
            panel4.Controls.Add(cbx_expired);
            panel4.Controls.Add(label11);
            panel4.Controls.Add(label10);
            panel4.Controls.Add(txt_clave_Tecnica);
            panel4.Controls.Add(dtpFechaExpedicion);
            panel4.Controls.Add(label6);
            panel4.Controls.Add(label4);
            panel4.Controls.Add(textBox2);
            panel4.Controls.Add(label3);
            panel4.Controls.Add(txt_id_dian);
            panel4.Controls.Add(cbx_en_uso);
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
            panel4.Size = new Size(660, 393);
            panel4.TabIndex = 4;
            // 
            // cbx_expired
            // 
            cbx_expired.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_expired.FormattingEnabled = true;
            cbx_expired.Items.AddRange(new object[] { "SI", "NO" });
            cbx_expired.Location = new Point(12, 301);
            cbx_expired.Name = "cbx_expired";
            cbx_expired.Size = new Size(287, 23);
            cbx_expired.TabIndex = 11;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(12, 281);
            label11.Name = "label11";
            label11.Size = new Size(49, 15);
            label11.TabIndex = 40;
            label11.Text = "Vencido";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(345, 166);
            label10.Name = "label10";
            label10.Size = new Size(77, 15);
            label10.TabIndex = 37;
            label10.Text = "Clave técnica";
            // 
            // txt_clave_Tecnica
            // 
            txt_clave_Tecnica.Location = new Point(345, 185);
            txt_clave_Tecnica.MaxLength = 100;
            txt_clave_Tecnica.Name = "txt_clave_Tecnica";
            txt_clave_Tecnica.Size = new Size(287, 23);
            txt_clave_Tecnica.TabIndex = 8;
            // 
            // dtpFechaExpedicion
            // 
            dtpFechaExpedicion.Format = DateTimePickerFormat.Short;
            dtpFechaExpedicion.Location = new Point(12, 241);
            dtpFechaExpedicion.MinDate = new DateTime(2025, 4, 17, 0, 0, 0, 0);
            dtpFechaExpedicion.Name = "dtpFechaExpedicion";
            dtpFechaExpedicion.Size = new Size(287, 23);
            dtpFechaExpedicion.TabIndex = 9;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 219);
            label6.Name = "label6";
            label6.Size = new Size(99, 15);
            label6.TabIndex = 36;
            label6.Text = "Fecha expedición";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(345, 111);
            label4.Name = "label4";
            label4.Size = new Size(135, 15);
            label4.TabIndex = 34;
            label4.Text = "Siguiente número rango";
            // 
            // textBox2
            // 
            textBox2.Enabled = false;
            textBox2.Location = new Point(345, 131);
            textBox2.MaxLength = 10;
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(287, 23);
            textBox2.TabIndex = 6;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 9);
            label3.Name = "label3";
            label3.Size = new Size(123, 15);
            label3.TabIndex = 31;
            label3.Text = "Id rango numeración*";
            // 
            // txt_id_dian
            // 
            txt_id_dian.Location = new Point(12, 29);
            txt_id_dian.MaxLength = 2;
            txt_id_dian.Name = "txt_id_dian";
            txt_id_dian.Size = new Size(287, 23);
            txt_id_dian.TabIndex = 1;
            txt_id_dian.KeyPress += txt_id_dian_KeyPress;
            // 
            // cbx_en_uso
            // 
            cbx_en_uso.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_en_uso.FormattingEnabled = true;
            cbx_en_uso.Items.AddRange(new object[] { "Si", "No" });
            cbx_en_uso.Location = new Point(12, 358);
            cbx_en_uso.Name = "cbx_en_uso";
            cbx_en_uso.Size = new Size(287, 23);
            cbx_en_uso.TabIndex = 13;
            // 
            // dtpk_fecha_vencimiento
            // 
            dtpk_fecha_vencimiento.Format = DateTimePickerFormat.Short;
            dtpk_fecha_vencimiento.Location = new Point(345, 241);
            dtpk_fecha_vencimiento.MinDate = new DateTime(2025, 4, 17, 0, 0, 0, 0);
            dtpk_fecha_vencimiento.Name = "dtpk_fecha_vencimiento";
            dtpk_fecha_vencimiento.Size = new Size(287, 23);
            dtpk_fecha_vencimiento.TabIndex = 10;
            // 
            // cbx_tipo_documento
            // 
            cbx_tipo_documento.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_tipo_documento.FormattingEnabled = true;
            cbx_tipo_documento.Location = new Point(345, 29);
            cbx_tipo_documento.Name = "cbx_tipo_documento";
            cbx_tipo_documento.Size = new Size(287, 23);
            cbx_tipo_documento.TabIndex = 2;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(345, 9);
            label14.Name = "label14";
            label14.Size = new Size(95, 15);
            label14.TabIndex = 30;
            label14.Text = "Tipo documento";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(12, 59);
            label13.Name = "label13";
            label13.Size = new Size(49, 15);
            label13.TabIndex = 26;
            label13.Text = "Prefijo *";
            // 
            // txt_prefijo
            // 
            txt_prefijo.Location = new Point(12, 79);
            txt_prefijo.MaxLength = 10;
            txt_prefijo.Name = "txt_prefijo";
            txt_prefijo.Size = new Size(287, 23);
            txt_prefijo.TabIndex = 3;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(12, 338);
            label9.Name = "label9";
            label9.Size = new Size(42, 15);
            label9.TabIndex = 17;
            label9.Text = "En uso";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(345, 219);
            label8.Name = "label8";
            label8.Size = new Size(107, 15);
            label8.TabIndex = 15;
            label8.Text = "Fecha vencimiento";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(12, 111);
            label7.Name = "label7";
            label7.Size = new Size(90, 15);
            label7.TabIndex = 14;
            label7.Text = "Número hasta *";
            // 
            // txt_numero_hasta
            // 
            txt_numero_hasta.Location = new Point(12, 131);
            txt_numero_hasta.MaxLength = 10;
            txt_numero_hasta.Name = "txt_numero_hasta";
            txt_numero_hasta.Size = new Size(287, 23);
            txt_numero_hasta.TabIndex = 5;
            txt_numero_hasta.KeyPress += txt_numero_hasta_KeyPress;
            // 
            // cbx_estado
            // 
            cbx_estado.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_estado.FormattingEnabled = true;
            cbx_estado.Items.AddRange(new object[] { "Activo", "Inactivo" });
            cbx_estado.Location = new Point(345, 301);
            cbx_estado.Name = "cbx_estado";
            cbx_estado.Size = new Size(287, 23);
            cbx_estado.TabIndex = 12;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(345, 281);
            label5.Name = "label5";
            label5.Size = new Size(42, 15);
            label5.TabIndex = 9;
            label5.Text = "Estado";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 166);
            label2.Name = "label2";
            label2.Size = new Size(125, 15);
            label2.TabIndex = 3;
            label2.Text = "Número de resolución";
            // 
            // txt_numero_autorizacion
            // 
            txt_numero_autorizacion.Location = new Point(12, 185);
            txt_numero_autorizacion.MaxLength = 50;
            txt_numero_autorizacion.Name = "txt_numero_autorizacion";
            txt_numero_autorizacion.Size = new Size(287, 23);
            txt_numero_autorizacion.TabIndex = 7;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(345, 59);
            label1.Name = "label1";
            label1.Size = new Size(93, 15);
            label1.TabIndex = 1;
            label1.Text = "Número desde *";
            // 
            // txt_numero_desde
            // 
            txt_numero_desde.Location = new Point(345, 79);
            txt_numero_desde.MaxLength = 10;
            txt_numero_desde.Name = "txt_numero_desde";
            txt_numero_desde.Size = new Size(287, 23);
            txt_numero_desde.TabIndex = 4;
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
            ClientSize = new Size(660, 449);
            Controls.Add(panel4);
            Controls.Add(panel3);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
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
        private ComboBox cbx_en_uso;
        private ErrorProvider errorProvider1;
        private Label label3;
        private TextBox txt_id_dian;
        private Label label4;
        private TextBox textBox2;
        private DateTimePicker dtpFechaExpedicion;
        private Label label6;
        private Label label10;
        private TextBox txt_clave_Tecnica;
        private ComboBox cbx_expired;
        private Label label11;
    }
}