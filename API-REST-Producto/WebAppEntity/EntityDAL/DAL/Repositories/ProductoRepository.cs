using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityDAL.DAL.DBContext;
using EntityDAL.DAL.Repositories.Contratos;
using EntityModel.Model;
using Microsoft.EntityFrameworkCore;
using EntityDTO.DTO;

namespace EntityDAL.DAL.Repositories
{
    public class ProductoRepository:IProductoRepository
    {
        private readonly DBDProductos _context;

        public ProductoRepository(DBDProductos context)
        {
            _context = context;
        }

        //async: palabra clave que se usa para marcar un método como "asíncrono"
        
        //await esperar la finalización de una tarea (generalmente una operación asíncrona) sin bloquear
        //el hilo principal de ejecución.
        public async Task<ProductoDto> GetProductoByIdAsync(int id)
        {
            return await _context.Productos.FindAsync(id);
        }

        public async Task<IEnumerable<ProductoDto>> GetAllProductosAsync()
        {
            return await _context.Productos.ToListAsync();
        }

        public async Task AddProductoAsync(ProductoDto producto)
        {
            await _context.Productos.AddAsync(producto);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProductoAsync(ProductoDto producto)
        {
            _context.Productos.Update(producto);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductoAsync(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
                await _context.SaveChangesAsync();
            }
        }

    }
}
