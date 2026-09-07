namespace RestaurantAPI.DTOs;

public class CreateClientDto
{
    public string Nom { get; set; } = null!;

    public string Prenom { get; set; } = null!;

    public string? Telephone { get; set; }

    public string? Email { get; set; }
}