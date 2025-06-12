# 📄 TendrAI – Assistant IA pour Appels d’Offres

> Automatiser la compréhension, la réponse et le suivi des appels d’offres publics et privés grâce à l’intelligence artificielle.

---

## 🚀 Objectif

**TendrAI** est une solution européenne et souveraine qui aide les TPE, PME, indépendants et consultants à :
- Comprendre rapidement les documents d’appel d’offres (PDF, Word…)
- Générer automatiquement des documents de réponse (lettres, mémoires techniques…)
- Suivre les pièces demandées avec des checklists IA
- Gagner du temps tout en respectant les exigences des marchés publics et privés

---

## 🧪 Fonctionnalités MVP

- 🔍 **Upload de documents** (PDF, DOCX…)
- 📌 **Résumé IA des exigences clés**
- ✍️ **Générateur de documents types personnalisés**
- ✅ **Checklist dynamique à cocher**
- ⏰ **Deadline + rappels (à venir)**

---

## 🧰 Stack technique (version 100 % européenne 🇪🇺)

| Composant         | Technologie / Fournisseur           |
|------------------|--------------------------------------|
| Backend API       | ASP.NET Core                        |
| Frontend UI       | Blazor Server (C#)                  |
| OCR Document      | Mindee API (🇫🇷)                    |
| IA Générative     | NLP Cloud + modèles Mistral (🇫🇷)   |
| Base de données   | PostgreSQL (via Clever Cloud)       |
| Hébergement       | Clever Cloud (🇫🇷)                   |
| Stockage fichiers | Scaleway Object Storage (🇫🇷)       |
| Authentification  | OAuth (Google / Microsoft 365)      |

---

## 📁 Structure du projet

```

/tendrai-mvp
├── backend/            → API ASP.NET Core
├── frontend/           → UI Blazor Server
├── prompts/            → Templates et prompts IA
├── docs/               → Spécifications, maquettes
├── scripts/            → Déploiement PaaS Clever

````

---

## 🔧 Lancer en local (dev)

### Prérequis
- [.NET SDK 8.0](https://dotnet.microsoft.com/)
- PostgreSQL local ou URL Clever Cloud
- Compte Mindee API (OCR)
- Compte NLP Cloud (ou modèle Mistral local)
- Clés OAuth Google/Microsoft pour authentification

### Setup
```bash
git clone https://github.com/saadsabir/tendrai-api.git
cd tendrai-api/backend
dotnet restore
dotnet run
````

UI : [http://localhost:5000](http://localhost:5000)
API : [http://localhost:5000/api](http://localhost:5000/api)

---

## 📦 Roadmap MVP

* [x] Intégration Mindee OCR
* [x] Résumé IA de documents
* [ ] Génération lettre de réponse IA
* [ ] Checklist dynamique
* [ ] UI propre avec tri de dossiers
* [ ] Export PDF mémoire de réponse

---

## 💡 Vision long terme

* Intégration Chorus Pro
* Espace multi-utilisateur et délégation
* Marketplace de rédacteurs experts
* Connexion avec journaux d’annonces (BOAMP, JOUE…)

---

## 🔒 Sécurité & conformité

* Données hébergées en 🇪🇺
* Respect RGPD
* Aucune conservation des documents par défaut

---

## 🧾 Licence

Licence propriétaire (commerciale, tous droits réservés) © 2025 – \[Saad SABIR / TendrAI]

```

---
