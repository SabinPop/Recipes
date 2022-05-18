using System.Collections.Generic;

namespace Recipes.Shared.Models
{
    public class RecipeDetails : RecipeBase
    {
        public int RecipeId { get; set; }

        // to see recipes tags on the Recipes/Home page

        public HashSet<Tag> Tags { get; set; }
    }
}
