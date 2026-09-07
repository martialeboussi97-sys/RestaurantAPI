using System;
using System.Collections.Generic;

namespace RestaurantAPI.Models;

public partial class Recette
{
    public int IdRecette { get; set; }

    public int IdPlat { get; set; }

    public string? Instructions { get; set; }

    public virtual Plat IdPlatNavigation { get; set; } = null!;

    public virtual ICollection<RecetteIngredient> RecetteIngredients { get; set; } = new List<RecetteIngredient>();
}
