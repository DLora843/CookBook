using System.ComponentModel.DataAnnotations;

namespace CookBook.Web.ViewModels
{
    public class RecipeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Моля, въведете име на рецептата.")]
        [StringLength(100)]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Моля, въведете описание .")]
        [StringLength(1000)]

        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "Моля, въведете инструкции за приготвне. ")]
        public string Instructions { get; set; } = null!;

        [Range(1, 600)]
        public int PreparationTimeMinutes { get; set; }

        [Range(1, 50)]
        public int Servings { get; set; }

        public int CategoryId { get; set; }

        public string? CategoryName { get; set; }

        public string? ImageUrl { get; set; }
    }
}
