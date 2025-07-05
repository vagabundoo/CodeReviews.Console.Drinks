using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace drinksRequestsProject.models;

public class DrinkCategory
{
    [JsonPropertyName("strCategory")]
    public required string StrCategory { get; set; }
}

public class DrinkCategoryRoot
{
    [JsonPropertyName("drinks")]
    public required List<DrinkCategory> DrinkCategories { get; set; }
    
}