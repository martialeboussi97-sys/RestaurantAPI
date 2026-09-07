using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Data;
using RestaurantAPI.DTOs;
using RestaurantAPI.Models;

namespace RestaurantAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TypeCommandesController : ControllerBase
{
    private readonly RestaurantDbContext _context;

    public TypeCommandesController(RestaurantDbContext context)
    {
        _context = context;
    }

    // GET: api/TypeCommandes
    [HttpGet]
    public async Task<IActionResult> GetTypeCommandes()
    {
        var types = await _context.TypeCommandes.ToListAsync();

        return Ok(types);
    }

    // GET: api/TypeCommandes/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTypeCommande(int id)
    {
        var typeCommande = await _context.TypeCommandes.FindAsync(id);

        if (typeCommande == null)
            return NotFound("Type de commande introuvable.");

        return Ok(typeCommande);
    }

    // POST: api/TypeCommandes
    [HttpPost]
    public async Task<IActionResult> CreateTypeCommande(
        [FromBody] CreateTypeCommandeDto dto)
    {
        var typeCommande = new TypeCommande
        {
            NomType = dto.NomType,
            Description = dto.Description
        };

        _context.TypeCommandes.Add(typeCommande);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetTypeCommande),
            new { id = typeCommande.IdTypeCommande },
            typeCommande
        );
    }

    // PUT: api/TypeCommandes/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTypeCommande(
        int id,
        [FromBody] CreateTypeCommandeDto dto)
    {
        var typeCommande = await _context.TypeCommandes.FindAsync(id);

        if (typeCommande == null)
            return NotFound("Type de commande introuvable.");

        typeCommande.NomType = dto.NomType;
        typeCommande.Description = dto.Description;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/TypeCommandes/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTypeCommande(int id)
    {
        var typeCommande = await _context.TypeCommandes.FindAsync(id);

        if (typeCommande == null)
            return NotFound("Type de commande introuvable.");

        _context.TypeCommandes.Remove(typeCommande);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}