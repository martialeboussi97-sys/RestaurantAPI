namespace RestaurantAPI.DTOs;

public class CreateReservationDto
{
    public int IdClient { get; set; }

    public int IdTable { get; set; }

    public DateOnly DateReservation { get; set; }

    public TimeOnly HeureReservation { get; set; }

    public int NombrePersonnes { get; set; }

    public string Statut { get; set; } = null!;
}