using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Data;
using RestaurantAPI.DTOs;
using RestaurantAPI.Models;

namespace RestaurantAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FacturesController : ControllerBase
{
    private readonly RestaurantDbContext _context;

    public FacturesController(RestaurantDbContext context)
    {
        _context = context;
    }

    // GET: api/Factures
    [HttpGet]
    public async Task<IActionResult> GetFactures()
    {
        var factures = await _context.Factures
            .Include(f => f.IdCommandeNavigation)
            .Include(f => f.IdTaxeNavigation)
            .ToListAsync();

        return Ok(factures);
    }

    // GET: api/Factures/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetFacture(int id)
    {
        var facture = await _context.Factures
            .Include(f => f.IdCommandeNavigation)
            .Include(f => f.IdTaxeNavigation)
            .FirstOrDefaultAsync(f => f.IdFacture == id);

        if (facture == null)
            return NotFound("Facture introuvable.");

        return Ok(facture);
    }

    // POST: api/Factures
    [HttpPost]
    public async Task<IActionResult> CreateFacture(
        [FromBody] CreateFactureDto dto)
    {
        // 1. Vérifier que la commande existe
        var commandeExiste = await _context.Commandes
            .AnyAsync(c => c.IdCommande == dto.IdCommande);

        if (!commandeExiste)
            return BadRequest("La commande spécifiée n'existe pas.");

        // 2. Récupérer les lignes de la commande avec les plats
        var lignesCommande = await _context.LigneCommandes
            .Include(l => l.IdPlatNavigation)
            .Where(l => l.IdCommande == dto.IdCommande)
            .ToListAsync();

        // 3. Vérifier que la commande contient au moins une ligne
        if (!lignesCommande.Any())
            return BadRequest(
                "Impossible de créer la facture : la commande ne contient aucune ligne."
            );

        // 4. Calculer le montant total
        decimal montantTotal = lignesCommande.Sum(
            l => l.IdPlatNavigation.Prix * l.Quantite
        );

        // 5. Vérifier la remise
        decimal remise = dto.Remise ?? 0;

        if (remise < 0)
            return BadRequest("La remise ne peut pas être négative.");

        if (remise > montantTotal)
            return BadRequest(
                "La remise ne peut pas être supérieure au montant total."
            );

        // 6. Calculer le montant après remise
        decimal montantApresRemise = montantTotal - remise;

        // 7. Vérifier et récupérer la taxe
        decimal montantTaxe = 0;
        Taxe? taxe = null;

        if (dto.IdTaxe.HasValue)
        {
            taxe = await _context.Taxes
                .FirstOrDefaultAsync(t => t.IdTaxe == dto.IdTaxe.Value);

            if (taxe == null)
                return BadRequest("La taxe spécifiée n'existe pas.");

            // Calcul de la taxe
            montantTaxe = montantApresRemise * taxe.Taux / 100;
        }

        // 8. Calculer le montant net
        decimal montantNet = montantApresRemise + montantTaxe;

        // 9. Créer la facture
        var facture = new Facture
        {
            IdCommande = dto.IdCommande,
            DateFacture = DateTime.Now,
            MontantTotal = montantTotal,
            Remise = remise,
            MontantNet = montantNet,
            IdTaxe = dto.IdTaxe
        };

        _context.Factures.Add(facture);

        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetFacture),
            new { id = facture.IdFacture },
            facture
        );
    }

    // PUT: api/Factures/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateFacture(
        int id,
        [FromBody] CreateFactureDto dto)
    {
        // 1. Vérifier que la facture existe
        var facture = await _context.Factures
            .FirstOrDefaultAsync(f => f.IdFacture == id);

        if (facture == null)
            return NotFound("Facture introuvable.");

        // 2. Vérifier que la commande existe
        var commandeExiste = await _context.Commandes
            .AnyAsync(c => c.IdCommande == dto.IdCommande);

        if (!commandeExiste)
            return BadRequest("La commande spécifiée n'existe pas.");

        // 3. Récupérer les lignes de commande
        var lignesCommande = await _context.LigneCommandes
            .Include(l => l.IdPlatNavigation)
            .Where(l => l.IdCommande == dto.IdCommande)
            .ToListAsync();

        if (!lignesCommande.Any())
            return BadRequest(
                "Impossible de modifier la facture : la commande ne contient aucune ligne."
            );

        // 4. Recalculer le montant total
        decimal montantTotal = lignesCommande.Sum(
            l => l.IdPlatNavigation.Prix * l.Quantite
        );

        // 5. Vérifier la remise
        decimal remise = dto.Remise ?? 0;

        if (remise < 0)
            return BadRequest("La remise ne peut pas être négative.");

        if (remise > montantTotal)
            return BadRequest(
                "La remise ne peut pas être supérieure au montant total."
            );

        // 6. Calculer le montant après remise
        decimal montantApresRemise = montantTotal - remise;

        // 7. Calculer la taxe
        decimal montantTaxe = 0;

        if (dto.IdTaxe.HasValue)
        {
            var taxe = await _context.Taxes
                .FirstOrDefaultAsync(t => t.IdTaxe == dto.IdTaxe.Value);

            if (taxe == null)
                return BadRequest("La taxe spécifiée n'existe pas.");

            montantTaxe = montantApresRemise * taxe.Taux / 100;
        }

        // 8. Calculer le montant net
        decimal montantNet = montantApresRemise + montantTaxe;

        // 9. Mettre à jour la facture
        facture.IdCommande = dto.IdCommande;
        facture.MontantTotal = montantTotal;
        facture.Remise = remise;
        facture.MontantNet = montantNet;
        facture.IdTaxe = dto.IdTaxe;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/Factures/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFacture(int id)
    {
        var facture = await _context.Factures.FindAsync(id);

        if (facture == null)
            return NotFound("Facture introuvable.");

        _context.Factures.Remove(facture);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}