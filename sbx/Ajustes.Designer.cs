namespace sbx
{
    partial class Ajustes
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ajustes));
            TabParametros = new TabPage();
            tabPage1 = new TabPage();
            tabControl2 = new TabControl();
            tabPage2 = new TabPage();
            dtg_rangos_numeracion = new DataGridView();
            cl_Nro = new DataGridViewTextBoxColumn();
            cl_estado_ra = new DataGridViewTextBoxColumn();
            cl_tipo_documento = new DataGridViewTextBoxColumn();
            cl_prefijo = new DataGridViewTextBoxColumn();
            cl_numero_desde = new DataGridViewTextBoxColumn();
            cl_numero_hasta = new DataGridViewTextBoxColumn();
            cl_nro_autorizacion = new DataGridViewTextBoxColumn();
            cl_fecha_vencimiento = new DataGridViewTextBoxColumn();
            cl_numero_actual = new DataGridViewTextBoxColumn();
            panel1 = new Panel();
            btn_buscar_ra = new Button();
            btn_eliminar_ra = new Button();
            textBox1 = new TextBox();
            btn_editar_ra = new Button();
            btn_agregar_ra = new Button();
            tabControl1 = new TabControl();
            tabPage1.SuspendLayout();
            tabControl2.SuspendLayout();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtg_rangos_numeracion).BeginInit();
            panel1.SuspendLayout();
            tabControl1.SuspendLayout();
            SuspendLayout();
            // 
            // TabParametros
            // 
            TabParametros.Location = new Point(4, 26);
            TabParametros.Name = "TabParametros";
            TabParametros.Padding = new Padding(3);
            TabParametros.Size = new Size(863, 522);
            TabParametros.TabIndex = 1;
            TabParametros.Text = "Parámetros";
            TabParametros.UseVisualStyleBackColor = true;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(tabControl2);
            tabPage1.Location = new Point(4, 26);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(863, 522);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Rangos de numeración";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabControl2
            // 
            tabControl2.Controls.Add(tabPage2);
            tabControl2.Dock = DockStyle.Fill;
            tabControl2.Location = new Point(3, 3);
            tabControl2.Name = "tabControl2";
            tabControl2.SelectedIndex = 0;
            tabControl2.Size = new Size(857, 516);
            tabControl2.TabIndex = 0;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(dtg_rangos_numeracion);
            tabPage2.Controls.Add(panel1);
            tabPage2.Location = new Point(4, 26);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(849, 486);
            tabPage2.TabIndex = 0;
            tabPage2.Text = "Rangos de numeración";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // dtg_rangos_numeracion
            // 
            dtg_rangos_numeracion.AllowUserToAddRows = false;
            dtg_rangos_numeracion.AllowUserToDeleteRows = false;
            dtg_rangos_numeracion.AllowUserToOrderColumns = true;
            dtg_rangos_numeracion.BackgroundColor = SystemColors.Control;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.ScrollBar;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dtg_rangos_numeracion.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dtg_rangos_numeracion.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dtg_rangos_numeracion.Columns.AddRange(new DataGridViewColumn[] { cl_Nro, cl_estado_ra, cl_tipo_documento, cl_prefijo, cl_numero_desde, cl_numero_hasta, cl_nro_autorizacion, cl_fecha_vencimiento, cl_numero_actual });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(172, 211, 236);
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dtg_rangos_numeracion.DefaultCellStyle = dataGridViewCellStyle2;
            dtg_rangos_numeracion.Dock = DockStyle.Fill;
            dtg_rangos_numeracion.Location = new Point(3, 59);
            dtg_rangos_numeracion.Name = "dtg_rangos_numeracion";
            dtg_rangos_numeracion.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = Color.Silver;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dtg_rangos_numeracion.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dtg_rangos_numeracion.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtg_rangos_numeracion.Size = new Size(843, 424);
            dtg_rangos_numeracion.TabIndex = 1;
            // 
            // cl_Nro
            // 
            cl_Nro.HeaderText = "Nro.";
            cl_Nro.MinimumWidth = 60;
            cl_Nro.Name = "cl_Nro";
            cl_Nro.ReadOnly = true;
            cl_Nro.Width = 60;
            // 
            // cl_estado_ra
            // 
            cl_estado_ra.HeaderText = "Estado";
            cl_estado_ra.MinimumWidth = 70;
            cl_estado_ra.Name = "cl_estado_ra";
            cl_estado_ra.ReadOnly = true;
            cl_estado_ra.Resizable = DataGridViewTriState.True;
            cl_estado_ra.SortMode = DataGridViewColumnSortMode.NotSortable;
            cl_estado_ra.Width = 70;
            // 
            // cl_tipo_documento
            // 
            cl_tipo_documento.HeaderText = "Tipo Documento";
            cl_tipo_documento.MinimumWidth = 150;
            cl_tipo_documento.Name = "cl_tipo_documento";
            cl_tipo_documento.ReadOnly = true;
            cl_tipo_documento.Width = 150;
            // 
            // cl_prefijo
            // 
            cl_prefijo.HeaderText = "Prefijo";
            cl_prefijo.MinimumWidth = 60;
            cl_prefijo.Name = "cl_prefijo";
            cl_prefijo.ReadOnly = true;
            cl_prefijo.Width = 60;
            // 
            // cl_numero_desde
            // 
            cl_numero_desde.HeaderText = "Número Desde";
            cl_numero_desde.MinimumWidth = 130;
            cl_numero_desde.Name = "cl_numero_desde";
            cl_numero_desde.ReadOnly = true;
            cl_numero_desde.Width = 130;
            // 
            // cl_numero_hasta
            // 
            cl_numero_hasta.HeaderText = "Número Hasta";
            cl_numero_hasta.MinimumWidth = 126;
            cl_numero_hasta.Name = "cl_numero_hasta";
            cl_numero_hasta.ReadOnly = true;
            cl_numero_hasta.Width = 126;
            // 
            // cl_nro_autorizacion
            // 
            cl_nro_autorizacion.HeaderText = "Nro. Autorización";
            cl_nro_autorizacion.MinimumWidth = 150;
            cl_nro_autorizacion.Name = "cl_nro_autorizacion";
            cl_nro_autorizacion.ReadOnly = true;
            cl_nro_autorizacion.Width = 150;
            // 
            // cl_fecha_vencimiento
            // 
            cl_fecha_vencimiento.HeaderText = "Fecha vencimiento";
            cl_fecha_vencimiento.MinimumWidth = 160;
            cl_fecha_vencimiento.Name = "cl_fecha_vencimiento";
            cl_fecha_vencimiento.ReadOnly = true;
            cl_fecha_vencimiento.Width = 160;
            // 
            // cl_numero_actual
            // 
            cl_numero_actual.HeaderText = "Número actual";
            cl_numero_actual.MinimumWidth = 140;
            cl_numero_actual.Name = "cl_numero_actual";
            cl_numero_actual.ReadOnly = true;
            cl_numero_actual.Width = 140;
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.Fixed3D;
            panel1.Controls.Add(btn_buscar_ra);
            panel1.Controls.Add(btn_eliminar_ra);
            panel1.Controls.Add(textBox1);
            panel1.Controls.Add(btn_editar_ra);
            panel1.Controls.Add(btn_agregar_ra);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(3, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(843, 56);
            panel1.TabIndex = 0;
            // 
            // btn_buscar_ra
            // 
            btn_buscar_ra.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btn_buscar_ra.FlatAppearance.BorderSize = 0;
            btn_buscar_ra.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_buscar_ra.FlatStyle = FlatStyle.Flat;
            btn_buscar_ra.Image = (Image)resources.GetObject("btn_buscar_ra.Image");
            btn_buscar_ra.Location = new Point(794, 3);
            btn_buscar_ra.Name = "btn_buscar_ra";
            btn_buscar_ra.Size = new Size(42, 45);
            btn_buscar_ra.TabIndex = 5;
            btn_buscar_ra.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_buscar_ra.UseVisualStyleBackColor = true;
            btn_buscar_ra.Click += btn_buscar_ra_Click;
            // 
            // btn_eliminar_ra
            // 
            btn_eliminar_ra.Enabled = false;
            btn_eliminar_ra.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_eliminar_ra.FlatStyle = FlatStyle.Flat;
            btn_eliminar_ra.Image = (Image)resources.GetObject("btn_eliminar_ra.Image");
            btn_eliminar_ra.Location = new Point(217, 3);
            btn_eliminar_ra.Name = "btn_eliminar_ra";
            btn_eliminar_ra.Size = new Size(101, 45);
            btn_eliminar_ra.TabIndex = 4;
            btn_eliminar_ra.Text = "Eliminar";
            btn_eliminar_ra.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_eliminar_ra.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            textBox1.Location = new Point(597, 14);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(191, 23);
            textBox1.TabIndex = 2;
            // 
            // btn_editar_ra
            // 
            btn_editar_ra.Enabled = false;
            btn_editar_ra.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_editar_ra.FlatStyle = FlatStyle.Flat;
            btn_editar_ra.Image = (Image)resources.GetObject("btn_editar_ra.Image");
            btn_editar_ra.Location = new Point(110, 3);
            btn_editar_ra.Name = "btn_editar_ra";
            btn_editar_ra.Size = new Size(101, 45);
            btn_editar_ra.TabIndex = 1;
            btn_editar_ra.Text = "Editar";
            btn_editar_ra.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_editar_ra.UseVisualStyleBackColor = true;
            btn_editar_ra.Click += btn_editar_ra_Click;
            // 
            // btn_agregar_ra
            // 
            btn_agregar_ra.Enabled = false;
            btn_agregar_ra.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_agregar_ra.FlatStyle = FlatStyle.Flat;
            btn_agregar_ra.Image = (Image)resources.GetObject("btn_agregar_ra.Image");
            btn_agregar_ra.Location = new Point(3, 3);
            btn_agregar_ra.Name = "btn_agregar_ra";
            btn_agregar_ra.Size = new Size(101, 45);
            btn_agregar_ra.TabIndex = 0;
            btn_agregar_ra.Text = "Agregar";
            btn_agregar_ra.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_agregar_ra.UseVisualStyleBackColor = true;
            btn_agregar_ra.Click += btn_agregar_ra_Click;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(TabParametros);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(871, 552);
            tabControl1.TabIndex = 0;
            // 
            // Ajustes
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(871, 552);
            Controls.Add(tabControl1);
            MinimumSize = new Size(887, 591);
            Name = "Ajustes";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Ajustes";
            Load += Ajustes_Load;
            tabPage1.ResumeLayout(false);
            tabControl2.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dtg_rangos_numeracion).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            tabControl1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private TabPage TabParametros;
        private TabPage tabPage1;
        private TabControl tabControl2;
        private TabPage tabPage2;
        private DataGridView dtg_rangos_numeracion;
        private Panel panel1;
        private Button btn_buscar_ra;
        private Button btn_eliminar_ra;
        private TextBox textBox1;
        private Button btn_editar_ra;
        private Button btn_agregar_ra;
        private TabControl tabControl1;
        private DataGridViewTextBoxColumn cl_Nro;
        private DataGridViewTextBoxColumn cl_estado_ra;
        private DataGridViewTextBoxColumn cl_tipo_documento;
        private DataGridViewTextBoxColumn cl_prefijo;
        private DataGridViewTextBoxColumn cl_numero_desde;
        private DataGridViewTextBoxColumn cl_numero_hasta;
        private DataGridViewTextBoxColumn cl_nro_autorizacion;
        private DataGridViewTextBoxColumn cl_fecha_vencimiento;
        private DataGridViewTextBoxColumn cl_numero_actual;
    }
}