using EntityDTO.DTO;
using EntityModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityBLL.BLLOracle
{
    public interface IProductoServiceOracle
    {
        Task<IEnumerable<Producto>> GetAllService();

        Task AgregarProductoAsync(Producto producto);

        Task<Producto> ObtenerProductoPorIdAsync(int id);

        Task ActualizarProductoAsync(Producto producto);

        Task EliminarProductoAsync(int id);
    }
}
