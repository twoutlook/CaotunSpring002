using DreamAITek.T001.Adapter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NS3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NS3
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
            //services.AddRazorPages();
            //services.AddServerSideBlazor();
            //services.AddSingleton<WeatherForecastService>();


            services.AddDbContextFactory<NS2Context>(options =>
                      options.UseSqlServer(
                          Configuration.GetConnectionString("BizConnection")));
            //https://stackoverflow.com/questions/64169025/using-identity-with-adddbcontextfactory-in-blazor

            services.AddScoped<NS2Context>(p => p.GetRequiredService<IDbContextFactory<NS2Context>>().CreateDbContext());


            //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<NS2Context>();


            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<WeatherForecastService>();


            services.AddScoped<A001Adapter>();
            services.AddScoped<A002Adapter>();
            services.AddScoped<A003Adapter>();
            services.AddScoped<A004Adapter>();
            services.AddScoped<A005Adapter>();
            services.AddScoped<A006Adapter>();
            services.AddScoped<A007Adapter>();
            services.AddScoped<A008Adapter>();
            services.AddScoped<A009Adapter>();
            services.AddScoped<A010Adapter>();
            services.AddScoped<A011Adapter>();
            services.AddScoped<A012Adapter>();
            services.AddScoped<A013Adapter>();
            services.AddScoped<A014Adapter>();
            services.AddScoped<A015Adapter>();
            services.AddScoped<A016Adapter>();
            services.AddScoped<A017Adapter>();
            services.AddScoped<A018Adapter>();
            services.AddScoped<A019Adapter>();
            services.AddScoped<A020Adapter>();
            services.AddScoped<A021Adapter>();
            services.AddScoped<A022Adapter>();
            services.AddScoped<A023Adapter>();
            services.AddScoped<A024Adapter>();
            services.AddScoped<A025Adapter>();
            services.AddScoped<A026Adapter>();
            services.AddScoped<A027Adapter>();
            services.AddScoped<A028Adapter>();
            services.AddScoped<A029Adapter>();
            services.AddScoped<A030Adapter>();
            services.AddScoped<A031Adapter>();
            services.AddScoped<A032Adapter>();
            services.AddScoped<A033Adapter>();
            services.AddScoped<A034Adapter>();
            services.AddScoped<A035Adapter>();
            services.AddScoped<A036Adapter>();
            services.AddScoped<A037Adapter>();
            services.AddScoped<A038Adapter>();
            services.AddScoped<A039Adapter>();
            services.AddScoped<A040Adapter>();
            services.AddScoped<A041Adapter>();
            services.AddScoped<A042Adapter>();
            services.AddScoped<A043Adapter>();
            services.AddScoped<A044Adapter>();
            services.AddScoped<A045Adapter>();
            services.AddScoped<A046Adapter>();
            services.AddScoped<A047Adapter>();
            services.AddScoped<A048Adapter>();
            services.AddScoped<A049Adapter>();
            services.AddScoped<A050Adapter>();
            services.AddScoped<A051Adapter>();
            services.AddScoped<A052Adapter>();
            services.AddScoped<A053Adapter>();
            services.AddScoped<A054Adapter>();
            services.AddScoped<A055Adapter>();
            services.AddScoped<A056Adapter>();
            services.AddScoped<A057Adapter>();
            services.AddScoped<A058Adapter>();
            services.AddScoped<A059Adapter>();
            services.AddScoped<A060Adapter>();
            services.AddScoped<A061Adapter>();
            services.AddScoped<A062Adapter>();
            services.AddScoped<A063Adapter>();
            services.AddScoped<A064Adapter>();
            services.AddScoped<A065Adapter>();
            services.AddScoped<A066Adapter>();
            services.AddScoped<A067Adapter>();
            services.AddScoped<A068Adapter>();
            services.AddScoped<A069Adapter>();
            services.AddScoped<A070Adapter>();
            services.AddScoped<A071Adapter>();
            services.AddScoped<A072Adapter>();
            services.AddScoped<A073Adapter>();
            services.AddScoped<A074Adapter>();
            services.AddScoped<A075Adapter>();
            services.AddScoped<A076Adapter>();
            services.AddScoped<A077Adapter>();
            services.AddScoped<A078Adapter>();
            services.AddScoped<A079Adapter>();
            services.AddScoped<A080Adapter>();
            services.AddScoped<A081Adapter>();
            services.AddScoped<A082Adapter>();
            services.AddScoped<A083Adapter>();
            services.AddScoped<A084Adapter>();
            services.AddScoped<A085Adapter>();
            services.AddScoped<A086Adapter>();
            services.AddScoped<A087Adapter>();
            services.AddScoped<A088Adapter>();
            services.AddScoped<A089Adapter>();
            services.AddScoped<A090Adapter>();
            services.AddScoped<A091Adapter>();
            services.AddScoped<A092Adapter>();
            services.AddScoped<A093Adapter>();
            services.AddScoped<A094Adapter>();
            services.AddScoped<A095Adapter>();
            services.AddScoped<A096Adapter>();
            services.AddScoped<A097Adapter>();
            services.AddScoped<A098Adapter>();
            services.AddScoped<A099Adapter>();

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
            }

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
