import { useState } from "react"
import { ShoppingBag, Utensils } from "lucide-react"
import { useNavigate } from "react-router-dom"

function ChoixTable() {
  const [table, setTable] = useState("")
  const [mode, setMode] = useState("")
  const navigate = useNavigate()

 const commencer = () => {
  if (mode === "emporter") {
    localStorage.removeItem("tableClient")
    localStorage.setItem("typeCommande", "emporter")
    navigate("/menu")
    return
  }

  const numeroTable = Number(table)

  const numerosTablesDisponibles = [1, 2, 3, 4, 5, 6]

  if (!numerosTablesDisponibles.includes(numeroTable)) {
    alert("Veuillez entrer un numéro de table valide : 1, 2, 3, 4, 5 ou 6.")
    return
  }

  localStorage.setItem("tableClient", String(numeroTable))
  localStorage.setItem("typeCommande", "sur-place")
  navigate("/menu")
}
  

  return (
    <div className="table-page">

      <div className="table-card">

        <div className="table-icon">
          <Utensils size={38} strokeWidth={1.7} />
        </div>

        <h1>Bienvenue</h1>

        <p>
          Comment souhaitez-vous recevoir votre commande ?
        </p>

        <div className="order-mode-options">
          <button
            type="button"
            className={`order-mode-option ${mode === "sur-place" ? "selected" : ""}`}
            onClick={() => setMode("sur-place")}
          >
            <Utensils size={24} />
            <span>Sur place</span>
          </button>
          <button
            type="button"
            className={`order-mode-option ${mode === "emporter" ? "selected" : ""}`}
            onClick={() => setMode("emporter")}
          >
            <ShoppingBag size={24} />
            <span>À emporter</span>
          </button>
        </div>

        {mode === "sur-place" && (
          <>
            <label htmlFor="numero-table">
              Numéro de table
            </label>

            <input
              id="numero-table"
              type="number"
              min="1"
              max="20"
              placeholder="Ex : 5"
              value={table}
              onChange={(e) => setTable(e.target.value)}
            />
          </>
        )}

        <button
          className="start-order-button"
          disabled={!mode}
          onClick={commencer}
        >
          {mode === "sur-place" ? "CONTINUER" : "VOIR LE MENU"}
        </button>

      </div>

    </div>
  )
}

export default ChoixTable