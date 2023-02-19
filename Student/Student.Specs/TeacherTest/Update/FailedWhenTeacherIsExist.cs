using FluentAssertions;
using Xunit;
using Student.UnitTests;
using Student.Specs.Infrastructure;
using TeacherManagement.Application.Contracts.Dto;
using TeacherManagement.Domain.TeacherAgg;
using CourseManagement.Domain.CourseAgg;
using Student.Test.Tools.TeacherTestTools;
using TeacherManagement.Application.Contracts.Teacher;
using TeacherManagement.Infrastructure.EFCore;
using Student.Test.Tools.CourseTestTools;
using TeacherManagement.Application.Contracts.Exceptions;

namespace Student.Specs.TeacherTest.Update;

public class FailedWhenTeacherIsExist : EFDataContextDatabaseFixture
{
    private readonly TeacherManagementContext _context;
    private readonly ITeacherApplication _sut;
    private Course _course;
    private Teacher _teacher;
    private Teacher _secondTeacher;
    private UpdateTeacherDto _dto;
    private Func<Task> _actualResult;

    public FailedWhenTeacherIsExist(
        ConfigurationFixture configuration)
        : base(configuration)
    {
        _context = CreateDataContext();
        _sut = TeacherServiceFactory.GenerateTeacherService(_context);
    }

    [BDDHelper.Given("استادی با نام ‘آرش چناری’با مدرک تحصیلی " +
                     "‘کارشناسی ارشد’ گرایش ‘مهدسی نرم افزار’" +
                     " با کد ملی ‘2294321905’ وجود دارد.")]
    [BDDHelper.And("استادی با نام ‘سعید انصاری ‘با مدرک تحصیلی" +
                   " ‘کارشناسی ‘ گرایش ‘مهدسی نرم افزار’ " +
                   "با کد ملی ‘2280509504’ وجود دارد.")]
    private void Given()
    {
        _course = new CourseDtoBuilder().Build();
        _context.Manipulate(_ => _context.Add(_course));
        _teacher = new TeacherBuilder()
            .WithFirstName("آرش")
            .WithLastName("چناری")
            .Build();
        _context.Manipulate(_ => _.Add(_teacher));
        _secondTeacher = new TeacherBuilder()
            .WithFirstName("سعید")
            .WithLastName("انصاری")
            .Build();
        _context.Manipulate(_ => _.Add(_secondTeacher));
    }

    [BDDHelper.When("استادی با نام ‘آرش چناری’ با مدرک تحصیلی" +
                    " ‘کارشناسی ارشد’ به گرایش ‘هوش مصنوعی’ " +
                    "با کد ملی ‘2280509504’ ویرایش می کنم.")]
    private async Task When()
    {
        _dto = new UpdateTeacherDtoBuilder()
            .WithFirstName("آرش")
            .WithLastName("چناری")
            .Build();

        _actualResult = async () =>  _sut.Update(_dto, _teacher.Id);
    }

    [BDDHelper.Then("پیغام خطایی با نام" +
                    " ‘استادی با این مشخصات در سیستم وجود دارد’ " +
                    "به کاربر نمایش میدهد.")]
    private async Task Then()
    {
        await _actualResult.Should()
            .ThrowExactlyAsync<TeacherDuplicatedNationalCodeException>();
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