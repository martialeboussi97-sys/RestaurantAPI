using System;
using System.Collections.Generic;

namespace RestaurantAPI.Models;

public partial class LigneCommandeAchat
{
    public int IdLigneAchat { get; set; }

    public int IdCommandeAchat { get; set; }

    public int IdIngredient { get; set; }

    public decimal Quantite { get; set; }

    public decimal PrixUnitaire { get; set; }

    public virtual CommandeAchat IdCommandeAchatNavigation { get; set; } = null!;

    public virtual Ingredient IdIngredientNavigation { get; set; } = null!;
}
