using Recipes.API.Models.Entities;
using System.Linq;

namespace Recipes.API.Services.Interfaces
{
    public interface IRecipeService
    {
        public bool Exists(RecipeEntity recipe);
        public bool Exists(int id);

        public bool CreateRecipe(RecipeEntity recipe);
        public RecipeEntity GetRecipeById(int id);
        public bool UpdateRecipe(RecipeEntity recipe);
        public bool DeleteRecipe(int id);

        public IQueryable<RecipeEntity> GetAllRecipes();
    }
}
