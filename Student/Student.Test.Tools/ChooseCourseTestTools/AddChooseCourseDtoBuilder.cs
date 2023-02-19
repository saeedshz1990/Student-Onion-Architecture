using ChooseCourseManagement.Application.Contracts.Dto;

namespace Student.Test.Tools.ChooseCourseTestTools;

public class AddChooseCourseDtoBuilder
{
    private readonly AddChooseCourseDto _dto;

    public AddChooseCourseDtoBuilder()
    {
        _dto = new AddChooseCourseDto()
        {
            CourseId = 1,
            StudentId = 1,
            TeacherId = 1,
        };
    }

    public AddChooseCourseDtoBuilder WithCourseId(int courseId)
    {
        _dto.CourseId = courseId;
        return this;
    }

    public AddChooseCourseDtoBuilder WithStudentId(int studentId)
    {
        _dto.StudentId = studentId;
        return this;
    }

    public AddChooseCourseDtoBuilder WithTeacherId(int teacherId)
    {
        _dto.TeacherId = teacherId;
        return this;
    }

    public AddChooseCourseDto Build()
    {
        return _dto;
    }
}