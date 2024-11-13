using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityDTO.DTO;
using EntityModel.Model;


namespace EntityDAL.DALOracle
{
    public interface IProductoOracleRepository
    {
        //Esta interfaz define los métodos que el repositorio debe implementar.
        Task<IEnumerable<Producto>> GetProductosAsync();

        Task AddProductoAsync(Producto producto);

        Task<Producto> GetProductoByIdAsync(int id);

        Task UpdateProductoAsync(Producto producto);

        Task DeleteProductoAsync(int id);
    }
}
