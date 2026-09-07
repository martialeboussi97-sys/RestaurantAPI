using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Models;

namespace RestaurantAPI.Data;

public partial class RestaurantDbContext : DbContext
{
    public RestaurantDbContext()
    {
    }

    public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Absence> Absences { get; set; }

    public virtual DbSet<Avi> Avis { get; set; }

    public virtual DbSet<Caisse> Caisses { get; set; }

    public virtual DbSet<Categorie> Categories { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Commande> Commandes { get; set; }

    public virtual DbSet<CommandeAchat> CommandeAchats { get; set; }

    public virtual DbSet<Conge> Conges { get; set; }

    public virtual DbSet<Employe> Employes { get; set; }

    public virtual DbSet<Facture> Factures { get; set; }

    public virtual DbSet<Formule> Formules { get; set; }

    public virtual DbSet<FormulePlat> FormulePlats { get; set; }

    public virtual DbSet<Fournisseur> Fournisseurs { get; set; }

    public virtual DbSet<Ingredient> Ingredients { get; set; }

    public virtual DbSet<LigneCommande> LigneCommandes { get; set; }

    public virtual DbSet<LigneCommandeAchat> LigneCommandeAchats { get; set; }

    public virtual DbSet<LigneCommandeOption> LigneCommandeOptions { get; set; }

    public virtual DbSet<Livraison> Livraisons { get; set; }

    public virtual DbSet<Livreur> Livreurs { get; set; }

    public virtual DbSet<MouvementCaisse> MouvementCaisses { get; set; }

    public virtual DbSet<MouvementStock> MouvementStocks { get; set; }

    public virtual DbSet<OptionPlat> OptionPlats { get; set; }

    public virtual DbSet<Paiement> Paiements { get; set; }

    public virtual DbSet<Planning> Plannings { get; set; }

    public virtual DbSet<Plat> Plats { get; set; }

    public virtual DbSet<Poste> Postes { get; set; }

    public virtual DbSet<Preparation> Preparations { get; set; }

    public virtual DbSet<Presence> Presences { get; set; }

    public virtual DbSet<Reception> Receptions { get; set; }

    public virtual DbSet<Recette> Recettes { get; set; }

    public virtual DbSet<RecetteIngredient> RecetteIngredients { get; set; }

    public virtual DbSet<Reclamation> Reclamations { get; set; }

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<RoleUtilisateur> RoleUtilisateurs { get; set; }

    public virtual DbSet<Salle> Salles { get; set; }

    public virtual DbSet<Serveur> Serveurs { get; set; }

    public virtual DbSet<SessionCaisse> SessionCaisses { get; set; }

    public virtual DbSet<Stock> Stocks { get; set; }

    public virtual DbSet<TableRestaurant> TableRestaurants { get; set; }

    public virtual DbSet<Taxe> Taxes { get; set; }

    public virtual DbSet<TicketCuisine> TicketCuisines { get; set; }

    public virtual DbSet<TypeCommande> TypeCommandes { get; set; }

    public virtual DbSet<Utilisateur> Utilisateurs { get; set; }

    public virtual DbSet<Zone> Zones { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=Restaurant;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Absence>(entity =>
        {
            entity.HasKey(e => e.IdAbsence).HasName("PK__ABSENCE__570D1C4D130A4BDE");

            entity.ToTable("ABSENCE");

            entity.Property(e => e.IdAbsence).HasColumnName("id_absence");
            entity.Property(e => e.DateAbsence).HasColumnName("date_absence");
            entity.Property(e => e.IdEmploye).HasColumnName("id_employe");
            entity.Property(e => e.Motif)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("motif");
            entity.Property(e => e.Statut)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("statut");

            entity.HasOne(d => d.IdEmployeNavigation).WithMany(p => p.Absences)
                .HasForeignKey(d => d.IdEmploye)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ABSENCE__id_empl__1A9EF37A");
        });

        modelBuilder.Entity<Avi>(entity =>
        {
            entity.HasKey(e => e.IdAvis).HasName("PK__AVIS__B053FEDD84256754");

            entity.ToTable("AVIS");

            entity.Property(e => e.IdAvis).HasColumnName("id_avis");
            entity.Property(e => e.Commentaire)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("commentaire");
            entity.Property(e => e.DateAvis)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("date_avis");
            entity.Property(e => e.IdClient).HasColumnName("id_client");
            entity.Property(e => e.IdCommande).HasColumnName("id_commande");
            entity.Property(e => e.Note).HasColumnName("note");

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.Avis)
                .HasForeignKey(d => d.IdClient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AVIS__id_client__09746778");

            entity.HasOne(d => d.IdCommandeNavigation).WithMany(p => p.Avis)
                .HasForeignKey(d => d.IdCommande)
                .HasConstraintName("FK__AVIS__id_command__0A688BB1");
        });

        modelBuilder.Entity<Caisse>(entity =>
        {
            entity.HasKey(e => e.IdCaisse).HasName("PK__CAISSE__CDA72B70497BF6F3");

            entity.ToTable("CAISSE");

            entity.Property(e => e.IdCaisse).HasColumnName("id_caisse");
            entity.Property(e => e.Emplacement)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("emplacement");
            entity.Property(e => e.NomCaisse)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nom_caisse");
            entity.Property(e => e.Statut)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("statut");
        });

        modelBuilder.Entity<Categorie>(entity =>
        {
            entity.HasKey(e => e.IdCategorie).HasName("PK__CATEGORI__CD54BC5E2CCC80BD");

            entity.ToTable("CATEGORIE");

            entity.Property(e => e.IdCategorie).HasColumnName("id_categorie");
            entity.Property(e => e.NomCategorie)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nom_categorie");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.IdClient).HasName("PK__CLIENT__6EC2B6C06A117C1A");

            entity.ToTable("CLIENT");

            entity.Property(e => e.IdClient).HasColumnName("id_client");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Nom)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nom");
            entity.Property(e => e.Prenom)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("prenom");
            entity.Property(e => e.Telephone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("telephone");
        });

        modelBuilder.Entity<Commande>(entity =>
        {
            entity.HasKey(e => e.IdCommande).HasName("PK__COMMANDE__385131BF551A3A5A");

            entity.ToTable("COMMANDE");

            entity.Property(e => e.IdCommande).HasColumnName("id_commande");
            entity.Property(e => e.DateCommande)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("date_commande");
            entity.Property(e => e.IdClient).HasColumnName("id_client");
            entity.Property(e => e.IdServeur).HasColumnName("id_serveur");
            entity.Property(e => e.IdTable).HasColumnName("id_table");
            entity.Property(e => e.IdTypeCommande).HasColumnName("id_type_commande");
            entity.Property(e => e.Remarque)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("remarque");
            entity.Property(e => e.Statut)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("statut");

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.Commandes)
                .HasForeignKey(d => d.IdClient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__COMMANDE__id_cli__6E01572D");

            entity.HasOne(d => d.IdServeurNavigation).WithMany(p => p.Commandes)
                .HasForeignKey(d => d.IdServeur)
                .HasConstraintName("FK_COMMANDE_SERVEUR");

            entity.HasOne(d => d.IdTableNavigation).WithMany(p => p.Commandes)
                .HasForeignKey(d => d.IdTable)
                .HasConstraintName("FK_COMMANDE_TABLE");

            entity.HasOne(d => d.IdTypeCommandeNavigation).WithMany(p => p.Commandes)
                .HasForeignKey(d => d.IdTypeCommande)
                .HasConstraintName("FK_COMMANDE_TYPE");
        });

        modelBuilder.Entity<CommandeAchat>(entity =>
        {
            entity.HasKey(e => e.IdCommandeAchat).HasName("PK__COMMANDE__BE073C486CC4AD87");

            entity.ToTable("COMMANDE_ACHAT");

            entity.Property(e => e.IdCommandeAchat).HasColumnName("id_commande_achat");
            entity.Property(e => e.DateCommande)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("date_commande");
            entity.Property(e => e.IdFournisseur).HasColumnName("id_fournisseur");
            entity.Property(e => e.Statut)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("statut");

            entity.HasOne(d => d.IdFournisseurNavigation).WithMany(p => p.CommandeAchats)
                .HasForeignKey(d => d.IdFournisseur)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__COMMANDE___id_fo__70A8B9AE");
        });

        modelBuilder.Entity<Conge>(entity =>
        {
            entity.HasKey(e => e.IdConge).HasName("PK__CONGE__75469DAB2274A4E9");

            entity.ToTable("CONGE");

            entity.Property(e => e.IdConge).HasColumnName("id_conge");
            entity.Property(e => e.DateDebut).HasColumnName("date_debut");
            entity.Property(e => e.DateFin).HasColumnName("date_fin");
            entity.Property(e => e.IdEmploye).HasColumnName("id_employe");
            entity.Property(e => e.Motif)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("motif");
            entity.Property(e => e.Statut)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("statut");
            entity.Property(e => e.TypeConge)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("type_conge");

            entity.HasOne(d => d.IdEmployeNavigation).WithMany(p => p.Conges)
                .HasForeignKey(d => d.IdEmploye)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CONGE__id_employ__17C286CF");
        });

        modelBuilder.Entity<Employe>(entity =>
        {
            entity.HasKey(e => e.IdEmploye).HasName("PK__EMPLOYE__420CFD165FE14063");

            entity.ToTable("EMPLOYE");

            entity.HasIndex(e => e.NumeroEmploye, "UQ__EMPLOYE__9ABF237003243708").IsUnique();

            entity.Property(e => e.IdEmploye).HasColumnName("id_employe");
            entity.Property(e => e.DateEmbauche).HasColumnName("date_embauche");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.IdPoste).HasColumnName("id_poste");
            entity.Property(e => e.Nom)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nom");
            entity.Property(e => e.NumeroEmploye)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("numero_employe");
            entity.Property(e => e.Prenom)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("prenom");
            entity.Property(e => e.Statut)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("statut");
            entity.Property(e => e.Telephone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("telephone");

            entity.HasOne(d => d.IdPosteNavigation).WithMany(p => p.Employes)
                .HasForeignKey(d => d.IdPoste)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EMPLOYE__id_post__18EBB532");
        });

        modelBuilder.Entity<Facture>(entity =>
        {
            entity.HasKey(e => e.IdFacture).HasName("PK__FACTURE__6C08ED5709275DA5");

            entity.ToTable("FACTURE");

            entity.Property(e => e.IdFacture).HasColumnName("id_facture");
            entity.Property(e => e.DateFacture)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("date_facture");
            entity.Property(e => e.IdCommande).HasColumnName("id_commande");
            entity.Property(e => e.IdTaxe).HasColumnName("id_taxe");
            entity.Property(e => e.MontantNet)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("montant_net");
            entity.Property(e => e.MontantTotal)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("montant_total");
            entity.Property(e => e.Remise)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("remise");

            entity.HasOne(d => d.IdCommandeNavigation).WithMany(p => p.Factures)
                .HasForeignKey(d => d.IdCommande)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FACTURE__id_comm__51300E55");

            entity.HasOne(d => d.IdTaxeNavigation).WithMany(p => p.Factures)
                .HasForeignKey(d => d.IdTaxe)
                .HasConstraintName("FK_FACTURE_TAXE");
        });

        modelBuilder.Entity<Formule>(entity =>
        {
            entity.HasKey(e => e.IdFormule).HasName("PK__FORMULE__B1F5179647FF1190");

            entity.ToTable("FORMULE");

            entity.Property(e => e.IdFormule).HasColumnName("id_formule");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Disponible)
                .HasDefaultValue(true)
                .HasColumnName("disponible");
            entity.Property(e => e.NomFormule)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("nom_formule");
            entity.Property(e => e.Prix)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("prix");
        });

        modelBuilder.Entity<FormulePlat>(entity =>
        {
            entity.HasKey(e => new { e.IdFormule, e.IdPlat }).HasName("PK__FORMULE___32650938F9918582");

            entity.ToTable("FORMULE_PLAT");

            entity.Property(e => e.IdFormule).HasColumnName("id_formule");
            entity.Property(e => e.IdPlat).HasColumnName("id_plat");
            entity.Property(e => e.Quantite)
                .HasDefaultValue(1)
                .HasColumnName("quantite");

            entity.HasOne(d => d.IdFormuleNavigation).WithMany(p => p.FormulePlats)
                .HasForeignKey(d => d.IdFormule)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FORMULE_P__id_fo__214BF109");

            entity.HasOne(d => d.IdPlatNavigation).WithMany(p => p.FormulePlats)
                .HasForeignKey(d => d.IdPlat)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FORMULE_P__id_pl__22401542");
        });

        modelBuilder.Entity<Fournisseur>(entity =>
        {
            entity.HasKey(e => e.IdFournisseur).HasName("PK__FOURNISS__5B874F945ECE32D2");

            entity.ToTable("FOURNISSEUR");

            entity.Property(e => e.IdFournisseur).HasColumnName("id_fournisseur");
            entity.Property(e => e.Adresse)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("adresse");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.NomFournisseur)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("nom_fournisseur");
            entity.Property(e => e.Statut)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasDefaultValue("ACTIF")
                .HasColumnName("statut");
            entity.Property(e => e.Telephone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("telephone");
        });

        modelBuilder.Entity<Ingredient>(entity =>
        {
            entity.HasKey(e => e.IdIngredient).HasName("PK__INGREDIE__9D79738D08FDECE0");

            entity.ToTable("INGREDIENT");

            entity.Property(e => e.IdIngredient).HasColumnName("id_ingredient");
            entity.Property(e => e.Actif)
                .HasDefaultValue(true)
                .HasColumnName("actif");
            entity.Property(e => e.NomIngredient)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nom_ingredient");
            entity.Property(e => e.SeuilAlerte)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("seuil_alerte");
            entity.Property(e => e.UniteMesure)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("unite_mesure");
        });

        modelBuilder.Entity<LigneCommande>(entity =>
        {
            entity.HasKey(e => e.IdLigneCommande).HasName("PK__LIGNE_CO__A9BAB78ABAFDCCFB");

            entity.ToTable("LIGNE_COMMANDE");

            entity.Property(e => e.IdLigneCommande).HasColumnName("id_ligne_commande");
            entity.Property(e => e.IdCommande).HasColumnName("id_commande");
            entity.Property(e => e.IdPlat).HasColumnName("id_plat");
            entity.Property(e => e.Quantite)
                .HasDefaultValue(1)
                .HasColumnName("quantite");

            entity.HasOne(d => d.IdCommandeNavigation).WithMany(p => p.LigneCommandes)
                .HasForeignKey(d => d.IdCommande)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LIGNE_COM__id_co__72C60C4A");

            entity.HasOne(d => d.IdPlatNavigation).WithMany(p => p.LigneCommandes)
                .HasForeignKey(d => d.IdPlat)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LIGNE_COM__id_pl__73BA3083");
        });

        modelBuilder.Entity<LigneCommandeAchat>(entity =>
        {
            entity.HasKey(e => e.IdLigneAchat).HasName("PK__LIGNE_CO__E03F95E11BE17589");

            entity.ToTable("LIGNE_COMMANDE_ACHAT");

            entity.Property(e => e.IdLigneAchat).HasColumnName("id_ligne_achat");
            entity.Property(e => e.IdCommandeAchat).HasColumnName("id_commande_achat");
            entity.Property(e => e.IdIngredient).HasColumnName("id_ingredient");
            entity.Property(e => e.PrixUnitaire)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("prix_unitaire");
            entity.Property(e => e.Quantite)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("quantite");

            entity.HasOne(d => d.IdCommandeAchatNavigation).WithMany(p => p.LigneCommandeAchats)
                .HasForeignKey(d => d.IdCommandeAchat)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LIGNE_COM__id_co__73852659");

            entity.HasOne(d => d.IdIngredientNavigation).WithMany(p => p.LigneCommandeAchats)
                .HasForeignKey(d => d.IdIngredient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LIGNE_COM__id_in__74794A92");
        });

        modelBuilder.Entity<LigneCommandeOption>(entity =>
        {
            entity.HasKey(e => new { e.IdLigneCommande, e.IdOption }).HasName("PK__LIGNE_CO__DE0D3A11AE4DA43A");

            entity.ToTable("LIGNE_COMMANDE_OPTION");

            entity.Property(e => e.IdLigneCommande).HasColumnName("id_ligne_commande");
            entity.Property(e => e.IdOption).HasColumnName("id_option");
            entity.Property(e => e.PrixSupplement)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("prix_supplement");
            entity.Property(e => e.Quantite)
                .HasDefaultValue(1)
                .HasColumnName("quantite");

            entity.HasOne(d => d.IdLigneCommandeNavigation).WithMany(p => p.LigneCommandeOptions)
                .HasForeignKey(d => d.IdLigneCommande)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LIGNE_COM__id_li__2DB1C7EE");

            entity.HasOne(d => d.IdOptionNavigation).WithMany(p => p.LigneCommandeOptions)
                .HasForeignKey(d => d.IdOption)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LIGNE_COM__id_op__2EA5EC27");
        });

        modelBuilder.Entity<Livraison>(entity =>
        {
            entity.HasKey(e => e.IdLivraison).HasName("PK__LIVRAISO__5C931768B2222D30");

            entity.ToTable("LIVRAISON");

            entity.Property(e => e.IdLivraison).HasColumnName("id_livraison");
            entity.Property(e => e.AdresseLivraison)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("adresse_livraison");
            entity.Property(e => e.DateLivraison)
                .HasColumnType("datetime")
                .HasColumnName("date_livraison");
            entity.Property(e => e.FraisLivraison)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("frais_livraison");
            entity.Property(e => e.IdCommande).HasColumnName("id_commande");
            entity.Property(e => e.IdLivreur).HasColumnName("id_livreur");
            entity.Property(e => e.Remarque)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("remarque");
            entity.Property(e => e.Statut)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("statut");
            entity.Property(e => e.TelephoneClient)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("telephone_client");

            entity.HasOne(d => d.IdCommandeNavigation).WithMany(p => p.Livraisons)
                .HasForeignKey(d => d.IdCommande)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LIVRAISON__id_co__3552E9B6");

            entity.HasOne(d => d.IdLivreurNavigation).WithMany(p => p.Livraisons)
                .HasForeignKey(d => d.IdLivreur)
                .HasConstraintName("FK_LIVRAISON_LIVREUR");
        });

        modelBuilder.Entity<Livreur>(entity =>
        {
            entity.HasKey(e => e.IdLivreur).HasName("PK__LIVREUR__52266C1F0EAD315E");

            entity.ToTable("LIVREUR");

            entity.HasIndex(e => e.NumeroLivreur, "UQ__LIVREUR__D3BE60EDFE02AD7B").IsUnique();

            entity.Property(e => e.IdLivreur).HasColumnName("id_livreur");
            entity.Property(e => e.IdEmploye).HasColumnName("id_employe");
            entity.Property(e => e.MoyenTransport)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("moyen_transport");
            entity.Property(e => e.NumeroLivreur)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("numero_livreur");
            entity.Property(e => e.Statut)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("statut");

            entity.HasOne(d => d.IdEmployeNavigation).WithMany(p => p.Livreurs)
                .HasForeignKey(d => d.IdEmploye)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LIVREUR__id_empl__39237A9A");
        });

        modelBuilder.Entity<MouvementCaisse>(entity =>
        {
            entity.HasKey(e => e.IdMouvementCaisse).HasName("PK__MOUVEMEN__4D1AEB2E760A2E36");

            entity.ToTable("MOUVEMENT_CAISSE");

            entity.Property(e => e.IdMouvementCaisse).HasColumnName("id_mouvement_caisse");
            entity.Property(e => e.DateMouvement)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("date_mouvement");
            entity.Property(e => e.IdSessionCaisse).HasColumnName("id_session_caisse");
            entity.Property(e => e.Montant)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("montant");
            entity.Property(e => e.Motif)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("motif");
            entity.Property(e => e.TypeMouvement)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("type_mouvement");

            entity.HasOne(d => d.IdSessionCaisseNavigation).WithMany(p => p.MouvementCaisses)
                .HasForeignKey(d => d.IdSessionCaisse)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MOUVEMENT__id_se__02C769E9");
        });

        modelBuilder.Entity<MouvementStock>(entity =>
        {
            entity.HasKey(e => e.IdMouvement).HasName("PK__MOUVEMEN__3036F81748B53853");

            entity.ToTable("MOUVEMENT_STOCK");

            entity.Property(e => e.IdMouvement).HasColumnName("id_mouvement");
            entity.Property(e => e.DateMouvement)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("date_mouvement");
            entity.Property(e => e.IdStock).HasColumnName("id_stock");
            entity.Property(e => e.Motif)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("motif");
            entity.Property(e => e.Quantite)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("quantite");
            entity.Property(e => e.TypeMouvement)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("type_mouvement");

            entity.HasOne(d => d.IdStockNavigation).WithMany(p => p.MouvementStocks)
                .HasForeignKey(d => d.IdStock)
                .HasConstraintName("FK_MOUVEMENT_STOCK_STOCK");
        });

        modelBuilder.Entity<OptionPlat>(entity =>
        {
            entity.HasKey(e => e.IdOption).HasName("PK__OPTION_P__7B78D9B6C9023A2D");

            entity.ToTable("OPTION_PLAT");

            entity.Property(e => e.IdOption).HasColumnName("id_option");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Disponible)
                .HasDefaultValue(true)
                .HasColumnName("disponible");
            entity.Property(e => e.NomOption)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nom_option");
            entity.Property(e => e.PrixSupplement)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("prix_supplement");
        });

        modelBuilder.Entity<Paiement>(entity =>
        {
            entity.HasKey(e => e.IdPaiement).HasName("PK__PAIEMENT__72D44CFFFEBE0C47");

            entity.ToTable("PAIEMENT");

            entity.Property(e => e.IdPaiement).HasColumnName("id_paiement");
            entity.Property(e => e.DatePaiement)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("date_paiement");
            entity.Property(e => e.IdFacture).HasColumnName("id_facture");
            entity.Property(e => e.ModePaiement)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("mode_paiement");
            entity.Property(e => e.MontantPaye)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("montant_paye");
            entity.Property(e => e.Statut)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("statut");

            entity.HasOne(d => d.IdFactureNavigation).WithMany(p => p.Paiements)
                .HasForeignKey(d => d.IdFacture)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PAIEMENT__id_fac__55009F39");
        });

        modelBuilder.Entity<Planning>(entity =>
        {
            entity.HasKey(e => e.IdPlanning).HasName("PK__PLANNING__0119D9CC12327589");

            entity.ToTable("PLANNING");

            entity.Property(e => e.IdPlanning).HasColumnName("id_planning");
            entity.Property(e => e.DatePlanning).HasColumnName("date_planning");
            entity.Property(e => e.HeureDebut).HasColumnName("heure_debut");
            entity.Property(e => e.HeureFin).HasColumnName("heure_fin");
            entity.Property(e => e.IdEmploye).HasColumnName("id_employe");
            entity.Property(e => e.Statut)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("statut");

            entity.HasOne(d => d.IdEmployeNavigation).WithMany(p => p.Plannings)
                .HasForeignKey(d => d.IdEmploye)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PLANNING__id_emp__1209AD79");
        });

        modelBuilder.Entity<Plat>(entity =>
        {
            entity.HasKey(e => e.IdPlat).HasName("PK__PLAT__3901EAE976258AB5");

            entity.ToTable("PLAT");

            entity.Property(e => e.IdPlat).HasColumnName("id_plat");
            entity.Property(e => e.IdCategorie).HasColumnName("id_categorie");
            entity.Property(e => e.NomPlat)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nom_plat");
            entity.Property(e => e.Prix)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("prix");
            entity.Property(e => e.TempsPreparation).HasColumnName("temps_preparation");

            entity.HasOne(d => d.IdCategorieNavigation).WithMany(p => p.Plats)
                .HasForeignKey(d => d.IdCategorie)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PLAT__id_categor__6383C8BA");

            entity.HasMany(d => d.IdOptions).WithMany(p => p.IdPlats)
                .UsingEntity<Dictionary<string, object>>(
                    "PlatOption",
                    r => r.HasOne<OptionPlat>().WithMany()
                        .HasForeignKey("IdOption")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__PLAT_OPTI__id_op__29E1370A"),
                    l => l.HasOne<Plat>().WithMany()
                        .HasForeignKey("IdPlat")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__PLAT_OPTI__id_pl__28ED12D1"),
                    j =>
                    {
                        j.HasKey("IdPlat", "IdOption").HasName("PK__PLAT_OPT__4EB66772D8CC04B6");
                        j.ToTable("PLAT_OPTION");
                        j.IndexerProperty<int>("IdPlat").HasColumnName("id_plat");
                        j.IndexerProperty<int>("IdOption").HasColumnName("id_option");
                    });
        });

        modelBuilder.Entity<Poste>(entity =>
        {
            entity.HasKey(e => e.IdPoste).HasName("PK__POSTE__2426C251433FDFFA");

            entity.ToTable("POSTE");

            entity.Property(e => e.IdPoste).HasColumnName("id_poste");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.NomPoste)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nom_poste");
        });

