using TeamSolution.Repository.Interface;
using TeamSolution.DatabaseContext;
using TeamSolution.Model;
using Microsoft.EntityFrameworkCore;
using TeamSolution.Enum;
using TeamSolution.ViewModel.Order;
using AutoMapper;

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

        

        public async Task<ICollection<Order>> GetAllAsync(bool includeIsDeleted)
        {
            try
            {
                var queryable = _context.Orders.AsNoTracking();
                if (!includeIsDeleted)
                {
                    queryable = queryable.Where(_ => _.IsDelete == includeIsDeleted);
                }
                ICollection<Order> list = await queryable.ToListAsync();
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Order> GetByIdAsync(Guid id)
        {
            try
            {
                return await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Guid> UpdateAsync(UpdateOrderRequestModel request, CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation("Delete order with ID:" + request.id);
                var entity = await _context.Orders.FindAsync(new object[] { request.id }, cancellationToken);
                if (entity == null)
                {
                    //NOT FOUND
                    return Guid.Empty;
                }
                if (entity.IsDelete)
                {
                    throw new Exception(ResponseCodeConstants.IS_DELETED);
                }
                _mapper.Map(request.orderModel, entity);
                entity.UpdateDateTime= DateTime.UtcNow;
                await _context.SaveChangesAsync();
                return entity.Id;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<Guid> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation("Delete order with ID:" + id);
                var entity = await _context.Orders.FindAsync(new object[] { id }, cancellationToken);
                if (entity == null)
                {
                    //NOT FOUND
                    return Guid.Empty;
                }
                if (entity.IsDelete)
                {
                    throw new Exception(ResponseCodeConstants.IS_DELETED);
                }
                entity.IsDelete = true;
                entity.DeleteDateTime = DateTime.UtcNow;
                await _context.SaveChangesAsync();
                return entity.Id;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
