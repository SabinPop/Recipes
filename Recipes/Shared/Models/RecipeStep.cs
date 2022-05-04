using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Shared.Models
{
    public class RecipeStep
    {
        public int StepId { get; set; }

        public int StepNumber { get; set; }

        public string StepTitle { get; set; } = string.Empty;

        public string StepDescription { get; set; } = string.Empty;
    }
}
