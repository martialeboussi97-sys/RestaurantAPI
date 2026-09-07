using System;
using System.Collections.Generic;

namespace RestaurantAPI.Models;

public partial class TicketCuisine
{
    public int IdTicket { get; set; }

    public int IdCommande { get; set; }

    public DateTime? DateEnvoi { get; set; }

    public string Statut { get; set; } = null!;

    public virtual Commande IdCommandeNavigation { get; set; } = null!;

    public virtual ICollection<Preparation> Preparations { get; set; } = new List<Preparation>();
}
