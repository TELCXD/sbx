using sbx.core.Entities;

namespace sbx.core.Interfaces.Marca
{
    public interface IMarca
    {
        Task<Response<dynamic>> ListMarca();
        Task<Response<dynamic>> ListMarcaId(int Id);
        Task<Response<dynamic>> BuscaMarca(string Buscar);
        Task<Response<dynamic>> Eliminar(int Id);
        Task<bool> ExisteNombre(string nombre, int Id_Marca);
        Task<Response<dynamic>> CreateUpdate(string nombre, int IdMarca);
    }
}
