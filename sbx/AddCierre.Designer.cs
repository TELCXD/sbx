namespace sbx
{
    partial class AddCierre
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddCierre));
            label1 = new Label();
            txt_monto_final = new TextBox();
            btn_cierre = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 4);
            label1.Name = "label1";
            label1.Size = new Size(69, 15);
            label1.TabIndex = 116;
            label1.Text = "Monto final";
            // 
            // txt_monto_final
            // 
            txt_monto_final.Location = new Point(12, 22);
            txt_monto_final.MaxLength = 200;
            txt_monto_final.Name = "txt_monto_final";
            txt_monto_final.Size = new Size(284, 23);
            txt_monto_final.TabIndex = 115;
            txt_monto_final.KeyPress += txt_monto_final_KeyPress;
            // 
            // btn_cierre
            // 
            btn_cierre.Enabled = false;
            btn_cierre.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_cierre.FlatStyle = FlatStyle.Flat;
            btn_cierre.Image = (Image)resources.GetObject("btn_cierre.Image");
            btn_cierre.Location = new Point(100, 57);
            btn_cierre.Name = "btn_cierre";
            btn_cierre.Size = new Size(101, 45);
            btn_cierre.TabIndex = 117;
            btn_cierre.Text = "Cierre";
            btn_cierre.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_cierre.UseVisualStyleBackColor = true;
            btn_cierre.Click += btn_cierre_Click;
            // 
            // AddCierre
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            ClientSize = new Size(308, 114);
            Controls.Add(btn_cierre);
            Controls.Add(label1);
            Controls.Add(txt_monto_final);
            MaximizeBox = false;
            MaximumSize = new Size(324, 153);
            MinimizeBox = false;
            MinimumSize = new Size(324, 153);
            Name = "AddCierre";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AddCierre";
            Load += AddCierre_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txt_monto_final;
        private Button btn_cierre;
    }
}