// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using Azure.Core;
using Azure.Identity;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using Microsoft.Graph.Me.SendMail;
using Microsoft.Graph.Drives.Item.Items.Item.GetActivitiesByIntervalWithStartDateTimeWithEndDateTimeWithInterval;

class GraphHelper
{
    // <UserAuthConfigSnippet>
    // Settings object
    private static Settings? _settings;
    // User auth token credential
    private static ClientSecretCredential? _clientSecretCredential;
    // Client configured with user authentication
    private static GraphServiceClient? _userClient;

    public static void InitializeGraphForAppRegistrationAuth(Settings settings,
    Func<CancellationToken, Task> cred)
    {
        _settings = settings;

        _clientSecretCredential = new Azure.Identity.ClientSecretCredential(settings.TenantId, settings.ClientId, settings.ClientSecret);

        _userClient = new GraphServiceClient(_clientSecretCredential, settings.GraphUserScopes);
    }
    // </UserAuthConfigSnippet>

    // <GetUserTokenSnippet>
    public static async Task<string> GetAppTokenAsync()
    {
        // Ensure credential isn't null
        _ = _clientSecretCredential ??
            throw new System.NullReferenceException("Graph has not been initialized for user auth");

        // Ensure scopes isn't null
        _ = _settings?.GraphUserScopes ?? throw new System.ArgumentNullException("Argument 'scopes' cannot be null");

        // Request token with given scopes
        var context = new TokenRequestContext(_settings.GraphUserScopes);
        var response = await _clientSecretCredential.GetTokenAsync(context);
        return response.Token;
    }
    // </GetUserTokenSnippet>

    #pragma warning disable CS1998
    // <MakeGraphCallSnippet>
    // This function serves as a playground for testing Graph snippets
    // or other code
    public async static Task MakeGraphCallAsync(string userObjectId)
    {

        var result = await _userClient.Users[userObjectId].Events.GetAsync((requestConfiguration) =>
        {
            requestConfiguration.QueryParameters.Select = new string[]
            { "subject", "body", "bodyPreview", "organizer", "attendees", "start", "end", "location" };
        });

        result.Value.ForEach(x => Console.WriteLine(x.Subject));
    }
    // </MakeGraphCallSnippet>
}
