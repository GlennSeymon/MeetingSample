using AutoMapper;
using MeetingSample.WebAPI.Interface;
using MeetingSample.WebAPI.Models;
using MeetingSample.WebAPI.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingSample.WebAPI.Services
{
	public class MeetingService : IMeetingService
    {
		private readonly MeetingSampleWebAPIContext context;

		public MeetingService(MeetingSampleWebAPIContext context)
		{
			this.context = context;
		}

		public async Task Add(IMapper mapper, MeetingVM meetingVM)
		{
			var meeting = mapper.Map<Meeting>(meetingVM);

			meeting.State = this.context.States.Find(meetingVM.StateCode);
			meeting.Venue = this.context.Venues.Find(meetingVM.VenueCode);

			this.context.Meetings.Add(meeting);
			await this.context.SaveChangesAsync();
		}

		public async Task<MeetingVM> Get(IMapper mapper, int meetCode)
        {
			var meeting = await (from m in this.context.Meetings.Include(m => m.Races).Include(m => m.Venue).Include(m => m.State)
								 where m.MeetCode == meetCode
								 select m).FirstOrDefaultAsync();

			return mapper.Map<MeetingVM>(meeting);
		}

		public async Task<IEnumerable<MeetingVM>> Get(IMapper mapper)
		{
			var meetings = await (from m in this.context.Meetings.Include(m => m.Races).Include(m => m.Venue).Include(m => m.State)
								 select m).ToListAsync();

			return mapper.Map<MeetingVM[]>(meetings);
		}

		public void Update(IMapper mapper, MeetingVM meetingVM)
		{
			var meeting = mapper.Map<Meeting>(meetingVM);

			meeting.State = this.context.States.Find(meetingVM.StateCode);
			meeting.Venue = this.context.Venues.Find(meetingVM.VenueCode);

			this.context.Entry(meeting).State = EntityState.Modified;
			this.context.SaveChangesAsync();
		}
	}
}
