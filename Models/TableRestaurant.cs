using System;
using System.Collections.Generic;

namespace RestaurantAPI.Models;

public partial class TableRestaurant
{
    public int IdTable { get; set; }

    public int NumeroTable { get; set; }

    public int Capacite { get; set; }

    public string Statut { get; set; } = null!;

    public int IdZone { get; set; }

    public virtual ICollection<Commande> Commandes { get; set; } = new List<Commande>();

    public virtual Zone IdZoneNavigation { get; set; } = null!;

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
