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
    }
}