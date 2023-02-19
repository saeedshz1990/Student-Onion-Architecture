using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Student.Specs.Infrastructure;
using Student.Test.Tools.TeacherTestTools;
using TeacherManagement.Application.Contracts.Dto;
using Xunit;
using Student.UnitTests;
using TeacherManagement.Domain.TeacherAgg;
using TeacherManagement.Infrastructure.EFCore;
using TeacherManagement.Application.Contracts.Teacher;

namespace Student.Specs.TeacherTest.Update;

public class UpdateTeacher : EFDataContextDatabaseFixture
{
    private readonly TeacherManagementContext _context;
    private readonly ITeacherApplication _sut;
    private Teacher _teacher;
    private UpdateTeacherDto _dto;

    public UpdateTeacher(ConfigurationFixture configuration) : base(configuration)
    {
        _context = CreateDataContext();
        _sut = TeacherServiceFactory.GenerateTeacherService(_context);
    }

    [BDDHelper.Given("استادی با نام ‘آرش چناری’با مدرک تحصیلی" +
                     " ‘کارشناسی ارشد’ گرایش ‘مهدسی نرم افزار’" +
                     " با کد ملی ‘2294321905’ وجود دارد.")]
    private void Given()
    {
        _teacher = new TeacherBuilder()
            .WithFirstName("آرش")
            .WithLastName("چناری")
            .Build();
        _context.Manipulate(_ => _.Add(_teacher));
    }

    [BDDHelper.When("استادی با نام ‘آرش چناری’ با مدرک تحصیلی" +
                    " ‘کارشناسی ارشد’ به گرایش ‘هوش مصنوعی’" +
                    " با کد ملی ‘2294321905’ ویرایش می کنم.")]
    private async Task When()
    {
        _dto = new UpdateTeacherDtoBuilder()
            .WithFirstName("آرش")
            .WithLastName("چناری")
            .Build();

        _sut.Update(_dto, _teacher.Id);
    }

    [BDDHelper.Then("تنها یک استاد با نام ‘آرش چناری’ " +
                    "با مدرک تحصیلی ‘کارشناسی ارشد’" +
                    " به گرایش ‘هوش مصنوعی’ با کد ملی ‘2294321905’" +
                    " در سیتسم وجود داشته باشد.")]
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