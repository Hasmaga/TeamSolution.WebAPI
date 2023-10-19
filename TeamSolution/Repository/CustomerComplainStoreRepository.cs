using TeamSolution.DatabaseContext;
using TeamSolution.Repository.Interface;

namespace TeamSolution.Repository
{
    public class CustomerComplainStoreRepository : ICustomerComplainStoreRepository
    {        
        private readonly ApplicationDbContext _context;
        public CustomerComplainStoreRepository(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
