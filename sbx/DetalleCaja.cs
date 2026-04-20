using sbx.core.Entities.Caja;
using sbx.core.Interfaces.Caja;

namespace sbx
{
    public partial class DetalleCaja : Form
    {
        private dynamic? _Permisos;
        private int _IdApertura_Cierre_caja;
        private readonly ICaja _ICaja;

        public DetalleCaja(ICaja caja)
        {
            _ICaja = caja;
            InitializeComponent();
        }

        public dynamic? Permisos
        {
            get => _Permisos;
            set => _Permisos = value;
        }

        public int IdApertura_Cierre_caja
        {
            get => _IdApertura_Cierre_caja;
            set => _IdApertura_Cierre_caja = value;
        }

        private async void DetalleCaja_Load(object sender, EventArgs e)
        {
            var DataCaja = await _ICaja.ListForId(_IdApertura_Cierre_caja);

            if (DataCaja.Data != null)
            {
                if (DataCaja.Data.Count > 0)
                {
                    CajaEntitie cajaEntitie = new CajaEntitie();
                    cajaEntitie.IdApertura_Cierre_caja = DataCaja.Data[0].IdApertura_Cierre_caja;
                    cajaEntitie.Usuario = DataCaja.Data[0].Usuario;
                    cajaEntitie.FechaHoraApertura = DataCaja.Data[0].FechaHoraApertura;
                    if (DataCaja.Data[0].FechaHoraCierre != null)
                    {
                        cajaEntitie.FechaHoraCierre = DataCaja.Data[0].FechaHoraCierre;
                    }
                    cajaEntitie.MontoInicialDeclarado = DataCaja.Data[0].MontoInicialDeclarado;
                    cajaEntitie.VentasTotales = DataCaja.Data[0].VentasTotales;
                    cajaEntitie.PagosEnEfectivo = DataCaja.Data[0].PagosEnEfectivo;
                    cajaEntitie.PagosEnEfectivo = DataCaja.Data[0].PagosEnEfectivo;
                    cajaEntitie.PagosEnNequi = DataCaja.Data[0].PagosEnNequi;
                    cajaEntitie.PagosEnDaviPlata = DataCaja.Data[0].PagosEnDaviPlata;
                    cajaEntitie.PagosEnBancolombiaQR = DataCaja.Data[0].PagosEnBancolombiaQR;
                    cajaEntitie.PagosEnTransferencia = DataCaja.Data[0].PagosEnTransferencia;
                    cajaEntitie.PagosEnTarjetaCredito = DataCaja.Data[0].PagosEnTarjetaCredito;
                    cajaEntitie.PagosEnTarjetaDebito = DataCaja.Data[0].PagosEnTarjetaDebito;
                    cajaEntitie.MontoFinalDeclarado = DataCaja.Data[0].MontoFinalDeclarado;
                    cajaEntitie.Diferencia = DataCaja.Data[0].Diferencia;
                    cajaEntitie.Estado = DataCaja.Data[0].Estado;

                    lbl_estado.Text = cajaEntitie.Estado;
                    lbl_usuario.Text = cajaEntitie.Usuario;
                    lbl_fechaApertura.Text = cajaEntitie.FechaHoraApertura.ToString();
                    lbl_fechaCierre.Text = cajaEntitie.FechaHoraCierre.ToString();
                    lbl_pagoEfectivo .Text = cajaEntitie.PagosEnEfectivo.ToString("N2");
                    lbl_pagoNequi.Text = cajaEntitie.PagosEnNequi.ToString("N2");
                    lbl_pagoDaviplata.Text = cajaEntitie.PagosEnDaviPlata.ToString("N2");
                    lbl_pagoBancolombia.Text = cajaEntitie.PagosEnBancolombiaQR.ToString("N2");
                    lbl_pagoTransferencia.Text = cajaEntitie.PagosEnTransferencia.ToString("N2");
                    lbl_pagoTarjetaCredito.Text = cajaEntitie.PagosEnTarjetaCredito.ToString("N2");
                    lbl_pagoTarjetaDebito.Text = cajaEntitie.PagosEnTarjetaDebito.ToString("N2");
                    lbl_montoInicial.Text = cajaEntitie.MontoInicialDeclarado.ToString("N2");
                    lbl_totalVentas.Text = cajaEntitie.VentasTotales.ToString("N2");
                    lbl_montoFinal.Text = cajaEntitie.MontoFinalDeclarado.ToString("N2");
                    lbl_diferencia.Text = cajaEntitie.Diferencia.ToString("N2");
                }
            }
        }
    }
}
