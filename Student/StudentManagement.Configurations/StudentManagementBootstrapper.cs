using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StudentManagement.Application;
using StudentManagement.Application.Contracts.Student;
using StudentManagement.Domain.StudentAgg;
using StudentManagement.Infrastructure.EFCore;
using StudentManagement.Infrastructure.EFCore.Repository;

namespace StudentManagement.Configurations
{
    public class StudentManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddTransient<IStudentApplication, StudentApplication>();
            services.AddTransient<IStudentRepository, StudentRepository>();

            services.AddDbContext<StudentManagementContext>(x => x.UseSqlServer(connectionString));
        }
    }
}
