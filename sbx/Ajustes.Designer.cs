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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ajustes));
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            TabParametros = new TabPage();
            txt_ruta_backup = new TextBox();
            txt_impresora = new TextBox();
            txt_ancho_tirilla = new TextBox();
            cbx_pregunta_imprimir_venta = new ComboBox();
            cbx_valida_stock_venta = new ComboBox();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            lbl_valida_stock = new Label();
            panel2 = new Panel();
            button1 = new Button();
            textBox2 = new TextBox();
            btn_guardar_parametros = new Button();
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
            TabParametros.SuspendLayout();
            panel2.SuspendLayout();
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
            TabParametros.Controls.Add(txt_ruta_backup);
            TabParametros.Controls.Add(txt_impresora);
            TabParametros.Controls.Add(txt_ancho_tirilla);
            TabParametros.Controls.Add(cbx_pregunta_imprimir_venta);
            TabParametros.Controls.Add(cbx_valida_stock_venta);
            TabParametros.Controls.Add(label4);
            TabParametros.Controls.Add(label3);
            TabParametros.Controls.Add(label2);
            TabParametros.Controls.Add(label1);
            TabParametros.Controls.Add(lbl_valida_stock);
            TabParametros.Controls.Add(panel2);
            TabParametros.Location = new Point(4, 26);
            TabParametros.Name = "TabParametros";
            TabParametros.Padding = new Padding(3);
            TabParametros.Size = new Size(863, 522);
            TabParametros.TabIndex = 1;
            TabParametros.Text = "Parámetros";
            TabParametros.UseVisualStyleBackColor = true;
            // 
            // txt_ruta_backup
            // 
            txt_ruta_backup.Location = new Point(253, 188);
            txt_ruta_backup.Name = "txt_ruta_backup";
            txt_ruta_backup.Size = new Size(223, 23);
            txt_ruta_backup.TabIndex = 11;
            // 
            // txt_impresora
            // 
            txt_impresora.Location = new Point(253, 159);
            txt_impresora.Name = "txt_impresora";
            txt_impresora.Size = new Size(223, 23);
            txt_impresora.TabIndex = 10;
            // 
            // txt_ancho_tirilla
            // 
            txt_ancho_tirilla.Enabled = false;
            txt_ancho_tirilla.Location = new Point(253, 130);
            txt_ancho_tirilla.Name = "txt_ancho_tirilla";
            txt_ancho_tirilla.Size = new Size(223, 23);
            txt_ancho_tirilla.TabIndex = 9;
            txt_ancho_tirilla.KeyPress += txt_ancho_tirilla_KeyPress;
            // 
            // cbx_pregunta_imprimir_venta
            // 
            cbx_pregunta_imprimir_venta.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_pregunta_imprimir_venta.FormattingEnabled = true;
            cbx_pregunta_imprimir_venta.Items.AddRange(new object[] { "NO", "SI" });
            cbx_pregunta_imprimir_venta.Location = new Point(253, 97);
            cbx_pregunta_imprimir_venta.Name = "cbx_pregunta_imprimir_venta";
            cbx_pregunta_imprimir_venta.Size = new Size(121, 25);
            cbx_pregunta_imprimir_venta.TabIndex = 8;
            // 
            // cbx_valida_stock_venta
            // 
            cbx_valida_stock_venta.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_valida_stock_venta.FormattingEnabled = true;
            cbx_valida_stock_venta.Items.AddRange(new object[] { "SI", "NO" });
            cbx_valida_stock_venta.Location = new Point(253, 66);
            cbx_valida_stock_venta.Name = "cbx_valida_stock_venta";
            cbx_valida_stock_venta.Size = new Size(121, 25);
            cbx_valida_stock_venta.TabIndex = 7;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(8, 194);
            label4.Name = "label4";
            label4.Size = new Size(92, 17);
            label4.TabIndex = 6;
            label4.Text = "Ruta backup";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(8, 165);
            label3.Name = "label3";
            label3.Size = new Size(72, 17);
            label3.TabIndex = 5;
            label3.Text = "Impresora";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(8, 136);
            label2.Name = "label2";
            label2.Size = new Size(84, 17);
            label2.TabIndex = 4;
            label2.Text = "Ancho tirilla";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(8, 102);
            label1.Name = "label1";
            label1.Size = new Size(240, 17);
            label1.TabIndex = 3;
            label1.Text = "Preguntar imprimir factura en venta";
            // 
            // lbl_valida_stock
            // 
            lbl_valida_stock.AutoSize = true;
            lbl_valida_stock.Location = new Point(8, 74);
            lbl_valida_stock.Name = "lbl_valida_stock";
            lbl_valida_stock.Size = new Size(169, 17);
            lbl_valida_stock.TabIndex = 2;
            lbl_valida_stock.Text = "Validar stock para venta";
            // 
            // panel2
            // 
            panel2.BorderStyle = BorderStyle.Fixed3D;
            panel2.Controls.Add(button1);
            panel2.Controls.Add(textBox2);
            panel2.Controls.Add(btn_guardar_parametros);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(3, 3);
            panel2.Name = "panel2";
            panel2.Size = new Size(857, 56);
            panel2.TabIndex = 1;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatAppearance.MouseDownBackColor = Color.Gray;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Image = (Image)resources.GetObject("button1.Image");
            button1.Location = new Point(1447, 3);
            button1.Name = "button1";
            button1.Size = new Size(42, 45);
            button1.TabIndex = 5;
            button1.TextImageRelation = TextImageRelation.ImageBeforeText;
            button1.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            textBox2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            textBox2.Location = new Point(1250, 14);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(191, 23);
            textBox2.TabIndex = 2;
            // 
            // btn_guardar_parametros
            // 
            btn_guardar_parametros.Enabled = false;
            btn_guardar_parametros.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_guardar_parametros.FlatStyle = FlatStyle.Flat;
            btn_guardar_parametros.Image = (Image)resources.GetObject("btn_guardar_parametros.Image");
            btn_guardar_parametros.Location = new Point(3, 3);
            btn_guardar_parametros.Name = "btn_guardar_parametros";
            btn_guardar_parametros.Size = new Size(101, 45);
            btn_guardar_parametros.TabIndex = 0;
            btn_guardar_parametros.Text = "Guardar";
            btn_guardar_parametros.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_guardar_parametros.UseVisualStyleBackColor = true;
            btn_guardar_parametros.Click += btn_guardar_parametros_Click;
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
            tabControl1.SelectedIndexChanged += tabControl1_SelectedIndexChanged;
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
            TabParametros.ResumeLayout(false);
            TabParametros.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
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
        private Panel panel2;
        private Button button1;
        private TextBox textBox2;
        private Button btn_guardar_parametros;
        private Label lbl_valida_stock;
        private Label label2;
        private Label label1;
        private Label label3;
        private Label label4;
        private ComboBox cbx_valida_stock_venta;
        private ComboBox cbx_pregunta_imprimir_venta;
        private TextBox txt_ancho_tirilla;
        private TextBox txt_ruta_backup;
        private TextBox txt_impresora;
    }
}