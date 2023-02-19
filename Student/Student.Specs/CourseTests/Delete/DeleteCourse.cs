using CourseManagement.Application.Contracts.Course;
using CourseManagement.Domain.CourseAgg;
using CourseManagement.Infrastructure.EFCore;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Student.Specs.Infrastructure;
using Student.Test.Tools.CourseTestTools;
using Xunit;

namespace Student.Specs.CourseTests.Delete;

public class DeleteCourse : EFDataContextDatabaseFixture
{
    private readonly CourseManagementContext _context;
    private readonly ICourseApplication _sut;
    private Course _course;

    public DeleteCourse(ConfigurationFixture configuration) : base(configuration)
    {
        _context = CreateDataContext();
        _sut = CourseServiceFactory.GenerateCourseService(_context);
    }

    [BDDHelper.Given("درسی با عنوان ‘ریاضی مهندسی’ وجود دارد.")]
    private void Given()
    {
        _course = new CourseDtoBuilder()
            .WithName("ریاضی مهندسی")
            .Build();
        _context.SaveChanges();
    }

    [BDDHelper.When("درسی با عنوان ‘ ریاضی مهندسی’ را حذف می کنیم.")]
    private async Task When()
    {
         _sut.Delete(_course.Id);
    }

    [BDDHelper.Then("بنابراین هیچ درسی با عنوان  ‘ریاضی مهندسی’ در سیستم وجود ندارد.")]
    private async Task Then()
    {
        var actualResult = await _context.Course.ToListAsync();
        actualResult.Should().HaveCount(0);
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