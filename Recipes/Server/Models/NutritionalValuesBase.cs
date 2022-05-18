using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recipes.Server.Models
{
    public class NutritionalValuesBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NutritionalValuesId { get; set; }

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

        public bool Equals(NutritionalValuesBase other)
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
            return Equals(obj as NutritionalValuesBase);
        }

        public static bool operator ==(NutritionalValuesBase left, NutritionalValuesBase right)
        {
            if (left is null)
            {
                if (right is null)
                    return true;
                return false;
            }
            return left.Equals(right);
        }

        public static bool operator !=(NutritionalValuesBase left, NutritionalValuesBase right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Kilocalories, Fat, Protein, Carbs);
        }
    }
}
