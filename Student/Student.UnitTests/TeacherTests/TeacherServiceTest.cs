using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Student.Test.Tools.TeacherTestTools;
using TeacherManagement.Application.Contracts.Exceptions;
using TeacherManagement.Application.Contracts.Teacher;
using TeacherManagement.Infrastructure.EFCore;
using Xunit;

namespace Student.UnitTests.TeacherTests;

public class TeacherServiceTest
{
    private readonly TeacherManagementContext _context;
    private readonly ITeacherApplication _sut;

    public TeacherServiceTest()
    {
        _context = new EFInMemoryDatabase().CreateDataContext<TeacherManagementContext>();
        _sut = TeacherServiceFactory.GenerateTeacherService(_context);
    }

    [Fact]
    public async Task Add_add_teacher_properly()
    {
        var dto = new AddTeacherDtoBuilder()

            .Build();

        _sut.Add(dto);

        var actualResult = await _context.Teacher.FirstOrDefaultAsync();
        actualResult!.FirstName.Should().Be(dto.FirstName);
        actualResult.LastName.Should().Be(dto.LastName);
        actualResult.YearBirth.Should().Be(dto.YearBirth);
        actualResult.MobilePhone.Should().Be(dto.MobilePhone);
        actualResult.NationalNumber.Should().Be(dto.NationalNumber);
    }

    [Fact]
    public async Task Add_throw_exception_When_teacher_name_is_exist_properly()
    {
        var teacher = new TeacherBuilder()
             .Build();
        _context.Manipulate(_ => _.Add(teacher));
        var dto = new AddTeacherDtoBuilder()
            .Build();

        var actualResult = () => _sut.Add(dto);

        actualResult.Should()
           .ThrowExactly<TeacherDuplicatedNationalCodeException>();
    }

    [Fact]
    public async Task Get_get_all_teacher_properly()
    {
        var teacher = new TeacherBuilder()
            .WithNationalNumber("94321905")
            .Build();
        _context.Manipulate(_ => _.Add(teacher));

        _sut.GetAll();

        var actualResult = await _context.Teacher.ToListAsync();
        actualResult.Should().HaveCount(1);
    }

    [Fact]
    public async Task Get_get_all_teacher_when_not_any_teacher_added_properly()
    {
        _sut.GetAll();

        var actualResult = await _context.Teacher.ToListAsync();
        actualResult.Should().HaveCount(0);
    }

    [Fact]
    public async Task Get_get_by_id_teacher_properly()
    {
        var teacher = new TeacherBuilder()
            .Build();
        _context.Manipulate(_ => _.Add(teacher));

        _sut.GetById(teacher.Id);

        var actualResult = await _context.Teacher.FirstOrDefaultAsync();
        actualResult!.FirstName.Should().Be(teacher.FirstName);
        actualResult.LastName.Should().Be(teacher.LastName);
        actualResult.YearBirth.Should().Be(teacher.YearBirth);
        actualResult.MobilePhone.Should().Be(teacher.MobilePhone);
        actualResult.NationalNumber.Should().Be(teacher.NationalNumber);
    }

    [Fact]
    public async Task Delete_delete_teacher_properly()
    {

        var teacher = new TeacherBuilder()
            .Build();
        _context.Manipulate(_ => _.Add(teacher));

        _sut.Delete(teacher.Id);

        var actualResult = _context.Teacher.ToList();
        actualResult.Should().HaveCount(0);
    }

    [Theory]
    [InlineData(-1)]
    public async Task Delete_throw_exception_when_teacher_not_found_properly(int invalidId)
    {
        var actualResult = () => _sut.Delete(invalidId);

        actualResult.Should()
           .ThrowExactly<TeacherNotFoundException>();
    }

    [Fact]
    public async Task Update_update_Teacher_properly()
    {
        var teacher = new TeacherBuilder()
            .Build();
        _context.Manipulate(_ => _.Add(teacher));

        var dto = new UpdateTeacherDtoBuilder()
            .WithFirstName("updatedName")
            .WithLastName("UpdatedLast")
            .Build();

        _sut.Update(dto, teacher.Id);

        var actualResult = await _context.Teacher.FirstOrDefaultAsync();
        actualResult!.FirstName.Should().Be(dto.FirstName);
        actualResult.LastName.Should().Be(dto.LastName);
        actualResult.YearBirth.Should().Be(dto.YearBirth);
        actualResult.MobilePhone.Should().Be(dto.MobilePhone);
        actualResult.NationalNumber.Should().Be(dto.NationalNumber);

    }

    [Theory]
    [InlineData(-1)]
    public async Task Update_throw_exception_when_teacher_not_found_properly(int invalidId)
    {
        var dto = new UpdateTeacherDtoBuilder()
            .Build();
        var actualResult = () => _sut.Update(dto, invalidId);

        actualResult.Should()
           .ThrowExactly<TeacherNotFoundException>();
    }

    [Fact]
    public async Task Update_throw_exception_teacher_is_exist_properly()
    {
        var teacher = new TeacherBuilder()
            .Build();
        _context.Manipulate(_ => _.Add(teacher));
        var secondTeacher = new TeacherBuilder()
            .WithFirstName("secondDummy")
            .WithLastName("secondLastDummy")
            .Build();
        _context.Manipulate(_ => _.Add(secondTeacher));

        var dto = new UpdateTeacherDtoBuilder()
            .WithFirstName("secondDummy")
            .WithLastName("secondLastDummy")
            .Build();

        var actualResult =  () =>  _sut.Update(dto, teacher.Id);

         actualResult.Should()
            .ThrowExactly<TeacherDuplicatedNationalCodeException>();
    }
}