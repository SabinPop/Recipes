using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recipes.API.Models.Entities
{
    public class RecipeStepEntity
    {
        [Key]
        public int StepId { get; set; }

        public RecipeEntity Recipe { get; set; }

        [ForeignKey(nameof(RecipeId))]
        public int RecipeId { get; set; }

        [Display(Name = "Step title")]
        [StringLength(100, ErrorMessage = "Step title cannot exceed {1} characters")]
        public string StepTitle { get; set; }

        [Display(Name = "Step description")]
        public string StepDescription { get; set; }
    }
}
