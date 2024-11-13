using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityModel.Model;
using EntityDTO.DTO;

namespace EntityBLL.BLL
{
    public interface IProductoService
    {
        Task<IEnumerable<ProductoDto>> ObtenerTodosLosProductosAsync();
        Task<ProductoDto> ObtenerProductoPorIdAsync(int id);
        Task AgregarProductoAsync(ProductoDto producto);
        Task ActualizarProductoAsync(ProductoDto producto);
        Task EliminarProductoAsync(int id);
    }
}
