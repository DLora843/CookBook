using CookBook.Data;
using CookBook.Data.Models;
using CookBook.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CookBook.Services.Implementations
{
    public class CommentService : ICommentService
    {
        private readonly ApplicationDbContext _context;

        public CommentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Comment>> GetAllAsync(int recipeId)
        {
            return await _context.Comments
                .Where(c => c.RecipeId == recipeId && !c.IsDeleted)
                .Include(c => c.Recipe)
                .OrderByDescending(c => c.CreatedOn)
                .ToListAsync();
        }

        public async Task<bool> AddAsync(Comment comment)
        {
            var recipeExists = await _context.Recipes
                .AnyAsync(r => r.Id == comment.RecipeId && !r.IsDeleted);

            if (!recipeExists)
            {
                return false;
            }

            await _context.Comments.AddAsync(comment);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id, string userId)
        {
            var comment = await _context.Comments
                .FirstOrDefaultAsync(c => 
                c.Id == id &&
                c.UserId == userId &&
                !c.IsDeleted);

            if (comment == null)
            {
                return false;
            }

            comment.IsDeleted = true;

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
