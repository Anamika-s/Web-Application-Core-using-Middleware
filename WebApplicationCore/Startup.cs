using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationCore.Context;
using WebApplicationCore.IRepository;
using WebApplicationCore.Repository;

namespace WebApplicationCore
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
            // We have to register services here
            // Also we can set their lifetime
            //services.AddSingleton(); // There will  be 1 instance for entire application
            //services.AddScoped(); // There will  be 1 instance for one scope
            //services.AddTransient();//  There will  be 1 instance for ever//y request

            services.AddScoped<IStudentRepo, StudentRepo>();

            services.AddDbContext<AppDbContext>(op => op.UseSqlServer(Configuration["ConnectionStrings:MyConnection"]));
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
            //app.UseDefaultFiles();
             app.UseStaticFiles();

            //app.UseFileServer();
          
            app.UseRouting();

            app.UseAuthorization();

            //app.Run(async context =>
            //{
            //    await context.Response.WriteAsync("Hello 1"); 
            //});
            //app.Run(async context =>
            //{
            //    await context.Response.WriteAsync("Hello 2");
            //});
           // app.Run(async context =>
           // {
           //     await context.Response.WriteAsync(Configuration["Name"]); 
           // });
           // app.Use(async (context, next) =>
           // {
           //     await context.Response.WriteAsync("1");
           //     await next();
           // });

           // app.Use(async (context, next) =>
           //{
           //    await context.Response.WriteAsync("2");
           //    await next();
           //});

            app.Map("/newBranch", context =>
            {
                context.Run(x => x.Response.WriteAsync("Hello from New Branch"));
            });
            app.Map("/branch", a => {
                a.Map("/branch1", brancha => brancha
                    .Run(c => c.Response.WriteAsync("Running from the newbranch/branch1 branch!")));
                a.Map("/branch2", brancha => brancha
                    .Run(c => c.Response.WriteAsync("Running from the newbranch/branch2 branch!")));

                a.Run(c => c.Response.WriteAsync("Running from the newbranch branch!"));
            });

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapControllerRoute(
                //    name: "EmpRoute",
                //    pattern: "{controller=Employee}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
