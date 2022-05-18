namespace Recipes.Shared.Models
{
    public class Ingredient
    {
        public string Name { get; set; }

        public bool UseWeight { get; set; } = true;

        public decimal WeightOfSinglePiece { get; set; }

        public NutritionalValues NutritionalValues { get; set; }
    }
}
