\# RestaurantAPI — Application de gestion de restaurant



\## 1. Présentation du projet



RestaurantAPI est une application de gestion de restaurant développée avec ASP.NET Core, Entity Framework Core, SQL Server et React.



Le projet vise à centraliser les principales opérations d'un restaurant au sein d'une application composée d'une API backend et d'une interface frontend.



Il comprend notamment la gestion des clients, des commandes, des plats, des catégories, des tables et de différentes opérations internes du restaurant.



\## 2. Technologies utilisées



\### Backend



\* C#

\* ASP.NET Core (.NET 10)

\* Entity Framework Core 10

\* SQL Server

\* Swagger / OpenAPI



\### Frontend



\* React 19

\* Vite

\* React Router

\* Lucide React



\### Base de données



\* Microsoft SQL Server

\* Script SQL de création du schéma disponible dans le dossier `scripts/`



\## 3. Structure du projet



```text

RestaurantAPI/

├── Controllers/             # Contrôleurs de l'API

├── Data/                    # Accès et configuration des données

├── DTOs/                    # Objets de transfert de données

├── Models/                  # Modèles de données

├── Properties/              # Configuration du lancement

├── restaurant-fronted/      # Interface React

├── scripts/

│   └── RestaurantSchema\_Portable.sql

├── appsettings.json         # Configuration de l'application

├── Program.cs               # Point d'entrée du backend

├── RestaurantAPI.csproj     # Projet .NET

└── RestaurantAPI.slnx       # Solution .NET

```



\## 4. Prérequis



Avant de lancer le projet, installer les outils suivants :



\* .NET SDK 10

\* Microsoft SQL Server, dans une version compatible avec le script SQL fourni

\* Node.js et npm

\* Git



SQL Server Management Studio (SSMS) est recommandé pour consulter et administrer la base de données.



\## 5. Installation de la base de données



Le script de création du schéma se trouve ici :



`scripts/RestaurantSchema\_Portable.sql`



\### Procédure



1\. Installer et démarrer une instance compatible de SQL Server.

2\. Ouvrir SQL Server Management Studio.

3\. Se connecter à l'instance SQL Server.

4\. Ouvrir le fichier `scripts/RestaurantSchema\_Portable.sql`.

5\. Vérifier que le script cible une base de données neuve et qu'aucune base `Restaurant` existante ne sera écrasée.

6\. Exécuter le script.

7\. Vérifier que les tables et leurs relations ont été créées correctement.



\*\*Important :\*\* ce script crée le schéma de la base de données, pas les données de démonstration. Il est destiné à une installation neuve et ne doit pas être exécuté sur une base `Restaurant` déjà existante.



\## 6. Configuration du backend



La chaîne de connexion se trouve dans `appsettings.json`.



Exemple de configuration locale utilisant l'authentification Windows :



```json

{

&#x20; "ConnectionStrings": {

&#x20;   "DefaultConnection": "Server=localhost\\\\SQLEXPRESS;Database=Restaurant;Trusted\_Connection=True;TrustServerCertificate=True;"

&#x20; }

}

```



Adapter la valeur de `Server` au nom de l'instance SQL Server utilisée sur l'ordinateur.



Cette configuration utilise l'authentification Windows. Le compte exécutant l'application doit disposer des autorisations nécessaires sur la base de données.



Le paramètre `TrustServerCertificate=True` convient à certains environnements de développement local. Pour un déploiement en production, configurer correctement le certificat TLS et la connexion sécurisée.



\## 7. Lancement du backend



Ouvrir un terminal à la racine du projet.



Restaurer les dépendances :



```powershell

dotnet restore

```



Compiler le projet :



```powershell

dotnet build

```



Démarrer l'API :



```powershell

dotnet run

```



L'API utilise les URL de lancement configurées dans le projet. Vérifier les URL affichées dans le terminal après le démarrage.



Si Swagger est activé dans l'environnement utilisé, ouvrir la route Swagger indiquée par la configuration de lancement afin de consulter et tester les endpoints de l'API.



\## 8. Installation et lancement du frontend



Ouvrir un \*\*deuxième terminal\*\* et se placer dans le dossier du frontend :



```powershell

cd restaurant-fronted

```



Installer les dépendances :



```powershell

npm install

```



Démarrer le serveur de développement :



```powershell

npm run dev

```



Vite affiche l'adresse locale de l'interface dans le terminal. Par défaut, elle est généralement :



`http://localhost:5173`



Pour vérifier la compilation du frontend :



```powershell

npm run build

```



\## 9. Organisation du lancement



Pour utiliser l'application, démarrer :



1\. SQL Server et vérifier que la base `Restaurant` est accessible.

2\. Le backend ASP.NET Core.

3\. Le frontend React.



Le backend et le frontend doivent être lancés dans deux terminaux distincts.



Les URL utilisées par le frontend pour communiquer avec l'API doivent correspondre à la configuration réelle du backend.



\## 10. Dépannage



\### Impossible de se connecter à SQL Server



\* Vérifier que le service SQL Server est démarré.

\* Vérifier le nom de l'instance dans la chaîne de connexion.

\* Vérifier que la base `Restaurant` existe.

\* Vérifier les autorisations du compte Windows utilisé.



\### Le backend ne démarre pas



\* Vérifier que le SDK .NET 10 est installé.

\* Exécuter `dotnet restore`.

\* Exécuter `dotnet build` et examiner les éventuelles erreurs.



\### Le frontend ne démarre pas



\* Vérifier que Node.js et npm sont installés.

\* Exécuter `npm install` dans `restaurant-fronted`.

\* Exécuter `npm run dev` et consulter les messages du terminal.



\## 11. Dépôt du projet



Code source et fichiers du projet :



https://github.com/martialeboussi97-sys/RestaurantAPI



\---



\*\*Projet de développement informatique — Application de gestion de restaurant\*\*



