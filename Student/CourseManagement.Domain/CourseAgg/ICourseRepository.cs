using _0_Framework.Domain;
using CourseManagement.Application.Contracts.Dto;

namespace CourseManagement.Domain.CourseAgg
{
    public interface ICourseRepository : IRepository<int, Course>
    {
        IList<GetCourseDto> GetAll();
        GetCourseByIdDto GetById(int id);
        void Update(UpdateCourseDto dto);
    }
}
