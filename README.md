# 📽️ MediaFlix
### Your Digital Book & Movie Shelf

MediaFlix is a fullstack web application where users can track their books and movies:

✅ What they’ve read or watched

✅ What they want to explore in the future

Users can also leave reviews and ratings for each title.

## ✨ Project Overview

Backend API development (ASP.NET Core Web API)

SQL database integration (via Entity Framework)

Frontend React application (communicating with the backend)

## 📦 Features

📚 Add books and movies with:

Title

Genre

Creator

Type

Status

📋 View a list of all media

🔍 Filter media by type or status

⭐ Leave reviews and ratings for each media item

🛠️ Manage and update your personal media library

## 🛠 Tech Stack

Layer	Tools
Backend	ASP.NET Core Web API, Entity Framework, SQL Server
Frontend	React (Vite or CRA), Axios or Fetch
Tools	Visual Studio, Visual Studio Code, Swagger, GitHub

## 📂 Project Structure

```
/MediaFlix
├── /Backend (ASP.NET Core API)
│ ├── Controllers/
│ ├── Models/
│ ├── Data/
│ └── MediaFlix.sln
└── /Frontend (React)
├── src/
└── package.json
```

## ⚙️ How to Run the Backend

Clone the repository:
```bash
git clone https://github.com/your-username/MediaFlix.git
```

Navigate to the backend folder and run migrations:
```bash
cd MediaFlix/backend
dotnet ef database update
```

Start the backend API:
```bash
dotnet run
```

Visit Swagger docs:
https://localhost:{port}/swagger

## ⚙️ How to Run the Frontend

Navigate to the React app folder:
```bash
cd MediaFlix/frontend
```

Install dependencies:
```bash
npm install
```

Start the React app:
```bash
npm run dev
```

## ✅ Project Goals

Build clean and tested API endpoints

Connect React frontend smoothly to backend

Practice teamwork, GitHub workflows, and project planning

Deliver a functional fullstack project ready to present

## 🤝 Contributors

Name	Role

Dorsa	Developer
Kristina	Developer

