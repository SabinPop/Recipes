using System.ComponentModel.DataAnnotations.Schema;

namespace Recipes.Server.Models.Entities
{
    public class NutritionalValuesRecipeEntity : NutritionalValuesBase
    {
        public RecipeEntity Recipe { get; set; }

        [ForeignKey(nameof(RecipeId))]
        public int RecipeId { get; set; }
    }
}
