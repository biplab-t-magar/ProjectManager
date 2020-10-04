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
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
         
            services.AddDbContext<ProjectManagerContext>(options => 
                options.UseNpgsql(Configuration.GetConnectionString("ProjectManagerConnection")));
        
            //AddIdentity adds cookie-based authentication
            //adds scoped classes for things like UserManager, SignInManager, PasswordHashers, etc...
            //Automatically adds validated user from a cookie to the HttpContext.User
            services.AddIdentity<AppUser, IdentityRole>()
                // {
                //     //make sure one email corresponds to one user account
                //     options.User.RequireUniqueEmail = true;
                // })
                //Adds the UserStore and RoleStore from this context that are consumed by UserManager and RoleManager
                .AddEntityFrameworkStores<ProjectManagerContext>()
                .AddDefaultTokenProviders();

            //add authorization service 
            // services.AddAuthorization(config => 
            // {
            //     config.AddPolicy("UserIsProjectMember", policy =>
            //     {
            //         policy.add
            //     });
            // });

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

            services.AddScoped<IProjectsRepo, SqlProjectsRepo>();
            services.AddScoped<IAppUsersRepo, SqlUsersRepo>();
            services.AddScoped<ITasksRepo, SqlTasksRepo>();
            services.AddScoped<ITaskTypesRepo, SqlTaskTypesRepo>();
            services.AddScoped<ProjectMemberValidation>();
            services.AddHttpContextAccessor();  

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
