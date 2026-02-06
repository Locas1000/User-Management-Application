# Task4 - User Management Application

A .NET 8 and React-based user management application with PostgreSQL database.

## Prerequisites

- .NET 8.0 SDK
- Node.js (for the React client)
- PostgreSQL database

## Configuration

This application requires database connection configuration. The connection string is **NOT** included in source control for security reasons.

### Local Development Setup (User Secrets)

For local development, use .NET User Secrets to store your connection string securely:

1. Navigate to the server project directory:
   ```bash
   cd Task4.Server
   ```

2. Set your database connection string using User Secrets:
   ```bash
   dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Database=UserManagementDb;Username=postgres;Password=YOUR_PASSWORD"
   ```

3. Replace `YOUR_PASSWORD` with your actual PostgreSQL password.

### Alternative: appsettings.Development.json

You can also create a local `appsettings.Development.json` file (this file is excluded from git):

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=UserManagementDb;Username=postgres;Password=YOUR_PASSWORD"
  }
}
```

**Note:** This file is ignored by git to prevent accidentally committing credentials.

### Production Deployment

For production deployments, use environment variables:

**Linux/macOS:**
```bash
export ConnectionStrings__DefaultConnection="Host=your-server;Database=UserManagementDb;Username=postgres;Password=YOUR_PASSWORD"
```

**Windows (PowerShell):**
```powershell
$env:ConnectionStrings__DefaultConnection="Host=your-server;Database=UserManagementDb;Username=postgres;Password=YOUR_PASSWORD"
```

**Docker:**
```yaml
environment:
  - ConnectionStrings__DefaultConnection=Host=your-server;Database=UserManagementDb;Username=postgres;Password=YOUR_PASSWORD
```

**Azure App Service / Cloud Platforms:**
Configure the connection string in the application settings/configuration section of your hosting platform.

## Running the Application

### Server (API)

1. Configure your connection string (see Configuration section above)
2. Run database migrations:
   ```bash
   cd Task4.Server
   dotnet ef database update
   ```
3. Start the server:
   ```bash
   dotnet run
   ```

The API will be available at `https://localhost:7221` (or as configured).

### Client (React App)

1. Navigate to the client directory:
   ```bash
   cd task4.client
   ```
2. Install dependencies:
   ```bash
   npm install
   ```
3. Start the development server:
   ```bash
   npm run dev
   ```

The client will be available at `http://localhost:5174` (or as configured).

## Security Notes

- **Never commit passwords or connection strings to source control**
- Use User Secrets for local development
- Use environment variables or secure configuration providers for production
- The `.gitignore` is configured to exclude sensitive configuration files

## Additional Configuration

The application also requires the following configuration in `appsettings.json`:

```json
{
  "AppSettings": {
    "ClientUrl": "http://localhost:5174"
  }
}
```

Update the `ClientUrl` if your React app runs on a different port.
