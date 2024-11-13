using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using EntityModel.Model;
using EntityDTO.DTO;
using Microsoft.EntityFrameworkCore;

namespace EntityDAL.DALOracle
{
    public class ProductoOracleRepository : IProductoOracleRepository
    {

        // Esta clase implementa la interfaz IProductoOracleRepository y contiene la lógica para interactuar con la BD
        private readonly OracleDbContext _oracleDbContext;

        public ProductoOracleRepository(OracleDbContext oracleDbContext)
        {
            _oracleDbContext = oracleDbContext;
        }

        public async Task<IEnumerable<Producto>> GetProductosAsync()
        {

            var productos = new List<Producto>();

            using (var connection = _oracleDbContext.CreateConnection())
            {
                await connection.OpenAsync();
                using (var command = new OracleCommand("SELECT * FROM producto", connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var producto = new Producto
                            {

                                ProductoId = reader.GetInt32(reader.GetOrdinal("PRODUCTOID")),
                                Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                                Descripcion = reader.GetString(reader.GetOrdinal("Descripcion")),
                                Precio = reader.GetDecimal(reader.GetOrdinal("Precio")),
                                Stock = reader.GetInt32(reader.GetOrdinal("Stock"))
                                // Añadir otros campos 
                            };
                            productos.Add(producto);
                        }
                    }
                }
            }

            return productos;

        }

        public async Task AddProductoAsync(Producto producto)
        {
            ///await _oracleDbContext
            ///
            using (var connection = _oracleDbContext.CreateConnection())
            {
                await connection.OpenAsync();

                using (var command = new OracleCommand("InsertarProducto", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Agrega los parámetros del procedimiento
                    command.Parameters.Add("p_nombre", OracleDbType.Varchar2).Value = producto.Nombre;
                    command.Parameters.Add("p_descripcion", OracleDbType.Varchar2).Value = producto.Descripcion;
                    command.Parameters.Add("p_precio", OracleDbType.Decimal).Value = producto.Precio;
                    command.Parameters.Add("p_stock", OracleDbType.Decimal).Value = producto.Stock;

                    // Ejecuta el comando asincrónicamente
                    await command.ExecuteNonQueryAsync();
                }

            }
        }


        public async Task<Producto> GetProductoByIdAsync(int id)
        {
            Producto producto = null;

            using (var connection = _oracleDbContext.CreateConnection())
            {
                await connection.OpenAsync();
                using (var command = new OracleCommand("SP_GET_PRODUCTO_BY_ID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Definir el parámetro de entrada para el productoid
                    command.Parameters.Add("p_productoid", OracleDbType.Int32).Value = id;

                    // Definir el parámetro de salida para el cursor
                    command.Parameters.Add("p_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            producto = new Producto
                            {
                                ProductoId = reader.GetInt32(reader.GetOrdinal("PRODUCTOID")),
                                Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                                Descripcion = reader.GetString(reader.GetOrdinal("Descripcion")),
                                Precio = reader.GetDecimal(reader.GetOrdinal("Precio")),
                                Stock = reader.GetInt32(reader.GetOrdinal("Stock"))
                                // Añadir otros campos según sea necesario
                            };
                        }
                    }
                }
            }

            return producto;
        }

        public async Task UpdateProductoAsync(Producto producto)
        {

            // Asegúrate de que el objeto 'producto' no sea nulo y contenga los datos necesarios
            if (producto == null)
            {
                throw new ArgumentNullException(nameof(producto));
            }

            using (var connection = _oracleDbContext.CreateConnection())
            {
                await connection.OpenAsync();

                // Crear el comando para llamar al procedimiento almacenado
                using (var command = new OracleCommand("SP_UPDATE_PRODUCTO", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Definir los parámetros de entrada para el procedimiento
                    command.Parameters.Add("p_productoid", OracleDbType.Int32).Value = producto.ProductoId;
                    command.Parameters.Add("p_nombre", OracleDbType.Varchar2).Value = producto.Nombre; // Añadir el nombre
                    command.Parameters.Add("p_descripcion", OracleDbType.Varchar2).Value = producto.Descripcion; // Añadir la descripción
                    command.Parameters.Add("p_precio", OracleDbType.Decimal).Value = producto.Precio; // Añadir el precio
                    command.Parameters.Add("p_stock", OracleDbType.Int32).Value = producto.Stock; // Añadir el stock

                    // Ejecutar el comando
                    await command.ExecuteNonQueryAsync(); // No se espera un resultado, solo la actualización
                }
            }

        }

        public async Task DeleteProductoAsync(int id)
        {
            using (var connection = _oracleDbContext.CreateConnection())
            {
                await connection.OpenAsync();

                using (var command = new OracleCommand("SP_DELETE_PRODUCTO", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Definir el parámetro de entrada para el productoid
                    command.Parameters.Add("p_productoid", OracleDbType.Int32).Value = id;

                    // Ejecutar el comando
                    await command.ExecuteNonQueryAsync(); // No se espera un resultado, solo la eliminación
                }
            }
        }

    }
}

