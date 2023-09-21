using TeamSolution.DAO.Interface;
using TeamSolution.DatabaseContext;

namespace TeamSolution.DAO
{
    public class ShipperDetailDAO : IShipperDetailDAO
    {
        private readonly ApplicationDbContext _context;

        public ShipperDetailDAO(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
