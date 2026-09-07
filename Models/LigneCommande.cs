using System;
using System.Collections.Generic;

namespace RestaurantAPI.Models;

public partial class LigneCommande
{
    public int IdLigneCommande { get; set; }

    public int IdCommande { get; set; }

    public int IdPlat { get; set; }

    public int Quantite { get; set; }

    public virtual Commande IdCommandeNavigation { get; set; } = null!;

    public virtual Plat IdPlatNavigation { get; set; } = null!;

    public virtual ICollection<LigneCommandeOption> LigneCommandeOptions { get; set; } = new List<LigneCommandeOption>();
}
