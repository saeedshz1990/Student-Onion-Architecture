using _0_Framework.Infrastructure;
using CourseManagement.Application.Contracts.Dto;
using CourseManagement.Domain.CourseAgg;

namespace CourseManagement.Infrastructure.EFCore.Repository
{
    public class CourseRepository : RepositoryBase<int, Course>, ICourseRepository
    {
        private readonly CourseManagementContext _context;

        public CourseRepository(CourseManagementContext context) : base(context)
        {
            _context = context;
        }

        public void Delete(int id)
        {
            _context.Remove(id);
        }

        public IList<GetCourseDto> GetAll()
        {
            return _context.Course.Select(_ => new GetCourseDto
            {
                Id = _.Id,
                Name = _.Name,
                Description = _.Description
            }).ToList();
        }

        public GetCourseByIdDto GetById(int id)
        {
            return _context.Course.Where(_ => _.Id == id)
                .Select(_ => new GetCourseByIdDto
                {
                    Id = _.Id,
                    Name = _.Name,
                    Description = _.Description
                }).FirstOrDefault()!;
        }

        public void Update(UpdateCourseDto dto)
        {
            _context.Update(dto);
        }
    }
}
