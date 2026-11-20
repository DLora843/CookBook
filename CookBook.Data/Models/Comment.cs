using System.ComponentModel.DataAnnotations;

namespace CookBook.Data.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(1000)]

        public string Content { get; set; } = null!;

        public DateTime CreatedOn {  get; set; } = DateTime.UtcNow;

        public bool IsDeleted { get; set; } = false;

        public int RecipeId { get; set; }

        public Recipe Recipe { get; set; } = null!;

        public string UserId { get; set; } = null!;
    }
}
