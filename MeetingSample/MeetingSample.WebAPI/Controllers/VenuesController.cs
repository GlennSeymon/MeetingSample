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
    public class VenuesController : ControllerBase
    {
        private readonly MeetingSampleWebAPIContext context;
		private readonly IVenueService venueService;
		private readonly IMapper mapper;

		public VenuesController(MeetingSampleWebAPIContext context, IVenueService venueService, IMapper mapper)
        {
            this.context = context;
			this.venueService = venueService;
			this.mapper = mapper;
        }

        // GET: api/Venues
        [HttpGet]
        public async Task<IEnumerable<VenueVM>> GetVenues()
        {
			return await this.venueService.Get(this.mapper);
		}

        // GET: api/Venues/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVenue([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

			var venue = await this.venueService.Get(this.mapper, id);

			if (venue == null)
                return NotFound();

            return Ok(venue);
        }

        // PUT: api/Venues/5
        [HttpPut("{id}")]
		public IActionResult PutVenue([FromRoute] int id, [FromBody] VenueVM venueVM)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			if (id != venueVM.VenueCode)
				return BadRequest();

			try
			{
				this.venueService.Update(this.mapper, venueVM);
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
        public async Task<IActionResult> PostVenue([FromBody] VenueVM venue)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

			await this.venueService.Add(this.mapper, venue);

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