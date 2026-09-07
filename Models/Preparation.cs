using System;
using System.Collections.Generic;

namespace RestaurantAPI.Models;

public partial class Preparation
{
    public int IdPreparation { get; set; }

    public int IdTicket { get; set; }

    public DateTime? HeureDebut { get; set; }

    public DateTime? HeureFin { get; set; }

    public string Statut { get; set; } = null!;

    public virtual TicketCuisine IdTicketNavigation { get; set; } = null!;
}
