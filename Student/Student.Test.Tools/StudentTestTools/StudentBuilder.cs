namespace Student.Test.Tools.StudentTestTools;

public class StudentBuilder
{
    private readonly StudentManagement.Domain.StudentAgg.Student _student;

    public StudentBuilder()
    {
        _student = new StudentManagement.Domain.StudentAgg.Student
        {
            FirstName = "dummyFirst",
            LastName = "dummyLast",
            MobilePhone = "09177877225",
            NationalNumber = "dummyNum",
            YearBirth = 1369,
            TeacherId = 1
        };
    }

    public StudentBuilder WithFirstName(string firstName)
    {
        _student.FirstName = firstName;
        return this;
    }

    public StudentBuilder WithLastName(string lastName)
    {
        _student.LastName = lastName;
        return this;
    }

    public StudentBuilder WithMobilePhone(string mobilePhone)
    {
        _student.MobilePhone = mobilePhone;
        return this;
    }

    public StudentBuilder WithNationalNumber(string number)
    {
        _student.NationalNumber = number;
        return this;
    }

    public StudentBuilder WithYearBirth(int birth)
    {
        _student.YearBirth = birth;
        return this;
    }

    public StudentBuilder WithTeacherId(int teacherId)
    {
        _student.TeacherId = teacherId;
        return this;
    }

    public StudentBuilder WithMobileNumber(string mobile)
    {
        _student.MobilePhone = mobile;
        return this;
    }

    public StudentManagement.Domain.StudentAgg.Student Build()
    {
        return _student;
    }
}