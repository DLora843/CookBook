
using CookBook.Data.Models;
using CookBook.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CookBook.Web.Controllers
{
    public class RecipesController : Controller
    {
        private readonly IRecipeService _recipeService;
        private readonly ICommentService _commentService;
        private readonly IRatingService _ratingService;
        private readonly ICategoryService _categoryService;

        public RecipesController(
            IRecipeService recipeService,
            ICommentService commentService,
            IRatingService ratingService,
            ICategoryService categoryService)
        {
            _recipeService = recipeService;
            _commentService = commentService;
            _ratingService = ratingService;
            _categoryService = categoryService;
        }


        public async Task<IActionResult> Index(string? searchTerm, int? categoryId)
        {
            Console.WriteLine($"Получена категория: {categoryId}");

            var recipes = await _recipeService.FilterAsync(searchTerm, categoryId);

            ViewBag.SearchTerm = searchTerm;
            ViewBag.CategoryId = categoryId;
            ViewBag.Categories = await _categoryService.GetAllAsync();

            return View(recipes);
        }


        public async Task<IActionResult> Details(int id)
        {
            var recipe = await _recipeService.GetByIdAsync(id);

            if (recipe == null)
            {
                return NotFound();
            }

            ViewBag.Comments = await _commentService.GetAllAsync(id);
            ViewBag.Ratings = await _ratingService.GetAllAsync(id);

            return View(recipe);
        }

        private async Task LoadCategoriesAsync()
        {
            ViewBag.Categories = await _categoryService.GetAllAsync();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadCategoriesAsync();
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Recipe recipe)
        {
            ModelState.Remove(nameof(recipe.Category));

            if (!ModelState.IsValid)
            {
                await LoadCategoriesAsync();
                return View(recipe);
            }

            recipe.CreatedOn = DateTime.UtcNow;

            var result = await _recipeService.AddAsync(recipe);

            if (!result)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "Рецептата не беше записана. Опитай отново.");

                await LoadCategoriesAsync();
                return View(recipe);
            }

            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var recipe = await _recipeService.GetByIdAsync(id);

            if (recipe == null)
            {
                return NotFound();
            }

            await LoadCategoriesAsync();

            return View(recipe);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Recipe recipe)
        {
            if (id != recipe.Id)
            {
                return NotFound();
            }

            ModelState.Remove(nameof(recipe.Category));

            if (!ModelState.IsValid)
            {
                await LoadCategoriesAsync();
                return View(recipe);
            }

            var existingRecipe = await _recipeService.GetByIdAsync(id);

            if (existingRecipe == null)
            {
                return NotFound();
            }

            existingRecipe.Title = recipe.Title;
            existingRecipe.Description = recipe.Description;
            existingRecipe.Instructions = recipe.Instructions;
            existingRecipe.PreparationTimeMinutes = recipe.PreparationTimeMinutes;
            existingRecipe.Servings = recipe.Servings;
            existingRecipe.ImageUrl = recipe.ImageUrl;
            existingRecipe.CategoryId = recipe.CategoryId;

            var result = await _recipeService.UpdateAsync(existingRecipe);

            if (!result)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "Промените не бяха записани. Опитай отново.");

                await LoadCategoriesAsync();
                return View(recipe);
            }

            return RedirectToAction(nameof(Index));
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var recipe = await _recipeService.GetByIdAsync(id);

            if (recipe == null)
            {
                return NotFound();
            }

            var result = await _recipeService.DeleteAsync(id);

            if (!result)
            {
                TempData["ErrorMessage"] =
                    "Рецептата не беше изтрита. Опитай отново.";
            }
            else
            {
                TempData["SuccessMessage"] =
                    "Рецептата беше изтрита успешно.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
