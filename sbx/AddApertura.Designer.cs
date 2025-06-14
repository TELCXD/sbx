namespace sbx
{
    partial class AddApertura
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddApertura));
            label1 = new Label();
            txt_monto_inicial = new TextBox();
            btn_apertura = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 4);
            label1.Name = "label1";
            label1.Size = new Size(77, 15);
            label1.TabIndex = 114;
            label1.Text = "Monto inicial";
            // 
            // txt_monto_inicial
            // 
            txt_monto_inicial.Location = new Point(12, 22);
            txt_monto_inicial.MaxLength = 200;
            txt_monto_inicial.Name = "txt_monto_inicial";
            txt_monto_inicial.Size = new Size(299, 23);
            txt_monto_inicial.TabIndex = 113;
            txt_monto_inicial.KeyPress += txt_monto_inicial_KeyPress;
            // 
            // btn_apertura
            // 
            btn_apertura.Enabled = false;
            btn_apertura.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_apertura.FlatStyle = FlatStyle.Flat;
            btn_apertura.Image = (Image)resources.GetObject("btn_apertura.Image");
            btn_apertura.Location = new Point(106, 51);
            btn_apertura.Name = "btn_apertura";
            btn_apertura.Size = new Size(101, 45);
            btn_apertura.TabIndex = 115;
            btn_apertura.Text = "Apertura";
            btn_apertura.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_apertura.UseVisualStyleBackColor = true;
            btn_apertura.Click += btn_apertura_Click;
            // 
            // AddApertura
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            ClientSize = new Size(323, 104);
            Controls.Add(btn_apertura);
            Controls.Add(label1);
            Controls.Add(txt_monto_inicial);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MaximumSize = new Size(339, 143);
            MinimizeBox = false;
            MinimumSize = new Size(339, 143);
            Name = "AddApertura";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AddApertura";
            FormClosing += AddApertura_FormClosing;
            Load += AddApertura_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private TextBox txt_monto_inicial;
        private Button btn_apertura;
    }
}