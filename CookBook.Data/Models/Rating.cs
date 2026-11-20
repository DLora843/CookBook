using System.ComponentModel.DataAnnotations;

namespace CookBook.Data.Models
{
    public class Rating
    {
        public int Id { get; set; }

        [Range(1, 5)]
        public int Value { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public int RecipeId { get; set; }

        public Recipe Recipe { get; set; } = null!;

        public string UserId { get; set; } = null!;
    }
}
