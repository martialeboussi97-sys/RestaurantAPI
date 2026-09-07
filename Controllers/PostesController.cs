using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Data;
using RestaurantAPI.DTOs;
using RestaurantAPI.Models;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostesController : ControllerBase
    {
        private readonly RestaurantDbContext _context;

        public PostesController(RestaurantDbContext context)
        {
            _context = context;
        }

        // GET : api/Postes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Poste>>> GetPostes()
        {
            return await _context.Postes.ToListAsync();
        }

        // GET : api/Postes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Poste>> GetPoste(int id)
        {
            var poste = await _context.Postes
                .FirstOrDefaultAsync(p => p.IdPoste == id);

            if (poste == null)
            {
                return NotFound(new
                {
                    message = "Poste introuvable."
                });
            }

            return poste;
        }

        // POST : api/Postes
        [HttpPost]
        public async Task<ActionResult<Poste>> CreatePoste(
            CreatePosteDto dto)
        {
            var poste = new Poste
            {
                NomPoste = dto.NomPoste,
                Description = dto.Description
            };

            _context.Postes.Add(poste);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetPoste),
                new { id = poste.IdPoste },
                poste
            );
        }

        // PUT : api/Postes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePoste(
            int id,
            CreatePosteDto dto)
        {
            var poste = await _context.Postes.FindAsync(id);

            if (poste == null)
            {
                return NotFound(new
                {
                    message = "Poste introuvable."
                });
            }

            poste.NomPoste = dto.NomPoste;
            poste.Description = dto.Description;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE : api/Postes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePoste(int id)
        {
            var poste = await _context.Postes.FindAsync(id);

            if (poste == null)
            {
                return NotFound(new
                {
                    message = "Poste introuvable."
                });
            }

            _context.Postes.Remove(poste);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}