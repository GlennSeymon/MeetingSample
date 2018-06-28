using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingSample.WebAPI.Models
{
    public class State
    {
        [Key]
        public int StateCode { get; set; }
        public string DescShort { get; set; }
        public string DescLong { get; set; }
        public ICollection<Venue> Venues { get; set; }
    }
}
