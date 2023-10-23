using TeamSolution.Service.Interface;
using TeamSolution.Repository.Interface;
using TeamSolution.Model;
using System.Security.Claims;
using TeamSolution.Enum;
using TeamSolution.ViewModel.Order;
using TeamSolution.ViewModel.Store;

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
                (
                    customerId: userLogged.Id,
                    orderAddress: order.OrderAddress,
                    phoneCustomer: order.OrderPhone,
                    createDateTime: DateTime.Now,
                    paymentMethod: order.PaymentMethod,
                    storeId: order.StoreId,
                    timeTakeOrder: order.TimeTakeOrder,
                    timeShipperTakeOrder: null,
                    timeDeliverOrder: order.TimeDeliveryOrder,
                    timeShipperDeliverOrder: null,
                    statusOrderId: await _statusRepository.FindIdByStatusNameAsync(StatusOrderEnumCode.WAITING_FOR_CONFIRMATION),
                    tourShipperId: null,
                    updateDateTime: null,
                    isDelete: false,
                    deleteDateTime: null
                );
                if (await _orderRepository.CreateOrderRepositoryAsync(newOrder))
                {
                    foreach (var item in order.OrderDetails)
                    {
                        StoreService storeService = await _storeServiceRepository.GetStoreServiceByIdRepositoryAsync(item.StoreServiceId);
                        OrderDetail newOrderDetail = new OrderDetail
                        (
                            orderId: newOrder.Id,
                            storeServiceId: storeService.Id,
                            weight: item.Weight,
                            price: storeService.ServicePrice * item.Weight
                        );
                        await _orderDetailRepository.CreateOrderDetailRepositoryAsync(newOrderDetail);
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
        public async Task<ICollection<Order>> GetAllOrder()
        {
            return await _orderRepository.GetAllAsync();
        }

        public async Task<Order> GetOrderById(Guid id)
        {
            return await _orderRepository.GetByIdAsync(id);
        }

        public async Task<Guid> UpdateOrderAsync(UpdateOrderRequestModel request)
        {
            return await _orderRepository.UpdateAsync(request);
        }
        public async Task<Guid> DeleteOrderAsync(Guid id)
        {
            return await _orderRepository.DeleteAsync(id);
        }

        #region private method
        private Guid GetSidLogged()
        {
            var sid = _http.HttpContext.User.FindFirst(ClaimTypes.Sid).Value;
            if (sid == null)
            {
                throw new Exception(ErrorCode.USER_NOT_FOUND);
            }
            return Guid.Parse(sid);
        }
        #endregion
    }
}
