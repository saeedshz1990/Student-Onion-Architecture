using CourseManagement.Application.Contracts.Dto;

namespace CourseManagement.Application.Contracts.Course
{
    public interface ICourseApplication
    {
        void Add(AddCourseDto dto);
        void Update(UpdateCourseDto dto, int id);
        IList<GetCourseDto> GetAll();
        GetCourseByIdDto GetById(int id);
        void Delete(int id);
    }
}
