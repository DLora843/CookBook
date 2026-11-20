namespace CookBook.Web.ViewModels
{
    public class RecipeListViewModelcs
    {
        public IEnumerable<RecipeViewModel> Recipes { get; set; }
        = new List<RecipeViewModel>();

        public string? SearchTerm { get; set; }

        public int TotalRecipes { get; set; }
    }
}
