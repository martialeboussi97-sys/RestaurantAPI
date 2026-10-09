using System;
using System.Collections.Generic;

namespace RestaurantAPI.Models;

public partial class Commande
{
    public int IdCommande { get; set; }

    public DateTime DateCommande { get; set; }

    public int? IdClient { get; set; }

    public int? IdTable { get; set; }

    public int? IdServeur { get; set; }

    public string? Statut { get; set; }

    public string? Remarque { get; set; }

    public int? IdTypeCommande { get; set; }

    public virtual ICollection<Avi> Avis { get; set; } = new List<Avi>();

    public virtual ICollection<Facture> Factures { get; set; } = new List<Facture>();

    public virtual Client? IdClientNavigation { get; set; }

    public virtual Serveur? IdServeurNavigation { get; set; }

    public virtual TableRestaurant? IdTableNavigation { get; set; }

    public virtual TypeCommande? IdTypeCommandeNavigation { get; set; }

    public virtual ICollection<LigneCommande> LigneCommandes { get; set; } = new List<LigneCommande>();

    public virtual ICollection<Livraison> Livraisons { get; set; } = new List<Livraison>();

    public virtual ICollection<Reclamation> Reclamations { get; set; } = new List<Reclamation>();

    public virtual ICollection<TicketCuisine> TicketCuisines { get; set; } = new List<TicketCuisine>();
}
