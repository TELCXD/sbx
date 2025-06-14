namespace sbx
{
    partial class AddPagosEfectivo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddPagosEfectivo));
            panel1 = new Panel();
            btn_guardar = new Button();
            label3 = new Label();
            txt_valor_pago = new TextBox();
            label2 = new Label();
            txt_descripcion = new TextBox();
            errorProvider1 = new ErrorProvider(components);
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.Window;
            panel1.BorderStyle = BorderStyle.Fixed3D;
            panel1.Controls.Add(btn_guardar);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(333, 56);
            panel1.TabIndex = 1;
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
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(5, 62);
            label3.Name = "label3";
            label3.Size = new Size(132, 15);
            label3.TabIndex = 52;
            label3.Text = "Valor pago en efectivo *";
            // 
            // txt_valor_pago
            // 
            txt_valor_pago.Location = new Point(5, 82);
            txt_valor_pago.MaxLength = 13;
            txt_valor_pago.Name = "txt_valor_pago";
            txt_valor_pago.Size = new Size(303, 23);
            txt_valor_pago.TabIndex = 50;
            txt_valor_pago.KeyPress += txt_valor_pago_KeyPress;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(5, 113);
            label2.Name = "label2";
            label2.Size = new Size(126, 15);
            label2.TabIndex = 51;
            label2.Text = "Descripcion del pago *";
            // 
            // txt_descripcion
            // 
            txt_descripcion.Location = new Point(5, 133);
            txt_descripcion.MaxLength = 100;
            txt_descripcion.Name = "txt_descripcion";
            txt_descripcion.Size = new Size(303, 23);
            txt_descripcion.TabIndex = 49;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // AddPagosEfectivo
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            ClientSize = new Size(333, 169);
            Controls.Add(label3);
            Controls.Add(txt_valor_pago);
            Controls.Add(label2);
            Controls.Add(txt_descripcion);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MaximumSize = new Size(349, 208);
            MinimizeBox = false;
            MinimumSize = new Size(349, 208);
            Name = "AddPagosEfectivo";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AddPagosEfectivo";
            Load += AddPagosEfectivo_Load;
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Button btn_guardar;
        private Label label3;
        private TextBox txt_valor_pago;
        private Label label2;
        private TextBox txt_descripcion;
        private ErrorProvider errorProvider1;
    }
}