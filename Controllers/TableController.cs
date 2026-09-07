using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Data;
using RestaurantAPI.DTOs;
using RestaurantAPI.Models;

namespace RestaurantAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TableRestaurantsController : ControllerBase
{
    private readonly RestaurantDbContext _context;

    public TableRestaurantsController(RestaurantDbContext context)
    {
        _context = context;
    }

    // GET: api/TableRestaurants
    [HttpGet]
    public async Task<IActionResult> GetTableRestaurants()
    {
        var tables = await _context.TableRestaurants
            .Include(t => t.IdZoneNavigation)
            .ToListAsync();

        return Ok(tables);
    }

    // GET: api/TableRestaurants/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTableRestaurant(int id)
    {
        var table = await _context.TableRestaurants
            .Include(t => t.IdZoneNavigation)
            .FirstOrDefaultAsync(t => t.IdTable == id);

        if (table == null)
            return NotFound("Table introuvable.");

        return Ok(table);
    }

    // POST: api/TableRestaurants
    [HttpPost]
    public async Task<IActionResult> CreateTableRestaurant(
        [FromBody] CreateTableRestaurantDto dto)
    {
        // Vérifier que la zone existe
        var zoneExiste = await _context.Zones
            .AnyAsync(z => z.IdZone == dto.IdZone);

        if (!zoneExiste)
            return BadRequest("La zone spécifiée n'existe pas.");

        var tableRestaurant = new TableRestaurant
        {
            NumeroTable = dto.NumeroTable,
            Capacite = dto.Capacite,
            Statut = dto.Statut,
            IdZone = dto.IdZone
        };

        _context.TableRestaurants.Add(tableRestaurant);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetTableRestaurant),
            new { id = tableRestaurant.IdTable },
            tableRestaurant
        );
    }

    // PUT: api/TableRestaurants/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTableRestaurant(
        int id,
        [FromBody] CreateTableRestaurantDto dto)
    {
        var tableRestaurant = await _context.TableRestaurants.FindAsync(id);

        if (tableRestaurant == null)
            return NotFound("Table introuvable.");

        // Vérifier que la zone existe
        var zoneExiste = await _context.Zones
            .AnyAsync(z => z.IdZone == dto.IdZone);

        if (!zoneExiste)
            return BadRequest("La zone spécifiée n'existe pas.");

        tableRestaurant.NumeroTable = dto.NumeroTable;
        tableRestaurant.Capacite = dto.Capacite;
        tableRestaurant.Statut = dto.Statut;
        tableRestaurant.IdZone = dto.IdZone;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/TableRestaurants/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTableRestaurant(int id)
    {
        var tableRestaurant = await _context.TableRestaurants.FindAsync(id);

        if (tableRestaurant == null)
            return NotFound("Table introuvable.");

        _context.TableRestaurants.Remove(tableRestaurant);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}