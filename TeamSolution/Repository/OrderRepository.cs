using TeamSolution.Repository.Interface;
using TeamSolution.DatabaseContext;
using TeamSolution.Model;
using Microsoft.EntityFrameworkCore;

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

        public async Task<Order?> GetOrderByOrderIdRepositoryAsync(Guid orderId)
        {
            _logger.LogInformation($"Get Order by OrderId: {orderId}");
            try
            {
                var order = await _context.Orders.FindAsync(orderId);
                if (order != null)
                {
                    return order;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error at get order by orderId repository: {ex}");
                throw;
            }
        }
    }
}
