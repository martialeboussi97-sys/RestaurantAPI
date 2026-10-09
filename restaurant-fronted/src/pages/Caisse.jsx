import { useCallback, useEffect, useMemo, useState } from "react"
import {
  ArrowLeft,
  Banknote,
  Check,
  Printer,
  ReceiptText,
  RefreshCw,
  Search,
  X
} from "lucide-react"
import { useNavigate } from "react-router-dom"
import logoRestaurant from "../assets/hero.png"

function Caisse() {
  const navigate = useNavigate()
  const donnees = localStorage.getItem("employeConnecte")
  const utilisateur = donnees ? JSON.parse(donnees) : null
  const poste = String(
    utilisateur?.poste?.nomPoste ||
    utilisateur?.poste?.libelle ||
    (typeof utilisateur?.poste === "string" ? utilisateur.poste : "") ||
    utilisateur?.nomPoste ||
    utilisateur?.role ||
    ""
  ).toLowerCase()
  const nomUtilisateur = String(
    utilisateur?.nomUtilisateur || utilisateur?.username || ""
  ).toLowerCase()
  const accesAutorise =
    poste.includes("caiss") ||
    poste.includes("caisse") ||
    nomUtilisateur === "emp002"
  const profil = utilisateur?.employe || utilisateur?.employee || utilisateur
  const prenom =
    profil?.prenom ||
    profil?.firstName ||
    profil?.prenomEmploye ||
    profil?.nom ||
    utilisateur?.nomUtilisateur ||
    "Caissier"
  const [commandes, setCommandes] = useState([])
  const [recherche, setRecherche] = useState("")
  const [chargement, setChargement] = useState(true)
  const [erreur, setErreur] = useState("")
  const [paiementId, setPaiementId] = useState(null)
  const [message, setMessage] = useState("")
  const [recu, setRecu] = useState(null)
  const [commandesEncaissees, setCommandesEncaissees] = useState([])

  const chargerCommandes = useCallback(async () => {
    try {
      setChargement(true)
      setErreur("")

      const response = await fetch("https://localhost:7217/api/Commandes")

      if (!response.ok) {
        throw new Error("Impossible de récupérer les commandes.")
      }

      setCommandes(await response.json())
    } catch (error) {
      setErreur(error.message || "La caisse est indisponible.")
    } finally {
      setChargement(false)
    }
  }, [])

  useEffect(() => {
    const chargementId = setTimeout(chargerCommandes, 0)
    return () => clearTimeout(chargementId)
  }, [chargerCommandes])

  const commandesFiltrees = useMemo(() => {
    const terme = recherche.trim().toLowerCase()

    if (!terme) return commandes

    return commandes.filter((commande) =>
      String(commande.idCommande).includes(terme)
    )
  }, [commandes, recherche])

  const totalEnAttente = commandesFiltrees.filter(
    (commande) =>
      commande.statut === "Servie" &&
      !commandesEncaissees.includes(commande.idCommande)
  ).length

  useEffect(() => {
    if (!utilisateur || !accesAutorise) {
      navigate("/espace-employe", { replace: true })
    }
  }, [accesAutorise, navigate, utilisateur])

  const confirmerPaiement = async (commande) => {
    try {
      setPaiementId(commande.idCommande)
      setMessage("")

      const response = await fetch(
        `https://localhost:7217/api/Commandes/${commande.idCommande}/statut`,
        {
          method: "PUT",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify({ statut: "Servie" })
        }
      )

      if (!response.ok) {
        const detail = await response.text()
        throw new Error(detail || "Le paiement n'a pas pu être enregistré.")
      }

      const lignesResponse = await fetch(
        "https://localhost:7217/api/LigneCommandes"
      )
      const toutesLesLignes = lignesResponse.ok
        ? await lignesResponse.json()
        : []
      const lignes = (Array.isArray(toutesLesLignes)
        ? toutesLesLignes
        : toutesLesLignes.$values || toutesLesLignes.data || []
      ).filter((ligne) => ligne.idCommande === commande.idCommande)
      const totalLignes = lignes.reduce(
        (total, ligne) =>
          total +
          Number(ligne.idPlatNavigation?.prix || ligne.prix || 0) *
            Number(ligne.quantite || 0),
        0
      )

      setCommandes((anciennesCommandes) =>
        anciennesCommandes.map((ancienneCommande) =>
          ancienneCommande.idCommande === commande.idCommande
            ? { ...ancienneCommande, statut: "Servie" }
            : ancienneCommande
        )
      )
      setMessage(`Commande #${commande.idCommande} encaissée.`)
      setCommandesEncaissees((anciennesCommandes) => [
        ...anciennesCommandes,
        commande.idCommande
      ])
      setRecu({
        idCommande: commande.idCommande,
        idTable: commande.idTable,
        montant:
          commande.total ||
          commande.montant ||
          commande.totalCommande ||
          totalLignes,
        lignes,
        datePaiement: new Date()
      })
    } catch (error) {
      setErreur(error.message || "Le paiement n'a pas pu être enregistré.")
    } finally {
      setPaiementId(null)
    }
  }

  if (!utilisateur || !accesAutorise) {
    return null
  }

  return (
    <div className="caisse-page">
      <header className="serveur-header">
        <button className="back-button" onClick={() => navigate("/espace-employe")}>
          <ArrowLeft size={20} />
          Retour
        </button>

        <div className="serveur-title">
          <Banknote size={28} />
          <div>
            <h1>Caisse</h1>
            <span>{prenom} - Encaissement des commandes</span>
          </div>
        </div>

        <button className="refresh-button" onClick={chargerCommandes}>
          <RefreshCw size={18} />
          Actualiser
        </button>
      </header>

      <main className="serveur-content">
        <div className="caisse-toolbar">
          <div>
            <h2>Commandes à encaisser</h2>
            <p>{totalEnAttente} commande(s) en attente de paiement</p>
          </div>
          <label className="caisse-search">
            <Search size={18} />
            <input
              type="search"
              placeholder="N° de commande"
              value={recherche}
              onChange={(event) => setRecherche(event.target.value)}
            />
          </label>
        </div>

        {message && <div className="caisse-success">{message}</div>}
        {erreur && <div className="serveur-error">{erreur}</div>}
        {chargement && <div className="serveur-message">Chargement des commandes...</div>}

        {!chargement && !erreur && commandesFiltrees.length === 0 && (
          <div className="serveur-message">Aucune commande à afficher.</div>
        )}

        {!chargement && commandesFiltrees.length > 0 && (
          <div className="caisse-list">
            {commandesFiltrees.map((commande) => {
              const commandeServie = commande.statut === "Servie"
              const commandeEncaissee = commandesEncaissees.includes(
                commande.idCommande
              )
              const commandeEncaissable = commandeServie && !commandeEncaissee

              return (
                <article className="caisse-order" key={commande.idCommande}>
                  <div>
                    <strong className="caisse-order-id">
                      ID commande : #{commande.idCommande}
                    </strong>
                    <span>
                      {commande.idTable
                        ? `Table : ${commande.idTable}`
                        : "Commande à emporter"}
                    </span>
                  </div>
                  <span className={`caisse-status ${commandeServie ? "paid" : "pending"}`}>
                    {commande.statut || "En attente"}
                  </span>
                  <button
                    className="cash-button"
                    disabled={!commandeEncaissable || paiementId === commande.idCommande}
                    onClick={() => confirmerPaiement(commande)}
                  >
                    <Check size={18} />
                    {commandeEncaissee
                      ? "Encaissée"
                      : paiementId === commande.idCommande
                        ? "Enregistrement..."
                        : commandeServie
                          ? "Encaisser"
                          : "Non encaissable"}
                  </button>
                </article>
              )
            })}
          </div>
        )}
      </main>

      {recu && (
        <div className="receipt-overlay" role="dialog" aria-modal="true">
          <article className="receipt-card">
            <button
              className="receipt-close"
              aria-label="Fermer le reçu"
              onClick={() => setRecu(null)}
            >
              <X size={20} />
            </button>

            <ReceiptText size={42} className="receipt-icon" />
            <div className="receipt-brand">
              <img src={logoRestaurant} alt="Logo Les Délices du G2" />
              <div>
                <strong>LES DÉLICES DU G2</strong>
                <span>Restaurant</span>
              </div>
            </div>

            <ReceiptText size={34} className="receipt-icon" />
            <h2>Reçu de paiement</h2>
            <p className="receipt-confirmed">Paiement confirmé</p>

            <div className="receipt-details">
              <div>
                <span>ID commande</span>
                <strong>#{recu.idCommande}</strong>
              </div>
              <div>
                <span>Destination</span>
                <strong>{recu.idTable ? `Table ${recu.idTable}` : "À emporter"}</strong>
              </div>
              <div>
                <span>Date</span>
                <strong>{recu.datePaiement.toLocaleDateString("fr-FR")}</strong>
              </div>
              <div>
                <span>Heure</span>
                <strong>{recu.datePaiement.toLocaleTimeString("fr-FR")}</strong>
              </div>
              <div>
                <span>Montant</span>
                <strong>
                  {recu.montant
                    ? `${Number(recu.montant).toLocaleString("fr-FR")} FCFA`
                    : "À compléter"}
                </strong>
              </div>
            </div>
            <section className="receipt-items">
              <h3>Plats commandés</h3>
              {recu.lignes?.length ? recu.lignes.map((ligne) => {
                const prix = Number(
                  ligne.idPlatNavigation?.prix || ligne.prix || 0
                )
                const quantite = Number(ligne.quantite || 0)
                const nom =
                  ligne.idPlatNavigation?.nomPlat ||
                  ligne.idPlatNavigation?.nom ||
                  `Plat #${ligne.idPlat}`

                return (
                  <div className="receipt-item" key={ligne.idLigneCommande || `${ligne.idPlat}-${quantite}`}>
                    <div>
                      <strong>{nom}</strong>
                      <span>{quantite} × {prix.toLocaleString("fr-FR")} FCFA</span>
                    </div>
                    <b>{(prix * quantite).toLocaleString("fr-FR")} FCFA</b>
                  </div>
                )
              }) : (
                <p className="receipt-empty">Détail des plats indisponible.</p>
              )}
            </section>

            <div className="receipt-total">
              <span>Total</span>
              <strong>
                {Number(recu.montant || 0).toLocaleString("fr-FR")} FCFA
              </strong>
            </div>

            <button className="print-receipt-button" onClick={() => window.print()}>
              <Printer size={18} />
              Imprimer le reçu
            </button>
          </article>
        </div>
      )}
    </div>
  )
}

export default Caisse