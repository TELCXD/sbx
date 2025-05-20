namespace sbx
{
    partial class Login
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            btn_login = new Button();
            txt_user = new TextBox();
            txt_password = new TextBox();
            errorProvider1 = new ErrorProvider(components);
            panel1 = new Panel();
            pictureBox1 = new PictureBox();
            label1 = new Label();
            label2 = new Label();
            panel2 = new Panel();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // btn_login
            // 
            btn_login.BackColor = Color.Transparent;
            btn_login.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_login.FlatStyle = FlatStyle.Flat;
            btn_login.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btn_login.ForeColor = SystemColors.ControlText;
            btn_login.Image = (Image)resources.GetObject("btn_login.Image");
            btn_login.ImageAlign = ContentAlignment.MiddleRight;
            btn_login.Location = new Point(16, 133);
            btn_login.Name = "btn_login";
            btn_login.Size = new Size(247, 34);
            btn_login.TabIndex = 2;
            btn_login.Text = "INGRESAR";
            btn_login.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_login.UseVisualStyleBackColor = false;
            btn_login.Click += btn_login_Click;
            // 
            // txt_user
            // 
            txt_user.BackColor = SystemColors.Window;
            txt_user.BorderStyle = BorderStyle.FixedSingle;
            txt_user.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txt_user.ForeColor = Color.Black;
            txt_user.Location = new Point(16, 35);
            txt_user.Name = "txt_user";
            txt_user.Size = new Size(247, 23);
            txt_user.TabIndex = 0;
            // 
            // txt_password
            // 
            txt_password.BackColor = SystemColors.Window;
            txt_password.BorderStyle = BorderStyle.FixedSingle;
            txt_password.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txt_password.Location = new Point(16, 90);
            txt_password.Name = "txt_password";
            txt_password.Size = new Size(247, 23);
            txt_password.TabIndex = 1;
            txt_password.UseSystemPasswordChar = true;
            txt_password.KeyPress += txt_password_KeyPress;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.Window;
            panel1.BorderStyle = BorderStyle.Fixed3D;
            panel1.Controls.Add(pictureBox1);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(192, 191);
            panel1.TabIndex = 3;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(4, 35);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(181, 107);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = SystemColors.Window;
            label1.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.InfoText;
            label1.Location = new Point(16, 15);
            label1.Name = "label1";
            label1.Size = new Size(54, 17);
            label1.TabIndex = 4;
            label1.Text = "Usuario";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = SystemColors.InfoText;
            label2.Location = new Point(16, 70);
            label2.Name = "label2";
            label2.Size = new Size(84, 17);
            label2.TabIndex = 5;
            label2.Text = "Contraseña";
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.Window;
            panel2.BorderStyle = BorderStyle.Fixed3D;
            panel2.Controls.Add(txt_user);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(btn_login);
            panel2.Controls.Add(label1);
            panel2.Controls.Add(txt_password);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(192, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(293, 191);
            panel2.TabIndex = 6;
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DimGray;
            ClientSize = new Size(485, 191);
            Controls.Add(panel2);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Login";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Iniciar sesión";
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button btn_login;
        private TextBox txt_user;
        private TextBox txt_password;
        private ErrorProvider errorProvider1;
        private Panel panel1;
        private PictureBox pictureBox1;
        private Label label2;
        private Label label1;
        private Panel panel2;
    }
}
