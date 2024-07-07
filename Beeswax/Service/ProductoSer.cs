using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Beeswax.Models;

namespace Beeswax.Service
{
    public class ProductoSer : ProductService
    {
        private string urlApi = "https://localhost:7073/api/Producto";

        public async Task<List<Producto>> Obtener()
        {
            var cliente = new HttpClient();
            var response = await cliente.GetAsync(urlApi);
            var responseBody = await response.Content.ReadAsStringAsync();
            JsonNode nodos = JsonNode.Parse(responseBody);

            // Assuming the response is directly a list of products
            var productosData = JsonSerializer.Deserialize<List<Producto>>(nodos.ToString());

            var tasks = productosData.Select(async producto =>
            {
                var productoResponse = await cliente.GetAsync($"{urlApi}/{producto.Id}");
                var productoBody = await productoResponse.Content.ReadAsStringAsync();
                JsonNode productoNode = JsonNode.Parse(productoBody);

                
                producto.nombreProducto = productoNode["nombreProducto"]?.ToString();
                producto.descripcion = productoNode["descripcion"]?.ToString();
                producto.precio = productoNode["precio"]?.GetValue<int>() ?? 0;
                producto.categoria = productoNode["categoria"]?.ToString();
                producto.stock = productoNode["stock"]?.GetValue<int>() ?? 0;
                producto.imagen = productoNode["imagen"]?.ToString();
            });

            await Task.WhenAll(tasks);

            return productosData;
        }
    }
}