// See https://aka.ms/new-console-template for more information
using Spectre.Console;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using drinksRequestsProject;
using Newtonsoft.Json;


using HttpClient client = new()
{
    BaseAddress = new Uri("https://www.thecocktaildb.com")
};

var response = await client.GetAsync("api/json/v1/1/random.php");

Console.WriteLine(response);



client.DefaultRequestHeaders.Accept.Clear();
//client.DefaultRequestHeaders.Accept.Add(
//    new MediaTypeWithQualityHeaderValue("www.thecocktaildb.com/api/json/v1/1/random.php"));
//client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

await ProccessListDrinks(client);

static async Task ProccessListDrinks(HttpClient client)
{
    var response = await client.GetAsync(
        //"www.thecocktaildb.com/api/json/v1/1/random.php");
        "https://www.thecocktaildb.com/api/json/v1/1/filter.php?c=Ordinary_Drink");

    if (response.IsSuccessStatusCode)
    {
        var drinks = response.Content.ToString();

        var serialize = JsonConvert.DeserializeObject<Root>(drinks);
        Console.WriteLine(serialize);
    }

    
   
}



Console.WriteLine("Welcome to Beach Bar");

// mock data
string[] drinkCategories = ["Alcoholic", "Non-aloholic"];

string[] alcoholicDrinks = ["dark beer", "white beer"];
string[] nonalcoholicDrinks = ["coffee", "tea"];

var categoryChoice = AnsiConsole.Prompt(
    new SelectionPrompt<string>()
        .Title("Choose an category:")
        .AddChoices(drinkCategories)
);

switch (categoryChoice)
{
    case "Alcoholic":
        var detailChoice1 = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Choose a drink")
                .AddChoices(alcoholicDrinks)
        );
        AnsiConsole.MarkupLine($"You have chosen: [purple]{detailChoice1}[/]!");
        break;
    case "Non-aloholic":
        var detailChoice2 = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Choose a drink")
                .AddChoices(nonalcoholicDrinks)
        );
        AnsiConsole.WriteLine($"You have chosen: {detailChoice2}!");
        break;
    default:
        break;
}
