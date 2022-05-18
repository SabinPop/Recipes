using System.ComponentModel.DataAnnotations.Schema;

namespace Recipes.Server.Models.Entities
{
    public class NutritionalValuesIngredientEntity : NutritionalValuesBase //: IEquatable<NutritionalValuesEntity>
    {
        public IngredientEntity Ingredient { get; set; }

        [ForeignKey(nameof(IngredientId))]
        public int IngredientId { get; set; }

    }
}
