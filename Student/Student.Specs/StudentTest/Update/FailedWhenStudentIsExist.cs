using FluentAssertions;
using Student.Specs.Infrastructure;
using StudentManagement.Application.Contracts.Dto;
using Student.UnitTests;
using Xunit;
using StudentManagement.Infrastructure.EFCore;
using StudentManagement.Application.Contracts.Student;
using Student.Test.Tools.StudentTestTools;
using StudentManagement.Application.Contracts.Exceptions;

namespace Student.Specs.StudentTest.Update;

public class FailedWhenStudentIsExist : EFDataContextDatabaseFixture
{
    private readonly StudentManagementContext _context;
    private readonly IStudentApplication _sut;
    private UpdateStudentDto _dto;
    private StudentManagement.Domain.StudentAgg.Student _student;
    private StudentManagement.Domain.StudentAgg.Student _secondStudent;
    private Func<Task> _actualResult;

    public FailedWhenStudentIsExist(ConfigurationFixture configuration)
        : base(configuration)
    {
        _context = CreateDataContext();
        _sut = StudentServiceFactory.GenerateStudentService(_context);
    }

    [BDDHelper.Given("دانشجویی با نام ‘ سعید انصاری’" +
                     " با تاریخ تولد ‘1369’" +
                     " و شماره شناسنامه ‘2280509504 ‘" +
                     " وجود دارد .")]
    [BDDHelper.And("دانشجویی با نام" +
                   " ‘ حسین محمدیان’ با تاریخ تولد ‘1380’" +
                   " و شماره شناسنامه ‘2291006572 ‘ " +
                   "با رشته تحصیلی ‘کامپیوتر’ در سیستم وجود دارد .")]
    private void Given()
    {
        _student = new StudentBuilder()
            .WithFirstName("سعید")
            .WithLastName("انصاری")
            .Build();
        _context.Manipulate(_ => _.Add(_student));
        _secondStudent = new StudentBuilder()
            .WithFirstName("حسین")
            .WithLastName("محمدیان")
            .Build();
        _context.Manipulate(_ => _.Add(_secondStudent));
    }

    [BDDHelper.When("نام دانشجو به ‘سعید انصاری’" +
                    " به تاریخ تولد ‘1369’" +
                    " و شماره شناسنامه ‘2291006572 ‘ " +
                    "با رشته تحصیلی ‘کامپیوتر’ ویرایش می کنم.")]
    private async Task When()
    {
        _dto = new UpdateStudentDtoBuilder()
            .WithFirstName("محمدرضا")
            .WithLastName("انصاری")
            .Build();

        _actualResult = async () =>  _sut.Update(_dto, _student.Id);
    }

    [BDDHelper.Then("یک پیغام خطا با نام " +
                    "‘دانشجو در سیستم وجود دارد’ به کاربر نمایش دهد.")]
    private async Task Then()
    {
        await _actualResult.Should()
            .ThrowExactlyAsync<StudentDuplicatedNationalCodeException>();
    }

    [Fact(Skip = "Not implementing")]
    public void Run()
    {
        BDDHelper.Runner.RunScenario(
            _ => Given(),
            _ => When().Wait(),
            _ => Then().Wait()
        );
    }
}