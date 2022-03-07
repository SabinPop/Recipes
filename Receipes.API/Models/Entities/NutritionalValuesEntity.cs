using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recipes.API.Models.Entities
{
    public class NutritionalValuesEntity : IEquatable<NutritionalValuesEntity>
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

        public bool Equals(NutritionalValuesEntity other)
        {
            if (other is null)
                return false;
            if (ReferenceEquals(this, other))
                return true;
            if (GetType() != other.GetType())
                return false;
            return (Kilocalories == other.Kilocalories)
                && (Fat == other.Fat)
                && (Protein == other.Protein)
                && (Carbs == other.Carbs);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as NutritionalValuesEntity);
        }

        public static bool operator ==(NutritionalValuesEntity left, NutritionalValuesEntity right)
        {
            if(left is null)
            {
                if(right is null)
                {
                    return true;
                }
                return false;
            }
            return left.Equals(right);
        }

        public static bool operator !=(NutritionalValuesEntity left, NutritionalValuesEntity right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            return (Kilocalories, Fat, Protein, Carbs).GetHashCode();
        }
    }
}
