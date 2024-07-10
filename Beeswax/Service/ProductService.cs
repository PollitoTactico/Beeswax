using Beeswax.Models;

namespace Beeswax.Service
{
    public interface ProductService
    {
        public Task<IEnumerable<Producto>> Obtener();
    }
}
