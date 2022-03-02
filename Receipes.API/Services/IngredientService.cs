using Microsoft.EntityFrameworkCore;
using Recipes.API.Data;
using Recipes.API.Models.Entities;
using Recipes.API.Services.Interfaces;
using System;
using System.Linq;

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
            if (ingredient == null)
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
            return _context.Ingredients.FirstOrDefault(i => i == ingredient) != null;
        }

        public bool Exists(int id)
        {
            return _context.Ingredients.FirstOrDefault(i => i.IngredientId == id) != null;
        }

        public IQueryable<IngredientEntity> GetAllIngredients()
        {
            return _context.Ingredients.AsNoTracking().Select(x => x);
        }

        public IngredientEntity GetIngredientById(int id)
        {
            return _context.Ingredients.FirstOrDefault(i => i.IngredientId == id);
        }

        public bool UpdateIngredient(IngredientEntity ingredient)
        {
            if (ingredient == null)
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
