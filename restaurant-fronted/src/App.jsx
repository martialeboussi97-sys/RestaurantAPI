import { BrowserRouter, Routes, Route } from "react-router-dom"

import Accueil from "./pages/Accueil"
import ChoixTable from "./pages/ChoixTable"
import Menu from "./pages/Menu"
import Panier from "./pages/Panier"
import Confirmation from "./pages/Confirmation"
import Employe from "./pages/Employe"
import Serveur from "./pages/Serveur"
import DetailsCommande from "./pages/DetailsCommande"
import EspaceEmploye from "./pages/EspaceEmploye"
import Caisse from "./pages/Caisse"

function App() {
  return (
    <BrowserRouter>

      <Routes>

        <Route path="/" element={<Accueil />} />

        <Route path="/employe" element={<Employe />} />

        <Route path="/serveur" element={<Serveur />} />

        <Route path="/caisse" element={<Caisse />} />

        <Route
  path="/espace-employe"
  element={<EspaceEmploye />}
/>

        <Route path="/table" element={<ChoixTable />} />

        <Route path="/menu" element={<Menu />} />
        
        <Route path="/panier" element={<Panier />} />
        
        <Route path="/confirmation" element={<Confirmation />} />

        <Route
  path="/commande/:id"
  element={<DetailsCommande />}
/>

      </Routes>

    </BrowserRouter>
  )
}

export default App