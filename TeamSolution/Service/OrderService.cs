using TeamSolution.Service.Interface;
using TeamSolution.Repository.Interface;
using TeamSolution.Model;
using System.Security.Claims;
using TeamSolution.Enum;
using TeamSolution.ViewModel.Order;
using AutoMapper;

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
        private readonly IMapper _mapper;

        public OrderService(
            IOrderRepository orderRepository, 
            IOrderDetailRepository orderDetailRepository, 
            ILogger<OrderService> logger, 
            IHttpContextAccessor http,
            IAccountRepository accountRepository,
            IStatusRepository statusRepository,
            IStoreServiceRepository storeServiceRepository,
            IStoreRepository storeRepository,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _logger = logger;
            _http = http;
            _accountRepository = accountRepository;
            _statusRepository = statusRepository;
            _storeServiceRepository = storeServiceRepository;
            _storeRepository = storeRepository;
            _mapper = mapper;
        }

        public async Task<bool> CancelOrder(Guid OrderId, Guid CustomerId)
        {
            var order = await _orderRepository.GetOrderByIdRepositoryAsync(OrderId);
            var customer = await _orderRepository.GetOrderByCustomerIdRepositoryAsync(CustomerId);
            if (order is not null && customer is not null)
            {
                order.StatusOrderId = await _statusRepository.FindIdByStatusNameAsync(StatusOrderEnum.CANCEL);
                await _orderRepository.UpdateOrderRepositoryAsync(order);
                return true;
            }
            return false;
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

        public async Task<List<Order>> GetAllOrderInProcess()
        {
            var order = await _orderRepository.GetAllOrdersRepositoryAsync();
            return order;
        }

        public async Task<bool> RecievedOrder(Guid OrderId, Guid CustomerId)
        {
            var order = await _orderRepository.GetOrderByIdRepositoryAsync(OrderId);
            var customer = await _orderRepository.GetOrderByCustomerIdRepositoryAsync(CustomerId);
            if (order is not null && customer is not null)
            {
                order.StatusOrderId = await _statusRepository.FindIdByStatusNameAsync(StatusOrderEnum.RECIEVED_ORDER);
                await _orderRepository.UpdateOrderRepositoryAsync(order);
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateOrderServiceAsync(Guid id, CreateNewOrderReqDto order)
        {
            var orderObj = await _orderRepository.GetOrderByIdRepositoryAsync(id);
            if (orderObj is not null)
            {
                _mapper.Map(order, orderObj);
                await _orderRepository.UpdateOrderRepositoryAsync(orderObj);
                return true;
            }
            return false;
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
