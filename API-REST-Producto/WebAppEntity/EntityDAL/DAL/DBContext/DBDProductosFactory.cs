using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace EntityDAL.DAL.DBContext
{
    //Se utiliza en el tiempo de diseño para crear instancias del DbContext cuando se ejecutan comandos de migración desde la línea de comandos
    //(por ejemplo, dotnet ef migrations add).


    //la clase DBDProductosFactory es específica para el tiempo de diseño y ayuda a Entity Framework a crear instancias del DbContext
    //cuando ejecutas comandos de migración.
    public class DBDProductosFactory : IDesignTimeDbContextFactory<DBDProductos>
    {
        public DBDProductos CreateDbContext(string[] args = null)
        {
            // Establece la ruta base como la carpeta del proyecto WebAppEntity
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "../WebAppEntity"); // Ajusta la ruta según sea necesario

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath) // Establecer el directorio base
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<DBDProductos>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection")); // Asegúrate de que el nombre de la cadena de conexión sea correcto

            return new DBDProductos(optionsBuilder.Options);
        }
    }
}
