using System.Text.Json.Serialization;

namespace drinksRequestsProject;

// Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
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

