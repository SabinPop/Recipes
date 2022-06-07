using Recipes.Server.Models.Entities;
using Recipes.Server.Services.Interfaces;
using Recipes.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recipes.Server.Services
{
    public static class Extensions
    {
        public static RecipeDetails InjectFavorite(this RecipeDetails recipe, IUserService service, UserRecipeRequest request)
        {
            recipe.IsFavorite = service.IsRecipeFavorite(request);
            return recipe;
        }

        public static IEnumerable<RecipeDetails> InjectFavorite(this IEnumerable<RecipeDetails> recipes, IUserService service, string username)
        {
            if (service.Exists(username)){
                foreach (var r in recipes) {
                    r.IsFavorite = service.IsRecipeFavorite(new UserRecipeRequest() { RecipeId = r.RecipeId, UserName = username });
                }
            }
            return recipes;
        }

        public static RecipeView InjectFavorite(this RecipeView recipe, IUserService service, UserRecipeRequest request)
        {
            recipe.IsFavorite = service.IsRecipeFavorite(request);
            return recipe;
        }

        public static IEnumerable<RecipeView> InjectFavorite(this IEnumerable<RecipeView> recipes, IUserService service, UserRecipeRequest request)
        {
            foreach (var r in recipes)
            {
                r.IsFavorite = service.IsRecipeFavorite(request);
            }
            return recipes;
        }
    }
}
