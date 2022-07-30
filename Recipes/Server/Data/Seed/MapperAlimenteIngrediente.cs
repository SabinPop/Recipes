using Recipes.Server.Models.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Recipes.Server.Data.Seed
{
    public class MapperAlimenteIngrediente
    {
        public List<Aliment> Alimente { get; set; } = new List<Aliment>();
        public List<IngredientEntity> Ingredients { get; set; } = new List<IngredientEntity>();

        public MapperAlimenteIngrediente(List<Aliment> alimente)
        {
            Alimente = alimente;
        }

        public MapperAlimenteIngrediente()
        {
        }

        public void LoadFromFile(string file)
        {
            using (StreamReader s = new StreamReader(file))
            {
                string json = s.ReadToEnd();
                Alimente = (List<Aliment>)JsonSerializer.Deserialize(json, typeof(List<Aliment>));
            }
        }

        public void SaveToFile(string file)
        {
            string json = JsonSerializer.Serialize(Ingredients);
            using (StreamWriter s = new StreamWriter(file))
            {
                s.Write(json);
            }
        }

        public void GetIngredientsFromAlimente()
        {
            foreach (var aliment in Alimente)
            {
                var ingredient = new IngredientEntity()
                {
                    Name = aliment.Nume,
                    NutritionalValues = new NutritionalValuesIngredientEntity()
                    {
                        Kilocalories = (decimal)aliment.Calorii,
                        Protein = Convert.ToDecimal(aliment.Proteine == "" ? 0 : aliment.Proteine.Contains("%") ? aliment.Proteine.Replace("%", "") : aliment.Proteine),
                        Carbs = Convert.ToDecimal(aliment.Carbohidrati == "" ? 0 : aliment.Carbohidrati.Contains("%") ? aliment.Carbohidrati.Replace("%", "") : 0),
                        Fat = (decimal)(aliment.Lipide != null ? aliment.Lipide : 0)
                    }
                };
                Ingredients.Add(ingredient);
            }
        }
    }
}
