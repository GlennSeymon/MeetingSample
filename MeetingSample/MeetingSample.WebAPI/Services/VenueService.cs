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
	public class VenueService : IVenueService
    {
		private readonly MeetingSampleWebAPIContext context;

		public VenueService(MeetingSampleWebAPIContext context)
		{
			this.context = context;
		}

		public async Task Add(IMapper mapper, VenueVM venueVM)
		{
			var venue = mapper.Map<Venue>(venueVM);

			venue.State = this.context.States.Find(venueVM.StateCode);

			this.context.Venues.Add(venue);
			await this.context.SaveChangesAsync();
		}

		public async Task<VenueVM> Get(IMapper mapper, int venueCode)
        {
			var venue = await (from v in this.context.Venues.Include(v => v.Meetings).Include(v => v.State)
								 where v.VenueCode == venueCode
								 select v).FirstOrDefaultAsync();

			return mapper.Map<VenueVM>(venue);
		}

		public async Task<IEnumerable<VenueVM>> Get(IMapper mapper)
		{
			var venues = await (from v in this.context.Venues.Include(v => v.State)
								select v).ToListAsync();

			return mapper.Map<VenueVM[]>(venues);
		}

		public void Update(IMapper mapper, VenueVM venueVM)
		{
			var venue = mapper.Map<Venue>(venueVM);

			venue.State = this.context.States.Find(venueVM.StateCode);

			this.context.Entry(venue).State = EntityState.Modified;
			this.context.SaveChangesAsync();
		}
	}
}
