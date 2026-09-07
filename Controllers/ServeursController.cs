using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Data;
using RestaurantAPI.DTOs;
using RestaurantAPI.Models;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServeursController : ControllerBase
    {
        private readonly RestaurantDbContext _context;

        public ServeursController(RestaurantDbContext context)
        {
            _context = context;
        }

        // GET : api/Serveurs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Serveur>>> GetServeurs()
        {
            return await _context.Serveurs
                .Include(s => s.IdEmployeNavigation)
                .ToListAsync();
        }

        // GET : api/Serveurs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Serveur>> GetServeur(int id)
        {
            var serveur = await _context.Serveurs
                .Include(s => s.IdEmployeNavigation)
                .FirstOrDefaultAsync(s => s.IdServeur == id);

            if (serveur == null)
            {
                return NotFound(new
                {
                    message = "Serveur introuvable."
                });
            }

            return serveur;
        }

        // POST : api/Serveurs
        [HttpPost]
        public async Task<ActionResult<Serveur>> CreateServeur(CreateServeurDto dto)
        {
            // Vérifier que l'employé existe
            var employeExiste = await _context.Employes
                .AnyAsync(e => e.IdEmploye == dto.IdEmploye);

            if (!employeExiste)
            {
                return BadRequest(new
                {
                    message = "L'employé indiqué n'existe pas."
                });
            }

            var serveur = new Serveur
            {
                NumeroServeur = dto.NumeroServeur,
                IdEmploye = dto.IdEmploye
            };

            _context.Serveurs.Add(serveur);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetServeur),
                new { id = serveur.IdServeur },
                serveur
            );
        }

        // PUT : api/Serveurs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateServeur(
            int id,
            CreateServeurDto dto)
        {
            var serveur = await _context.Serveurs.FindAsync(id);

            if (serveur == null)
            {
                return NotFound(new
                {
                    message = "Serveur introuvable."
                });
            }

            // Vérifier que l'employé existe
            var employeExiste = await _context.Employes
                .AnyAsync(e => e.IdEmploye == dto.IdEmploye);

            if (!employeExiste)
            {
                return BadRequest(new
                {
                    message = "L'employé indiqué n'existe pas."
                });
            }

            serveur.NumeroServeur = dto.NumeroServeur;
            serveur.IdEmploye = dto.IdEmploye;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE : api/Serveurs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServeur(int id)
        {
            var serveur = await _context.Serveurs.FindAsync(id);

            if (serveur == null)
            {
                return NotFound(new
                {
                    message = "Serveur introuvable."
                });
            }

            _context.Serveurs.Remove(serveur);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}