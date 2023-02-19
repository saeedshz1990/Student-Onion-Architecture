using FluentAssertions;
using Student.Specs.Infrastructure;
using StudentManagement.Application.Contracts.Dto;
using StudentManagement.Application.Contracts.Student;
using StudentManagement.Infrastructure.EFCore;
using Xunit;
using Student.Test.Tools.StudentTestTools;
using Student.UnitTests;
using StudentManagement.Application.Contracts.Exceptions;

namespace Student.Specs.StudentTest.Add;

public class FailedWhenStudentIsExist : EFDataContextDatabaseFixture
{
    private readonly StudentManagementContext _context;
    private readonly IStudentApplication _sut;
    private AddStudentDto _dto;
    private StudentManagement.Domain.StudentAgg.Student _student;
    private Func<Task> _actualResult;

    public FailedWhenStudentIsExist(
        ConfigurationFixture configuration) : base(configuration)
    {
        _context = CreateDataContext();
        _sut = StudentServiceFactory.GenerateStudentService(_context);
    }

    [BDDHelper.Given("دانشجویی با نام ‘ سعید انصاری’ به " +
                     "تاریخ تولد ‘1369’ و شماره شناسنامه ‘2280509504 ‘" +
                     " وجود دارد.")]
    private void Given()
    {
        _student = new StudentBuilder()
            .WithFirstName("سعید")
            .WithLastName("انصاری")
            .Build();
        _context.Manipulate(_ => _.Add(_student));
    }

    [BDDHelper.When("یک دانشجو با نام" +
                    " ‘ سعید انصاری’ " +
                    "به تاریخ تولد ‘1369’ و " +
                    "شماره شناسنامه ‘2280509504 ‘ " +
                    " در سیستم ثبت می کنم.")]
    private async Task When()
    {
        _dto = new AddStudentDtoBuilder()
            .WithFirstName("سعید")
            .WithLastName("انصاری")
            .Build();

        _actualResult = async () =>  _sut.Add(_dto);
    }

    [BDDHelper.Then("یک پیغام خطا با نام ‘دانشجو در سیستم وجود دارد’ به کاربر نمایش دهد.")]
    private async Task Then()
    {
        await _actualResult.Should().ThrowExactlyAsync<StudentDuplicatedNationalCodeException>();
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