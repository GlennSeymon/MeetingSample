using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingSample.WebAPI.Models
{
    public class Meeting
    {
        [Key]
		public int MeetCode { get; set; }
		public string Title { get; set; }
		public string StateDesc { get; set; }
		public DateTime MeetDate { get; set; }
		public int VenueCode { get; set; }
		public string MeetCatArea { get; set; }
		public int MeetCatCode { get; set; }
        public ICollection<Race> Races { get; set; }
	}
}
