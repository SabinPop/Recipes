using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Shared.Models.Recipe
{
    public class RecipeUpdate
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreationDate { get; set; }

        public int DurationInMinutes { get; set; }

        public int NumberOfServings { get; set; }


        public string UserName { get; set; }

        public IDictionary<Ingredient, int> IngredientsWithQuantities { get; set; }

        public ICollection<RecipeStep> RecipeSteps { get; set; }

        public ICollection<Tag> Tags { get; set; }

        public NutritionalValues NutritionalValues => CalculateFromIngredients();

        private NutritionalValues CalculateFromIngredients()
        {
            var nutritionalValues = new NutritionalValues();
            foreach (var ingredient in IngredientsWithQuantities)
            {
                nutritionalValues.Kilocalories += ingredient.Key.NutritionalValues.Kilocalories * ingredient.Value
                    * (ingredient.Key.UseWeight ? 1 : ingredient.Key.WeightOfSinglePiece);
                nutritionalValues.Carbs += ingredient.Key.NutritionalValues.Carbs * ingredient.Value
                    * (ingredient.Key.UseWeight ? 1 : ingredient.Key.WeightOfSinglePiece);
                nutritionalValues.Fat += ingredient.Key.NutritionalValues.Fat * ingredient.Value
                    * (ingredient.Key.UseWeight ? 1 : ingredient.Key.WeightOfSinglePiece);
                nutritionalValues.Protein += ingredient.Key.NutritionalValues.Protein * ingredient.Value
                    * (ingredient.Key.UseWeight ? 1 : ingredient.Key.WeightOfSinglePiece);
            }
            return nutritionalValues;
        }
    }
}
