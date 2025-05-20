using sbx.core.Entities.Producto;
using sbx.core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sbx.core.Entities.Proveedor;

namespace sbx.core.Interfaces.Proveedor
{
    public interface IProveedor
    {
        Task<Response<dynamic>> CreateUpdate(ProveedorEntitie proveedorEntitie, int IdUser);
        Task<Response<dynamic>> List(int Id);
        Task<bool> ExisteNumeroDocumento(string numeroDocumento, int Id_Proveedor);
        Task<bool> ExisteNombreRazonSocial(string nombreRazonSocial, int Id_Proveedor);
        Task<bool> ExisteTelefono(string telefono, int Id_Proveedor);
        Task<bool> ExisteEmail(string email, int Id_Proveedor);
        Task<Response<dynamic>> Buscar(string dato, string campoFiltro, string tipoFiltro);
    }
}
