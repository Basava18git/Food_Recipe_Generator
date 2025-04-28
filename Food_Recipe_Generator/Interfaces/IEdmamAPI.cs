using Food_Recipe_Generator.Models;

namespace Food_Recipe_Generator.Interfaces
{
    public interface IEdmamAPI 
    {
        public void Method1();

        public Task<FoodContents?> GetRecipe(string ingredient);
    }
}
