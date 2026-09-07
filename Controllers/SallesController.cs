using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Data;
using RestaurantAPI.DTOs;
using RestaurantAPI.Models;

namespace RestaurantAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SallesController : ControllerBase
{
    private readonly RestaurantDbContext _context;

    public SallesController(RestaurantDbContext context)
    {
        _context = context;
    }

    // GET: api/Salles
    [HttpGet]
    public async Task<IActionResult> GetSalles()
    {
        var salles = await _context.Salles.ToListAsync();

        return Ok(salles);
    }

    // GET: api/Salles/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetSalle(int id)
    {
        var salle = await _context.Salles.FindAsync(id);

        if (salle == null)
            return NotFound("Salle introuvable.");

        return Ok(salle);
    }

    // POST: api/Salles
    [HttpPost]
    public async Task<IActionResult> CreateSalle(
        [FromBody] CreateSalleDto dto)
    {
        var salle = new Salle
        {
            NomSalle = dto.NomSalle,
            Description = dto.Description
        };

        _context.Salles.Add(salle);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetSalle),
            new { id = salle.IdSalle },
            salle
        );
    }

    // PUT: api/Salles/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSalle(
        int id,
        [FromBody] CreateSalleDto dto)
    {
        var salle = await _context.Salles.FindAsync(id);

        if (salle == null)
            return NotFound("Salle introuvable.");

        salle.NomSalle = dto.NomSalle;
        salle.Description = dto.Description;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/Salles/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSalle(int id)
    {
        var salle = await _context.Salles.FindAsync(id);

        if (salle == null)
            return NotFound("Salle introuvable.");

        _context.Salles.Remove(salle);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}