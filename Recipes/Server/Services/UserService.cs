using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Recipes.Server.Data;
using Recipes.Server.Models;
using Recipes.Server.Models.Entities;
using Recipes.Server.Services.Interfaces;
using Recipes.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Recipes.Server.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _accessor;

        public UserService(ApplicationDbContext context, IHttpContextAccessor accessor)
        {
            _context = context;
            _accessor = accessor;
        }

        public ClaimsPrincipal GetUser()
        {
            return _accessor.HttpContext.User;

        }
        public string GetUserName()
        {
            return _accessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
        }

        public bool Exists(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return false;
            return _context.Users.AsNoTracking().Any(u => u.NormalizedUserName == username.ToUpper());
        }

        public ApplicationUser GetUserByUserName(string username)
        {
            if (Exists(username))
                return _context.Users.AsNoTracking()
                    .Include(u => u.FavoriteRecipes)
                    .Include(u => u.UserRecipes)
                    .FirstOrDefault(u => u.NormalizedUserName == username.ToUpper());
            return null;
        }

        public bool HasPublishedRecipes(string username)
        {
            if (!Exists(username))
                return false;
            return _context.Users.AsNoTracking()
                .Include(u => u.UserRecipes)
                .FirstOrDefault(u => u.NormalizedUserName == username.ToUpper())
                .UserRecipes.Any();
        }

        public bool HasFavorites(string username)
        {
            if (!Exists(username))
                return false;
            return _context.Users.AsNoTracking()
                .Include(u => u.FavoriteRecipes)
                .FirstOrDefault(u => u.NormalizedUserName == username.ToUpper())
                .FavoriteRecipes.Any();
        }

        public List<RecipeEntity> GetUserFavoriteRecipes(string username)
        {
            var user = GetUser();
            if (user.Identity.IsAuthenticated)
            {
                if (HasFavorites(username))
                    return _context.Users.AsNoTracking()
                        .Include(u => u.FavoriteRecipes)
                            .ThenInclude(r => r.Tags)
                        .FirstOrDefault(u => u.UserName == username)
                        .FavoriteRecipes.ToList();
            }
            return new List<RecipeEntity>();
        }

        public bool IsRecipeFavorite(UserRecipeRequest request)
        {
            //var user = GetUser();
            //if (user.Identity.IsAuthenticated)
            //{
                return _context.Users.AsNoTracking()
                    .Include(u => u.FavoriteRecipes)
                    .FirstOrDefault(u => u.UserName == request.UserName)
                    .FavoriteRecipes.Any(r => r.RecipeId == request.RecipeId);
            //}
            // return false;
        }

        public bool AddRecipeToFavorites(RecipeEntity recipe, string username)
        {
            var user = GetUser();
            if (user.Identity.IsAuthenticated)
            {
                try
                {
                    _context.Users.Include(u => u.FavoriteRecipes)
                        .FirstOrDefault(u => u.UserName == username)
                        .FavoriteRecipes.Add(recipe);
                    _context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {

                }
            }
            return false;
        }

        public bool RemoveRecipeFromFavorites(int recipeId, string username)
        {
            var user = GetUser();
            if (user.Identity.IsAuthenticated)
            {
                try
                {
                    _context.Users.Include(u => u.FavoriteRecipes)
                        .FirstOrDefault(u => u.UserName == username)
                        .FavoriteRecipes.RemoveWhere(r => r.RecipeId == recipeId);
                    _context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {

                }
            }
            return false;
        }
    }
}
