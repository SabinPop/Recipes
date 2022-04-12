using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Recipes.Server.Models.Entities
{
    public class TagEntity : IEquatable<TagEntity>
    {
        [Key]
        public int TagId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "{0} length cannot exceed {1} characters")]
        public string Name { get; set; }

        public virtual ICollection<RecipeEntity> Recipes { get; set; }

        public bool Equals(TagEntity other)
        {
            if (other is null)
                return false;
            if (ReferenceEquals(this, other))
                return true;
            if (GetType() != other.GetType())
                return false;
            return Name == other.Name;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as TagEntity);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public static bool operator ==(TagEntity left, TagEntity right)
        {
            if (left is null)
            {
                if (right is null)
                    return true;
                return false;
            }
            return left.Equals(right);
        }

        public static bool operator !=(TagEntity left, TagEntity right)
        {
            return !(left == right);
        }
    }
}