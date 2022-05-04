using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Shared.Models.Recipe
{
    public class RecipeView
    {
        public int RecipeId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreationDate { get; set; }

        public int DurationInMinutes { get; set; }

        public int NumberOfServings { get; set; }


    }
}
