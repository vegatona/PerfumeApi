using Microsoft.EntityFrameworkCore;
using PerfumeApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Forzamos la carga del archivo JSON
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// 1. Obtener la cadena desde appsettings.json
var connectionString = "Host=ep-divine-leaf-ai6pjs57-pooler.c-4.us-east-1.aws.neon.tech;Database=DonarumaDB;Username=neondb_owner;Password=npg_4nvHDatfA2FS;SSL Mode=Require;Trust Server Certificate=true";

// VALIDACIÓN: Si la cadena es nula, lanzamos un error personalizado
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("La cadena 'PostgresConnection' sigue siendo nula. Verifica que el archivo appsettings.json esté en la raíz.");
}

// 2. Registrar SOLO PostgreSQL (Borra la línea de UseInMemoryDatabase)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();