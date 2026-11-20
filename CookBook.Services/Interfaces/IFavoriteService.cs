using CookBook.Data.Models;

namespace CookBook.Services.Interfaces
{
    public interface IFavoriteService
    {
        Task<IEnumerable<Favorite>> GetAllAsync(string userId);

        Task<bool> AddAsync(string userId, int recipeId);

        Task<bool> RemoveAsync(string userId, int recipeId);
    }
}
