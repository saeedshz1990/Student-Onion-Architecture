using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Student.Test.Tools.StudentTestTools;
using StudentManagement.Application.Contracts.Exceptions;
using StudentManagement.Application.Contracts.Student;
using StudentManagement.Infrastructure.EFCore;
using Xunit;

namespace Student.UnitTests.StudentTests;

public class StudentServiceTest
{
    private readonly StudentManagementContext _context;
    private readonly IStudentApplication _sut;

    public StudentServiceTest()
    {
        _context = new EFInMemoryDatabase().CreateDataContext<StudentManagementContext>();
        _sut = StudentServiceFactory.GenerateStudentService(_context);
    }

    [Fact]
    public async Task Add_add_student_properly()
    {
        var dto = new AddStudentDtoBuilder()
            .Build();

        _sut.Add(dto);

        var actualResult = await _context.Student.FirstOrDefaultAsync();
        actualResult!.FirstName.Should().Be(dto.FirstName);
        actualResult.LastName.Should().Be(dto.LastName);
        actualResult.YearBirth.Should().Be(dto.YearBirth);
        actualResult.MobilePhone.Should().Be(dto.MobilePhone);
        actualResult.NationalNumber.Should().Be(dto.NationalNumber);
        actualResult.TeacherId.Should().Be(dto.TeacherId);
    }

    [Fact]
    public async Task Add_throw_exception_when_student_is_exist_properly()
    {
        var student = new StudentBuilder()
            .Build();
        _context.Manipulate(_ => _.Add(student));
        var dto = new AddStudentDtoBuilder()
            .Build();

        var actualResult = () => _sut.Add(dto);

        actualResult.Should().ThrowExactly<StudentNotFoundException>();
    }

    [Fact]
    public async Task Update_update_student_properly()
    {
        var student = new StudentBuilder()
            .Build();
        _context.Manipulate(_ => _.Add(student));
        var dto = new UpdateStudentDtoBuilder()
            .WithFirstName("updatedDummy")
            .WithLastName("updatedLast")
            .Build();

        _sut.Update(dto, student.Id);

        var actualResult = await _context.Student
            .FirstOrDefaultAsync(_ => _.Id == student.Id);
        actualResult!.FirstName.Should().Be(dto.FirstName);
        actualResult.LastName.Should().Be(dto.LastName);
        actualResult.YearBirth.Should().Be(dto.YearBirth);
        actualResult.MobilePhone.Should().Be(dto.MobilePhone);
        actualResult.NationalNumber.Should().Be(dto.NationalNumber);
        actualResult.TeacherId.Should().Be(dto.TeacherId);
    }

    [Theory]
    [InlineData(-1)]
    public async Task Update_throw_exception_when_student_not_exist_properly(int invalidId)
    {
        var dto = new UpdateStudentDtoBuilder()
            .Build();

        var actualResult = () => _sut.Update(dto, invalidId);

        actualResult.Should().ThrowExactly<StudentNotFoundException>();
    }

    [Fact]
    public async Task Update_throw_exception_when_student_is_exist_properly()
    {
        var student = new StudentBuilder()
            .Build();
        _context.Manipulate(_ => _.Add(student));
        var secondStudent = new StudentBuilder()
            .Build();
        _context.Manipulate(_ => _.Add(secondStudent));
        var dto = new UpdateStudentDtoBuilder()
            .WithFirstName("secondDummy")
            .WithLastName("secondLast")
            .Build();

        var actualResult = () => _sut.Update(dto, student.Id);

        actualResult.Should().ThrowExactly<StudentDuplicatedNationalCodeException>();
    }

    [Fact]
    public async Task Get_get_all_student_properly()
    {
        var student = new StudentBuilder()
            .Build();
        _context.Manipulate(_ => _.Add(student));
        var secondStudent = new StudentBuilder()
            .Build();
        _context.Manipulate(_ => _.Add(secondStudent));

        _sut.GetAll();

        var actualResult = await _context.Student.ToListAsync();
        actualResult.Should().HaveCount(2);
    }

    [Fact]
    public async Task Get_get_all_not_any_student_exist_properly()
    {
        _sut.GetAll();

        var actualResult = await _context.Student.ToListAsync();
        actualResult.Should().HaveCount(0);
    }

    [Fact]
    public async Task Get_get_by_id_student_properly()
    {
        var student = new StudentBuilder()
            .Build();
        _context.Manipulate(_ => _.Add(student));
        var secondStudent = new StudentBuilder()
            .Build();
        _context.Manipulate(_ => _.Add(secondStudent));

        _sut.GetById(student.Id);

        var actualResult = _context.Student
            .FirstOrDefault(_ => _.Id == student.Id);

        actualResult!.FirstName.Should().Be(student.FirstName);
        actualResult.LastName.Should().Be(student.LastName);
        actualResult.YearBirth.Should().Be(student.YearBirth);
        actualResult.MobilePhone.Should().Be(student.MobilePhone);
        actualResult.NationalNumber.Should().Be(student.NationalNumber);
        actualResult.TeacherId.Should().Be(student.TeacherId);
    }

    [Fact]
    public async Task Delete_delete_student_properly()
    {
        var student = new StudentBuilder()
            .Build();
        _context.Manipulate(_ => _.Add(student));

        _sut.Delete(student.Id);

        var actualResult = await _context.Student.ToListAsync();
        actualResult.Should().HaveCount(0);
    }

    [Theory]
    [InlineData(-1)]
    public async Task Delete_throw_exception_when_student_not_found_properly(int invalidId)
    {
        var actualResult = () => _sut.Delete(invalidId);

        actualResult.Should().ThrowExactly<StudentNotFoundException>();
    }
}