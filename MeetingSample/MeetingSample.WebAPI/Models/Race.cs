using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingSample.WebAPI.Models
{
    public class Race
    {
        [Key]
		public int RaceCode { get; set; }
		public int MeetCode { get; set; }
		public int RaceNumber { get; set; }
		public string Name { get; set; }
		public int Distance { get; set; }
	}
}
