using CaotunSpring002.Adapter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NS002.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NS002
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // NOTE 
            // 1. ref CS7Comps, Add ConfigureServices using forstartup.txt
            // 2. Project ItemGroup , NuGet, dotnet restore
            // 3. EF Core Models and Data, appsettings
            // 4. _Imports.razor

            services.AddDbContextFactory<NS002Context>(options =>
                                options.UseSqlServer(
                                    Configuration.GetConnectionString("BizConnection")));
            //https://stackoverflow.com/questions/64169025/using-identity-with-adddbcontextfactory-in-blazor

            services.AddScoped<NS002Context>(p => p.GetRequiredService<IDbContextFactory<NS002Context>>().CreateDbContext());


            //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<NS2Context>();


            services.AddRazorPages();
            services.AddServerSideBlazor();

            services.AddScoped<IPageHelperV7, PageHelperV7>();
            services.AddScoped<IFiltersV7, FiltersV7>();

            services.AddScoped<A00Adapter>();
            services.AddScoped<A01Adapter>();
            services.AddScoped<A02Adapter>();
            services.AddScoped<A03Adapter>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
