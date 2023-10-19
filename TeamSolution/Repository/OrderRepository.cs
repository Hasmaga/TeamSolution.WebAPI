using TeamSolution.Repository.Interface;
using TeamSolution.DatabaseContext;
using TeamSolution.Model;

namespace TeamSolution.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public OrderRepository(ApplicationDbContext context, ILogger<OrderRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> CreateOrderRepositoryAsync(Order order)
        {
            try
            {
                _logger.LogInformation("CreateOrderRepositoryAsync");
                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();
                return true;
            } catch (Exception ex)
            {
                _logger.LogError("Error Create Order At Repository: " + ex.ToString());
                throw;
            }
        }
    }
}
