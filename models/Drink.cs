using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace drinksRequestsProject.models;
public class Drink
{
    [JsonPropertyName("strDrink")]
    public required string StrDrink { get; set; }

    [JsonPropertyName("strDrinkThumb")]
    public string? StrDrinkThumb { get; set; }

    [JsonPropertyName("idDrink")]
    public string? IdDrink { get; set; }
}

public class Root
{
    [JsonPropertyName("drinks")]
    public required List<Drink> Drinks { get; set; }
}