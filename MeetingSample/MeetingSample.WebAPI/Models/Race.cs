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
        public int RaceNumber { get; set; }
        public string NameRaceFull { get; set; }
        public int DistanceRace { get; set; }
    }
}
