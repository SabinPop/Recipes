using Microsoft.EntityFrameworkCore;
using Recipes.Server.Data;
using Recipes.Server.Models.Entities;
using Recipes.Shared.Paging;
using Recipes.Server.Services.Interfaces;
using Recipes.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Recipes.Server.Services
{
    public class RecipeService : IRepository<RecipeEntity, int>
    {
        private readonly ApplicationDbContext _context;

        public RecipeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Create(RecipeEntity recipe)
        {
            if (recipe is null)
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

        public bool Delete(int id)
        {
            if (Exists(id) == false)
                return false;
            try
            {
                var recipe = GetById(id);
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
            return _context.Recipes.AsNoTracking().Any(r => r == recipe);
        }

        public bool Exists(int id)
        {
            return _context.Recipes.AsNoTracking().Any(r => r.RecipeId == id);
        }

        public IQueryable<RecipeEntity> GetAll()
        {
            return _context.Recipes.AsNoTracking().Select(x => x);
        }

        public RecipeEntity GetById(int id)
        {
            return _context.Recipes.AsNoTracking().Include(r => r.IngredientsWithQuantities)
                .Include(r => r.RecipeSteps).Include(r => r.Tags).ToList()
                .FirstOrDefault(r => r.RecipeId == id);
        }

        //not necessarily at this moment 
        public IEnumerable<IngredientEntity> GetIngredientsByRecipeId(int id)
        {
            if (Exists(id) == false)
                return new List<IngredientEntity>();
            var recipe = _context.Recipes.AsNoTracking().Include(r => r.IngredientsWithQuantities)
                .ToList().FirstOrDefault(r => r.RecipeId == id);
            return recipe.IngredientsWithQuantities.Select(iq => iq.Ingredient);
        }

        public PagedList<RecipeEntity> GetPage(Parameters parameters)
        {
            var recipes = GetAll().ToList();
            return PagedList<RecipeEntity>
                .ToPagedList(recipes,
                    parameters.PageNumber,
                    parameters.PageSize);
        }

        public IQueryable<RecipeEntity> GetStartingWithString(string name)
        {
            return _context.Recipes.AsNoTracking().Where(i => i.Name.ToLower().StartsWith(name.ToLower()));
        }

        public bool Update(RecipeEntity recipe)
        {
            if (recipe is null)
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
