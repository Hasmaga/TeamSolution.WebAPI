using TeamSolution.DAO.Interface;
using TeamSolution.DatabaseContext;

namespace TeamSolution.DAO
{
    public class OrderDAO : IOrderDAO
    {
        private readonly ApplicationDbContext _context;

        public OrderDAO(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
