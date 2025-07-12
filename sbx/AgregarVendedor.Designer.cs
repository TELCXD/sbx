namespace sbx
{
    partial class AgregarVendedor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AgregarVendedor));
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
            txt_nombre = new TextBox();
            label4 = new Label();
            txt_apellidos = new TextBox();
            errorProvider1 = new ErrorProvider(components);
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
            panel3.Name = "panel3";
            panel3.Size = new Size(680, 56);
            panel3.TabIndex = 64;
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
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(5, 269);
            label10.Name = "label10";
            label10.Size = new Size(57, 15);
            label10.TabIndex = 115;
            label10.Text = "Direccion";
            // 
            // txt_direccion
            // 
            txt_direccion.Location = new Point(5, 289);
            txt_direccion.MaxLength = 150;
            txt_direccion.Name = "txt_direccion";
            txt_direccion.Size = new Size(654, 23);
            txt_direccion.TabIndex = 5;
            // 
            // cbx_tipo_identificacion
            // 
            cbx_tipo_identificacion.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_tipo_identificacion.FormattingEnabled = true;
            cbx_tipo_identificacion.Location = new Point(5, 80);
            cbx_tipo_identificacion.Name = "cbx_tipo_identificacion";
            cbx_tipo_identificacion.Size = new Size(303, 23);
            cbx_tipo_identificacion.TabIndex = 1;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(5, 62);
            label6.Name = "label6";
            label6.Size = new Size(105, 15);
            label6.TabIndex = 114;
            label6.Text = "Tipo identificacion";
            // 
            // cbx_estado
            // 
            cbx_estado.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_estado.FormattingEnabled = true;
            cbx_estado.Items.AddRange(new object[] { "Activo", "Inactivo" });
            cbx_estado.Location = new Point(356, 80);
            cbx_estado.Name = "cbx_estado";
            cbx_estado.Size = new Size(303, 23);
            cbx_estado.TabIndex = 6;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(356, 62);
            label14.Name = "label14";
            label14.Size = new Size(42, 15);
            label14.TabIndex = 113;
            label14.Text = "Estado";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(356, 172);
            label3.Name = "label3";
            label3.Size = new Size(36, 15);
            label3.TabIndex = 112;
            label3.Text = "Email";
            // 
            // txt_email
            // 
            txt_email.Location = new Point(356, 192);
            txt_email.MaxLength = 100;
            txt_email.Name = "txt_email";
            txt_email.Size = new Size(303, 23);
            txt_email.TabIndex = 8;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(356, 118);
            label2.Name = "label2";
            label2.Size = new Size(52, 15);
            label2.TabIndex = 111;
            label2.Text = "Telefono";
            // 
            // txt_telefono
            // 
            txt_telefono.Location = new Point(356, 136);
            txt_telefono.MaxLength = 20;
            txt_telefono.Name = "txt_telefono";
            txt_telefono.Size = new Size(303, 23);
            txt_telefono.TabIndex = 7;
            txt_telefono.KeyPress += txt_telefono_KeyPress;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(5, 116);
            label1.Name = "label1";
            label1.Size = new Size(116, 15);
            label1.TabIndex = 110;
            label1.Text = "Numero documento";
            // 
            // txt_numero_documento
            // 
            txt_numero_documento.Location = new Point(5, 136);
            txt_numero_documento.MaxLength = 200;
            txt_numero_documento.Name = "txt_numero_documento";
            txt_numero_documento.Size = new Size(303, 23);
            txt_numero_documento.TabIndex = 2;
            txt_numero_documento.KeyPress += txt_numero_documento_KeyPress;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(5, 174);
            label7.Name = "label7";
            label7.Size = new Size(56, 15);
            label7.TabIndex = 109;
            label7.Text = "Nombres";
            // 
            // txt_nombre
            // 
            txt_nombre.Location = new Point(5, 192);
            txt_nombre.MaxLength = 100;
            txt_nombre.Name = "txt_nombre";
            txt_nombre.Size = new Size(303, 23);
            txt_nombre.TabIndex = 3;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(5, 222);
            label4.Name = "label4";
            label4.Size = new Size(56, 15);
            label4.TabIndex = 117;
            label4.Text = "Apellidos";
            // 
            // txt_apellidos
            // 
            txt_apellidos.Location = new Point(5, 240);
            txt_apellidos.MaxLength = 100;
            txt_apellidos.Name = "txt_apellidos";
            txt_apellidos.Size = new Size(303, 23);
            txt_apellidos.TabIndex = 4;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // AgregarVendedor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            ClientSize = new Size(680, 320);
            Controls.Add(label4);
            Controls.Add(txt_apellidos);
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
            Controls.Add(txt_nombre);
            Controls.Add(panel3);
            MaximizeBox = false;
            MaximumSize = new Size(696, 359);
            MinimumSize = new Size(696, 359);
            Name = "AgregarVendedor";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AgregarVendedor";
            Load += AgregarVendedor_Load;
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
        private TextBox txt_nombre;
        private Label label4;
        private TextBox txt_apellidos;
        private ErrorProvider errorProvider1;
    }
}