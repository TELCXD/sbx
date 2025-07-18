﻿using DocumentFormat.OpenXml.Spreadsheet;
using sbx.core.Entities.Caja;
using sbx.core.Entities.Venta;
using sbx.core.Helper.Impresion;
using sbx.core.Interfaces.Caja;
using sbx.core.Interfaces.Pago;
using sbx.core.Interfaces.Parametros;
using sbx.core.Interfaces.Venta;
using System.Globalization;
using System.Text;

namespace sbx
{
    public partial class AddCierre : Form
    {
        private readonly ICaja _ICaja;
        char decimalSeparator = ',';
        private dynamic? _Permisos;
        private readonly IServiceProvider _serviceProvider;
        private readonly IVenta _IVenta;
        private readonly IPagosEfectivo _IPagosEfectivo;
        private readonly IParametros _IParametros;

        public AddCierre(ICaja caja, IServiceProvider serviceProvider, IVenta venta, IPagosEfectivo pagosEfectivo, IParametros iParametros)
        {
            InitializeComponent();
            _ICaja = caja;
            _serviceProvider = serviceProvider;
            _IVenta = venta;
            _IPagosEfectivo = pagosEfectivo;
            _IParametros = iParametros;
        }

        public dynamic? Permisos
        {
            get => _Permisos;
            set => _Permisos = value;
        }

        private void AddCierre_Load(object sender, EventArgs e)
        {
            ValidaPermisos();
        }

