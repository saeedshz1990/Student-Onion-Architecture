using CourseManagement.Domain.CourseAgg;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Student.Specs.Infrastructure;
using Student.Test.Tools.TeacherTestTools;
using TeacherManagement.Application.Contracts.Teacher;
using TeacherManagement.Domain.TeacherAgg;
using TeacherManagement.Infrastructure.EFCore;
using Xunit;
using Student.UnitTests;
using Student.Test.Tools.CourseTestTools;

namespace Student.Specs.TeacherTest.Delete;

public class DeleteTeacher : EFDataContextDatabaseFixture
{
    private readonly TeacherManagementContext _context;
    private readonly ITeacherApplication _sut;
    private Course _course;
    private Teacher _teacher;

    public DeleteTeacher(ConfigurationFixture configuration) : base(configuration)
    {
        _context = CreateDataContext();
        _sut = TeacherServiceFactory.GenerateTeacherService(_context);
    }

    [BDDHelper.Given("استادی با نام ‘آرش چناری’با مدرک تحصیلی ‘" +
                     "کارشناسی ارشد’ گرایش ‘مهدسی نرم افزار’" +
                     " با کد ملی ‘2294321905’ وجود دارد.")]
    private void Given()
    {
        _course = new CourseDtoBuilder().Build();
        _context.Manipulate(_ => _context.Add(_course));
        _teacher = new TeacherBuilder()
            .WithFirstName("آرش")
            .WithLastName("چناری")
            .Build();
        _context.Manipulate(_ => _.Add(_teacher));
    }

    [BDDHelper.When("استادی با نام ‘آرش چناری’با مدرک تحصیلی" +
                    " ‘کارشناسی ارشد’ گرایش ‘مهدسی نرم افزار’" +
                    " با کد ملی ‘2294321905’ را حذف می کنیم.")]
    private async Task When()
    {
         _sut.Delete(_teacher.Id);
    }

    [BDDHelper.Then("نباید هیچ استادی در سیستم وجود داشته باشد.")]
    private async Task Then()
    {
        var actualResult = await _context.Teacher.ToListAsync();
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