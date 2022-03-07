using Microsoft.EntityFrameworkCore;
using Recipes.API.Data;
using Recipes.API.Models.Entities;
using Recipes.API.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Recipes.API.Services
{
    public class IngredientService : IIngredientService
    {
        private readonly RecipesDbContext _context;

        public IngredientService(RecipesDbContext context)
        {
            _context = context;
        }

        public bool CreateIngredient(IngredientEntity ingredient)
        {
            if (ingredient is null)
                return false;
            if (Exists(ingredient))
                return false;

            try
            {
                _context.Ingredients.Add(ingredient);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteIngredient(int id)
        {
            if (Exists(id) == false)
                return false;
            try
            {
                var ingredient = GetIngredientById(id);
                _context.Ingredients.Remove(ingredient);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Exists(IngredientEntity ingredient)
        {
            return _context.Ingredients.AsNoTracking().Include(i => i.NutritionalValues).ToList().Any(i => i == ingredient);
        }
        
        public async Task<bool> ExistsAsync(IngredientEntity ingredient)
        {
            return (await _context.Ingredients.AsNoTracking().Include(i => i.NutritionalValues).ToListAsync()).Any(i => i == ingredient);
        }

        public bool Exists(int id)
        {
            return _context.Ingredients.Any(i => i.IngredientId == id);
        }
        
        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Ingredients.AnyAsync(i => i.IngredientId == id);
        }

        public IQueryable<IngredientEntity> GetAllIngredients()
        {
            return _context.Ingredients.AsNoTracking().Select(x => x);
        }

        public IngredientEntity GetIngredientById(int id)
        {
            return _context.Ingredients.AsNoTracking().Include(i => i.NutritionalValues).FirstOrDefault(i => i.IngredientId == id);
        }

        public async Task<IngredientEntity> GetIngredientByIdAsync(int id)
        {
            return await _context.Ingredients.AsNoTracking().Include(i => i.NutritionalValues).FirstOrDefaultAsync(i => i.IngredientId == id);
        }

        public bool UpdateIngredient(IngredientEntity ingredient)
        {
            if (ingredient is null)
                return false;
            if (Exists(ingredient) == false)
                return false;
            try
            {
                _context.Ingredients.Update(ingredient);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
