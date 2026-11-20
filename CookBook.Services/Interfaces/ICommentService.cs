using CookBook.Data.Models;

namespace CookBook.Services.Interfaces
{
    public interface ICommentService
    {
        Task<IEnumerable<Comment>> GetAllAsync(int recipeId);

        Task<bool> AddAsync(Comment comment);

        Task<bool> DeleteAsync(int id, string userId);
    }
}
