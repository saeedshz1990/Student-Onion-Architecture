using _0_Framework.Domain;

namespace ChooseCourseManagement.Domain.ChooseCourseAgg
{
    public class ChooseCourse :EntityBase
    {
        public int TeacherId { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
    }
}