using AutoMapper;
using MeetingSample.WebAPI.Models;
using MeetingSample.WebAPI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingSample.WebAPI.Classes
{
    public class DomainProfile : Profile
    {
		public DomainProfile()
		{
			CreateMap<Venue, VenueVM>();
			//CreateMap<Meeting, MeetingVM>().ForMember(dest => dest.VenueName, opt => opt.MapFrom(src => src.Venue.Name)); 
			CreateMap<Meeting, MeetingVM>();
			CreateMap<Race, RaceVM>();
		}
    }
}
