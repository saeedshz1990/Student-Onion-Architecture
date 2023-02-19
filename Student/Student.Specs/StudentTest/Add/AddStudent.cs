using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Student.Specs.Infrastructure;
using Student.Test.Tools.StudentTestTools;
using StudentManagement.Application.Contracts.Dto;
using StudentManagement.Application.Contracts.Student;
using StudentManagement.Infrastructure.EFCore;
using Xunit;

namespace Student.Specs.StudentTest.Add;

public class AddStudent : EFDataContextDatabaseFixture
{
    private readonly StudentManagementContext _context;
    private readonly IStudentApplication _sut;
    private AddStudentDto _dto;

    public AddStudent(ConfigurationFixture configuration) : base(configuration)
    {
        _context = CreateDataContext();
        _sut = StudentServiceFactory.GenerateStudentService(_context);
    }

    [BDDHelper.Given("هیچ دانشجویی در سیستم ثبت نشده است")]
    private void Given()
    {
    }

    [BDDHelper.When("یک دانشجو با نام ‘ سعید انصاری’" +
                    " به تاریخ تولد ‘1369’" +
                    " و شماره شناسنامه ‘2280509504 ‘ با " +
                    " در سیستم ثبت می کنم.")]
    private async Task When()
    {
        _dto = new AddStudentDtoBuilder()
            .WithFirstName("سعید")
            .WithLastName("انصاری")
            .Build();

        _sut.Add(_dto);
    }

    [BDDHelper.Then("تنها یک دانشجو با نام" +
                    " ‘ سعید انصاری’ به تاریخ تولد ‘1369’ و " +
                    "شماره شناسنامه ‘2280509504 ‘ با رشته تحصیلی" +
                    " ‘کامپیوتر’ باید وجود داشته باشد.")]
    private async Task Then()
    {
        var actualResult = await _context.Student.FirstOrDefaultAsync();
        actualResult!.FirstName.Should().Be(_dto.FirstName);
        actualResult.LastName.Should().Be(_dto.LastName);
        actualResult.YearBirth.Should().Be(_dto.YearBirth);
        actualResult.MobilePhone.Should().Be(_dto.MobilePhone);
        actualResult.NationalNumber.Should().Be(_dto.NationalNumber);
        actualResult.TeacherId.Should().Be(_dto.TeacherId);
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