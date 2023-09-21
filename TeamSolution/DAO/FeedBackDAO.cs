using TeamSolution.DAO.Interface;
using TeamSolution.DatabaseContext;

namespace TeamSolution.DAO
{
    public class FeedBackDAO : IFeedBackDAO
    {
        private readonly ApplicationDbContext _context;

        public FeedBackDAO(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
