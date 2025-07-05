using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using drinksRequestsProject.models;

namespace drinksRequestsProject;

public static class DrinkApiService
{
    public static async Task<List<string>> FetchDrinkCategories(HttpClient httpClient)
    {
        string endpoint = $"api/json/v1/1/list.php?c=list";
        List<string> list = new List<string>();
        try
        {
            var response = await httpClient.GetAsync(endpoint);
            if (response.IsSuccessStatusCode)
            {
                string responseContent = response.Content.ReadAsStringAsync().Result;
                DrinkCategoryRoot? listOfCategories = JsonSerializer.Deserialize<DrinkCategoryRoot>(responseContent);
                if (listOfCategories != null)
                {
                    list.AddRange(listOfCategories.DrinkCategories.Select(c => c.StrCategory));
                }
            }
            else
                Console.WriteLine($"One API call failed, with status code {response.StatusCode}.");
        }
        catch (JsonException e)
        {
            Console.WriteLine($"Json Error: {e.Message}");
        }

        return list;
    }

    public static async Task<Dictionary<string, List<Drink>>> FetchDrinksByCategory(List<string> drinkCategories,
        HttpClient client1)
    {
        Dictionary<string, List<Drink>> dictionary = new Dictionary<string, List<Drink>>();
        foreach (var category in drinkCategories)
        {
            string endpoint = $"api/json/v1/1/filter.php?c={category}";
            try
            {
                var response = await client1.GetAsync(endpoint);
                if (response.IsSuccessStatusCode)
                {
                    string responseContent = response.Content.ReadAsStringAsync().Result;
                    Root? listOfDrinks = JsonSerializer.Deserialize<Root>(responseContent);
                    if (listOfDrinks != null)
                    {
                        dictionary[category] = listOfDrinks.Drinks;
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

        return dictionary;
    }
}