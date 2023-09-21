using TeamSolution.DAO.Interface;
using TeamSolution.DatabaseContext;

namespace TeamSolution.DAO
{
    public class StoreModeSetingDAO : IStoreModeSetingDAO
    {
        private readonly ApplicationDbContext _context;

        public StoreModeSetingDAO(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
