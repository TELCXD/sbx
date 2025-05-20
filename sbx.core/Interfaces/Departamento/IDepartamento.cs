using sbx.core.Entities;

namespace sbx.core.Interfaces.Departamento
{
    public interface IDepartamento
    {
        Task<Response<dynamic>> ListDepartamento();
    }
}
