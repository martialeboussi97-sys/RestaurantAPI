using System;
using System.Collections.Generic;

namespace RestaurantAPI.Models;

public partial class Reclamation
{
    public int IdReclamation { get; set; }

    public int IdClient { get; set; }

    public int? IdCommande { get; set; }

    public string Sujet { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime? DateReclamation { get; set; }

    public string Statut { get; set; } = null!;

    public virtual Client IdClientNavigation { get; set; } = null!;

    public virtual Commande? IdCommandeNavigation { get; set; }
}
