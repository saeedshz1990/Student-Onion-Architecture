using CourseManagement.Domain.CourseAgg;
using FluentAssertions;
using Student.Specs.Infrastructure;
using Student.Test.Tools.TeacherTestTools;
using TeacherManagement.Application.Contracts.Dto;
using TeacherManagement.Application.Contracts.Teacher;
using TeacherManagement.Domain.TeacherAgg;
using TeacherManagement.Infrastructure.EFCore;
using Xunit;
using Student.UnitTests;
using Student.Test.Tools.CourseTestTools;
using TeacherManagement.Application.Contracts.Exceptions;

namespace Student.Specs.TeacherTest.Add;

public class FailedWhenTeacherNamesDuplicated : EFDataContextDatabaseFixture
{
    private readonly TeacherManagementContext _context;
    private readonly ITeacherApplication _sut;
    private AddTeacherDto _dto;
    private Teacher _teacher;
    private Course _course;
    private Func<Task> _actualResult;

    public FailedWhenTeacherNamesDuplicated(
        ConfigurationFixture configuration)
        : base(configuration)
    {
        _context = CreateDataContext();
        _sut = TeacherServiceFactory.GenerateTeacherService(_context);
    }

    [BDDHelper.Given("استادی با نام ‘آرش چناری’با" +
                     " مدرک تحصیلی ‘کارشناسی ارشد’ گرایش" +
                     " ‘مهدسی نرم افزار’ با کد" +
                     " ملی ‘2294321905’ در سیستم وجود دارد.")]
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

    [BDDHelper.When("یک استاد با نام ‘آرش چناری’با مدرک تحصیلی " +
                    "‘کارشناسی ارشد’ گرایش ‘مهدسی نرم افزار’ " +
                    "با کد ملی ‘2294321905’ ثبت می نمایم. ")]
    private async Task When()
    {
        _dto = new AddTeacherDtoBuilder()
            .WithFirstName("آرش")
            .WithLastName("چناری")
            .Build();

        _actualResult = async () =>  _sut.Add(_dto);
    }

    [BDDHelper.Then("پیغام خطایی با نام  ‘این استاد در سیستم" +
                    " وجود دارد’ به کاربر نمایش می دهد.")]
    private async Task Then()
    {
        await _actualResult.Should()
            .ThrowExactlyAsync<TeacherDuplicatedNationalCodeException>();
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