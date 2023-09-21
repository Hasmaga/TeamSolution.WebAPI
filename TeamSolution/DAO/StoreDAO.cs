using TeamSolution.DAO.Interface;
using TeamSolution.DatabaseContext;

namespace TeamSolution.DAO
{
    public class StoreDAO : IStoreDAO
    {
        private readonly ApplicationDbContext _context;

        public StoreDAO(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
