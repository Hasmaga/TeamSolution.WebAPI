using TeamSolution.Repository.Interface;
using TeamSolution.DatabaseContext;

namespace TeamSolution.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
