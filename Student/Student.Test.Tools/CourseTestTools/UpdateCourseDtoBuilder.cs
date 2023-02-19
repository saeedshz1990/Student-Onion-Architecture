using CourseManagement.Application.Contracts.Dto;

namespace Student.Test.Tools.CourseTestTools;

public class UpdateCourseDtoBuilder
{
    private readonly UpdateCourseDto _dto;

    public UpdateCourseDtoBuilder()
    {
        _dto = new UpdateCourseDto
        {
            Name = "dummy",
        };
    }

    public UpdateCourseDtoBuilder WithName(string name)
    {
        _dto.Name = name;
        return this;
    }

    public UpdateCourseDtoBuilder WithDescription(string description)
    {
        _dto.Name = description;
        return this;
    }

    public UpdateCourseDto Build()
    {
        return _dto;
    }
}