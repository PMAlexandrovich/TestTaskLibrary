using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OData.Edm;
using Quartz;
using TestTaskLibrary.Domain.Application;
using TestTaskLibrary.Domain.Application.Features.AuthorFeatures.ViewModels;
using TestTaskLibrary.Domain.Application.Features.BookFeatures.ViewModels;
using TestTaskLibrary.Domain.Application.Features.GenreFeatures.ViewModels;
using TestTaskLibrary.Domain.Application.Interfaces;
using TestTaskLibrary.Domain.Application.Interfaces.Managers;
using TestTaskLibrary.Domain.Application.Interfaces.Repositories;
using TestTaskLibrary.Domain.Core;
using TestTaskLibrary.Infrastructure.Business;
using TestTaskLibrary.Infrastructure.Data;
using TestTaskLibrary.Infrastructure.Data.Repositories;
using TestTaskLibrary.Models;
using TestTaskLibrary.Models.BasicAuthentication;

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

            services.AddHttpContextAccessor();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddTransient<IGenericRepository<BookStatus>, EFGenericRepository<BookStatus>>();
            services.AddTransient<IGenericRepository<Author>, EFGenericRepository<Author>>();
            services.AddTransient<IGenericRepository<Book>, EFGenericRepository<Book>>();
            services.AddTransient<IGenericRepository<BookAdditionalInfo>, EFGenericRepository<BookAdditionalInfo>>();
            services.AddTransient<IGenericRepository<BookReview>, EFGenericRepository<BookReview>>();
            services.AddTransient<IGenericRepository<Genre>, EFGenericRepository<Genre>>();
            services.AddTransient<IGenericRepository<User>, EFGenericRepository<User>>();

            services.AddTransient<IBookReviewsManager,BookReviewsManager>();
            services.AddTransient<ILibraryManager, LibraryManager>();

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

            
            services.AddOData();

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

            services.AddAuthentication("Basic")
                .AddScheme<BasicAuthenticationSchemeOptions, BasicAuthenticationHandler>("Basic", null);

            services.AddControllersWithViews().AddNewtonsoftJson();
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
                endpoints.Select().Expand().Filter().OrderBy().MaxTop(100).Count();

                endpoints.MapODataRoute("odata", "odata",GetEdmModel());

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Library}/{action=Index}/{id?}");
            });

            app.UseDbInit();
        }

        private static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();

            var books = builder.EntitySet<BookViewModel>("Books");
            var users = builder.EntitySet<UserViewModel>("Users");
            var reviews = builder.EntitySet<ReviewViewModel>("Reviews");
            builder.EntitySet<AuthorViewModel>("Authors");
            builder.EntitySet<GenreViewModel>("Genres");

            var action = books.EntityType.Action("Book");

            action = books.EntityType.Action("Unbook");

            action = books.EntityType.Action("Issue");
            action.Parameter<string>("UserEmail");


            action = books.EntityType.Action("Take");

            action = users.EntityType.Action("ChangePassword");
            action.Parameter<string>("Password");
            
            return builder.GetEdmModel();
        }
    }
}
