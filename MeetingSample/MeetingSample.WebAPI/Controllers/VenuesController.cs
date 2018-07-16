using AutoMapper;
using MeetingSample.WebAPI.Interface;
using MeetingSample.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingSample.WebAPI.Controllers
{
	[Route("api/[controller]")]
    [ApiController]
    public class VenuesController : ControllerBase
    {
        private readonly MeetingSampleWebAPIContext context;
		private readonly IVenueService venueService;
		private readonly IMapper mapper;

		public VenuesController(MeetingSampleWebAPIContext context)
        {
            this.context = context;
        }

        // GET: api/Venues
        [HttpGet]
        public IEnumerable<Venue> GetVenues()
        {
            return this.context.Venues;
        }

        // GET: api/Venues/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVenue([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var venue = await this.context.Venues.FindAsync(id);

            if (venue == null)
                return NotFound();

            return Ok(venue);
        }

        // PUT: api/Venues/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVenue([FromRoute] int id, [FromBody] Venue venue)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != venue.VenueCode)
                return BadRequest();

            this.context.Entry(venue).State = EntityState.Modified;

            try
            {
                await this.context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VenueExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // POST: api/Venues
        [HttpPost]
        public async Task<IActionResult> PostVenue([FromBody] Venue venue)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            this.context.Venues.Add(venue);
            await this.context.SaveChangesAsync();

            return CreatedAtAction("GetVenue", new { id = venue.VenueCode }, venue);
        }

        // DELETE: api/Venues/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVenue([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var venue = await this.context.Venues.FindAsync(id);
            if (venue == null)
                return NotFound();

            this.context.Venues.Remove(venue);
            await this.context.SaveChangesAsync();

            return Ok(venue);
        }

        private bool VenueExists(int id)
        {
            return this.context.Venues.Any(e => e.VenueCode == id);
        }
    }
}