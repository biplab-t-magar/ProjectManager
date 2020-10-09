/* Startup.cs
    This file contains the Startup class. The Startup class is generated by the ASP.NET Core framework. It is where all the 
    services require by the app are configured. It also delineates the app's request handling pipeline as a series of middleware components
*/
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProjectManager.Data;
using ProjectManager.Data.Interfaces;
using ProjectManager.Data.Services;
using ProjectManager.Data.SqlRepositories;
using ProjectManager.Models;

namespace ProjectManager
{
    public class Startup
    {
        /**/
        /*
        * NAME:
        *      Startup() - The constructor for the Startup class, created by the ASP.NET Core framework
        * SYNOPSIS:
        *      Startup()
        * DESCRIPTION:
        *      Initialization the Configuration object
        * RETURNS
        * AUTHOR
        *      Microsoft
        * DATE
        * /
        /**/
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }


        /**/
        /*
        * NAME:
        *      ConfigureServices() - The function where services used by the application can be registered. These services
                                        may be accessed in any other class using dependency injections, created by Asp.NET Core framework
        * SYNOPSIS:
        *      ConfigureServices(IServiceCollection services)
                        services --> the object that can be used to add services 
        * DESCRIPTION:
        * RETURNS
        * AUTHOR
        *      Microsoft
        * DATE
        * /
        /**/
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
         
            //add the Entity Framework Core service to the project manager web application in order
            //to create, manage, and communicate with databases effectively
            services.AddDbContext<ProjectManagerContext>(options => 
                options.UseNpgsql(Configuration.GetConnectionString("ProjectManagerConnection")));
        
            //AddIdentity adds cookie-based authentication to the web application
            //adds scoped classes for things like UserManager, SignInManager, PasswordHashers, etc...
            //Automatically adds validated user from a cookie to the HttpContext.User
            services.AddIdentity<AppUser, IdentityRole>()
                //Adds the UserStore and RoleStore from this context that are consumed by UserManager and RoleManager
                //Identity uses Entity Framework to store tables containing user data, among the other tables in the web application
                .AddEntityFrameworkStores<ProjectManagerContext>()
                .AddDefaultTokenProviders();

            //change password strength requirements to make them lax for demonstration purposes
            services.Configure<IdentityOptions>(options => 
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 4;
            });
            //change login URL so unauthorized users can be redirected
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/login";
                
            });

            services.AddControllersWithViews();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

            //for accessing the user who is making requests to the server application
            services.AddHttpContextAccessor();  

            //all the Services below are added as "scoped" services, which means the same instance of the classes
            //will be injected to classes during one request. Another instane will be created for the next request and so on.

            //user defined services
            //these services will be available through dependency injection for any classes that need them

            //add the SqlProjectsRepo service, which will act as the implementation for the IProjectsRepo interface
            //when classes ask for a concrete instance of the IProjectsRepo
            services.AddScoped<IProjectsRepo, SqlProjectsRepo>();

            //add the SqlAppUsersRepo service, which will act as the implementation for the IAppUsersRepo interface
            //when classes ask for a concrete instance of the IAppUsersRepo
            services.AddScoped<IAppUsersRepo, SqlUsersRepo>();

            //add the SqlTasksRepo service, which will act as the implementation for the ITasksRepo interface
            //when classes ask for a concrete instance of the ITasksRepo
            services.AddScoped<ITasksRepo, SqlTasksRepo>();

            //add the SqlTaskTypesRepo service, which will act as the implementation for the ITaskTypesRepo interface
            //when classes ask for a concrete instance of the ITaskTypesRepo
            services.AddScoped<ITaskTypesRepo, SqlTaskTypesRepo>();

            //add the ProjectMember validation service
            services.AddScoped<ProjectMemberValidation>();

            //add the ProjectActivity service
            services.AddScoped<ProjectActivity>();

        }

        /**/
        /*
        * NAME:
        *      Configure() - The function where the middle ware pipeline for requests are set up, created by the ASP.NET Core framework
        * SYNOPSIS:
        *      Configure(IApplicationBuilder app, IWebHostEnvironment env)
                        app --> the ApplicationBuilder object through which the middleware pipeline components are set up in the given order
                        env --> defines the environment of the web application (development or production environment)
        * DESCRIPTION:
                The function where the middle ware pipeline for requests are set up. Each HTTP request sent to the server has 
                to pass through this pipeline and each middleware component in the pipeline evaluates the HTTP request
                and passes on the request (changed or not) to the next component or back to the client.
        * RETURNS
        * AUTHOR
        *      Microsoft
        * DATE
        * /
        /**/
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
            app.UseSpaStaticFiles();

            app.UseRouting();

            //set up Identity
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
