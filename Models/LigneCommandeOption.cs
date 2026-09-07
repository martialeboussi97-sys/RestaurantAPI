using System;
using System.Collections.Generic;

namespace RestaurantAPI.Models;

public partial class LigneCommandeOption
{
    public int IdLigneCommande { get; set; }

    public int IdOption { get; set; }

    public int Quantite { get; set; }

    public decimal PrixSupplement { get; set; }

    public virtual LigneCommande IdLigneCommandeNavigation { get; set; } = null!;

    public virtual OptionPlat IdOptionNavigation { get; set; } = null!;
}
