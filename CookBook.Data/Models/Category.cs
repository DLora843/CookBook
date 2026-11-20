using System.ComponentModel.DataAnnotations;

namespace CookBook.Data.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        [MaxLength(200)]
        public string? Description { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<Recipe> Recipes { get; set; }
        = new HashSet<Recipe>();
    }
}