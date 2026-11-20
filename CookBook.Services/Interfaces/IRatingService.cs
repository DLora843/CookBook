using CookBook.Data.Models;

namespace CookBook.Services.Interfaces
{
    public interface IRatingService
    {
        Task<IEnumerable<Rating>> GetAllAsync(int recipeId);

        Task<Rating?> GetUserAsync(int recipeId, string userId);

        Task<bool> AddOrUpdateAsync(int recipeId, string userId, int value);

        Task<bool> DeleteAsync(int recipeId, string userId);
    }
}
