using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Data;
using RestaurantAPI.DTOs;
using RestaurantAPI.Models;

namespace RestaurantAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaiementsController : ControllerBase
{
    private readonly RestaurantDbContext _context;

    public PaiementsController(RestaurantDbContext context)
    {
        _context = context;
    }

    // GET: api/Paiements
    [HttpGet]
    public async Task<IActionResult> GetPaiements()
    {
        var paiements = await _context.Paiements
            .Include(p => p.IdFactureNavigation)
            .ToListAsync();

        return Ok(paiements);
    }

    // GET: api/Paiements/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPaiement(int id)
    {
        var paiement = await _context.Paiements
            .Include(p => p.IdFactureNavigation)
            .FirstOrDefaultAsync(p => p.IdPaiement == id);

        if (paiement == null)
            return NotFound("Paiement introuvable.");

        return Ok(paiement);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePaiement(
    [FromBody] CreatePaiementDto dto)
    {
        var factureExiste = await _context.Factures
            .AnyAsync(f => f.IdFacture == dto.IdFacture);

        if (!factureExiste)
            return BadRequest("La facture spécifiée n'existe pas.");

        if (dto.MontantPaye <= 0)
            return BadRequest("Le montant payé doit être supérieur à zéro.");

        var paiement = new Paiement
        {
            IdFacture = dto.IdFacture,
            MontantPaye = dto.MontantPaye,
            ModePaiement = dto.ModePaiement,
            Statut = dto.Statut,
            DatePaiement = DateTime.Now
        };

        _context.Paiements.Add(paiement);

        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetPaiement),
            new { id = paiement.IdPaiement },
            paiement
        );
    }

    // PUT: api/Paiements/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePaiement(
        int id,
        [FromBody] Paiement paiement)
    {
        var paiementExiste = await _context.Paiements
            .FindAsync(id);

        if (paiementExiste == null)
            return NotFound("Paiement introuvable.");

        var factureExiste = await _context.Factures
            .AnyAsync(f => f.IdFacture == paiement.IdFacture);

        if (!factureExiste)
            return BadRequest("La facture spécifiée n'existe pas.");

        if (paiement.MontantPaye <= 0)
            return BadRequest("Le montant payé doit être supérieur à zéro.");

        paiementExiste.IdFacture = paiement.IdFacture;
        paiementExiste.MontantPaye = paiement.MontantPaye;
        paiementExiste.ModePaiement = paiement.ModePaiement;
        paiementExiste.Statut = paiement.Statut;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/Paiements/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePaiement(int id)
    {
        var paiement = await _context.Paiements
            .FindAsync(id);

        if (paiement == null)
            return NotFound("Paiement introuvable.");

        _context.Paiements.Remove(paiement);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}