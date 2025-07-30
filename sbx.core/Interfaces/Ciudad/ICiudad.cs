using sbx.core.Entities;

namespace sbx.core.Interfaces.Ciudad
{
    public interface ICiudad
    {
        Task<Response<dynamic>> ListCiudad();
        Task<Response<dynamic>> ListCiudadForDepartament(int IdDepartamento);
    }
}
