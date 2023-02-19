using CourseManagement.Application;
using CourseManagement.Application.Contracts.Course;
using CourseManagement.Domain.CourseAgg;
using CourseManagement.Infrastructure.EFCore;
using CourseManagement.Infrastructure.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CourseManagement.Configurations
{
    public class CourseManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddTransient<ICourseApplication, CourseApplication>();
            services.AddTransient<ICourseRepository, CourseRepository>();

            services.AddDbContext<CourseManagementContext>(x => x.UseSqlServer(connectionString));
        }
    }
}