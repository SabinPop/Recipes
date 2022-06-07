using System.Collections.Generic;

namespace Recipes.Shared.Models
{
    public class RecipeView : Recipe
    {
        // I actually don't know if I need this
        public int RecipeId { get; set; }

        public bool IsFavorite { get; set; }

        public Dictionary<string, decimal> NutritionalValuesDictionary()
        {
            var x = new Dictionary<string, decimal>
            {
                { "Energy", NutritionalValues.Kilocalories },
                { "Protein", NutritionalValues.Protein },
                { "Fat", NutritionalValues.Fat },
                { "Carbs", NutritionalValues.Carbs }
            };
            return x;
        }
    }
}
