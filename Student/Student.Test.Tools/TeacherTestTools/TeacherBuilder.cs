using TeacherManagement.Domain.TeacherAgg;

namespace Student.Test.Tools.TeacherTestTools;

public class TeacherBuilder
{
    private readonly Teacher _teacher;

    public TeacherBuilder()
    {
        _teacher = new Teacher
        {
            FirstName = "dummyFirst",
            LastName = "dummyLast",
            MobilePhone = "09177877225",
            NationalNumber = "dummyNum",
            YearBirth = 1369,
        };
    }

    public TeacherBuilder WithFirstName(string firstName)
    {
        _teacher.FirstName = firstName;
        return this;
    }

    public TeacherBuilder WithLastName(string lastName)
    {
        _teacher.LastName = lastName;
        return this;
    }

    public TeacherBuilder WithMobilePhone(string mobilePhone)
    {
        _teacher.MobilePhone = mobilePhone;
        return this;
    }

    public TeacherBuilder WithNationalNumber(string number)
    {
        _teacher.NationalNumber = number;
        return this;
    }

    public TeacherBuilder WithYearBirth(int birth)
    {
        _teacher.YearBirth = birth;
        return this;
    }

    public TeacherBuilder WithMobileNumber(string mobile)
    {
        _teacher.MobilePhone = mobile;
        return this;
    }

    public Teacher Build()
    {
        return _teacher;
    }
}