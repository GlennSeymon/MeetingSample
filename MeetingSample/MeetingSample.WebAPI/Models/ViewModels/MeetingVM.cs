using System;
using System.Collections.Generic;

namespace MeetingSample.WebAPI.Models.ViewModels
{
	public class MeetingVM
    {
		public int MeetCode { get; set; }
		public DateTime MeetDate { get; set; }
		public string Title { get; set; }
		public int StateCode { get; set; }
		public string StateDescLong { get; set; }
		public int VenueCode { get; set; }
		public string VenueName { get; set; }
		public IEnumerable<RaceVM> Races { get; set; }
	}
}
