using TeacherManagement.Application.Contracts.Dto;

namespace TeacherManagement.Application.Contracts.Teacher
{
    public interface ITeacherApplication
    {
        void Add(AddTeacherDto dto);
        void Update(UpdateTeacherDto dto, int id);
        IList<GetTeacherDto> GetAll();
        GetTeacherByIdDto GetById(int id);
        void Delete(int id);
    }
}
