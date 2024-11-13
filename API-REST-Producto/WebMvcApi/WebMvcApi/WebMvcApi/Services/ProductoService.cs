using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using WebMvcApi.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebMvcApi.Services
{
    public class ProductoService
    {

        private readonly HttpClient _httpClient;

        public ProductoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Obtener todos los productos
        public async Task<List<Producto>> ObtenerProductosAsync()
        {
            var response = await _httpClient.GetAsync("/api/Productos/GetAllProducts");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Producto>>(content);
        }


        // Obtener un producto por ID
        public async Task<Producto> ObtenerProductoPorIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"/api/Productos/GetProductById/{id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Producto>(content);
        }

       
        // Crear un producto
        public async Task<bool> CrearProductoAsync(Producto producto)
        {
            var json = JsonConvert.SerializeObject(producto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/Productos/CreateProduct", content);
            return response.IsSuccessStatusCode;
        }

        // Actualizar un producto
        public async Task<bool> ActualizarProductoAsync(int id, Producto producto)
        {
            var json = JsonConvert.SerializeObject(producto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"/api/Productos/updateProduct/{id}", content);
            return response.IsSuccessStatusCode;
        }

        // Eliminar un producto SQL
        public async Task<bool> EliminarProductoAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/Productos/deleteProductById/{id}");
            return response.IsSuccessStatusCode;
        }


        public async Task<List<Producto>> ObtenerProductosOracleAsync()
        {
            var response = await _httpClient.GetAsync("/api/Productos/GetAllProductsOracle");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Producto>>(content);
        }

        // Crear un producto oracle
        public async Task<bool> CrearProductoOracleAsync(Producto producto)
        {
            var json = JsonConvert.SerializeObject(producto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/Productos/CreateProductOracle", content);
            return response.IsSuccessStatusCode;
        }

        // Obtener un producto por ID oracle
        public async Task<Producto> ObtenerProductoPorIdOracleAsync(int id)
        {
            var response = await _httpClient.GetAsync($"/api/Productos/GetProductByIdOracle/{id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Producto>(content);
        }



        // Actualizar un producto oracle
        public async Task<bool> ActualizarProductoOracleAsync(int id, Producto producto)
        {
            var json = JsonConvert.SerializeObject(producto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"/api/Productos/updateProductOracle/{id}", content);
            return response.IsSuccessStatusCode;
        }


        // Eliminar un producto ORACLE
        public async Task<bool> EliminarProductoOracleAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/Productos/deleteProductByIdOracle/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
