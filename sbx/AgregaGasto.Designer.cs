namespace sbx
{
    partial class AgregaGasto
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AgregaGasto));
            panel3 = new Panel();
            btn_guardar = new Button();
            cbx_detalle = new ComboBox();
            label9 = new Label();
            cbx_subcategoria = new ComboBox();
            label8 = new Label();
            cbx_categoria = new ComboBox();
            label6 = new Label();
            label1 = new Label();
            txt_valor = new TextBox();
            errorProvider1 = new ErrorProvider(components);
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // panel3
            // 
            panel3.BackColor = SystemColors.Window;
            panel3.BorderStyle = BorderStyle.Fixed3D;
            panel3.Controls.Add(btn_guardar);
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(0, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(471, 56);
            panel3.TabIndex = 62;
            // 
            // btn_guardar
            // 
            btn_guardar.Enabled = false;
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
            // cbx_detalle
            // 
            cbx_detalle.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_detalle.FormattingEnabled = true;
            cbx_detalle.Location = new Point(6, 131);
            cbx_detalle.Name = "cbx_detalle";
            cbx_detalle.Size = new Size(207, 23);
            cbx_detalle.TabIndex = 65;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(6, 113);
            label9.Name = "label9";
            label9.Size = new Size(43, 15);
            label9.TabIndex = 68;
            label9.Text = "Detalle";
            // 
            // cbx_subcategoria
            // 
            cbx_subcategoria.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_subcategoria.FormattingEnabled = true;
            cbx_subcategoria.Location = new Point(241, 83);
            cbx_subcategoria.Name = "cbx_subcategoria";
            cbx_subcategoria.Size = new Size(207, 23);
            cbx_subcategoria.TabIndex = 64;
            cbx_subcategoria.SelectedIndexChanged += cbx_subcategoria_SelectedIndexChanged;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(241, 65);
            label8.Name = "label8";
            label8.Size = new Size(79, 15);
            label8.TabIndex = 67;
            label8.Text = "Sub categoria";
            // 
            // cbx_categoria
            // 
            cbx_categoria.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_categoria.FormattingEnabled = true;
            cbx_categoria.Items.AddRange(new object[] { "Gastos operativos", "Gastos de personal", "Gastos administrativos", "Gastos de ventas y marketing", "Gastos financieros", "Gastos misceláneos" });
            cbx_categoria.Location = new Point(6, 83);
            cbx_categoria.Name = "cbx_categoria";
            cbx_categoria.Size = new Size(207, 23);
            cbx_categoria.TabIndex = 63;
            cbx_categoria.SelectedIndexChanged += cbx_categoria_SelectedIndexChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(6, 65);
            label6.Name = "label6";
            label6.Size = new Size(58, 15);
            label6.TabIndex = 66;
            label6.Text = "Categoria";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(241, 111);
            label1.Name = "label1";
            label1.Size = new Size(33, 15);
            label1.TabIndex = 72;
            label1.Text = "Valor";
            // 
            // txt_valor
            // 
            txt_valor.Location = new Point(241, 131);
            txt_valor.MaxLength = 100;
            txt_valor.Name = "txt_valor";
            txt_valor.Size = new Size(207, 23);
            txt_valor.TabIndex = 71;
            txt_valor.KeyPress += txt_valor_KeyPress;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // AgregaGasto
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            ClientSize = new Size(471, 169);
            Controls.Add(label1);
            Controls.Add(txt_valor);
            Controls.Add(cbx_detalle);
            Controls.Add(label9);
            Controls.Add(cbx_subcategoria);
            Controls.Add(label8);
            Controls.Add(cbx_categoria);
            Controls.Add(label6);
            Controls.Add(panel3);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MaximumSize = new Size(487, 208);
            MinimumSize = new Size(487, 208);
            Name = "AgregaGasto";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AgregaEgreso";
            Load += AgregaGasto_Load;
            panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel3;
        private Button btn_guardar;
        private ComboBox cbx_detalle;
        private Label label9;
        private ComboBox cbx_subcategoria;
        private Label label8;
        private ComboBox cbx_categoria;
        private Label label6;
        private Label label1;
        private TextBox txt_valor;
        private ErrorProvider errorProvider1;
    }
}