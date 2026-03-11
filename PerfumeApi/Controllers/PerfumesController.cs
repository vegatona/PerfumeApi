using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
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