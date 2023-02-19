using StudentManagement.Application.Contracts.Dto;

namespace StudentManagement.Application.Contracts.Student
{
    public interface IStudentApplication
    {
        void Add(AddStudentDto dto);
        void Update(UpdateStudentDto dto, int id);
        IList<GetStudentDto> GetAll();
        GetStudentByIdDto GetById(int id);
        void Delete(int id);
    }
}