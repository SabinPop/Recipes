using Microsoft.AspNetCore.Identity;
using Recipes.Server.Models.Entities;
using System.Collections.Generic;

namespace Recipes.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<RecipeEntity> UserRecipes { get; set; }
        public virtual ICollection<RecipeEntity> FavoriteRecipes { get; set; }
    }
}
