using CaotunSpring.C000.Adapter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NS2.Data;
using NS7V2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NS7V2
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

            services.AddDbContextFactory<NS2Context>(options =>
                                options.UseSqlServer(
                                    Configuration.GetConnectionString("BizConnection")));
            //https://stackoverflow.com/questions/64169025/using-identity-with-adddbcontextfactory-in-blazor

            services.AddScoped<NS2Context>(p => p.GetRequiredService<IDbContextFactory<NS2Context>>().CreateDbContext());


            //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<NS2Context>();


            services.AddRazorPages();
            services.AddServerSideBlazor();
            //      services.AddSingleton<WeatherForecastService>();

            services.AddScoped<IPageHelperV7, PageHelperV7>();

            services.AddScoped<C000Adapter>();
            services.AddScoped<C001Adapter>();
            services.AddScoped<C002Adapter>();
            services.AddScoped<C003Adapter>();
            services.AddScoped<C004Adapter>();
            services.AddScoped<C005Adapter>();
            services.AddScoped<C006Adapter>();
            services.AddScoped<C007Adapter>();
            services.AddScoped<C008Adapter>();
            services.AddScoped<C009Adapter>();
            services.AddScoped<C010Adapter>();
            services.AddScoped<C011Adapter>();
            services.AddScoped<C012Adapter>();
            services.AddScoped<C013Adapter>();
            services.AddScoped<C014Adapter>();
            services.AddScoped<C015Adapter>();
            services.AddScoped<C016Adapter>();
            services.AddScoped<C017Adapter>();
            services.AddScoped<C018Adapter>();
            services.AddScoped<C019Adapter>();
            services.AddScoped<C020Adapter>();
            services.AddScoped<C021Adapter>();
            services.AddScoped<C022Adapter>();
            services.AddScoped<C023Adapter>();
            services.AddScoped<C024Adapter>();
            services.AddScoped<C025Adapter>();
            services.AddScoped<C026Adapter>();
            services.AddScoped<C027Adapter>();
            services.AddScoped<C028Adapter>();
            services.AddScoped<C029Adapter>();
            services.AddScoped<C030Adapter>();
            services.AddScoped<C031Adapter>();
            services.AddScoped<C032Adapter>();
            services.AddScoped<C033Adapter>();
            services.AddScoped<C034Adapter>();
            services.AddScoped<C035Adapter>();
            services.AddScoped<C036Adapter>();
            services.AddScoped<C037Adapter>();
            services.AddScoped<C038Adapter>();
            services.AddScoped<C039Adapter>();
            services.AddScoped<C040Adapter>();
            services.AddScoped<C041Adapter>();
            services.AddScoped<C042Adapter>();
            services.AddScoped<C043Adapter>();
            services.AddScoped<C044Adapter>();
            services.AddScoped<C045Adapter>();
            services.AddScoped<C046Adapter>();
            services.AddScoped<C047Adapter>();
            services.AddScoped<C048Adapter>();
            services.AddScoped<C049Adapter>();
            services.AddScoped<C050Adapter>();
            services.AddScoped<C051Adapter>();
            services.AddScoped<C052Adapter>();
            services.AddScoped<C053Adapter>();
            services.AddScoped<C054Adapter>();
            services.AddScoped<C055Adapter>();
            services.AddScoped<C056Adapter>();
            services.AddScoped<C057Adapter>();
            services.AddScoped<C058Adapter>();
            services.AddScoped<C059Adapter>();
            services.AddScoped<C060Adapter>();
            services.AddScoped<C061Adapter>();
            services.AddScoped<C062Adapter>();
            services.AddScoped<C063Adapter>();
            services.AddScoped<C064Adapter>();
            services.AddScoped<C065Adapter>();
            services.AddScoped<C066Adapter>();
            services.AddScoped<C067Adapter>();
            services.AddScoped<C068Adapter>();
            services.AddScoped<C069Adapter>();
            services.AddScoped<C070Adapter>();
            services.AddScoped<C071Adapter>();
            services.AddScoped<C072Adapter>();
            services.AddScoped<C073Adapter>();
            services.AddScoped<C074Adapter>();
            services.AddScoped<C075Adapter>();
            services.AddScoped<C076Adapter>();
            services.AddScoped<C077Adapter>();
            services.AddScoped<C078Adapter>();
            services.AddScoped<C079Adapter>();
            services.AddScoped<C080Adapter>();
            services.AddScoped<C081Adapter>();
            services.AddScoped<C082Adapter>();
            services.AddScoped<C083Adapter>();
            services.AddScoped<C084Adapter>();
            services.AddScoped<C085Adapter>();
            services.AddScoped<C086Adapter>();
            services.AddScoped<C087Adapter>();
            services.AddScoped<C088Adapter>();
            services.AddScoped<C089Adapter>();
            services.AddScoped<C090Adapter>();
            services.AddScoped<C091Adapter>();
            services.AddScoped<C092Adapter>();
            services.AddScoped<C093Adapter>();
            services.AddScoped<C094Adapter>();
            services.AddScoped<C095Adapter>();
            services.AddScoped<C096Adapter>();
            services.AddScoped<C097Adapter>();
            services.AddScoped<C098Adapter>();
            services.AddScoped<C099Adapter>();

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
