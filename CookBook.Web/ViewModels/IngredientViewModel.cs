using System.ComponentModel.DataAnnotations;

namespace CookBook.Web.ViewModels
{
    public class IngredientViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Моля, въведете име на продукта.")]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        [StringLength(50)]
        public string? Unit { get; set; }
        public decimal Quantity { get; set; }
    }
}
