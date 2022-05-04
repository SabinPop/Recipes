using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recipes.Shared.Models
{
    public class Ingredient
    {
        public int IngredientId { get; set; }

        public string Name { get; set; }

        public bool UseWeight { get; set; } = true;

        public decimal WeightOfSinglePiece { get; set; }

        public NutritionalValues NutritionalValues { get; set; }
    }
}
