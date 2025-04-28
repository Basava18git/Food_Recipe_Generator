using System.ComponentModel.DataAnnotations;

namespace Food_Recipe_Generator.Models
{
    public class FoodContents
    {
        
        public int Id { get; set; }
      
        public string? ingredient { get; set; }
    }
}
