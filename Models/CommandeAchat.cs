using System;
using System.Collections.Generic;

namespace RestaurantAPI.Models;

public partial class CommandeAchat
{
    public int IdCommandeAchat { get; set; }

    public int IdFournisseur { get; set; }

    public DateTime? DateCommande { get; set; }

    public string Statut { get; set; } = null!;

    public virtual Fournisseur IdFournisseurNavigation { get; set; } = null!;

    public virtual ICollection<LigneCommandeAchat> LigneCommandeAchats { get; set; } = new List<LigneCommandeAchat>();

    public virtual ICollection<Reception> Receptions { get; set; } = new List<Reception>();
}
