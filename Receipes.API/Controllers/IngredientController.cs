using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Recipes.API.Data;
using Recipes.API.Models.Entities;

namespace Recipes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientController : ControllerBase
    {
        private readonly RecipesDbContext _context;

        public IngredientController(RecipesDbContext context)
        {
            _context = context;
        }

        // GET: api/Ingredient
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IngredientEntity>>> GetIngredientEntities()
        {
            return await _context.IngredientEntities.ToListAsync();
        }

        // GET: api/Ingredient/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IngredientEntity>> GetIngredientEntity(int id)
        {
            var ingredientEntity = await _context.IngredientEntities.FindAsync(id);

            if (ingredientEntity == null)
            {
                return NotFound();
            }

            return ingredientEntity;
        }

        // PUT: api/Ingredient/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIngredientEntity(int id, IngredientEntity ingredientEntity)
        {
            if (id != ingredientEntity.IngredientId)
            {
                return BadRequest();
            }

            _context.Entry(ingredientEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IngredientEntityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Ingredient
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<IngredientEntity>> PostIngredientEntity(IngredientEntity ingredientEntity)
        {
            _context.IngredientEntities.Add(ingredientEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIngredientEntity", new { id = ingredientEntity.IngredientId }, ingredientEntity);
        }

        // DELETE: api/Ingredient/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngredientEntity(int id)
        {
            var ingredientEntity = await _context.IngredientEntities.FindAsync(id);
            if (ingredientEntity == null)
            {
                return NotFound();
            }

            _context.IngredientEntities.Remove(ingredientEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IngredientEntityExists(int id)
        {
            return _context.IngredientEntities.Any(e => e.IngredientId == id);
        }
    }
}
