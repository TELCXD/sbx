namespace sbx
{
    partial class AgregaProductoGrupo
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AgregaProductoGrupo));
            label1 = new Label();
            txt_producto_grupo = new TextBox();
            panel4 = new Panel();
            lbl_total_cantidad = new Label();
            label9 = new Label();
            lbl_total_costos = new Label();
            label8 = new Label();
            lbl_total_precios = new Label();
            label4 = new Label();
            dtg_prd_individual = new DataGridView();
            cl_id_producto = new DataGridViewTextBoxColumn();
            cl_sku = new DataGridViewTextBoxColumn();
            cl_codigo_barras = new DataGridViewTextBoxColumn();
            cl_Nombre = new DataGridViewTextBoxColumn();
            cl_cantidad = new DataGridViewTextBoxColumn();
            cl_costo_unitario = new DataGridViewTextBoxColumn();
            cl_precio_unitario = new DataGridViewTextBoxColumn();
            panel2 = new Panel();
            btn_quitar1 = new Button();
            label20 = new Label();
            cbx_busca_por = new ComboBox();
            label5 = new Label();
            txt_buscar_producto = new TextBox();
            btn_busca_producto = new Button();
            lbl_nombre_producto = new Label();
            btn_busca_pr = new Button();
            label2 = new Label();
            label3 = new Label();
            lbl_precio_unitario = new Label();
            lbl_costo_unitario = new Label();
            label6 = new Label();
            errorProvider1 = new ErrorProvider(components);
            panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtg_prd_individual).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(7, 9);
            label1.Name = "label1";
            label1.Size = new Size(91, 15);
            label1.TabIndex = 0;
            label1.Text = "Producto grupo";
            // 
            // txt_producto_grupo
            // 
            txt_producto_grupo.Enabled = false;
            txt_producto_grupo.Location = new Point(7, 27);
            txt_producto_grupo.Name = "txt_producto_grupo";
            txt_producto_grupo.Size = new Size(488, 23);
            txt_producto_grupo.TabIndex = 1;
            // 
            // panel4
            // 
            panel4.BackColor = Color.White;
            panel4.BorderStyle = BorderStyle.Fixed3D;
            panel4.Controls.Add(lbl_total_cantidad);
            panel4.Controls.Add(label9);
            panel4.Controls.Add(lbl_total_costos);
            panel4.Controls.Add(label8);
            panel4.Controls.Add(lbl_total_precios);
            panel4.Controls.Add(label4);
            panel4.Dock = DockStyle.Bottom;
            panel4.Location = new Point(0, 440);
            panel4.Name = "panel4";
            panel4.Size = new Size(945, 108);
            panel4.TabIndex = 69;
            // 
            // lbl_total_cantidad
            // 
            lbl_total_cantidad.AutoSize = true;
            lbl_total_cantidad.Font = new Font("Segoe UI", 14F);
            lbl_total_cantidad.Location = new Point(734, 75);
            lbl_total_cantidad.Name = "lbl_total_cantidad";
            lbl_total_cantidad.Size = new Size(20, 25);
            lbl_total_cantidad.TabIndex = 35;
            lbl_total_cantidad.Text = "_";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 14F);
            label9.Location = new Point(598, 75);
            label9.Name = "label9";
            label9.Size = new Size(134, 25);
            label9.TabIndex = 34;
            label9.Text = "Total cantidad:";
            // 
            // lbl_total_costos
            // 
            lbl_total_costos.AutoSize = true;
            lbl_total_costos.Font = new Font("Segoe UI", 14F);
            lbl_total_costos.Location = new Point(734, 6);
            lbl_total_costos.Name = "lbl_total_costos";
            lbl_total_costos.Size = new Size(20, 25);
            lbl_total_costos.TabIndex = 33;
            lbl_total_costos.Text = "_";
            lbl_total_costos.Click += label7_Click;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 14F);
            label8.Location = new Point(598, 6);
            label8.Name = "label8";
            label8.Size = new Size(114, 25);
            label8.TabIndex = 32;
            label8.Text = "Total costos:";
            // 
            // lbl_total_precios
            // 
            lbl_total_precios.AutoSize = true;
            lbl_total_precios.Font = new Font("Segoe UI", 14F);
            lbl_total_precios.Location = new Point(734, 42);
            lbl_total_precios.Name = "lbl_total_precios";
            lbl_total_precios.Size = new Size(20, 25);
            lbl_total_precios.TabIndex = 31;
            lbl_total_precios.Text = "_";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 14F);
            label4.Location = new Point(598, 42);
            label4.Name = "label4";
            label4.Size = new Size(122, 25);
            label4.TabIndex = 30;
            label4.Text = "Total precios:";
            // 
            // dtg_prd_individual
            // 
            dtg_prd_individual.AllowUserToAddRows = false;
            dtg_prd_individual.AllowUserToDeleteRows = false;
            dtg_prd_individual.AllowUserToOrderColumns = true;
            dtg_prd_individual.BackgroundColor = SystemColors.Control;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.ScrollBar;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dtg_prd_individual.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dtg_prd_individual.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dtg_prd_individual.Columns.AddRange(new DataGridViewColumn[] { cl_id_producto, cl_sku, cl_codigo_barras, cl_Nombre, cl_cantidad, cl_costo_unitario, cl_precio_unitario });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(172, 211, 236);
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dtg_prd_individual.DefaultCellStyle = dataGridViewCellStyle2;
            dtg_prd_individual.Dock = DockStyle.Bottom;
            dtg_prd_individual.Location = new Point(0, 183);
            dtg_prd_individual.Name = "dtg_prd_individual";
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = Color.Silver;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dtg_prd_individual.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dtg_prd_individual.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtg_prd_individual.Size = new Size(945, 257);
            dtg_prd_individual.TabIndex = 70;
            dtg_prd_individual.CellBeginEdit += dtg_prd_individual_CellBeginEdit;
            dtg_prd_individual.CellEndEdit += dtg_prd_individual_CellEndEdit;
            dtg_prd_individual.EditingControlShowing += dtg_prd_individual_EditingControlShowing;
            dtg_prd_individual.KeyPress += dtg_prd_individual_KeyPress;
            // 
            // cl_id_producto
            // 
            cl_id_producto.HeaderText = "Id";
            cl_id_producto.Name = "cl_id_producto";
            cl_id_producto.Width = 50;
            // 
            // cl_sku
            // 
            cl_sku.HeaderText = "Sku";
            cl_sku.Name = "cl_sku";
            // 
            // cl_codigo_barras
            // 
            cl_codigo_barras.HeaderText = "Codigo b.";
            cl_codigo_barras.Name = "cl_codigo_barras";
            cl_codigo_barras.Width = 150;
            // 
            // cl_Nombre
            // 
            cl_Nombre.HeaderText = "Nombre";
            cl_Nombre.Name = "cl_Nombre";
            cl_Nombre.Width = 200;
            // 
            // cl_cantidad
            // 
            cl_cantidad.HeaderText = "Cantidad";
            cl_cantidad.Name = "cl_cantidad";
            // 
            // cl_costo_unitario
            // 
            cl_costo_unitario.HeaderText = "Costo und";
            cl_costo_unitario.Name = "cl_costo_unitario";
            cl_costo_unitario.Width = 150;
            // 
            // cl_precio_unitario
            // 
            cl_precio_unitario.HeaderText = "Precio und";
            cl_precio_unitario.Name = "cl_precio_unitario";
            cl_precio_unitario.Width = 150;
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.Window;
            panel2.BorderStyle = BorderStyle.Fixed3D;
            panel2.Controls.Add(btn_quitar1);
            panel2.Controls.Add(label20);
            panel2.Controls.Add(cbx_busca_por);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(txt_buscar_producto);
            panel2.Controls.Add(btn_busca_producto);
            panel2.Dock = DockStyle.Bottom;
            panel2.Enabled = false;
            panel2.Location = new Point(0, 127);
            panel2.Name = "panel2";
            panel2.Size = new Size(945, 56);
            panel2.TabIndex = 71;
            // 
            // btn_quitar1
            // 
            btn_quitar1.Enabled = false;
            btn_quitar1.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_quitar1.FlatStyle = FlatStyle.Flat;
            btn_quitar1.Image = (Image)resources.GetObject("btn_quitar1.Image");
            btn_quitar1.Location = new Point(899, 22);
            btn_quitar1.Name = "btn_quitar1";
            btn_quitar1.Size = new Size(32, 26);
            btn_quitar1.TabIndex = 150;
            btn_quitar1.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_quitar1.UseVisualStyleBackColor = true;
            btn_quitar1.Click += btn_quitar1_Click;
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Location = new Point(377, 7);
            label20.Name = "label20";
            label20.Size = new Size(59, 15);
            label20.TabIndex = 149;
            label20.Text = "Busca por";
            // 
            // cbx_busca_por
            // 
            cbx_busca_por.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_busca_por.FormattingEnabled = true;
            cbx_busca_por.Items.AddRange(new object[] { "Id", "Sku", "Codigo barras" });
            cbx_busca_por.Location = new Point(377, 25);
            cbx_busca_por.Name = "cbx_busca_por";
            cbx_busca_por.Size = new Size(147, 23);
            cbx_busca_por.TabIndex = 148;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(5, 7);
            label5.Name = "label5";
            label5.Size = new Size(111, 15);
            label5.TabIndex = 147;
            label5.Text = "Producto individual";
            // 
            // txt_buscar_producto
            // 
            txt_buscar_producto.Anchor = AnchorStyles.Top;
            txt_buscar_producto.Location = new Point(5, 25);
            txt_buscar_producto.Name = "txt_buscar_producto";
            txt_buscar_producto.Size = new Size(321, 23);
            txt_buscar_producto.TabIndex = 145;
            txt_buscar_producto.KeyPress += txt_buscar_producto_KeyPress;
            // 
            // btn_busca_producto
            // 
            btn_busca_producto.Enabled = false;
            btn_busca_producto.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_busca_producto.FlatStyle = FlatStyle.Flat;
            btn_busca_producto.Image = (Image)resources.GetObject("btn_busca_producto.Image");
            btn_busca_producto.Location = new Point(345, 22);
            btn_busca_producto.Name = "btn_busca_producto";
            btn_busca_producto.Size = new Size(26, 26);
            btn_busca_producto.TabIndex = 146;
            btn_busca_producto.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_busca_producto.UseVisualStyleBackColor = true;
            btn_busca_producto.Click += btn_busca_producto_Click;
            // 
            // lbl_nombre_producto
            // 
            lbl_nombre_producto.AutoSize = true;
            lbl_nombre_producto.Location = new Point(103, 55);
            lbl_nombre_producto.Name = "lbl_nombre_producto";
            lbl_nombre_producto.Size = new Size(12, 15);
            lbl_nombre_producto.TabIndex = 126;
            lbl_nombre_producto.Text = "_";
            // 
            // btn_busca_pr
            // 
            btn_busca_pr.Enabled = false;
            btn_busca_pr.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_busca_pr.FlatStyle = FlatStyle.Flat;
            btn_busca_pr.Image = (Image)resources.GetObject("btn_busca_pr.Image");
            btn_busca_pr.Location = new Point(501, 24);
            btn_busca_pr.Name = "btn_busca_pr";
            btn_busca_pr.Size = new Size(26, 26);
            btn_busca_pr.TabIndex = 125;
            btn_busca_pr.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_busca_pr.UseVisualStyleBackColor = true;
            btn_busca_pr.Click += btn_busca_pr_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(7, 55);
            label2.Name = "label2";
            label2.Size = new Size(57, 15);
            label2.TabIndex = 127;
            label2.Text = "Nombre: ";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(7, 106);
            label3.Name = "label3";
            label3.Size = new Size(90, 15);
            label3.TabIndex = 128;
            label3.Text = "Precio unitario: ";
            // 
            // lbl_precio_unitario
            // 
            lbl_precio_unitario.AutoSize = true;
            lbl_precio_unitario.Location = new Point(103, 106);
            lbl_precio_unitario.Name = "lbl_precio_unitario";
            lbl_precio_unitario.Size = new Size(12, 15);
            lbl_precio_unitario.TabIndex = 129;
            lbl_precio_unitario.Text = "_";
            // 
            // lbl_costo_unitario
            // 
            lbl_costo_unitario.AutoSize = true;
            lbl_costo_unitario.Location = new Point(103, 79);
            lbl_costo_unitario.Name = "lbl_costo_unitario";
            lbl_costo_unitario.Size = new Size(12, 15);
            lbl_costo_unitario.TabIndex = 131;
            lbl_costo_unitario.Text = "_";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(7, 79);
            label6.Name = "label6";
            label6.Size = new Size(88, 15);
            label6.TabIndex = 130;
            label6.Text = "Costo unitario: ";
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // AgregaProductoGrupo
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            ClientSize = new Size(945, 548);
            Controls.Add(lbl_costo_unitario);
            Controls.Add(label6);
            Controls.Add(lbl_precio_unitario);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(lbl_nombre_producto);
            Controls.Add(btn_busca_pr);
            Controls.Add(panel2);
            Controls.Add(dtg_prd_individual);
            Controls.Add(panel4);
            Controls.Add(txt_producto_grupo);
            Controls.Add(label1);
            MaximizeBox = false;
            Name = "AgregaProductoGrupo";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AgregaProductoGrupo";
            Load += AgregaProductoGrupo_Load;
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dtg_prd_individual).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txt_producto_grupo;
        private Panel panel4;
        private Label lbl_total_precios;
        private Label label4;
        private DataGridView dtg_prd_individual;
        private Panel panel2;
        private Label lbl_nombre_producto;
        private Button btn_busca_pr;
        private DataGridViewTextBoxColumn cl_id_producto;
        private DataGridViewTextBoxColumn cl_sku;
        private DataGridViewTextBoxColumn cl_codigo_barras;
        private DataGridViewTextBoxColumn cl_Nombre;
        private DataGridViewTextBoxColumn cl_cantidad;
        private DataGridViewTextBoxColumn cl_costo_unitario;
        private DataGridViewTextBoxColumn cl_precio_unitario;
        private Label label2;
        private Label label3;
        private Label lbl_precio_unitario;
        private Label lbl_costo_unitario;
        private Label label6;
        private Label lbl_total_costos;
        private Label label8;
        private Label label20;
        private ComboBox cbx_busca_por;
        private Label label5;
        private TextBox txt_buscar_producto;
        private Button btn_busca_producto;
        private Button btn_quitar1;
        private ErrorProvider errorProvider1;
        private Label lbl_total_cantidad;
        private Label label9;
    }
}