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
    public class MeetingsController : ControllerBase
    {
        private readonly MeetingSampleWebAPIContext context;
		private readonly IMeetingService meetingService;
		private readonly IMapper mapper;

        public MeetingsController(MeetingSampleWebAPIContext context, IMeetingService meetingService, IMapper mapper)
        {
            this.context = context;
			this.meetingService = meetingService;
			this.mapper = mapper;
        }

        // GET: api/Meetings
        [HttpGet]
        public async Task<IEnumerable<MeetingVM>> GetMeetings()
        {
			return await this.meetingService.Get(this.mapper);
		}

		// GET: api/Meetings/1
		[HttpGet("{id}")]
		public async Task<IActionResult> GetMeeting([FromRoute] int id)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var meeting = await this.meetingService.Get(this.mapper, id);

			if (meeting == null)
				return NotFound();

			return Ok(meeting);
		}

		// PUT: api/Meetings/5
		[HttpPut("{id}")]
        public IActionResult PutMeeting([FromRoute] int id, [FromBody] MeetingVM meetingVM)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != meetingVM.MeetCode)
                return BadRequest();

            try
            {
				this.meetingService.Update(this.mapper, meetingVM);
			}
            catch (DbUpdateConcurrencyException)
            {
                if (!MeetingExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // POST: api/Meetings
        [HttpPost]
        public async Task<IActionResult> PostMeeting([FromBody] MeetingVM meetingVM)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

			await this.meetingService.Add(this.mapper, meetingVM);

            return CreatedAtAction("GetMeeting", new { id = meetingVM.MeetCode }, meetingVM);
        }

        // DELETE: api/Meetings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMeeting([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var meeting = await this.context.Meetings.FindAsync(id);
            if (meeting == null)
                return NotFound();

            this.context.Meetings.Remove(meeting);
            await this.context.SaveChangesAsync();

            return Ok(meeting);
        }

        private bool MeetingExists(int id)
        {
            return this.context.Meetings.Any(e => e.MeetCode == id);
        }
    }
}