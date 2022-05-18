using System.Collections.Generic;

namespace Recipes.Shared.Models
{
    public class Recipe : RecipeBase
    {
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

        public HashSet<TagEdit> GetTagsFromNutritionalValues()
        {
            var tags = new HashSet<TagEdit>();
            if (NutritionalValues.Fat <= 3)
            {
                if (NutritionalValues.Fat < (decimal)0.2)
                {
                    tags.Add(new TagEdit()
                    {
                        Name = "no fat"
                    });
                }
                else
                {
                    tags.Add(new TagEdit
                    {
                        Name = "fow fat"
                    });
                }
            }
            else if (NutritionalValues.Fat >= 17)
            {
                tags.Add(new TagEdit()
                {
                    Name = "high fat"
                });
            }
            if (NutritionalValues.Carbs <= 4)
            {
                if (NutritionalValues.Carbs < (decimal)0.2)
                {
                    tags.Add(new TagEdit()
                    {
                        Name = "no carbs"
                    });
                }
                else
                {
                    tags.Add(new TagEdit
                    {
                        Name = "low carbs"
                    });
                }

            }
            else if (NutritionalValues.Carbs >= 20)
            {
                tags.Add(new TagEdit()
                {
                    Name = "high carbs"
                });
            }
            if (NutritionalValues.Protein >= 16)
            {
                tags.Add(new TagEdit()
                {
                    Name = "high protein"
                });
            }

            return tags;
        }
    }
}
