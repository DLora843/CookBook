using System.ComponentModel.DataAnnotations;

namespace CookBook.Web.ViewModels
{
    public class FavoriteViewModel
    {
        public int Id { get; set; }
        public int RecipeId{ get; set; }
        public string? RecipeTitle { get; set; } = null!;
        public string? ImageUrl { get; set; }

        public DateTime AddedOn { get; set; }
    }
}
