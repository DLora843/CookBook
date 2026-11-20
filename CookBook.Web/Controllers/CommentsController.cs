using CookBook.Data.Models;
using CookBook.Services.Interfaces;
using CookBook.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CookBook.Web.Controllers
{
    [Authorize]
    public class CommentsController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public async Task<IActionResult> Index(int recipeId)
        {
            var comments = await _commentService.GetAllAsync(recipeId);

            var model = comments.Select(c => new CommentViewModel
            {
                Id = c.Id,
                Content = c.Content,
                RecipeId = c.RecipeId,
                UserName = c.UserId,
                CreatedOn = c.CreatedOn
            });
            return View(model);
        }
        [Authorize]
        [HttpGet]
        public IActionResult Create(int recipeId)
        {
            return View(new CommentViewModel
            {
                RecipeId = recipeId
            });
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CommentViewModel model)
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

            var comment = new Comment
            {
                Content = model.Content,
                RecipeId = model.RecipeId,
                UserId = userId
            };

            await _commentService.AddAsync(comment);

            return RedirectToAction("Details", "Recipes",
                new { id = model.RecipeId });
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, int recipeId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Challenge();
            }

            await _commentService.DeleteAsync(id, userId);

            return RedirectToAction("Details", "Recipes",
                new { id = recipeId });
        }
    }
}
