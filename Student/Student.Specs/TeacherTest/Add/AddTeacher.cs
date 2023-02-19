using CourseManagement.Domain.CourseAgg;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Student.Specs.Infrastructure;
using Student.Test.Tools.CourseTestTools;
using Student.Test.Tools.TeacherTestTools;
using TeacherManagement.Application.Contracts.Dto;
using TeacherManagement.Application.Contracts.Teacher;
using TeacherManagement.Infrastructure.EFCore;
using Xunit;
using Student.UnitTests;

namespace Student.Specs.TeacherTest.Add;

public class AddTeacher : EFDataContextDatabaseFixture
{
    private readonly TeacherManagementContext _context;
    private readonly ITeacherApplication _sut;
    private Course _course;
    private AddTeacherDto _dto;

    public AddTeacher(ConfigurationFixture configuration) : base(configuration)
    {
        _context = CreateDataContext();
        _sut = TeacherServiceFactory.GenerateTeacherService(_context);
    }

    [BDDHelper.Given("هیچ استادی در سیستم ثبت نشده است")]
    private void Given()
    {
        _course = new CourseDtoBuilder()
            .WithName("ریاضی مهندسی")
            .Build();
        _context.Manipulate(_ => _.Add(_course));
    }

    [BDDHelper.When("یک استاد با نام ‘آرش چناری’با" +
                    " مدرک تحصیلی ‘کارشناسی ارشد’ گرایش" +
                    " ‘مهدسی نرم افزار’ " +
                    "با کد ملی ‘2294321905’ ثبت می نمایم. ")]
    private async Task When()
    {
        _dto = new AddTeacherDtoBuilder()
            .WithFirstName("آرش")
            .WithLastName("چناری")
            .Build();

         _sut.Add(_dto);
    }

    [BDDHelper.Then("تنها یک استادی با نام " +
                    "‘ آرش چناری’ با مدرک تحصیلی ‘کارشناسی ارشد’ " +
                    "گرایش ‘مهدسی نرم افزار’ با کد ملی ‘2294321905’" +
                    " در سیستم باید وجود داشته باشد.")]
    private async Task Then()
    {
        var actualResult = await _context.Teacher.FirstOrDefaultAsync();
        actualResult!.FirstName.Should().Be(_dto.FirstName);
        actualResult.LastName.Should().Be(_dto.LastName);
        actualResult.YearBirth.Should().Be(_dto.YearBirth);
        actualResult.MobilePhone.Should().Be(_dto.MobilePhone);
        actualResult.NationalNumber.Should().Be(_dto.NationalNumber);
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