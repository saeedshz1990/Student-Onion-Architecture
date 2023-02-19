using _0_Framework.Domain;
using CourseManagement.Domain.CourseAgg;
using StudentManagement.Domain.StudentAgg;

namespace TeacherManagement.Domain.TeacherAgg
{
    public class Teacher : PersonEntityBase
    {
        public List<Student> Students { get; set; }
        public List<Course> Courses{ get; set; }
    }
}