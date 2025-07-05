using System.Text.Json;
using System.Text.Json.Serialization;
using drinksRequestsProject;
using drinksRequestsProject.models;
using Spectre.Console;

using HttpClient client = new();
client.BaseAddress = new Uri("https://www.thecocktaildb.com");

var requestedDrinkCategories = new List<string> {};

// Get categories
{
    string endpoint = $"api/json/v1/1/list.php?c=list";
    try
    {
        var response = await client.GetAsync(endpoint);

        if (response.IsSuccessStatusCode)
        {
            string responseContent = response.Content.ReadAsStringAsync().Result;
            
            DrinkCategoryRoot? listOfCategories = JsonSerializer.Deserialize<DrinkCategoryRoot>(responseContent);
            if (listOfCategories != null)
            {
                Console.WriteLine(listOfCategories.DrinkCategories[0].StrCategory);
                requestedDrinkCategories.AddRange(listOfCategories.DrinkCategories.Select(c => c.StrCategory));
            }
        }
        else
            Console.WriteLine($"One API call failed, with status code {response.StatusCode}.");
    }
    catch (JsonException e)
    {
        Console.WriteLine($"Json Error: {e.Message}");
    }
}

// Get drinks for each category
Dictionary<string, List<Drink>> drinksByCategory = new Dictionary<string, List<Drink>>();

foreach (var category in requestedDrinkCategories)
{
    string endpoint = $"api/json/v1/1/filter.php?c={category}";
    try
    
    {
        var response = await client.GetAsync(endpoint);

        if (response.IsSuccessStatusCode)
        {
            string responseContent = response.Content.ReadAsStringAsync().Result;
            
            Root? listOfDrinks = JsonSerializer.Deserialize<Root>(responseContent);
            if (listOfDrinks != null)
            {
                drinksByCategory[category] = listOfDrinks.Drinks;
            }
        }
        else
            Console.WriteLine($"One API call failed, with status code {response.StatusCode}.");
        
    }
    catch (JsonException e)
    {
        Console.WriteLine($"One category failed to load: {category}. Error: {e.Message}");
    }
}

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


