using Recipes.API.Models.Entities;
using Recipes.API.Services;
using Recipes.API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Recipes.API.Data.Seed
{
    public class PopulateIngredientsTableService
    {
        private readonly IIngredientService _ingredientService;

        public PopulateIngredientsTableService(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
            
        }

        public void Populate()
        {
            var ingredients = GetIngredientsFromFile("Data\\Seed\\ingrediente.json");
            Console.WriteLine("no of ingredients:");
            Console.WriteLine(ingredients.Count);
            PopulateIngredientTable(ingredients);
            Console.WriteLine("completed");
        }

        private static List<IngredientEntity> GetIngredientsFromFile(string filename)
        {
            var ingredients = new List<IngredientEntity>();
            using(StreamReader reader = new StreamReader(filename))
            {
                var json = reader.ReadToEnd();
                ingredients = (List<IngredientEntity>)JsonSerializer.Deserialize(json, typeof(List<IngredientEntity>));
            }
            return ingredients;
        }

        private void PopulateIngredientTable(List<IngredientEntity> ingredients)
        {
            foreach(var i in ingredients)
            {
                var result = _ingredientService.CreateIngredient(i);
                if (result)
                {
                    Console.WriteLine("Adding {0} to the ingredient table", i.Name);
                }
                else
                {
                    Console.WriteLine("skip {0}", i.Name);
                }
            }
        }
    }
}
