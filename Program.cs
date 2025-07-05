using drinksRequestsProject.models;
using Spectre.Console;
using DrinkApiService = drinksRequestsProject.DrinkApiService;

using HttpClient client = new();
client.BaseAddress = new Uri("https://www.thecocktaildb.com");



// Get categories
var requestedDrinkCategories = await DrinkApiService.FetchDrinkCategories(client);

// Get drinks by category
var drinksByCategory = await DrinkApiService.FetchDrinksByCategory(requestedDrinkCategories, client);

// Placeholder data to be used in case of no categories / drinks found
if (drinksByCategory.Count == 0)
{
    AnsiConsole.MarkupLine("[red]No drinks found.[/]");
    AnsiConsole.MarkupLine("[red]Providing placeholder data for testing purposes.[/]");
    requestedDrinkCategories = new List<string> {"Alcoholic", "Non_Alcoholic"};
    drinksByCategory = new Dictionary<string, List<Drink>>
    {
        {"Alcoholic", new List<Drink> {new Drink {StrDrink = "MissingDrink", StrDrinkThumb = "", IdDrink = "000"}}},
        {"Non_Alcoholic", new List<Drink> {new Drink {StrDrink = "MissingDrink", StrDrinkThumb = "", IdDrink = "000"}}}
    };
}

// Logic for menu
Console.WriteLine("Welcome to The Beech Bar");

var menuOptions = requestedDrinkCategories.Select(c => c).ToList();
string exitOption = $"[red]Exit application[/]";
menuOptions.Add(exitOption);

while (true)
{
    var categoryChoice = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
            .Title("Choose a drink category, or exit application.")
            .AddChoices(menuOptions)
    );

    if (categoryChoice == exitOption)
    {
        AnsiConsole.MarkupLine($"[green]Goodbye![/]");
        return;
    }

    var chosenDrink = AnsiConsole.Prompt(
        new SelectionPrompt<Drink>()
            .Title("Choose a drink for details on the drink")
            .UseConverter(d => $"{d.StrDrink}")
            .AddChoices(
                drinksByCategory[categoryChoice])
    );
    AnsiConsole.MarkupLine($"You have chosen: [purple]{chosenDrink.StrDrink}[/]");
    AnsiConsole.MarkupLine($"Click here for a picture of the drink: {chosenDrink.StrDrinkThumb}");
}