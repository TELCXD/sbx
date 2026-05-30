using sbx.core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbx.core.Interfaces.PagosVenta
{
    public interface IPagosVenta
    {
        Task<Response<dynamic>> List(int IdVenta);
    }
}
