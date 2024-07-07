using Beeswax.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beeswax.Service
{
    public interface ProductService
    {
        public Task<List<Producto>> Obtener();    
    }
}
