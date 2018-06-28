using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingSample.WebAPI.Models
{
    public class MeetingSampleWebAPIContext : DbContext
	{
		public MeetingSampleWebAPIContext(DbContextOptions<MeetingSampleWebAPIContext> options) : base(options)
		{
		}

		public DbSet<Meeting> Meetings { get; set; }
		public DbSet<Race> Races { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Meeting>().HasData(
                new Meeting { MeetCode = 1, MeetDate = new DateTime(2018, 1, 1), Title = "Meeting 1", MeetCatArea = "MC", MeetCatCode = 0, StateDesc = "VIC", VenueCode = 151 },
                new Meeting { MeetCode = 2, MeetDate = new DateTime(2018, 1, 2), Title = "Meeting 2", MeetCatArea = "MC", MeetCatCode = 0, StateDesc = "VIC", VenueCode = 151 },
                new Meeting { MeetCode = 3, MeetDate = new DateTime(2018, 1, 3), Title = "Meeting 3", MeetCatArea = "MC", MeetCatCode = 0, StateDesc = "VIC", VenueCode = 151 },
                new Meeting { MeetCode = 4, MeetDate = new DateTime(2018, 1, 4), Title = "Meeting 4", MeetCatArea = "MC", MeetCatCode = 0, StateDesc = "VIC", VenueCode = 151 },
                new Meeting { MeetCode = 5, MeetDate = new DateTime(2018, 1, 5), Title = "Meeting 5", MeetCatArea = "MC", MeetCatCode = 0, StateDesc = "VIC", VenueCode = 151 }
            );

            modelBuilder.Entity<Race>().HasData(
                new { RaceCode = 1, MeetCode = 1, RaceNumber = 1, Name = "Meeting 1, Race 1", Distance = 1000 },
                new { RaceCode = 2, MeetCode = 1, RaceNumber = 2, Name = "Meeting 1, Race 2", Distance = 1200 },
                new { RaceCode = 3, MeetCode = 1, RaceNumber = 3, Name = "Meeting 1, Race 3", Distance = 1300 },

                new { RaceCode = 4, MeetCode = 2, RaceNumber = 1, Name = "Meeting 2, Race 1", Distance = 1000 },
                new { RaceCode = 5, MeetCode = 2, RaceNumber = 2, Name = "Meeting 2, Race 2", Distance = 1200 },
                new { RaceCode = 6, MeetCode = 2, RaceNumber = 3, Name = "Meeting 2, Race 3", Distance = 1300 },
                new { RaceCode = 7, MeetCode = 2, RaceNumber = 4, Name = "Meeting 2, Race 4", Distance = 1000 }
            );
        }
	}
}
