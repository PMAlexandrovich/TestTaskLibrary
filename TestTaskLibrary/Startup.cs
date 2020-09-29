using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using TestTaskLibrary.Domain.Application;
using TestTaskLibrary.Domain.Core;
using TestTaskLibrary.Domain.Interfaces;
using TestTaskLibrary.Infrastructure.Business;
using TestTaskLibrary.Infrastructure.Data;
using TestTaskLibrary.Infrastructure.Data.Repositories;

namespace TestTaskLibrary
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
            string connection = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<LibraryContext>(options =>

                options.UseNpgsql(connection)
            );

            services.AddTransient<IBooksRepository, BooksRepository>();
            services.AddTransient<IBookStatusesRepository, BookStatusesRepository>();
            services.AddTransient<IBookAdditionalInfosRepository, BookAdditionalInfosRepository>();
            services.AddTransient<IBookReviewsRepository, BookReviewRepository>();

            services.AddTransient<BookReviewsManager>();
            services.AddTransient<LibraryManager>();

            services.AddApplication();

            services.AddIdentity<User, CustomRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Stores.ProtectPersonalData = false;
            })
            .AddEntityFrameworkStores<LibraryContext>()
            .AddErrorDescriber<RussianIdentityErrorDescriber>();
            services.AddQuartz(q =>
            {
                q.SchedulerId = "Scheduler-Core";
                q.SchedulerName = "Quartz Scheduler";

                q.UseMicrosoftDependencyInjectionScopedJobFactory();

                q.UseSimpleTypeLoader();
                q.UseInMemoryStore();
                q.UseDefaultThreadPool(tp =>
                {
                    tp.MaxConcurrency = 10;
                });

                var jobKey = new JobKey("bookJob", "group 1");

                q.AddJob<BookJob>(j => 
                {
                    j.WithIdentity(jobKey);
                    j.StoreDurably();
                });

                q.AddTrigger(t =>
                {
                    t.WithIdentity("Simple Trigger");
                    t.ForJob(jobKey);
                    t.StartNow();
                    t.WithSimpleSchedule(x => x.WithInterval(TimeSpan.FromSeconds(30)).RepeatForever());
                });
            });

            // ASP.NET Core hosting
            services.AddQuartzServer(options =>
            {
                // when shutting down we want jobs to complete gracefully
                options.WaitForJobsToComplete = true;
            });

            services.AddControllersWithViews();
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Library}/{action=Index}/{id?}");
            });
        }
    }
}
