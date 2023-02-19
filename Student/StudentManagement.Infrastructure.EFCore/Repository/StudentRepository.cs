using _0_Framework.Infrastructure;
using StudentManagement.Application.Contracts.Dto;
using StudentManagement.Domain.StudentAgg;
using System.Linq;

namespace StudentManagement.Infrastructure.EFCore.Repository
{
    public class StudentRepository : RepositoryBase<int, Student>, IStudentRepository
    {
        private readonly StudentManagementContext _context;

        public StudentRepository(StudentManagementContext context) : base(context)
        {
            _context = context;
        }

        public void Delete(int id)
        {
            _context.Remove(id);
        }

        public IList<GetStudentDto> GetAll()
        {
            return _context.Student
                 .Select(_ => new GetStudentDto
                 {
                     Id = _.Id,
                     FirstName = _.FirstName,
                     LastName = _.LastName,
                     MobilePhone = _.MobilePhone,
                     NationalNumber = _.NationalNumber,
                     TeacherId = _.TeacherId,
                     YearBirth = _.YearBirth
                 }).ToList();
        }

        public GetStudentByIdDto GetById(int id)
        {
            return _context.Student
                .Where(_ => _.Id == id)
                .Select(_ => new GetStudentByIdDto
                {
                    Id = _.Id,
                    FirstName = _.FirstName,
                    LastName = _.LastName,
                    MobilePhone = _.MobilePhone,
                    NationalNumber = _.NationalNumber,
                    TeacherId = _.TeacherId,
                    YearBirth = _.YearBirth
                }).FirstOrDefault()!;
        }

        public void Update(UpdateStudentDto dto)
        {
            _context.Update(dto);
        }
    }
}
