using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentManagement.Domain.StudentAgg;

namespace StudentManagement.Infrastructure.EFCore.Mapping
{
    public class StudentMapping : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Students");
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