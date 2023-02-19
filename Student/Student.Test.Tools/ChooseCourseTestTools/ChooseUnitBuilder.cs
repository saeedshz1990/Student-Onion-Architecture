using ChooseCourseManagement.Domain.ChooseCourseAgg;

namespace Student.Test.Tools.ChooseCourseTestTools;

public class ChooseUnitBuilder
{
    private readonly ChooseCourse _dto;

    public ChooseUnitBuilder()
    {
        _dto = new ChooseCourse
        {
            CourseId = 1,
            StudentId = 1,
            TeacherId = 1,
        };
    }

    public ChooseUnitBuilder WithCourseId(int courseId)
    {
        _dto.CourseId = courseId;
        return this;
    }

    public ChooseUnitBuilder WithStudentId(int studentId)
    {
        _dto.StudentId = studentId;
        return this;
    }

    public ChooseUnitBuilder WithTeacherId(int teacherId)
    {
        _dto.TeacherId = teacherId;
        return this;
    }

    public ChooseCourse Build()
    {
        return _dto;
    }
}