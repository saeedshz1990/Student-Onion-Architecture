using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Student.Specs.Infrastructure;
using Student.Test.Tools.StudentTestTools;
using StudentManagement.Application.Contracts.Student;
using StudentManagement.Infrastructure.EFCore;
using Student.UnitTests;
using Xunit;

namespace Student.Specs.StudentTest.Delete;

public class DeleteStudent : EFDataContextDatabaseFixture
{
    private readonly StudentManagementContext _context;
    private readonly IStudentApplication _sut;
    private StudentManagement.Domain.StudentAgg.Student _student;

    public DeleteStudent(ConfigurationFixture configuration)
        : base(configuration)
    {
        _context = CreateDataContext();
        _sut = StudentServiceFactory.GenerateStudentService(_context);
    }

    [BDDHelper.Given("یک دانشجو با نام ‘ سعید انصاری’" +
                     " به تاریخ تولد ‘1369’ " +
                     "و شماره شناسنامه" +
                     " ‘2280509504 ‘ وجود دارد.")]
    private void Given()
    {
        _student = new StudentBuilder()
            .WithFirstName("سعید")
            .WithLastName("انصاری")
            .Build();
        _context.Manipulate(_ => _.Add(_student));
    }

    [BDDHelper.When("دانشجو با نام ‘ سعید انصاری’" +
                    " به تاریخ تولد ‘1369’ " +
                    "و شماره شناسنامه ‘2280509504 ‘" +
                    "از سیستم حذف می کنم")]
    private async Task When()
    {
         _sut.Delete(_student.Id);
    }

    [BDDHelper.Then("نباید دانشجویی با نام ‘ سعید انصاری’" +
                    " به تاریخ تولد ‘1369’ " +
                    "و شماره شناسنامه ‘2280509504 ‘" +
                    "در سیستم وجود داشته باشد.")]
    private async Task Then()
    {
        var actualResult = await _context.Student.ToListAsync();
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