        modelBuilder.Entity<Preparation>(entity =>
        {
            entity.HasKey(e => e.IdPreparation).HasName("PK__PREPARAT__A59B53614D9406B4");

            entity.ToTable("PREPARATION");

            entity.Property(e => e.IdPreparation).HasColumnName("id_preparation");
            entity.Property(e => e.HeureDebut)
                .HasColumnType("datetime")
                .HasColumnName("heure_debut");
            entity.Property(e => e.HeureFin)
                .HasColumnType("datetime")
                .HasColumnName("heure_fin");
            entity.Property(e => e.IdTicket).HasColumnName("id_ticket");
            entity.Property(e => e.Statut)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("statut");

            entity.HasOne(d => d.IdTicketNavigation).WithMany(p => p.Preparations)
                .HasForeignKey(d => d.IdTicket)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PREPARATI__id_ti__3E1D39E1");
        });

        modelBuilder.Entity<Presence>(entity =>
        {
            entity.HasKey(e => e.IdPresence).HasName("PK__PRESENCE__F3BA19A3C4403FB6");

            entity.ToTable("PRESENCE");

            entity.Property(e => e.IdPresence).HasColumnName("id_presence");
            entity.Property(e => e.DatePresence).HasColumnName("date_presence");
            entity.Property(e => e.HeureArrivee).HasColumnName("heure_arrivee");
            entity.Property(e => e.HeureDepart).HasColumnName("heure_depart");
            entity.Property(e => e.IdEmploye).HasColumnName("id_employe");
            entity.Property(e => e.Remarque)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("remarque");
            entity.Property(e => e.Statut)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("statut");

            entity.HasOne(d => d.IdEmployeNavigation).WithMany(p => p.Presences)
                .HasForeignKey(d => d.IdEmploye)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PRESENCE__id_emp__14E61A24");
        });

