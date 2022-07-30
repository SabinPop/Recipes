using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recipes.Server.Models.Entities
{
    public class IngredientEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IngredientId { get; set; }

        public virtual ICollection<IngredientWithQuantityEntity> IngredientsWithQuantities { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }

        public bool UseWeight { get; set; } = true;

        [Range(0, 10000)]
        [Column(TypeName = "decimal(6, 1)")]
        public decimal WeightOfSinglePiece { get; set; }

        public NutritionalValuesIngredientEntity NutritionalValues { get; set; }

    }
}
