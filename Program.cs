// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

// <ProgramSnippet>
Console.WriteLine(".NET Graph Tutorial\n");

var settings = Settings.LoadSettings();

// Initialize Graph
InitializeGraph(settings);


int choice = -1;

while (choice != 0)
{
    Console.WriteLine("Please choose one of the following options:");
    Console.WriteLine("0. Exit");
    Console.WriteLine("1. Display access token");
    Console.WriteLine("2. Make a Graph call");

    try
    {
        choice = int.Parse(Console.ReadLine() ?? string.Empty);
    }
    catch (System.FormatException)
    {
        // Set to invalid value
        choice = -1;
    }

    switch(choice)
    {
        case 0:
            // Exit the program
            Console.WriteLine("Goodbye...");
            break;
        case 1:
            // Display access token
            await DisplayAccessTokenAsync();
            break;
        case 2:
            // Run any Graph code
            Console.Write("Enter the user object ID whose calendar events you want to see: ");
            await MakeGraphCallAsync(Console.ReadLine());
            break;
        default:
            Console.WriteLine("Invalid choice! Please try again.");
            break;
    }
}
// </ProgramSnippet>

// <InitializeGraphSnippet>
void InitializeGraph(Settings settings)
{
    GraphHelper.InitializeGraphForAppRegistrationAuth(settings,
        (cancel) =>
        {
            // Display the device code message to
            // the user. This tells them
            // where to go to sign in and provides the
            // code to use.
            return Task.FromResult(0);
        });
}
// </InitializeGraphSnippet>


// <DisplayAccessTokenSnippet>
async Task DisplayAccessTokenAsync()
{
    try
    {
        var appToken = await GraphHelper.GetAppTokenAsync();
        Console.WriteLine($"App token: {appToken}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error getting app access token: {ex.Message}");
    }
}
// </DisplayAccessTokenSnippet>

// <MakeGraphCallSnippet>
async Task MakeGraphCallAsync(string userObjectId)
{
    await GraphHelper.MakeGraphCallAsync(userObjectId);
}
// </MakeGraphCallSnippet>
