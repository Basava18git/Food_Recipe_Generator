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
                var RecipeID = _context.FoodIngred.SingleOrDefault(expenseId => expenseId.Id == id);
                return View(RecipeID);
            }
            return View();
        }
        public async Task<IActionResult> ShowFinal(FoodContents model)
        {

            var fetchedrecipe=await ClickForURL(model);
            var foodcontents = new FoodContents()
            {
                ingredient = fetchedrecipe,
                Id = model.Id
            };


            //var ingredients = _context.FoodIngred.ToList();
            //var x = ingredients;
            //_context.FoodIngred.AddRange(x);
            //_context.SaveChanges();
            //ingredients.Clear();
            return RedirectToAction("FinalRecipeName",foodcontents);


            //return View(y);

        }

        public IActionResult Delete_id(int? id)
        {
            var recipeId = _context.FoodIngred.SingleOrDefault(recipeId => recipeId.Id == id);
            if (recipeId == null)
            {
                return NotFound();
            }
            else
            {
                _context.FoodIngred.Remove(recipeId);
                _context.SaveChanges();
                return RedirectToAction("ShowFinal");
            }
        }

        public void AddIngredient(FoodContents model)
        {
            if (model.Id == 0)
            {
                

                _context.FoodIngred.Add(model);
               
            }
            else
            {
               
                _context.FoodIngred.Update(model);
            }

            _context.SaveChanges();
           // return RedirectToAction("RecipePage");
        }
        public async Task<string> ClickForURL(FoodContents model)
        {
            
            Food_Recipe_Generator.Models.edamamAPI edamamAPI = new Food_Recipe_Generator.Models.edamamAPI();
            var x=await edamamAPI.GetRecipe(model.ingredient);
            if (x == null)
            {
            }
            return x.ingredient.ToString();
        }
        public IActionResult GenerateRecipe()
        {

            var latest = _context.FoodIngred.OrderByDescending(r => r.Id).FirstOrDefault();

            if (latest == null)
            {
                return Content("No ingredients found in database.");
            }

            var input = latest.ingredient?.ToLower();

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
