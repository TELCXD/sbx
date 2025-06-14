namespace sbx
{
    partial class VentasSuspendidas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VentasSuspendidas));
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            panel1 = new Panel();
            btn_eliminar = new Button();
            btn_reactivar = new Button();
            dtg_ventas_suspendidas = new DataGridView();
            cl_idVenta_suspendida = new DataGridViewTextBoxColumn();
            cl_numeracion = new DataGridViewTextBoxColumn();
            cl_fecha = new DataGridViewTextBoxColumn();
            cl_cliente = new DataGridViewTextBoxColumn();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtg_ventas_suspendidas).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.Window;
            panel1.BorderStyle = BorderStyle.Fixed3D;
            panel1.Controls.Add(btn_eliminar);
            panel1.Controls.Add(btn_reactivar);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(584, 60);
            panel1.TabIndex = 0;
            // 
            // btn_eliminar
            // 
            btn_eliminar.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_eliminar.FlatStyle = FlatStyle.Flat;
            btn_eliminar.Image = (Image)resources.GetObject("btn_eliminar.Image");
            btn_eliminar.Location = new Point(110, 6);
            btn_eliminar.Name = "btn_eliminar";
            btn_eliminar.Size = new Size(101, 45);
            btn_eliminar.TabIndex = 5;
            btn_eliminar.Text = "Eliminar";
            btn_eliminar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_eliminar.UseVisualStyleBackColor = true;
            btn_eliminar.Click += btn_eliminar_Click;
            // 
            // btn_reactivar
            // 
            btn_reactivar.FlatAppearance.MouseDownBackColor = Color.Gray;
            btn_reactivar.FlatStyle = FlatStyle.Flat;
            btn_reactivar.Image = (Image)resources.GetObject("btn_reactivar.Image");
            btn_reactivar.Location = new Point(3, 6);
            btn_reactivar.Name = "btn_reactivar";
            btn_reactivar.Size = new Size(101, 45);
            btn_reactivar.TabIndex = 1;
            btn_reactivar.Text = "Reactivar";
            btn_reactivar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_reactivar.UseVisualStyleBackColor = true;
            btn_reactivar.Click += btn_reactivar_Click;
            // 
            // dtg_ventas_suspendidas
            // 
            dtg_ventas_suspendidas.AllowUserToAddRows = false;
            dtg_ventas_suspendidas.AllowUserToDeleteRows = false;
            dtg_ventas_suspendidas.AllowUserToOrderColumns = true;
            dtg_ventas_suspendidas.BackgroundColor = SystemColors.Control;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.ScrollBar;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dtg_ventas_suspendidas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dtg_ventas_suspendidas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dtg_ventas_suspendidas.Columns.AddRange(new DataGridViewColumn[] { cl_idVenta_suspendida, cl_numeracion, cl_fecha, cl_cliente });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(172, 211, 236);
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dtg_ventas_suspendidas.DefaultCellStyle = dataGridViewCellStyle2;
            dtg_ventas_suspendidas.Dock = DockStyle.Fill;
            dtg_ventas_suspendidas.Location = new Point(0, 60);
            dtg_ventas_suspendidas.Name = "dtg_ventas_suspendidas";
            dtg_ventas_suspendidas.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = Color.Silver;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dtg_ventas_suspendidas.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dtg_ventas_suspendidas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtg_ventas_suspendidas.Size = new Size(584, 390);
            dtg_ventas_suspendidas.TabIndex = 7;
            dtg_ventas_suspendidas.DoubleClick += dtg_ventas_suspendidas_DoubleClick;
            // 
            // cl_idVenta_suspendida
            // 
            cl_idVenta_suspendida.HeaderText = "Id Venta suspendida";
            cl_idVenta_suspendida.Name = "cl_idVenta_suspendida";
            cl_idVenta_suspendida.ReadOnly = true;
            cl_idVenta_suspendida.Visible = false;
            // 
            // cl_numeracion
            // 
            cl_numeracion.HeaderText = "#";
            cl_numeracion.Name = "cl_numeracion";
            cl_numeracion.ReadOnly = true;
            cl_numeracion.Width = 40;
            // 
            // cl_fecha
            // 
            cl_fecha.HeaderText = "Fecha";
            cl_fecha.Name = "cl_fecha";
            cl_fecha.ReadOnly = true;
            cl_fecha.Width = 200;
            // 
            // cl_cliente
            // 
            cl_cliente.HeaderText = "Cliente";
            cl_cliente.Name = "cl_cliente";
            cl_cliente.ReadOnly = true;
            cl_cliente.Width = 300;
            // 
            // VentasSuspendidas
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(584, 450);
            Controls.Add(dtg_ventas_suspendidas);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MaximumSize = new Size(600, 489);
            MinimumSize = new Size(600, 489);
            Name = "VentasSuspendidas";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "VentasSuspendidas";
            Load += VentasSuspendidas_Load;
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dtg_ventas_suspendidas).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private DataGridView dtg_ventas_suspendidas;
        private Button btn_reactivar;
        private DataGridViewTextBoxColumn cl_idVenta_suspendida;
        private DataGridViewTextBoxColumn cl_numeracion;
        private DataGridViewTextBoxColumn cl_fecha;
        private DataGridViewTextBoxColumn cl_cliente;
        private Button btn_eliminar;
    }
}