using TeamSolution.Repository.Interface;
using TeamSolution.DatabaseContext;
using TeamSolution.Model;
using Microsoft.EntityFrameworkCore;
using TeamSolution.Enum;
using TeamSolution.ViewModel.Order;
using AutoMapper;
using System.Linq;
using MailKit.Search;
using Org.BouncyCastle.Asn1.X509;
using TeamSolution.Helper;

namespace TeamSolution.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        private IMapper _mapper;

        public OrderRepository(ApplicationDbContext context, ILogger<OrderRepository> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
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

        

        public async Task<ICollection<Order>> GetAllOrdersRepositoryAsync(bool includeIsDeleted)
        {
            try
            {
                _logger.LogInformation("Get all orders");
                var queryable = _context.Orders.AsNoTracking();
                if (!includeIsDeleted)
                {
                    queryable = queryable.Where(o => o.DeleteDateTime == null);
                }
                ICollection<Order> list = await queryable.ToListAsync();
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Order> GetOrderByIdRepositoryAsync(Guid id)
        {
            try
            {
                _logger.LogInformation("Get order with ID:" + id);
                return await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<ICollection<Order>> GetOrdersByCustomerIdRepositoryAsync(Guid id, bool includeIsDeleted)
        {
            try
            {
                _logger.LogInformation("Get order with Customer ID:" + id);
                var queryable = _context.Orders.Where(x => x.CustomerId == id);
                if (!includeIsDeleted)
                {
                    queryable = queryable.Where(o => o.DeleteDateTime == null);
                }
                return await queryable.ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<ICollection<Order>> GetOrdersByStoreIdRepositoryAsync(Guid id, bool includeIsDeleted)
        {
            try
            {
                _logger.LogInformation("Get order with Store ID:" + id);
                var queryable = _context.Orders.Where(x => x.StoreId == id);
                if (!includeIsDeleted)
                {
                    queryable = queryable.Where(o => o.DeleteDateTime == null);
                }
                return await queryable.ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<Guid> UpdateOrderStateRepositoryAsync(Order order, CancellationToken cancellationToken = default)
        {
           
            try
            {
                _logger.LogInformation("Update order state with ID:" + order.Id);
                var entity = await _context.Orders.FindAsync(new object[] { order.Id }, cancellationToken);
                if (entity == null)
                {
                    //NOT FOUND
                    return Guid.Empty;
                }
                if (entity.DeleteDateTime != null)
                {
                    throw new Exception(ResponseCodeConstants.IS_DELETED);
                }
                entity.StatusOrderId = order.StatusOrderId;
                entity.UpdateDateTime = order.UpdateDateTime;
                await _context.SaveChangesAsync();
                return entity.Id;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<Guid> UpdateOrderRepositoryAsync(Order order, CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation("Update order with ID:" + order.Id);
                var entity = await _context.Orders.FindAsync(new object[] { order.Id }, cancellationToken);
                if (entity == null)
                {
                    //NOT FOUND
                    return Guid.Empty;
                }
                if (entity.DeleteDateTime != null)
                {
                    throw new Exception(ResponseCodeConstants.IS_DELETED);
                }
                if(!string.IsNullOrEmpty(order.OrderAddress))
                {
                    entity.OrderAddress = order.OrderAddress;
                }
                if (!string.IsNullOrEmpty(order.PhoneCustomer))
                {
                    entity.PhoneCustomer = order.PhoneCustomer;
                }
                if(order.TimeTakeOrder != CoreHelper.DefaultTime)
                {
                    entity.TimeTakeOrder= order.TimeTakeOrder;
                }
                if (order.TimeDeliverOrder != CoreHelper.DefaultTime)
                {
                    entity.TimeDeliverOrder = order.TimeDeliverOrder;
                }
                entity.UpdateDateTime= order.UpdateDateTime;
                await _context.SaveChangesAsync();
                return entity.Id;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<Guid> DeleteOrderRepositoryAsync(Order order, CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation("Delete order with ID:" + order.Id);
                var entity = await _context.Orders.FindAsync(new object[] { order.Id }, cancellationToken);
                if (entity == null)
                {
                    //NOT FOUND
                    return Guid.Empty;
                }
                if (entity.DeleteDateTime != null)
                {
                    throw new Exception(ResponseCodeConstants.IS_DELETED);
                }
                entity.DeleteDateTime = order.DeleteDateTime;
                await _context.SaveChangesAsync();
                return entity.Id;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<Order>> GetOrdersWithStatusAsync(string status)
        {
            _logger.LogInformation("GetOrdersWithStatusAsync");
            try
            {
                return await _context.Orders
                     .Where(order => order.StatusOrder.StatusName == status)
                     .Include(order => order.Store)
                     .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateOrderWithToGetOrderId(Guid orderId, Guid tourGetOrderId)
        {
            try
            {
                var order = await _context.Orders.FindAsync(orderId);
                order.TourGetOrderId = tourGetOrderId;
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
