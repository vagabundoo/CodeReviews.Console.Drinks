using System.Text.Json;
using System.Text.Json.Serialization;
using Spectre.Console;

var listOfDrinksAlcoholic = new Root();
var listOfDrinksNonAlcoholic = new Root();

string jsonExample = """
                     {
                         "drinks": [
                         {
                             "strDrink": "Afterglow",
                             "strDrinkThumb": "https://www.thecocktaildb.com/images/media/drink/vuquyv1468876052.jpg",
                             "idDrink": "12560"
                         },
                         {
                             "strDrink": "Alice Cocktail",
                             "strDrinkThumb": "https://www.thecocktaildb.com/images/media/drink/qyqtpv1468876144.jpg",
                             "idDrink": "12562"
                         }]
                     }
                     """;

using HttpClient client = new()
{
    BaseAddress = new Uri("https://www.thecocktaildb.com")
};
var responseAlcoholic = await client.GetAsync(
    //"www.thecocktaildb.com/api/json/v1/1/random.php");
    "https://www.thecocktaildb.com/api/json/v1/1/filter.php?a=Non_Alcoholic");

if (responseAlcoholic.IsSuccessStatusCode)
{
    var responseContentAlcoholic = responseAlcoholic.Content.ReadAsStringAsync().Result;

    listOfDrinksAlcoholic = JsonSerializer.Deserialize<Root>(responseContentAlcoholic);

        //var drink1 = listOfAlcoholicDrinks .drinks[0];
        //var drink2 = listOfAlcoholicDrinks .drinks[1];

        //Console.WriteLine(drink1.idDrink);
        //Console.WriteLine(drink1.strDrink);
        //Console.WriteLine(drink1.strDrinkThumb);
        //Console.WriteLine(drink2); 
    
}

var responseNonAlcoholic = await client.GetAsync(
    //"www.thecocktaildb.com/api/json/v1/1/random.php");
    "https://www.thecocktaildb.com/api/json/v1/1/filter.php?a=Non_Alcoholic");

if (responseNonAlcoholic.IsSuccessStatusCode)
{
    var responseContentNonAlcoholic = responseNonAlcoholic.Content.ReadAsStringAsync().Result;

    listOfDrinksNonAlcoholic = JsonSerializer.Deserialize<Root>(responseContentNonAlcoholic);
}

var alcoholicDrinks = listOfDrinksAlcoholic.drinks;
var nonAlcoholicDrinks = listOfDrinksNonAlcoholic.drinks;

// Logic for menu
Console.WriteLine("Welcome to The Beech Bar");

string[] drinkCategories = ["Alcoholic", "Non-Alcoholic"];

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
        AnsiConsole.MarkupLine($"You have chosen: [purple]{chosenDrink2.strDrink}[/]");
        AnsiConsole.MarkupLine($"Click here for a picture of the drink: {chosenDrink2.strDrinkThumb}");
        break;
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
