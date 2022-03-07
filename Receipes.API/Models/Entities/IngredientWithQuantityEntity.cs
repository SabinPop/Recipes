﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recipes.API.Models.Entities
{
    public class IngredientWithQuantityEntity
    {
        public RecipeEntity Recipe { get; set; }
        [ForeignKey(nameof(RecipeId))]
        public int RecipeId { get; set; }

        public IngredientEntity Ingredient { get; set; }
        [ForeignKey(nameof(IngredientId))]
        public int IngredientId { get; set; } 

        [Column(TypeName = "decimal(5, 1)")]
        public decimal Quantity { get; set; }
    }
}