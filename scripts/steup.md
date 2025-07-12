Below is a **working-directory blueprint** that matches every code file we’ve produced so far, followed by a **from-scratch, copy-paste guide** that gets you from an empty folder to a running API + WPF client.

---

## 1  Directory & file map  (`GymAccess/` root)

```
GymAccess/
│  GymAccess.sln
│
├─src/
│   ├─Gym.Api/
│   │   │  Gym.Api.csproj
│   │   │  Program.cs
│   │   │  appsettings.json
│   │   │
│   │   ├─Auth/
│   │   │     JwtSettings.cs
│   │   │     TokenService.cs
│   │   │     AuthExtensions.cs
│   │   │
│   │   ├─Data/
│   │   │     GymContext.cs
│   │   │     DesignTimeFactory.cs
│   │   │
│   │   ├─Models/
│   │   │     Member.cs
│   │   │     Plan.cs
│   │   │     AppUser.cs
│   │   │
│   │   ├─Dtos/
│   │   │     MemberDto.cs
│   │   │     PlanDto.cs
│   │   │
│   │   ├─Mapping/
│   │   │     MappingProfile.cs
│   │   │
│   │   ├─Repositories/
│   │   │     IMemberRepo.cs
│   │   │     MemberRepo.cs
│   │   │     IPlanRepo.cs
│   │   │     PlanRepo.cs
│   │   │
│   │   ├─Services/
│   │   │     MemberService.cs
│   │   │     PlanService.cs
│   │   │
│   │   ├─Endpoints/
│   │   │     AuthEndpoints.cs
│   │   │     MemberEndpoints.cs
│   │   │     PlanEndpoints.cs
│   │   │
│   │   ├─Middleware/
│   │   │     ExceptionMiddleware.cs
│   │   │
│   │   └─DI/
│   │         DiRegistration.cs
│   │
│   ├─Gym.Core/
│   │   │  Gym.Core.csproj
│   │   │  (empty for now – add shared primitives later)
│   │
│   └─Gym.Client/
│        Gym.Client.csproj
│        MainWindow.xaml
│        MainWindow.xaml.cs
│        (your WPF/XAML files here)
│
├─scripts/
│     schema_v1.1_utf8mb4.sql
│     demo_data_2025-07-12.sql
│
└─README.md   (deployment & build notes)
```

*Everything else—`.gitignore`, `Directory.Build.props`, CI YAML—lives alongside these folders.*

---

## 2  Step-by-step setup (PowerShell)

### 2.0 Prerequisites

* .NET SDK 8.\* (`dotnet --version` should print `8.x.x`)
* **Optional**: Windows Desktop workload (`dotnet workload install windowsdesktop`) for WPF
* Git, MySQL 8.3, Node if you’ll build the Electron client later

```powershell
# 1️⃣  Create the workspace
mkdir GymAccess ; cd GymAccess
git init
dotnet new sln -n GymAccess
mkdir src , scripts
```

---

### 2.1 Create projects

```powershell
# Web API
dotnet new webapi  -n Gym.Api   -f net8.0  -o .\src\Gym.Api

# Core shared lib
dotnet new classlib -n Gym.Core -f net8.0  -o .\src\Gym.Core

# Desktop client (WPF)
dotnet new wpf      -n Gym.Client -f net8.0 -o .\src\Gym.Client
```

Add them to the solution:

```powershell
dotnet sln add .\src\Gym.Api\Gym.Api.csproj `
               .\src\Gym.Core\Gym.Core.csproj `
               .\src\Gym.Client\Gym.Client.csproj
```

---

### 2.2 Install NuGet packages

```powershell
cd src/Gym.Api
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Pomelo.EntityFrameworkCore.MySql
dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection
dotnet add package Swashbuckle.AspNetCore
dotnet add package Serilog.AspNetCore
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
cd ../..
```

(WPF client will need packages when you start UI work.)

---

### 2.3 Drop the code in place

*Open your editor* and copy-paste the exact contents from the canvas into their respective paths:

* `Program.cs`, `appsettings.json`, each sub-folder file, etc.

> **Tip:** in VS Code the *Explorer* panel shows the same tree—add each file with **right-click → New File**.

---

### 2.4 Database

1. Install MySQL 8.3, create `gymapp` user.
2. Run `scripts/schema_v1.1_utf8mb4.sql`, then `scripts/demo_data_2025-07-12.sql`.
3. Put the resulting connection string in `src/Gym.Api/appsettings.json`.

---

### 2.5 Run & test

```powershell
dotnet build     # should succeed
dotnet run --project .\src\Gym.Api      # API listens on https://localhost:5001

# Open browser -> https://localhost:5001/swagger
# Click Authorize (top-right), enter Bearer token after /auth/login
```

For the WPF client:

```powershell
dotnet run --project .\src\Gym.Client
```

Point it at `https://localhost:5001`.

---

### 2.6 Commit & push

```powershell
git add .
git commit -m "Initial skeleton with API, Core and Client projects"
git remote add origin <your-repo-url>
git push -u origin main
```

---

## 3  Next milestones (cheat sheet)

| Milestone         | What to do                                                    |
| ----------------- | ------------------------------------------------------------- |
| **EF Migrations** | `dotnet ef migrations add Init -p src/Gym.Api -s src/Gym.Api` |
| **Unit tests**    | `dotnet new xunit -n Gym.Tests -o tests/Gym.Tests`            |
| **CI/CD**         | Add `.github/workflows/ci.yml` building the solution          |

Once these are in, you’ve got a **clean, reproducible, production-grade skeleton**. Drop me a line any time you’re ready to flesh out new features or automate the build pipeline!
