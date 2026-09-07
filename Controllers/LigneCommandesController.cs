using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Data;
using RestaurantAPI.DTOs;
using RestaurantAPI.Models;

namespace RestaurantAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LigneCommandesController : ControllerBase
{
    private readonly RestaurantDbContext _context;

    public LigneCommandesController(RestaurantDbContext context)
    {
        _context = context;
    }

    // GET: api/LigneCommandes
    [HttpGet]
    public async Task<IActionResult> GetLigneCommandes()
    {
        var lignes = await _context.LigneCommandes
            .Include(l => l.IdCommandeNavigation)
            .Include(l => l.IdPlatNavigation)
            .ToListAsync();

        return Ok(lignes);
    }

    // GET: api/LigneCommandes/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetLigneCommande(int id)
    {
        var ligne = await _context.LigneCommandes
            .Include(l => l.IdCommandeNavigation)
            .Include(l => l.IdPlatNavigation)
            .FirstOrDefaultAsync(l => l.IdLigneCommande == id);

        if (ligne == null)
            return NotFound("Ligne de commande introuvable.");

        return Ok(ligne);
    }

    // POST: api/LigneCommandes
    [HttpPost]
    public async Task<IActionResult> CreateLigneCommande(
        [FromBody] CreateLigneCommandeDto dto)
    {
        // Vérifier que la commande existe
        var commandeExiste = await _context.Commandes
            .AnyAsync(c => c.IdCommande == dto.IdCommande);

        if (!commandeExiste)
            return BadRequest("La commande spécifiée n'existe pas.");

        // Vérifier que le plat existe
        var platExiste = await _context.Plats
            .AnyAsync(p => p.IdPlat == dto.IdPlat);

        if (!platExiste)
            return BadRequest("Le plat spécifié n'existe pas.");

        // Vérifier que la quantité est valide
        if (dto.Quantite <= 0)
            return BadRequest("La quantité doit être supérieure à zéro.");

        var ligneCommande = new LigneCommande
        {
            IdCommande = dto.IdCommande,
            IdPlat = dto.IdPlat,
            Quantite = dto.Quantite
        };

        _context.LigneCommandes.Add(ligneCommande);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetLigneCommande),
            new { id = ligneCommande.IdLigneCommande },
            ligneCommande
        );
    }

    // PUT: api/LigneCommandes/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLigneCommande(
        int id,
        [FromBody] CreateLigneCommandeDto dto)
    {
        var ligneCommande = await _context.LigneCommandes.FindAsync(id);

        if (ligneCommande == null)
            return NotFound("Ligne de commande introuvable.");

        // Vérifier que la commande existe
        var commandeExiste = await _context.Commandes
            .AnyAsync(c => c.IdCommande == dto.IdCommande);

        if (!commandeExiste)
            return BadRequest("La commande spécifiée n'existe pas.");

        // Vérifier que le plat existe
        var platExiste = await _context.Plats
            .AnyAsync(p => p.IdPlat == dto.IdPlat);

        if (!platExiste)
            return BadRequest("Le plat spécifié n'existe pas.");

        if (dto.Quantite <= 0)
            return BadRequest("La quantité doit être supérieure à zéro.");

        ligneCommande.IdCommande = dto.IdCommande;
        ligneCommande.IdPlat = dto.IdPlat;
        ligneCommande.Quantite = dto.Quantite;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/LigneCommandes/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLigneCommande(int id)
    {
        var ligneCommande = await _context.LigneCommandes.FindAsync(id);

        if (ligneCommande == null)
            return NotFound("Ligne de commande introuvable.");

        _context.LigneCommandes.Remove(ligneCommande);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}