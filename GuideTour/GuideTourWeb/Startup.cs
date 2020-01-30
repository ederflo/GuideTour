using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuideTourData.Models;
using GuideTourData.Services;
using GuideTourLogic.Logics;
using GuideTourLogic.Services;
using GuideTourWeb.Hubs;
using GuideTourWeb.Mqtt;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GuideTourWeb
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
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddSignalR();

            services.Configure<DbProperties>(
                options =>
                {
                    options.ConnectionString = Configuration.GetSection("MongoDb:ConnectionString").Value;
                    options.DatabaseName = Configuration.GetSection("MongoDb:DatabaseName").Value;
                });

            services.AddTransient<IDocumentDbRepository, DocumentDbRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IDocumentDbRepository ddb, IHubContext<TourHub> hubcontext)
        {
            try
            {
                MqttService.Init(ddb, hubcontext);

                TeamLogic teamLogic = new TeamLogic(ddb);
                GuideLogic guideLogic = new GuideLogic(ddb);
                TeacherLogic teacherLogic = new TeacherLogic(ddb);
                TeamImporter teamImporter = new TeamImporter(ddb);
                TeacherImporter teacherImporter = new TeacherImporter(ddb);
                List<Team> teams = teamLogic.Get().GetAwaiter().GetResult();
                List<Guide> guides = guideLogic.Get().GetAwaiter().GetResult();
                List<Teacher> teachers = teacherLogic.Get().GetAwaiter().GetResult();
                if ((guides == null && teams == null) || (guides.Count() <= 0 && teams.Count() <= 0))
                    teamImporter.ImportTeams().GetAwaiter().GetResult();
                if (teachers == null || teachers.Count <= 0)
                    teacherImporter.ImportTeachers().GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Tour}/{action=Index}/{id?}");
                endpoints.MapHub<TourHub>("/tourHub");

            });
        }
    }
}
