using TeamSolution.Repository.Interface;
using TeamSolution.DatabaseContext;
using TeamSolution.Model;
using Microsoft.EntityFrameworkCore;

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
        public async Task<OrderDetail> GetOrderDetailByIdAsync(Guid orderDetailId)
        {
            return await _context.OrderDetails.FirstOrDefaultAsync(od => od.Id == orderDetailId);
        }

        public async Task UpdateOrderDetailAsync(OrderDetail orderDetail)
        {
            // Thực hiện cập nhật OrderDetail ở đây
            await _context.SaveChangesAsync();
        }

        public async Task<Guid?> GetStatusOrderIdByOrderDetailId(Guid orderDetailId)
        {
            // Use DbSet to query the database and retrieve the StatusOrderId
            var statusOrderId = await _context.OrderDetails
                .Where(od => od.Id == orderDetailId) // Assuming Id is the primary key of OrderDetail
                .Select(od => od.Order.StatusOrderId)
                .FirstOrDefaultAsync();

            return statusOrderId;
        }
    }
}
