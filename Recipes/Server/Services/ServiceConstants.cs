using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recipes.Server.Services
{
    public static class ServiceConstants
    {
        public enum MarkAsFavoriteResponse
        {
            RecipeDoesNotExist,
            UserIsNotLoggedIn,
            Marked,
            Unmarked,
            CouldNotMark
        }
    }
}
