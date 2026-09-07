using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Data;
using RestaurantAPI.DTOs;
using RestaurantAPI.Models;

namespace RestaurantAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReservationsController : ControllerBase
{
    private readonly RestaurantDbContext _context;

    public ReservationsController(RestaurantDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetReservations()
    {
        var reservations = await _context.Reservations
            .Include(r => r.IdClientNavigation)
            .Include(r => r.IdTableNavigation)
            .ToListAsync();

        return Ok(reservations);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetReservation(int id)
    {
        var reservation = await _context.Reservations
            .Include(r => r.IdClientNavigation)
            .Include(r => r.IdTableNavigation)
            .FirstOrDefaultAsync(r => r.IdReservation == id);

        if (reservation == null)
            return NotFound("Réservation introuvable.");

        return Ok(reservation);
    }

    [HttpPost]
    public async Task<IActionResult> CreateReservation(
        [FromBody] CreateReservationDto dto)
    {
        var clientExiste = await _context.Clients
            .AnyAsync(c => c.IdClient == dto.IdClient);

        if (!clientExiste)
            return BadRequest("Le client n'existe pas.");

        var tableExiste = await _context.TableRestaurants
            .AnyAsync(t => t.IdTable == dto.IdTable);

        if (!tableExiste)
            return BadRequest("La table n'existe pas.");

        var reservation = new Reservation
        {
            IdClient = dto.IdClient,
            IdTable = dto.IdTable,
            DateReservation = dto.DateReservation,
            HeureReservation = dto.HeureReservation,
            NombrePersonnes = dto.NombrePersonnes,
            Statut = dto.Statut
        };
        // Vérifier si la table est déjà réservée
        var reservationExiste = await _context.Reservations
            .AnyAsync(r =>
                r.IdTable == dto.IdTable &&
                r.DateReservation == dto.DateReservation &&
                r.HeureReservation == dto.HeureReservation &&
                r.Statut != "Annulée");

        if (reservationExiste)
            return BadRequest("Cette table est déjà réservée à cette date et cette heure.");

        _context.Reservations.Add(reservation);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetReservation),
            new { id = reservation.IdReservation },
            reservation);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateReservation(
        int id,
        [FromBody] CreateReservationDto dto)
    {
        var reservation = await _context.Reservations.FindAsync(id);

        if (reservation == null)
            return NotFound("Réservation introuvable.");

        reservation.IdClient = dto.IdClient;
        reservation.IdTable = dto.IdTable;
        reservation.DateReservation = dto.DateReservation;
        reservation.HeureReservation = dto.HeureReservation;
        reservation.NombrePersonnes = dto.NombrePersonnes;
        reservation.Statut = dto.Statut;

        await _context.SaveChangesAsync();

        return NoContent();
        var reservationExiste = await _context.Reservations
    .AnyAsync(r =>
        r.IdReservation != id &&
        r.IdTable == dto.IdTable &&
        r.DateReservation == dto.DateReservation &&
        r.HeureReservation == dto.HeureReservation &&
        r.Statut != "Annulée");

        if (reservationExiste)
            return BadRequest("Cette table est déjà réservée à cette date et cette heure.");
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReservation(int id)
    {
        var reservation = await _context.Reservations.FindAsync(id);

        if (reservation == null)
            return NotFound("Réservation introuvable.");

        _context.Reservations.Remove(reservation);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}