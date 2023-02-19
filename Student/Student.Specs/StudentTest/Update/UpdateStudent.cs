using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Student.Specs.Infrastructure;
using Student.Test.Tools.StudentTestTools;
using StudentManagement.Application.Contracts.Dto;
using StudentManagement.Application.Contracts.Student;
using StudentManagement.Infrastructure.EFCore;
using Xunit;
using Student.UnitTests;


namespace Student.Specs.StudentTest.Update;

public class UpdateStudent : EFDataContextDatabaseFixture
{
    private readonly StudentManagementContext _context;
    private readonly IStudentApplication _sut;
    private UpdateStudentDto _dto;
    private StudentManagement.Domain.StudentAgg.Student _student;

    public UpdateStudent(ConfigurationFixture configuration) : base(configuration)
    {
        _context = CreateDataContext();
        _sut = StudentServiceFactory.GenerateStudentService(_context);
    }

    [BDDHelper.Given("یک دانشجو با نام" +
                     " ‘ سعید انصاری’ به تاریخ تولد ‘1369’" +
                     " و شماره شناسنامه ‘2280509504 ‘ " +
                     "با رشته تحصیلی ‘کامپیوتر’ وجود دارد.")]
    private void Given()
    {
        _student = new StudentBuilder().WithFirstName("سعید").WithLastName("انصاری")
            .Build();
        _context.Manipulate(_ => _.Add(_student));
    }

    [BDDHelper.When("نام دانشجو به " +
                    "‘محمدرضا انصاری’ به تاریخ تولد ‘1369’" +
                    " و شماره شناسنامه ‘2280509504 ‘ " +
                    "با رشته تحصیلی ‘کامپیوتر’ ویرایش می کنم.")]
    private async Task When()
    {
        _dto = new UpdateStudentDtoBuilder()
            .WithFirstName("محمدرضا")
            .WithLastName("انصاری")
            .Build();

        _sut.Update(_dto, _student.Id);
    }

    [BDDHelper.Then("تنها یک دانشجو با نام " +
                    " ‘محمدرضا انصاری’ به تاریخ تولد ‘1369’" +
                    " و شماره شناسنامه ‘2280509504 ‘ " +
                    "با رشته تحصیلی ‘کامپیوتر’ در سیستم موجود می باشد.")]
    private async Task Then()
    {
        var actualResult = await _context
            .Student.FirstOrDefaultAsync();
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