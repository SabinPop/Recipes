using Recipes.API.Models.Entities;
using System.Linq;

namespace Recipes.API.Services.Interfaces
{
    public interface IIngredientService
    {
        public bool Exists(IngredientEntity ingredient);
        public bool Exists(int id);

        public bool CreateIngredient(IngredientEntity ingredient);
        public IngredientEntity GetIngredientById(int id);
        public bool UpdateIngredient(IngredientEntity ingredient);
        public bool DeleteIngredient(int id);

        public IQueryable<IngredientEntity> GetAllIngredients();

    }
}
