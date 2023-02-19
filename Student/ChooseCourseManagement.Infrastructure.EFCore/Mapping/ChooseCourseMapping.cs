using ChooseCourseManagement.Domain.ChooseCourseAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChooseCourseManagement.Infrastructure.EFCore.Mapping
{
    public class ChooseCourseMapping : IEntityTypeConfiguration<ChooseCourse>
    {
        public void Configure(EntityTypeBuilder<ChooseCourse> builder)
        {
            builder.ToTable("ChooseCourses");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.StudentId);
            builder.Property(x => x.CourseId);
            builder.Property(x => x.TeacherId);
        }
    }
}
