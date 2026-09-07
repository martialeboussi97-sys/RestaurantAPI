using System;
using System.Collections.Generic;

namespace RestaurantAPI.Models;

public partial class Serveur
{
    public int IdServeur { get; set; }

    public string NumeroServeur { get; set; } = null!;

    public int IdEmploye { get; set; }

    public virtual ICollection<Commande> Commandes { get; set; } = new List<Commande>();

    public virtual Employe IdEmployeNavigation { get; set; } = null!;
}
