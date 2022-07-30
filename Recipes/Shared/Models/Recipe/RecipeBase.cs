using System;

namespace Recipes.Shared.Models
{
    public class RecipeBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int DurationInMinutes { get; set; }
        public int NumberOfServings { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
