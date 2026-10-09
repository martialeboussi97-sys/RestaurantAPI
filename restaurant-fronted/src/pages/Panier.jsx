import { useContext, useState } from "react"
import { useNavigate } from "react-router-dom"
import {
  ArrowLeft,
  Plus,
  Minus,
  Trash2,
  ShoppingBag,
  ArrowRight
} from "lucide-react"

import { CartContext } from "../context/CartContext"

const idTableParNumero = {
  1: 1,
  6: 2,
  2: 3,
  3: 4,
  4: 5,
  5: 6
}

function Panier() {
  const navigate = useNavigate()
  const tableClient = localStorage.getItem("tableClient")
  const typeCommande = localStorage.getItem("typeCommande")
  const idTable = tableClient
    ? idTableParNumero[Number(tableClient)] || null
    : null
  const [chargement, setChargement] = useState(false)
  const [erreur, setErreur] = useState("")

  const {
    panier,
    supprimerDuPanier,
    augmenterQuantite,
    diminuerQuantite
  } = useContext(CartContext)

  const total = panier.reduce(
    (somme, plat) => somme + plat.prix * plat.quantite,
    0
  )

  const nombreArticles = panier.reduce(
    (total, plat) => total + plat.quantite,
    0
  )

  const validerCommande = async () => {
    setErreur("")
    setChargement(true)

    try {
      const commandeResponse = await fetch(
        "https://localhost:7217/api/Commandes",
        {
          method: "POST",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify({
            idClient: null,
            idTable,
            dateCommande: new Date().toISOString(),
            statut: "En attente",
            total
          })
        }
      )

      if (!commandeResponse.ok) {
        const details = await commandeResponse.text()
        throw new Error(
          details || "La commande n'a pas pu être créée."
        )
      }

      const commande = await commandeResponse.json()
      const idCommande = commande.idCommande || commande.id

      if (!idCommande) {
        throw new Error("L'API n'a pas renvoyé l'identifiant de la commande.")
      }

      for (const plat of panier) {
        const ligneResponse = await fetch(
          "https://localhost:7217/api/LigneCommandes",
          {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({
              idCommande,
              idPlat: plat.id,
              quantite: plat.quantite
            })
          }
        )

        if (!ligneResponse.ok) {
          const details = await ligneResponse.text()
          throw new Error(
            details || "Les articles de la commande n'ont pas pu être enregistrés."
          )
        }
      }

      navigate("/confirmation", {
        state: { idCommande, typeCommande }
      })
    } catch (error) {
      console.error("ERREUR ENREGISTREMENT COMMANDE :", error)
      setErreur(error.message || "Impossible d'enregistrer la commande.")
    } finally {
      setChargement(false)
    }
  }

  return (
    <div className="panier-page">

      {/* HEADER */}
      <header className="panier-header">

        <button
          className="back-button"
          onClick={() => navigate("/menu")}
        >
          <ArrowLeft size={20} />
          Continuer mes achats
        </button>

        <div className="panier-title">
          <ShoppingBag size={26} />
          <div>
            <h1>Ma commande</h1>
            <span>{nombreArticles} article(s)</span>
          </div>
        </div>

      </header>


      {/* CONTENU */}
      <main className="panier-content">

        {panier.length === 0 ? (

          /* PANIER VIDE */

          <div className="empty-cart">

            <ShoppingBag size={60} />

            <h2>Votre panier est vide</h2>

            <p>
              Découvrez nos délicieux plats et composez votre commande.
            </p>

            <button
              className="return-menu-button"
              onClick={() => navigate("/menu")}
            >
              Découvrir le menu
              <ArrowRight size={20} />
            </button>

          </div>

        ) : (

          /* PANIER AVEC PRODUITS */

          <div className="cart-layout">

            <section className="cart-items">

              {panier.map((plat) => (

                <article
                  className="cart-item"
                  key={plat.id}
                >

                  <img
                    src={plat.image}
                    alt={plat.nom}
                    className="cart-item-image"
                  />

                  <div className="cart-item-info">

                    <div>
                      <h3>{plat.nom}</h3>

                      <span>
                        {plat.prix.toLocaleString()} FCFA
                      </span>
                    </div>


                    <div className="cart-item-actions">

                      <div className="quantity-control">

                        <button
                          onClick={() => diminuerQuantite(plat.id)}
                        >
                          <Minus size={17} />
                        </button>

                        <strong>{plat.quantite}</strong>

                        <button
                          onClick={() => augmenterQuantite(plat.id)}
                        >
                          <Plus size={17} />
                        </button>

                      </div>


                      <button
                        className="delete-button"
                        onClick={() => supprimerDuPanier(plat.id)}
                      >
                        <Trash2 size={19} />
                      </button>

                    </div>

                  </div>


                  <strong className="cart-item-total">
                    {(plat.prix * plat.quantite).toLocaleString()} FCFA
                  </strong>

                </article>

              ))}

            </section>


            {/* RESUME */}

            <aside className="order-summary">

              <h2>Résumé</h2>
              <div className="summary-line">
  <span>Table</span>
  <strong>
    {tableClient || "Non renseignée"}
  </strong>
</div>

              <div className="summary-line">
                <span>Sous-total</span>

                <strong>
                  {total.toLocaleString()} FCFA
                </strong>
              </div>

              <div className="summary-line">
                <span>Service</span>
                <strong>Inclus</strong>
              </div>

              <div className="summary-total">
                <span>Total</span>

                <strong>
                  {total.toLocaleString()} FCFA
                </strong>
              </div>


              <button
                className="validate-order-button"
                onClick={validerCommande}
                disabled={chargement}
              >
                {chargement ? "Enregistrement..." : "Valider ma commande"}
                <ArrowRight size={20} />
              </button>

              {erreur && <p className="order-error">{erreur}</p>}

            </aside>

          </div>

        )}

      </main>

    </div>
  )
}

export default Panier