using EntityDAL.DAL.DBContext;
using EntityDAL.DAL.Repositories.Contratos;
using EntityDAL.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using EntityBLL.BLL;
using Oracle.ManagedDataAccess.Client;
using EntityDAL.DALOracle;
using EntityBLL.BLLOracle;


var builder = WebApplication.CreateBuilder(args);


// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTodo", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});


//Se encarga de registrar el contexto de base de datos (DBDProductos) en el contenedor de servicios de ASP.NET Core.
// Configura el DbContext utilizando la cadena de conexión desde appsettings.json es para la ejecución normal de la aplicación
builder.Services.AddDbContext<DBDProductos>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("EntityDAL"))); // Cambia el nombre de ensamblado si es necesario


//var oracleConnectionString = builder.Configuration.GetConnectionString("OracleTnsConnection");
var oracleConnectionString = builder.Configuration.GetConnectionString("OracleBasicConnection");

// Registrar OracleDbContext en el contenedor de servicios
builder.Services.AddSingleton<OracleDbContext>(provider => new OracleDbContext(oracleConnectionString));

// Configuración de la conexión para Oracle usando TNS
/*builder.Services.AddSingleton(provider =>
    new OracleConnection(builder.Configuration.GetConnectionString("OracleTnsConnection")));

// Configuración de la conexión para Oracle usando conexión básica (si necesitas ambas)
builder.Services.AddSingleton(provider =>
    new OracleConnection(builder.Configuration.GetConnectionString("OracleBasicConnection")));*/


builder.Services.AddScoped<IProductoOracleRepository, ProductoOracleRepository>(); // Repositorio de Oracle
builder.Services.AddScoped<IProductoServiceOracle, ProductoService_Oracle>(); // Servicio de Producto oracle


// Añadir servicios para controlar los productos
builder.Services.AddScoped<IProductoRepository, ProductoRepository>(); // Asegúrate de tener esta línea para el repositorio
builder.Services.AddScoped<IProductoService, ProductoService>();// Asegúrate de tener esta línea para el servicio


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Aplicar la política de CORS
app.UseCors("PermitirTodo");

// Configurar el pipeline HTTP aquí
app.UseAuthorization();
// Configurar el pipeline HTTP aquí (si es necesario)
app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.Run();
