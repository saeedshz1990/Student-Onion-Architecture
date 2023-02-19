using CourseManagement.Application.Contracts.Course;
using CourseManagement.Application.Contracts.Dto;
using CourseManagement.Application.Contracts.Exceptions;
using CourseManagement.Domain.CourseAgg;
using CourseManagement.Infrastructure.EFCore;
using FluentAssertions;
using Student.Specs.Infrastructure;
using Student.Test.Tools.CourseTestTools;
using Xunit;

namespace Student.Specs.CourseTests.Update;

public class FailedWhenCourseNameIsDuplicated : EFDataContextDatabaseFixture
{
    private readonly CourseManagementContext _context;
    private readonly ICourseApplication _sut;
    private UpdateCourseDto _dto;
    private Course _firstCourse;
    private Course _secondCourse;
    private Func<Task> _actualResult;

    public FailedWhenCourseNameIsDuplicated(
        ConfigurationFixture configuration)
        : base(configuration)
    {
        _context = CreateDataContext();
        _sut = CourseServiceFactory.GenerateCourseService(_context);
    }

    [BDDHelper.Given("یک درس با عنوان ‘ریاضی مهندسی’ وجود دارد.")]
    [BDDHelper.And("یک درس با عنوان ‘مهندسی نرم افزار’ وجود دارد.")]
    private void Given()
    {
        _firstCourse = new CourseDtoBuilder()
            .WithName("ریاضی مهندسی")
            .Build();
        _context.SaveChanges();
        _secondCourse = new CourseDtoBuilder()
            .WithName("مهدسی نرم افزار")
            .Build();
        _context.SaveChanges();
    }

    [BDDHelper.When("درسی با عنوان ‘ریاضی مهندسی ‘ " +
                    "را به ‘مهندسی نرم افزار’ ویرایش می کنم.")]
    private async Task When()
    {
        _dto = new UpdateCourseDtoBuilder()
            .WithName("مهندسی نرم افزار")
            .Build();

        _actualResult = async () => _sut.Update(_dto, _firstCourse.Id);
    }

    [BDDHelper.Then("پیغام خطایی با عنوان" +
                    "  ‘ نام درس تکراری می باشد’" +
                    " به کاربر نمایش می دهد.")]
    private async Task Then()
    {
        await _actualResult.Should()
            .ThrowExactlyAsync<CourseDuplicatedNameException>();
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