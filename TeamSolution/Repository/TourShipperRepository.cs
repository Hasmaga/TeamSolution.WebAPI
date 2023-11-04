using TeamSolution.DatabaseContext;
using TeamSolution.Model;
using TeamSolution.Repository.Interface;

namespace TeamSolution.Repository
{
    public class TourShipperRepository : ITourShipperRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        public TourShipperRepository(ApplicationDbContext context, ILogger<OrderRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> AddTourShipper(TourShipper tourShipper)
        {
            try
            {
                _context.TourShippers.Add(tourShipper);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
