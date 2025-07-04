using System.Text.Json;
using System.Text.Json.Serialization;

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
var response = await client.GetAsync(
    //"www.thecocktaildb.com/api/json/v1/1/random.php");
    "https://www.thecocktaildb.com/api/json/v1/1/filter.php?a=Non_Alcoholic");

if (response.IsSuccessStatusCode)
{
    var responseContent = response.Content.ReadAsStringAsync().Result;

    if (responseContent != null)
    {
        Root listOfDrinks = JsonSerializer.Deserialize<Root>(responseContent);

        var drink1 = listOfDrinks.drinks[0];
        var drink2 = listOfDrinks.drinks[1];

        Console.WriteLine(drink1.idDrink);
        Console.WriteLine(drink1.strDrink);
        Console.WriteLine(drink1.strDrinkThumb);
        Console.WriteLine(drink2); 
        
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
