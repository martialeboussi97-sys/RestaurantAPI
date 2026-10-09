import { useState } from "react"
import { useNavigate } from "react-router-dom"
import { UserRound, ArrowLeft, ArrowRight } from "lucide-react"

function Employe() {
  const navigate = useNavigate()

  const [identifiant, setIdentifiant] = useState("")
  const [motDePasse, setMotDePasse] = useState("")
  const [chargement, setChargement] = useState(false)
  const [erreur, setErreur] = useState("")

  const handleConnexion = async (e) => {
    e.preventDefault()

    setErreur("")

    if (!identifiant || !motDePasse) {
      setErreur("Veuillez remplir tous les champs.")
      return
    }

    try {
      setChargement(true)

      const response = await fetch(
        "https://localhost:7217/api/Utilisateurs/login",
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json"
          },
          body: JSON.stringify({
            nomUtilisateur: identifiant,
            motDePasse: motDePasse
          })
        }
      )

      const texte = await response.text()

      console.log("CONNEXION :", response.status)
      console.log("RÉPONSE :", texte)

      if (!response.ok) {
        throw new Error(
          texte || "Identifiant ou mot de passe incorrect."
        )
      }

      const utilisateur = JSON.parse(texte) || {}

      const profil = utilisateur.employe || utilisateur.employee || utilisateur
      const utilisateurAvecProfil = {
        ...utilisateur,
        nomUtilisateur: utilisateur.nomUtilisateur || identifiant.trim(),
        employe: {
          ...profil,
          prenom:
            profil.prenom ||
            profil.firstName ||
            profil.prenomEmploye ||
            identifiant
        }
      }

      // Sauvegarder les informations de l'employé
      localStorage.setItem(
        "employeConnecte",
        JSON.stringify(utilisateurAvecProfil)
      )

      const poste = String(
        utilisateurAvecProfil.poste?.nomPoste ||
        utilisateurAvecProfil.poste?.libelle ||
        utilisateurAvecProfil.posteNavigation?.nomPoste ||
        (typeof utilisateurAvecProfil.poste === "string"
          ? utilisateurAvecProfil.poste
          : "") ||
        utilisateurAvecProfil.nomPoste ||
        utilisateurAvecProfil.role ||
        ""
      ).toLowerCase()
      const nomUtilisateur = identifiant.trim().toLowerCase()
      const estCaissier =
        poste.includes("caiss") ||
        poste.includes("caisse") ||
        nomUtilisateur === "emp002"

      navigate(estCaissier ? "/caisse" : "/espace-employe")

    } catch (error) {
      console.error("ERREUR CONNEXION :", error)
      setErreur(
        error.message ||
        "Impossible de se connecter."
      )
    } finally {
      setChargement(false)
    }
  }

  return (
    <div className="employe-page">

      <button
        className="back-button"
        onClick={() => navigate("/")}
      >
        <ArrowLeft size={20} />
        Retour
      </button>

      <div className="employe-card">

        <div className="employe-icon">
          <UserRound size={45} />
        </div>

        <h1>Espace Employé</h1>

        <p>
          Connectez-vous pour accéder à votre espace de travail.
        </p>

        <form onSubmit={handleConnexion}>

          <label>Identifiant</label>

          <input
            type="text"
            placeholder="Votre identifiant"
            value={identifiant}
            onChange={(e) => setIdentifiant(e.target.value)}
          />

          <label>Mot de passe</label>

          <input
            type="password"
            placeholder="Votre mot de passe"
            value={motDePasse}
            onChange={(e) => setMotDePasse(e.target.value)}
          />

          {erreur && (
            <div className="serveur-error">
              {erreur}
            </div>
          )}

          <button
            type="submit"
            className="connexion-button"
            disabled={chargement}
          >
            {chargement
              ? "Connexion..."
              : "Se connecter"}

            {!chargement && (
              <ArrowRight size={20} />
            )}
          </button>

        </form>

      </div>

    </div>
  )
}

export default Employe