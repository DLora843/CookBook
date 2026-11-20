
using CookBook.Data;
using CookBook.Data.Models;
using CookBook.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CookBook.Services.Implementations
{
    public class RecipeService : IRecipeService
    {
        private readonly ApplicationDbContext _context;

        public RecipeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Recipe>> GetAllAsync()
        {
            return await _context.Recipes
                .Where(r => !r.IsDeleted)
                .Include(r => r.Category)
                .OrderByDescending(r => r.CreatedOn)
                .ToListAsync();
        }

        public async Task<Recipe?> GetByIdAsync(int id)
        {
            return await _context.Recipes
                .Where(r => !r.IsDeleted && r.Id == id)
                .Include(r => r.Category)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Recipe>> SearchAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return await GetAllAsync();
            }

            searchTerm = searchTerm.Trim();

            return await _context.Recipes
                .Where(r => !r.IsDeleted &&
                            r.Title.Contains(searchTerm))
                .Include(r => r.Category)
                .OrderByDescending(r => r.CreatedOn)
                .ToListAsync();
        }

        public async Task<IEnumerable<Recipe>> FilterAsync(
            string? searchTerm,
            int? categoryId)
        {
            var query = _context.Recipes
                .Where(r => !r.IsDeleted)
                .Include(r => r.Category)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                searchTerm = searchTerm.Trim();

                query = query.Where(r =>
                    r.Title.Contains(searchTerm));
            }

            if (categoryId.HasValue && categoryId.Value > 0)
            {
                query = query.Where(r =>
                    r.CategoryId == categoryId.Value);
            }

            return await query
                .OrderByDescending(r => r.CreatedOn)
                .ToListAsync();
        }

        public async Task<bool> AddAsync(Recipe recipe)
        {
            await _context.Recipes.AddAsync(recipe);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Recipe recipe)
        {
            _context.Recipes.Update(recipe);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var recipe = await _context.Recipes
                .FirstOrDefaultAsync(r => r.Id == id && !r.IsDeleted);

            if (recipe == null)
            {
                return false;
            }

            recipe.IsDeleted = true;

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
