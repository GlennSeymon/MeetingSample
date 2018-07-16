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
	public class RaceService : IRaceService
    {
		private readonly MeetingSampleWebAPIContext context;

		public RaceService(MeetingSampleWebAPIContext context)
		{
			this.context = context;
		}

		public async Task<RaceVM> Get(IMapper mapper, int raceCode)
        {
			var race = await (from m in this.context.Races
								 where m.RaceCode == raceCode
								 select m).FirstOrDefaultAsync();

			return mapper.Map<RaceVM>(race);
		}

		public async Task<IEnumerable<RaceVM>> Get(IMapper mapper)
		{
			var races = await (from m in this.context.Races
								 select m).ToListAsync();

			return mapper.Map<RaceVM[]>(races);
		}
	}
}
