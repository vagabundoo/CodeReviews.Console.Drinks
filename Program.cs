using System.Text.Json;
using System.Text.Json.Serialization;
using drinksRequestsProject;
using Spectre.Console;

using HttpClient client = new()
{
    BaseAddress = new Uri("https://www.thecocktaildb.com")
};

// We make two requests to the api, one for each drink category, and save the results for use in the rest of the program.
var responseAlcoholic = await client.GetAsync(
    //"www.thecocktaildb.com/api/json/v1/1/random.php");
    "api/json/v1/1/filter.php?a=Alcoholic");

string responseContentAlcoholic;
if (responseAlcoholic.IsSuccessStatusCode)
    responseContentAlcoholic = responseAlcoholic.Content.ReadAsStringAsync().Result;
else
    responseContentAlcoholic = new DrinkExamples().GetAlcoholicDrinksExamples();

var listOfDrinksAlcoholic = JsonSerializer.Deserialize<Root>(responseContentAlcoholic);
var alcoholicDrinks = listOfDrinksAlcoholic!.drinks;

string responseContentNonAlcoholic;
var responseNonAlcoholic = await client.GetAsync(
    //"www.thecocktaildb.com/api/json/v1/1/random.php");
    "api/json/v1/1/filter.php?a=Non_Alcoholic");

if (responseNonAlcoholic.IsSuccessStatusCode)
    responseContentNonAlcoholic = responseNonAlcoholic.Content.ReadAsStringAsync().Result;
else
    responseContentNonAlcoholic = new DrinkExamples().GetNonAlcoholicDrinksExamples();

var listOfDrinksNonAlcoholic = JsonSerializer.Deserialize<Root>(responseContentNonAlcoholic);
var nonAlcoholicDrinks = listOfDrinksNonAlcoholic!.drinks;

// Logic for menu
string[] drinkCategories = ["Alcoholic", "Non-Alcoholic", "Exit application"];
Console.Clear();
Console.WriteLine("Welcome to The Beech Bar");

while (true)
{
    var categoryChoice = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
            .Title("Choose a drink category.")
            .AddChoices(drinkCategories)
    );

    switch (categoryChoice)
    {
        case "Alcoholic":
            var chosenDrink1 = AnsiConsole.Prompt(
                new SelectionPrompt<Drink>()
                    .Title("Choose a drink")
                    .UseConverter(d => $"{d.strDrink}")
                    .AddChoices(
                        alcoholicDrinks)
            );
            AnsiConsole.MarkupLine($"You have chosen: [purple]{chosenDrink1.strDrink}[/]");
            AnsiConsole.MarkupLine($"Click here for a picture of the drink: {chosenDrink1.strDrinkThumb}");
            break;
        case "Non-Alcoholic":
            var chosenDrink2 = AnsiConsole.Prompt(
                new SelectionPrompt<Drink>()
                    .Title("Choose a drink")
                    .UseConverter(d => $"{d.strDrink}")
                    .AddChoices(
                        nonAlcoholicDrinks)
            );
            AnsiConsole.MarkupLine($"You have chosen: [blue]{chosenDrink2.strDrink}[/]");
            AnsiConsole.MarkupLine($"Click here for a picture of the drink: {chosenDrink2.strDrinkThumb}");
            break;
        case "Exit application":
            AnsiConsole.MarkupLine($"[green]Goodbye![/]");
            return;
    }
}



public class Drink
{
    [JsonPropertyName("strDrink")]
    public string strDrink { get; set; }

    [JsonPropertyName("strDrinkThumb")]
    public string strDrinkThumb { get; set; }

    [JsonPropertyName("idDrink")]
    public string idDrink { get; set; }
}

public class Root
{
    [JsonPropertyName("drinks")]
    public List<Drink> drinks { get; set; }
}
