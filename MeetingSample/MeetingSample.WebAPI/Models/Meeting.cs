using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingSample.WebAPI.Models
{
    public class Meeting
    {
        [Key]
        public int MeetCode { get; set; }
        public DateTime MeetDate { get; set; }
        public string MeetTitle { get; set; }
        public IEnumerable<Race> Races { get; set; }
    }
}
