import { useEffect, useState, useContext } from "react"
import { ShoppingCart, ChevronLeft, ChevronRight, Plus, Check } from "lucide-react"
import { CartContext } from "../context/CartContext"
import { useNavigate } from "react-router-dom"

const platsDuJour = [
  {
    nom: "Poulet braisé",
    prix: 4500,
    image:
      "https://images.unsplash.com/photo-1532550907401-a500c9a57435?auto=format&fit=crop&w=1200&q=80",
  },
  {
    nom: "Poisson grillé",
    prix: 5000,
    image:
      "https://images.unsplash.com/photo-1519708227418-c8fd9a32b7a2?auto=format&fit=crop&w=1200&q=80",
  },
  {
    nom: "Pâtes à la sauce tomate",
    prix: 4000,
    image:
      "https://images.unsplash.com/photo-1551183053-bf91a1d81141?auto=format&fit=crop&w=1200&q=80",
  },
  {
    nom: "Burger maison",
    prix: 4500,
    image:
      "https://images.unsplash.com/photo-1568901346375-23c9450c58cd?auto=format&fit=crop&w=1200&q=80",
  },
]

const platsActuels = new Set([
  "koumou patte d'arachide",
  "feuille de manioc",
  "bouillon de poisson",
  "sanguilier",
  "manioc",
  "pizza royale"
])

const imagesParPlat = {
  "koumou patte d'arachide": "https://upload.wikimedia.org/wikipedia/commons/0/0e/Peanut_stew.jpg",
  "feuille de manioc": "https://upload.wikimedia.org/wikipedia/commons/0/05/Saka-saka_-_pounded_and_cooked_cassava_leaves.jpg",
  "bouillon de poisson": "https://upload.wikimedia.org/wikipedia/commons/f/f1/02025_0353_Fish_soup.jpg",
  sanguilier: "https://upload.wikimedia.org/wikipedia/commons/e/e3/Jabal%C3%AD_estofado_-_juantiagues_2013.jpg",
  manioc: "https://upload.wikimedia.org/wikipedia/commons/b/ba/Sweet_Cassava%2C_May_2024.jpg",
  "pizza royale": "https://images.unsplash.com/photo-1574071318508-1cdbab80d002?auto=format&fit=crop&w=800&q=80"
}

const descriptionsParPlat = {
  "koumou patte d'arachide": "Un plat traditionnel mijote dans une sauce onctueuse a la pate d'arachide.",
  "feuille de manioc": "Feuilles de manioc cuisinees avec une sauce genereuse et parfumee.",
  "bouillon de poisson": "Un bouillon de poisson savoureux, servi chaud avec ses accompagnements.",
  sanguilier: "Une specialite genereuse preparee avec des ingredients soigneusement selectionnes.",
  manioc: "Un accompagnement de manioc tendre et savoureux.",
  "pizza royale": "Pizza royale preparee avec des ingredients savoureux."
}

const imageParDefaut = "https://upload.wikimedia.org/wikipedia/commons/2/2f/Village_Kitchen_%2831385994687%29.jpg"

