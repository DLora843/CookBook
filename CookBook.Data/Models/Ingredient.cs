using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBook.Data.Models
{
    public class Ingredient
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [MaxLength(50)]
        public string? Unit { get; set; }

        public bool IsDeleted { get; set; } = false;

        public ICollection<RecipeIngredient> RecipeIngredients { get; set; }
        = new HashSet<RecipeIngredient>();
    }
}
