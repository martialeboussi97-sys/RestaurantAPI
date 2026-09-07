namespace RestaurantAPI.DTOs;

public class CreateTicketCuisineDto
{
    public int IdCommande { get; set; }

    public string Statut { get; set; } = null!;
}