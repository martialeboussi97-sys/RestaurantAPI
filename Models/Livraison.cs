using System;
using System.Collections.Generic;

namespace RestaurantAPI.Models;

public partial class Livraison
{
    public int IdLivraison { get; set; }

    public int IdCommande { get; set; }

    public string AdresseLivraison { get; set; } = null!;

    public string TelephoneClient { get; set; } = null!;

    public DateTime? DateLivraison { get; set; }

    public string Statut { get; set; } = null!;

    public decimal? FraisLivraison { get; set; }

    public string? Remarque { get; set; }

    public int? IdLivreur { get; set; }

    public virtual Commande IdCommandeNavigation { get; set; } = null!;

    public virtual Livreur? IdLivreurNavigation { get; set; }
}
