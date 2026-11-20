namespace CookBook.Data.Models
{
    public class Favorite
    {   
        public int Id { get; set; }

        public string UserId { get; set; } = null!;

        public int RecipeId { get; set; }

        public Recipe Recipe { get; set; } = null!;

        public DateTime AddedOn { get; set; } = DateTime.UtcNow;
    }
}
