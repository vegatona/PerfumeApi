using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PerfumeApi.Data;
using PerfumeApi.Models;

namespace PerfumeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerfumesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PerfumesController(AppDbContext context)
        {
            _context = context;
        }

        // Endpoint para listar perfumes con stock > 0
        [HttpGet("stock")]
        public async Task<ActionResult<IEnumerable<Perfume>>> GetConStock()
        {
            // Buscamos en la tabla 'perfumes' donde la columna 'stock' sea mayor a 0
            var lista = await _context.Perfumes
                .Where(p => p.Stock > 0)
                .ToListAsync();

            return Ok(lista);
        }

        // Obtener un método de pago específico por su ID de tarjeta
        [HttpGet("pagos/{idtarjeta}")]
        public async Task<ActionResult<MetodoPago>> GetPagoPorId(long idtarjeta)
        {
            // Buscamos directamente por la llave primaria (idtarjeta)
            var pago = await _context.MetodosPago.FindAsync(idtarjeta);

            if (pago == null)
            {
                return NotFound($"No se encontró ninguna tarjeta con el ID {idtarjeta}");
            }

            return Ok(pago);
        }

        // Eliminar por idtarjeta
        [HttpDelete("pagos/{idtarjeta}")]
        public async Task<IActionResult> DeletePago(long idtarjeta)
        {
            // 1. Buscamos el registro en PostgreSQL usando la llave primaria
            var pago = await _context.MetodosPago.FindAsync(idtarjeta);

            // 2. Si no existe, devolvemos un error 404
            if (pago == null)
            {
                return NotFound($"No se pudo eliminar: La tarjeta con ID {idtarjeta} no existe.");
            }

            // 3. Lo removemos del contexto de datos
            _context.MetodosPago.Remove(pago);

            // 4. Guardamos los cambios físicamente en la base de datos
            await _context.SaveChangesAsync();

            // 5. Devolvemos un mensaje de éxito
            return Ok(new
            {
                Mensaje = "Eliminación exitosa",
                IdTarjetaEliminada = idtarjeta,
                FechaOperacion = DateTime.Now
            });
        }

        // Buscar la dirección de envío de un usuario por su ID
        [HttpGet("usuarios/{idusuario}/direccion")]
        public async Task<ActionResult<string>> GetDireccionUsuario(int idusuario)
        {
            // Buscamos al usuario por su ID
            var usuario = await _context.Usuarios.FindAsync(idusuario);

            if (usuario == null)
            {
                return NotFound($"No se encontró el usuario con ID {idusuario}");
            }

            // Devolvemos un objeto anónimo para que el JSON sea claro en Swagger
            return Ok(new
            {
                IdUsuario = usuario.Id,
                Nombre = usuario.Nombre,
                DireccionEnvio = usuario.Direccion
            });
        }
    }
}