function Menu() {
  const navigate = useNavigate()
  const tableClient = localStorage.getItem("tableClient")
  const typeCommande = localStorage.getItem("typeCommande")
  const { panier, ajouterAuPanier } = useContext(CartContext)
  const [platAjouteId, setPlatAjouteId] = useState(null)
  const [plats, setPlats] = useState([])
  const [chargementPlats, setChargementPlats] = useState(true)
  const [erreurPlats, setErreurPlats] = useState("")

  const ajouterAvecFeedback = (plat) => {
    ajouterAuPanier(plat)
    setPlatAjouteId(plat.id)

    setTimeout(() => {
      setPlatAjouteId(null)
    }, 1500)

    if (navigator.vibrate) {
      navigator.vibrate(100)
    }

    const audio = new Audio("/sounds/audio-to-star.mp3")
    audio.volume = 0.3
    audio.play().catch(() => {})
  }
  const [slide, setSlide] = useState(0)

  useEffect(() => {
    const chargerPlats = async () => {
      try {
        const response = await fetch("https://localhost:7217/api/Plats")
        if (!response.ok) throw new Error("Impossible de recuperer les plats.")

        const donnees = await response.json()
        const listePlats = Array.isArray(donnees)
          ? donnees
          : donnees.$values || donnees.data || []

        const platsTransformes = listePlats.map((plat) => {
          const nom = plat.nomPlat || plat.nom_plat || plat.nom || "Plat sans nom"
          const nomNormalise = nom.toLowerCase().trim()

          return {
            id: plat.idPlat || plat.id_plat || plat.id,
            nom,
            description: plat.description || descriptionsParPlat[nomNormalise] || "Un plat prepare avec soin par notre equipe.",
            prix: Number(plat.prix || plat.prixPlat || plat.prix_plat || 0),
            image: plat.image || plat.imageUrl || imagesParPlat[nomNormalise] || imageParDefaut
          }
        })

        setPlats(platsTransformes.filter((plat) => platsActuels.has(plat.nom.toLowerCase().trim())))
      } catch (error) {
        console.error("ERREUR CHARGEMENT PLATS :", error)
        setErreurPlats(error.message || "Impossible de charger les plats.")
      } finally {
        setChargementPlats(false)
      }
    }

    chargerPlats()
  }, [])

  useEffect(() => {
    const interval = setInterval(() => {
      setSlide((ancienSlide) =>
        ancienSlide === platsDuJour.length - 1
          ? 0
          : ancienSlide + 1
      )
    }, 4000)

    return () => clearInterval(interval)
  }, [])

  useEffect(() => {
    window.scrollTo(0, 0)
  }, [])

  const precedent = () => {
    setSlide(
      slide === 0
        ? platsDuJour.length - 1
        : slide - 1
    )
  }

  const suivant = () => {
    setSlide(
      slide === platsDuJour.length - 1
        ? 0
        : slide + 1
    )
  }

  return (
    <div className="menu-page">

      <header className="menu-header">

        <div>
          <h1>LES DÉLICES DU G2</h1>
          <p>Notre menu
            {tableClient && ` • Table ${tableClient}`}
            {typeCommande === "emporter" && " • À emporter"}
          </p>
        </div>

        <button
          className="cart-button"
          onClick={() => navigate("/panier")}
        >
          <ShoppingCart size={22} />
          <span>Panier</span>
          <strong>
            {panier.reduce(
              (total, plat) => total + plat.quantite,
              0
            )}
          </strong>
        </button>

      </header>

      <main className="menu-content">

        {/* PLATS DU JOUR */}

        <section className="daily-section">

          <div className="daily-title">
            <span>PLATS DU JOUR</span>
            <h2>Nos suggestions du jour</h2>
          </div>

          <div className="slider">

            <img
              src={platsDuJour[slide].image}
              alt={platsDuJour[slide].nom}
              className="slider-image"
            />

            <div className="slider-overlay"></div>

            <div className="slider-info">

              <span className="daily-badge">
                PLAT DU JOUR
              </span>

              <h2>
                {platsDuJour[slide].nom}
              </h2>

              <p>
                {platsDuJour[slide].prix.toLocaleString("fr-FR")} FCFA
              </p>

            </div>

            <button
              className="slider-button slider-left"
              onClick={precedent}
            >
              <ChevronLeft size={28} />
            </button>

            <button
              className="slider-button slider-right"
              onClick={suivant}
            >
              <ChevronRight size={28} />
            </button>

            <div className="slider-dots">

              {platsDuJour.map((_, index) => (
                <button
                  key={index}
                  className={
                    index === slide
                      ? "slider-dot active"
                      : "slider-dot"
                  }
                  onClick={() => setSlide(index)}
                ></button>
              ))}

            </div>

          </div>

        </section>

        {/* MENU */}

        <div className="menu-introduction">
          <h2>Découvrez notre menu</h2>

          <p>
            Choisissez les plats que vous souhaitez commander.
          </p>
        </div>

        <div className="category-title">
          Plats principaux
        </div>

        <div className="plats-container">

          {plats.map((plat) => (
            <div className="plat-card" key={plat.id}>

            <div className="plat-image">
              <img
              src={plat.image}
              alt={plat.nom}
              />
            </div>

              <div className="plat-info">

                <h3>{plat.nom}</h3>

                <p>
                  {plat.description}
                </p>

                <div className="plat-footer">

                  <span className="plat-price">
                    {plat.prix.toLocaleString("fr-FR")} FCFA
                  </span>

                  <button
                    className={`add-button ${platAjouteId === plat.id ? "added" : ""}`}
                    onClick={() => ajouterAvecFeedback(plat)}
                    aria-label={`${platAjouteId === plat.id ? "Plat ajouté" : "Ajouter"} ${plat.nom}`}
                  >
                    <Plus
                      className={platAjouteId === plat.id ? "feedback-hidden" : ""}
                      size={20}
                    />
                    <Check
                      className={platAjouteId === plat.id ? "" : "feedback-hidden"}
                      size={20}
                    />
                    <span className={platAjouteId === plat.id ? "feedback-hidden" : ""}>
                      Ajouter
                    </span>
                    <span className={platAjouteId === plat.id ? "" : "feedback-hidden"}>
                      Ajouté
                    </span>
                  </button>

                </div>

              </div>

            </div>
          ))}

        </div>

      </main>

    </div>
  )
}

export default Menu