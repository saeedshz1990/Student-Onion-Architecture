using CourseManagement.Application.Contracts.Course;
using CourseManagement.Application.Contracts.Dto;
using CourseManagement.Domain.CourseAgg;
using CourseManagement.Infrastructure.EFCore;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Student.Specs.Infrastructure;
using Student.Test.Tools.CourseTestTools;
using Xunit;

namespace Student.Specs.CourseTests.Add;

public class AddCourse : EFDataContextDatabaseFixture
{
    private readonly CourseManagementContext _context;
    private readonly ICourseApplication _sut;
    private AddCourseDto _dto;
    private Course _course;

    public AddCourse(ConfigurationFixture configuration) : base(configuration)
    {
        _context = CreateDataContext();
        _sut = CourseServiceFactory.GenerateCourseService(_context);
    }

    [BDDHelper.Given("هیچ درسی در سیستم ثبت نشده است")]
    private void Given()
    {
    }

    [BDDHelper.When("یک درس با عنوان ‘ریاضی مهندسی’ در سیستم ثبت می کنم.")]
    private async Task When()
    {
        _dto = new AddCourseDtoBuilder()
            .WithName("ریاضی مهندسی")
            .Build();

         _sut.Add(_dto);
    }

    [BDDHelper.Then("بنابراین یک درس با " +
                    "عنوان ‘ریاضی مهندسی ‘ " +
                    "باید در سیستم وجود داشته باشد.")]
    private async Task Then()
    {
        var actualResult = await _context.Course.FirstOrDefaultAsync();
        actualResult!.Name.Should().Be(_dto.Name);
        actualResult!.Description.Should().Be(_dto.Description);
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