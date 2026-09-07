using System;
using System.Collections.Generic;

namespace RestaurantAPI.Models;

public partial class Fournisseur
{
    public int IdFournisseur { get; set; }

    public string NomFournisseur { get; set; } = null!;

    public string? Telephone { get; set; }

    public string? Email { get; set; }

    public string? Adresse { get; set; }

    public string Statut { get; set; } = null!;

    public virtual ICollection<CommandeAchat> CommandeAchats { get; set; } = new List<CommandeAchat>();
}
