using Microsoft.AspNetCore.Identity;
using Recipes.Server.Models.Entities;
using System.Collections.Generic;

namespace Recipes.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual HashSet<RecipeEntity> UserRecipes { get; set; }
        public virtual HashSet<RecipeEntity> FavoriteRecipes { get; set; }
    }
}
