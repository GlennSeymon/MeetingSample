using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MeetingSample.WebAPI.Models
{
	public class Meeting
    {
        [Key]
		public int MeetCode { get; set; }
		public string Title { get; set; }
		public State State { get; set; }
		public DateTime MeetDate { get; set; }
		public Venue Venue { get; set; }
        public MeetingCategory MeetingCategory { get; set; }
        public virtual ICollection<Race> Races { get; set; }
	}
}
