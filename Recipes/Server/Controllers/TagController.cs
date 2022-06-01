using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Recipes.Server.Data.Seed;
using Recipes.Server.Models.Entities;
using Recipes.Server.Services.Interfaces;
using Recipes.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Recipes.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly IRepository<TagEntity, int> _service;
        private readonly IMapper _mapper;

        private readonly PopulateIngredientsTableService tableService;

        public TagController(IRepository<TagEntity, int> service,
            PopulateIngredientsTableService tableService, IMapper mapper)
        {
            _service = service;
            this.tableService = tableService;
            _mapper = mapper;
        }

        // GET: api/Tag
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TagEntity>>> GetTagsAsync()
        {
            //tableService.Populate();
            return Ok(_mapper.Map<IEnumerable<TagEdit>>(await _service.GetAll().ToListAsync()));
        }

        // GET: api/Tag/5
        [HttpGet("{id}")]
        public ActionResult<Tag> GetTag(int id)
        {
            var tag = _service.GetById(id);

            if (tag is null)
                return NotFound();
            return Ok(_mapper.Map<Tag>(tag));
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
