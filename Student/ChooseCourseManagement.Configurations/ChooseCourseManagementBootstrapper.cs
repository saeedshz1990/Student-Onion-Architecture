using ChooseCourseManagement.Application;
using ChooseCourseManagement.Application.Contracts.ChooseCourse;
using ChooseCourseManagement.Domain.ChooseCourseAgg;
using ChooseCourseManagement.Infrastructure.EFCore;
using ChooseCourseManagement.Infrastructure.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ChooseCourseManagement.Configurations
{
    public class ChooseCourseManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddTransient<IChooseCourseApplication, ChooseCourseApplication>();
            services.AddTransient<IChooseCourseRepository, ChooseCourseRepository>();

            services.AddDbContext<ChooseCourseManagementContext>(x => x.UseSqlServer(connectionString));
        }
    }
}