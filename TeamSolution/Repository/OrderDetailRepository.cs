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

        public async Task<OrderDetail?> GetOrderDetailByOrderDetailIdRepositoryAsync(Guid orderDetailId)
        {
            _logger.LogInformation("GetOrderDetailByOrderDetailIdRepositoryAsync" + orderDetailId);
            try
            {
                var orderdetail = await _context.OrderDetails.FindAsync(orderDetailId);
                if (orderdetail != null)
                {
                    return orderdetail;
                }
                else
                {
                    return null;
                }
            } catch (Exception ex)
            {
                _logger.LogInformation("GetOrderDetailByOrderDetailIdRepositoryAsync Error at: " + ex.ToString());
                throw;
            }
        }

        public async Task<bool> UpdateOrderDetailByOrderServiceIdRepositoryAsync(Guid orderDetailId, Guid newStoreServiceId)
        {
            _logger.LogInformation("UpdateOrderDetailByOrderServiceIdRepositoryAsync");
            try
            {
                var orderDetail = await _context.OrderDetails.FindAsync(orderDetailId);
                orderDetail.StoreServiceId = newStoreServiceId;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Update Order Detail At Repository: " + ex.ToString());
                throw;
            }
        }

        public async Task<ICollection<OrderDetail>> GetListOrderDetailByOrderIdRepositoryAsync(Guid orderId)
        {
            _logger.LogInformation("GetListOrderDetailByOrderIdRepositoryAsync");
            try
            {
                var orderDetails = await _context.OrderDetails.Where(x => x.OrderId == orderId).ToListAsync();
                return orderDetails;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Get List Order Detail At Repository: " + ex.ToString());
                throw;
            }
        }
    }
}
