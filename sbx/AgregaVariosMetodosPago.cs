using sbx.core.Entities.AgregaVenta;
using sbx.core.Interfaces.Banco;
using sbx.core.Interfaces.MedioPago;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sbx
{
    public partial class AgregaVariosMetodosPago : Form
    {
        public delegate void EnviarConfirma(bool confirmacion, List<AgregaVariosMediosPago> agregaVariosMediosPagos);
        public event EnviarConfirma EnviaConfirma;
        char decimalSeparator = ',';
        private readonly IBanco _IBanco;
        private readonly IMedioPago _IMedioPago;
        private List<dynamic> _metodosPago;

        public decimal Total { get; set; }

        public AgregaVariosMetodosPago(IBanco banco, IMedioPago medioPago)
        {
            InitializeComponent();
            _IBanco = banco;
            _IMedioPago = medioPago;
        }

        private async void AgregaVariosMetodosPago_Load(object sender, EventArgs e)
        {
            lbl_total.Text = Total.ToString("N2", new CultureInfo("es-CO"));
            lbl_faltante.Text = Total.ToString("N2", new CultureInfo("es-CO"));

            var resp = await _IBanco.List();

            //Bancolombia QR
            var lista = ((IEnumerable<dynamic>)resp.Data!);

            var filtrados = lista
                .Where(b => b.Nombre == "Bancolombia" && b.Estado)
                .ToList();

            cbx_banco_bacolombia_qr.DataSource = filtrados;
            cbx_banco_bacolombia_qr.ValueMember = "IdBanco";
            cbx_banco_bacolombia_qr.DisplayMember = "Nombre";
            cbx_banco_bacolombia_qr.SelectedIndex = 0;

            //Transferencia
            cbx_banco_transferencia.DataSource = resp.Data;
            cbx_banco_transferencia.ValueMember = "IdBanco";
            cbx_banco_transferencia.DisplayMember = "Nombre";
            cbx_banco_transferencia.SelectedIndex = 0;

            //Tarjeta Crédito
            cbx_banco_tarjeta_credito.DataSource = resp.Data;
            cbx_banco_tarjeta_credito.ValueMember = "IdBanco";
            cbx_banco_tarjeta_credito.DisplayMember = "Nombre";
            cbx_banco_tarjeta_credito.SelectedIndex = 0;

            //Tarjeta Débito
            cbx_banco_tarjeta_debito.DataSource = resp.Data;
            cbx_banco_tarjeta_debito.ValueMember = "IdBanco";
            cbx_banco_tarjeta_debito.DisplayMember = "Nombre";
            cbx_banco_tarjeta_debito.SelectedIndex = 0;

            var respMetodos = await _IMedioPago.List(0);
            if (respMetodos.Data != null)
            {
                _metodosPago = ((IEnumerable<dynamic>)respMetodos.Data).ToList();
            }
        }

        private void txt_valor_efectivo_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox txt = (TextBox)sender;

            if (e.KeyChar == (char)Keys.Enter) // o (char)13
            {
                e.Handled = true; // opcional: evita beep
                return;
            }

            if (char.IsControl(e.KeyChar))
                return;

            if (char.IsDigit(e.KeyChar))
            {
                // Si ya existe un separador decimal, validar que no haya más de 2 decimales
                int indexDecimal = txt.Text.IndexOf(decimalSeparator);
                if (indexDecimal >= 0)
                {
                    string decimales = txt.Text.Substring(indexDecimal + 1);
                    if (txt.SelectionStart > indexDecimal && decimales.Length >= 2)
                    {
                        e.Handled = true; // Bloquear si ya hay dos decimales
                        return;
                    }
                }
                return;
            }

            if (e.KeyChar == decimalSeparator && !((TextBox)sender).Text.Contains(decimalSeparator))
                return;

            e.Handled = true;
        }

        private void txt_valor_nequi_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox txt = (TextBox)sender;

            if (e.KeyChar == (char)Keys.Enter) // o (char)13
            {
                e.Handled = true; // opcional: evita beep
                return;
            }

            if (char.IsControl(e.KeyChar))
                return;

            if (char.IsDigit(e.KeyChar))
            {
                // Si ya existe un separador decimal, validar que no haya más de 2 decimales
                int indexDecimal = txt.Text.IndexOf(decimalSeparator);
                if (indexDecimal >= 0)
                {
                    string decimales = txt.Text.Substring(indexDecimal + 1);
                    if (txt.SelectionStart > indexDecimal && decimales.Length >= 2)
                    {
                        e.Handled = true; // Bloquear si ya hay dos decimales
                        return;
                    }
                }
                return;
            }

            if (e.KeyChar == decimalSeparator && !((TextBox)sender).Text.Contains(decimalSeparator))
                return;

            e.Handled = true;
        }

        private void txt_valor_davi_plata_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox txt = (TextBox)sender;

            if (e.KeyChar == (char)Keys.Enter) // o (char)13
            {
                e.Handled = true; // opcional: evita beep
                return;
            }

            if (char.IsControl(e.KeyChar))
                return;

            if (char.IsDigit(e.KeyChar))
            {
                // Si ya existe un separador decimal, validar que no haya más de 2 decimales
                int indexDecimal = txt.Text.IndexOf(decimalSeparator);
                if (indexDecimal >= 0)
                {
                    string decimales = txt.Text.Substring(indexDecimal + 1);
                    if (txt.SelectionStart > indexDecimal && decimales.Length >= 2)
                    {
                        e.Handled = true; // Bloquear si ya hay dos decimales
                        return;
                    }
                }
                return;
            }

            if (e.KeyChar == decimalSeparator && !((TextBox)sender).Text.Contains(decimalSeparator))
                return;

            e.Handled = true;
        }

        private void txt_valor_bancolombia_qr_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox txt = (TextBox)sender;

            if (e.KeyChar == (char)Keys.Enter) // o (char)13
            {
                e.Handled = true; // opcional: evita beep
                return;
            }

            if (char.IsControl(e.KeyChar))
                return;

            if (char.IsDigit(e.KeyChar))
            {
                // Si ya existe un separador decimal, validar que no haya más de 2 decimales
                int indexDecimal = txt.Text.IndexOf(decimalSeparator);
                if (indexDecimal >= 0)
                {
                    string decimales = txt.Text.Substring(indexDecimal + 1);
                    if (txt.SelectionStart > indexDecimal && decimales.Length >= 2)
                    {
                        e.Handled = true; // Bloquear si ya hay dos decimales
                        return;
                    }
                }
                return;
            }

            if (e.KeyChar == decimalSeparator && !((TextBox)sender).Text.Contains(decimalSeparator))
                return;

            e.Handled = true;
        }

        private void txt_valor_transferencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox txt = (TextBox)sender;

            if (e.KeyChar == (char)Keys.Enter) // o (char)13
            {
                e.Handled = true; // opcional: evita beep
                return;
            }

            if (char.IsControl(e.KeyChar))
                return;

            if (char.IsDigit(e.KeyChar))
            {
                // Si ya existe un separador decimal, validar que no haya más de 2 decimales
                int indexDecimal = txt.Text.IndexOf(decimalSeparator);
                if (indexDecimal >= 0)
                {
                    string decimales = txt.Text.Substring(indexDecimal + 1);
                    if (txt.SelectionStart > indexDecimal && decimales.Length >= 2)
                    {
                        e.Handled = true; // Bloquear si ya hay dos decimales
                        return;
                    }
                }
                return;
            }

            if (e.KeyChar == decimalSeparator && !((TextBox)sender).Text.Contains(decimalSeparator))
                return;

            e.Handled = true;
        }

        private void txt_valor_tarjeta_credito_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox txt = (TextBox)sender;

            if (e.KeyChar == (char)Keys.Enter) // o (char)13
            {
                e.Handled = true; // opcional: evita beep
                return;
            }

            if (char.IsControl(e.KeyChar))
                return;

            if (char.IsDigit(e.KeyChar))
            {
                // Si ya existe un separador decimal, validar que no haya más de 2 decimales
                int indexDecimal = txt.Text.IndexOf(decimalSeparator);
                if (indexDecimal >= 0)
                {
                    string decimales = txt.Text.Substring(indexDecimal + 1);
                    if (txt.SelectionStart > indexDecimal && decimales.Length >= 2)
                    {
                        e.Handled = true; // Bloquear si ya hay dos decimales
                        return;
                    }
                }
                return;
            }

            if (e.KeyChar == decimalSeparator && !((TextBox)sender).Text.Contains(decimalSeparator))
                return;

            e.Handled = true;
        }

        private void txt_valor_tarjeta_debito_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox txt = (TextBox)sender;

            if (e.KeyChar == (char)Keys.Enter) // o (char)13
            {
                e.Handled = true; // opcional: evita beep
                return;
            }

            if (char.IsControl(e.KeyChar))
                return;

            if (char.IsDigit(e.KeyChar))
            {
                // Si ya existe un separador decimal, validar que no haya más de 2 decimales
                int indexDecimal = txt.Text.IndexOf(decimalSeparator);
                if (indexDecimal >= 0)
                {
                    string decimales = txt.Text.Substring(indexDecimal + 1);
                    if (txt.SelectionStart > indexDecimal && decimales.Length >= 2)
                    {
                        e.Handled = true; // Bloquear si ya hay dos decimales
                        return;
                    }
                }
                return;
            }

            if (e.KeyChar == decimalSeparator && !((TextBox)sender).Text.Contains(decimalSeparator))
                return;

            e.Handled = true;
        }

        private void txt_valor_efectivo_KeyUp(object sender, KeyEventArgs e)
        {
            mtd_calcular();
        }

        public void mtd_calcular()
        {
            decimal pago = 0;
            decimal faltante = 0;
            lbl_mensaje.Text = "_";
            btn_completar_venta.Enabled = false;

            if (txt_valor_efectivo.Text.Trim() != "")
            {
                pago += Convert.ToDecimal(txt_valor_efectivo.Text.Trim(), new CultureInfo("es-CO"));
            }

            if (txt_valor_nequi.Text.Trim() != "")
            {
                pago += Convert.ToDecimal(txt_valor_nequi.Text.Trim(), new CultureInfo("es-CO"));
            }

            if (txt_valor_davi_plata.Text.Trim() != "")
            {
                pago += Convert.ToDecimal(txt_valor_davi_plata.Text.Trim(), new CultureInfo("es-CO"));
            }

            if (txt_valor_bancolombia_qr.Text.Trim() != "")
            {
                pago += Convert.ToDecimal(txt_valor_bancolombia_qr.Text.Trim(), new CultureInfo("es-CO"));
            }

            if (txt_valor_transferencia.Text.Trim() != "")
            {
                pago += Convert.ToDecimal(txt_valor_transferencia.Text.Trim(), new CultureInfo("es-CO"));
            }

            if (txt_valor_tarjeta_credito.Text.Trim() != "")
            {
                pago += Convert.ToDecimal(txt_valor_tarjeta_credito.Text.Trim(), new CultureInfo("es-CO"));
            }

            if (txt_valor_tarjeta_debito.Text.Trim() != "")
            {
                pago += Convert.ToDecimal(txt_valor_tarjeta_debito.Text.Trim(), new CultureInfo("es-CO"));
            }

            faltante = Convert.ToDecimal(lbl_total.Text.Trim(), new CultureInfo("es-CO")) - pago;

            if (faltante < 0)
            {
                lbl_faltante.ForeColor = Color.Black;
                lbl_mensaje.Text = "Valor faltante NO puede ser menor a cero";
                lbl_mensaje.ForeColor = Color.Red;
            }
            else if (faltante > 0)
            {
                lbl_faltante.ForeColor = Color.Red;
                lbl_mensaje.Text = "Monto faltante para completar el total";
                lbl_mensaje.ForeColor = Color.Red;
            }
            else
            {
                lbl_faltante.ForeColor = Color.SeaGreen;
                lbl_mensaje.Text = "Monto cubierto en su totalidad";
                lbl_mensaje.ForeColor = Color.SeaGreen;
                btn_completar_venta.Enabled = true;
            }

            lbl_faltante.Text = faltante.ToString("N2", new CultureInfo("es-CO"));
        }

        private void txt_valor_nequi_KeyUp(object sender, KeyEventArgs e)
        {
            mtd_calcular();
        }

        private void txt_valor_davi_plata_KeyUp(object sender, KeyEventArgs e)
        {
            mtd_calcular();
        }

        private void txt_valor_bancolombia_qr_KeyUp(object sender, KeyEventArgs e)
        {
            mtd_calcular();
        }

        private void txt_valor_transferencia_KeyUp(object sender, KeyEventArgs e)
        {
            mtd_calcular();
        }

        private void txt_valor_tarjeta_credito_KeyUp(object sender, KeyEventArgs e)
        {
            mtd_calcular();
        }

        private void txt_valor_tarjeta_debito_KeyUp(object sender, KeyEventArgs e)
        {
            mtd_calcular();
        }

        private void btn_completar_venta_Click(object sender, EventArgs e)
        {
            var culture = new CultureInfo("es-CO");
            var lista = new List<AgregaVariosMediosPago>();

            decimal GetValor(TextBox txt)
            {
                if (txt.Text.Trim() == "") return 0;
                return Convert.ToDecimal(txt.Text.Trim(), culture);
            }

            int GetIdMetodoPago(string nombre)
            {
                var metodo = _metodosPago?.FirstOrDefault(m => m.Nombre == nombre);
                return metodo != null ? (int)metodo.IdMetodoPago : 0;
            }

            var efectivo = GetValor(txt_valor_efectivo);
            if (efectivo > 0)
            {
                lista.Add(new AgregaVariosMediosPago
                {
                    IdMetodoPago = GetIdMetodoPago(txt_medio_pago_efectivo.Text),
                    valor = efectivo,
                    Referencia = "",
                    IdBanco = 1
                });
            }

            var nequi = GetValor(txt_valor_nequi);
            if (nequi > 0)
            {
                lista.Add(new AgregaVariosMediosPago
                {
                    IdMetodoPago = GetIdMetodoPago(txt_medio_pago_nequi.Text),
                    valor = nequi,
                    Referencia = txt_ref_pago_nequi.Text,
                    IdBanco = 1
                });
            }

            var daviPlata = GetValor(txt_valor_davi_plata);
            if (daviPlata > 0)
            {
                lista.Add(new AgregaVariosMediosPago
                {
                    IdMetodoPago = GetIdMetodoPago(txt_medio_pago_davi_plata.Text),
                    valor = daviPlata,
                    Referencia = txt_ref_pago_davi_plata.Text,
                    IdBanco = 1
                });
            }

            var bancolombiaQr = GetValor(txt_valor_bancolombia_qr);
            if (bancolombiaQr > 0)
            {
                lista.Add(new AgregaVariosMediosPago
                {
                    IdMetodoPago = GetIdMetodoPago(txt_medio_pago_bancolombia_qr.Text),
                    valor = bancolombiaQr,
                    Referencia = txt_ref_pago_bancolombia_qr.Text,
                    IdBanco = Convert.ToInt32(cbx_banco_bacolombia_qr.SelectedValue)
                });
            }

            var transferencia = GetValor(txt_valor_transferencia);
            if (transferencia > 0)
            {
                lista.Add(new AgregaVariosMediosPago
                {
                    IdMetodoPago = GetIdMetodoPago(txt_medio_pago_transferencia.Text),
                    valor = transferencia,
                    Referencia = txt_ref_pago_transferencia.Text,
                    IdBanco = Convert.ToInt32(cbx_banco_transferencia.SelectedValue)
                });
            }

            var tarjetaCredito = GetValor(txt_valor_tarjeta_credito);
            if (tarjetaCredito > 0)
            {
                lista.Add(new AgregaVariosMediosPago
                {
                    IdMetodoPago = GetIdMetodoPago(txt_medio_pago_tarjeta_credito.Text),
                    valor = tarjetaCredito,
                    Referencia = txt_ref_pago_tarjeta_credito.Text,
                    IdBanco = Convert.ToInt32(cbx_banco_tarjeta_credito.SelectedValue)
                });
            }

            var tarjetaDebito = GetValor(txt_valor_tarjeta_debito);
            if (tarjetaDebito > 0)
            {
                lista.Add(new AgregaVariosMediosPago
                {
                    IdMetodoPago = GetIdMetodoPago(txt_medio_pago_tarjeta_debito.Text),
                    valor = tarjetaDebito,
                    Referencia = txt_ref_pago_tarjeta_debito.Text,
                    IdBanco = Convert.ToInt32(cbx_banco_tarjeta_debito.SelectedValue)
                });
            }

            EnviaConfirma(true, lista);
            this.Close();
        }
    }
}
