using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PlanIt.Models;
// using Microsoft.AspNetCore.Identity;

namespace PlanIt
{
  public class Startup
  {
    public Startup(IHostingEnvironment env)
    {
      var builder = new ConfigurationBuilder()
          .SetBasePath(env.ContentRootPath)
          .AddEnvironmentVariables();
          // .AddJsonFile("appsettings.json");
      Configuration = builder.Build();
    }

    public IConfigurationRoot Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMvc();

      services.AddEntityFrameworkMySql()
      .AddDbContext<PlanItContext>(options => options
      .UseMySql(Configuration["ConnectionStrings:DefaultConnection"]));

      
      // services.AddIdentity<ApplicationUser, IdentityRole>()
      // .AddEntityFrameworkStores<PlanItContext>()
      // .AddDefaultTokenProviders();
    
    }

    public void Configure(IApplicationBuilder app)
    {
      app.UseStaticFiles();

      app.UseDeveloperExceptionPage();

      // app.UseAuthentication();

      app.UseMvc(routes =>
      {
        routes.MapRoute(
          name: "default",
          template: "{controller=Home}/{action=Index}/{id?}");
      });

      app.Run(async (context) =>
      {
        await context.Response.WriteAsync("Something went wrong!");
      });
    }
  }

  public static class DBConfiguration
  {
    public static string ConnectionString = "server=localhost;user id=root;password=epicodus;port=3306;database=plan_it;";
  }
}