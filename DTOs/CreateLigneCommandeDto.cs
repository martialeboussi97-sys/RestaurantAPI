namespace RestaurantAPI.DTOs;

public class CreateLigneCommandeDto
{
    public int IdCommande { get; set; }

    public int IdPlat { get; set; }

    public int Quantite { get; set; }
}