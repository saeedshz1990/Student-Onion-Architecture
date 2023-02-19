using _0_Framework.Domain;

namespace StudentManagement.Domain.StudentAgg
{
    public class Student : PersonEntityBase
    {
        public int TeacherId { get; set; }
    }
}
