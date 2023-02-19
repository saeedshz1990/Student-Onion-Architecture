using _0_Framework.Domain;

namespace CourseManagement.Domain.CourseAgg
{
    public class Course : EntityBase
    {

        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        public int TeacherId { get; set; }
    }
}
