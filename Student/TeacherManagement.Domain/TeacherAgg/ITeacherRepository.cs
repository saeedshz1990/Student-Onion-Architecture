using _0_Framework.Domain;
using TeacherManagement.Application.Contracts.Dto;

namespace TeacherManagement.Domain.TeacherAgg
{
    public interface ITeacherRepository : IRepository<int,Teacher>
    {
        IList<GetTeacherDto> GetAll();
        GetTeacherByIdDto GetById(int id);
        void Update(UpdateTeacherDto dto);
    }
}
