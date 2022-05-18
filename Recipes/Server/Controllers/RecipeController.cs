using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Recipes.Server.Models.Entities;
using Recipes.Server.Services.Interfaces;
using Recipes.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Recipes.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IRepository<RecipeEntity, int> _service;
        private readonly IMapper _mapper;

        public RecipeController(IRepository<RecipeEntity, int> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Parameters parameters)
        {
            if (parameters.Pagination == false)
            {
                var recipes = _mapper.Map<List<RecipeDetails>>(_service.GetAll());
                return Ok(recipes);
            }
            else
            {
                var recipes = _service.GetPage(parameters);

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(recipes.MetaData));

                return Ok(recipes);
            }
        }

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
        public ActionResult<RecipeEntity> GetRecipe(int id)
        {
            if (_service.Exists(id) == false)
                return NotFound();
            return Ok(
                _mapper.Map<RecipeView>(
                _service.GetById(id)));
        }

        // PUT: api/Recipe/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutRecipe(RecipeEdit recipe)
        {
            var result = _service.Update(_mapper.Map<RecipeEntity>(recipe));
            if (result)
                return Ok(recipe);
            return NotFound();
        }

        // POST: api/Recipe
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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
        [HttpDelete("{id}")]
        public IActionResult DeleteRecipe(int id)
        {
            var result = _service.Delete(id);
            if (result)
                return Ok();
            return NotFound();
        }
    }
}
