using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Data;
using RestaurantAPI.DTOs;
using RestaurantAPI.Models;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZonesController : ControllerBase
    {
        private readonly RestaurantDbContext _context;

        public ZonesController(RestaurantDbContext context)
        {
            _context = context;
        }

        // GET : api/Zones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Zone>>> GetZones()
        {
            return await _context.Zones
                .Include(z => z.IdSalleNavigation)
                .ToListAsync();
        }

        // GET : api/Zones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Zone>> GetZone(int id)
        {
            var zone = await _context.Zones
                .Include(z => z.IdSalleNavigation)
                .FirstOrDefaultAsync(z => z.IdZone == id);

            if (zone == null)
            {
                return NotFound(new
                {
                    message = "Zone introuvable."
                });
            }

            return zone;
        }

        // POST : api/Zones
        [HttpPost]
        public async Task<ActionResult<Zone>> CreateZone(CreateZoneDto dto)
        {
            var salleExiste = await _context.Salles
                .AnyAsync(s => s.IdSalle == dto.IdSalle);

            if (!salleExiste)
            {
                return BadRequest(new
                {
                    message = "La salle indiquée n'existe pas."
                });
            }

            var zone = new Zone
            {
                NomZone = dto.NomZone,
                IdSalle = dto.IdSalle
            };

            _context.Zones.Add(zone);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetZone),
                new { id = zone.IdZone },
                zone
            );
        }

        // PUT : api/Zones/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateZone(int id, CreateZoneDto dto)
        {
            var zone = await _context.Zones.FindAsync(id);

            if (zone == null)
            {
                return NotFound(new
                {
                    message = "Zone introuvable."
                });
            }

            var salleExiste = await _context.Salles
                .AnyAsync(s => s.IdSalle == dto.IdSalle);

            if (!salleExiste)
            {
                return BadRequest(new
                {
                    message = "La salle indiquée n'existe pas."
                });
            }

            zone.NomZone = dto.NomZone;
            zone.IdSalle = dto.IdSalle;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE : api/Zones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteZone(int id)
        {
            var zone = await _context.Zones.FindAsync(id);

            if (zone == null)
            {
                return NotFound(new
                {
                    message = "Zone introuvable."
                });
            }

            _context.Zones.Remove(zone);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}