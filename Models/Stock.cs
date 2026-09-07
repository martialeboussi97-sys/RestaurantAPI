using System;
using System.Collections.Generic;

namespace RestaurantAPI.Models;

public partial class Stock
{
    public int IdStock { get; set; }

    public int IdIngredient { get; set; }

    public decimal QuantiteActuelle { get; set; }

    public DateTime? DateMiseAJour { get; set; }

    public virtual Ingredient IdIngredientNavigation { get; set; } = null!;

    public virtual ICollection<MouvementStock> MouvementStocks { get; set; } = new List<MouvementStock>();
}
