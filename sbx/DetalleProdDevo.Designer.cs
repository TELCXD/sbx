namespace sbx
{
    partial class DetalleProdDevo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DetalleProdDevo));
            dtg_detalle_nota_credito = new DataGridView();
            cl_idProducto = new DataGridViewTextBoxColumn();
            cl_sku = new DataGridViewTextBoxColumn();
            cl_codigo_barras = new DataGridViewTextBoxColumn();
            cl_nombre = new DataGridViewTextBoxColumn();
            cl_precio = new DataGridViewTextBoxColumn();
            cl_cantidad = new DataGridViewTextBoxColumn();
            cl_descuento = new DataGridViewTextBoxColumn();
            cl_iva = new DataGridViewTextBoxColumn();
            cl_total = new DataGridViewTextBoxColumn();
            lbl_cantidad_devolucion = new Label();
            lbl_total_devolucion = new Label();
            label17 = new Label();
            label16 = new Label();
            txt_motivo_devolucion = new TextBox();
            label14 = new Label();
            lbl_nota_credito = new Label();
            label15 = new Label();
            panel1 = new Panel();
            btn_ver_factura = new Button();
            lbl_factura = new Label();
            label3 = new Label();
            lbl_usuario = new Label();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)dtg_detalle_nota_credito).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // dtg_detalle_nota_credito
            // 
            dtg_detalle_nota_credito.AllowUserToAddRows = false;
            dtg_detalle_nota_credito.AllowUserToDeleteRows = false;
            dtg_detalle_nota_credito.AllowUserToOrderColumns = true;
            dtg_detalle_nota_credito.BackgroundColor = SystemColors.Control;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.ScrollBar;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dtg_detalle_nota_credito.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dtg_detalle_nota_credito.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dtg_detalle_nota_credito.Columns.AddRange(new DataGridViewColumn[] { cl_idProducto, cl_sku, cl_codigo_barras, cl_nombre, cl_precio, cl_cantidad, cl_descuento, cl_iva, cl_total });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(172, 211, 236);
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dtg_detalle_nota_credito.DefaultCellStyle = dataGridViewCellStyle2;
            dtg_detalle_nota_credito.Location = new Point(2, 145);
            dtg_detalle_nota_credito.Name = "dtg_detalle_nota_credito";
            dtg_detalle_nota_credito.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = Color.Silver;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dtg_detalle_nota_credito.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dtg_detalle_nota_credito.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtg_detalle_nota_credito.Size = new Size(1104, 253);
            dtg_detalle_nota_credito.TabIndex = 10;
            // 
            // cl_idProducto
            // 
            cl_idProducto.HeaderText = "Id";
            cl_idProducto.Name = "cl_idProducto";
            cl_idProducto.ReadOnly = true;
            cl_idProducto.Width = 50;
            // 
            // cl_sku
            // 
            cl_sku.HeaderText = "sku";
            cl_sku.Name = "cl_sku";
            cl_sku.ReadOnly = true;
            // 
            // cl_codigo_barras
            // 
            cl_codigo_barras.HeaderText = "Codigo b";
            cl_codigo_barras.Name = "cl_codigo_barras";
            cl_codigo_barras.ReadOnly = true;
            cl_codigo_barras.Width = 150;
            // 
            // cl_nombre
            // 
            cl_nombre.HeaderText = "Nombre";
            cl_nombre.Name = "cl_nombre";
            cl_nombre.ReadOnly = true;
            cl_nombre.Width = 220;
            // 
            // cl_precio
            // 
            cl_precio.HeaderText = "Precio";
            cl_precio.Name = "cl_precio";
            cl_precio.ReadOnly = true;
            cl_precio.Width = 150;
            // 
            // cl_cantidad
            // 
            cl_cantidad.HeaderText = "Cantidad";
            cl_cantidad.Name = "cl_cantidad";
            cl_cantidad.ReadOnly = true;
            // 
            // cl_descuento
            // 
            cl_descuento.HeaderText = "Desc %";
            cl_descuento.Name = "cl_descuento";
            cl_descuento.ReadOnly = true;
            cl_descuento.Width = 80;
            // 
            // cl_iva
            // 
            cl_iva.HeaderText = "Iva %";
            cl_iva.Name = "cl_iva";
            cl_iva.ReadOnly = true;
            cl_iva.Width = 67;
            // 
            // cl_total
            // 
            cl_total.HeaderText = "Total";
            cl_total.Name = "cl_total";
            cl_total.ReadOnly = true;
            cl_total.Width = 142;
            // 
            // lbl_cantidad_devolucion
            // 
            lbl_cantidad_devolucion.AutoSize = true;
            lbl_cantidad_devolucion.Font = new Font("Segoe UI", 9.75F);
            lbl_cantidad_devolucion.ForeColor = SystemColors.InfoText;
            lbl_cantidad_devolucion.Location = new Point(147, 85);
            lbl_cantidad_devolucion.Name = "lbl_cantidad_devolucion";
            lbl_cantidad_devolucion.RightToLeft = RightToLeft.No;
            lbl_cantidad_devolucion.Size = new Size(13, 17);
            lbl_cantidad_devolucion.TabIndex = 178;
            lbl_cantidad_devolucion.Text = "_";
            // 
            // lbl_total_devolucion
            // 
            lbl_total_devolucion.AutoSize = true;
            lbl_total_devolucion.Font = new Font("Segoe UI", 9.75F);
            lbl_total_devolucion.ForeColor = SystemColors.InfoText;
            lbl_total_devolucion.Location = new Point(147, 61);
            lbl_total_devolucion.Name = "lbl_total_devolucion";
            lbl_total_devolucion.RightToLeft = RightToLeft.No;
            lbl_total_devolucion.Size = new Size(13, 17);
            lbl_total_devolucion.TabIndex = 176;
            lbl_total_devolucion.Text = "_";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Font = new Font("Segoe UI", 9.75F);
            label17.ForeColor = SystemColors.InfoText;
            label17.Location = new Point(8, 85);
            label17.Name = "label17";
            label17.Size = new Size(134, 17);
            label17.TabIndex = 177;
            label17.Text = "Cantidad devolucion: ";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new Font("Segoe UI", 9.75F);
            label16.ForeColor = SystemColors.InfoText;
            label16.Location = new Point(8, 61);
            label16.Name = "label16";
            label16.Size = new Size(110, 17);
            label16.TabIndex = 175;
            label16.Text = "Total devolucion: ";
            // 
            // txt_motivo_devolucion
            // 
            txt_motivo_devolucion.Anchor = AnchorStyles.Top;
            txt_motivo_devolucion.Enabled = false;
            txt_motivo_devolucion.Location = new Point(144, 104);
            txt_motivo_devolucion.Name = "txt_motivo_devolucion";
            txt_motivo_devolucion.Size = new Size(953, 23);
            txt_motivo_devolucion.TabIndex = 173;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label14.Location = new Point(5, 107);
            label14.Name = "label14";
            label14.Size = new Size(119, 17);
            label14.TabIndex = 174;
            label14.Text = "Motivo devolucion:";
            // 
            // lbl_nota_credito
            // 
            lbl_nota_credito.AutoSize = true;
            lbl_nota_credito.Font = new Font("Segoe UI", 9.75F);
            lbl_nota_credito.ForeColor = SystemColors.InfoText;
            lbl_nota_credito.Location = new Point(147, 11);
            lbl_nota_credito.Name = "lbl_nota_credito";
            lbl_nota_credito.RightToLeft = RightToLeft.No;
            lbl_nota_credito.Size = new Size(13, 17);
            lbl_nota_credito.TabIndex = 182;
            lbl_nota_credito.Text = "_";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("Segoe UI", 9.75F);
            label15.ForeColor = SystemColors.InfoText;
            label15.Location = new Point(8, 11);
            label15.Name = "label15";
            label15.Size = new Size(89, 17);
            label15.TabIndex = 181;
            label15.Text = "Nota credito: ";
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(btn_ver_factura);
            panel1.Controls.Add(lbl_factura);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(lbl_usuario);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(txt_motivo_devolucion);
            panel1.Controls.Add(label14);
            panel1.Location = new Point(2, 5);
            panel1.Name = "panel1";
            panel1.Size = new Size(1104, 134);
            panel1.TabIndex = 183;
            // 
            // btn_ver_factura
            // 
            btn_ver_factura.Enabled = false;
            btn_ver_factura.FlatAppearance.BorderSize = 0;
            btn_ver_factura.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_ver_factura.FlatStyle = FlatStyle.Flat;
            btn_ver_factura.Image = (Image)resources.GetObject("btn_ver_factura.Image");
            btn_ver_factura.Location = new Point(360, 3);
            btn_ver_factura.Name = "btn_ver_factura";
            btn_ver_factura.Size = new Size(26, 26);
            btn_ver_factura.TabIndex = 187;
            btn_ver_factura.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_ver_factura.UseVisualStyleBackColor = true;
            btn_ver_factura.Click += btn_ver_factura_Click;
            // 
            // lbl_factura
            // 
            lbl_factura.AutoSize = true;
            lbl_factura.Font = new Font("Segoe UI", 9.75F);
            lbl_factura.ForeColor = SystemColors.InfoText;
            lbl_factura.Location = new Point(392, 5);
            lbl_factura.Name = "lbl_factura";
            lbl_factura.RightToLeft = RightToLeft.No;
            lbl_factura.Size = new Size(13, 17);
            lbl_factura.TabIndex = 186;
            lbl_factura.Text = "_";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9.75F);
            label3.ForeColor = SystemColors.InfoText;
            label3.Location = new Point(312, 5);
            label3.Name = "label3";
            label3.Size = new Size(57, 17);
            label3.TabIndex = 185;
            label3.Text = "Factura: ";
            // 
            // lbl_usuario
            // 
            lbl_usuario.AutoSize = true;
            lbl_usuario.Font = new Font("Segoe UI", 9.75F);
            lbl_usuario.ForeColor = SystemColors.InfoText;
            lbl_usuario.Location = new Point(144, 30);
            lbl_usuario.Name = "lbl_usuario";
            lbl_usuario.RightToLeft = RightToLeft.No;
            lbl_usuario.Size = new Size(13, 17);
            lbl_usuario.TabIndex = 184;
            lbl_usuario.Text = "_";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9.75F);
            label2.ForeColor = SystemColors.InfoText;
            label2.Location = new Point(5, 30);
            label2.Name = "label2";
            label2.Size = new Size(127, 17);
            label2.TabIndex = 183;
            label2.Text = "Usuario devolucion: ";
            // 
            // DetalleProdDevo
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            ClientSize = new Size(1112, 401);
            Controls.Add(lbl_nota_credito);
            Controls.Add(label15);
            Controls.Add(lbl_cantidad_devolucion);
            Controls.Add(lbl_total_devolucion);
            Controls.Add(label17);
            Controls.Add(label16);
            Controls.Add(dtg_detalle_nota_credito);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "DetalleProdDevo";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "DetalleProdDevo";
            Load += DetalleProdDevo_Load;
            ((System.ComponentModel.ISupportInitialize)dtg_detalle_nota_credito).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dtg_detalle_nota_credito;
        private DataGridViewTextBoxColumn cl_idProducto;
        private DataGridViewTextBoxColumn cl_sku;
        private DataGridViewTextBoxColumn cl_codigo_barras;
        private DataGridViewTextBoxColumn cl_nombre;
        private DataGridViewTextBoxColumn cl_precio;
        private DataGridViewTextBoxColumn cl_cantidad;
        private DataGridViewTextBoxColumn cl_descuento;
        private DataGridViewTextBoxColumn cl_iva;
        private DataGridViewTextBoxColumn cl_total;
        private Label lbl_cantidad_devolucion;
        private Label lbl_total_devolucion;
        private Label label17;
        private Label label16;
        private TextBox txt_motivo_devolucion;
        private Label label14;
        private Label lbl_nota_credito;
        private Label label15;
        private Panel panel1;
        private Label lbl_usuario;
        private Label label2;
        private Button btn_ver_factura;
        private Label lbl_factura;
        private Label label3;
    }
}