# Gym.Client

This WPF desktop application acts as the front-desk interface for the Gym Access System. It communicates with the REST API and lets an operator manage members, plans, subscriptions and other administrative data.

## Running

1. Install **.NET SDK 8** on Windows.
2. Ensure the API is running (by default on `http://localhost:5000`).
3. From the repository root execute:

   ```bash
   dotnet run --project src/Gym.Client
   ```

   The application launches a window with a simple navigation bar.

## Features

* **Dashboard** – view summary counts, late members and current subscriptions. The "Latest" button shows a toast notification with the status of the last access log entry.
* **Members** – list, add, edit and delete gym members.
* **Plans** – manage subscription plans with price, duration and grace days.
* **Users** – admin management of application users including enable/disable and password changes.
* **Logs** – view the access log of member entries.
* **Reminders** – list subscriptions expiring tomorrow and send manual reminder emails.
* **Alerts** – review emails that have been sent by the system.

Most pages have a *Refresh* button to reload data from the API. Dialog windows are provided for editing members, plans and users.

## Configuration

The API base URL is set in `MainWindow.xaml.cs` when creating the `ApiClient` instance. Adjust it if the API runs on a different address.

---

This client is intentionally lightweight and meant for demonstration. It does not implement authentication or advanced error handling.
