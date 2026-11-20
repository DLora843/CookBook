using System.ComponentModel.DataAnnotations;

namespace CookBook.Web.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Моля, въведете име на категорита.")]
        [StringLength(50)]
        public string Name { get; set; } = null!;

        [StringLength(200)]

        public string? Description { get; set; }

        public int RecipesCount { get; set; }
    }
}
