using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recipes.API.Models.Entities
{
    public class IngredientEntity : IEquatable<IngredientEntity>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IngredientId { get; set; }

        public virtual ICollection<IngredientWithQuantityEntity> IngredientsWithQuantities { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }

        // eg if we measure flour, this is true
        // if we measure no of eggs or something that usually isn't
        // weighted, this property is false
        public bool UseWeight { get; set; } = true;

        [Range(0, 10000)]
        [Column(TypeName = "decimal(6, 1)")]
        public decimal WeightOfSinglePiece { get; set; }

        public NutritionalValuesEntity NutritionalValues { get; set; }

        public bool Equals(IngredientEntity other)
        {
            if (other is null)
                return false;
            if (ReferenceEquals(this, other))
                return true;
            if (GetType() != other.GetType())
                return false;
            return (Name == other.Name) 
                && (NutritionalValues == other.NutritionalValues);
        }

        public override bool Equals(object obj)
        {
            return Equals(Equals(obj as IngredientEntity));
        }

        public static bool operator ==(IngredientEntity left, IngredientEntity right)
        {
            if (left is null)
            {
                if (right is null)
                {
                    return true;
                }
                return false;
            }
            return left.Equals(right);
        }

        public static bool operator !=(IngredientEntity left, IngredientEntity right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            return (Name, NutritionalValues.GetHashCode()).GetHashCode();
        }
    }
}
