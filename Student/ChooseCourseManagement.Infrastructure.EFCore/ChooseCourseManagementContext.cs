using ChooseCourseManagement.Domain.ChooseCourseAgg;
using ChooseCourseManagement.Infrastructure.EFCore.Mapping;
using Microsoft.EntityFrameworkCore;

namespace ChooseCourseManagement.Infrastructure.EFCore
{
    public class ChooseCourseManagementContext : DbContext
    {

        public DbSet<ChooseCourse> ChooseCourse { get; set; }
        public ChooseCourseManagementContext(DbContextOptions<ChooseCourseManagementContext> options) 
            :base(options)
        {
            
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(ChooseCourseMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);

            base.OnModelCreating(modelBuilder);
        }

    }
}