using Microsoft.EntityFrameworkCore;
using Recipes.Server.Data;
using Recipes.Server.Models.Entities;
using Recipes.Server.Services.Interfaces;
using Recipes.Shared.Models;
using Recipes.Shared.Paging;
using System;
using System.Linq;

namespace Recipes.Server.Services
{


    /// <summary>
    /// TODO:
    /// make sure all services are using Automapper to map from Entity type to DTO type
    /// </summary>
    /// 
    public class IngredientService : IRepository<IngredientEntity, int>
    {
        private readonly ApplicationDbContext _context;

        public IngredientService(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Create(IngredientEntity ingredient)
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

        public bool Delete(int id)
        {
            if (Exists(id) == false)
                return false;
            try
            {
                var ingredient = GetById(id);
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

        public bool Exists(int id)
        {
            return _context.Ingredients.Any(i => i.IngredientId == id);
        }

        public IQueryable<IngredientEntity> GetAll()
        {
            return _context.Ingredients.AsNoTracking().Include(i => i.NutritionalValues).Select(x => x);
        }

        public IngredientEntity GetById(int id)
        {
            return _context.Ingredients.AsNoTracking().Include(i => i.NutritionalValues).FirstOrDefault(i => i.IngredientId == id);
        }

        public PagedList<IngredientEntity> GetPage(Parameters parameters)
        {
            var ingredients = GetAll().ToList();
            return PagedList<IngredientEntity>
                .ToPagedList(ingredients,
                    parameters.PageNumber,
                    parameters.PageSize);
        }


        //get ingredients that start with {string}
        public IQueryable<IngredientEntity> GetStartingWithString(string name)
        {
            return _context.Ingredients.AsNoTracking().Where(i => i.Name.ToLower().StartsWith(name.ToLower()));
        }

        public bool Update(IngredientEntity ingredient)
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
