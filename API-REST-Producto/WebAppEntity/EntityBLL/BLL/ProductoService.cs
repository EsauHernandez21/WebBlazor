using EntityModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityDAL.DAL.Repositories.Contratos;
using EntityDTO.DTO;
using Microsoft.Extensions.Caching.Memory; // Asegúrate de tener este using


namespace EntityBLL.BLL
{
    public class ProductoService:IProductoService
    {
       // private readonly OracleConnection _oracleConnection;
       // private readonly OracleConnection _oracleConnection;
        private readonly IProductoRepository _productoRepository;
       
        //private readonly IMemoryCache _cache;
        //private readonly MemoryCacheEntryOptions _cacheOptions;
        // Constructor que inyecta el repositorio
        /* public ProductoService(IProductoRepository productoRepository, IMemoryCache cache)
         {
             _productoRepository = productoRepository;
             _cache = cache;



             _cacheOptions = new MemoryCacheEntryOptions()
             {
                 AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
             };

         }*/


        public ProductoService(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        // Implementación para obtener todos los productos
        public async Task<IEnumerable<ProductoDto>> ObtenerTodosLosProductosAsync()
        {
            return await _productoRepository.GetAllProductosAsync();
        }

        // Implementación para obtener un producto por ID
        /* public async Task<ProductoDto> ObtenerProductoPorIdAsync(int id)
         {
             //return await _productoRepository.GetProductoByIdAsync(id);

             // Intenta obtener el producto del caché
             if (!_cache.TryGetValue(id, out ProductoDto productoCacheado))
             {
                 // Si no está en caché, obtener de la base de datos
                 productoCacheado = await _productoRepository.GetProductoByIdAsync(id);

                 // Si se encontró el producto, agregarlo al caché
                 if (productoCacheado != null)
                 {
                     _cache.Set(id, productoCacheado, _cacheOptions);
                 }
             }

             return productoCacheado;



         }*/

        public async Task<ProductoDto> ObtenerProductoPorIdAsync(int id)
        {
            return await _productoRepository.GetProductoByIdAsync(id);
        }

        // Implementación para agregar un nuevo producto
        /* public async Task AgregarProductoAsync(ProductoDto producto)
         {
             // Lógica de negocio (ejemplo: validación)
             if (producto.Precio < 0)
             {
                 throw new Exception("El precio no puede ser negativo.");
             }
             await _productoRepository.AddProductoAsync(producto);
             _cache.Remove(producto.ProductoId); // Remover el producto d
         }*/

        public async Task AgregarProductoAsync(ProductoDto producto)
        {
            // Lógica de negocio (ejemplo: validación)
            if (producto.Precio < 0)
            {
                throw new Exception("El precio no puede ser negativo.");
            }
            await _productoRepository.AddProductoAsync(producto);
        }

        // Implementación para actualizar un producto
        /* public async Task ActualizarProductoAsync(ProductoDto producto)
         {
             if (producto.Precio < 0)
             {
                 throw new Exception("El precio no puede ser negativo.");
             }
             await _productoRepository.UpdateProductoAsync(producto);
             _cache.Remove(producto.ProductoId); // Remover el producto del caché
         }*/

        public async Task ActualizarProductoAsync(ProductoDto producto)
        {
            if (producto.Precio < 0)
            {
                throw new Exception("El precio no puede ser negativo.");
            }
            await _productoRepository.UpdateProductoAsync(producto);
        }


        // Implementación para eliminar un producto
        /*public async Task EliminarProductoAsync(int id)
        {
            await _productoRepository.DeleteProductoAsync(id);
            _cache.Remove(id); // Remover el producto del caché
        }*/

        public async Task EliminarProductoAsync(int id)
        {
            await _productoRepository.DeleteProductoAsync(id);
        }
    }
}
