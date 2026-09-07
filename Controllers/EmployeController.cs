using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Data;
using RestaurantAPI.DTOs;
using RestaurantAPI.Models;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployesController : ControllerBase
    {
        private readonly RestaurantDbContext _context;

        public EmployesController(RestaurantDbContext context)
        {
            _context = context;
        }

        // GET : api/Employes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employe>>> GetEmployes()
        {
            return await _context.Employes
                .Include(e => e.IdPosteNavigation)
                .ToListAsync();
        }

        // GET : api/Employes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employe>> GetEmploye(int id)
        {
            var employe = await _context.Employes
                .Include(e => e.IdPosteNavigation)
                .FirstOrDefaultAsync(e => e.IdEmploye == id);

            if (employe == null)
            {
                return NotFound(new
                {
                    message = "Employé introuvable."
                });
            }

            return employe;
        }

        // POST : api/Employes
        [HttpPost]
        public async Task<ActionResult<Employe>> CreateEmploye(
            CreateEmployeDto dto)
        {
            var posteExiste = await _context.Postes
                .AnyAsync(p => p.IdPoste == dto.IdPoste);

            if (!posteExiste)
            {
                return BadRequest(new
                {
                    message = "Le poste indiqué n'existe pas."
                });
            }

            var employe = new Employe
            {
                NumeroEmploye = dto.NumeroEmploye,
                Nom = dto.Nom,
                Prenom = dto.Prenom,
                IdPoste = dto.IdPoste
            };

            _context.Employes.Add(employe);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetEmploye),
                new { id = employe.IdEmploye },
                employe
            );
        }

        // PUT : api/Employes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmploye(
            int id,
            CreateEmployeDto dto)
        {
            var employe = await _context.Employes.FindAsync(id);

            if (employe == null)
            {
                return NotFound(new
                {
                    message = "Employé introuvable."
                });
            }

            var posteExiste = await _context.Postes
                .AnyAsync(p => p.IdPoste == dto.IdPoste);

            if (!posteExiste)
            {
                return BadRequest(new
                {
                    message = "Le poste indiqué n'existe pas."
                });
            }

            employe.NumeroEmploye = dto.NumeroEmploye;
            employe.Nom = dto.Nom;
            employe.Prenom = dto.Prenom;
            employe.IdPoste = dto.IdPoste;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE : api/Employes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmploye(int id)
        {
            var employe = await _context.Employes.FindAsync(id);

            if (employe == null)
            {
                return NotFound(new
                {
                    message = "Employé introuvable."
                });
            }

            _context.Employes.Remove(employe);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}