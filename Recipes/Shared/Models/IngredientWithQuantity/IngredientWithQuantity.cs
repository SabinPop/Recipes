namespace Recipes.Shared.Models
{
    public class IngredientWithQuantity
    {
        public IngredientView Ingredient { get; set; } // = new IngredientView();
        public string Name { get => Ingredient == null ? "" : Ingredient.Name ?? ""; }
        public int Quantity { get; set; }
    }
}
