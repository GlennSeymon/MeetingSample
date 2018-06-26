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
	}
}
