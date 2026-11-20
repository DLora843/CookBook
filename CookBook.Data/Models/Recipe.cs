using System.ComponentModel.DataAnnotations;

namespace CookBook.Data.Models
{
    public class Recipe
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; } = null!;

        [Required]
        public string Instructions { get; set; } = null!;

        [Range(1, 600)]
        public int PreparationTimeMinutes { get; set; }

        [Range(1, 50)]
        public int Servings { get; set; }

        [MaxLength(500)]
        public string? ImageUrl { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public bool IsDeleted { get; set; } = false;

        public int CategoryId { get; set; }

        public Category Category { get; set; } = null!;

        public ICollection<Comment> Comments { get; set; }
        = new HashSet<Comment>();

        public ICollection<Rating> Ratings { get; set; }
        = new HashSet<Rating>();

        public ICollection<Favorite> Favorites { get; set; }
        = new HashSet<Favorite>();
    }
}
