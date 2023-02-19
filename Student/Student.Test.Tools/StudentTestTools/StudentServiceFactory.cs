using StudentManagement.Application;
using StudentManagement.Application.Contracts.Student;
using StudentManagement.Infrastructure.EFCore;
using StudentManagement.Infrastructure.EFCore.Repository;

namespace Student.Test.Tools.StudentTestTools;

public static class StudentServiceFactory
{
    public static IStudentApplication GenerateStudentService(StudentManagementContext context)
    {
        var repository = new StudentRepository(context);
        return new StudentApplication(repository);
    }
}