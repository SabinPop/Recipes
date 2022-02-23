using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recipes.API.Models.Entities
{
    public class NutritionalValuesEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NutritionalValuesId { get; set; }

        public IngredientEntity Ingredient { get; set; }

        [ForeignKey(nameof(IngredientId))]
        public int IngredientId { get; set; }

        [Range(1, 900)]
        [Column(TypeName = "decimal(5, 1)")]
        public decimal Kilocalories { get; set; }

        [Range(0, 100)]
        [Column(TypeName = "decimal(5, 1)")]
        public decimal Fat { get; set; }

        [Range(0, 100)]
        [Column(TypeName = "decimal(5, 1)")]
        public decimal Protein { get; set; }

        [Range(0, 100)]
        [Column(TypeName = "decimal(5, 1)")]
        public decimal Carbs { get; set; }
    }
}
