using ChooseCourseManagement.Application.Contracts.ChooseCourse;
using ChooseCourseManagement.Application.Contracts.Dto;
using ChooseCourseManagement.Domain.ChooseCourseAgg;
using ChooseCourseManagement.Infrastructure.EFCore;
using CourseManagement.Infrastructure.EFCore;
using FluentAssertions;
using Student.Test.Tools.ChooseCourseTestTools;
using Student.Test.Tools.CourseTestTools;
using Student.Test.Tools.StudentTestTools;
using Student.Test.Tools.TeacherTestTools;
using StudentManagement.Infrastructure.EFCore;
using TeacherManagement.Infrastructure.EFCore;
using Xunit;

namespace Student.UnitTests.ChooseCourseTest
{
    public class ChooseCourseServiceTest
    {
        private readonly ChooseCourseManagementContext _context;
        private readonly CourseManagementContext _courseContext;
        private readonly TeacherManagementContext _teacherContext;
        private readonly StudentManagementContext _studentContext;
        private readonly IChooseCourseApplication _sut;

        public ChooseCourseServiceTest()
        {
            _context = new EFInMemoryDatabase().CreateDataContext<ChooseCourseManagementContext>();
            _sut = ChooseCourseServiceFactory
                .GenerateChooseCourseServiceFactory(
                _context, 
                _courseContext, 
                _teacherContext,
                _studentContext);
        }

        [Fact]
        public async Task Add_add_choose_course_properly()
        {
            var course = new CourseDtoBuilder()
                .WithName("مهندسی نرم افزار")
                .Build();
            _context.Manipulate(_ => _context.Add(course));
            var teacher = new TeacherBuilder()
                .WithFirstName("آرش")
                .WithLastName("چناری")
                .WithNationalNumber("2294321905")
                .Build();
            _context.Manipulate(_ => _.Add(teacher));
            var student = new StudentBuilder()
                .WithFirstName("سعید")
                .WithLastName("انصاری")
                .WithNationalNumber("2280509504")
                .Build();
            _context.Manipulate(_ => _.Add(student));
            var dto = new AddChooseCourseDto
            {
                StudentId = student.Id,
                CourseId = course.Id,
                TeacherId = teacher.Id,
            };

            _sut.Add(dto);

            var actual = _context.ChooseCourse.ToList();
            actual.Should().HaveCount(1);
        }
    }
}
