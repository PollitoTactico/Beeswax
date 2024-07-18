using Beeswax.Models;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Beeswax.Service
{
    public class ProductoSer
    {
        private readonly string urlApi = "https://apibeeswax20240708001418.azurewebsites.net/api/Producto";

        public async Task<List<Producto>> Obtener()
        {
            try
            {
                using var cliente = new HttpClient();
                var response = await cliente.GetAsync($"{urlApi}/GetAll");
                response.EnsureSuccessStatusCode(); 

                var responseBody = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrWhiteSpace(responseBody))
                {
                    var productosData = JsonSerializer.Deserialize<List<Producto>>(responseBody, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    return productosData ?? new List<Producto>();
                }
                return new List<Producto>();
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error al obtener productos: {ex.Message}");
                return new List<Producto>();
            }
        }

        public async Task<Producto> ObtenerPorId(int id)
        {
            try
            {
                using var cliente = new HttpClient();
                var response = await cliente.GetAsync($"{urlApi}/GetById/{id}");
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrWhiteSpace(responseBody))
                {
                    var productoData = JsonSerializer.Deserialize<Producto>(responseBody, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    return productoData;
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener producto por ID: {ex.Message}");
                return null;
            }
        }

        public async Task<Producto> Crear(Producto producto)
        {
            try
            {
                using var cliente = new HttpClient();
                var productoJson = JsonSerializer.Serialize(producto);
                var contenido = new StringContent(productoJson, System.Text.Encoding.UTF8, "application/json");
                var response = await cliente.PostAsync($"{urlApi}/Create", contenido);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrWhiteSpace(responseBody))
                {
                    var productoData = JsonSerializer.Deserialize<Producto>(responseBody, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    return productoData;
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear producto: {ex.Message}");
                return null;
            }
        }

        public async Task<Producto> Actualizar(Producto producto)
        {
            try
            {
                using var cliente = new HttpClient();
                var productoJson = JsonSerializer.Serialize(producto);
                var contenido = new StringContent(productoJson, System.Text.Encoding.UTF8, "application/json");
                var response = await cliente.PutAsync($"{urlApi}/Update", contenido);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrWhiteSpace(responseBody))
                {
                    var productoData = JsonSerializer.Deserialize<Producto>(responseBody, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    return productoData;
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar producto: {ex.Message}");
                return null;
            }
        }
        public async Task<bool> Eliminar(int id)
        {
            try
            {
                using var cliente = new HttpClient();
                var response = await cliente.DeleteAsync($"{urlApi}/Delete/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error al eliminar producto: {response.ReasonPhrase}, Detalles: {responseBody}");
                    return false;
                }

                return response.StatusCode == System.Net.HttpStatusCode.NoContent;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error al eliminar producto: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inesperado: {ex.Message}");
                return false;
            }
        }
    }
}


