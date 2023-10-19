using TeamSolution.DatabaseContext;
using TeamSolution.Repository.Interface;

namespace TeamSolution.Repository
{
    public class TourShipperRepository : ITourShipperRepository
    {
        private readonly ApplicationDbContext _context;
        public TourShipperRepository(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
