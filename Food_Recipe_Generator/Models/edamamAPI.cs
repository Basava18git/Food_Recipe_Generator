
using Food_Recipe_Generator.Interfaces;
using Newtonsoft.Json.Linq;

namespace Food_Recipe_Generator.Models
{
    public class edamamAPI : IEdmamAPI
    {
        public async Task<FoodContents?> GetRecipe(string ingredient)
        {
            string YOUR_APP_KEY = "dc68f4533535c29db8694a8e102383d0";
            string YOUR_APP_ID = "4152f6ae";
            string url = $"https://api.edamam.com/api/recipes/v2?type=public&q={ingredient}&app_id={YOUR_APP_ID}&app_key={YOUR_APP_KEY}\r\n";
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Edamam-Account-User", "admin");
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(data))
                    {
                        JObject jdata = JObject.Parse(data);
                        var hits = jdata["hits"] as JArray;

                        if (hits != null && hits.Count > 0)
                        {
                            var recipe = hits[0]?["recipe"];
                            if (recipe != null)
                            {
                                var label = recipe["label"]?.ToString();
                                if (!string.IsNullOrEmpty(label))
                                {
                                    return new FoodContents
                                    {
                                        ingredient = label
                                    };
                                }
                            }
                        }
                    }
                }

                return null;
            }
        }

        public void Method1()
        {
            throw new NotImplementedException();
        }
    }
}
