using _0_Framework;
using CourseManagement.Application.Contracts.Course;
using CourseManagement.Application.Contracts.Dto;
using CourseManagement.Application.Contracts.Exceptions;
using CourseManagement.Domain.CourseAgg;

namespace CourseManagement.Application
{
    public class CourseApplication : ICourseApplication
    {
        private readonly ICourseRepository _courseRepository;

        public CourseApplication(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public void Add(AddCourseDto dto)
        {
            var operation = new OperationResult();
            if (_courseRepository.Exists(x => x.Name == dto.Name))
            {
                throw new CourseDuplicatedNameException();
            }

            var course = new Course
            {
                Name = dto.Name,
                Description = dto.Description,
            };

            _courseRepository.Create(course);
            _courseRepository.SaveChanges();

        }

        public void Delete(int id)
        {
            var operation = new OperationResult();
            var course = _courseRepository.Exists(x => x.Id != id);
            if (!course)
            {
                throw new CourseNotFoundException();
            }

            _courseRepository.Delete(id);
            _courseRepository.SaveChanges();

        }

        public IList<GetCourseDto> GetAll()
        {
            return _courseRepository.GetAll();
        }

        public GetCourseByIdDto GetById(int id)
        {
            return _courseRepository.GetById(id);
        }

        public void Update(UpdateCourseDto dto, int id)
        {
            var operation = new OperationResult();
            if (_courseRepository.Exists(x => x.Id != id))
            {
                throw new CourseNotFoundException();
            }
            else
            {
                throw new CourseDuplicatedNameException();
            }
            var course =_courseRepository.GetById(id);

            dto.Name = course.Name;
            dto.Name = course.Description;

            _courseRepository.SaveChanges();
        }
    }
}