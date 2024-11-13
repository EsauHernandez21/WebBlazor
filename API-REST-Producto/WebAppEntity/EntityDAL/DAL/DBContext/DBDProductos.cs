using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EntityModel.Model;
using EntityDTO.DTO;

namespace EntityDAL.DAL.DBContext
{
    public class DBDProductos:DbContext
    {
        public DBDProductos() : base(new DbContextOptions<DBDProductos>()) { }

        //El parámetro DbContextOptions<DBDProductos> recibe la configuración necesaria para que Entity Framework
        //pueda conectar y trabajar con la base de datos.

        //:base(options) llama al constructor de la clase base (DbContext) y le pasa las opciones (la configuración de la base
        //de datos). Esto es necesario porque la clase DBDProductos está heredando de DbContext
        public DBDProductos(DbContextOptions<DBDProductos> options) : base(options) { }

        //colección genérica  representa una entidad
        //ProductoDto:clase que define el modelo de datos que corresponde a una tabla 
        //Productos:Propiedad que actúa como un contenedor para los datos 
        public DbSet<ProductoDto> Productos { get; set; }


        //personalizar y configurar cómo las entidades de tu modelo
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Se indica  que se va trabajar con la entidad ProductoDto. Esto le dice a Entity Framework que se desea
            //configurar esa entidad 


            modelBuilder.Entity<ProductoDto>(entity =>
            {
                // Define la clave primaria
                entity.HasKey(p => p.ProductoId);

                //Define el tipo de columna para la propiedad Precio
                //Property: accedera una propiedad específica de la entidad
                //p => p.Precio indica que vas a configurar la propiedad Precio de la entidad ProductoDto
                entity.Property(p => p.Precio)
                      .HasColumnType("decimal(18,2)"); // Ajusta la precisión y la escala según sea necesario,especifica el tipo de datos
           });

           
        }

    }
}
