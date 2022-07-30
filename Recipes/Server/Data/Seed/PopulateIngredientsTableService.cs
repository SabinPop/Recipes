using Recipes.Server.Models.Entities;
using Recipes.Server.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Recipes.Server.Data.Seed
{
    public class PopulateIngredientsTableService
    {
        private readonly IRepository<IngredientEntity, int> _ingredientService;

        public PopulateIngredientsTableService(IRepository<IngredientEntity, int> ingredientService)
        {
            _ingredientService = ingredientService;
        }

        public void Populate()
        {
            MapperAlimenteIngrediente m = new MapperAlimenteIngrediente();
            m.LoadFromFile("Data\\Seed\\alimente.json");
            m.GetIngredientsFromAlimente();
            m.SaveToFile("Data\\Seed\\ingrediente.json");

            var ingredients = m.Ingredients;
            //var ingredients = GetIngredientsFromFile("Data\\Seed\\ingrediente.json");
            Console.WriteLine("no of ingredients:");
            Console.WriteLine(ingredients.Count);
            PopulateIngredientTable(ingredients);
            Console.WriteLine("completed");
        }

        private static List<IngredientEntity> GetIngredientsFromFile(string filename)
        {
            var ingredients = new List<IngredientEntity>();
            using (StreamReader reader = new StreamReader(filename))
            {
                var json = reader.ReadToEnd();
                ingredients = (List<IngredientEntity>)JsonSerializer.Deserialize(json, typeof(List<IngredientEntity>));
            }
            return ingredients;
        }

        private void PopulateIngredientTable(List<IngredientEntity> ingredients)
        {
            foreach (var i in ingredients.ToHashSet().OrderBy(x => x.Name))
            {
                var result = _ingredientService.Create(i);
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
