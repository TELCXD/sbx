using Dapper;
using Microsoft.Data.SqlClient;
using sbx.core.Entities;
using sbx.core.Interfaces.PagosVenta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbx.repositories.PagosVenta
{
    public class PagosVentaRepository :  IPagosVenta
    {
        private readonly string _connectionString;

        public PagosVentaRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Response<dynamic>> List(int IdVenta)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var response = new Response<dynamic>();

                try
                {
                    await connection.OpenAsync();

                    string sql = $@"SELECT a.IdPagoVenta,a.IdVenta,a.IdMetodoPago,b.Nombre,
									a.Recibido,a.Monto,a.Referencia,a.IdBanco,a.CreationDate,a.IdUserAction  
									FROM T_PagosVenta a
									INNER JOIN T_MetodoPago b on a.IdMetodoPago = b.IdMetodoPago 
									WHERE IdVenta = " + IdVenta;

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
