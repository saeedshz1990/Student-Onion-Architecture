using CourseManagement.Application;
using CourseManagement.Application.Contracts.Course;
using CourseManagement.Infrastructure.EFCore;
using CourseManagement.Infrastructure.EFCore.Repository;

namespace Student.Test.Tools.CourseTestTools;

public static class CourseServiceFactory
{
    public static ICourseApplication GenerateCourseService(
        CourseManagementContext context)
    {
        var repository = new CourseRepository(context);
        return new CourseApplication(repository);
    }
}