using AutoMapper;
using MeetingSample.WebAPI.Models;
using MeetingSample.WebAPI.Models.ViewModels;

namespace MeetingSample.WebAPI.Classes
{
	public class DomainProfile : Profile
    {
		public DomainProfile()
		{
			CreateMap<Venue, VenueVM>().ReverseMap();
			CreateMap<Meeting, MeetingVM>()
				.ForMember(dest => dest.VenueCode, opt => opt.MapFrom(src => src.Venue.VenueCode))
				.ForMember(dest => dest.StateCode, opt => opt.MapFrom(src => src.State.StateCode))
				.ReverseMap()
				/*
				.ForPath(s => s.Venue, opt => opt.MapFrom(src => src.VenueCode))
				.ForPath(s => s.State, opt => opt.MapFrom(src => src.StateCode))*/;

/*
			CreateMap<MeetingVM, Meeting>()
				.ForMember(dest => dest.Venue, opt => opt.MapFrom(src => src.VenueCode))
				.ForMember(dest => dest.State, opt => opt.MapFrom(src => src.StateCode));
*/
			CreateMap<Race, RaceVM>().ReverseMap();
		}
    }
}
