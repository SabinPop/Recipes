using Recipes.Server.Models.Entities;
using System.Linq;

namespace Recipes.Server.Services.Interfaces
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
