using AutoMapper;
using Recipes.Server.Models.Entities;
using Recipes.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recipes.Server.Automapper.Profiles
{
    public class RecipeProfile : Profile
    {
        public RecipeProfile()
        {
            CreateMap<IngredientEntity, Ingredient>().ReverseMap();
            CreateMap<NutritionalValuesEntity, NutritionalValues>().ReverseMap();
            //CreateMap<IEnumerable<IngredientEntity>, IEnumerable<Ingredient>>().ReverseMap();
        }
    }
}
