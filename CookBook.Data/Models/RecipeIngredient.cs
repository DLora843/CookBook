using System.ComponentModel.DataAnnotations;

namespace CookBook.Data.Models
{
    public class RecipeIngredient
    {
        public int RecipeId { get; set; }

        public Recipe Recipe { get; set; } = null!;

        public int IngredientId { get; set; }

        public Ingredient Ingredient { get; set; } = null!;

        [Range(0.01, 100000)]
        public decimal Quantity { get; set; }

        [Required]
        [MaxLength(30)]
        public string Unit { get; set; } = null!;
    }
}
