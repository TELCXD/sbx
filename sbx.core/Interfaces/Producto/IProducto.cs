using sbx.core.Entities;
using sbx.core.Entities.Producto;

namespace sbx.core.Interfaces.Producto
{
    public interface IProducto
    {
        Task<Response<dynamic>> CreateUpdate(ProductoEntitie productoEntitie, int IdUser);
        Task<Response<dynamic>> List(int Id);
        Task<bool> ExisteSku(string sku, int Id_Producto);
        Task<bool> ExisteCodigoBarras(string codigoBarras, int Id_Producto);
        Task<bool> ExisteNombre(string nombre, int Id_Producto);
        Task<Response<dynamic>> Buscar(string dato, string campoFiltro, string tipoFiltro);
        Task<Response<dynamic>> ListSku(string sku);
        Task<Response<dynamic>> ListCodigoBarras(string CodigoBarras);
        Task<Response<dynamic>> BuscarProductoPadre(string dato, string campoFiltro, string tipoFiltro);
        Task<Response<dynamic>> BuscarProductoHijo(string dato, string campoFiltro, string tipoFiltro);
        Task<Response<dynamic>> BuscarExportarExcel(string dato, string campoFiltro, string tipoFiltro);
        Task<Response<dynamic>> Eliminar(int Id);
        Task<bool> ExisteIdProducto(int Id_Producto);
        Task<Response<dynamic>> BuscarProductoGrupo(string dato, string campoFiltro, string tipoFiltro);
        Task<Response<dynamic>> BuscarProductoIndividual(string dato, string campoFiltro, string tipoFiltro);
        Task<Response<dynamic>> AgregaProdIndiviToProdGrupo(int IdProductoGrupo, int IdProductoIndividual, decimal Cantidad, int IdUser);
        Task<bool> ExistePrdIndiv(int id_prd_grupo, int id_prd_indiv);
        Task<Response<dynamic>> BuscarXProdGrupo(int IdProductoGrupo);
        Task<Response<dynamic>> EliminarPrdIndvXPrdGrp(int id_prd_grupo, int id_prd_indiv);
        Task<Response<dynamic>> UpdateCantidadProdIndiviToProdGrupo(int IdProductoGrupo, int IdProductoIndividual, decimal Cantidad, int IdUser);
        Task<Response<dynamic>> BuscarProdGrupoDetalle(string dato, string campoFiltro, string tipoFiltro);
        Task<Response<dynamic>> EliminarPrdGrp(int Id);
        Task<Response<dynamic>> ListPrdGrp(int Id);
        Task<bool> ListIdCodigoBarras(int IdProducto, string CodigoBarras);
        Task<Response<dynamic>> ListCodigoBarras(int IdProducto);
        Task<Response<dynamic>> ListCodigoBarrasTexto(string CodigoBarras, int Id_Producto);
        Task<Response<dynamic>> CreateCodigoBarras(string CodigoBarras,int IdProducto, int IdUser);
        Task<Response<dynamic>> EliminarCodigoBarras(string CodigoBarras, int IdProducto);
        Task<bool> ListIdCodigoBarras2(int IdProducto, string CodigoBarras);
        Task<Response<dynamic>> ListCodigoBarras2(string CodigoBarras);
        Task<Response<dynamic>> ListCodigoBarras3(int IdProducto);
    }
}
