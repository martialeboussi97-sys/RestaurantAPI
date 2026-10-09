import { useCallback, useEffect, useState } from "react"
import { useNavigate, useParams } from "react-router-dom"
import {
  ArrowLeft,
  ClipboardList
} from "lucide-react"

function DetailsCommande() {
  const navigate = useNavigate()
  const { id } = useParams()

  const [commande, setCommande] = useState(null)
  const [lignes, setLignes] = useState([])
  const [chargement, setChargement] = useState(true)
  const [erreur, setErreur] = useState("")
  const [miseAJour, setMiseAJour] = useState(false)
  const [message, setMessage] = useState("")

  const statuts = [
    "En attente",
    "En préparation",
    "Prête",
    "Servie",
    "Annulée"
  ]

  // Charger la commande et ses lignes
  const chargerCommande = useCallback(async () => {
    try {
      setChargement(true)
      setErreur("")

      // Récupérer la commande
      const commandeResponse = await fetch(
        `https://localhost:7217/api/Commandes/${id}`
      )

      if (!commandeResponse.ok) {
        throw new Error("Commande introuvable.")
      }

      const commandeData = await commandeResponse.json()
      setCommande(commandeData)

      // Récupérer les lignes de commande
      const lignesResponse = await fetch(
        "https://localhost:7217/api/LigneCommandes"
      )

      if (!lignesResponse.ok) {
        throw new Error(
          "Impossible de récupérer les lignes de commande."
        )
      }

      const lignesData = await lignesResponse.json()

      // Garder uniquement les lignes de cette commande
      const lignesCommande = lignesData.filter(
        (ligne) =>
          ligne.idCommande === Number(id)
      )

      setLignes(lignesCommande)

    } catch (error) {
      console.error("ERREUR :", error)

      setErreur(
        "Impossible de charger les détails de la commande."
      )
    } finally {
      setChargement(false)
    }
  }, [id])

  useEffect(() => {
    const chargementId = setTimeout(() => {
      chargerCommande()
    }, 0)

    return () => clearTimeout(chargementId)
  }, [chargerCommande])

  // Modifier le statut
  const modifierStatut = async (nouveauStatut) => {
    try {
      setMiseAJour(true)
      setMessage("")
      setErreur("")

      const response = await fetch(
        `https://localhost:7217/api/Commandes/${id}/statut`,
        {
          method: "PUT",
          headers: {
            "Content-Type": "application/json"
          },
          body: JSON.stringify({
            statut: nouveauStatut
          })
        }
      )

      const texte = await response.text()

      console.log("STATUT MODIFICATION :", response.status)
      console.log("RÉPONSE :", texte)

      if (!response.ok) {
        throw new Error(
          texte || "Impossible de modifier le statut."
        )
      }

      // Mettre à jour immédiatement l'affichage
      setCommande((ancienneCommande) => ({
        ...ancienneCommande,
        statut: nouveauStatut
      }))

      setMessage(
        "Le statut de la commande a été mis à jour."
      )

    } catch (error) {
      console.error("ERREUR STATUT :", error)

      setErreur(
        error.message ||
        "Impossible de modifier le statut."
      )
    } finally {
      setMiseAJour(false)
    }
  }

  // Calcul du total
  const total = lignes.reduce(
    (somme, ligne) => {
      const prix =
        ligne.idPlatNavigation?.prix || 0

      return somme + prix * ligne.quantite
    },
    0
  )

  // Chargement
  if (chargement) {
    return (
      <div className="order-state">
        <p>Chargement de la commande...</p>
        <button className="details-commande-button" onClick={() => navigate("/serveur")}>
          <ArrowLeft size={18} />
          Retour aux commandes
        </button>
      </div>
    )
  }

  // Erreur
  if (erreur && !commande) {
    return (
      <div className="order-state error">
        <h1>Commande indisponible</h1>
        <p>{erreur}</p>
        <button className="details-commande-button" onClick={() => navigate("/serveur")}>
          <ArrowLeft size={18} />
          Retour aux commandes
        </button>
      </div>
    )
  }

  // Commande inexistante
  if (!commande) {
    return (
      <div className="order-state error">
        <h1>Commande introuvable</h1>
        <p>Cette commande n'existe pas ou n'est plus disponible.</p>
        <button className="details-commande-button" onClick={() => navigate("/serveur")}>
          <ArrowLeft size={18} />
          Retour aux commandes
        </button>
      </div>
    )
  }

  return (
    <div className="details-commande-page">

      {/* EN-TÊTE */}

      <header className="serveur-header">

        <button
          className="back-button"
          onClick={() => navigate("/serveur")}
        >
          <ArrowLeft size={20} />
          Retour aux commandes
        </button>

        <div className="serveur-title">

          <ClipboardList size={28} />

          <div>
            <h1>
              Commande #{commande.idCommande}
            </h1>

            <span>
              Détails de la commande
            </span>
          </div>

        </div>

      </header>


      {/* CONTENU */}

      <main className="details-commande-content">

        {/* INFORMATIONS */}

        <section className="commande-details-card">

          <h2>Informations</h2>

          <p>
            <strong>Client :</strong>{" "}
            {commande.idClient}
          </p>

          <p>
            <strong>Table :</strong>{" "}
            {commande.idTable || "Non renseignée"}
          </p>

          <p>
            <strong>Date :</strong>{" "}
            {commande.dateCommande
              ? new Date(
                  commande.dateCommande
                ).toLocaleString()
              : "Non renseignée"}
          </p>

          {/* STATUT */}

          <div className="statut-commande">

            <label htmlFor="statut">
              <strong>Statut :</strong>
            </label>

            <select
              id="statut"
              value={commande.statut || "En attente"}
              onChange={(e) =>
                modifierStatut(e.target.value)
              }
              disabled={miseAJour}
            >

              {statuts.map((statut) => (
                <option
                  key={statut}
                  value={statut}
                >
                  {statut}
                </option>
              ))}

            </select>

            {miseAJour && (
              <span>
                Mise à jour...
              </span>
            )}

          </div>

          {/* MESSAGE DE SUCCÈS */}

          {message && (
            <div className="serveur-success">
              {message}
            </div>
          )}

          {/* MESSAGE D'ERREUR */}

          {erreur && (
            <div className="serveur-error">
              {erreur}
            </div>
          )}

        </section>


        {/* PLATS */}

        <section className="commande-plats">

          <h2>Plats commandés</h2>

          {lignes.length === 0 ? (

            <p>
              Aucun plat dans cette commande.
            </p>

          ) : (

            lignes.map((ligne) => {

              const nomPlat =
                ligne.idPlatNavigation?.nomPlat ||
                `Plat #${ligne.idPlat}`

              const prix =
                ligne.idPlatNavigation?.prix || 0

              const sousTotal =
                prix * ligne.quantite

              return (
                <article
                  className="detail-plat-card"
                  key={ligne.idLigneCommande}
                >

                  <div>

                    <h3>
                      {nomPlat}
                    </h3>

                    <span>
                      {prix.toLocaleString()} FCFA ×{" "}
                      {ligne.quantite}
                    </span>

                  </div>

                  <strong>
                    {sousTotal.toLocaleString()} FCFA
                  </strong>

                </article>
              )
            })
          )}

        </section>


        {/* TOTAL */}

        <section className="commande-total">

          <span>
            Total
          </span>

          <strong>
            {total.toLocaleString()} FCFA
          </strong>

        </section>

      </main>

    </div>
  )
}

export default DetailsCommande