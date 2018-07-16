using AutoMapper;
using MeetingSample.WebAPI.Interface;
using MeetingSample.WebAPI.Models;
using MeetingSample.WebAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MeetingSample.WebAPI
{
	public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
			services.AddTransient<IMeetingService, MeetingService>();
			services.AddTransient<IRaceService, RaceService>();
			services.AddTransient<IVenueService, VenueService>();

			services.AddDbContext<MeetingSampleWebAPIContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddAutoMapper();
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
		}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<MeetingSampleWebAPIContext>();
                //context.Database.EnsureDeleted();
                ////context.SaveChanges();

                //context.Database.Migrate(); // This must be called before EnsureCreated.
                //context.Database.EnsureCreated();
                ////context.SaveChanges();
            }

			app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
