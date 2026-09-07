using System;
using System.Collections.Generic;

namespace RestaurantAPI.Models;

public partial class Plat
{
    public int IdPlat { get; set; }

    public string NomPlat { get; set; } = null!;

    public decimal Prix { get; set; }

    public int IdCategorie { get; set; }

    public int? TempsPreparation { get; set; }

    public virtual ICollection<FormulePlat> FormulePlats { get; set; } = new List<FormulePlat>();

    public virtual Categorie IdCategorieNavigation { get; set; } = null!;

    public virtual ICollection<LigneCommande> LigneCommandes { get; set; } = new List<LigneCommande>();

    public virtual Recette? Recette { get; set; }

    public virtual ICollection<OptionPlat> IdOptions { get; set; } = new List<OptionPlat>();
}
