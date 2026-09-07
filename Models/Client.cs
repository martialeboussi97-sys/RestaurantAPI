using System;
using System.Collections.Generic;

namespace RestaurantAPI.Models;

public partial class Client
{
    public int IdClient { get; set; }

    public string Nom { get; set; } = null!;

    public string Prenom { get; set; } = null!;

    public string? Telephone { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Avi> Avis { get; set; } = new List<Avi>();

    public virtual ICollection<Commande> Commandes { get; set; } = new List<Commande>();

    public virtual ICollection<Reclamation> Reclamations { get; set; } = new List<Reclamation>();

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
