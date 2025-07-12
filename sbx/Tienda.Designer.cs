namespace sbx
{
    partial class Tienda
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Tienda));
            panel3 = new Panel();
            btn_guardar = new Button();
            panel4 = new Panel();
            cbx_tipo_documento = new ComboBox();
            label14 = new Label();
            btn_cargar = new Button();
            pbx_logo = new PictureBox();
            txt_digito_verificacion = new TextBox();
            label13 = new Label();
            txt_telefono = new TextBox();
            cbx_actividad_economica = new ComboBox();
            label12 = new Label();
            cbx_codigo_postal = new ComboBox();
            label11 = new Label();
            cbx_municipio = new ComboBox();
            label10 = new Label();
            cbx_departamento = new ComboBox();
            label9 = new Label();
            cbx_pais = new ComboBox();
            label8 = new Label();
            label7 = new Label();
            txt_direccion = new TextBox();
            label6 = new Label();
            txt_correo_distribucion = new TextBox();
            cbx_tipo_responsabilidad = new ComboBox();
            label5 = new Label();
            cbx_tipo_contribuyente = new ComboBox();
            label4 = new Label();
            cbx_responsabilidad_tributaria = new ComboBox();
            label3 = new Label();
            label2 = new Label();
            txt_nombre_razon_social = new TextBox();
            label1 = new Label();
            txt_numero_documento = new TextBox();
            errorProvider1 = new ErrorProvider(components);
            panel3.SuspendLayout();
            panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbx_logo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // panel3
            // 
            panel3.BackColor = Color.WhiteSmoke;
            panel3.BorderStyle = BorderStyle.Fixed3D;
            panel3.Controls.Add(btn_guardar);
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(0, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(724, 56);
            panel3.TabIndex = 2;
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
            panel4.Controls.Add(cbx_tipo_documento);
            panel4.Controls.Add(label14);
            panel4.Controls.Add(btn_cargar);
            panel4.Controls.Add(pbx_logo);
            panel4.Controls.Add(txt_digito_verificacion);
            panel4.Controls.Add(label13);
            panel4.Controls.Add(txt_telefono);
            panel4.Controls.Add(cbx_actividad_economica);
            panel4.Controls.Add(label12);
            panel4.Controls.Add(cbx_codigo_postal);
            panel4.Controls.Add(label11);
            panel4.Controls.Add(cbx_municipio);
            panel4.Controls.Add(label10);
            panel4.Controls.Add(cbx_departamento);
            panel4.Controls.Add(label9);
            panel4.Controls.Add(cbx_pais);
            panel4.Controls.Add(label8);
            panel4.Controls.Add(label7);
            panel4.Controls.Add(txt_direccion);
            panel4.Controls.Add(label6);
            panel4.Controls.Add(txt_correo_distribucion);
            panel4.Controls.Add(cbx_tipo_responsabilidad);
            panel4.Controls.Add(label5);
            panel4.Controls.Add(cbx_tipo_contribuyente);
            panel4.Controls.Add(label4);
            panel4.Controls.Add(cbx_responsabilidad_tributaria);
            panel4.Controls.Add(label3);
            panel4.Controls.Add(label2);
            panel4.Controls.Add(txt_nombre_razon_social);
            panel4.Controls.Add(label1);
            panel4.Controls.Add(txt_numero_documento);
            panel4.Dock = DockStyle.Fill;
            panel4.Location = new Point(0, 56);
            panel4.Name = "panel4";
            panel4.Size = new Size(724, 496);
            panel4.TabIndex = 3;
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
            label14.Size = new Size(119, 15);
            label14.TabIndex = 30;
            label14.Text = "Tipo de documento *";
            // 
            // btn_cargar
            // 
            btn_cargar.Enabled = false;
            btn_cargar.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_cargar.FlatStyle = FlatStyle.Flat;
            btn_cargar.Image = (Image)resources.GetObject("btn_cargar.Image");
            btn_cargar.Location = new Point(812, 89);
            btn_cargar.Name = "btn_cargar";
            btn_cargar.Size = new Size(30, 30);
            btn_cargar.TabIndex = 16;
            btn_cargar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_cargar.UseVisualStyleBackColor = true;
            btn_cargar.Visible = false;
            // 
            // pbx_logo
            // 
            pbx_logo.BorderStyle = BorderStyle.FixedSingle;
            pbx_logo.Location = new Point(726, 39);
            pbx_logo.Name = "pbx_logo";
            pbx_logo.Size = new Size(80, 80);
            pbx_logo.TabIndex = 28;
            pbx_logo.TabStop = false;
            pbx_logo.Visible = false;
            // 
            // txt_digito_verificacion
            // 
            txt_digito_verificacion.Enabled = false;
            txt_digito_verificacion.Location = new Point(280, 92);
            txt_digito_verificacion.Name = "txt_digito_verificacion";
            txt_digito_verificacion.Size = new Size(48, 23);
            txt_digito_verificacion.TabIndex = 3;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(374, 19);
            label13.Name = "label13";
            label13.Size = new Size(52, 15);
            label13.TabIndex = 26;
            label13.Text = "Teléfono";
            // 
            // txt_telefono
            // 
            txt_telefono.Location = new Point(374, 39);
            txt_telefono.MaxLength = 10;
            txt_telefono.Name = "txt_telefono";
            txt_telefono.Size = new Size(303, 23);
            txt_telefono.TabIndex = 9;
            txt_telefono.KeyPress += txt_telefono_KeyPress;
            // 
            // cbx_actividad_economica
            // 
            cbx_actividad_economica.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_actividad_economica.FormattingEnabled = true;
            cbx_actividad_economica.Location = new Point(374, 377);
            cbx_actividad_economica.Name = "cbx_actividad_economica";
            cbx_actividad_economica.Size = new Size(303, 23);
            cbx_actividad_economica.TabIndex = 15;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(374, 357);
            label12.Name = "label12";
            label12.Size = new Size(119, 15);
            label12.TabIndex = 23;
            label12.Text = "Actividad económica";
            // 
            // cbx_codigo_postal
            // 
            cbx_codigo_postal.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_codigo_postal.FormattingEnabled = true;
            cbx_codigo_postal.Location = new Point(374, 318);
            cbx_codigo_postal.Name = "cbx_codigo_postal";
            cbx_codigo_postal.Size = new Size(303, 23);
            cbx_codigo_postal.TabIndex = 14;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(374, 298);
            label11.Name = "label11";
            label11.Size = new Size(81, 15);
            label11.TabIndex = 21;
            label11.Text = "Código postal";
            // 
            // cbx_municipio
            // 
            cbx_municipio.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_municipio.FormattingEnabled = true;
            cbx_municipio.Location = new Point(374, 259);
            cbx_municipio.Name = "cbx_municipio";
            cbx_municipio.Size = new Size(303, 23);
            cbx_municipio.TabIndex = 13;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(374, 239);
            label10.Name = "label10";
            label10.Size = new Size(112, 15);
            label10.TabIndex = 19;
            label10.Text = "Municipio/Ciudad *";
            // 
            // cbx_departamento
            // 
            cbx_departamento.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_departamento.FormattingEnabled = true;
            cbx_departamento.Location = new Point(374, 200);
            cbx_departamento.Name = "cbx_departamento";
            cbx_departamento.Size = new Size(303, 23);
            cbx_departamento.TabIndex = 12;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(374, 180);
            label9.Name = "label9";
            label9.Size = new Size(83, 15);
            label9.TabIndex = 17;
            label9.Text = "Departamento";
            // 
            // cbx_pais
            // 
            cbx_pais.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_pais.FormattingEnabled = true;
            cbx_pais.Location = new Point(374, 143);
            cbx_pais.Name = "cbx_pais";
            cbx_pais.Size = new Size(303, 23);
            cbx_pais.TabIndex = 11;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(374, 123);
            label8.Name = "label8";
            label8.Size = new Size(28, 15);
            label8.TabIndex = 15;
            label8.Text = "País";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(374, 72);
            label7.Name = "label7";
            label7.Size = new Size(65, 15);
            label7.TabIndex = 14;
            label7.Text = "Dirección *";
            // 
            // txt_direccion
            // 
            txt_direccion.Location = new Point(374, 92);
            txt_direccion.MaxLength = 200;
            txt_direccion.Name = "txt_direccion";
            txt_direccion.Size = new Size(303, 23);
            txt_direccion.TabIndex = 10;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(41, 357);
            label6.Name = "label6";
            label6.Size = new Size(143, 15);
            label6.TabIndex = 12;
            label6.Text = "Correo para distribución *";
            // 
            // txt_correo_distribucion
            // 
            txt_correo_distribucion.Location = new Point(41, 377);
            txt_correo_distribucion.MaxLength = 100;
            txt_correo_distribucion.Name = "txt_correo_distribucion";
            txt_correo_distribucion.Size = new Size(287, 23);
            txt_correo_distribucion.TabIndex = 8;
            // 
            // cbx_tipo_responsabilidad
            // 
            cbx_tipo_responsabilidad.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_tipo_responsabilidad.FormattingEnabled = true;
            cbx_tipo_responsabilidad.Location = new Point(41, 200);
            cbx_tipo_responsabilidad.Name = "cbx_tipo_responsabilidad";
            cbx_tipo_responsabilidad.Size = new Size(287, 23);
            cbx_tipo_responsabilidad.TabIndex = 5;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(41, 180);
            label5.Name = "label5";
            label5.Size = new Size(140, 15);
            label5.TabIndex = 9;
            label5.Text = "Tipo de responsabilidad *";
            // 
            // cbx_tipo_contribuyente
            // 
            cbx_tipo_contribuyente.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_tipo_contribuyente.FormattingEnabled = true;
            cbx_tipo_contribuyente.Location = new Point(41, 318);
            cbx_tipo_contribuyente.Name = "cbx_tipo_contribuyente";
            cbx_tipo_contribuyente.Size = new Size(287, 23);
            cbx_tipo_contribuyente.TabIndex = 7;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(41, 298);
            label4.Name = "label4";
            label4.Size = new Size(123, 15);
            label4.TabIndex = 7;
            label4.Text = "Tipo de contribuyente";
            // 
            // cbx_responsabilidad_tributaria
            // 
            cbx_responsabilidad_tributaria.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_responsabilidad_tributaria.FormattingEnabled = true;
            cbx_responsabilidad_tributaria.Location = new Point(41, 259);
            cbx_responsabilidad_tributaria.Name = "cbx_responsabilidad_tributaria";
            cbx_responsabilidad_tributaria.Size = new Size(287, 23);
            cbx_responsabilidad_tributaria.TabIndex = 6;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(41, 239);
            label3.Name = "label3";
            label3.Size = new Size(152, 15);
            label3.TabIndex = 5;
            label3.Text = "Responsabilidad tributaria *";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(41, 125);
            label2.Name = "label2";
            label2.Size = new Size(137, 15);
            label2.TabIndex = 3;
            label2.Text = "Nombre o Razón social *";
            // 
            // txt_nombre_razon_social
            // 
            txt_nombre_razon_social.Location = new Point(41, 145);
            txt_nombre_razon_social.MaxLength = 100;
            txt_nombre_razon_social.Name = "txt_nombre_razon_social";
            txt_nombre_razon_social.Size = new Size(287, 23);
            txt_nombre_razon_social.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(41, 72);
            label1.Name = "label1";
            label1.Size = new Size(132, 15);
            label1.TabIndex = 1;
            label1.Text = "Número de documento";
            // 
            // txt_numero_documento
            // 
            txt_numero_documento.Location = new Point(41, 92);
            txt_numero_documento.MaxLength = 11;
            txt_numero_documento.Name = "txt_numero_documento";
            txt_numero_documento.Size = new Size(217, 23);
            txt_numero_documento.TabIndex = 2;
            txt_numero_documento.KeyPress += txt_numero_documento_KeyPress;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // Tienda
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(724, 552);
            Controls.Add(panel4);
            Controls.Add(panel3);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MaximumSize = new Size(740, 591);
            MinimumSize = new Size(740, 591);
            Name = "Tienda";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Tienda";
            Load += Tienda_Load;
            panel3.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pbx_logo).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel3;
        private Button btn_guardar;
        private Panel panel4;
        private Button btn_cargar;
        private PictureBox pbx_logo;
        private TextBox txt_digito_verificacion;
        private Label label13;
        private TextBox txt_telefono;
        private ComboBox cbx_actividad_economica;
        private Label label12;
        private ComboBox cbx_codigo_postal;
        private Label label11;
        private ComboBox cbx_municipio;
        private Label label10;
        private ComboBox cbx_departamento;
        private Label label9;
        private ComboBox cbx_pais;
        private Label label8;
        private Label label7;
        private TextBox txt_direccion;
        private Label label6;
        private TextBox txt_correo_distribucion;
        private ComboBox cbx_tipo_responsabilidad;
        private Label label5;
        private ComboBox cbx_tipo_contribuyente;
        private Label label4;
        private ComboBox cbx_responsabilidad_tributaria;
        private Label label3;
        private Label label2;
        private TextBox txt_nombre_razon_social;
        private Label label1;
        private TextBox txt_numero_documento;
        private ComboBox cbx_tipo_documento;
        private Label label14;
        private ErrorProvider errorProvider1;
    }
}