using System.ComponentModel.DataAnnotations;

namespace CookBook.Web.ViewModels
{
    public class CommentViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Моля, напишете коментар.")]
        [StringLength(1000, ErrorMessage = "Коментарът не може да бъде по-дълъг от 1000 символа. ")]
        public string Content{ get; set; } = null!;

        public int RecipeId { get; set; }

        public string? UserName { get; set; }

        public DateTime CreatedOn{ get; set; }
    }
}
