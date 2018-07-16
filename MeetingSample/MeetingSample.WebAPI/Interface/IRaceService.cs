using AutoMapper;
using MeetingSample.WebAPI.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeetingSample.WebAPI.Interface
{
	public interface IRaceService
    {
		Task<RaceVM> Get(IMapper mapper, int raceCode);
		Task<IEnumerable<RaceVM>> Get(IMapper mapper);
	}
}

