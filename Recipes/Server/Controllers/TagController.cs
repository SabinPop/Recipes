using Microsoft.AspNetCore.Mvc;
using Recipes.Server.Data.Seed;
using Recipes.Server.Models.Entities;
using Recipes.Server.Services.Interfaces;
using System.Collections.Generic;

namespace Recipes.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly IRepository<TagEntity, int> _service;


        private readonly PopulateIngredientsTableService tableService;

        public TagController(IRepository<TagEntity, int> service, PopulateIngredientsTableService tableService)
        {
            _service = service;
            this.tableService = tableService;
        }

        // GET: api/Tag
        [HttpGet]
        public ActionResult<IEnumerable<TagEntity>> GetTags()
        {
            //tableService.Populate();
            return Ok(_service.GetAll());
        }

        // GET: api/Tag/5
        [HttpGet("{id}")]
        public ActionResult<TagEntity> GetTag(int id)
        {
            var tag = _service.GetById(id);

            if (tag is null)
                return NotFound();
            return Ok(tag);
        }

        // PUT: api/Tag/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutTag(TagEntity tag)
        {
            if (tag is null)
                return BadRequest();
            var result = _service.Update(tag);
            if (result)
                return Ok(tag);
            return NotFound();
        }

        // POST: api/Tag
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<TagEntity> PostTag(TagEntity tag)
        {
            var result = _service.Create(tag);
            if (result)
                return CreatedAtAction("GetTag", new { id = tag.TagId }, tag);
            return BadRequest();
        }

        // DELETE: api/Tag/5
        [HttpDelete("{id}")]
        public IActionResult DeleteTag(int id)
        {
            var result = _service.Delete(id);
            if (result)
                return Ok();
            return NotFound();
        }
    }
}
