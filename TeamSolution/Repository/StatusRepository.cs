using TeamSolution.Repository.Interface;
using TeamSolution.DatabaseContext;

namespace TeamSolution.Repository
{
    public class StatusRepository : IStatusRepository
    {
        private readonly ApplicationDbContext _context;

        public StatusRepository(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
