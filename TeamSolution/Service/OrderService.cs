using TeamSolution.Service.Interface;
using TeamSolution.Repository.Interface;
using TeamSolution.Model;
using System.Security.Claims;
using TeamSolution.Enum;
using TeamSolution.ViewModel.Order;
using TeamSolution.ViewModel.Store;
using TeamSolution.Helper;

namespace TeamSolution.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly ILogger _logger;
        private readonly IHttpContextAccessor _http;
        private readonly IAccountRepository _accountRepository;
        private readonly IStatusRepository _statusRepository;
        private readonly IStoreServiceRepository _storeServiceRepository;
        private readonly IStoreRepository _storeRepository;
        public OrderService(
            IOrderRepository orderRepository, 
            IOrderDetailRepository orderDetailRepository, 
            ILogger<OrderService> logger, 
            IHttpContextAccessor http,
            IAccountRepository accountRepository,
            IStatusRepository statusRepository,
            IStoreServiceRepository storeServiceRepository,
            IStoreRepository storeRepository)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _logger = logger;
            _http = http;
            _accountRepository = accountRepository;
            _statusRepository = statusRepository;
            _storeServiceRepository = storeServiceRepository;
            _storeRepository = storeRepository;
        }

        public async Task<bool> CreateOrderServiceAsync(CreateNewOrderReqDto order)
        {
            _logger.LogInformation("CreateOrderServiceAsync");
            try
            {
                var store = await _storeRepository.GetStoreByIdRepositoryAsync(order.StoreId);
                var userLogged = await _accountRepository.GetUserByIdAsync(GetSidLogged());
                
                Order newOrder = new Order
                {
                    CustomerId = userLogged.Id,
                    OrderAddress = order.OrderAddress,
                    PhoneCustomer = order.OrderPhone,
                    PaymentMethod = order.PaymentMethod,
                    StoreId = order.StoreId,
                    TimeTakeOrder = order.TimeTakeOrder,
                    TimeDeliverOrder = order.TimeDeliveryOrder,
                    StatusOrderId = await _statusRepository.FindIdByStatusNameAsync(StatusOrderEnumCode.WAITING_STORE_ACCEPT)
                };
                if (await _orderRepository.CreateOrderRepositoryAsync(newOrder))
                {
                    foreach (var item in order.OrderDetails)
                    {
                        StoreService storeService = await _storeServiceRepository.GetStoreServiceByIdRepositoryAsync(item.StoreServiceId);
                        
                        OrderDetail newOrderDetail = new OrderDetail
                        {
                            OrderId = newOrder.Id,
                            StoreServiceId = storeService.Id,
                            Weight = item.Weight,
                            Price = storeService.ServicePrice * item.Weight
                        };
                        if (await _orderDetailRepository.CreateOrderDetailRepositoryAsync(newOrderDetail))
                        {
                            continue;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Create Order At Service: " + ex.ToString());
                throw;
            }
        }
        public async Task<ICollection<Order>> GetAllOrderServiceAsync()
        {
            return await _orderRepository.GetAllOrdersRepositoryAsync();
        }

        public async Task<Order> GetOrderByIdServiceAsync(Guid id)
        {
            return await _orderRepository.GetOrderByIdRepositoryAsync(id);
        }

        public async Task<Guid> UpdateOrderServiceAsync(UpdateOrderRequestModel request)
        {
            if(request.orderModel.TimeTakeOrder == null)
            {
                request.orderModel.TimeTakeOrder = CoreHelper.DefaultTime;
            }
            if (request.orderModel.TimeDeliverOrder == null)
            {
                request.orderModel.TimeDeliverOrder = CoreHelper.DefaultTime;
            }
            Order order = new Order
            {
                Id= request.id,
                OrderAddress = request.orderModel.OrderAddress ?? CoreHelper.DefaultEmptyString,
                PhoneCustomer = request.orderModel.PhoneCustomer ?? CoreHelper.DefaultEmptyString,
                TimeTakeOrder = (DateTime) request.orderModel.TimeTakeOrder,
                TimeDeliverOrder = (DateTime) request.orderModel.TimeDeliverOrder,
                UpdateDateTime = CoreHelper.SystemTimeNow
            };
            return await _orderRepository.UpdateOrderRepositoryAsync(order);
        }
        public async Task<Guid> DeleteOrderServiceAsync(Guid id)
        {
            Order order = new Order
            {
                Id = id,
                DeleteDateTime = CoreHelper.SystemTimeNow
            };
            return await _orderRepository.DeleteOrderRepositoryAsync(order);
        }

        public async Task<ICollection<Order>> GetByCustomerIdServiceAsync(Guid id)
        {
            return await _orderRepository.GetOrdersByCustomerIdRepositoryAsync(id);
        }

        public async Task<ICollection<Order>> GetByStoreIdServiceAsync(Guid id)
        {
            return await _orderRepository.GetOrdersByStoreIdRepositoryAsync(id);
        }
        #region private method
        private Guid GetSidLogged()
        {
            var sid = _http.HttpContext?.User.FindFirst(ClaimTypes.Sid)?.Value;
            if (sid == null)
            {
                throw new Exception(ErrorCode.USER_NOT_FOUND);
            }
            return Guid.Parse(sid);
        }

        #endregion
    }
}
