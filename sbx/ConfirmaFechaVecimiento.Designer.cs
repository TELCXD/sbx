namespace sbx
{
    partial class ConfirmaFechaVecimiento
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
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            dtg_fecha_vence = new DataGridView();
            cl_idProducto = new DataGridViewTextBoxColumn();
            cl_sku = new DataGridViewTextBoxColumn();
            cl_codigoBarras = new DataGridViewTextBoxColumn();
            cl_nombre = new DataGridViewTextBoxColumn();
            cl_fecha_vencimiento = new DataGridViewTextBoxColumn();
            cl_stock = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dtg_fecha_vence).BeginInit();
            SuspendLayout();
            // 
            // dtg_fecha_vence
            // 
            dtg_fecha_vence.AllowUserToAddRows = false;
            dtg_fecha_vence.AllowUserToDeleteRows = false;
            dtg_fecha_vence.AllowUserToOrderColumns = true;
            dtg_fecha_vence.BackgroundColor = SystemColors.Control;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = SystemColors.Control;
            dataGridViewCellStyle4.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.ScrollBar;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            dtg_fecha_vence.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dtg_fecha_vence.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dtg_fecha_vence.Columns.AddRange(new DataGridViewColumn[] { cl_idProducto, cl_sku, cl_codigoBarras, cl_nombre, cl_fecha_vencimiento, cl_stock });
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = SystemColors.Window;
            dataGridViewCellStyle5.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle5.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = Color.FromArgb(172, 211, 236);
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.ControlText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.False;
            dtg_fecha_vence.DefaultCellStyle = dataGridViewCellStyle5;
            dtg_fecha_vence.Dock = DockStyle.Fill;
            dtg_fecha_vence.Location = new Point(0, 0);
            dtg_fecha_vence.Name = "dtg_fecha_vence";
            dtg_fecha_vence.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = SystemColors.Control;
            dataGridViewCellStyle6.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle6.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = Color.Silver;
            dataGridViewCellStyle6.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.True;
            dtg_fecha_vence.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            dtg_fecha_vence.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtg_fecha_vence.Size = new Size(866, 327);
            dtg_fecha_vence.TabIndex = 7;
            dtg_fecha_vence.DoubleClick += dtg_fecha_vence_DoubleClick;
            // 
            // cl_idProducto
            // 
            cl_idProducto.HeaderText = "Id";
            cl_idProducto.Name = "cl_idProducto";
            cl_idProducto.ReadOnly = true;
            cl_idProducto.Width = 70;
            // 
            // cl_sku
            // 
            cl_sku.HeaderText = "Sku";
            cl_sku.Name = "cl_sku";
            cl_sku.ReadOnly = true;
            // 
            // cl_codigoBarras
            // 
            cl_codigoBarras.HeaderText = "Codigo b.";
            cl_codigoBarras.Name = "cl_codigoBarras";
            cl_codigoBarras.ReadOnly = true;
            // 
            // cl_nombre
            // 
            cl_nombre.HeaderText = "Nombre";
            cl_nombre.Name = "cl_nombre";
            cl_nombre.ReadOnly = true;
            cl_nombre.Width = 250;
            // 
            // cl_fecha_vencimiento
            // 
            cl_fecha_vencimiento.HeaderText = "Fecha vencimiento";
            cl_fecha_vencimiento.Name = "cl_fecha_vencimiento";
            cl_fecha_vencimiento.ReadOnly = true;
            cl_fecha_vencimiento.Width = 200;
            // 
            // cl_stock
            // 
            cl_stock.HeaderText = "Stock";
            cl_stock.Name = "cl_stock";
            cl_stock.ReadOnly = true;
            // 
            // ConfirmaFechaVecimiento
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(866, 327);
            Controls.Add(dtg_fecha_vence);
            MaximizeBox = false;
            MaximumSize = new Size(882, 366);
            MinimizeBox = false;
            MinimumSize = new Size(882, 366);
            Name = "ConfirmaFechaVecimiento";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ConfirmaFechaVecimiento";
            FormClosing += ConfirmaFechaVecimiento_FormClosing;
            Load += ConfirmaFechaVecimiento_Load;
            ((System.ComponentModel.ISupportInitialize)dtg_fecha_vence).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dtg_fecha_vence;
        private DataGridViewTextBoxColumn cl_idProducto;
        private DataGridViewTextBoxColumn cl_sku;
        private DataGridViewTextBoxColumn cl_codigoBarras;
        private DataGridViewTextBoxColumn cl_nombre;
        private DataGridViewTextBoxColumn cl_fecha_vencimiento;
        private DataGridViewTextBoxColumn cl_stock;
    }
}