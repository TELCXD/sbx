using Dapper;
using Microsoft.Data.SqlClient;
using sbx.core.Entities;
using sbx.core.Entities.Tienda;
using sbx.core.Interfaces.Tienda;

namespace sbx.repositories.TiendaRepository
{
    public class TiendaRepository : ITienda
    {
        private readonly string _connectionString;

        public TiendaRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Response<dynamic>> CreateUpdate(TiendaEntitie tiendaEntitie, int IdUser)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();

                try
                {
                    await connection.OpenAsync();

                    string sql = "";

                    DateTime FechaActual = DateTime.Now;
                    FechaActual = Convert.ToDateTime(FechaActual.ToString("yyyy-MM-dd HH:mm:ss"));

                    if (tiendaEntitie.IdTienda > 0)
                    {
                        sql = @$" UPDATE T_Tienda SET IdIdentificationType = @IdIdentificationType,NumeroDocumento = @NumeroDocumento,
                                  NombreRazonSocial=@NombreRazonSocial,IdTipoResponsabilidad=@IdTipoResponsabilidad,
                                  IdResponsabilidadTributaria=@IdResponsabilidadTributaria,IdTipoContribuyente=@IdTipoContribuyente,
                                  CorreoDistribucion=@CorreoDistribucion,Telefono=@Telefono,Direccion=@Direccion,IdCountry=@IdCountry,
                                  IdDepartament=@IdDepartament,IdCity=@IdCity,IdCodigoPostal=@IdCodigoPostal,
                                  IdActividadEconomica=@IdActividadEconomica,UpdateDate=@UpdateDate,IdUserAction=@IdUserAction
                                  WHERE IdTienda = @IdTienda";

                        var parametros = new
                        {
                            tiendaEntitie.IdTienda,
                            IdIdentificationType = tiendaEntitie.TipoDocumento,
                            tiendaEntitie.NumeroDocumento,
                            tiendaEntitie.NombreRazonSocial,
                            IdTipoResponsabilidad = tiendaEntitie.TipoResponsabilidad,
                            IdResponsabilidadTributaria = tiendaEntitie.ResponsabilidadTributaria,
                            IdTipoContribuyente = tiendaEntitie.TipoContribuyente,
                            tiendaEntitie.CorreoDistribucion,
                            tiendaEntitie.Telefono,
                            tiendaEntitie.Direccion,
                            IdCountry = tiendaEntitie.Pais,
                            IdDepartament = tiendaEntitie.Departamento,
                            IdCity = tiendaEntitie.Municipio,
                            IdCodigoPostal = tiendaEntitie.CodigoPostal,
                            IdActividadEconomica = tiendaEntitie.ActividadEconomica,
                            UpdateDate = FechaActual,
                            IdUserAction = IdUser
                        };

                        int FilasAfectadas = await connection.ExecuteAsync(sql, parametros);

                        if (FilasAfectadas > 0)
                        {
                            response.Flag = true;
                            response.Message = "Tienda actualizada correctamente";
                        }
                        else
                        {
                            response.Flag = false;
                            response.Message = "Se presento un error al intentar actualizar Tienda";
                        }
                    }
                    else
                    {
                        sql = @$" INSERT INTO T_Tienda (IdTienda,IdIdentificationType,NumeroDocumento,NombreRazonSocial,IdTipoResponsabilidad,
                                  IdResponsabilidadTributaria,IdTipoContribuyente,CorreoDistribucion,Telefono,Direccion,IdCountry,
                                  IdDepartament,IdCity,IdCodigoPostal,IdActividadEconomica,CreationDate,IdUserAction)
                                  VALUES(@IdTienda,@IdIdentificationType,@NumeroDocumento,@NombreRazonSocial,@IdTipoResponsabilidad,
                                  @IdResponsabilidadTributaria,@IdTipoContribuyente,@CorreoDistribucion,@Telefono,@Direccion,@IdCountry,
                                  @IdDepartament,@IdCity,@IdCodigoPostal,@IdActividadEconomica,@CreationDate,@IdUserAction)";

                        var parametros = new
                        {
                            IdTienda = 1,
                            IdIdentificationType = tiendaEntitie.TipoDocumento,
                            tiendaEntitie.NumeroDocumento,
                            tiendaEntitie.NombreRazonSocial,
                            IdTipoResponsabilidad = tiendaEntitie.TipoResponsabilidad,
                            IdResponsabilidadTributaria = tiendaEntitie.ResponsabilidadTributaria,
                            IdTipoContribuyente = tiendaEntitie.TipoContribuyente,
                            tiendaEntitie.CorreoDistribucion,
                            tiendaEntitie.Telefono,
                            tiendaEntitie.Direccion,
                            IdCountry = tiendaEntitie.Pais,
                            IdDepartament = tiendaEntitie.Departamento,
                            IdCity = tiendaEntitie.Municipio,
                            IdCodigoPostal = tiendaEntitie.CodigoPostal,
                            IdActividadEconomica = tiendaEntitie.ActividadEconomica,
                            CreationDate = FechaActual,
                            IdUserAction = IdUser
                        };

                        int FilasAfectadas = await connection.ExecuteAsync(sql, parametros);

                        if (FilasAfectadas > 0)
                        {
                            response.Flag = true;
                            response.Message = "Tienda creada correctamente";
                        }
                        else
                        {
                            response.Flag = false;
                            response.Message = "Se presento un error al intentar crear Tienda";
                        }
                    }

                    return response;
                }
                catch (Exception ex)
                {
                    response.Flag = false;
                    response.Message = "Error: " + ex.Message;
                    return response;
                }
            }
        }

        public async Task<Response<dynamic>> List()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();

                try
                {
                    await connection.OpenAsync();

                    string sql = @"SELECT
	                               IdTienda
                                  ,IdIdentificationType
                                  ,NumeroDocumento
                                  ,NombreRazonSocial
                                  ,IdTipoResponsabilidad
                                  ,IdResponsabilidadTributaria
                                  ,IdTipoContribuyente
                                  ,CorreoDistribucion
                                  ,Telefono
                                  ,Direccion
                                  ,IdCountry
                                  ,IdDepartament
                                  ,IdCity
                                  ,IdCodigoPostal
                                  ,IdActividadEconomica
                                  ,Logo
                                  ,CreationDate
                                  ,UpdateDate
                                  ,IdUserAction
                                   FROM T_Tienda ";

                    dynamic resultado = await connection.QueryAsync(sql);

                    response.Flag = true;
                    response.Message = "Proceso realizado correctamente";
                    response.Data = resultado;
                    return response;
                }
                catch (Exception ex)
                {
                    response.Flag = false;
                    response.Message = "Error: " + ex.Message;
                    return response;
                }
            }
        }
    }
}
