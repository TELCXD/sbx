using Dapper;
using Microsoft.Data.SqlClient;
using sbx.core.Entities;
using sbx.core.Interfaces.Marca;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbx.repositories.Marca
{
    public class MarcaRepository : IMarca
    {
        private readonly string _connectionString;

        public MarcaRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Response<dynamic>> ListMarca()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();

                try
                {
                    await connection.OpenAsync();

                    string sql = "SELECT IdMarca, Nombre FROM T_Marcas  ORDER BY IdMarca";

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
