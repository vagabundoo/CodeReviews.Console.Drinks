namespace drinksRequestsProject;
// In case request to the public api don't work, this class provides placeholder data so app still works.
public class FetchMockDrinkData
{
    private readonly string _jsonExampleAlcoholic = """
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

        private readonly string _jsonExampleNonAlcoholic = """
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
        
        private readonly string _jsonExampleMissing = """
                                                           {
                                                           "drinks": [
                                                             {
                                                               "strDrink": "MissingDrink",
                                                               "strDrinkThumb": "",
                                                               "idDrink": "000"
                                                             }]
                                                           }
                                                           """;

        public string GetAlcoholicDrinksExamples()
        {
            return _jsonExampleAlcoholic;
        }
        
        public string GetNonAlcoholicDrinksExamples()
        {
            return _jsonExampleNonAlcoholic;
        }

        public string GetDefaultDrinksExamples()
        {
            return _jsonExampleMissing;
        }
        
        public string GetDefaultExample(string category) =>
            category switch
            {
                "Alcoholic" => new FetchMockDrinkData().GetAlcoholicDrinksExamples(),
                "Non_Alcoholic" => new FetchMockDrinkData().GetNonAlcoholicDrinksExamples(),
                _ => new FetchMockDrinkData().GetDefaultDrinksExamples() // fallback for unknown categories
            };
    
}


