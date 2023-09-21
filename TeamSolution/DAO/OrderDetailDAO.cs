using TeamSolution.DAO.Interface;
using TeamSolution.DatabaseContext;

namespace TeamSolution.DAO
{
    public class OrderDetailDAO : IOrderDetailDAO
    {
        private readonly ApplicationDbContext _context;

        public OrderDetailDAO(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
