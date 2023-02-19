using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeacherManagement.Domain.TeacherAgg;

namespace TeacherManagement.Infrastructure.EFCore.Mapping
{
    public class TeacherMapping : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.ToTable("Teachers");
            builder.HasKey(x => x.Id);

            builder.HasKey(x => x.FirstName);
            builder.HasKey(x => x.LastName);
            builder.HasKey(x => x.YearBirth);
            builder.HasKey(x => x.CreationDate);
            builder.HasKey(x => x.MobilePhone);
            builder.HasKey(x => x.NationalNumber);
        }
    }
}
