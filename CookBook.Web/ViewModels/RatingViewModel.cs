using System.ComponentModel.DataAnnotations;

namespace CookBook.Web.ViewModels
{
    public class RatingViewModel
    {
        public int Id { get; set; }

        [Range(1, 5, ErrorMessage = "Оценката трябва да бъде междду 1 и 5.")]
        public int Value { get; set; }
        public int RecipeId { get; set; }

        public string? UserName { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
