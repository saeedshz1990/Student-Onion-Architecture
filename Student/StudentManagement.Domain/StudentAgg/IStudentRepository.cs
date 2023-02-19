using _0_Framework.Domain;
using StudentManagement.Application.Contracts.Dto;

namespace StudentManagement.Domain.StudentAgg
{
    public interface IStudentRepository : IRepository<int, Student>
    {
        IList<GetStudentDto> GetAll();
        GetStudentByIdDto GetById(int id);
        void Update(UpdateStudentDto dto);
        void Delete(int id);
    }
}