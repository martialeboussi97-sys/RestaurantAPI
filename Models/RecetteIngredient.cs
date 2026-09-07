using System;
using System.Collections.Generic;

namespace RestaurantAPI.Models;

public partial class RecetteIngredient
{
    public int IdRecette { get; set; }

    public int IdIngredient { get; set; }

    public decimal Quantite { get; set; }

    public virtual Ingredient IdIngredientNavigation { get; set; } = null!;

    public virtual Recette IdRecetteNavigation { get; set; } = null!;
}
