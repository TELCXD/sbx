using sbx.core.Entities;

namespace sbx.core.Interfaces.Categoria
{
    public interface ICategoria
    {
        Task<Response<dynamic>> ListCategoria();
    }
}
