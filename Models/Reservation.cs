using System;
using System.Collections.Generic;

namespace RestaurantAPI.Models;

public partial class Reservation
{
    public int IdReservation { get; set; }

    public int IdClient { get; set; }

    public int IdTable { get; set; }

    public DateOnly DateReservation { get; set; }

    public TimeOnly HeureReservation { get; set; }

    public int NombrePersonnes { get; set; }

    public string Statut { get; set; } = null!;

    public virtual Client IdClientNavigation { get; set; } = null!;

    public virtual TableRestaurant IdTableNavigation { get; set; } = null!;
}
