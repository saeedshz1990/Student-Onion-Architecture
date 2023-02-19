using CourseManagement.Application.Contracts.Course;
using CourseManagement.Application.Contracts.Dto;
using CourseManagement.Domain.CourseAgg;
using CourseManagement.Infrastructure.EFCore;
using FluentAssertions;
using Student.Specs.Infrastructure;
using Student.Test.Tools.CourseTestTools;
using Xunit;

namespace Student.Specs.CourseTests.Update;

public class UpdateCourse : EFDataContextDatabaseFixture
{
    private readonly CourseManagementContext _context;
    private readonly ICourseApplication _sut;
    private UpdateCourseDto _dto;
    private Course _course;

    public UpdateCourse(ConfigurationFixture configuration) : base(configuration)
    {
        _context = CreateDataContext();
        _sut = CourseServiceFactory.GenerateCourseService(_context);
    }

    [BDDHelper.Given("یک درس با عنوان ‘ریاضی مهندسی’ در سیستم وجود دارد.")]
    private void Given()
    {
        _course = new CourseDtoBuilder()
            .WithName("ریاضی مهندسی")
            .Build();
        _context.SaveChanges();
    }

    [BDDHelper.When("درسی با عنوان ‘ریاضی مهندسی ‘ را به " +
                    "‘مهندسی نرم افزار’ ویرایش می کنم.")]
    private async Task When()
    {
        _dto = new UpdateCourseDtoBuilder()
            .WithName("مهندسی نرم افزار")
            .Build();

        _sut.Update(_dto, _course.Id);
    }

    [BDDHelper.Then("تنها یک درس با عنوان" +
                    " ‘مهندسی نرم افزار’ در سیستم وجود دارد.")]
    private async Task Then()
    {
        var actualResult = _context.Course.FirstOrDefault();
        actualResult!.Name.Should().Be(_dto.Name);

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