using Microsoft.AspNetCore.Mvc;
using project.Entity;
using project.Generics;
using project.Validators;

namespace project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ValidateModel]
    public abstract class AppDbController<TEntity, TRepository> : ControllerBase
        where TEntity : class, IEntity
        where TRepository : IGenericService<TEntity>
    {
        private readonly IGenericService<TEntity> _genericService;

        internal AppDbController(IGenericService<TEntity> genericService) => _genericService = genericService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TEntity>>> Get() => await _genericService.GetAll();

        [HttpGet("{id}")]
        public async Task<ActionResult<TEntity>> Get(int id)
        {
            var movie = await _genericService.Get(id);
            if (movie == null) return NotFound();
            return movie;
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, TEntity movie)
        {
            if (id != movie.Id)
                return BadRequest();
            await _genericService.Update(movie);
            return NoContent();
        }
        [HttpPost]
        public async Task<ActionResult<TEntity>> Post(TEntity movie)
        {
            await _genericService.Add(movie);
            return (movie);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<TEntity>> Delete(int id)
        {
            var movie = await _genericService.Delete(id);
            return movie;
        }
    }
}
