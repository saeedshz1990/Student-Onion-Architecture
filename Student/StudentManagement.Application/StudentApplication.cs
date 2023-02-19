using _0_Framework;
using CourseManagement.Application.Contracts.Exceptions;
using CourseManagement.Domain.CourseAgg;
using StudentManagement.Application.Contracts.Dto;
using StudentManagement.Application.Contracts.Exceptions;
using StudentManagement.Application.Contracts.Student;
using StudentManagement.Domain.StudentAgg;
using StudentManagement.Infrastructure.EFCore.Repository;

namespace StudentManagement.Application
{
    public class StudentApplication : IStudentApplication
    {
        private readonly StudentRepository _repository;

        public StudentApplication(StudentRepository repository)
        {
            _repository = repository;
        }

        public void Add(AddStudentDto dto)
        {
            var operation = new OperationResult();
            if (_repository.Exists(x => x.NationalNumber == dto.NationalNumber))
            {
                throw new StudentDuplicatedNationalCodeException();

            }
            var student = new Student
            {
                NationalNumber = dto.NationalNumber,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                CreationDate = DateTime.UtcNow,
                MobilePhone = dto.MobilePhone,
                YearBirth = dto.YearBirth,
                TeacherId = dto.TeacherId
            };

            _repository.Create(student);
            _repository.SaveChanges();
        }

        public void Delete(int id)
        {
            var operation = new OperationResult();
            if (_repository.Exists(x => x.Id != id))
            {
                throw new StudentNotFoundException();
            }

            _repository.Delete(id);
            _repository.SaveChanges();
        }

        public IList<GetStudentDto> GetAll()
        {
            return _repository.GetAll();
        }

        public GetStudentByIdDto GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Update(UpdateStudentDto dto, int id)
        {
            var operation = new OperationResult();
            if (_repository.Exists(x => x.Id != id))
            {
                throw new CourseNotFoundException();
            }

            var course = new Student
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                MobilePhone = dto.MobilePhone,
                NationalNumber = dto.NationalNumber,
                YearBirth = dto.YearBirth,
                TeacherId = dto.TeacherId,
                CreationDate = DateTime.UtcNow
            };

            _repository.SaveChanges();
        }
    }
}
