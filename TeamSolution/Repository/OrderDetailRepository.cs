using TeamSolution.Repository.Interface;
using TeamSolution.DatabaseContext;
using TeamSolution.Model;

namespace TeamSolution.Repository
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public OrderDetailRepository(ApplicationDbContext context, ILogger<OrderDetailRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> CreateOrderDetailRepositoryAsync(OrderDetail orderDetail)
        {
            _logger.LogInformation("CreateOrderDetailRepositoryAsync");
            try
            {
                await _context.OrderDetails.AddAsync(orderDetail);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Create Order Detail At Repository: " + ex.ToString());
                throw;
            }
        }
    }
}
