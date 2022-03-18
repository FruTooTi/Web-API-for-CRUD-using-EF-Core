using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tangy_DataAccess;
using Tangy_DataAccess.Data;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        public ApplicationDbContext _Context;
        public SuperHeroController(ApplicationDbContext Context)
        {
            _Context = Context;
        }
        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            return Ok(await _Context.SuperHeroes.ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            var hero = await _Context.SuperHeroes.FindAsync(id);
            if (hero == null)
                return BadRequest("Hero Not Found.");
            return Ok(hero);
        }
        [HttpPost]
        public async Task<IActionResult> Post(SuperHero hero)
        {
            await _Context.SuperHeroes.AddAsync(hero);
            await _Context.SaveChangesAsync();
            return Ok(await _Context.SuperHeroes.ToListAsync());
        }
        [HttpPut]
        public async Task<IActionResult> put(SuperHero request)
        {
            var hero = _Context.SuperHeroes.FirstOrDefault(h => h.Id == request.Id);
            if (hero == null)
                return BadRequest("hero not found.");
            //hero = request;
            hero.Name = request.Name;
            hero.FirstName = request.FirstName;
            hero.LastName = request.LastName;
            hero.Place = request.Place;
            await _Context.SaveChangesAsync();
            return Ok(await _Context.SuperHeroes.ToListAsync());
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> Delete(int id)
        {
            var hero = _Context.SuperHeroes.FirstOrDefault(h => h.Id == id);
            if (hero == null)
                return BadRequest("Hero Not Found.");
            _Context.SuperHeroes.Remove(hero);
            await _Context.SaveChangesAsync();
            return Ok(await _Context.SuperHeroes.ToListAsync());
        }
    }
}
