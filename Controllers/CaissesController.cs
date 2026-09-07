using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Data;
using RestaurantAPI.DTOs;
using RestaurantAPI.Models;

namespace RestaurantAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CaissesController : ControllerBase
{
    private readonly RestaurantDbContext _context;

    public CaissesController(RestaurantDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetCaisses()
    {
        return Ok(await _context.Caisses.ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCaisse(int id)
    {
        var caisse = await _context.Caisses.FindAsync(id);

        if (caisse == null)
            return NotFound("Caisse introuvable.");

        return Ok(caisse);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCaisse(
        [FromBody] CreateCaisseDto dto)
    {
        var caisse = new Caisse
        {
            NomCaisse = dto.NomCaisse,
            Emplacement = dto.Emplacement,
            Statut = dto.Statut
        };

        _context.Caisses.Add(caisse);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetCaisse),
            new { id = caisse.IdCaisse },
            caisse);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCaisse(
        int id,
        [FromBody] CreateCaisseDto dto)
    {
        var caisse = await _context.Caisses.FindAsync(id);

        if (caisse == null)
            return NotFound("Caisse introuvable.");

        caisse.NomCaisse = dto.NomCaisse;
        caisse.Emplacement = dto.Emplacement;
        caisse.Statut = dto.Statut;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCaisse(int id)
    {
        var caisse = await _context.Caisses.FindAsync(id);

        if (caisse == null)
            return NotFound("Caisse introuvable.");

        _context.Caisses.Remove(caisse);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}