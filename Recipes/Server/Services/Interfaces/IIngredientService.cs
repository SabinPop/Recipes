using Recipes.Server.Models.Entities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recipes.Server.Services.Interfaces
{
    public interface IIngredientService
    {
        public bool Exists(IngredientEntity ingredient);
        public Task<bool> ExistsAsync(IngredientEntity ingredient);
        public bool Exists(int id);
        public Task<bool> ExistsAsync(int id);

        public bool CreateIngredient(IngredientEntity ingredient);

        public IngredientEntity GetIngredientById(int id);
        public Task<IngredientEntity> GetIngredientByIdAsync(int id);

        public bool UpdateIngredient(IngredientEntity ingredient);

        public bool DeleteIngredient(int id);

        public IQueryable<IngredientEntity> GetAllIngredients();

        public IEnumerable<IngredientEntity> GetIngredientsThatStartWithString(string name);
    }
}
