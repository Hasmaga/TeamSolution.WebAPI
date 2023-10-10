using TeamSolution.Repository.Interface;
using TeamSolution.DatabaseContext;

namespace TeamSolution.Repository
{
    public class FeedBackRepository : IFeedBackRepository
    {
        private readonly ApplicationDbContext _context;

        public FeedBackRepository(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
