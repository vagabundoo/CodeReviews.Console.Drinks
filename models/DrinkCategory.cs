using System.Text.Json.Serialization;

namespace drinksRequestsProject;

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