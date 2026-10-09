import { CheckCircle, Home, ShoppingBag } from "lucide-react"
import { useNavigate } from "react-router-dom"
import { useContext } from "react"
import { CartContext } from "../context/CartContext"

function Confirmation() {
  const navigate = useNavigate()
  const { viderPanier } = useContext(CartContext)

  const confirmerCommande = () => {
    viderPanier()
    navigate("/")
  }

  return (
    <div className="confirmation-page">

      <div className="confirmation-card">

        <CheckCircle
          size={80}
          className="success-icon"
        />

        <h1>Commande confirmée !</h1>

        <p>
          Votre commande a été enregistrée avec succès.
        </p>

        <p className="confirmation-message">
          Notre équipe va préparer votre commande dans les meilleurs délais.
        </p>

        <div className="confirmation-actions">

          <button
            className="home-button"
            onClick={confirmerCommande}
          >
            <Home size={20} />
            Retour à l'accueil
          </button>

          <button
            className="menu-button-confirmation"
            onClick={() => navigate("/menu")}
          >
            <ShoppingBag size={20} />
            Voir le menu
          </button>

        </div>

      </div>

    </div>
  )
}

export default Confirmation