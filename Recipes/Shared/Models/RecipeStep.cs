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

        public string StepTitle { get; set; }

        public string StepDescription { get; set; }
    }
}
