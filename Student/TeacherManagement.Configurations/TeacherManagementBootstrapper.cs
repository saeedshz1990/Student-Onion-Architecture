using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TeacherManagement.Application;
using TeacherManagement.Application.Contracts.Teacher;
using TeacherManagement.Domain.TeacherAgg;
using TeacherManagement.Infrastructure.EFCore;
using TeacherManagement.Infrastructure.EFCore.Repository;

namespace TeacherManagement.Configurations
{
    public class TeacherManagementBootstrapper
    {

        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddTransient<ITeacherApplication, TeacherApplication>();
            services.AddTransient<ITeacherRepository, TeacherRepository>();

            services.AddDbContext<TeacherManagementContext>(x => x.UseSqlServer(connectionString));
        }
    }
}
