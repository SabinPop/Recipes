using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientController : ControllerBase
    {
        private readonly IRepository<IngredientEntity, int> _service;
        private readonly IMapper _mapper;

        public IngredientController(IRepository<IngredientEntity, int> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Parameters parameters)
        {
            if (parameters.Pagination == false)
            {
                var ingredients = await _service.GetAll().ToListAsync();
                return Ok(ingredients);
            }
            else
            {
                var ingredients = _service.GetPage(parameters);

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(ingredients.MetaData));

                return Ok(ingredients);
            }
        }

        /*
         * 
        // GET: api/Ingredient
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ingredient>>> GetIngredients()
        {
            var x = _mapper.Map<IEnumerable<Ingredient>>(_service.GetAllIngredients());
            return Ok(x);
        }

        */

        // GET: api/Ingredient/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IngredientEntity>> GetIngredient(int id)
        {
            var ingredient = _service.GetById(id);
            if (ingredient is null)
                return NotFound();
            return Ok(ingredient);
        }

        [HttpGet("q/")]
        public ActionResult<IEnumerable<IngredientEntity>> GetIngredientsThatStartWithString([FromQuery] string name, [FromQuery] int id)
        {
            var ingredients = _service.GetStartingWithString(name);
            return Ok(ingredients);
        }

        // PUT: api/Ingredient/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutIngredient(IngredientEntity ingredient)
        {
            var isUpdated = _service.Update(ingredient);

            if (isUpdated)
                return Ok(ingredient);
            else
                return NotFound();
        }

        // POST: api/Ingredient
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<IngredientEntity> PostIngredient(IngredientEntity ingredient)
        {
            var isCreated = _service.Create(ingredient);
            if (isCreated)
                return CreatedAtAction("GetIngredient", new { id = ingredient.IngredientId }, ingredient);
            else
                return BadRequest();
        }

        // DELETE: api/Ingredient/5
        [HttpDelete("{id}")]
        public IActionResult DeleteIngredient(int id)
        {
            var isDeleted = _service.Delete(id);
            if (isDeleted)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }

        }
    }
}
