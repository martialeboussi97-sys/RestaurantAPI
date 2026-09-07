using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Data;
using RestaurantAPI.Models;
using RestaurantAPI.DTOs;

namespace RestaurantAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlatsController : ControllerBase
{
    private readonly RestaurantDbContext _context;

    public PlatsController(RestaurantDbContext context)
    {
        _context = context;
    }

    // GET: api/Plats
    [HttpGet]
    public async Task<IActionResult> GetPlats()
    {
        var plats = await _context.Plats
            .Include(p => p.IdCategorieNavigation)
            .ToListAsync();

        return Ok(plats);
    }

    // GET: api/Plats/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPlat(int id)
    {
        var plat = await _context.Plats
            .Include(p => p.IdCategorieNavigation)
            .FirstOrDefaultAsync(p => p.IdPlat == id);

        if (plat == null)
            return NotFound();

        return Ok(plat);
    }

    // POST: api/Plats
    [HttpPost]
    public async Task<IActionResult> CreatePlat([FromBody] CreatePlatDto dto)
    {
        // Vérifier que la catégorie existe
        var categorieExiste = await _context.Categories
            .AnyAsync(c => c.IdCategorie == dto.IdCategorie);

        if (!categorieExiste)
            return BadRequest("La catégorie spécifiée n'existe pas.");

        // Créer l'objet Plat à partir du DTO
        var plat = new Plat
        {
            NomPlat = dto.NomPlat,
            Prix = dto.Prix,
            IdCategorie = dto.IdCategorie,
            TempsPreparation = dto.TempsPreparation
        };

        _context.Plats.Add(plat);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetPlat),
            new { id = plat.IdPlat },
            plat
        );
    }

    // PUT: api/Plats/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePlat(
        int id,
        [FromBody] UpdatePlatDto dto)
    {
        var plat = await _context.Plats.FindAsync(id);

        if (plat == null)
            return NotFound("Plat introuvable.");

        var categorieExiste = await _context.Categories
            .AnyAsync(c => c.IdCategorie == dto.IdCategorie);

        if (!categorieExiste)
            return BadRequest("La catégorie spécifiée n'existe pas.");

        // Modification des informations
        plat.NomPlat = dto.NomPlat;
        plat.Prix = dto.Prix;
        plat.IdCategorie = dto.IdCategorie;
        plat.TempsPreparation = dto.TempsPreparation;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/Plats/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePlat(int id)
    {
        var plat = await _context.Plats.FindAsync(id);

        if (plat == null)
            return NotFound();

        _context.Plats.Remove(plat);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}