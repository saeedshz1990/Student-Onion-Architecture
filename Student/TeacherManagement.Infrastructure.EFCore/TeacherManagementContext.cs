using Microsoft.EntityFrameworkCore;
using TeacherManagement.Domain.TeacherAgg;
using TeacherManagement.Infrastructure.EFCore.Mapping;

namespace TeacherManagement.Infrastructure.EFCore
{
    public class TeacherManagementContext : DbContext
    {
        public DbSet<Teacher> Teacher { get; set; }

        public TeacherManagementContext(DbContextOptions<TeacherManagementContext> options)
           : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(TeacherMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
