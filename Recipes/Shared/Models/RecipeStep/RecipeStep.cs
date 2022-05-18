namespace Recipes.Shared.Models
{
    public class RecipeStep
    {
        public int StepNumber { get; set; }

        public string StepTitle { get; set; } = string.Empty;

        public string StepDescription { get; set; } = string.Empty;
    }
}
