using System;
using System.Collections.Generic;

namespace RestaurantAPI.Models;

public partial class Livreur
{
    public int IdLivreur { get; set; }

    public string NumeroLivreur { get; set; } = null!;

    public int IdEmploye { get; set; }

    public string? MoyenTransport { get; set; }

    public string Statut { get; set; } = null!;

    public virtual Employe IdEmployeNavigation { get; set; } = null!;

    public virtual ICollection<Livraison> Livraisons { get; set; } = new List<Livraison>();
}
