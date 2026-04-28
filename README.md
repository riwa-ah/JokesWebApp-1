# Ha Ha Hub - Jokes Web Application

> A vibrant, full-featured ASP.NET Core web application for managing and sharing jokes with a comic-inspired UI and user authentication.

**Live Demo:** https://jokesweb1.onrender.com

---

## Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Technology Stack](#technology-stack)
- [Prerequisites](#prerequisites)
- [Installation & Setup](#installation--setup)
- [Configuration](#configuration)
- [Running the Application](#running-the-application)
- [Project Structure](#project-structure)
- [Usage](#usage)
- [Deployment](#deployment)
- [License](#license)

---

## Overview

**Ha Ha Hub** is a modern ASP.NET Core web application designed for browsing, creating, editing, and managing jokes. The application features a playful, comic-inspired user interface with vibrant colors, smooth animations, and an engaging user experience. Built with security in mind, it includes user authentication, role-based access control, and data persistence.

---

## Features

### Core Functionality
- ✅ **Browse Jokes** - View all jokes with a beautiful flip-card interface
- ✅ **Create Jokes** - Add new jokes to the collection with questions and answers
- ✅ **Edit Jokes** - Modify existing jokes
- ✅ **Delete Jokes** - Remove jokes from the database
- ✅ **Search Jokes** - Find specific jokes by keyword
- ✅ **User Authentication** - Secure login and registration with ASP.NET Identity
- ✅ **Responsive Design** - Mobile-friendly interface that works on all devices

### UI/UX Features
- 🎨 Comic-inspired design with custom branding
- 🎭 Animated flip-card jokes with reveal mechanics
- 📱 Mobile-responsive Bootstrap layout
- ✨ Smooth transitions and hover effects
- 🎪 Vibrant color scheme (yellow, red, teal, dark ink)
- 🌐 Full-width hero sections and call-to-action banners

---

## Technology Stack

### Backend
- **Framework:** ASP.NET Core 8.0
- **Language:** C#
- **ORM:** Entity Framework Core 8.0
- **Database:** PostgreSQL (Supabase)
- **Authentication:** ASP.NET Identity

### Frontend
- **UI Framework:** Bootstrap 5
- **Styling:** Custom CSS with animations
- **Fonts:** Google Fonts (Bangers, Nunito)
- **JavaScript:** jQuery, Bootstrap JS

### DevOps & Deployment
- **Container:** Docker
- **Hosting:** Render
- **Database Hosting:** Supabase PostgreSQL

---

## Prerequisites

Before you begin, ensure you have the following installed:

- **.NET SDK 8.0 or later** - [Download](https://dotnet.microsoft.com/download)
- **Visual Studio 2022 or VS Code** - Code editor
- **PostgreSQL 12+** - Database (local development)
- **Docker** (Optional) - For containerized deployment

### Recommended
- Git for version control
- Postman for API testing (if applicable)

---

## Installation & Setup

### 1. Clone the Repository
```bash
git clone https://github.com/yourusername/JokesWebApp.git
cd JokesWebApp
```

### 2. Restore Dependencies
```bash
dotnet restore
```

### 3. Set Up the Database
```bash
dotnet ef database update
```

This will create the database and apply all migrations.

### 4. (Optional) Seed Sample Data
```bash
# Run the app and use the UI to manually add jokes, or modify Program.cs 
# to include seed data initialization
```

---

## Configuration

### Database Connection String
Update the connection string in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=your_host;Port=5432;Database=your_db;Username=your_user;Password=your_password;SSL Mode=Require"
  }
}
```

### Environment Variables
Create a `.env` file or use user secrets for sensitive data:

```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "your-connection-string"
```

### Application Settings
Modify `appsettings.json` for:
- Logging levels
- Allowed hosts
- CORS policies
- Identity settings

---

## Running the Application

### Development Mode
```bash
dotnet run
```
The application will start at `https://localhost:5001` (or `http://localhost:5000`)

### With Hot Reload
```bash
dotnet watch run
```

### Production Mode
```bash
dotnet publish -c Release
dotnet JokesWebApp.dll --environment=Production
```

---

## Project Structure

```
JokesWebApp/
├── Areas/                          # ASP.NET Identity scaffolded pages
│   └── Identity/Pages/
│       └── Account/               # Login, Register, Logout pages
├── Controllers/                    # MVC Controllers
│   ├── HomeController.cs          # Home page logic
│   └── JokesController.cs         # Jokes CRUD operations
├── Data/                           # Database context
│   └── ApplicationDbContext.cs    # EF Core DbContext
├── Models/                         # Data models
│   ├── Joke.cs                    # Joke model
│   └── ErrorViewModel.cs          # Error handling model
├── Views/                          # Razor views
│   ├── Home/                      # Home page views
│   ├── Jokes/                     # Joke management views
│   └── Shared/                    # Layout and partials
├── Properties/                     # Project configuration
├── wwwroot/                        # Static files
│   ├── css/site.css               # Custom styles
│   ├── js/site.js                 # Client-side scripts
│   └── favicon.ico                # Favicon
├── Migrations/                     # EF Core migrations
├── Program.cs                      # Application startup configuration
├── appsettings.json               # Configuration settings
├── JokesWebApp.csproj             # Project file
├── Dockerfile                      # Docker configuration
└── README.md                       # This file
```

---

## Usage

### For End Users

1. **Browse Jokes**
   - Navigate to the Jokes page from the main menu
   - Click on any joke card to flip and reveal the punchline
   - Use the Search function to find specific jokes

2. **Create a Joke**
   - Click "Create New Joke"
   - Enter the joke question and answer
   - Click Save

3. **Edit/Delete**
   - Navigate to the Jokes index
   - Click Edit or Delete on any joke card
   - Confirm your action

### For Developers

**Adding a New Feature:**
1. Create migrations for database changes: `dotnet ef migrations add FeatureName`
2. Update the DbContext in `Data/ApplicationDbContext.cs`
3. Implement controllers and views
4. Apply migrations: `dotnet ef database update`

**Running Tests:**
```bash
dotnet test
```

---

## Deployment

### Deploy to Render

1. Push code to GitHub repository
2. Connect repository to Render
3. Configure environment variables:
   - `ConnectionStrings:DefaultConnection`
   - `ASPNETCORE_ENVIRONMENT=Production`
4. Set build and start commands:
   - **Build:** `dotnet publish -c Release -o out`
   - **Start:** `cd out && dotnet JokesWebApp.dll`
5. Deploy and monitor logs

### Docker Deployment

```bash
# Build Docker image
docker build -t jokes-webapp .

# Run container
docker run -p 8080:80 \
  -e "ConnectionStrings:DefaultConnection=your-connection-string" \
  jokes-webapp
```

---

## Database Schema

### Jokes Table
```sql
CREATE TABLE Jokes (
    Id INT PRIMARY KEY IDENTITY(1,1),
    JokeQuestion NVARCHAR(MAX) NOT NULL,
    JokeAnswer NVARCHAR(MAX) NOT NULL
);
```

### AspNetUsers Table
(Managed by ASP.NET Identity)

---

## Troubleshooting

### Connection String Errors
- Ensure PostgreSQL is running
- Verify connection string credentials
- Check firewall/network settings

### Migration Errors
```bash
# Reset migrations (Development only)
dotnet ef database drop
dotnet ef database update
```

### Port Already in Use
```bash
# Change port in launchSettings.json or use:
dotnet run --urls "http://localhost:5002"
```

---

## Contributing

Contributions are welcome! Please follow these steps:

1. Fork the repository
2. Create a feature branch: `git checkout -b feature/YourFeature`
3. Commit changes: `git commit -m "Add YourFeature"`
4. Push to branch: `git push origin feature/YourFeature`
5. Open a Pull Request

---

## License

This project is licensed under the MIT License - see the LICENSE file for details.

---

## Contact & Support

For questions or issues, please [open an issue](https://github.com/riwa-ah/JokesWebApp/issues) on GitHub.

---

**Made with ❤️ by the Ha Ha Hub Team**
