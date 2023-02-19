using TeacherManagement.Application.Contracts.Dto;

namespace Student.Test.Tools.TeacherTestTools;

public class AddTeacherDtoBuilder
{
    private readonly AddTeacherDto _dto;

    public AddTeacherDtoBuilder()
    {
        _dto = new AddTeacherDto
        {
            FirstName = "dummyFirst",
            LastName = "dummyLast",
            MobilePhone = "09177877225",
            NationalNumber = "dummyNum",
            YearBirth = 1369,
        };
    }

    public AddTeacherDtoBuilder WithFirstName(string firstName)
    {
        _dto.FirstName = firstName;
        return this;
    }

    public AddTeacherDtoBuilder WithLastName(string lastName)
    {
        _dto.LastName = lastName;
        return this;
    }

    public AddTeacherDtoBuilder WithMobilePhone(string mobilePhone)
    {
        _dto.MobilePhone = mobilePhone;
        return this;
    }

    public AddTeacherDtoBuilder WithNationalNumber(string number)
    {
        _dto.NationalNumber = number;
        return this;
    }

    public AddTeacherDtoBuilder WithYearBirth(int birth)
    {
        _dto.YearBirth = birth;
        return this;
    }

    public AddTeacherDtoBuilder WithMobileNumber(string mobile)
    {
        _dto.MobilePhone = mobile;
        return this;
    }

    public AddTeacherDto Build()
    {
        return _dto;
    }
}