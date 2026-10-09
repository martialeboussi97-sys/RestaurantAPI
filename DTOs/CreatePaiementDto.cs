namespace RestaurantAPI.DTOs;

public class CreatePaiementDto
{
    public int IdFacture { get; set; }

    public decimal MontantPaye { get; set; }

    public string ModePaiement { get; set; } = null!;

    public string Statut { get; set; } = null!;
}