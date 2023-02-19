using CourseManagement.Application.Contracts.Dto;

namespace Student.Test.Tools.CourseTestTools;

public class AddCourseDtoBuilder
{
    private readonly AddCourseDto _dto;


    public AddCourseDtoBuilder()
    {
        _dto = new AddCourseDto
        {
            Name = "dummy",
            Description="dummyDesc"
        };
    }

    public AddCourseDtoBuilder WithName(string name)
    {
        _dto.Name = name;
        return this;
    }
    public AddCourseDtoBuilder WithDescription(string description)
    {
        _dto.Name = description;
        return this;
    }

    

    public AddCourseDto Build()
    {
        return _dto;
    }
}