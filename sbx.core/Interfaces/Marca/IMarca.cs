using sbx.core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbx.core.Interfaces.Marca
{
    public interface IMarca
    {
        Task<Response<dynamic>> ListMarca();
    }
}
