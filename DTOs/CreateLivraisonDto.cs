namespace RestaurantAPI.DTOs;

public class CreateLivraisonDto
{
    public int IdCommande { get; set; }

    public string AdresseLivraison { get; set; } = null!;

    public string TelephoneClient { get; set; } = null!;

    public string Statut { get; set; } = null!;

    public decimal? FraisLivraison { get; set; }

    public string? Remarque { get; set; }

    public int? IdLivreur { get; set; }
}