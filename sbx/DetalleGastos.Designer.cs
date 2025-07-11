namespace sbx
{
    partial class DetalleGastos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DetalleGastos));
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            panel1 = new Panel();
            cbx_tipo_filtro = new ComboBox();
            cbx_campo_filtro = new ComboBox();
            btn_buscar = new Button();
            txt_buscar = new TextBox();
            btn_eliminar = new Button();
            btn_editar = new Button();
            btn_agregar = new Button();
            panel2 = new Panel();
            lbl_total_gastos = new Label();
            label1 = new Label();
            dtg_gastos = new DataGridView();
            cl_idGasto = new DataGridViewTextBoxColumn();
            cl_Categoria = new DataGridViewTextBoxColumn();
            cl_SubCategoria = new DataGridViewTextBoxColumn();
            cl_detalle = new DataGridViewTextBoxColumn();
            cl_valor_gasto = new DataGridViewTextBoxColumn();
            cl_fecha = new DataGridViewTextBoxColumn();
            cl_usuario = new DataGridViewTextBoxColumn();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtg_gastos).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.Window;
            panel1.BorderStyle = BorderStyle.Fixed3D;
            panel1.Controls.Add(cbx_tipo_filtro);
            panel1.Controls.Add(cbx_campo_filtro);
            panel1.Controls.Add(btn_buscar);
            panel1.Controls.Add(txt_buscar);
            panel1.Controls.Add(btn_eliminar);
            panel1.Controls.Add(btn_editar);
            panel1.Controls.Add(btn_agregar);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(955, 57);
            panel1.TabIndex = 0;
            // 
            // cbx_tipo_filtro
            // 
            cbx_tipo_filtro.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cbx_tipo_filtro.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_tipo_filtro.FormattingEnabled = true;
            cbx_tipo_filtro.Items.AddRange(new object[] { "Inicia con", "Igual a", "Contiene" });
            cbx_tipo_filtro.Location = new Point(592, 17);
            cbx_tipo_filtro.Name = "cbx_tipo_filtro";
            cbx_tipo_filtro.Size = new Size(87, 23);
            cbx_tipo_filtro.TabIndex = 12;
            // 
            // cbx_campo_filtro
            // 
            cbx_campo_filtro.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cbx_campo_filtro.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_campo_filtro.FormattingEnabled = true;
            cbx_campo_filtro.Items.AddRange(new object[] { "Categoria", "Subcategoria", "Detalle" });
            cbx_campo_filtro.Location = new Point(472, 17);
            cbx_campo_filtro.Name = "cbx_campo_filtro";
            cbx_campo_filtro.Size = new Size(114, 23);
            cbx_campo_filtro.TabIndex = 11;
            // 
            // btn_buscar
            // 
            btn_buscar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btn_buscar.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_buscar.FlatStyle = FlatStyle.Flat;
            btn_buscar.Image = (Image)resources.GetObject("btn_buscar.Image");
            btn_buscar.Location = new Point(913, 15);
            btn_buscar.Name = "btn_buscar";
            btn_buscar.Size = new Size(26, 26);
            btn_buscar.TabIndex = 9;
            btn_buscar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_buscar.UseVisualStyleBackColor = true;
            btn_buscar.Click += btn_buscar_Click;
            // 
            // txt_buscar
            // 
            txt_buscar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txt_buscar.Location = new Point(685, 17);
            txt_buscar.Name = "txt_buscar";
            txt_buscar.Size = new Size(222, 23);
            txt_buscar.TabIndex = 8;
            txt_buscar.KeyPress += txt_buscar_KeyPress;
            // 
            // btn_eliminar
            // 
            btn_eliminar.Enabled = false;
            btn_eliminar.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_eliminar.FlatStyle = FlatStyle.Flat;
            btn_eliminar.Image = (Image)resources.GetObject("btn_eliminar.Image");
            btn_eliminar.Location = new Point(224, 4);
            btn_eliminar.Name = "btn_eliminar";
            btn_eliminar.Size = new Size(101, 45);
            btn_eliminar.TabIndex = 7;
            btn_eliminar.Text = "Eliminar";
            btn_eliminar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_eliminar.UseVisualStyleBackColor = true;
            btn_eliminar.Click += btn_eliminar_Click;
            // 
            // btn_editar
            // 
            btn_editar.Enabled = false;
            btn_editar.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_editar.FlatStyle = FlatStyle.Flat;
            btn_editar.Image = (Image)resources.GetObject("btn_editar.Image");
            btn_editar.Location = new Point(117, 4);
            btn_editar.Name = "btn_editar";
            btn_editar.Size = new Size(101, 45);
            btn_editar.TabIndex = 6;
            btn_editar.Text = "Editar";
            btn_editar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_editar.UseVisualStyleBackColor = true;
            btn_editar.Click += btn_editar_Click;
            // 
            // btn_agregar
            // 
            btn_agregar.Enabled = false;
            btn_agregar.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_agregar.FlatStyle = FlatStyle.Flat;
            btn_agregar.Image = (Image)resources.GetObject("btn_agregar.Image");
            btn_agregar.Location = new Point(10, 4);
            btn_agregar.Name = "btn_agregar";
            btn_agregar.Size = new Size(101, 45);
            btn_agregar.TabIndex = 5;
            btn_agregar.Text = "Agregar";
            btn_agregar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_agregar.UseVisualStyleBackColor = true;
            btn_agregar.Click += btn_agregar_Click;
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.Window;
            panel2.Controls.Add(lbl_total_gastos);
            panel2.Controls.Add(label1);
            panel2.Dock = DockStyle.Bottom;
            panel2.Location = new Point(0, 409);
            panel2.Name = "panel2";
            panel2.Size = new Size(955, 41);
            panel2.TabIndex = 4;
            // 
            // lbl_total_gastos
            // 
            lbl_total_gastos.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lbl_total_gastos.AutoSize = true;
            lbl_total_gastos.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbl_total_gastos.Location = new Point(712, 6);
            lbl_total_gastos.Name = "lbl_total_gastos";
            lbl_total_gastos.Size = new Size(22, 30);
            lbl_total_gastos.TabIndex = 31;
            lbl_total_gastos.Text = "_";
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(649, 6);
            label1.Name = "label1";
            label1.Size = new Size(57, 30);
            label1.TabIndex = 30;
            label1.Text = "Total";
            // 
            // dtg_gastos
            // 
            dtg_gastos.AllowUserToAddRows = false;
            dtg_gastos.AllowUserToDeleteRows = false;
            dtg_gastos.AllowUserToOrderColumns = true;
            dtg_gastos.BackgroundColor = SystemColors.Control;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = SystemColors.Control;
            dataGridViewCellStyle4.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.ScrollBar;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            dtg_gastos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dtg_gastos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dtg_gastos.Columns.AddRange(new DataGridViewColumn[] { cl_idGasto, cl_Categoria, cl_SubCategoria, cl_detalle, cl_valor_gasto, cl_fecha, cl_usuario });
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = SystemColors.Window;
            dataGridViewCellStyle5.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle5.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = Color.FromArgb(172, 211, 236);
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.ControlText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.False;
            dtg_gastos.DefaultCellStyle = dataGridViewCellStyle5;
            dtg_gastos.Dock = DockStyle.Fill;
            dtg_gastos.Location = new Point(0, 57);
            dtg_gastos.Name = "dtg_gastos";
            dtg_gastos.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = SystemColors.Control;
            dataGridViewCellStyle6.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle6.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = Color.Silver;
            dataGridViewCellStyle6.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.True;
            dtg_gastos.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            dtg_gastos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtg_gastos.Size = new Size(955, 352);
            dtg_gastos.TabIndex = 5;
            // 
            // cl_idGasto
            // 
            cl_idGasto.HeaderText = "Id";
            cl_idGasto.Name = "cl_idGasto";
            cl_idGasto.ReadOnly = true;
            cl_idGasto.Width = 70;
            // 
            // cl_Categoria
            // 
            cl_Categoria.HeaderText = "Categoria";
            cl_Categoria.Name = "cl_Categoria";
            cl_Categoria.ReadOnly = true;
            cl_Categoria.Width = 180;
            // 
            // cl_SubCategoria
            // 
            cl_SubCategoria.HeaderText = "Sub categoria";
            cl_SubCategoria.Name = "cl_SubCategoria";
            cl_SubCategoria.ReadOnly = true;
            cl_SubCategoria.Width = 180;
            // 
            // cl_detalle
            // 
            cl_detalle.HeaderText = "Detalle";
            cl_detalle.Name = "cl_detalle";
            cl_detalle.ReadOnly = true;
            // 
            // cl_valor_gasto
            // 
            cl_valor_gasto.HeaderText = "Valor gasto";
            cl_valor_gasto.Name = "cl_valor_gasto";
            cl_valor_gasto.ReadOnly = true;
            cl_valor_gasto.Width = 180;
            // 
            // cl_fecha
            // 
            cl_fecha.HeaderText = "Fecha";
            cl_fecha.Name = "cl_fecha";
            cl_fecha.ReadOnly = true;
            // 
            // cl_usuario
            // 
            cl_usuario.HeaderText = "Usuario";
            cl_usuario.Name = "cl_usuario";
            cl_usuario.ReadOnly = true;
            // 
            // DetalleGastos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(955, 450);
            Controls.Add(dtg_gastos);
            Controls.Add(panel2);
            Controls.Add(panel1);
            MaximizeBox = false;
            MaximumSize = new Size(971, 489);
            MinimumSize = new Size(971, 489);
            Name = "DetalleGastos";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "DetalleGastos";
            Load += DetalleGastos_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dtg_gastos).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button btn_eliminar;
        private Button btn_editar;
        private Button btn_agregar;
        private Button btn_buscar;
        private TextBox txt_buscar;
        private Panel panel2;
        private DataGridView dtg_gastos;
        private DataGridViewTextBoxColumn cl_idGasto;
        private DataGridViewTextBoxColumn cl_Categoria;
        private DataGridViewTextBoxColumn cl_SubCategoria;
        private DataGridViewTextBoxColumn cl_detalle;
        private DataGridViewTextBoxColumn cl_valor_gasto;
        private DataGridViewTextBoxColumn cl_fecha;
        private DataGridViewTextBoxColumn cl_usuario;
        private Label lbl_total_gastos;
        private Label label1;
        private ComboBox cbx_tipo_filtro;
        private ComboBox cbx_campo_filtro;
    }
}