﻿namespace sbx
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
            panel3.Size = new Size(701, 56);
            panel3.TabIndex = 63;
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
            label10.Location = new Point(12, 290);
            label10.Name = "label10";
            label10.Size = new Size(57, 15);
            label10.TabIndex = 99;
            label10.Text = "Direccion";
            // 
            // txt_direccion
            // 
            txt_direccion.Location = new Point(12, 310);
            txt_direccion.MaxLength = 150;
            txt_direccion.Name = "txt_direccion";
            txt_direccion.Size = new Size(654, 23);
            txt_direccion.TabIndex = 89;
            // 
            // cbx_tipo_identificacion
            // 
            cbx_tipo_identificacion.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_tipo_identificacion.FormattingEnabled = true;
            cbx_tipo_identificacion.Location = new Point(12, 87);
            cbx_tipo_identificacion.Name = "cbx_tipo_identificacion";
            cbx_tipo_identificacion.Size = new Size(303, 23);
            cbx_tipo_identificacion.TabIndex = 86;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 69);
            label6.Name = "label6";
            label6.Size = new Size(105, 15);
            label6.TabIndex = 98;
            label6.Text = "Tipo identificacion";
            // 
            // cbx_estado
            // 
            cbx_estado.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_estado.FormattingEnabled = true;
            cbx_estado.Items.AddRange(new object[] { "Activo", "Inactivo" });
            cbx_estado.Location = new Point(363, 87);
            cbx_estado.Name = "cbx_estado";
            cbx_estado.Size = new Size(303, 23);
            cbx_estado.TabIndex = 90;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(363, 69);
            label14.Name = "label14";
            label14.Size = new Size(42, 15);
            label14.TabIndex = 97;
            label14.Text = "Estado";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(363, 179);
            label3.Name = "label3";
            label3.Size = new Size(36, 15);
            label3.TabIndex = 96;
            label3.Text = "Email";
            // 
            // txt_email
            // 
            txt_email.Location = new Point(363, 199);
            txt_email.MaxLength = 100;
            txt_email.Name = "txt_email";
            txt_email.Size = new Size(303, 23);
            txt_email.TabIndex = 92;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(363, 125);
            label2.Name = "label2";
            label2.Size = new Size(52, 15);
            label2.TabIndex = 95;
            label2.Text = "Telefono";
            // 
            // txt_telefono
            // 
            txt_telefono.Location = new Point(363, 143);
            txt_telefono.MaxLength = 20;
            txt_telefono.Name = "txt_telefono";
            txt_telefono.Size = new Size(303, 23);
            txt_telefono.TabIndex = 91;
            txt_telefono.KeyPress += txt_telefono_KeyPress;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 123);
            label1.Name = "label1";
            label1.Size = new Size(116, 15);
            label1.TabIndex = 94;
            label1.Text = "Numero documento";
            // 
            // txt_numero_documento
            // 
            txt_numero_documento.Location = new Point(12, 143);
            txt_numero_documento.MaxLength = 200;
            txt_numero_documento.Name = "txt_numero_documento";
            txt_numero_documento.Size = new Size(303, 23);
            txt_numero_documento.TabIndex = 87;
            txt_numero_documento.KeyPress += txt_numero_documento_KeyPress;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(12, 181);
            label7.Name = "label7";
            label7.Size = new Size(127, 15);
            label7.TabIndex = 93;
            label7.Text = "Nombre / Razon social";
            // 
            // txt_nombre_razon_social
            // 
            txt_nombre_razon_social.Location = new Point(12, 199);
            txt_nombre_razon_social.MaxLength = 100;
            txt_nombre_razon_social.Name = "txt_nombre_razon_social";
            txt_nombre_razon_social.Size = new Size(303, 23);
            txt_nombre_razon_social.TabIndex = 88;
            // 
            // cbx_tipo_cliente
            // 
            cbx_tipo_cliente.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_tipo_cliente.FormattingEnabled = true;
            cbx_tipo_cliente.Location = new Point(12, 253);
            cbx_tipo_cliente.Name = "cbx_tipo_cliente";
            cbx_tipo_cliente.Size = new Size(303, 23);
            cbx_tipo_cliente.TabIndex = 100;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 235);
            label4.Name = "label4";
            label4.Size = new Size(68, 15);
            label4.TabIndex = 101;
            label4.Text = "Tipo cliente";
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // AgregarCliente
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            ClientSize = new Size(701, 348);
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
            MaximizeBox = false;
            MaximumSize = new Size(717, 387);
            MinimumSize = new Size(717, 387);
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
    }
}