import { createContext, useState } from "react"

export const CartContext = createContext()

export function CartProvider({ children }) {
  const [panier, setPanier] = useState([])

  const ajouterAuPanier = (plat) => {
    setPanier((ancienPanier) => {
      const platExiste = ancienPanier.find(
        (item) => item.id === plat.id
      )

      if (platExiste) {
        return ancienPanier.map((item) =>
          item.id === plat.id
            ? { ...item, quantite: item.quantite + 1 }
            : item
        )
      }

      return [
        ...ancienPanier,
        {
          ...plat,
          quantite: 1,
        },
      ]
    })
  }

  const supprimerDuPanier = (id) => {
    setPanier((ancienPanier) =>
      ancienPanier.filter((item) => item.id !== id)
    )
  }

  const augmenterQuantite = (id) => {
    setPanier((ancienPanier) =>
      ancienPanier.map((item) =>
        item.id === id
          ? { ...item, quantite: item.quantite + 1 }
          : item
      )
    )
  }

  const diminuerQuantite = (id) => {
    setPanier((ancienPanier) =>
      ancienPanier
        .map((item) =>
          item.id === id
            ? { ...item, quantite: item.quantite - 1 }
            : item
        )
        .filter((item) => item.quantite > 0)
    )
  }

  const viderPanier = () => {
    setPanier([])
  }

  return (
    <CartContext.Provider
      value={{
        panier,
        ajouterAuPanier,
        supprimerDuPanier,
        augmenterQuantite,
        diminuerQuantite,
        viderPanier,
      }}
    >
      {children}
    </CartContext.Provider>
  )
}