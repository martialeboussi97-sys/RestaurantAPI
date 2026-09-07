using System;
using System.Collections.Generic;

namespace RestaurantAPI.Models;

public partial class Employe
{
    public int IdEmploye { get; set; }

    public string NumeroEmploye { get; set; } = null!;

    public string Nom { get; set; } = null!;

    public string? Prenom { get; set; }

    public string? Telephone { get; set; }

    public string? Email { get; set; }

    public DateOnly? DateEmbauche { get; set; }

    public string? Statut { get; set; }

    public int IdPoste { get; set; }

    public virtual ICollection<Absence> Absences { get; set; } = new List<Absence>();

    public virtual ICollection<Conge> Conges { get; set; } = new List<Conge>();

    public virtual Poste IdPosteNavigation { get; set; } = null!;

    public virtual ICollection<Livreur> Livreurs { get; set; } = new List<Livreur>();

    public virtual ICollection<Planning> Plannings { get; set; } = new List<Planning>();

    public virtual ICollection<Presence> Presences { get; set; } = new List<Presence>();

    public virtual Serveur? Serveur { get; set; }

    public virtual ICollection<SessionCaisse> SessionCaisses { get; set; } = new List<SessionCaisse>();

    public virtual Utilisateur? Utilisateur { get; set; }
}
