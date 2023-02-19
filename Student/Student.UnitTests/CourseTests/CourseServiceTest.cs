using _0_Framework;
using CourseManagement.Application.Contracts.Course;
using CourseManagement.Application.Contracts.Dto;
using CourseManagement.Application.Contracts.Exceptions;
using CourseManagement.Infrastructure.EFCore;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Student.Test.Tools.CourseTestTools;
using Xunit;

namespace Student.UnitTests.CourseTests;

public class CourseServiceTest
{
    private readonly CourseManagementContext _context;
    private readonly ICourseApplication _sut;

    public CourseServiceTest()
    {
        _context = new EFInMemoryDatabase().CreateDataContext<CourseManagementContext>();
        _sut = CourseServiceFactory.GenerateCourseService(_context);
    }

    [Fact]
    public void Add_add_Course_properly()
    {
        var dto = new AddCourseDtoBuilder()
            .Build();

        _sut.Add(dto);

        var actualResult = _context.Course.FirstOrDefault();
        actualResult!.Name.Should().Be(dto.Name);
        actualResult!.Description.Should().Be(dto.Description);
    }

    [Fact]
    public async Task Add_throw_exception_when_course_name_is_repeated_for_same_teacher()
    {
        var dto = new AddCourseDtoBuilder()
            .Build();

        var actualResult = () => _sut.Add(dto);

        actualResult.Should()
            .ThrowExactly<CourseDuplicatedNameException>();
    }


    [Fact]
    public async Task Update_update_course_properly()
    {
        var course = new CourseDtoBuilder()
            .Build();
        _context.Manipulate(_ => _.Add(course));
        var dto = new UpdateCourseDto
        {
            Name = "updatedDummy",
        };

        _sut.Update(dto, course.Id);

        var actualResult = await _context.Course.FirstOrDefaultAsync();
        actualResult!.Name.Should().Be(dto.Name);
    }

    [Theory]
    [InlineData(-1)]
    public async Task Update_throw_exception_When_course_notfound_properly(int invalidId)
    {
        var dto = new UpdateCourseDto
        {
            Name = "updatedDummy",
        };
        var actualResult = () => _sut.Update(dto, invalidId);

        actualResult.Should()
           .ThrowExactly<CourseNotFoundException>();
    }

    [Fact]
    public async Task Update_throw_exception_when_course_name_is_duplicated_properly()
    {
        var firstCourse = new CourseDtoBuilder().Build();
        _context.Manipulate(_ => _.Add(firstCourse));
        var secondCourse = new CourseDtoBuilder()
            .WithName("secondDummy")
            .Build();
        _context.Manipulate(_ => _.Add(secondCourse));
        var dto = new UpdateCourseDtoBuilder()
            .WithName("secondDummy")
            .Build();

        var actualResult = () => _sut.Update(dto, firstCourse.Id);

        actualResult.Should()
           .ThrowExactly<CourseNotFoundException>();
    }

    [Fact]
    public async Task Get_get_all_course_properly()
    {
        var firstDummy = new CourseDtoBuilder()
            .WithName("dummy")
            .Build();
        _context.Manipulate(_ => _.Add(firstDummy));
        var secondDummy = new CourseDtoBuilder()
            .WithName("  new dummy")
            .Build();
        _context.Manipulate(_ => _.Add(secondDummy));

        _sut.GetAll();

        var actualResult = await _context.Course.ToListAsync();
        actualResult.Should().HaveCount(2);
    }

    [Fact]
    public async Task Get_get_all_when_not_any_courses_added_properly()
    {
        _sut.GetAll();

        var actualResult = await _context.Course.ToListAsync();
        actualResult.Should().HaveCount(0);
    }

    [Fact]
    public async Task Get_get_by_id_course_properly()
    {
        var firstDummy = new CourseDtoBuilder()
            .WithName("dummy")
            .Build();
        _context.SaveChanges();
        var secondDummy = new CourseDtoBuilder()
            .WithName("new dummy")
            .Build();
        _context.SaveChanges();

        _sut.GetById(secondDummy.Id);

        var actualResult = await _context.Course
            .FirstOrDefaultAsync(_ => _.Id == secondDummy.Id);
        actualResult!.Name.Should().Be(secondDummy.Name);
    }


    [Fact]
    public async Task Delete_delete_course_properly()
    {
        var course = new CourseDtoBuilder()
            .WithName("dummy")
            .Build();
        _context.Manipulate(_ => _.Add(course));

        _sut.Delete(course.Id);

        var actualResult = await _context.Course.ToListAsync();
        actualResult.Should().HaveCount(0);
    }

    [Theory]
    [InlineData(-1)]
    public async Task Delete_throw_exception_when_course_notFound_properly(int invalidId)
    {
        var actualResult = () => _sut.Delete(invalidId);

         actualResult.Should()
            .ThrowExactly<CourseNotFoundException>();
    }
}