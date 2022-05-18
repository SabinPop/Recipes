using AutoMapper;
using Recipes.Server.Models.Entities;
using Recipes.Shared.Models;
using System.Collections.Generic;

namespace Recipes.Server.Automapper.Profiles
{
    public class RecipeProfile : Profile
    {
        public RecipeProfile()
        {
            #region Ingredient Mapping
            CreateMap<IngredientEntity, IngredientView>().ReverseMap();
            CreateMap<IngredientEntity, IngredientCreate>().ReverseMap();
            CreateMap<IngredientEntity, Ingredient>().ReverseMap();
            #endregion

            #region Nutritional Values Mapping 
            CreateMap<NutritionalValuesIngredientEntity, NutritionalValues>().ReverseMap();
            CreateMap<NutritionalValuesRecipeEntity, NutritionalValues>().ReverseMap();
            //CreateMap<IEnumerable<IngredientEntity>, IEnumerable<Ingredient>>().ReverseMap();
            #endregion

            #region Recipe Mapping
            CreateMap<List<RecipeEntity>, List<RecipeDetails>>();

            CreateMap<RecipeEntity, Recipe>().ReverseMap();
            CreateMap<RecipeCreate, RecipeEntity>()
                //.ForMember(r => r.RecipeId, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<RecipeEntity, RecipeDetails>().ReverseMap();
            CreateMap<RecipeEntity, RecipeEdit>().ReverseMap();
            CreateMap<RecipeEntity, RecipeView>().ReverseMap();

            CreateMap<RecipeCreate, RecipeView>().ReverseMap();
            CreateMap<RecipeCreate, RecipeEdit>().ReverseMap();
            #endregion

            #region Ingredient With Quantity Mapping 
            CreateMap<IngredientWithQuantityEntity, IngredientWithQuantity>().ReverseMap();
            //.ForMember(x=>x.IngredientId, x => x.MapFrom(x => x.Ingredient.IngredientId));
            #endregion

            #region Recipe Step Mapping
            CreateMap<RecipeStepEntity, RecipeStep>().ReverseMap();
            CreateMap<RecipeStepEntity, RecipeStepEdit>().ReverseMap();
            CreateMap<RecipeStepEntity, RecipeStepView>().ReverseMap();
            #endregion

            #region Tag Mapping 
            CreateMap<TagEntity, Tag>().ReverseMap();
            CreateMap<TagEntity, TagCreate>().ReverseMap();
            CreateMap<TagEntity, TagEdit>().ReverseMap();
            #endregion 
        }
    }
}
