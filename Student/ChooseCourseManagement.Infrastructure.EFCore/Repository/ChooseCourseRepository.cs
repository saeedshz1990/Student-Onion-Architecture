using _0_Framework.Infrastructure;
using ChooseCourseManagement.Domain.ChooseCourseAgg;

namespace ChooseCourseManagement.Infrastructure.EFCore.Repository
{
    public class ChooseCourseRepository :RepositoryBase<int,ChooseCourse>, IChooseCourseRepository
    {
        private readonly ChooseCourseManagementContext  _context;

        public ChooseCourseRepository(ChooseCourseManagementContext context) : base(context)
        {
            _context = context;
        }
    }
}
