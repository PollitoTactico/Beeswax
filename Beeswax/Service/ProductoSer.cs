using Beeswax.Models;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Beeswax.Service
{
    public class ProductoSer : ProductService
    {
        private string urlApi = "https://apibeeswax20240708001418.azurewebsites.net/api/Producto";

        public async Task<List<Producto>> Obtener()
        {
            var cliente = new HttpClient();
            var response = await cliente.GetAsync($"{urlApi}/GetAll");
            var responseBody = await response.Content.ReadAsStringAsync();
            JsonNode nodos = JsonNode.Parse(responseBody);


            var productosData = JsonSerializer.Deserialize<List<Producto>>(nodos.ToString());

            return productosData;
        }

        public async Task<Producto> ObtenerPorId(int id)
        {
            var cliente = new HttpClient();
            var response = await cliente.GetAsync($"{urlApi}/GetById/{id}");
            var responseBody = await response.Content.ReadAsStringAsync();
            JsonNode nodos = JsonNode.Parse(responseBody);


            var productoData = JsonSerializer.Deserialize<Producto>(nodos.ToString());

            return productoData;
        }

        public async Task<Producto> Crear(Producto producto)
        {
            var cliente = new HttpClient();
            var productoJson = JsonSerializer.Serialize(producto);
            var contenido = new StringContent(productoJson, System.Text.Encoding.UTF8, "application/json");
            var response = await cliente.PostAsync($"{urlApi}/Create", contenido);
            var responseBody = await response.Content.ReadAsStringAsync();
            JsonNode nodos = JsonNode.Parse(responseBody);

            var productoData = JsonSerializer.Deserialize<Producto>(nodos.ToString());

            return productoData;
        }

        public async Task<Producto> Actualizar(Producto producto)
        {
            var cliente = new HttpClient();
            var productoJson = JsonSerializer.Serialize(producto);
            var contenido = new StringContent(productoJson, System.Text.Encoding.UTF8, "application/json");
            var response = await cliente.PutAsync($"{urlApi}/Update", contenido);
            var responseBody = await response.Content.ReadAsStringAsync();
            JsonNode nodos = JsonNode.Parse(responseBody);

            var productoData = JsonSerializer.Deserialize<Producto>(nodos.ToString());

            return productoData;
        }

        public async Task<Producto> Eliminar(int id)
        {
            var cliente = new HttpClient();
            var response = await cliente.DeleteAsync($"{urlApi}/Delete/{id}");
            var responseBody = await response.Content.ReadAsStringAsync();
            JsonNode nodos = JsonNode.Parse(responseBody);

            var productoData = JsonSerializer.Deserialize<Producto>(nodos.ToString());

            return productoData;
        }
    }
}