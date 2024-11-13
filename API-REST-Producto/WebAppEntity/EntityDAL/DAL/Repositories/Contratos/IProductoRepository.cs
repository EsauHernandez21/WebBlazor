using EntityModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityDTO.DTO;

namespace EntityDAL.DAL.Repositories.Contratos
{
    public interface IProductoRepository
    {
        //Es una operación asíncrona
        Task<ProductoDto> GetProductoByIdAsync(int id);
        //Es una interfaz en C# que representa una colección de elementos se puede recorrer o iterar sobre ellos,
        //generalmente mediante un bucle foreach
        Task<IEnumerable<ProductoDto>> GetAllProductosAsync();
        Task AddProductoAsync(ProductoDto producto);
        Task UpdateProductoAsync(ProductoDto producto);
        //Task indica que el método es asíncrono  y se ejecuta en segundo plano
        Task DeleteProductoAsync(int id);
    }
}