        modelBuilder.Entity<Reception>(entity =>
        {
            entity.HasKey(e => e.IdReception).HasName("PK__RECEPTIO__A7C81DD77BEFB068");

            entity.ToTable("RECEPTION");

            entity.Property(e => e.IdReception).HasColumnName("id_reception");
            entity.Property(e => e.DateReception)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("date_reception");
            entity.Property(e => e.IdCommandeAchat).HasColumnName("id_commande_achat");
            entity.Property(e => e.Remarque)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("remarque");
            entity.Property(e => e.Statut)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("statut");

            entity.HasOne(d => d.IdCommandeAchatNavigation).WithMany(p => p.Receptions)
                .HasForeignKey(d => d.IdCommandeAchat)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RECEPTION__id_co__7849DB76");
        });

        modelBuilder.Entity<Recette>(entity =>
        {
            entity.HasKey(e => e.IdRecette).HasName("PK__RECETTE__0BA4168AEB9B9796");

            entity.ToTable("RECETTE");

            entity.HasIndex(e => e.IdPlat, "UQ__RECETTE__3901EAE808C9C920").IsUnique();

            entity.Property(e => e.IdRecette).HasColumnName("id_recette");
            entity.Property(e => e.IdPlat).HasColumnName("id_plat");
            entity.Property(e => e.Instructions)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("instructions");

            entity.HasOne(d => d.IdPlatNavigation).WithOne(p => p.Recette)
                .HasForeignKey<Recette>(d => d.IdPlat)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RECETTE__id_plat__5CA1C101");
        });

