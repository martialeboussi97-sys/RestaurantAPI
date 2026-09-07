using System;
using System.Collections.Generic;

namespace RestaurantAPI.Models;

public partial class Avi
{
    public int IdAvis { get; set; }

    public int IdClient { get; set; }

    public int? IdCommande { get; set; }

    public int Note { get; set; }

    public string? Commentaire { get; set; }

    public DateTime? DateAvis { get; set; }

    public virtual Client IdClientNavigation { get; set; } = null!;

    public virtual Commande? IdCommandeNavigation { get; set; }
}
