namespace Recipes.Shared.Models
{
    public class RecipeCreate : Recipe
    {
        // public string AuthorId { get; set; }
        private NutritionalValues nutritionalValues;
        public new NutritionalValues NutritionalValues
        {
            get
            {
                nutritionalValues = CalculateFromIngredients();
                return nutritionalValues;
            }

            set
            {
                nutritionalValues = value;
            }
        }
    }
}
