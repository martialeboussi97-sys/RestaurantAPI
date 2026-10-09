import { ArrowRight, BriefcaseBusiness, Star } from "lucide-react"
import { useNavigate } from "react-router-dom"

function Accueil() {
  const navigate = useNavigate()

  return (
    <div className="accueil-page">

      {/* Navigation */}
      <header className="accueil-navbar">

        <div className="brand">
          <div className="brand-logo">G2</div>

          <div className="brand-text">
            <h2>LES DÉLICES DU G2</h2>
            <span>Restaurant & Saveurs</span>
          </div>
        </div>

        <button
          className="employee-link"
          onClick={() => navigate("/employe")}
        >
          <BriefcaseBusiness size={19} />
          Espace employé
        </button>

      </header>


      {/* Hero */}
      <main className="hero-section">

        <div className="hero-content">

          <div className="hero-badge">
            <Star size={16} fill="currentColor" />
            UNE EXPÉRIENCE CULINAIRE
          </div>

          <h1>
            Le plaisir de bien manger,
            <span> simplement.</span>
          </h1>

          <p>
            Découvrez une cuisine généreuse, des saveurs authentiques
            et des plats préparés avec passion pour vous offrir
            une expérience inoubliable.
          </p>

          <div className="hero-actions">

            <button
              className="primary-button"
              onClick={() => navigate("/table")}
            >
              Découvrir le menu
              <ArrowRight size={20} />
            </button>

          </div>

          <div className="hero-info">

            <div>
              <strong>Des plats savoureux</strong>
              <span>Préparés avec passion</span>
            </div>

            <div className="hero-line"></div>

            <div>
              <strong>Une expérience unique</strong>
              <span>Pour tous vos moments</span>
            </div>

          </div>

        </div>

      </main>


      {/* Bas de page */}
      <footer className="accueil-footer">
        <span>LES DÉLICES DU G2</span>
        <span>Une expérience qui se savoure</span>
      </footer>

    </div>
  )
}

export default Accueil