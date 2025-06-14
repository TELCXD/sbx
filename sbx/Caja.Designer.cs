namespace sbx
{
    partial class Caja
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Caja));
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle9 = new DataGridViewCellStyle();
            panel1 = new Panel();
            label2 = new Label();
            label1 = new Label();
            dtp_F_f = new DateTimePicker();
            dtp_F_ini = new DateTimePicker();
            cbx_tipo_filtro = new ComboBox();
            cbx_campo_filtro = new ComboBox();
            btn_buscar = new Button();
            txt_buscar = new TextBox();
            btn_cierre = new Button();
            btn_apertura = new Button();
            dtg_aperturas_cierres_caja = new DataGridView();
            cl_IdApertura_Cierre_caja = new DataGridViewTextBoxColumn();
            cl_IdUser = new DataGridViewTextBoxColumn();
            cl_usuario = new DataGridViewTextBoxColumn();
            cl_fecha_apertura = new DataGridViewTextBoxColumn();
            cl_monto_inicial = new DataGridViewTextBoxColumn();
            cl_fecha_cierre = new DataGridViewTextBoxColumn();
            cl_montoFinal = new DataGridViewTextBoxColumn();
            cl_ventasTotales = new DataGridViewTextBoxColumn();
            cl_pago_en_efectivo = new DataGridViewTextBoxColumn();
            cl_diferencia = new DataGridViewTextBoxColumn();
            cl_estado = new DataGridViewTextBoxColumn();
            errorProvider1 = new ErrorProvider(components);
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtg_aperturas_cierres_caja).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.Window;
            panel1.BorderStyle = BorderStyle.Fixed3D;
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(dtp_F_f);
            panel1.Controls.Add(dtp_F_ini);
            panel1.Controls.Add(cbx_tipo_filtro);
            panel1.Controls.Add(cbx_campo_filtro);
            panel1.Controls.Add(btn_buscar);
            panel1.Controls.Add(txt_buscar);
            panel1.Controls.Add(btn_cierre);
            panel1.Controls.Add(btn_apertura);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1211, 56);
            panel1.TabIndex = 0;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(624, 21);
            label2.Name = "label2";
            label2.Size = new Size(35, 15);
            label2.TabIndex = 18;
            label2.Text = "F. Fin";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(454, 21);
            label1.Name = "label1";
            label1.Size = new Size(48, 15);
            label1.TabIndex = 17;
            label1.Text = "F. Inicio";
            // 
            // dtp_F_f
            // 
            dtp_F_f.Format = DateTimePickerFormat.Short;
            dtp_F_f.Location = new Point(665, 15);
            dtp_F_f.Name = "dtp_F_f";
            dtp_F_f.Size = new Size(110, 23);
            dtp_F_f.TabIndex = 16;
            // 
            // dtp_F_ini
            // 
            dtp_F_ini.Format = DateTimePickerFormat.Short;
            dtp_F_ini.Location = new Point(508, 15);
            dtp_F_ini.Name = "dtp_F_ini";
            dtp_F_ini.Size = new Size(110, 23);
            dtp_F_ini.TabIndex = 15;
            // 
            // cbx_tipo_filtro
            // 
            cbx_tipo_filtro.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cbx_tipo_filtro.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_tipo_filtro.FormattingEnabled = true;
            cbx_tipo_filtro.Items.AddRange(new object[] { "Inicia con", "Igual a", "Contiene" });
            cbx_tipo_filtro.Location = new Point(901, 15);
            cbx_tipo_filtro.Name = "cbx_tipo_filtro";
            cbx_tipo_filtro.Size = new Size(87, 23);
            cbx_tipo_filtro.TabIndex = 14;
            // 
            // cbx_campo_filtro
            // 
            cbx_campo_filtro.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cbx_campo_filtro.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_campo_filtro.FormattingEnabled = true;
            cbx_campo_filtro.Items.AddRange(new object[] { "Nombre usuario", "Id usuario" });
            cbx_campo_filtro.Location = new Point(781, 15);
            cbx_campo_filtro.Name = "cbx_campo_filtro";
            cbx_campo_filtro.Size = new Size(114, 23);
            cbx_campo_filtro.TabIndex = 13;
            // 
            // btn_buscar
            // 
            btn_buscar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btn_buscar.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_buscar.FlatStyle = FlatStyle.Flat;
            btn_buscar.Image = (Image)resources.GetObject("btn_buscar.Image");
            btn_buscar.Location = new Point(1177, 13);
            btn_buscar.Name = "btn_buscar";
            btn_buscar.Size = new Size(26, 26);
            btn_buscar.TabIndex = 12;
            btn_buscar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_buscar.UseVisualStyleBackColor = true;
            btn_buscar.Click += btn_buscar_Click;
            // 
            // txt_buscar
            // 
            txt_buscar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txt_buscar.Location = new Point(994, 15);
            txt_buscar.Name = "txt_buscar";
            txt_buscar.Size = new Size(177, 23);
            txt_buscar.TabIndex = 11;
            // 
            // btn_cierre
            // 
            btn_cierre.Enabled = false;
            btn_cierre.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_cierre.FlatStyle = FlatStyle.Flat;
            btn_cierre.Image = (Image)resources.GetObject("btn_cierre.Image");
            btn_cierre.Location = new Point(110, 4);
            btn_cierre.Name = "btn_cierre";
            btn_cierre.Size = new Size(101, 45);
            btn_cierre.TabIndex = 3;
            btn_cierre.Text = "Cierre";
            btn_cierre.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_cierre.UseVisualStyleBackColor = true;
            btn_cierre.Click += btn_cierre_Click;
            // 
            // btn_apertura
            // 
            btn_apertura.Enabled = false;
            btn_apertura.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_apertura.FlatStyle = FlatStyle.Flat;
            btn_apertura.Image = (Image)resources.GetObject("btn_apertura.Image");
            btn_apertura.Location = new Point(3, 4);
            btn_apertura.Name = "btn_apertura";
            btn_apertura.Size = new Size(101, 45);
            btn_apertura.TabIndex = 2;
            btn_apertura.Text = "Apertura";
            btn_apertura.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_apertura.UseVisualStyleBackColor = true;
            btn_apertura.Click += btn_apertura_Click;
            // 
            // dtg_aperturas_cierres_caja
            // 
            dtg_aperturas_cierres_caja.AllowUserToAddRows = false;
            dtg_aperturas_cierres_caja.AllowUserToDeleteRows = false;
            dtg_aperturas_cierres_caja.AllowUserToOrderColumns = true;
            dtg_aperturas_cierres_caja.BackgroundColor = SystemColors.Control;
            dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = SystemColors.Control;
            dataGridViewCellStyle7.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle7.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = SystemColors.ScrollBar;
            dataGridViewCellStyle7.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = DataGridViewTriState.True;
            dtg_aperturas_cierres_caja.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            dtg_aperturas_cierres_caja.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dtg_aperturas_cierres_caja.Columns.AddRange(new DataGridViewColumn[] { cl_IdApertura_Cierre_caja, cl_IdUser, cl_usuario, cl_fecha_apertura, cl_monto_inicial, cl_fecha_cierre, cl_montoFinal, cl_ventasTotales, cl_pago_en_efectivo, cl_diferencia, cl_estado });
            dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = SystemColors.Window;
            dataGridViewCellStyle8.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle8.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = Color.FromArgb(172, 211, 236);
            dataGridViewCellStyle8.SelectionForeColor = SystemColors.ControlText;
            dataGridViewCellStyle8.WrapMode = DataGridViewTriState.False;
            dtg_aperturas_cierres_caja.DefaultCellStyle = dataGridViewCellStyle8;
            dtg_aperturas_cierres_caja.Dock = DockStyle.Fill;
            dtg_aperturas_cierres_caja.Location = new Point(0, 56);
            dtg_aperturas_cierres_caja.Name = "dtg_aperturas_cierres_caja";
            dtg_aperturas_cierres_caja.ReadOnly = true;
            dataGridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = SystemColors.Control;
            dataGridViewCellStyle9.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle9.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = Color.Silver;
            dataGridViewCellStyle9.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = DataGridViewTriState.True;
            dtg_aperturas_cierres_caja.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            dtg_aperturas_cierres_caja.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtg_aperturas_cierres_caja.Size = new Size(1211, 443);
            dtg_aperturas_cierres_caja.TabIndex = 3;
            // 
            // cl_IdApertura_Cierre_caja
            // 
            cl_IdApertura_Cierre_caja.HeaderText = "Id";
            cl_IdApertura_Cierre_caja.Name = "cl_IdApertura_Cierre_caja";
            cl_IdApertura_Cierre_caja.ReadOnly = true;
            cl_IdApertura_Cierre_caja.Visible = false;
            // 
            // cl_IdUser
            // 
            cl_IdUser.HeaderText = "IdUsuario";
            cl_IdUser.Name = "cl_IdUser";
            cl_IdUser.ReadOnly = true;
            cl_IdUser.Visible = false;
            // 
            // cl_usuario
            // 
            cl_usuario.HeaderText = "Usuario";
            cl_usuario.Name = "cl_usuario";
            cl_usuario.ReadOnly = true;
            cl_usuario.Width = 135;
            // 
            // cl_fecha_apertura
            // 
            cl_fecha_apertura.HeaderText = "Fecha Apertura";
            cl_fecha_apertura.Name = "cl_fecha_apertura";
            cl_fecha_apertura.ReadOnly = true;
            cl_fecha_apertura.Width = 135;
            // 
            // cl_monto_inicial
            // 
            cl_monto_inicial.HeaderText = "Monto inicial";
            cl_monto_inicial.Name = "cl_monto_inicial";
            cl_monto_inicial.ReadOnly = true;
            cl_monto_inicial.Width = 120;
            // 
            // cl_fecha_cierre
            // 
            cl_fecha_cierre.HeaderText = "Fecha cierre";
            cl_fecha_cierre.Name = "cl_fecha_cierre";
            cl_fecha_cierre.ReadOnly = true;
            cl_fecha_cierre.Width = 135;
            // 
            // cl_montoFinal
            // 
            cl_montoFinal.HeaderText = "Monto final";
            cl_montoFinal.Name = "cl_montoFinal";
            cl_montoFinal.ReadOnly = true;
            cl_montoFinal.Width = 130;
            // 
            // cl_ventasTotales
            // 
            cl_ventasTotales.HeaderText = "Total ventas";
            cl_ventasTotales.Name = "cl_ventasTotales";
            cl_ventasTotales.ReadOnly = true;
            cl_ventasTotales.Width = 130;
            // 
            // cl_pago_en_efectivo
            // 
            cl_pago_en_efectivo.HeaderText = "Pagos en Efectivo";
            cl_pago_en_efectivo.Name = "cl_pago_en_efectivo";
            cl_pago_en_efectivo.ReadOnly = true;
            cl_pago_en_efectivo.Width = 150;
            // 
            // cl_diferencia
            // 
            cl_diferencia.HeaderText = "Diferencia";
            cl_diferencia.Name = "cl_diferencia";
            cl_diferencia.ReadOnly = true;
            cl_diferencia.Width = 130;
            // 
            // cl_estado
            // 
            cl_estado.HeaderText = "Estado";
            cl_estado.Name = "cl_estado";
            cl_estado.ReadOnly = true;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // Caja
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1211, 499);
            Controls.Add(dtg_aperturas_cierres_caja);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MaximumSize = new Size(1227, 538);
            MinimumSize = new Size(1227, 538);
            Name = "Caja";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Caja";
            Load += Caja_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dtg_aperturas_cierres_caja).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button btn_cierre;
        private Button btn_apertura;
        private ComboBox cbx_tipo_filtro;
        private ComboBox cbx_campo_filtro;
        private Button btn_buscar;
        private TextBox txt_buscar;
        private DateTimePicker dtp_F_ini;
        private DateTimePicker dtp_F_f;
        private Label label2;
        private Label label1;
        private DataGridView dtg_aperturas_cierres_caja;
        private ErrorProvider errorProvider1;
        private DataGridViewTextBoxColumn cl_IdApertura_Cierre_caja;
        private DataGridViewTextBoxColumn cl_IdUser;
        private DataGridViewTextBoxColumn cl_usuario;
        private DataGridViewTextBoxColumn cl_fecha_apertura;
        private DataGridViewTextBoxColumn cl_monto_inicial;
        private DataGridViewTextBoxColumn cl_fecha_cierre;
        private DataGridViewTextBoxColumn cl_montoFinal;
        private DataGridViewTextBoxColumn cl_ventasTotales;
        private DataGridViewTextBoxColumn cl_pago_en_efectivo;
        private DataGridViewTextBoxColumn cl_diferencia;
        private DataGridViewTextBoxColumn cl_estado;
    }
}