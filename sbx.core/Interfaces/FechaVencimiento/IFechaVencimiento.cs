using sbx.core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbx.core.Interfaces.FechaVencimiento
{
    public interface IFechaVencimiento
    {
        Task<Response<dynamic>> Buscar(string dato, string campoFiltro, string tipoFiltro);

        Task<Response<dynamic>> BuscarxIdProducto(int IdProducto);

        Task<Response<dynamic>> BuscarxIdProductoTieneVence(int IdProducto);

        Task<Response<dynamic>> BuscarStock(string dato, string campoFiltro, string tipoFiltro);
    }
}
