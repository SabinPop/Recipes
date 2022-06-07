using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Recipes.Server.Models;
using Recipes.Server.Models.Entities;
using Recipes.Server.Services;
using Recipes.Server.Services.Interfaces;
using Recipes.Shared.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recipes.Server.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IRepository<RecipeEntity, int> _service;
        private readonly IUserService _userService;
        private readonly UserManager<ApplicationUser> manager;
        private readonly IMapper _mapper;

        public RecipeController(IRepository<RecipeEntity, int> service, IMapper mapper, IUserService userService, UserManager<ApplicationUser> manager)
        {
            _service = service;
            _mapper = mapper;
            _userService = userService;
            this.manager = manager;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Parameters parameters)
        {
            if (parameters.Pagination == false)
            {
                List<RecipeDetails> recipes;
                if (string.IsNullOrWhiteSpace(parameters.Author) && string.IsNullOrWhiteSpace(parameters.UserName))
                {
                    // user is not logged in
                    // requests all the recipes
                    recipes = _mapper.Map<IEnumerable<RecipeDetails>>(_service.GetAll()).ToList();
                    return Ok(recipes);
                }
                else if (string.IsNullOrWhiteSpace(parameters.UserName))
                {
                    // user is not loggged in
                    // requests an author's recipes 
                    recipes = _mapper.Map<IEnumerable<RecipeDetails>>((_service as RecipeService).GetUserRecipes(parameters.Author)).ToList();
                    return Ok(recipes);
                }
                else if (string.IsNullOrWhiteSpace(parameters.Author))
                {
                    // user is loggged in
                    // requests an author's recipes
                    // inject user favorites
                    recipes = _mapper.Map<IEnumerable<RecipeDetails>>(_service.GetAll()).InjectFavorite(_userService, parameters.UserName).ToList();
                    return Ok(recipes);
                }
                
                if (parameters.Author.Equals(parameters.UserName))
                {
                    // user is logged in
                    // requests his recipes
                    // inject user favorites
                    recipes = _mapper.Map<IEnumerable<RecipeDetails>>((_service as RecipeService).GetUserRecipes(parameters.UserName)).InjectFavorite(_userService, parameters.UserName).ToList();
                }
                else
                {
                    // user is logged in
                    // requests other's author recipes
                    // inject user favorite
                    recipes = _mapper.Map<IEnumerable<RecipeDetails>>((_service as RecipeService).GetUserRecipes(parameters.Author)).InjectFavorite(_userService, parameters.UserName).ToList();
                }
                return Ok(recipes);
            }
            else
            {
                var pagedRecipes = _service.GetPage(parameters);

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(pagedRecipes.MetaData));

                return Ok(pagedRecipes);
            }
        }

        [Authorize]
        [HttpGet("my/favorites")]
        public async Task<IActionResult> GetUserFavorites([FromQuery] string username)
        {
            var recipes = _mapper.Map<IEnumerable<RecipeDetails>>((_service as RecipeService).GetUserFavorites(username)).InjectFavorite(_userService, username);
            return Ok(recipes);
        }

        [HttpGet("by/{author}")]
        public async Task<IActionResult> GetRecipesByAuthor(string author)
        {
            var recipes = _mapper.Map<IEnumerable<RecipeDetails>>((_service as RecipeService).GetUserRecipes(author));
            return Ok(recipes);
        }

        [Authorize]
        [HttpPost("mark")]
        public async Task<IActionResult> MarkRecipe([FromBody] UserRecipeRequest request)
        {
            var result = (_service as RecipeService).MarkAsFavorite(request);

            switch (result)
            {
                case ServiceConstants.MarkAsFavoriteResponse.Marked:
                    return Ok("marked");
                case ServiceConstants.MarkAsFavoriteResponse.Unmarked:
                    return Ok("unmarked");
                case ServiceConstants.MarkAsFavoriteResponse.RecipeDoesNotExist:
                    return NotFound(request.RecipeId);
                case ServiceConstants.MarkAsFavoriteResponse.UserIsNotLoggedIn:
                    return Unauthorized();
                case ServiceConstants.MarkAsFavoriteResponse.CouldNotMark:
                    return BadRequest();
                default:
                    return BadRequest();
            }
            
        }

        //[httppost("isfavorite/{isfavorite}")]
        //public async task<iactionresult>



        /*
         * 
        // GET: api/Recipe
        [HttpGet]
        public ActionResult<IEnumerable<RecipeEntity>> GetRecipes()
        {
            return Ok(_service.GetAll());
        }
        *
        */

        // GET: api/Recipe/5
        [HttpGet("{id}")]
        public ActionResult<RecipeView> GetRecipe(int id)
        {
            if (_service.Exists(id) == false)
                return NotFound();
            return Ok(
                _mapper.Map<RecipeView>(
                _service.GetById(id)));
        }

        //GET: api/Recipe/tags
        [HttpPost("tags")]
        public async Task<ActionResult<IEnumerable<RecipeDetails>>> GetRecipesWithGivenTags(IEnumerable<string> tags)
        {
            var recipes = await (_service as RecipeService).GetRecipesWithGivenTagsAsync(tags);
            var rds = _mapper.Map<IEnumerable<RecipeDetails>>(recipes);
            return Ok(rds);
        }

        // PUT: api/Recipe/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Policy = "EditDeletePolicy")]
        [HttpPut("{id}")]
        public IActionResult PutRecipe(RecipeEdit recipe)
        {
            if (User.Identity.Name == recipe.Author)
            {
                var result = _service.Update(_mapper.Map<RecipeEntity>(recipe));
                if (result)
                    return Ok(recipe);

                return NotFound();
            }
            return Unauthorized();
        }

        // POST: api/Recipe
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPost]
        public IActionResult PostRecipe(RecipeCreate recipe)
        {
            var r = _mapper.Map<RecipeEntity>(recipe);
            //get recipe json 
            var json = JsonConvert.SerializeObject(recipe);
            var result = _service.Create(r);
            if (result)
            {
                var recipeView = _mapper.Map<RecipeView>(r);
                return CreatedAtAction(nameof(GetRecipe), new { id = _mapper.Map<RecipeView>(r).RecipeId }, recipeView);
            }
            return BadRequest();
        }

        // DELETE: api/Recipe/5
        [Authorize(Policy = "EditDeletePolicy")]
        [HttpDelete("{id}")]
        public IActionResult DeleteRecipe(int id)
        {
            if (User.Identity.Name == _service.GetById(id).Author)
            {
                var result = _service.Delete(id);
                if (result)
                    return Ok();
                return NotFound();
            }
            return Unauthorized();
        }
    }
}
