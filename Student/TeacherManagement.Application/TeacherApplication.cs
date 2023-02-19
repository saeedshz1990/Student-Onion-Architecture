using _0_Framework;
using StudentManagement.Domain.StudentAgg;
using TeacherManagement.Application.Contracts.Dto;
using TeacherManagement.Application.Contracts.Exceptions;
using TeacherManagement.Application.Contracts.Teacher;
using TeacherManagement.Domain.TeacherAgg;

namespace TeacherManagement.Application
{
    public class TeacherApplication : ITeacherApplication
    {

        private readonly ITeacherRepository _repository;

        public TeacherApplication(ITeacherRepository repository)
        {
            _repository = repository;
        }

        public void Add(AddTeacherDto dto)
        {
            var operation = new OperationResult();
            if (_repository.Exists(x => x.NationalNumber == dto.NationalNumber))
            {
                throw new TeacherDuplicatedNationalCodeException();
            }
            var student = new Teacher
            {
                NationalNumber = dto.NationalNumber,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                MobilePhone = dto.MobilePhone,
                YearBirth = dto.YearBirth,
                CreationDate = DateTime.UtcNow
            };
                
            _repository.Create(student);
            _repository.SaveChanges();
        }

        public void Delete(int id)
        {
            var operation = new OperationResult();
            if (_repository.Exists(x => x.Id != id))
            {
                throw new TeacherNotFoundException();
            }

            _repository.Delete(id);
            _repository.SaveChanges();
        }

        public IList<GetTeacherDto> GetAll()
        {
            return _repository.GetAll();
        }

        public GetTeacherByIdDto GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Update(UpdateTeacherDto dto, int id)
        {
            var operation = new OperationResult();
            if (_repository.Exists(x => x.Id != id))
            {
                throw new TeacherNotFoundException();
            }

            var course = new Student
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                MobilePhone = dto.MobilePhone,
                NationalNumber = dto.NationalNumber,
                YearBirth = dto.YearBirth,
                CreationDate = DateTime.UtcNow
            };

            _repository.SaveChanges();
        }
    }
}
