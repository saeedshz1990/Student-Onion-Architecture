using CourseManagement.Domain.CourseAgg;
using CourseManagement.Infrastructure.EFCore.Mapping;
using Microsoft.EntityFrameworkCore;

namespace CourseManagement.Infrastructure.EFCore
{
    public class CourseManagementContext : DbContext
    {
        public DbSet<Course> Course { get; set; }


        public CourseManagementContext(DbContextOptions<CourseManagementContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(CourseMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