        private void ValidaPermisos()
        {
            if (_Permisos != null)
            {
                foreach (var item in _Permisos)
                {
                    switch (item.MenuUrl)
                    {
                        case "caja":
                            btn_cierre.Enabled = item.ToCreate == 1 ? true : false;
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {
                MessageBox.Show("No hay informacion de permisos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txt_monto_final_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
                return;

            if (char.IsDigit(e.KeyChar))
                return;

            if (e.KeyChar == decimalSeparator && !((TextBox)sender).Text.Contains(decimalSeparator))
                return;

            e.Handled = true;
        }

        private async void btn_cierre_Click(object sender, EventArgs e)
        {
            if (txt_monto_final.Text.Trim() != "")
            {
                var estadoCaja = await _ICaja.EstadoCaja(Convert.ToInt32(_Permisos?[0]?.IdUser));
                if (estadoCaja.Data != null)
                {
                    if (estadoCaja.Data.Count > 0)
                    {
                        if (estadoCaja.Data[0].Estado == "CERRADA") 
                        {
                            MessageBox.Show("Caja ya esta cerrada", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            var DataVentas = await _IVenta.VentasTotales(Convert.ToInt32(_Permisos?[0]?.IdUser), Convert.ToDateTime(estadoCaja.Data[0].FechaHoraApertura));
                            if (DataVentas.Data != null)
                            {
                                decimal MontoInicial;
                                CajaEntitie Cierre;

                                if (DataVentas.Data.Count > 0)
                                {
                                    decimal Subtotal = 0;
                                    decimal cantidadTotal = 0;
                                    decimal DescuentoLinea;
                                    decimal Descuento = 0;
                                    decimal Impuesto = 0;
                                    decimal ImpuestoLinea;
                                    decimal SubtotalLinea;
                                    decimal Total = 0;
                                    decimal diferencia;                                   

                                    foreach (var item in DataVentas.Data)
                                    {
                                        cantidadTotal += Convert.ToDecimal(item.Cantidad);
                                        Subtotal += Convert.ToDecimal(item.PrecioUnitario) * Convert.ToDecimal(item.Cantidad);
                                        SubtotalLinea = Convert.ToDecimal(item.PrecioUnitario) * Convert.ToDecimal(item.Cantidad);
                                        Descuento += CalcularDescuento(SubtotalLinea, Convert.ToDecimal(item.Descuento));
                                        DescuentoLinea = CalcularDescuento(SubtotalLinea, Convert.ToDecimal(item.Descuento));
                                        Impuesto += CalcularIva(SubtotalLinea - DescuentoLinea, Convert.ToDecimal(item.Impuesto));
                                        ImpuestoLinea = CalcularIva(SubtotalLinea - DescuentoLinea, Convert.ToDecimal(item.Impuesto));
                                    }

                                    MontoInicial = Convert.ToDecimal(estadoCaja.Data[0].MontoInicialDeclarado, new CultureInfo("es-CO"));
                                    Total += (Subtotal - Descuento) + Impuesto;

                                    decimal Valorpagos = 0;

                                    var respPagosEfectivo = await _IPagosEfectivo.List(Convert.ToInt32(_Permisos?[0]?.IdUser), Convert.ToDateTime(estadoCaja.Data[0].FechaHoraApertura));
                                    if (respPagosEfectivo.Data != null)
                                    {
                                        if (respPagosEfectivo.Data.Count > 0)
                                        {
                                            foreach (var pago in respPagosEfectivo.Data)
                                            {
                                                Valorpagos += Convert.ToDecimal(pago.ValorPago);
                                            }                                          
                                        }
                                    }

                                    diferencia = (Total + MontoInicial) - Convert.ToDecimal(txt_monto_final.Text, new CultureInfo("es-CO"));

                                    diferencia = diferencia - Valorpagos;

                                    Cierre = new CajaEntitie
                                    {
                                        IdApertura_Cierre_caja = Convert.ToInt32(estadoCaja.Data[0].IdApertura_Cierre_caja),
                                        MontoFinalDeclarado = Convert.ToDecimal(txt_monto_final.Text, new CultureInfo("es-CO")),
                                        IdUserAction = Convert.ToInt32(_Permisos?[0]?.IdUser),
                                        VentasTotales = Total,
                                        PagosEnEfectivo = Valorpagos,
                                        Diferencia = diferencia,
                                        Estado = "CERRADA"
                                    };
                                }
                                else
                                {
                                    MontoInicial = Convert.ToDecimal(estadoCaja.Data[0].MontoInicialDeclarado, new CultureInfo("es-CO"));

                                    decimal Valorpagos = 0;

                                    var respPagosEfectivo = await _IPagosEfectivo.List(Convert.ToInt32(_Permisos?[0]?.IdUser), Convert.ToDateTime(estadoCaja.Data[0].FechaHoraApertura));
                                    if (respPagosEfectivo.Data != null)
                                    {
                                        if (respPagosEfectivo.Data.Count > 0)
                                        {
                                            foreach (var pago in respPagosEfectivo.Data)
                                            {
                                                Valorpagos += Convert.ToDecimal(pago.ValorPago);
                                            }
                                        }
                                    }

                                    Cierre = new CajaEntitie
                                    {
                                        IdApertura_Cierre_caja = Convert.ToInt32(estadoCaja.Data[0].IdApertura_Cierre_caja),
                                        MontoFinalDeclarado = Convert.ToDecimal(txt_monto_final.Text, new CultureInfo("es-CO")),
                                        IdUserAction = Convert.ToInt32(_Permisos?[0]?.IdUser),
                                        VentasTotales = 0,
                                        PagosEnEfectivo = Valorpagos,
                                        Diferencia = MontoInicial - Convert.ToDecimal(txt_monto_final.Text, new CultureInfo("es-CO")) - Valorpagos,
                                        Estado = "CERRADA"
                                    };
                                }

                                var resp = await _ICaja.CreateUpdate(Cierre);

                                if (resp != null)
                                {
                                    if (resp.Flag == true)
                                    {
                                        MessageBox.Show(resp.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                        ImprimirCierre(Cierre.IdApertura_Cierre_caja);

                                        this.Close();
                                    }
                                    else
                                    {
                                        MessageBox.Show(resp.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("NO se obtuvo respuesta de ventas", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("NO se obtuvo informacion de cajas", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("NO se obtuvo informacion de cajas", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Debe ingresar monto final", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private decimal CalcularIva(decimal valorBase, decimal porcentajeIva)
        {
            decimal ValorIva = 0;

            if (valorBase >= 0 && porcentajeIva >= 0)
            {
                ValorIva = Math.Round(valorBase * (porcentajeIva / 100m), 2);
            }

            return ValorIva;
        }

        private decimal CalcularDescuento(decimal valorBase, decimal porcentajeDescuento)
        {
            decimal ValorDescuento = 0;

            if (valorBase >= 0 && porcentajeDescuento >= 0)
            {
                ValorDescuento = Math.Round(valorBase * (porcentajeDescuento / 100m), 2);
            }

            return ValorDescuento;
        }

        private async void ImprimirCierre(int Id_Cierre_Apertura)
        {
            var DataCaja = await _ICaja.ListForId(Id_Cierre_Apertura);

            if (DataCaja.Data != null)
            {
                if (DataCaja.Data.Count > 0)
                {
                    CajaEntitie cajaEntitie = new CajaEntitie();

                    cajaEntitie.IdApertura_Cierre_caja = DataCaja.Data[0].IdApertura_Cierre_caja;
                    cajaEntitie.Usuario = DataCaja.Data[0].Usuario;
                    cajaEntitie.FechaHoraApertura = DataCaja.Data[0].FechaHoraApertura;
                    cajaEntitie.FechaHoraCierre = DataCaja.Data[0].FechaHoraCierre;
                    cajaEntitie.MontoInicialDeclarado = DataCaja.Data[0].MontoInicialDeclarado;
                    cajaEntitie.VentasTotales = DataCaja.Data[0].VentasTotales;
                    cajaEntitie.PagosEnEfectivo = DataCaja.Data[0].PagosEnEfectivo;
                    cajaEntitie.MontoFinalDeclarado = DataCaja.Data[0].MontoFinalDeclarado;
                    cajaEntitie.Diferencia = DataCaja.Data[0].Diferencia;
                    cajaEntitie.Estado = DataCaja.Data[0].Estado;

                    var DataParametros = await _IParametros.List("");

                    if (DataParametros.Data != null)
                    {
                        if (DataParametros.Data.Count > 0)
                        {
                            int ANCHO_TIRILLA = 0;
                            string Impresora = "";
                            int LineasAbajo = 0;

                            foreach (var itemParametros in DataParametros.Data)
                            {
                                switch (itemParametros.Nombre)
                                {
                                    case "Ancho tirilla":
                                        ANCHO_TIRILLA = Convert.ToInt32(itemParametros.Value);
                                        break;
                                    case "Impresora":
                                        Impresora = itemParametros.Value;
                                        break;
                                    case "lineas abajo de la tirilla":
                                        LineasAbajo = Convert.ToInt32(itemParametros.Value);
                                        break;
                                    default:
                                        break;
                                }
                            }

                            StringBuilder tirilla = GenerarTirillaPOS.GenerarTirillaCajaCierre(cajaEntitie, ANCHO_TIRILLA);

                            string carpetaCaja = "Caja";
                            if (!Directory.Exists(carpetaCaja))
                            {
                                Directory.CreateDirectory(carpetaCaja);
                            }

                            File.WriteAllText(Path.Combine(carpetaCaja, $"Caja_{cajaEntitie.IdApertura_Cierre_caja}.txt"),
                                                      tirilla.ToString(),
                                                      Encoding.UTF8);

                            RawPrinterHelper.SendStringToPrinter(Impresora, tirilla.ToString(), LineasAbajo);
                        }
                        else
                        {
                            MessageBox.Show("No se encuentra informacion de parametros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se encuentra informacion de parametros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("No hay datos", "Sin datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("No hay datos", "Sin datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
