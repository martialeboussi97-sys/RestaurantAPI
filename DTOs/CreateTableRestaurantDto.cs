namespace RestaurantAPI.DTOs;

public class CreateTableRestaurantDto
{
    public int NumeroTable { get; set; }

    public int Capacite { get; set; }

    public string Statut { get; set; } = null!;

    public int IdZone { get; set; }
}