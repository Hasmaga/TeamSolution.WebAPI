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

        public async Task<List<Order>> GetAllOrdersRepositoryAsync()
        {
            try
            {
                return await _context.Orders.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Get Order At Repository: " + ex.ToString());
                throw;
                throw;
            }
        }

        public async Task<Order> GetOrderByCustomerIdRepositoryAsync(Guid id)
        {
            try
            {
                return await _context.Orders.FirstOrDefaultAsync(x => x.CustomerId == id);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Get Order At Repository: " + ex.ToString());
                throw;
                throw;
            }
        }

        public async Task<Order> GetOrderByIdRepositoryAsync(Guid id)
        {
            try
            {
                return await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Get Order At Repository: " + ex.ToString());
                throw;
                throw;
            }
        }

        public async Task<bool> UpdateOrderRepositoryAsync(Order order)
        {
            try
            {
                _context.Orders.Update(order);
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
