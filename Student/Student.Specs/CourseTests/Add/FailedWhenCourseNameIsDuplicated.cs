using CourseManagement.Application.Contracts.Course;
using CourseManagement.Application.Contracts.Dto;
using CourseManagement.Application.Contracts.Exceptions;
using CourseManagement.Domain.CourseAgg;
using CourseManagement.Infrastructure.EFCore;
using FluentAssertions;
using Student.Specs.Infrastructure;
using Student.Test.Tools.CourseTestTools;
using Xunit;

namespace Student.Specs.CourseTests.Add;

public class FailedWhenCourseNameIsDuplicated : EFDataContextDatabaseFixture
{
    private readonly CourseManagementContext _context;
    private readonly ICourseApplication _sut;
    private AddCourseDto _dto;
    private Course _course;
    private Func<Task> _actualResult;

    public FailedWhenCourseNameIsDuplicated(
        ConfigurationFixture configuration)
        : base(configuration)
    {
        _context = CreateDataContext();
        _sut = CourseServiceFactory.GenerateCourseService(_context);
    }

    [BDDHelper.Given("درسی با عنوان ‘ریاضی مهندسی ‘" +
                     " در سیستم موجود می باشد.")]
    private void Given()
    {

        _course = new CourseDtoBuilder()
            .WithName("ریاضی مهندسی")
            .Build();
        _context.SaveChanges();
    }

    [BDDHelper.When("یک درس با عنوان ‘ریاضی مهندسی’ " +
                    "در سیستم ثبت می کنم.")]
    private void When()
    {
        _dto = new AddCourseDtoBuilder()
            .WithName("ریاضی مهندسی")
            .Build();

        _actualResult = async () => _sut.Add(_dto);
    }

    [BDDHelper.Then("پیغام خطایی با عنوان ‘نام درس تکراری می باشد’" +
                    " به کاربر نمایش می دهد.")]
    private async Task Then()
    {
        _actualResult.Should()
            .ThrowExactlyAsync<CourseDuplicatedNameException>();
    }

    [Fact]
    public void Run()
    {
        BDDHelper.Runner.RunScenario(
            _ => Given(),
            _ => When(),
            _ => Then().Wait()
        );
    }
}