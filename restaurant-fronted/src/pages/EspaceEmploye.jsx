import { useEffect, useState } from "react"
import { useNavigate } from "react-router-dom"
import {
  ClipboardList,
  Banknote,
  BarChart3,
  LogOut,
  UserRound,
  Table2
} from "lucide-react"
function EspaceEmploye() {
  const navigate = useNavigate()
  const [tableSelectionnee, setTableSelectionnee] = useState(null)

const tables = [
  { id: 1, statut: "Libre" },
  { id: 2, statut: "Libre" },
  { id: 3, statut: "Occupée" },
  { id: 4, statut: "Libre" },
  { id: 5, statut: "Réservée" },
  { id: 6, statut: "Libre" }
]

const tableIndisponible = (statut) =>
  statut === "Occupée" || statut === "Réservée"

  const donnees = localStorage.getItem("employeConnecte")
  const utilisateur = donnees ? JSON.parse(donnees) : null
  const poste =
    utilisateur?.poste?.nomPoste ||
    utilisateur?.poste?.libelle ||
    (typeof utilisateur?.poste === "string" ? utilisateur.poste : "") ||
    utilisateur?.nomPoste ||
    utilisateur?.role ||
    "Employé"
  const posteNormalise = String(poste).toLowerCase()
  const nomUtilisateur = String(
    utilisateur?.nomUtilisateur || utilisateur?.username || ""
  ).toLowerCase()
  const accesCaisse =
    posteNormalise.includes("caiss") ||
    posteNormalise.includes("caisse") ||
    nomUtilisateur === "emp002"

  useEffect(() => {
    if (!donnees) {
      navigate("/employe", { replace: true })
    }
  }, [donnees, navigate])

  if (!donnees) return null

  const profil = utilisateur?.employe || utilisateur?.employee || utilisateur
  const prenom =
    profil?.prenom ||
    profil?.firstName ||
    profil?.prenomEmploye ||
    profil?.nom ||
    utilisateur?.nomUtilisateur ||
    "Employé"

  const seDeconnecter = () => {
    localStorage.removeItem("employeConnecte")
    navigate("/employe")
  }

  return (
    <div className="espace-employe-page">

      <header className="espace-employe-header">
        <div className="espace-employe-user">
          <UserRound size={28} />

          <div>
            <span>{poste}</span>
            <h1>Bonjour {prenom} !</h1>
          </div>
        </div>

        <button
          className="logout-button"
          onClick={seDeconnecter}
        >
          <LogOut size={19} />
          Déconnexion
        </button>
      </header>

      <main className="espace-employe-content">
  <div className="welcome-section">
    <h2>Bienvenue dans votre espace de travail</h2>
    <p>
      Retrouvez ici les informations et les outils nécessaires pour votre service.
    </p>
  </div>

  <div className="employe-options">
    <button
      className="employe-option-card"
      onClick={() => navigate("/serveur")}
    >
      <div className="option-icon">
        <ClipboardList size={30} />
      </div>

      <div>
        <h3>Voir les commandes</h3>
        <p>Consultez les commandes enregistrées et leur état.</p>
      </div>
    </button>

    {accesCaisse && (
      <button
        className="employe-option-card"
        onClick={() => navigate("/caisse")}
      >
        <div className="option-icon">
          <Banknote size={30} />
        </div>

        <div>
          <h3>Accéder à la caisse</h3>
          <p>Enregistrez les paiements des commandes.</p>
        </div>
      </button>
    )}

    <button
      className="employe-option-card"
      onClick={() => navigate("/service-journee")}
    >
      <div className="option-icon">
        <BarChart3 size={30} />
      </div>

      <div>
        <h3>Service de la journée</h3>
        <p>Consultez les commandes servies aujourd'hui.</p>
      </div>
    </button>
  </div>

  <section className="employee-tables-card">
    <div className="tables-title">
      <div className="option-icon">
        <Table2 size={30} />
      </div>

      <div>
        <h2>Choix de table</h2>
        <p>Sélectionnez une table disponible.</p>
      </div>
    </div>

    <div className="tables-grid">
      {tables.map((table) => {
        const indisponible = tableIndisponible(table.statut)

        return (
          <button
            key={table.id}
            type="button"
            disabled={indisponible}
            className={`table-option ${
              tableSelectionnee === table.id ? "selected" : ""
            } ${indisponible ? "occupied" : ""}`}
            onClick={() => setTableSelectionnee(table.id)}
          >
            <strong>Table {table.id}</strong>
            <span>{table.statut}</span>
          </button>
        )
      })}
    </div>

    {tableSelectionnee && (
      <div className="selected-table-message">
        Table {tableSelectionnee} sélectionnée
      </div>
    )}
  </section>
</main>

    </div>
  )
}

export default EspaceEmploye