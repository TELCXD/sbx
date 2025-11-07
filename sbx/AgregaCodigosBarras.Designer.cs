namespace sbx
{
    partial class AgregaCodigosBarras
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AgregaCodigosBarras));
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            panel1 = new Panel();
            btn_guardar = new Button();
            label2 = new Label();
            lbl_id_producto = new Label();
            label5 = new Label();
            txt_codigo_barras = new TextBox();
            btn_quitar = new Button();
            panel2 = new Panel();
            dtg_prd_codigoBarras = new DataGridView();
            cl_id_producto = new DataGridViewTextBoxColumn();
            cl_codigo_barras = new DataGridViewTextBoxColumn();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtg_prd_codigoBarras).BeginInit();
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
            panel1.Size = new Size(430, 55);
            panel1.TabIndex = 0;
            // 
            // btn_guardar
            // 
            btn_guardar.Enabled = false;
            btn_guardar.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_guardar.FlatStyle = FlatStyle.Flat;
            btn_guardar.Image = (Image)resources.GetObject("btn_guardar.Image");
            btn_guardar.Location = new Point(7, 3);
            btn_guardar.Name = "btn_guardar";
            btn_guardar.Size = new Size(96, 45);
            btn_guardar.TabIndex = 1;
            btn_guardar.Text = "Guardar";
            btn_guardar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_guardar.UseVisualStyleBackColor = true;
            btn_guardar.Click += btn_guardar_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(106, 9);
            label2.Name = "label2";
            label2.Size = new Size(66, 15);
            label2.TabIndex = 129;
            label2.Text = "IdProducto";
            label2.Visible = false;
            // 
            // lbl_id_producto
            // 
            lbl_id_producto.AutoSize = true;
            lbl_id_producto.Location = new Point(184, 9);
            lbl_id_producto.Name = "lbl_id_producto";
            lbl_id_producto.Size = new Size(12, 15);
            lbl_id_producto.TabIndex = 128;
            lbl_id_producto.Text = "_";
            lbl_id_producto.Visible = false;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(7, 9);
            label5.Name = "label5";
            label5.Size = new Size(81, 15);
            label5.TabIndex = 1;
            label5.Text = "Codigo barras";
            // 
            // txt_codigo_barras
            // 
            txt_codigo_barras.Anchor = AnchorStyles.Top;
            txt_codigo_barras.Location = new Point(7, 27);
            txt_codigo_barras.Name = "txt_codigo_barras";
            txt_codigo_barras.Size = new Size(324, 23);
            txt_codigo_barras.TabIndex = 2;
            txt_codigo_barras.KeyPress += txt_codigo_barras_KeyPress;
            // 
            // btn_quitar
            // 
            btn_quitar.Enabled = false;
            btn_quitar.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_quitar.FlatStyle = FlatStyle.Flat;
            btn_quitar.Image = (Image)resources.GetObject("btn_quitar.Image");
            btn_quitar.Location = new Point(395, 24);
            btn_quitar.Name = "btn_quitar";
            btn_quitar.Size = new Size(28, 28);
            btn_quitar.TabIndex = 151;
            btn_quitar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_quitar.UseVisualStyleBackColor = true;
            btn_quitar.Click += btn_quitar_Click;
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.Window;
            panel2.BorderStyle = BorderStyle.Fixed3D;
            panel2.Controls.Add(btn_quitar);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(txt_codigo_barras);
            panel2.Controls.Add(lbl_id_producto);
            panel2.Controls.Add(label5);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 55);
            panel2.Name = "panel2";
            panel2.Size = new Size(430, 59);
            panel2.TabIndex = 1;
            // 
            // dtg_prd_codigoBarras
            // 
            dtg_prd_codigoBarras.AllowUserToAddRows = false;
            dtg_prd_codigoBarras.AllowUserToDeleteRows = false;
            dtg_prd_codigoBarras.AllowUserToOrderColumns = true;
            dtg_prd_codigoBarras.BackgroundColor = SystemColors.Control;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.ScrollBar;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dtg_prd_codigoBarras.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dtg_prd_codigoBarras.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dtg_prd_codigoBarras.Columns.AddRange(new DataGridViewColumn[] { cl_id_producto, cl_codigo_barras });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(172, 211, 236);
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dtg_prd_codigoBarras.DefaultCellStyle = dataGridViewCellStyle2;
            dtg_prd_codigoBarras.Dock = DockStyle.Fill;
            dtg_prd_codigoBarras.Location = new Point(0, 114);
            dtg_prd_codigoBarras.Name = "dtg_prd_codigoBarras";
            dtg_prd_codigoBarras.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = Color.Silver;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dtg_prd_codigoBarras.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dtg_prd_codigoBarras.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtg_prd_codigoBarras.Size = new Size(430, 336);
            dtg_prd_codigoBarras.TabIndex = 4;
            // 
            // cl_id_producto
            // 
            cl_id_producto.HeaderText = "Id";
            cl_id_producto.Name = "cl_id_producto";
            cl_id_producto.ReadOnly = true;
            cl_id_producto.Visible = false;
            cl_id_producto.Width = 200;
            // 
            // cl_codigo_barras
            // 
            cl_codigo_barras.HeaderText = "Codigo barras";
            cl_codigo_barras.Name = "cl_codigo_barras";
            cl_codigo_barras.ReadOnly = true;
            cl_codigo_barras.Width = 380;
            // 
            // AgregaCodigosBarras
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(430, 450);
            Controls.Add(dtg_prd_codigoBarras);
            Controls.Add(panel2);
            Controls.Add(panel1);
            MaximizeBox = false;
            MaximumSize = new Size(446, 489);
            MinimumSize = new Size(446, 489);
            Name = "AgregaCodigosBarras";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "s";
            Load += AgregaCodigosBarras_Load;
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dtg_prd_codigoBarras).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label label5;
        private TextBox txt_codigo_barras;
        private Label label2;
        private Label lbl_id_producto;
        private Button btn_quitar;
        private Panel panel2;
        private DataGridView dtg_prd_codigoBarras;
        private DataGridViewTextBoxColumn cl_id_producto;
        private DataGridViewTextBoxColumn cl_codigo_barras;
        private Button btn_guardar;
    }
}