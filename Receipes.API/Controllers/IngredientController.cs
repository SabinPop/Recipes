using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Recipes.API.Data;
using Recipes.API.Models.Entities;
using Recipes.API.Services.Interfaces;

namespace Recipes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientController : ControllerBase
    {
        private readonly IIngredientService _service;

        public IngredientController(IIngredientService service)
        {
            _service = service;
        }

        // GET: api/Ingredient
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IngredientEntity>>> GetIngredients()
        {
            return await _service.GetAllIngredients().ToListAsync();
        }

        // GET: api/Ingredient/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IngredientEntity>> GetIngredient(int id)
        {
            var ingredient = await _service.GetIngredientByIdAsync(id);
            if (ingredient is null)
                return NotFound();
            return Ok(ingredient);
        }

        // PUT: api/Ingredient/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutIngredient(IngredientEntity ingredient)
        {
            if (_service.Exists(ingredient.IngredientId) == false)
                return BadRequest();

            var isUpdated = _service.UpdateIngredient(ingredient);

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
            var isCreated = _service.CreateIngredient(ingredient);
            if (isCreated)
                return CreatedAtAction("GetIngredient", new { id = ingredient.IngredientId }, ingredient);
            else
                return BadRequest();
        }

        // DELETE: api/Ingredient/5
        [HttpDelete("{id}")]
        public IActionResult DeleteIngredient(int id)
        {
            var isDeleted = _service.DeleteIngredient(id);
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