        modelBuilder.Entity<RecetteIngredient>(entity =>
        {
            entity.HasKey(e => new { e.IdRecette, e.IdIngredient }).HasName("PK__RECETTE___C27381B2323809E2");

            entity.ToTable("RECETTE_INGREDIENT");

            entity.Property(e => e.IdRecette).HasColumnName("id_recette");
            entity.Property(e => e.IdIngredient).HasColumnName("id_ingredient");
            entity.Property(e => e.Quantite)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("quantite");

            entity.HasOne(d => d.IdIngredientNavigation).WithMany(p => p.RecetteIngredients)
                .HasForeignKey(d => d.IdIngredient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RECETTE_I__id_in__607251E5");

            entity.HasOne(d => d.IdRecetteNavigation).WithMany(p => p.RecetteIngredients)
                .HasForeignKey(d => d.IdRecette)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RECETTE_I__id_re__5F7E2DAC");
        });

        modelBuilder.Entity<Reclamation>(entity =>
        {
            entity.HasKey(e => e.IdReclamation).HasName("PK__RECLAMAT__DF8B17F114B08211");

            entity.ToTable("RECLAMATION");

            entity.Property(e => e.IdReclamation).HasColumnName("id_reclamation");
            entity.Property(e => e.DateReclamation)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("date_reclamation");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.IdClient).HasColumnName("id_client");
            entity.Property(e => e.IdCommande).HasColumnName("id_commande");
            entity.Property(e => e.Statut)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("statut");
            entity.Property(e => e.Sujet)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("sujet");

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.Reclamations)
                .HasForeignKey(d => d.IdClient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RECLAMATI__id_cl__0E391C95");

            entity.HasOne(d => d.IdCommandeNavigation).WithMany(p => p.Reclamations)
                .HasForeignKey(d => d.IdCommande)
                .HasConstraintName("FK__RECLAMATI__id_co__0F2D40CE");
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => e.IdReservation).HasName("PK__RESERVAT__92EE588F97FB2610");

            entity.ToTable("RESERVATION");

            entity.Property(e => e.IdReservation).HasColumnName("id_reservation");
            entity.Property(e => e.DateReservation).HasColumnName("date_reservation");
            entity.Property(e => e.HeureReservation).HasColumnName("heure_reservation");
            entity.Property(e => e.IdClient).HasColumnName("id_client");
            entity.Property(e => e.IdTable).HasColumnName("id_table");
            entity.Property(e => e.NombrePersonnes).HasColumnName("nombre_personnes");
            entity.Property(e => e.Statut)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("statut");

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.IdClient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RESERVATI__id_cl__32AB8735");

            entity.HasOne(d => d.IdTableNavigation).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.IdTable)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RESERVATI__id_ta__339FAB6E");
        });

        modelBuilder.Entity<RoleUtilisateur>(entity =>
        {
            entity.HasKey(e => e.IdRole).HasName("PK__ROLE_UTI__3D48441DF9599B37");

            entity.ToTable("ROLE_UTILISATEUR");

            entity.Property(e => e.IdRole).HasColumnName("id_role");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.NomRole)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nom_role");
        });

        modelBuilder.Entity<Salle>(entity =>
        {
            entity.HasKey(e => e.IdSalle).HasName("PK__SALLE__6C467389D17E6DDE");

            entity.ToTable("SALLE");

            entity.Property(e => e.IdSalle).HasColumnName("id_salle");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.NomSalle)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nom_salle");
        });

        modelBuilder.Entity<Serveur>(entity =>
        {
            entity.HasKey(e => e.IdServeur).HasName("PK__SERVEUR__EBE5691E877909A2");

            entity.ToTable("SERVEUR");

            entity.HasIndex(e => e.IdEmploye, "UQ_SERVEUR_EMPLOYE").IsUnique();

            entity.HasIndex(e => e.NumeroServeur, "UQ__SERVEUR__9196F4E4A55EF431").IsUnique();

            entity.Property(e => e.IdServeur).HasColumnName("id_serveur");
            entity.Property(e => e.IdEmploye).HasColumnName("id_employe");
            entity.Property(e => e.NumeroServeur)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("numero_serveur");

            entity.HasOne(d => d.IdEmployeNavigation).WithOne(p => p.Serveur)
                .HasForeignKey<Serveur>(d => d.IdEmploye)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SERVEUR__id_empl__1DB06A4F");
        });

        modelBuilder.Entity<SessionCaisse>(entity =>
        {
            entity.HasKey(e => e.IdSessionCaisse).HasName("PK__SESSION___DA56BA9ECD2E7E76");

            entity.ToTable("SESSION_CAISSE");

            entity.Property(e => e.IdSessionCaisse).HasColumnName("id_session_caisse");
            entity.Property(e => e.DateFermeture)
                .HasColumnType("datetime")
                .HasColumnName("date_fermeture");
            entity.Property(e => e.DateOuverture)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("date_ouverture");
            entity.Property(e => e.IdCaisse).HasColumnName("id_caisse");
            entity.Property(e => e.IdEmploye).HasColumnName("id_employe");
            entity.Property(e => e.MontantFinal)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("montant_final");
            entity.Property(e => e.MontantInitial)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("montant_initial");
            entity.Property(e => e.Statut)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("statut");

            entity.HasOne(d => d.IdCaisseNavigation).WithMany(p => p.SessionCaisses)
                .HasForeignKey(d => d.IdCaisse)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SESSION_C__id_ca__7E02B4CC");

            entity.HasOne(d => d.IdEmployeNavigation).WithMany(p => p.SessionCaisses)
                .HasForeignKey(d => d.IdEmploye)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SESSION_C__id_em__7EF6D905");
        });

        modelBuilder.Entity<Stock>(entity =>
        {
            entity.HasKey(e => e.IdStock).HasName("PK__STOCK__3A39590A027C1D45");

            entity.ToTable("STOCK");

            entity.HasIndex(e => e.IdIngredient, "UQ__STOCK__9D79738CF1C18F16").IsUnique();

            entity.Property(e => e.IdStock).HasColumnName("id_stock");
            entity.Property(e => e.DateMiseAJour)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("date_mise_a_jour");
            entity.Property(e => e.IdIngredient).HasColumnName("id_ingredient");
            entity.Property(e => e.QuantiteActuelle)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("quantite_actuelle");

            entity.HasOne(d => d.IdIngredientNavigation).WithOne(p => p.Stock)
                .HasForeignKey<Stock>(d => d.IdIngredient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__STOCK__id_ingred__662B2B3B");
        });

        modelBuilder.Entity<TableRestaurant>(entity =>
        {
            entity.HasKey(e => e.IdTable).HasName("PK__TABLE_RE__B8DC49863CDA77C5");

            entity.ToTable("TABLE_RESTAURANT");

            entity.Property(e => e.IdTable).HasColumnName("id_table");
            entity.Property(e => e.Capacite).HasColumnName("capacite");
            entity.Property(e => e.IdZone).HasColumnName("id_zone");
            entity.Property(e => e.NumeroTable).HasColumnName("numero_table");
            entity.Property(e => e.Statut)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("statut");

            entity.HasOne(d => d.IdZoneNavigation).WithMany(p => p.TableRestaurants)
                .HasForeignKey(d => d.IdZone)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TABLE_RES__id_zo__2FCF1A8A");
        });

        modelBuilder.Entity<Taxe>(entity =>
        {
            entity.HasKey(e => e.IdTaxe).HasName("PK__TAXE__C1D310D8235743D9");

            entity.ToTable("TAXE");

            entity.Property(e => e.IdTaxe).HasColumnName("id_taxe");
            entity.Property(e => e.NomTaxe)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nom_taxe");
            entity.Property(e => e.Statut)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("statut");
            entity.Property(e => e.Taux)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("taux");
        });

        modelBuilder.Entity<TicketCuisine>(entity =>
        {
            entity.HasKey(e => e.IdTicket).HasName("PK__TICKET_C__48C6F523DF886003");

            entity.ToTable("TICKET_CUISINE");

            entity.Property(e => e.IdTicket).HasColumnName("id_ticket");
            entity.Property(e => e.DateEnvoi)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("date_envoi");
            entity.Property(e => e.IdCommande).HasColumnName("id_commande");
            entity.Property(e => e.Statut)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("statut");

            entity.HasOne(d => d.IdCommandeNavigation).WithMany(p => p.TicketCuisines)
                .HasForeignKey(d => d.IdCommande)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TICKET_CU__id_co__3B40CD36");
        });

        modelBuilder.Entity<TypeCommande>(entity =>
        {
            entity.HasKey(e => e.IdTypeCommande).HasName("PK__TYPE_COM__C500CC15F4B9D51F");

            entity.ToTable("TYPE_COMMANDE");

            entity.Property(e => e.IdTypeCommande).HasColumnName("id_type_commande");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.NomType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nom_type");
        });

        modelBuilder.Entity<Utilisateur>(entity =>
        {
            entity.HasKey(e => e.IdUtilisateur).HasName("PK__UTILISAT__1A4FA5B87E583B3E");

            entity.ToTable("UTILISATEUR");

            entity.HasIndex(e => e.IdEmploye, "UQ_UTILISATEUR_EMPLOYE").IsUnique();

            entity.HasIndex(e => e.NomUtilisateur, "UQ__UTILISAT__8EE74574DE100B88").IsUnique();

            entity.Property(e => e.IdUtilisateur).HasColumnName("id_utilisateur");
            entity.Property(e => e.IdEmploye).HasColumnName("id_employe");
            entity.Property(e => e.MotDePasse)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("mot_de_passe");
            entity.Property(e => e.NomUtilisateur)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nom_utilisateur");

            entity.HasOne(d => d.IdEmployeNavigation).WithOne(p => p.Utilisateur)
                .HasForeignKey<Utilisateur>(d => d.IdEmploye)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UTILISATE__id_em__245D67DE");

            entity.HasMany(d => d.IdRoles).WithMany(p => p.IdUtilisateurs)
                .UsingEntity<Dictionary<string, object>>(
                    "UtilisateurRole",
                    r => r.HasOne<RoleUtilisateur>().WithMany()
                        .HasForeignKey("IdRole")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__UTILISATE__id_ro__282DF8C2"),
                    l => l.HasOne<Utilisateur>().WithMany()
                        .HasForeignKey("IdUtilisateur")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__UTILISATE__id_ut__2739D489"),
                    j =>
                    {
                        j.HasKey("IdUtilisateur", "IdRole").HasName("PK__UTILISAT__D99B21F91C8572A3");
                        j.ToTable("UTILISATEUR_ROLE");
                        j.IndexerProperty<int>("IdUtilisateur").HasColumnName("id_utilisateur");
                        j.IndexerProperty<int>("IdRole").HasColumnName("id_role");
                    });
        });

        modelBuilder.Entity<Zone>(entity =>
        {
            entity.HasKey(e => e.IdZone).HasName("PK__ZONE__67C93615406A44E3");

            entity.ToTable("ZONE");

            entity.Property(e => e.IdZone).HasColumnName("id_zone");
            entity.Property(e => e.IdSalle).HasColumnName("id_salle");
            entity.Property(e => e.NomZone)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nom_zone");

            entity.HasOne(d => d.IdSalleNavigation).WithMany(p => p.Zones)
                .HasForeignKey(d => d.IdSalle)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ZONE__id_salle__2CF2ADDF");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
