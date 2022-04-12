using Microsoft.AspNetCore.Mvc;
using Recipes.Server.Services.Interfaces;
using Recipes.Server.Models.Entities;
using System.Collections.Generic;

namespace Recipes.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeService _service;

        public RecipeController(IRecipeService service)
        {
            _service = service;
        }

        // GET: api/Recipe
        [HttpGet]
        public ActionResult<IEnumerable<RecipeEntity>> GetRecipes()
        {
            return Ok(_service.GetAllRecipes());
        }

        // GET: api/Recipe/5
        [HttpGet("{id}")]
        public ActionResult<RecipeEntity> GetRecipe(int id)
        {
            if (_service.Exists(id) == false)
                return NotFound();
            return Ok(_service.GetRecipeById(id));
        }

        // PUT: api/Recipe/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutRecipe(RecipeEntity recipe)
        {
            var result = _service.UpdateRecipe(recipe);
            if (result)
                return Ok(recipe);
            return NotFound();
        }

        // POST: api/Recipe
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<RecipeEntity> PostRecipe(RecipeEntity recipe)
        {
            var result = _service.CreateRecipe(recipe);
            if (result)
                return CreatedAtAction("GetRecipe", new { id = recipe.RecipeId }, recipe);
            return BadRequest();
        }

        // DELETE: api/Recipe/5
        [HttpDelete("{id}")]
        public IActionResult DeleteRecipe(int id)
        {
            var result = _service.DeleteRecipe(id);
            if (result)
                return Ok();
            return NotFound();
        }
    }
}
