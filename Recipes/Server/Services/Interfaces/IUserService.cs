using Recipes.Server.Models;
using Recipes.Server.Models.Entities;
using Recipes.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Recipes.Server.Services.Interfaces
{
    public interface IUserService
    {
        bool AddRecipeToFavorites(RecipeEntity recipe, string username);
        bool Exists(string username);
        ClaimsPrincipal GetUser();
        ApplicationUser GetUserByUserName(string username);
        List<RecipeEntity> GetUserFavoriteRecipes(string username);
        bool HasFavorites(string username);
        bool HasPublishedRecipes(string username);
        bool IsRecipeFavorite(UserRecipeRequest request);
        bool RemoveRecipeFromFavorites(int recipeId, string username);
    }
}
