using ChooseCourseManagement.Application;
using ChooseCourseManagement.Application.Contracts.Dto;
using ChooseCourseManagement.Infrastructure.EFCore;
using CourseManagement.Domain.CourseAgg;
using Student.Specs.Infrastructure;
using Student.Test.Tools.CourseTestTools;
using Student.Test.Tools.StudentTestTools;
using Student.Test.Tools.TeacherTestTools;
using TeacherManagement.Domain.TeacherAgg;
using Xunit;
using Student.UnitTests;
using FluentAssertions;
using Student.Test.Tools.ChooseCourseTestTools;
using CourseManagement.Infrastructure.EFCore;
using StudentManagement.Infrastructure.EFCore;
using TeacherManagement.Infrastructure.EFCore;
using ChooseCourseManagement.Application.Contracts.ChooseCourse;

namespace Student.Specs.ChooseCourseTests
{
    public class AddChooseCourse : EFDataContextDatabaseFixture
    {
        private readonly ChooseCourseManagementContext _context;
        private readonly CourseManagementContext _courseContext;
        private readonly TeacherManagementContext _teacherContext;
        private readonly StudentManagementContext _studentContext;
        private readonly IChooseCourseApplication _sut;
        private Course _course;
        private Course _secondCourse;
        private StudentManagement.Domain.StudentAgg.Student _student;
        private Teacher _teacher;
        private Teacher _secondTeacher;
        private AddChooseCourseDto _dto;

        public AddChooseCourse(ConfigurationFixture configuration)
            : base(configuration)
        {
            _context = CreateDataContext();
            _sut = ChooseCourseServiceFactory
                            .GenerateChooseCourseServiceFactory(
                _context,
                _courseContext,
                _teacherContext,
                _studentContext);
        }

        [BDDHelper.Given("")]
        private void Given()
        {
            _course = new CourseDtoBuilder()
                .WithName("مهندسی نرم افزار")
                .Build();
            _context.Manipulate(_ => _context.Add(_course));
            _teacher = new TeacherBuilder()
                .WithFirstName("آرش")
                .WithLastName("چناری")
                .Build();
            _context.Manipulate(_ => _.Add(_teacher));
            _student = new StudentBuilder()
                .WithFirstName("سعید")
                .WithLastName("انصاری")
                .WithNationalNumber("2280509507")
                .Build();
            _context.Manipulate(_ => _.Add(_student));
        }

        [BDDHelper.When("")]
        private async Task When()
        {
            _dto = new AddChooseCourseDto
            {
                StudentId = _student.Id,
                CourseId = _course.Id,
                TeacherId = _teacher.Id,
            };

            _sut.Add(_dto);
        }

        [BDDHelper.Then("")]
        private async Task Then()
        {
            var actual = _context.ChooseCourse.ToList();
            actual.Should().HaveCount(1);
        }

        [Fact]
        public void Run()
        {
            BDDHelper.Runner.RunScenario(
                _ => Given(),
                _ => When().Wait(),
                _ => Then().Wait()
            );
        }
    }
}
