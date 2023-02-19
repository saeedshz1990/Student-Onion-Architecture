using StudentManagement.Application.Contracts.Dto;

namespace Student.Test.Tools.StudentTestTools;

public class AddStudentDtoBuilder
{
    private readonly AddStudentDto _dto;

    public AddStudentDtoBuilder()
    {
        _dto = new AddStudentDto
        {
            FirstName = "dummyFirst",
            LastName = "dummyLast",
            MobilePhone = "09177877225",
            NationalNumber = "dummyNum",
            YearBirth = 1369,
            TeacherId = 1
        };
    }

    public AddStudentDtoBuilder WithFirstName(string firstName)
    {
        _dto.FirstName = firstName;
        return this;
    }

    public AddStudentDtoBuilder WithLastName(string lastName)
    {
        _dto.LastName = lastName;
        return this;
    }

    public AddStudentDtoBuilder WithMobilePhone(string mobilePhone)
    {
        _dto.MobilePhone = mobilePhone;
        return this;
    }

    public AddStudentDtoBuilder WithNationalNumber(string number)
    {
        _dto.NationalNumber = number;
        return this;
    }

    public AddStudentDtoBuilder WithYearBirth(int birth)
    {
        _dto.YearBirth = birth;
        return this;
    }

    public AddStudentDtoBuilder WithTeacherId(int teacherId)
    {
        _dto.TeacherId = teacherId;
        return this;
    }

    public AddStudentDtoBuilder WithMobileNumber(string mobile)
    {
        _dto.MobilePhone = mobile;
        return this;
    }

    public AddStudentDto Build()
    {
        return _dto;
    }
}