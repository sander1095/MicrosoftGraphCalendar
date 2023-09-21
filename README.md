# How to run the completed project

## Prerequisites

To run the completed project in this folder, you need the following:

- Create app registration in Azure AD (Microsoft Entra ID)
- Store the `tenantId` and `clientId` in `appsettings.json` (or user secrets if you dont want to commit this)
- Create client secret and store it in user secrets: `dotnet user-secrets set "settings:clientSecret" "YOUR_CLIENT_SECRET"`
- Go to `API Permissions`in your app registration and add application permissions: `Calendars.Read` and grant admin consent
- Run the app!