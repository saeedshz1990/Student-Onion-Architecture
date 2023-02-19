using TeacherManagement.Application;
using TeacherManagement.Application.Contracts.Teacher;
using TeacherManagement.Infrastructure.EFCore;
using TeacherManagement.Infrastructure.EFCore.Repository;

namespace Student.Test.Tools.TeacherTestTools;

public static class TeacherServiceFactory
{
    public static ITeacherApplication GenerateTeacherService(TeacherManagementContext context)
    {
        var repository = new TeacherRepository(context);
        return new TeacherApplication(repository);
    }
}