using System.Collections.Generic;

namespace Recipes.Shared.Models
{
    public class Recipe : RecipeBase
    {
        public string Author { get; set; }

        public NutritionalValues NutritionalValues { get; set; } = new NutritionalValues();

        public HashSet<IngredientWithQuantity> IngredientsWithQuantities { get; set; }
            = new HashSet<IngredientWithQuantity>();

        public List<RecipeStep> RecipeSteps { get; set; }
            = new List<RecipeStep>();

        public HashSet<TagEdit> Tags { get; set; }
            = new HashSet<TagEdit>();


        protected NutritionalValues CalculateFromIngredients()
        {
            var nutritionalValues = new NutritionalValues();
            var _weight = 0;
            foreach (var iq in IngredientsWithQuantities)
            {
                nutritionalValues.Kilocalories += iq.Ingredient.NutritionalValues.Kilocalories * iq.Quantity
                    * (iq.Ingredient.UseWeight ? (decimal)0.01 : iq.Ingredient.WeightOfSinglePiece);
                nutritionalValues.Carbs += iq.Ingredient.NutritionalValues.Carbs * iq.Quantity
                    * (iq.Ingredient.UseWeight ? (decimal)0.01 : iq.Ingredient.WeightOfSinglePiece);
                nutritionalValues.Fat += iq.Ingredient.NutritionalValues.Fat * iq.Quantity
                    * (iq.Ingredient.UseWeight ? (decimal)0.01 : iq.Ingredient.WeightOfSinglePiece);
                nutritionalValues.Protein += iq.Ingredient.NutritionalValues.Protein * iq.Quantity
                    * (iq.Ingredient.UseWeight ? (decimal)0.01 : iq.Ingredient.WeightOfSinglePiece);
                _weight += iq.Quantity;
            }
            nutritionalValues.Kilocalories /= _weight == 0 ? 1 : _weight / 100;
            nutritionalValues.Carbs /= _weight == 0 ? 1 : _weight / 100;
            nutritionalValues.Protein /= _weight == 0 ? 1 : _weight / 100;
            nutritionalValues.Fat /= _weight == 0 ? 1 : _weight / 100;

            return nutritionalValues;
        }

        
    }
}
