using AutoMapper;
using MeetingSample.WebAPI.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeetingSample.WebAPI.Interface
{
	public interface IMeetingService
    {
		Task<MeetingVM> Get(IMapper mapper, int meetCode);
		Task<IEnumerable<MeetingVM>> Get(IMapper mapper);
		void Update(IMapper mapper, MeetingVM meetingVM);
		Task Add(IMapper mapper, MeetingVM meetingVM);
	}
}

