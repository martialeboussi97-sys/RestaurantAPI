namespace RestaurantAPI.DTOs;

public class CreateFactureDto
{
    public int IdCommande { get; set; }

    public decimal? Remise { get; set; }

    public int? IdTaxe { get; set; }
}