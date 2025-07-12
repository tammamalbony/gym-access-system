# Gym Access System – README

*A complete local-LAN membership, billing, and door-control stack.*

---

## 1  Project Snapshot

| Layer                | Key tech                             | Role                                        |
| -------------------- | ------------------------------------ | ------------------------------------------- |
| **Desktop client**   | .NET 8 WPF **or** Electron + React   | Front-desk data entry & fingerprint capture |
| **API / Backend**    | ASP.NET Core 8 + EF Core, REST JSON  | Business logic, token push, email alerts    |
| **DB**               | **MySQL 8.3** (`utf8mb4_unicode_ci`) | Members, subscriptions, logs                |
| **Door controllers** | SecuPrint FW 1.2.3 (LAN)             | Local cache & finger match                  |
| **Reporting portal** | Razor Pages (hosted by API)          | Admin dashboards, PDF/CSV exports           |

---

## 2  Prerequisites

| Tool                       | Min Version     | Install                                 |
| -------------------------- | --------------- | --------------------------------------- |
| Windows 11 23H2            | Build 22631     | Already on PC                           |
| .NET SDK                   | **8 LTS**       | `winget install Microsoft.DotNet.SDK.8` |
| Node (only if Electron UI) | 20 LTS          | `winget install OpenJS.NodeJS.LTS`      |
| MySQL Server               | 8.3             | `winget install Oracle.MySQL`           |
| MySQL Workbench (optional) | 8.3             | GUI admin                               |
| Fingerprint SDK            | Vendor’s latest | USB driver & `bio.dll`                  |

> **All installs must run from an elevated PowerShell.**
> Re-boot once after driver setup.

---

## 3  Quick-start in 7 Steps

```powershell
# 1️⃣  Clone
git clone https://github.com/YourOrg/gym-access-system C:\GymApp
cd C:\GymApp

# 2️⃣  Configure DB (run in MySQL CLI / Workbench)
SOURCE scripts/schema_v1.1_utf8mb4.sql     -- tables
SOURCE scripts/demo_data_2025-07-12.sql     -- demo rows

# 3️⃣  Add DB user
CREATE USER 'gymapp'@'%' IDENTIFIED BY 'S3cureP@ss!';
GRANT ALL ON gym_access_system.* TO 'gymapp'@'%';

# 4️⃣  Update API settings
copy appsettings.Production.json.example appsettings.Production.json
copy .env.example .env                     # connection string & JWT secret
notepad appsettings.Production.json        # add SMTP, DB, subnet

# 5️⃣  Publish API as Windows service
dotnet publish src/Gym.Api -c Release -o C:\GymApi\dist --sc
sc create GymApiSvc binPath= "C:\GymApi\dist\Gym.Api.exe" start= auto
sc start GymApiSvc

# 6️⃣  Open firewall
New-NetFirewallRule -DisplayName "Gym API" -Dir In -Action Allow -Protocol TCP -LocalPort 5000

# 7️⃣  Install front-desk client
msiexec /i installers/GymClientSetup.msi /qn
```

Open browser → `http://<api-ip>:5000/swagger` to verify endpoints.

---

## 4  Default Demo Accounts

| Role              | Username     | Password (on first boot) | Notes                                       |
| ----------------- | ------------ | ------------------------ | ------------------------------------------- |
| **Administrator** | `admin`      | `ChangeMe!`              | Full access; **must change on first login** |
| Data-entry clerk  | `data_entry` | `ChangeMe!`              | Member CRUD, payments                       |
| Support tech      | `support`    | `ChangeMe!`              | Firmware & network diagnostics              |

> Passwords are stored **bcrypt-hashed**. After login the app forces a reset.

---

## 5  Database Cheatsheet

* **Schema file:** `scripts/schema_v1.1_utf8mb4.sql`
  \*All tables created with `IF NOT EXISTS`, engine = InnoDB, charset = utf8mb4, collation = utf8mb4\_unicode\_ci\`.
* **Demo data:** `scripts/demo_data_2025-07-12.sql` – 3 plans (Arabic, EN, BG), 3 members, tokens, logs.
* **Backup task (daily 07:00):**

```cmd
mysqldump -ugymapp -pS3cureP@ss! --routines --events gym_access_system ^
  > D:\Backups\gym_%DATE:~10,4%-%DATE:~4,2%-%DATE:~7,2%.sql
```

---

## 6  Directory Layout

```
C:\GymApp\
│  README.md               ← this file
├─ installers\              ← MSI & EXE builds
├─ scripts\
│   ├─ schema_v1.1_utf8mb4.sql
│   └─ demo_data_2025-07-12.sql
├─ src\
│   ├─ Gym.Api\             ← ASP.NET Core project
│   ├─ Gym.Client\          ← WPF desktop
│   └─ Gym.Electron\        ← (optional) Electron UI
└─ docs\                    ← architecture diagrams
```

---

## 7  Config Reference (`appsettings.Production.json`)

```jsonc
{
  "ConnectionStrings": {
    "Default": "server=192.168.10.10;uid=gymapp;pwd=S3cureP@ss!;database=gym_access_system;SslMode=none;"
  },
  "SMTP": {
    "Host": "smtp.yourmail.com",
    "Port": 587,
    "User": "noreply@yourgym.com",
    "Pass": "********"
  },
  "TokenPush": {
    "ControllerSubnet": "192.168.10.0/24",
    "RetrySeconds": 30
  },
  "Jwt": {
    "Key": "GENERATE-32-BYTE-SECRET",
    "Issuer": "GymAccess"
  }
}
```

Values can also be provided via environment variables or a `.env` file using the same keys, e.g. `CONNECTIONSTRINGS__DEFAULT` and `JWT__KEY`.

---

## 8  Typical Admin Tasks

| Task                        | Command / UI                             | Schedule  |
| --------------------------- | ---------------------------------------- | --------- |
| **Update NuGet packages**   | `dotnet outdated` → `dotnet add package` | Monthly   |
| **DB physical backup**      | Percona XtraBackup                       | Weekly    |
| **Firmware upgrade**        | Admin Portal → Maintenance → Upload      | Quarterly |
| **Token cleanup**           | Background job `TokenCleaner` (hourly)   | Always on |
| **Disaster-recovery drill** | Restore latest SQL + file dump to VM     | Quarterly |

---

## 9  Troubleshooting

| Symptom                        | Fix                                                                                |
| ------------------------------ | ---------------------------------------------------------------------------------- |
| *Client says “DB unavailable”* | Check `gymapp` user, port 3306 firewall, correct IP in settings.                   |
| *Arabic names appear as ???*   | Ensure Workbench session is `SET NAMES utf8mb4`, reports use fonts like **Cairo**. |
| *Token stuck in PENDING*       | Ping controller IP, verify port (80/8888), look at `controller_token_status`.      |
| *Emails not sending*           | Telnet/`openssl s_client` to SMTP:587; double-check TLS & credentials.             |

---

## 10  License & Credits

Released under the MIT License © 2025 Your Organisation.
Uses open-source components: .NET, MySQL, Fingerprint SDK (vendor), Electron, React.

---

### Happy deploying!

Clone → configure → run, and you’ll have a fully working, Arabic-capable gym access system in under an hour.
