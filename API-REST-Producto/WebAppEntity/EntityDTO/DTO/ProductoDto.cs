using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityDTO.DTO
{
    public class ProductoDto
    {
        public int ProductoId { get; set; } // Esta propiedad se usará como clave primaria.
        public string Nombre { get; set; }
        public string Descripcion { get; set; } 
        public decimal Precio { get; set; }
        public int Stock { get; set; } 
    }
}
