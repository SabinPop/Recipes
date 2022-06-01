using Recipes.Shared.Extensions;
using System.Collections.Generic;

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
        public HashSet<TagEdit> GetTagsFromNutritionalValues()
        {
            var tags = new HashSet<TagEdit>();
            if (NutritionalValues.Fat.IsLessThan(3))
            {
                if (NutritionalValues.Fat.IsLessThan((decimal)0.2))
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
                        Name = "low fat"
                    });
                }
            }
            else if (NutritionalValues.Fat.IsGreaterThan(17))
            {
                tags.Add(new TagEdit()
                {
                    Name = "high fat"
                });
            }
            if (NutritionalValues.Carbs.IsLessThan(4))
            {
                if (NutritionalValues.Carbs.IsLessThan((decimal)0.2))
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
            else if (NutritionalValues.Carbs.IsGreaterThan(20))
            {
                tags.Add(new TagEdit()
                {
                    Name = "high carbs"
                });
            }
            if (NutritionalValues.Protein.IsGreaterThan(16))
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
