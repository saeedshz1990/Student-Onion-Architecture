using _0_Framework.Infrastructure;
using StudentManagement.Application.Contracts.Dto;
using TeacherManagement.Application.Contracts.Dto;
using TeacherManagement.Domain.TeacherAgg;

namespace TeacherManagement.Infrastructure.EFCore.Repository
{
    public class TeacherRepository : RepositoryBase<int, Teacher>, ITeacherRepository
    {
        private readonly TeacherManagementContext _context;

        public TeacherRepository(TeacherManagementContext context) : base(context)
        {
            _context = context;
        }

        public void Delete(int id)
        {
            _context.Remove(id);
        }

        public IList<GetTeacherDto> GetAll()
        {
            return _context.Teacher
                 .Select(_ => new GetTeacherDto
                 {
                     Id = _.Id,
                     FirstName = _.FirstName,
                     LastName = _.LastName,
                     MobilePhone = _.MobilePhone,
                     NationalNumber = _.NationalNumber,
                     YearBirth = _.YearBirth
                 }).ToList();
        }

        public GetTeacherByIdDto GetById(int id)
        {
            return _context.Teacher
              .Where(_ => _.Id == id)
              .Select(_ => new GetTeacherByIdDto
              {
                  Id = _.Id,
                  FirstName = _.FirstName,
                  LastName = _.LastName,
                  MobilePhone = _.MobilePhone,
                  NationalNumber = _.NationalNumber,
                  YearBirth = _.YearBirth
              }).FirstOrDefault()!;
        }

        public void Update(UpdateTeacherDto dto)
        {
            _context.Update(dto);
        }
    }
}
