using StudentManagement.Application.Contracts.Dto;

namespace Student.Test.Tools.StudentTestTools;

public class UpdateStudentDtoBuilder
{
    private readonly UpdateStudentDto _dto;

    public UpdateStudentDtoBuilder()
    {
        _dto = new UpdateStudentDto
        {
            FirstName = "dummyFirst",
            LastName = "dummyLast",
            MobilePhone = "09177877225",
            NationalNumber = "dummyNum",
            YearBirth = 1369,
            TeacherId = 1
        };
    }

    public UpdateStudentDtoBuilder WithFirstName(string firstName)
    {
        _dto.FirstName = firstName;
        return this;
    }

    public UpdateStudentDtoBuilder WithLastName(string lastName)
    {
        _dto.LastName = lastName;
        return this;
    }

    public UpdateStudentDtoBuilder WithMobilePhone(string mobilePhone)
    {
        _dto.MobilePhone = mobilePhone;
        return this;
    }

    public UpdateStudentDtoBuilder WithNationalNumber(string number)
    {
        _dto.NationalNumber = number;
        return this;
    }

    public UpdateStudentDtoBuilder WithYearBirth(int birth)
    {
        _dto.YearBirth = birth;
        return this;
    }

    public UpdateStudentDtoBuilder WithTeacherId(int teacherId)
    {
        _dto.TeacherId = teacherId;
        return this;
    }

    public UpdateStudentDtoBuilder WithMobileNumber(string mobile)
    {
        _dto.MobilePhone = mobile;
        return this;
    }

    public UpdateStudentDto Build()
    {
        return _dto;
    }
}