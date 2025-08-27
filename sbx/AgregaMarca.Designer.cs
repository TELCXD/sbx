namespace sbx
{
    partial class AgregaMarca
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AgregaMarca));
            panel3 = new Panel();
            btn_guardar = new Button();
            label1 = new Label();
            txt_id = new TextBox();
            label7 = new Label();
            txt_nombre = new TextBox();
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
            panel3.Size = new Size(341, 56);
            panel3.TabIndex = 62;
            // 
            // btn_guardar
            // 
            btn_guardar.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_guardar.FlatStyle = FlatStyle.Flat;
            btn_guardar.Image = (Image)resources.GetObject("btn_guardar.Image");
            btn_guardar.Location = new Point(4, 3);
            btn_guardar.Name = "btn_guardar";
            btn_guardar.Size = new Size(96, 45);
            btn_guardar.TabIndex = 0;
            btn_guardar.Text = "Guardar";
            btn_guardar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_guardar.UseVisualStyleBackColor = true;
            btn_guardar.Click += btn_guardar_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 67);
            label1.Name = "label1";
            label1.Size = new Size(17, 15);
            label1.TabIndex = 66;
            label1.Text = "Id";
            // 
            // txt_id
            // 
            txt_id.Enabled = false;
            txt_id.Location = new Point(6, 87);
            txt_id.MaxLength = 200;
            txt_id.Name = "txt_id";
            txt_id.Size = new Size(303, 23);
            txt_id.TabIndex = 63;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(6, 125);
            label7.Name = "label7";
            label7.Size = new Size(51, 15);
            label7.TabIndex = 65;
            label7.Text = "Nombre";
            // 
            // txt_nombre
            // 
            txt_nombre.Location = new Point(6, 143);
            txt_nombre.MaxLength = 50;
            txt_nombre.Name = "txt_nombre";
            txt_nombre.Size = new Size(303, 23);
            txt_nombre.TabIndex = 64;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // AgregaMarca
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            ClientSize = new Size(341, 180);
            Controls.Add(label1);
            Controls.Add(txt_id);
            Controls.Add(label7);
            Controls.Add(txt_nombre);
            Controls.Add(panel3);
            MaximizeBox = false;
            MaximumSize = new Size(357, 219);
            MinimumSize = new Size(357, 219);
            Name = "AgregaMarca";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AgregaMarca";
            Load += AgregaMarca_Load;
            panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel3;
        private Button btn_guardar;
        private Label label1;
        private TextBox txt_id;
        private Label label7;
        private TextBox txt_nombre;
        private ErrorProvider errorProvider1;
    }
}