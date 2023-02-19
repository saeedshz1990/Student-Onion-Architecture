using CourseManagement.Domain.CourseAgg;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Domain.StudentAgg;
using StudentManagement.Infrastructure.EFCore.Mapping;

namespace StudentManagement.Infrastructure.EFCore
{
    public class StudentManagementContext : DbContext
    {
        public DbSet<Student> Student { get; set; }


        public StudentManagementContext(DbContextOptions<StudentManagementContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(StudentMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
