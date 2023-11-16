using Microsoft.AspNetCore.Mvc;
using WebApiDapper.Models;
using WebApiDapper.Repositories;

namespace WebApiDapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly ISuperHeroRepository _superHeroRepository;

        public SuperHeroController(ISuperHeroRepository superHeroRepository)
        {
            _superHeroRepository = superHeroRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<SuperHero>> GetAll()
        {
            return await _superHeroRepository.GetSuperHeroes();
        }

        [HttpGet("getById/{id}")]
        public async Task<ActionResult<SuperHero>> GetById(int id)
        {
            return await _superHeroRepository.GetSuperHero(id);
        }

        [HttpPost]
        public async Task<ActionResult<SuperHero>> Post([FromBody] SuperHero hero)
        {
            await _superHeroRepository.Add(hero);
            return hero;
        }

        [HttpPut]
        public async Task<ActionResult<SuperHero>> Put([FromBody] SuperHero hero)
        {
            var heroToUpdate = await _superHeroRepository.GetSuperHero(hero.Id);
            if (heroToUpdate == null) return NotFound();

            await _superHeroRepository.Update(hero);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<SuperHero>> Delete(int id)
        {
            var heroToDelete = await _superHeroRepository.GetSuperHero(id);
            if (heroToDelete == null) return NotFound();

            await _superHeroRepository.Delete(id);
            return Ok();
        }
    }
}
