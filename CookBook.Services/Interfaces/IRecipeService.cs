
using CookBook.Data.Models;

namespace CookBook.Services.Interfaces
{
    public interface IRecipeService
    {
        Task<IEnumerable<Recipe>> GetAllAsync();

        Task<Recipe?> GetByIdAsync(int id);

        Task<IEnumerable<Recipe>> SearchAsync(string searchTerm);

        Task<IEnumerable<Recipe>> FilterAsync(string? searchTerm, int? categoryId);

        Task<bool> AddAsync(Recipe recipe);

        Task<bool> UpdateAsync(Recipe recipe);

        Task<bool> DeleteAsync(int id);
    }
}
