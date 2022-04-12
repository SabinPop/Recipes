using Microsoft.AspNetCore.Mvc;
using Recipes.Server.Models.Entities;
using Recipes.Server.Services.Interfaces;
using System.Collections.Generic;

namespace Recipes.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagService _service;

        public TagController(ITagService service)
        {
            _service = service;
        }

        // GET: api/Tag
        [HttpGet]
        public ActionResult<IEnumerable<TagEntity>> GetTags()
        {
            return Ok(_service.GetAllTags());
        }

        // GET: api/Tag/5
        [HttpGet("{id}")]
        public ActionResult<TagEntity> GetTag(int id)
        {
            var tag = _service.GetTagById(id);

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
            var result = _service.UpdateTag(tag);
            if (result)
                return Ok(tag);
            return NotFound();
        }

        // POST: api/Tag
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<TagEntity> PostTag(TagEntity tag)
        {
            var result = _service.CreateTag(tag);
            if (result)
                return CreatedAtAction("GetTag", new { id = tag.TagId }, tag);
            return BadRequest();
        }

        // DELETE: api/Tag/5
        [HttpDelete("{id}")]
        public IActionResult DeleteTag(int id)
        {
            var result = _service.DeleteTag(id);
            if (result)
                return Ok();
            return NotFound();
        }
    }
}
