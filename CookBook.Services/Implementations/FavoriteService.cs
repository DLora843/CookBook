using CookBook.Data;
using CookBook.Data.Models;
using CookBook.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CookBook.Services.Implementations
{
    public class FavoriteService : IFavoriteService
    {
        private readonly ApplicationDbContext _context;

        public FavoriteService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Favorite>> GetAllAsync(string userId)
        {
            return await _context.Favorites
                .Where(f => f.UserId == userId)
                .Include(f => f.Recipe)
                .ThenInclude(r => r.Category)
                .OrderByDescending(f => f.AddedOn)
                .ToListAsync();
        }

        public async Task<bool> AddAsync(string userId, int recipeId)
        {
            var recipeExists = await _context.Recipes
                .AnyAsync(r => r.Id == recipeId && !r.IsDeleted);

            if (!recipeExists)
            {
                return false;
            }

            var alreadyExists = await _context.Favorites
                .AnyAsync(f => f.UserId == userId && f.RecipeId == recipeId);

            if (alreadyExists)
            {
                return false;
            }

            var favorite = new Favorite
            {
                UserId = userId,
                RecipeId = recipeId
            };

            await _context.Favorites.AddAsync(favorite);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveAsync(string userId, int recipeId)
        {
            var favorite = await _context.Favorites
                .FirstOrDefaultAsync(f => f.UserId == userId && f.RecipeId == recipeId);
            if (favorite == null)
            {
                return false;
            }

            _context.Favorites.Remove(favorite);

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
