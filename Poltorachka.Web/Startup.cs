using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Poltorachka.DataAccess;
using Poltorachka.DataAccess.Facts;
using Poltorachka.DataAccess.Individuals;
using Poltorachka.Domain.Facts;
using Poltorachka.Domain.Individuals;
using Poltorachka.Web.Authorization;
using Poltorachka.Web.Data;
using Poltorachka.Web.Services;

namespace Poltorachka.Web
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
                    options.Conventions.AuthorizeFolder("/Profile");
                    options.Conventions.AuthorizeFolder("/DevClub", "DevClub");

                    options.Conventions.AddPageRoute("/Profile/ProfileEdit", "Profile/{userName}");
                    options.Conventions.AddPageRoute("/Facts/FactEdit", "Facts/{id}");
                    options.Conventions.AddPageRoute("/Facts/CreateFact", "Facts/New");
                });

            services.AddSingleton<IConfiguration>(Configuration);
            services.AddSingleton<IPagesRedirectHelper, PagesRedirectHelper>();

            services.AddScoped<IFactAccessService, FactAccessService>();
            services.AddScoped<IFactAppService, FactAppService>();
            services.AddScoped<IIndividualsAppService, IndividualsAppService>();
            services.AddScoped<IDashboardAppService, DashboardAppService>();
            services.AddScoped<IFactAggregateRepository, FactAggregateRepository>();
            services.AddScoped<IFactSummaryQuery, FactSummaryQuery>();
            services.AddScoped<IFactsQuery, FactsQuery>();
            services.AddScoped<IIndividualsQuery, IndividualsQuery>();
            services.AddScoped<IUserRemainingBalanceQuery, UserRemainingBalanceQuery>();
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
