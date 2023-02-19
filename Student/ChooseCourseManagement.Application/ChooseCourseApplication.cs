using ChooseCourseManagement.Application.Contracts.ChooseCourse;
using ChooseCourseManagement.Application.Contracts.Dto;
using ChooseCourseManagement.Domain.ChooseCourseAgg;
using CourseManagement.Application.Contracts.Exceptions;
using CourseManagement.Domain.CourseAgg;
using StudentManagement.Application.Contracts.Exceptions;
using StudentManagement.Domain.StudentAgg;
using TeacherManagement.Application.Contracts.Exceptions;
using TeacherManagement.Domain.TeacherAgg;

namespace ChooseCourseManagement.Application
{
    public class ChooseCourseApplication : IChooseCourseApplication
    {
        private readonly IChooseCourseRepository _repository;
        private readonly ICourseRepository _courseRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ITeacherRepository _teacherRepository;

        public ChooseCourseApplication(
            IChooseCourseRepository repository,
            ICourseRepository courseRepository,
            IStudentRepository studentRepository,
            ITeacherRepository teacherRepository)
        {
            _repository = repository;
            _courseRepository = courseRepository;
            _studentRepository = studentRepository;
            _teacherRepository = teacherRepository;
        }

        public void Add(AddChooseCourseDto dto)
        {
            var course = _courseRepository.Exists(_ => _.Id == dto.CourseId);
            if (!course)
            {
                throw new CourseNotFoundException();
            }

            var student = _studentRepository.Exists(_ => _.Id == dto.StudentId);
            if (!student)
            {
                throw new StudentNotFoundException();
            }

            var teacher = _teacherRepository.Exists(_ => _.Id == dto.StudentId);
            if (!teacher)
            {
                throw new TeacherNotFoundException();
            }

            var chooseCourse = new ChooseCourse
            {
                CourseId = dto.CourseId,
                StudentId = dto.StudentId,
                TeacherId = dto.TeacherId,
                CreationDate = DateTime.UtcNow
            };

            _repository.Create(chooseCourse);
            _repository.SaveChanges();
        }
    }
}