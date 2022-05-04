using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Shared.Models.IngredientWithQuantity
{
    public class IngredientWithQuantity
    {
        public Ingredient Ingredient { get; set; } = new Ingredient();
        public string Name { get => Ingredient.Name ?? ""; }
        public int Quantity { get; set; }
    }
}
