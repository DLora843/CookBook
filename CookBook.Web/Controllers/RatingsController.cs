using System.Security.Claims;
using CookBook.Services.Interfaces;
using CookBook.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CookBook.Web.Controllers
{
    public class RatingsController : Controller
    {
        private readonly IRatingService _ratingService;

        public RatingsController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }
        public async Task<IActionResult> Index(int recipeId)
        {
            var ratings = await _ratingService.GetAllAsync(recipeId);

            var model = ratings.Select(r => new RatingViewModel
            {
                Id = r.Id,
                Value = r.Value,
                RecipeId = recipeId,
                UserName = r.UserId,
                CreatedOn = r.CreatedOn
            });

            return View(model);
        }
        [Authorize]
        [HttpGet]
        public IActionResult Create(int recipeId)
        {
            return View(new RatingViewModel
            {
                RecipeId = recipeId
            });
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RatingViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);

            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Challenge();
            }

            await _ratingService.AddOrUpdateAsync(
               model.RecipeId,
               userId,
               model.Value);

            return RedirectToAction("Details", "Recipes",
                new { id = model.RecipeId });
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int recipeId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Challenge();
            }

            await _ratingService.DeleteAsync(recipeId, userId);

            return RedirectToAction("Details", "Recipes",
                new { id = recipeId });
        }
    }
}
