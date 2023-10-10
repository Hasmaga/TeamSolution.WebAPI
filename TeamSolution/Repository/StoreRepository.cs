using TeamSolution.Repository.Interface;
using TeamSolution.DatabaseContext;

namespace TeamSolution.Repository
{
    public class StoreRepository : IStoreRepository
    {
        private readonly ApplicationDbContext _context;

        public StoreRepository(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
