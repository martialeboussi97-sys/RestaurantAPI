import { useEffect, useState } from "react"
import { useNavigate } from "react-router-dom"
import {
  ArrowLeft,
  ClipboardList,
  RefreshCw
} from "lucide-react"

function Serveur() {
  const navigate = useNavigate()

  const [commandes, setCommandes] = useState([])
  const [chargement, setChargement] = useState(true)
  const [erreur, setErreur] = useState("")

  const chargerCommandes = async () => {
    try {
      setChargement(true)
      setErreur("")

     const response = await fetch(
  "https://localhost:7217/api/Commandes"
)

      const texte = await response.text()

      console.log("STATUT :", response.status)
      console.log("RÉPONSE :", texte)

      if (!response.ok) {
        throw new Error(
          `Erreur API ${response.status} : ${texte}`
        )
      }

      const data = JSON.parse(texte)

      setCommandes(data)

    } catch (error) {
      console.error("ERREUR API :", error)
      setErreur(error.message)
    } finally {
      setChargement(false)
    }
  }

  useEffect(() => {
    chargerCommandes()
  }, [])

  return (
    <div className="serveur-page">

      <header className="serveur-header">

        <button
          className="back-button"
          onClick={() => navigate("/employe")}
        >
          <ArrowLeft size={20} />
          Retour
        </button>

        <div className="serveur-title">
          <ClipboardList size={28} />

          <div>
            <h1>Interface Serveur</h1>
            <span>Gestion des commandes</span>
          </div>
        </div>

        <button
          className="refresh-button"
          onClick={chargerCommandes}
        >
          <RefreshCw size={18} />
          Actualiser
        </button>

      </header>

      <main className="serveur-content">

        <div className="serveur-welcome">
          <h2>Commandes clients</h2>
          <p>
            Consultez les commandes reçues et leur état.
          </p>
        </div>

        {chargement && (
          <div className="serveur-message">
            Chargement des commandes...
          </div>
        )}

        {erreur && (
          <div className="serveur-error">
            {erreur}
          </div>
        )}

        {!chargement && !erreur && commandes.length === 0 && (
          <div className="serveur-message">
            Aucune commande pour le moment.
          </div>
        )}

        {!chargement && !erreur && commandes.length > 0 && (
          <div className="commandes-grid">

            {commandes.map((commande) => (
              <article
                className="commande-card"
                key={commande.idCommande}
              >

                <div className="commande-card-header">

                  <strong>
                    Commande #{commande.idCommande}
                  </strong>

                  <span className="commande-statut">
                    {commande.statut || "En attente"}
                  </span>

                </div>

                <div className="commande-info">

                  <p>
                    <strong>Date :</strong>{" "}
                    {commande.dateCommande
                      ? new Date(
                          commande.dateCommande
                        ).toLocaleString()
                      : "Non renseignée"}
                  </p>

                  <p>
                    <strong>Client :</strong>{" "}
                    {commande.idClient}
                  </p>

                  {commande.idTable && (
                    <p>
                      <strong>Table :</strong>{" "}
                      {commande.idTable}
                    </p>
                  )}

                </div>

                <button
                  className="details-commande-button"
                  onClick={() =>
                    navigate(
                      `/commande/${commande.idCommande}`
                    )
                  }
                >
                  Voir la commande
                </button>

              </article>
            ))}

          </div>
        )}

      </main>

    </div>
  )
}

export default Serveur