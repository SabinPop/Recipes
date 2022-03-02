using Microsoft.EntityFrameworkCore;
using Recipes.API.Data;
using Recipes.API.Models.Entities;
using Recipes.API.Services.Interfaces;
using System;
using System.Linq;

namespace Recipes.API.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly RecipesDbContext _context;

        public RecipeService(RecipesDbContext context)
        {
            _context = context;
        }

        public bool CreateRecipe(RecipeEntity recipe)
        {
            if (recipe == null)
                return false;
            if (Exists(recipe))
                return false;
            try
            {
                _context.Recipes.Add(recipe);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteRecipe(int id)
        {
            if (Exists(id) == false)
                return false;
            try
            {
                var recipe = GetRecipeById(id);
                _context.Recipes.Remove(recipe);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Exists(RecipeEntity recipe)
        {
            return _context.Recipes.FirstOrDefault(r => r == recipe) != null;
        }

        public bool Exists(int id)
        {
            return _context.Recipes.FirstOrDefault(r => r.RecipeId == id) != null;
        }

        public IQueryable<RecipeEntity> GetAllRecipes()
        {
            return _context.Recipes.AsNoTracking().Select(x => x);
        }

        public RecipeEntity GetRecipeById(int id)
        {
            return _context.Recipes.FirstOrDefault(r => r.RecipeId == id);
        }

        public bool UpdateRecipe(RecipeEntity recipe)
        {
            if (recipe == null)
                return false;
            if (Exists(recipe) == false)
                return false;
            try
            {
                _context.Recipes.Update(recipe);
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
