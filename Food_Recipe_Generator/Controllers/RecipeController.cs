using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Food_Recipe_Generator.Models;

namespace Food_Recipe_Generator.Controllers
{
    public class RecipeController : Controller
    {
        
        private readonly ILogger<RecipeController> _logger;
        private readonly RecipeDBContext _context;

        public RecipeController(ILogger<RecipeController> logger, RecipeDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult HealthyFood()
        {
            return View();
        }
        public IActionResult RecipePage(int? id)
        {

            if (id != null)

            {
                var RecipeID = _context.recipes.SingleOrDefault(expenseId => expenseId.Id == id);
                return View(RecipeID);
            }
            return View();
        }
        public IActionResult ShowFinal()
        {
            var ingredients = _context.recipes.ToList();
            var x = ingredients;
            _context.recipes.AddRange(x);
            _context.SaveChanges();
            ingredients.Clear();


            return View(x);
           
        }

        public IActionResult Delete_id(int? id)
        {
            var recipeId = _context.recipes.SingleOrDefault(recipeId => recipeId.Id == id);
            if (recipeId == null)
            {
                return NotFound();
            }
            else
            {
                _context.recipes.Remove(recipeId);
                _context.SaveChanges();
                return RedirectToAction("ShowFinal");
            }
        }

        public IActionResult AddIngredient(MenuProperties model)
        {
            if (model.Id == 0)
            {
                

                _context.recipes.Add(model);
               
            }
            else
            {
               
                _context.recipes.Update(model);
            }

            _context.SaveChanges();
            return RedirectToAction("RecipePage");
        }
        public IActionResult GenerateRecipe()
        {
            var latest = _context.recipes.OrderByDescending(r => r.Id).FirstOrDefault();

            if (latest == null)
            {
                return Content("No ingredients found in database.");
            }

            var input = latest.Ingredient_Name?.ToLower();

            if (input == "egg")
            {
                return Content("Omelette");
            }
            else
            {
                return Content($"Salad (Input was: '{input}')");
            }
            
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
