# Gym.Api

This project contains the REST API for the Gym Access System. It is built with ASP.NET Core 8 and Entity Framework Core.

## Running the API locally

1. Install **.NET SDK 8** and make sure a MySQL 8 server is accessible.
2. Copy `.env.example` to `.env` and adjust the connection string and JWT settings. Optionally set `INIT_DEMO_DATA=true` to load sample plans and users on first run.
3. From the repository root execute:

   ```bash
   dotnet run --project src/Gym.Api
   ```
   The API creates the database tables automatically if they do not exist and starts listening on `http://localhost:5000` by default.
4. Navigate to `http://localhost:5000/swagger` in your browser to explore the endpoints and try them interactively.

## Basic usage

### Authenticate
Send a `POST /api/auth/login` request with JSON `{ "username": "admin", "password": "ChangeMe!" }` (use your own credentials). The response contains a JWT access token to include in the `Authorization: Bearer` header for other requests.

### Members
- `GET /api/members` – list all members.
- `POST /api/members` – create a new member (requires `DATA_ENTRY` or `ADMIN` role).
- `PUT /api/members/{id}` – update a member.
- `DELETE /api/members/{id}` – remove a member (ADMIN only).

### Plans
- `GET /api/plans` – list subscription plans.
- `POST /api/plans` – create a plan (ADMIN only).
- `PUT /api/plans/{id}` – update a plan.
- `DELETE /api/plans/{id}` – delete a plan.

Other tables such as `Subscriptions`, `Payments`, `AccessTokens` and logs are exposed through similar endpoints that follow the same pattern.

## Backups

If `BACKUP__DIR` and related keys are set in `.env`, the API runs a background service that dumps the database every twelve hours by default and keeps only the fifty most recent files in the backup directory.
