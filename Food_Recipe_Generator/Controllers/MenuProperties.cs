using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
namespace Food_Recipe_Generator.Controllers
{
    public class MenuProperties
    {
        [Required]
        public int Id { get; set; }
        public string? Ingredient_Name { get; set; }

       

    }
}
