import { Component } from "react"

class ErrorBoundary extends Component {
  state = { hasError: false, error: null }

  static getDerivedStateFromError(error) {
    return { hasError: true, error }
  }

  render() {
    if (!this.state.hasError) {
      return this.props.children
    }

    return (
      <main style={{ padding: "40px", fontFamily: "Arial, sans-serif", color: "#32145f" }}>
        <h1>Une erreur est survenue</h1>
        <p>Recharge la page avec Ctrl + F5.</p>
        <pre style={{ whiteSpace: "pre-wrap" }}>{this.state.error?.message}</pre>
      </main>
    )
  }
}

export default ErrorBoundary