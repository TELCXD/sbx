using sbx.core.Entities.Tienda;
using sbx.core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbx.core.Interfaces.TipoContribuyente
{
    public interface ITipoContribuyente
    {
        Task<Response<dynamic>> ListTipoContribuyente();
    }
}
