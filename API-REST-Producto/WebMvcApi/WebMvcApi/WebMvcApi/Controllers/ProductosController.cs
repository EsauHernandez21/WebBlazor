using Microsoft.AspNetCore.Mvc;
using WebMvcApi.Models;
using WebMvcApi.Services;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebMvcApi.Controllers
{
    public class ProductosController : Controller
    {
        private readonly ProductoService _productoService;

      
        public ProductosController(ProductoService productoService)
        {
            _productoService = productoService;
        }

        // Acción para listar los productos SQL
        /*public async Task<IActionResult> Index()
        {
            var productos = await _productoService.ObtenerProductosAsync();
            return View(productos);
        }*/

        public async Task<IActionResult> Index()
        {
            var productos = await _productoService.ObtenerProductosOracleAsync();
            return View(productos);
        }

        
        // Acción para ver los detalles de un producto SQL
        /*public async Task<IActionResult> Details(int id)
        {
            var producto = await _productoService.ObtenerProductoPorIdAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }*/


        // Acción para ver los detalles de un producto ORACLE
        public async Task<IActionResult> Details(int id)
        {
            var producto = await _productoService.ObtenerProductoPorIdOracleAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }


        //SQL BUSCAR POR ID
        /*public async Task<IActionResult> Buscar(string nombre)
        {
            int id = int.Parse(nombre);
            var producto = await _productoService.ObtenerProductoPorIdAsync(id); // Llama a un nuevo método de servicio para buscar productos


            // Envolver el producto en una lista
            var productos = new List<Producto> { producto };

            // Devuelve la vista Index con la lista de productos
            return View("Index", productos); // No es necesario convertir a lista si ya es IEnumerable<Producto>
        }*/

        public async Task<IActionResult> Buscar(string nombre)
        {
            int id = int.Parse(nombre);
            var producto = await _productoService.ObtenerProductoPorIdOracleAsync(id); // Llama a un nuevo método de servicio para buscar productos


            // Envolver el producto en una lista
            var productos = new List<Producto> { producto };

            // Devuelve la vista Index con la lista de productos
            return View("Index", productos); // No es necesario convertir a lista si ya es IEnumerable<Producto>
        }

        //Update SQL SERVER
        /* public async Task<IActionResult> Edit(int id)
         {

             var producto = await _productoService.ObtenerProductoPorIdAsync(id);

             if (producto == null)
             {
                 return NotFound();
             }


             return View(producto); // Pasa el producto a la vista
         }


         // POST: Productos/Edit/5
         [HttpPost] // Asegúrate de tener el atributo HttpPost aquí
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> Edit(int id, Producto producto)
         {
             if (id != producto.ProductoId)
             {
                 return NotFound();
             }

             if (ModelState.IsValid)
             {
                 var result = await _productoService.ActualizarProductoAsync(id, producto);
                 if (result)
                 {
                     return RedirectToAction(nameof(Index));
                 }
                 ModelState.AddModelError("", "Error al actualizar el producto.");
             }
             return View(producto);
         }*/


        public async Task<IActionResult> Edit(int id)
        {
            // Obtener el producto por ID
            var producto = await _productoService.ObtenerProductoPorIdOracleAsync(id);

            // Verificar si el producto existe
            if (producto == null)
            {
                return NotFound();
            }

            // Devolver la vista de edición con el producto
            return View(producto); // Pasa el producto a la vista
        }


        // UPDATE ORACCLE
        [HttpPost] // Asegúrate de tener el atributo HttpPost aquí
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Producto producto)
        {
            if (id != producto.ProductoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var result = await _productoService.ActualizarProductoOracleAsync(id, producto);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Error al actualizar el producto.");
            }
            return View(producto);
        }

        

        // GET: Productos/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Productos/Create SQL
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Producto producto)
        {
            if (ModelState.IsValid)
            {
                var result = await _productoService.CrearProductoAsync(producto);
                if (result)
                {
                    return RedirectToAction(nameof(Index)); 
                }
                ModelState.AddModelError("", "Error al crear el producto.");
            }
            return View(producto); 
        }*/


        // POST: Productos/Create  Oracle
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Producto producto)
        {
            if (ModelState.IsValid)
            {
                var result = await _productoService.CrearProductoOracleAsync(producto);
                if (result)
                {
                    return RedirectToAction(nameof(Index)); 
                }
                ModelState.AddModelError("", "Error al crear el producto.");
            }
            return View(producto); 
        }



        //DELETE SQL SERVER
        [HttpPost]
        /*public async Task<IActionResult> Delete(int id)
        {
            var result = await _productoService.EliminarProductoAsync(id);
            if (result)
            {
                
                return RedirectToAction("Index");
            }

           
            return NotFound();
        }*/


        //DELETE ORACLE
        public async Task<IActionResult> Delete(int id)
       {
           var result = await _productoService.EliminarProductoOracleAsync(id);
           if (result)
           {

               return RedirectToAction("Index");
           }


           return NotFound();
       }

        
    }
}
