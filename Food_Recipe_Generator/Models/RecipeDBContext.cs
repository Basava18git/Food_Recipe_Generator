using Food_Recipe_Generator.Controllers;
using Microsoft.EntityFrameworkCore;

namespace Food_Recipe_Generator.Models
{
    public class RecipeDBContext:DbContext
    {
        public DbSet<MenuProperties> recipes { get; set; }

       public RecipeDBContext(DbContextOptions<RecipeDBContext> recipes)
            : base(recipes)
        {
        }
  
    }
}
