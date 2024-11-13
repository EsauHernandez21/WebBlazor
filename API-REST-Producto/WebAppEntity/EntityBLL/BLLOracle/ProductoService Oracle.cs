using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityDAL.DAL.Repositories; // Asegúrate de que apunta a la interfaz de tu repositorio
using EntityModel.Model;
using System;
using EntityDAL.DALOracle;
using System.Data;
using EntityDAL.DAL.Repositories.Contratos;
using EntityDTO.DTO;

namespace EntityBLL.BLLOracle
{
    public class ProductoService_Oracle : IProductoServiceOracle
    {

        private readonly IProductoOracleRepository _productoRepositoryOracle;

        public ProductoService_Oracle(IProductoOracleRepository productoRepository)
        {
            _productoRepositoryOracle = productoRepository; // Inyección de dependencia
        }

              
        public async Task<IEnumerable<Producto>> GetAllService()
        {
    
            return await _productoRepositoryOracle.GetProductosAsync();
        }


        public async Task AgregarProductoAsync(Producto producto)
        {
            // Lógica de negocio (ejemplo: validación)
            if (producto.Precio < 0)
            {
                throw new Exception("El precio no puede ser negativo.");
            }
            await _productoRepositoryOracle.AddProductoAsync(producto);
        }

        public async Task<Producto> ObtenerProductoPorIdAsync(int id)
        {
            return await _productoRepositoryOracle.GetProductoByIdAsync(id);
        }

        public async Task ActualizarProductoAsync(Producto producto)
        {
            if (producto.Precio < 0)
            {
                throw new Exception("El precio no puede ser negativo.");
            }
            await _productoRepositoryOracle.UpdateProductoAsync(producto);
        }

        public async Task EliminarProductoAsync(int id)
        {
            await _productoRepositoryOracle.DeleteProductoAsync(id);
        }

    }
}

