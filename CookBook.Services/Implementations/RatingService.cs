using CookBook.Data;
using CookBook.Data.Models;
using CookBook.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CookBook.Services.Implementations
{
    public class RatingService : IRatingService
    {
        private readonly ApplicationDbContext _context;

        public RatingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Rating>> GetAllAsync(int recipeId)
        {
            return await _context.Ratings
                .Where(r => r.RecipeId == recipeId)
                .OrderByDescending(r => r.CreatedOn)
                .ToListAsync();
        }

        public async Task<Rating?> GetUserAsync(int recipeId, string userId)
        {
            return await _context.Ratings
                .FirstOrDefaultAsync(r =>
                r.RecipeId == recipeId && r.UserId == userId);
        }

        public async Task<bool> AddOrUpdateAsync(int recipeId, string userId, int value)
        {
            if (value < 1 || value > 5)
            {
                return false;
            }

            var recipeExists = await _context.Recipes
                .AnyAsync(r => r.Id == recipeId && !r.IsDeleted);

            if (!recipeExists)
            {
                return false;
            }

            var rating = await _context.Ratings
                .FirstOrDefaultAsync(r => r.RecipeId == recipeId && r.UserId == userId);

            if (rating == null)
            {
                rating = new Rating
                {
                    RecipeId = recipeId,
                    UserId = userId,
                    Value = value
                };

                await _context.Ratings.AddAsync(rating);
            }
            else
            {
                rating.Value = value;
                rating.CreatedOn = DateTime.UtcNow;
            }

            return await _context.SaveChangesAsync() > 0;
        }
    public async Task<bool> DeleteAsync(int recipeId, string userId)
        {
            var rating = await _context.Ratings
                .FirstOrDefaultAsync(r =>
                    r.RecipeId == recipeId && r.UserId == userId);

            if (rating == null)
            {
                return false;
            }

            _context.Ratings.Remove(rating);

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
