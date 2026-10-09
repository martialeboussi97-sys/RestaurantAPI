namespace RestaurantAPI.DTOs;

public class CreateCommandeDto
{
    public int? IdClient { get; set; }

    public int? IdTable { get; set; }

    public int? IdServeur { get; set; }

    public string? Statut { get; set; }

    public string? Remarque { get; set; }

    public int? IdTypeCommande { get; set; }
}