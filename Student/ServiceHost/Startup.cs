using CourseManagement.Configurations;
using Microsoft.AspNetCore.Authentication.Cookies;
using StudentManagement.Configurations;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using TeacherManagement.Configurations;

namespace ServiceHost
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
            services.AddHttpContextAccessor();
            var connectionString = Configuration.GetConnectionString("Students");
            CourseManagementBootstrapper.Configure(services, connectionString);
            StudentManagementBootstrapper.Configure(services, connectionString);
            TeacherManagementBootstrapper.Configure(services, connectionString);

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
                app.UseDeveloperExceptionPage();
                //app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseAuthentication();

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });
        }
    }
}
