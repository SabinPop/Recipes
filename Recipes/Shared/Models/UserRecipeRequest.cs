using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Shared.Models
{
    public class UserRecipeRequest
    {
        public int RecipeId { get; set; }
        public string UserName { get; set; }
    }
}
