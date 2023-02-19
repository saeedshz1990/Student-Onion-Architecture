
using TeacherManagement.Application.Contracts.Dto;

namespace Student.Test.Tools.TeacherTestTools;

public class UpdateTeacherDtoBuilder
{
    private readonly UpdateTeacherDto _dto;

    public UpdateTeacherDtoBuilder()
    {
        _dto = new UpdateTeacherDto
        {
            FirstName = "dummyFirst",
            LastName = "dummyLast",
            MobilePhone = "09177877225",
            NationalNumber = "dummyNum",
            YearBirth = 1369,
        };
    }

    public UpdateTeacherDtoBuilder WithFirstName(string firstName)
    {
        _dto.FirstName = firstName;
        return this;
    }

    public UpdateTeacherDtoBuilder WithLastName(string lastName)
    {
        _dto.LastName = lastName;
        return this;
    }

    public UpdateTeacherDtoBuilder WithMobilePhone(string mobilePhone)
    {
        _dto.MobilePhone = mobilePhone;
        return this;
    }

    public UpdateTeacherDtoBuilder WithNationalNumber(string number)
    {
        _dto.NationalNumber = number;
        return this;
    }

    public UpdateTeacherDtoBuilder WithYearBirth(int birth)
    {
        _dto.YearBirth = birth;
        return this;
    }

    public UpdateTeacherDtoBuilder WithMobileNumber(string mobile)
    {
        _dto.MobilePhone = mobile;
        return this;
    }

    public UpdateTeacherDto Build()
    {
        return _dto;
    }
}