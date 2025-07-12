namespace sbx
{
    partial class AddUsuario
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddUsuario));
            panel1 = new Panel();
            btn_guardar = new Button();
            cbx_tipo_identificacion = new ComboBox();
            label6 = new Label();
            label1 = new Label();
            txt_numero_documento = new TextBox();
            label7 = new Label();
            txt_nombre = new TextBox();
            label2 = new Label();
            txt_apellido = new TextBox();
            label3 = new Label();
            dtp_fecha_cumpleaños = new DateTimePicker();
            cbx_estado = new ComboBox();
            label14 = new Label();
            label4 = new Label();
            txt_email = new TextBox();
            label5 = new Label();
            txt_telefono = new TextBox();
            label8 = new Label();
            txt_usuario = new TextBox();
            label9 = new Label();
            txt_contrasena = new TextBox();
            cbx_rol = new ComboBox();
            label10 = new Label();
            errorProvider1 = new ErrorProvider(components);
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.WhiteSmoke;
            panel1.BorderStyle = BorderStyle.Fixed3D;
            panel1.Controls.Add(btn_guardar);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(681, 56);
            panel1.TabIndex = 0;
            // 
            // btn_guardar
            // 
            btn_guardar.Enabled = false;
            btn_guardar.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_guardar.FlatStyle = FlatStyle.Flat;
            btn_guardar.Image = (Image)resources.GetObject("btn_guardar.Image");
            btn_guardar.Location = new Point(3, 4);
            btn_guardar.Name = "btn_guardar";
            btn_guardar.Size = new Size(96, 45);
            btn_guardar.TabIndex = 1;
            btn_guardar.Text = "Guardar";
            btn_guardar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_guardar.UseVisualStyleBackColor = true;
            btn_guardar.Click += btn_guardar_Click;
            // 
            // cbx_tipo_identificacion
            // 
            cbx_tipo_identificacion.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_tipo_identificacion.FormattingEnabled = true;
            cbx_tipo_identificacion.Location = new Point(12, 79);
            cbx_tipo_identificacion.Name = "cbx_tipo_identificacion";
            cbx_tipo_identificacion.Size = new Size(303, 23);
            cbx_tipo_identificacion.TabIndex = 99;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 61);
            label6.Name = "label6";
            label6.Size = new Size(105, 15);
            label6.TabIndex = 104;
            label6.Text = "Tipo identificacion";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 115);
            label1.Name = "label1";
            label1.Size = new Size(116, 15);
            label1.TabIndex = 103;
            label1.Text = "Numero documento";
            // 
            // txt_numero_documento
            // 
            txt_numero_documento.Location = new Point(12, 135);
            txt_numero_documento.MaxLength = 200;
            txt_numero_documento.Name = "txt_numero_documento";
            txt_numero_documento.Size = new Size(303, 23);
            txt_numero_documento.TabIndex = 100;
            txt_numero_documento.KeyPress += txt_numero_documento_KeyPress;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(12, 173);
            label7.Name = "label7";
            label7.Size = new Size(51, 15);
            label7.TabIndex = 102;
            label7.Text = "Nombre";
            // 
            // txt_nombre
            // 
            txt_nombre.Location = new Point(12, 191);
            txt_nombre.MaxLength = 100;
            txt_nombre.Name = "txt_nombre";
            txt_nombre.Size = new Size(303, 23);
            txt_nombre.TabIndex = 101;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 223);
            label2.Name = "label2";
            label2.Size = new Size(51, 15);
            label2.TabIndex = 106;
            label2.Text = "Apellido";
            // 
            // txt_apellido
            // 
            txt_apellido.Location = new Point(12, 241);
            txt_apellido.MaxLength = 100;
            txt_apellido.Name = "txt_apellido";
            txt_apellido.Size = new Size(303, 23);
            txt_apellido.TabIndex = 105;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 276);
            label3.Name = "label3";
            label3.Size = new Size(106, 15);
            label3.TabIndex = 107;
            label3.Text = "Fecha cumpleaños";
            // 
            // dtp_fecha_cumpleaños
            // 
            dtp_fecha_cumpleaños.Format = DateTimePickerFormat.Short;
            dtp_fecha_cumpleaños.Location = new Point(12, 294);
            dtp_fecha_cumpleaños.Name = "dtp_fecha_cumpleaños";
            dtp_fecha_cumpleaños.Size = new Size(303, 23);
            dtp_fecha_cumpleaños.TabIndex = 108;
            // 
            // cbx_estado
            // 
            cbx_estado.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_estado.FormattingEnabled = true;
            cbx_estado.Items.AddRange(new object[] { "Activo", "Inactivo" });
            cbx_estado.Location = new Point(346, 294);
            cbx_estado.Name = "cbx_estado";
            cbx_estado.Size = new Size(303, 23);
            cbx_estado.TabIndex = 109;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(346, 276);
            label14.Name = "label14";
            label14.Size = new Size(42, 15);
            label14.TabIndex = 114;
            label14.Text = "Estado";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(346, 59);
            label4.Name = "label4";
            label4.Size = new Size(36, 15);
            label4.TabIndex = 113;
            label4.Text = "Email";
            // 
            // txt_email
            // 
            txt_email.Location = new Point(346, 79);
            txt_email.MaxLength = 100;
            txt_email.Name = "txt_email";
            txt_email.Size = new Size(303, 23);
            txt_email.TabIndex = 111;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 326);
            label5.Name = "label5";
            label5.Size = new Size(52, 15);
            label5.TabIndex = 112;
            label5.Text = "Telefono";
            // 
            // txt_telefono
            // 
            txt_telefono.Location = new Point(12, 344);
            txt_telefono.MaxLength = 20;
            txt_telefono.Name = "txt_telefono";
            txt_telefono.Size = new Size(303, 23);
            txt_telefono.TabIndex = 110;
            txt_telefono.KeyPress += txt_telefono_KeyPress;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(346, 117);
            label8.Name = "label8";
            label8.Size = new Size(47, 15);
            label8.TabIndex = 116;
            label8.Text = "Usuario";
            // 
            // txt_usuario
            // 
            txt_usuario.Location = new Point(346, 135);
            txt_usuario.MaxLength = 100;
            txt_usuario.Name = "txt_usuario";
            txt_usuario.Size = new Size(303, 23);
            txt_usuario.TabIndex = 115;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(346, 173);
            label9.Name = "label9";
            label9.Size = new Size(67, 15);
            label9.TabIndex = 118;
            label9.Text = "Contraseña";
            // 
            // txt_contrasena
            // 
            txt_contrasena.Location = new Point(346, 191);
            txt_contrasena.MaxLength = 100;
            txt_contrasena.Name = "txt_contrasena";
            txt_contrasena.Size = new Size(303, 23);
            txt_contrasena.TabIndex = 117;
            txt_contrasena.UseSystemPasswordChar = true;
            // 
            // cbx_rol
            // 
            cbx_rol.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_rol.FormattingEnabled = true;
            cbx_rol.Location = new Point(346, 241);
            cbx_rol.Name = "cbx_rol";
            cbx_rol.Size = new Size(303, 23);
            cbx_rol.TabIndex = 119;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(346, 223);
            label10.Name = "label10";
            label10.Size = new Size(24, 15);
            label10.TabIndex = 120;
            label10.Text = "Rol";
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // AddUsuario
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            ClientSize = new Size(681, 379);
            Controls.Add(cbx_rol);
            Controls.Add(label10);
            Controls.Add(label9);
            Controls.Add(txt_contrasena);
            Controls.Add(label8);
            Controls.Add(txt_usuario);
            Controls.Add(cbx_estado);
            Controls.Add(label14);
            Controls.Add(label4);
            Controls.Add(txt_email);
            Controls.Add(label5);
            Controls.Add(txt_telefono);
            Controls.Add(dtp_fecha_cumpleaños);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(txt_apellido);
            Controls.Add(cbx_tipo_identificacion);
            Controls.Add(label6);
            Controls.Add(label1);
            Controls.Add(txt_numero_documento);
            Controls.Add(label7);
            Controls.Add(txt_nombre);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MaximumSize = new Size(697, 418);
            MinimumSize = new Size(697, 418);
            Name = "AddUsuario";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AddUsuario";
            Load += AddUsuario_Load;
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Button btn_guardar;
        private ComboBox cbx_tipo_identificacion;
        private Label label6;
        private Label label1;
        private TextBox txt_numero_documento;
        private Label label7;
        private TextBox txt_nombre;
        private Label label2;
        private TextBox txt_apellido;
        private Label label3;
        private DateTimePicker dtp_fecha_cumpleaños;
        private ComboBox cbx_estado;
        private Label label14;
        private Label label4;
        private TextBox txt_email;
        private Label label5;
        private TextBox txt_telefono;
        private Label label8;
        private TextBox txt_usuario;
        private Label label9;
        private TextBox txt_contrasena;
        private ComboBox cbx_rol;
        private Label label10;
        private ErrorProvider errorProvider1;
    }
}