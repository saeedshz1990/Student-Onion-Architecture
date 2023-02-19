using CourseManagement.Domain.CourseAgg;

namespace Student.Test.Tools.CourseTestTools;

public class CourseDtoBuilder
{
    private readonly Course _course;

    public CourseDtoBuilder()
    {
        _course = new Course
        {
            Name = "dummy",
        };
    }

    public CourseDtoBuilder WithName(string name)
    {
        _course.Name = name;
        return this;
    }

    public CourseDtoBuilder WithDescription(string description)
    {
        _course.Name = description;
        return this;
    }

    public Course Build()
    {
        return _course;
    }
}