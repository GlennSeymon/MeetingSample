using AutoMapper;
using MeetingSample.WebAPI.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeetingSample.WebAPI.Interface
{
	public interface IVenueService
    {
		Task<VenueVM> Get(IMapper mapper, int venueCode);
		Task<IEnumerable<VenueVM>> Get(IMapper mapper);
		void Update(IMapper mapper, VenueVM venueVM);
		Task Add(IMapper mapper, VenueVM venueVM);
	}
}

