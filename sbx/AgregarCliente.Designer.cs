namespace sbx
{
    partial class AgregarCliente
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AgregarCliente));
            panel3 = new Panel();
            btn_guardar = new Button();
            label10 = new Label();
            txt_direccion = new TextBox();
            cbx_tipo_identificacion = new ComboBox();
            label6 = new Label();
            cbx_estado = new ComboBox();
            label14 = new Label();
            label3 = new Label();
            txt_email = new TextBox();
            label2 = new Label();
            txt_telefono = new TextBox();
            label1 = new Label();
            txt_numero_documento = new TextBox();
            label7 = new Label();
            txt_nombre_razon_social = new TextBox();
            cbx_tipo_cliente = new ComboBox();
            label4 = new Label();
            errorProvider1 = new ErrorProvider(components);
            cbx_tipo_responsabilidad = new ComboBox();
            label5 = new Label();
            cbx_tipo_contribuyente = new ComboBox();
            label8 = new Label();
            cbx_responsabilidad_tributaria = new ComboBox();
            label9 = new Label();
            cbx_municipio = new ComboBox();
            label11 = new Label();
            cbx_departamento = new ComboBox();
            label12 = new Label();
            cbx_pais = new ComboBox();
            label13 = new Label();
            panel3.SuspendLayout();
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
            panel3.Margin = new Padding(3, 4, 3, 4);
            panel3.Name = "panel3";
            panel3.Size = new Size(799, 73);
            panel3.TabIndex = 63;
            // 
            // btn_guardar
            // 
            btn_guardar.Enabled = false;
            btn_guardar.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_guardar.FlatStyle = FlatStyle.Flat;
            btn_guardar.Image = (Image)resources.GetObject("btn_guardar.Image");
            btn_guardar.Location = new Point(3, 4);
            btn_guardar.Margin = new Padding(3, 4, 3, 4);
            btn_guardar.Name = "btn_guardar";
            btn_guardar.Size = new Size(110, 60);
            btn_guardar.TabIndex = 0;
            btn_guardar.Text = "Guardar";
            btn_guardar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_guardar.UseVisualStyleBackColor = true;
            btn_guardar.Click += btn_guardar_Click;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(14, 387);
            label10.Name = "label10";
            label10.Size = new Size(72, 20);
            label10.TabIndex = 99;
            label10.Text = "Direccion";
            // 
            // txt_direccion
            // 
            txt_direccion.Location = new Point(14, 413);
            txt_direccion.Margin = new Padding(3, 4, 3, 4);
            txt_direccion.MaxLength = 150;
            txt_direccion.Name = "txt_direccion";
            txt_direccion.Size = new Size(346, 27);
            txt_direccion.TabIndex = 5;
            // 
            // cbx_tipo_identificacion
            // 
            cbx_tipo_identificacion.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_tipo_identificacion.FormattingEnabled = true;
            cbx_tipo_identificacion.Location = new Point(14, 116);
            cbx_tipo_identificacion.Margin = new Padding(3, 4, 3, 4);
            cbx_tipo_identificacion.Name = "cbx_tipo_identificacion";
            cbx_tipo_identificacion.Size = new Size(346, 28);
            cbx_tipo_identificacion.TabIndex = 1;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(14, 92);
            label6.Name = "label6";
            label6.Size = new Size(133, 20);
            label6.TabIndex = 98;
            label6.Text = "Tipo identificacion";
            // 
            // cbx_estado
            // 
            cbx_estado.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_estado.FormattingEnabled = true;
            cbx_estado.Items.AddRange(new object[] { "Activo", "Inactivo" });
            cbx_estado.Location = new Point(408, 486);
            cbx_estado.Margin = new Padding(3, 4, 3, 4);
            cbx_estado.Name = "cbx_estado";
            cbx_estado.Size = new Size(346, 28);
            cbx_estado.TabIndex = 14;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(408, 462);
            label14.Name = "label14";
            label14.Size = new Size(54, 20);
            label14.TabIndex = 97;
            label14.Text = "Estado";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(408, 165);
            label3.Name = "label3";
            label3.Size = new Size(46, 20);
            label3.TabIndex = 96;
            label3.Text = "Email";
            // 
            // txt_email
            // 
            txt_email.Location = new Point(408, 191);
            txt_email.Margin = new Padding(3, 4, 3, 4);
            txt_email.MaxLength = 100;
            txt_email.Name = "txt_email";
            txt_email.Size = new Size(346, 27);
            txt_email.TabIndex = 10;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(408, 93);
            label2.Name = "label2";
            label2.Size = new Size(67, 20);
            label2.TabIndex = 95;
            label2.Text = "Telefono";
            // 
            // txt_telefono
            // 
            txt_telefono.Location = new Point(408, 117);
            txt_telefono.Margin = new Padding(3, 4, 3, 4);
            txt_telefono.MaxLength = 20;
            txt_telefono.Name = "txt_telefono";
            txt_telefono.Size = new Size(346, 27);
            txt_telefono.TabIndex = 9;
            txt_telefono.KeyPress += txt_telefono_KeyPress;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(14, 164);
            label1.Name = "label1";
            label1.Size = new Size(143, 20);
            label1.TabIndex = 94;
            label1.Text = "Numero documento";
            // 
            // txt_numero_documento
            // 
            txt_numero_documento.Location = new Point(14, 191);
            txt_numero_documento.Margin = new Padding(3, 4, 3, 4);
            txt_numero_documento.MaxLength = 200;
            txt_numero_documento.Name = "txt_numero_documento";
            txt_numero_documento.Size = new Size(346, 27);
            txt_numero_documento.TabIndex = 2;
            txt_numero_documento.KeyPress += txt_numero_documento_KeyPress;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(14, 241);
            label7.Name = "label7";
            label7.Size = new Size(161, 20);
            label7.TabIndex = 93;
            label7.Text = "Nombre / Razon social";
            // 
            // txt_nombre_razon_social
            // 
            txt_nombre_razon_social.Location = new Point(14, 265);
            txt_nombre_razon_social.Margin = new Padding(3, 4, 3, 4);
            txt_nombre_razon_social.MaxLength = 100;
            txt_nombre_razon_social.Name = "txt_nombre_razon_social";
            txt_nombre_razon_social.Size = new Size(346, 27);
            txt_nombre_razon_social.TabIndex = 3;
            // 
            // cbx_tipo_cliente
            // 
            cbx_tipo_cliente.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_tipo_cliente.FormattingEnabled = true;
            cbx_tipo_cliente.Location = new Point(14, 337);
            cbx_tipo_cliente.Margin = new Padding(3, 4, 3, 4);
            cbx_tipo_cliente.Name = "cbx_tipo_cliente";
            cbx_tipo_cliente.Size = new Size(346, 28);
            cbx_tipo_cliente.TabIndex = 4;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(14, 313);
            label4.Name = "label4";
            label4.Size = new Size(87, 20);
            label4.TabIndex = 101;
            label4.Text = "Tipo cliente";
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // cbx_tipo_responsabilidad
            // 
            cbx_tipo_responsabilidad.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_tipo_responsabilidad.FormattingEnabled = true;
            cbx_tipo_responsabilidad.Location = new Point(408, 266);
            cbx_tipo_responsabilidad.Margin = new Padding(3, 4, 3, 4);
            cbx_tipo_responsabilidad.Name = "cbx_tipo_responsabilidad";
            cbx_tipo_responsabilidad.Size = new Size(346, 28);
            cbx_tipo_responsabilidad.TabIndex = 11;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(408, 239);
            label5.Name = "label5";
            label5.Size = new Size(180, 20);
            label5.TabIndex = 107;
            label5.Text = "Tipo de responsabilidad *";
            // 
            // cbx_tipo_contribuyente
            // 
            cbx_tipo_contribuyente.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_tipo_contribuyente.FormattingEnabled = true;
            cbx_tipo_contribuyente.Location = new Point(408, 423);
            cbx_tipo_contribuyente.Margin = new Padding(3, 4, 3, 4);
            cbx_tipo_contribuyente.Name = "cbx_tipo_contribuyente";
            cbx_tipo_contribuyente.Size = new Size(346, 28);
            cbx_tipo_contribuyente.TabIndex = 13;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(408, 396);
            label8.Name = "label8";
            label8.Size = new Size(155, 20);
            label8.TabIndex = 106;
            label8.Text = "Tipo de contribuyente";
            // 
            // cbx_responsabilidad_tributaria
            // 
            cbx_responsabilidad_tributaria.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_responsabilidad_tributaria.FormattingEnabled = true;
            cbx_responsabilidad_tributaria.Location = new Point(408, 344);
            cbx_responsabilidad_tributaria.Margin = new Padding(3, 4, 3, 4);
            cbx_responsabilidad_tributaria.Name = "cbx_responsabilidad_tributaria";
            cbx_responsabilidad_tributaria.Size = new Size(346, 28);
            cbx_responsabilidad_tributaria.TabIndex = 12;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(408, 318);
            label9.Name = "label9";
            label9.Size = new Size(194, 20);
            label9.TabIndex = 103;
            label9.Text = "Responsabilidad tributaria *";
            // 
            // cbx_municipio
            // 
            cbx_municipio.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_municipio.FormattingEnabled = true;
            cbx_municipio.Location = new Point(14, 637);
            cbx_municipio.Margin = new Padding(3, 4, 3, 4);
            cbx_municipio.Name = "cbx_municipio";
            cbx_municipio.Size = new Size(346, 28);
            cbx_municipio.TabIndex = 8;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(14, 611);
            label11.Name = "label11";
            label11.Size = new Size(138, 20);
            label11.TabIndex = 113;
            label11.Text = "Municipio/Ciudad *";
            // 
            // cbx_departamento
            // 
            cbx_departamento.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_departamento.FormattingEnabled = true;
            cbx_departamento.Location = new Point(14, 562);
            cbx_departamento.Margin = new Padding(3, 4, 3, 4);
            cbx_departamento.Name = "cbx_departamento";
            cbx_departamento.Size = new Size(346, 28);
            cbx_departamento.TabIndex = 7;
            cbx_departamento.SelectedIndexChanged += cbx_departamento_SelectedIndexChanged;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(14, 535);
            label12.Name = "label12";
            label12.Size = new Size(106, 20);
            label12.TabIndex = 112;
            label12.Text = "Departamento";
            // 
            // cbx_pais
            // 
            cbx_pais.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_pais.FormattingEnabled = true;
            cbx_pais.Location = new Point(14, 486);
            cbx_pais.Margin = new Padding(3, 4, 3, 4);
            cbx_pais.Name = "cbx_pais";
            cbx_pais.Size = new Size(346, 28);
            cbx_pais.TabIndex = 6;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(14, 459);
            label13.Name = "label13";
            label13.Size = new Size(34, 20);
            label13.TabIndex = 111;
            label13.Text = "País";
            // 
            // AgregarCliente
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            ClientSize = new Size(799, 679);
            Controls.Add(cbx_municipio);
            Controls.Add(label11);
            Controls.Add(cbx_departamento);
            Controls.Add(label12);
            Controls.Add(cbx_pais);
            Controls.Add(label13);
            Controls.Add(cbx_tipo_responsabilidad);
            Controls.Add(label5);
            Controls.Add(cbx_tipo_contribuyente);
            Controls.Add(label8);
            Controls.Add(cbx_responsabilidad_tributaria);
            Controls.Add(label9);
            Controls.Add(cbx_tipo_cliente);
            Controls.Add(label4);
            Controls.Add(label10);
            Controls.Add(txt_direccion);
            Controls.Add(cbx_tipo_identificacion);
            Controls.Add(label6);
            Controls.Add(cbx_estado);
            Controls.Add(label14);
            Controls.Add(label3);
            Controls.Add(txt_email);
            Controls.Add(label2);
            Controls.Add(txt_telefono);
            Controls.Add(label1);
            Controls.Add(txt_numero_documento);
            Controls.Add(label7);
            Controls.Add(txt_nombre_razon_social);
            Controls.Add(panel3);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            Name = "AgregarCliente";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AgregarCliente";
            Load += AgregarCliente_Load;
            panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel3;
        private Button btn_guardar;
        private Label label10;
        private TextBox txt_direccion;
        private ComboBox cbx_tipo_identificacion;
        private Label label6;
        private ComboBox cbx_estado;
        private Label label14;
        private Label label3;
        private TextBox txt_email;
        private Label label2;
        private TextBox txt_telefono;
        private Label label1;
        private TextBox txt_numero_documento;
        private Label label7;
        private TextBox txt_nombre_razon_social;
        private ComboBox cbx_tipo_cliente;
        private Label label4;
        private ErrorProvider errorProvider1;
        private ComboBox cbx_tipo_responsabilidad;
        private Label label5;
        private ComboBox cbx_tipo_contribuyente;
        private Label label8;
        private ComboBox cbx_responsabilidad_tributaria;
        private Label label9;
        private ComboBox cbx_municipio;
        private Label label11;
        private ComboBox cbx_departamento;
        private Label label12;
        private ComboBox cbx_pais;
        private Label label13;
    }
}