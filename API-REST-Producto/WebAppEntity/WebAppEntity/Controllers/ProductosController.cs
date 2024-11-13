using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EntityBLL.BLL;
using EntityModel.Model;
using EntityDTO.DTO;
using EntityBLL.BLLOracle;

namespace WebAppEntity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {

        //La BLL llama a la capa DAL para acceder a los datos necesarios
        private readonly IProductoService _productoService;
        private readonly IProductoServiceOracle _productoServiceOracle;


        // Lista en memoria simulando los productos
        private static List<ProductoDto> productosEnMemoria = new List<ProductoDto>
        {
            new ProductoDto { ProductoId = 1, Nombre = "Producto 1", Descripcion = "Descripción del Producto 1", Precio = 100.00m, Stock = 50 },
            new ProductoDto { ProductoId = 2, Nombre = "Producto 2", Descripcion = "Descripción del Producto 2", Precio = 150.00m, Stock = 30 },
            new ProductoDto { ProductoId = 3, Nombre = "Producto 3", Descripcion = "Descripción del Producto 3", Precio = 200.00m, Stock = 20 }
        };

        // Constructor que acepta los dos servicios
        public ProductosController(IProductoService productoService, IProductoServiceOracle productoServiceOracle)
        {
            _productoService = productoService;
            _productoServiceOracle = productoServiceOracle;
        }

        [HttpGet("GetAllProducts")]
        public async Task<IEnumerable<ProductoDto>> GetProductos()
        {

            // Simulamos una tarea asíncrona, aunque estamos usando datos en memoria
            await Task.Delay(50); // Simula la latencia de una base de datos

            return productosEnMemoria;

            //return await _productoService.ObtenerTodosLosProductosAsync();
        }

        [HttpGet("GetProductById/{id}")]
        public async Task<ActionResult<ProductoDto>> GetProducto(int id)
        {
            // Simula una tarea asíncrona
            await Task.Delay(50);

            var producto = productosEnMemoria.Find(p => p.ProductoId == id);
            if (producto == null)
            {
                return NotFound($"Producto con ID {id} no encontrado.");
            }


            /*var producto = await _productoService.ObtenerProductoPorIdAsync(id);
            if (producto == null)
            {
                return NotFound();
            }*/

            return producto;
        }

        [HttpPost("CreateProduct")]
        public async Task<ActionResult> PostProducto(ProductoDto producto)
        {
            // Simula una tarea asíncrona
            await Task.Delay(50);

            producto.ProductoId = productosEnMemoria.Count + 1; // Asignamos un nuevo ID
            productosEnMemoria.Add(producto);

            return CreatedAtAction(nameof(GetProducto), new { id = producto.ProductoId }, producto);


            /*await _productoService.AgregarProductoAsync(producto);
            return CreatedAtAction(nameof(GetProducto), new { id = producto.ProductoId }, producto);*/
        }

        [HttpPut("updateProduct/{id}")]
        public async Task<ActionResult> PutProducto(int id, ProductoDto producto)
        {
            // Simula una tarea asíncrona
            await Task.Delay(50);

            var productoExistente = productosEnMemoria.Find(p => p.ProductoId == id);
            if (productoExistente == null)
            {
                return NotFound($"Producto con ID {id} no encontrado.");
            }

            productoExistente.Nombre = producto.Nombre;
            productoExistente.Descripcion = producto.Descripcion;
            productoExistente.Precio = producto.Precio;
            productoExistente.Stock = producto.Stock;

            return NoContent(); 



           /* if (id != producto.ProductoId)
            {
                return BadRequest();
            }

            await _productoService.ActualizarProductoAsync(producto);
            return NoContent();*/
        }

        [HttpDelete("deleteProductById/{id}")]
        public async Task<ActionResult> DeleteProducto(int id)
        {
            // Simula una tarea asíncrona
            await Task.Delay(50);

            var producto = productosEnMemoria.Find(p => p.ProductoId == id);
            if (producto == null)
            {
                return NotFound($"Producto con ID {id} no encontrado.");
            }

            productosEnMemoria.Remove(producto);

            return NoContent();

            /*await _productoService.EliminarProductoAsync(id);
            return NoContent();*/
        }

        /*************************************+METODOS ORACLE***************************************/


        // Método para obtener productos (puedes usar uno de los servicios basado en la lógica que necesites)
        [HttpGet("GetAllProductsOracle")]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductosOracle()
        {
           
             var productos = await _productoServiceOracle.GetAllService();
             return Ok(productos);
            
            
        }


        [HttpPost("CreateProductOracle")]
        public async Task<ActionResult> PostProductoOracle(Producto producto)
        {

            if (producto == null)
            {
                return BadRequest("El producto no puede ser nulo.");
            }

            try
            {
                await _productoServiceOracle.AgregarProductoAsync(producto);
                return CreatedAtAction(nameof(PostProductoOracle), new { id = producto.Nombre }, producto);
            }
            catch (Exception ex)
            {
                // Puedes registrar el error aquí si lo necesitas
                return StatusCode(500, $"Error al crear el producto: {ex.Message}");
            }

        }


        [HttpGet("GetProductByIdOracle/{id}")]
        public async Task<ActionResult<Producto>> GetProductoId(int id)
        {
            var producto = await _productoServiceOracle.ObtenerProductoPorIdAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            return producto;
        }


        [HttpPut("updateProductOracle/{id}")]
        public async Task<ActionResult> PutProductoOracle(int id, Producto producto)
        {
            if (id != producto.ProductoId)
            {
                return BadRequest();
            }

            await _productoServiceOracle.ActualizarProductoAsync(producto);
            return NoContent();
        }


        [HttpDelete("deleteProductByIdOracle/{id}")]
        public async Task<ActionResult> DeleteProductoOracle(int id)
        {
            await _productoServiceOracle.EliminarProductoAsync(id);
            return NoContent();
        }
    }
}
