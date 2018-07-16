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

		public async Task<VenueVM> Get(IMapper mapper, int venueCode)
        {
			var venue = await (from v in this.context.Venues.Include(v => v.Meetings)
								 where v.VenueCode == venueCode
								 select v).FirstOrDefaultAsync();

			return mapper.Map<VenueVM>(venue);
		}

		public async Task<IEnumerable<VenueVM>> Get(IMapper mapper)
		{
			var venues = await (from v in this.context.Venues.Include(v => v.Meetings)
								 select v).ToListAsync();

			return mapper.Map<VenueVM[]>(venues);
		}
	}
}
