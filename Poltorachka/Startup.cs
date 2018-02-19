using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Poltorachka.Data;
using Poltorachka.DataAccess;
using Poltorachka.Domain;
using Poltorachka.Models;
using Poltorachka.Services;

namespace Poltorachka
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("MainDb")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminAccess", (policy) =>
                    policy.RequireRole(Roles.CreateAndEditFacts));
                options.AddPolicy("DevClub", (policy) => 
                    policy.RequireRole(Roles.ManageDevClub));
            });

            services.AddMvc()
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.AuthorizeFolder("/Facts", "AdminAccess");
                    options.Conventions.AuthorizeFolder("/DevClub", "DevClub");
                });

            services.AddSingleton<IConfiguration>(Configuration);
            services.AddScoped<IFactService, FactService>();
            services.AddScoped<IFactsService, FactsService>();
            services.AddScoped<IFactRepository, FactRepository>();
            services.AddScoped<IFactSummaryQuery, FactSummaryQuery>(); 
            services.AddScoped<IIndividualsQuery, IndividualsQuery>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseAuthentication();

            app.UseStaticFiles();

            app.UseMvc();
        }
    }
}
