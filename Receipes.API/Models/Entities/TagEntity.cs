using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Recipes.API.Models.Entities
{
    public class TagEntity
    {
        [Key]
        public int TagId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "{0} length cannot exceed {1} characters")]
        public string Name { get; set; }

        public virtual ICollection<RecipeEntity> Recipes { get; set; }
    }
}