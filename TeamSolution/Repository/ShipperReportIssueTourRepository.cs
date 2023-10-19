using TeamSolution.DatabaseContext;
using TeamSolution.Repository.Interface;

namespace TeamSolution.Repository
{
    public class ShipperReportIssueTourRepository : IShipperReportIssueTourRepository
    { 
        private readonly ApplicationDbContext _context;
        public ShipperReportIssueTourRepository(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
