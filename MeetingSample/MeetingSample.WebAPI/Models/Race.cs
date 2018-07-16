using System.ComponentModel.DataAnnotations;

namespace MeetingSample.WebAPI.Models
{
	public class Race
    {
        [Key]
		public int RaceCode { get; set; }
        public Meeting Meeting { get; set; }
        public int RaceNumber { get; set; }
		public string Name { get; set; }
		public int Distance { get; set; }
    }
}
