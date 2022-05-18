using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recipes.Server.Models.Entities
{
    public class RecipeEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RecipeId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 100 characters")]
        public string Name { get; set; }

        [Display(Name = "Creation Date")]
        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Duration")]
        [Range(1, 24 * 60, ErrorMessage = "Duration must be between 1 minute and 24 hours")]
        public int DurationInMinutes { get; set; }

        [StringLength(500, MinimumLength = 10, ErrorMessage = "Description must be between 10 and 500 characters")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Number Of Servings")]
        [Range(1, 12, ErrorMessage = "Number of servings must be between 1 and 12")]
        public int NumberOfServings { get; set; }

        public string ImageUrl { get; set; }


        // public ApplicationUser Author { get; set; }


        // public virtual ICollection<ApplicationUser> UsersWhoLikedThis { get; set; }


        public HashSet<IngredientWithQuantityEntity> IngredientsWithQuantities { get; set; }
        public List<RecipeStepEntity> RecipeSteps { get; set; }

        public HashSet<TagEntity> Tags { get; set; }

        public NutritionalValuesRecipeEntity NutritionalValues { get; set; }
    }
}