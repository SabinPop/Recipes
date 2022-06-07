using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Recipes.Server.Data;
using Recipes.Server.Models.Entities;
using Recipes.Server.Services.Interfaces;
using Recipes.Shared.Models;
using Recipes.Shared.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Recipes.Server.Services.ServiceConstants;

namespace Recipes.Server.Services
{
    public class RecipeService : IRepository<RecipeEntity, int>
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public RecipeService(ApplicationDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public bool Create(RecipeEntity recipe)
        {
            if (recipe is null)
                return false;
            if (Exists(recipe))
                return false;
            try
            {
                recipe = PrepareRecipeEntityForEF(recipe);
                recipe.CreationDate = DateTime.Now;
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
            return _context.Recipes.AsNoTracking()
                .Select(x => x)
                .Include(r => r.User)
                    .ThenInclude(x => x.FavoriteRecipes)
                .Include(r => r.Tags)
                .Include(r => r.UsersWhoLikedThis);
        }

        public RecipeEntity GetById(int id)
        {
            return _context.Recipes.AsNoTracking()
                .Include(r => r.IngredientsWithQuantities)
                    .ThenInclude(iq => iq.Ingredient)
                .ThenInclude(i => i.NutritionalValues)
                .Include(r => r.RecipeSteps)
                .Include(r => r.Tags)
                .Include(r => r.NutritionalValues).ToList()
                .FirstOrDefault(r => r.RecipeId == id);
        }

        public IEnumerable<RecipeEntity> GetUserRecipes(string username)
        {
            if (!_userService.Exists(username) || !_userService.HasPublishedRecipes(username))
                return new List<RecipeEntity>();
            return GetAll().Where(r => r.Author.ToUpper() == username.ToUpper());
        }

        public IEnumerable<RecipeEntity> GetUserFavorites(string username)
        {
            return _userService.GetUserFavoriteRecipes(username);
        }

        public bool IsUserFavorite(UserRecipeRequest request)
        {
            return _userService.IsRecipeFavorite(request);
        }

        // mark/unmark recipe as favorite for the user
        public MarkAsFavoriteResponse MarkAsFavorite(UserRecipeRequest request)
        {
            if (!Exists(request.RecipeId))
                return MarkAsFavoriteResponse.RecipeDoesNotExist;
            var recipe = _context.Recipes.FirstOrDefault(r => r.RecipeId == request.RecipeId);
            var isFavorite = _userService.IsRecipeFavorite(request);
            var user = _userService.GetUser();
            var appUser = _userService.GetUserByUserName(request.UserName);
            if (!user.Identity.IsAuthenticated)
                return MarkAsFavoriteResponse.UserIsNotLoggedIn;
            if(isFavorite is false)
            {
                //recipe.UsersWhoLikedThis.Add((Models.ApplicationUser)user.Identity);
                var result = _userService.AddRecipeToFavorites(recipe, request.UserName);
                if (result is true)
                {
                    recipe.UsersWhoLikedThis.Add(appUser);
                    return MarkAsFavoriteResponse.Marked;
                }
            }
            else
            {
                //recipe.UsersWhoLikedThis.Remove((Models.ApplicationUser)user.Identity);
                var result = _userService.RemoveRecipeFromFavorites(request.RecipeId, request.UserName);
                if (result is true)
                {
                    recipe.UsersWhoLikedThis.Remove(appUser);
                    return MarkAsFavoriteResponse.Unmarked;
                }
            }
            return MarkAsFavoriteResponse.CouldNotMark;
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

        //this method is not optimized at all. use the next one 

        public async Task<IEnumerable<RecipeEntity>> GetRecipesWithTagsAsync(IEnumerable<string> tags)
        {
            var entities = await _context.Recipes.AsNoTracking()
                //.Include(r => r.IngredientsWithQuantities)
                //.Include(r => r.RecipeSteps)
                //.Include(r => r.NutritionalValues)
                .Include(r => r.Tags).ToListAsync();
            var recipes = entities
                .Where(r => tags.All(t => r.Tags.Select(tt => tt.Name).Contains(t)));

            return recipes;
        }

        //use this method as it's more efficient than the other one
        public async Task<IEnumerable<RecipeEntity>> GetRecipesWithGivenTagsAsync(IEnumerable<string> tags)
        {
            if (tags is not null)
            {
                var recipesWithFirstTag = await GetRecipesWithGivenTagAsync(tags.First());
                if (tags.Count() > 1)
                {
                    var fullyFilteredList = recipesWithFirstTag.Where(r => tags.Skip(1)
                        .All(t => r.Tags.Select(tt => tt.Name).Contains(t)));
                    return fullyFilteredList;
                }
                return recipesWithFirstTag;
            }
            return new List<RecipeEntity>();
        }

        //used for one Tag only
        public async Task<IEnumerable<RecipeEntity>> GetRecipesWithGivenTagAsync(string tag)
        {
            var recipes = await _context.Tags.AsNoTracking()
                .Include(t => t.Recipes).ThenInclude(r => r.Tags)
                .Where(t => t.Name == tag)
                .SelectMany(t => t.Recipes).ToListAsync();
            return recipes;
        }

        public PagedList<RecipeEntity> GetPage(Parameters parameters)
        {
            List<RecipeEntity> recipes;
            if (string.IsNullOrWhiteSpace(parameters.Author))
                recipes = GetAll().ToList();
            else
                recipes = GetUserRecipes(parameters.Author).ToList();
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
                recipe = PrepareRecipeEntityForEF(recipe);
                
                recipe = UpdateChildren(recipe);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private RecipeEntity UpdateChildren(RecipeEntity recipeToUpdate)
        {
            var recipe = _context.Recipes.Where(r => r.RecipeId == recipeToUpdate.RecipeId)
                .Include(r => r.IngredientsWithQuantities)
                    .ThenInclude(iq => iq.Ingredient)
                    .ThenInclude(x => x.NutritionalValues)
                .Include(r => r.RecipeSteps)
                .Include(r => r.Tags)
                .Include(r => r.NutritionalValues)
                .SingleOrDefault();

            if(recipe is not null)
            {
                _context.Entry(recipe).CurrentValues.SetValues(recipeToUpdate);
                var anyIngredientChange = false;
                foreach (var tag in recipe.Tags)
                {
                    // if there are no matching tags in the original recipe, delete them
                    if (!recipeToUpdate.Tags.Any(t => t.TagId == tag.TagId))
                    {
                        recipe.Tags.Remove(tag);
                        //_context.Entry(tag).State = EntityState.Deleted;
                    }
                    else
                    {
                        recipeToUpdate.Tags.RemoveWhere(x => x.Name == tag.Name);
                    }
                }
                // add tags from recipeToUpdate.Tags
                //foreach(var tt in recipeToUpdate.Tags.Except(recipe.Tags))
                //{
                //    recipe.Tags.Add(tt);
                //}
                recipe.Tags.UnionWith(recipeToUpdate.Tags);
                foreach (var iq in recipe.IngredientsWithQuantities)
                {
                    if (!recipeToUpdate.IngredientsWithQuantities.Any(i => i.Ingredient.IngredientId == iq.Ingredient.IngredientId))
                    {
                        recipe.IngredientsWithQuantities.Remove(iq);
                        //_context.Entry(iq).State = EntityState.Deleted;
                        anyIngredientChange = true;
                    }
                    else
                    {
                        if (iq.Quantity != recipeToUpdate.IngredientsWithQuantities.FirstOrDefault(x => x.Ingredient.IngredientId == iq.Ingredient.IngredientId).Quantity)
                        {
                            iq.Quantity = recipeToUpdate.IngredientsWithQuantities.FirstOrDefault(x => x.Ingredient.IngredientId == iq.Ingredient.IngredientId).Quantity;
                            //_context.Entry(iq).State = EntityState.Modified;
                            anyIngredientChange = true;
                        }
                        var duplicate = recipeToUpdate.IngredientsWithQuantities.FirstOrDefault(x => x.Ingredient.IngredientId == iq.Ingredient.IngredientId);
                        recipeToUpdate.IngredientsWithQuantities.Remove(duplicate);
                    }
                }
                recipe.IngredientsWithQuantities.UnionWith(recipeToUpdate.IngredientsWithQuantities);
                //foreach (var ii in recipeToUpdate.IngredientsWithQuantities)
                //{
                //    if(recipe.IngredientsWithQuantities.Any(x => x.Ingredient.IngredientId == ii.Ingredient.IngredientId))
                //        recipe.IngredientsWithQuantities.Add(ii);
                //}
                foreach (var rs in recipe.RecipeSteps)
                {
                    if (!recipeToUpdate.RecipeSteps.Any(r => r.StepId == rs.StepId))
                    {
                        recipe.RecipeSteps.Remove(rs);
                        //_context.Entry(rs).State = EntityState.Deleted;
                    }
                    else
                    {
                        var recipeStepUpdated = recipeToUpdate.RecipeSteps.FirstOrDefault(x => x.StepId == rs.StepId);
                        if (rs.StepTitle != recipeStepUpdated.StepTitle)
                        {
                            rs.StepTitle = recipeStepUpdated.StepTitle;
                            //_context.Entry(rs).State = EntityState.Modified;
                        }
                        if(rs.StepDescription != recipeStepUpdated.StepDescription)
                        {
                            rs.StepDescription = recipeStepUpdated.StepDescription;
                            //_context.Entry(rs).State = EntityState.Modified;
                        }
                        var duplicate = recipeToUpdate.RecipeSteps.FirstOrDefault(x => x.StepId == rs.StepId);

                        recipeToUpdate.RecipeSteps.Remove(duplicate);
                    }
                }
                //recipe.RecipeSteps.Clear();
                recipe.RecipeSteps.AddRange(recipeToUpdate.RecipeSteps);
                if(anyIngredientChange is true)
                {
                    recipe.NutritionalValues = recipeToUpdate.NutritionalValues;
                    //_context.Entry(recipe.NutritionalValues).State = EntityState.Modified;
                }
                
            }
            return recipe;
        }

        private RecipeEntity PrepareRecipeEntityForEF(RecipeEntity recipeToPrepare)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == recipeToPrepare.Author);
            recipeToPrepare.User = user;
            recipeToPrepare.UserId = user.Id;
            var ii = new HashSet<IngredientWithQuantityEntity>(recipeToPrepare.IngredientsWithQuantities.AsEnumerable());
            recipeToPrepare.IngredientsWithQuantities.Clear();
            foreach (var i in ii)
            {
                if (_context.Ingredients.Any(x => x.IngredientId == i.Ingredient.IngredientId))
                {
                    var iq = new IngredientWithQuantityEntity()
                    {
                        Ingredient = _context.Ingredients.FirstOrDefault(x => x.IngredientId == i.Ingredient.IngredientId),
                        IngredientId = 0,
                        Recipe = recipeToPrepare,
                        RecipeId = 0,
                        Quantity = i.Quantity
                    };
                    recipeToPrepare.IngredientsWithQuantities.Add(iq);
                }
            }
            var tt = new HashSet<TagEntity>(recipeToPrepare.Tags.AsEnumerable());
            recipeToPrepare.Tags.Clear();
            foreach (var t in tt)
            {
                if (_context.Tags.Any(x => x.Name == t.Name))
                {
                    var rtt = _context.Tags.FirstOrDefault(x => x.Name == t.Name);
                    //var rt = new TagEntity()
                    //{
                    //    Name = t.Name,
                    //    TagId = _context.Tags.FirstOrDefault(x => x.Name == t.Name).TagId,
                    //    Recipes = _context.Tags.FirstOrDefault(x => x.Name == t.Name).Recipes
                    //};
                    recipeToPrepare.Tags.Add(rtt);
                }
                else
                {
                    recipeToPrepare.Tags.Add(t);
                }
            }
            return recipeToPrepare;
        }
    }
}
