using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingSample.WebAPI.Models
{
    public class Venue
    {
        [Key]
        public int VenueCode { get; set; }
        public string Name { get; set; }
        public State State { get; set; }
        public ICollection<Meeting> Meetings { get; set; }
    }
}
