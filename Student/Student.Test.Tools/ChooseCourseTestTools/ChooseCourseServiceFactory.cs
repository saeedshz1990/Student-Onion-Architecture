using ChooseCourseManagement.Application;
using ChooseCourseManagement.Application.Contracts.ChooseCourse;
using ChooseCourseManagement.Infrastructure.EFCore;
using ChooseCourseManagement.Infrastructure.EFCore.Repository;
using CourseManagement.Infrastructure.EFCore;
using CourseManagement.Infrastructure.EFCore.Repository;
using StudentManagement.Infrastructure.EFCore;
using StudentManagement.Infrastructure.EFCore.Repository;
using TeacherManagement.Infrastructure.EFCore;
using TeacherManagement.Infrastructure.EFCore.Repository;

namespace Student.Test.Tools.ChooseCourseTestTools;

public static class ChooseCourseServiceFactory
{
    public static IChooseCourseApplication GenerateChooseCourseServiceFactory(
                            ChooseCourseManagementContext context,
                            CourseManagementContext _courseContext,
                            TeacherManagementContext _context,
                            StudentManagementContext studentContext)
    {
        var repository = new ChooseCourseRepository(context);
        var teacherRepository = new TeacherRepository(_context);
        var studentRepository = new StudentRepository(studentContext);
        var courseRepository = new CourseRepository(_courseContext);

        return new ChooseCourseApplication(
            repository,
            courseRepository,
            studentRepository,
            teacherRepository);
    }
}