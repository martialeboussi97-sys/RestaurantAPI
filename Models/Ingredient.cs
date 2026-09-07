using System;
using System.Collections.Generic;

namespace RestaurantAPI.Models;

public partial class Ingredient
{
    public int IdIngredient { get; set; }

    public string NomIngredient { get; set; } = null!;

    public string UniteMesure { get; set; } = null!;

    public decimal? SeuilAlerte { get; set; }

    public bool Actif { get; set; }

    public virtual ICollection<LigneCommandeAchat> LigneCommandeAchats { get; set; } = new List<LigneCommandeAchat>();

    public virtual ICollection<RecetteIngredient> RecetteIngredients { get; set; } = new List<RecetteIngredient>();

    public virtual Stock? Stock { get; set; }
}
