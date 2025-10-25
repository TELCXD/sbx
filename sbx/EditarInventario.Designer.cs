namespace sbx
{
    partial class EditarInventario
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditarInventario));
            dtpkNuevaFechaVencimiento = new DateTimePicker();
            lbl_fechaVencimiento = new Label();
            panel3 = new Panel();
            btn_guardar = new Button();
            dtpkActualFechaVencimiento = new DateTimePicker();
            label1 = new Label();
            chk_sin_fecha_vence = new CheckBox();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // dtpkNuevaFechaVencimiento
            // 
            dtpkNuevaFechaVencimiento.Format = DateTimePickerFormat.Short;
            dtpkNuevaFechaVencimiento.Location = new Point(36, 170);
            dtpkNuevaFechaVencimiento.Name = "dtpkNuevaFechaVencimiento";
            dtpkNuevaFechaVencimiento.Size = new Size(209, 23);
            dtpkNuevaFechaVencimiento.TabIndex = 128;
            // 
            // lbl_fechaVencimiento
            // 
            lbl_fechaVencimiento.AutoSize = true;
            lbl_fechaVencimiento.Location = new Point(36, 152);
            lbl_fechaVencimiento.Name = "lbl_fechaVencimiento";
            lbl_fechaVencimiento.Size = new Size(144, 15);
            lbl_fechaVencimiento.TabIndex = 127;
            lbl_fechaVencimiento.Text = "Nueva Fecha vencimiento";
            // 
            // panel3
            // 
            panel3.BackColor = Color.WhiteSmoke;
            panel3.BorderStyle = BorderStyle.Fixed3D;
            panel3.Controls.Add(btn_guardar);
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(0, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(302, 56);
            panel3.TabIndex = 129;
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
            // dtpkActualFechaVencimiento
            // 
            dtpkActualFechaVencimiento.Enabled = false;
            dtpkActualFechaVencimiento.Format = DateTimePickerFormat.Short;
            dtpkActualFechaVencimiento.Location = new Point(36, 86);
            dtpkActualFechaVencimiento.Name = "dtpkActualFechaVencimiento";
            dtpkActualFechaVencimiento.Size = new Size(209, 23);
            dtpkActualFechaVencimiento.TabIndex = 131;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(36, 68);
            label1.Name = "label1";
            label1.Size = new Size(144, 15);
            label1.TabIndex = 130;
            label1.Text = "Actual Fecha vencimiento";
            // 
            // chk_sin_fecha_vence
            // 
            chk_sin_fecha_vence.AutoSize = true;
            chk_sin_fecha_vence.Location = new Point(36, 130);
            chk_sin_fecha_vence.Name = "chk_sin_fecha_vence";
            chk_sin_fecha_vence.Size = new Size(143, 19);
            chk_sin_fecha_vence.TabIndex = 132;
            chk_sin_fecha_vence.Text = "Sin fecha vencimiento";
            chk_sin_fecha_vence.UseVisualStyleBackColor = true;
            chk_sin_fecha_vence.CheckedChanged += chk_sin_fecha_vence_CheckedChanged;
            // 
            // EditarInventario
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            ClientSize = new Size(302, 205);
            Controls.Add(chk_sin_fecha_vence);
            Controls.Add(dtpkActualFechaVencimiento);
            Controls.Add(label1);
            Controls.Add(panel3);
            Controls.Add(dtpkNuevaFechaVencimiento);
            Controls.Add(lbl_fechaVencimiento);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "EditarInventario";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "EditarInventario";
            Load += EditarInventario_Load;
            panel3.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DateTimePicker dtpkNuevaFechaVencimiento;
        private Label lbl_fechaVencimiento;
        private Panel panel3;
        private Button btn_guardar;
        private DateTimePicker dtpkActualFechaVencimiento;
        private Label label1;
        private CheckBox chk_sin_fecha_vence;
    }
}