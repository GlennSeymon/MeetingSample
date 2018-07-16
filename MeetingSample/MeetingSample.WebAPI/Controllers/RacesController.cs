using AutoMapper;
using MeetingSample.WebAPI.Interface;
using MeetingSample.WebAPI.Models;
using MeetingSample.WebAPI.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingSample.WebAPI.Controllers
{
	[Route("api/[controller]")]
    [ApiController]
    public class RacesController : ControllerBase
    {
        private readonly MeetingSampleWebAPIContext context;
		private readonly IRaceService raceService;
		private readonly IMapper mapper;

		public RacesController(MeetingSampleWebAPIContext context, IRaceService raceService, IMapper mapper)
        {
            this.context = context;
			this.raceService = raceService;
			this.mapper = mapper;
		}

        // GET: api/Races
        [HttpGet]
		public async Task<IEnumerable<RaceVM>> GetRaces()
		{
			return await this.raceService.Get(this.mapper);
		}

		// GET: api/Races/5
		[HttpGet("{id}")]
		public async Task<IActionResult> GetRace([FromRoute] int id)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var race = await this.raceService.Get(this.mapper, id);

			if (race == null)
				return NotFound();

			return Ok(race);
		}

		// PUT: api/Races/5
		[HttpPut("{id}")]
        public async Task<IActionResult> PutRace([FromRoute] int id, [FromBody] Race race)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != race.RaceCode)
                return BadRequest();

            this.context.Entry(race).State = EntityState.Modified;

            try
            {
                await this.context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RaceExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // POST: api/Races
        [HttpPost]
        public async Task<IActionResult> PostRace([FromBody] Race race)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            this.context.Races.Add(race);
            await this.context.SaveChangesAsync();

            return CreatedAtAction("GetRace", new { id = race.RaceCode }, race);
        }

        // DELETE: api/Races/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRace([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var race = await this.context.Races.FindAsync(id);
            if (race == null)
                return NotFound();

            this.context.Races.Remove(race);
            await this.context.SaveChangesAsync();

            return Ok(race);
        }

        private bool RaceExists(int id)
        {
            return this.context.Races.Any(e => e.RaceCode == id);
        }
    }
}