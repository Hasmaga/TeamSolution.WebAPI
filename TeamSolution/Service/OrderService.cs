using TeamSolution.Service.Interface;
using TeamSolution.Repository.Interface;
using TeamSolution.Model;
using System.Security.Claims;
using TeamSolution.Enum;
using TeamSolution.ViewModel.Order;
using TeamSolution.ViewModel.Store;
using TeamSolution.Helper;
using System.Collections.Generic;

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
        public async Task<ICollection<Order>?> GetByStoreIdAndStatusServiceAsync(Guid storeId, string orderState)
        {
            ICollection<Order>? list = null;
            var userLogged = await _accountRepository.GetUserByIdAsync(GetSidLogged());
            if(userLogged == null)
            {
                throw new Exception(ErrorCode.NOT_AUTHORIZED);
            }
            var store = await _storeRepository.GetStoreByAccountIdRepositoryAsync(storeId);
            if(store != null && store.Id == storeId)
            {
                var statusId = await _statusRepository.FindIdByStatusNameAsync(orderState);
                list = await _orderRepository.GetOrdersByStoreIdAndStatusIdRepositoryAsync(storeId, statusId);
            }
              
            return list;
        }

        // method for store manage orders
        public async Task<Guid> UpdateOrderStatusForStoreServiceAsync(Guid orderId, string newState)
        {
            List<string> validStates = new List<string> { 
                StatusOrderEnumCode.STORE_ACCEPT,
                StatusOrderEnumCode.STORE_DECLINCE,
                StatusOrderEnumCode.READY_TO_WASH_ORDER,
                StatusOrderEnumCode.ORDER_IN_PROGRESS,
                StatusOrderEnumCode.WASH_DONE,
            };
            if(!validStates.Any(vs => vs.Equals(newState)))
            {
                throw new Exception(ErrorCode.NOT_ALLOW);
            }
            //Kiểm người dùng hiện tại
            var userLogged = await _accountRepository.GetUserByIdAsync(GetSidLogged());
            if (userLogged == null)
            {
                throw new Exception(ErrorCode.NOT_AUTHORIZED);
            }
            //Kiểm dữ liệu của order
            var order = await GetOrderByIdServiceAsync(orderId);
            var currentState = await _statusRepository.GetStatusNameByStatusIdRepositoryAsync(order.StatusOrderId);
            if(currentState == null)
            {
                throw new Exception(ResponseCodeConstantsOrder.UPDATE_ORDER_FAILED);
            }
            //kiểm xem order có thuộc cửa hàng hay không
            if(userLogged.StoreId != order.StoreId)
            {
                throw new Exception(ErrorCode.NOT_ALLOW);
            }

            string? status = CheckValidStatus(currentState, newState);
            if(status == null)
            {
                throw new Exception(ResponseCodeConstantsOrder.UPDATE_ORDER_FAILED);
            }
            var statusId = await _statusRepository.FindIdByStatusNameAsync(newState);
            Order updateModel = new Order
            {
                Id = orderId,
                StatusOrderId = statusId
            };

            return await _orderRepository.UpdateOrderStateRepositoryAsync(updateModel);

        }

        public async Task<Guid> UpdateOrderStatusForShipperServiceAsync(Guid orderId, string newState)
        {
            //shipper
            List<string> validStates = new List<string> {
                StatusOrderEnumCode.SHIPPER_ARRIVED_CUSTOMER,
                StatusOrderEnumCode.SHIPPER_TAKE_ORDER,
                StatusOrderEnumCode.DELIVER_TO_STORE,
                StatusOrderEnumCode.SHIPPER_ARRIVED_STORE,
            };
            if (!validStates.Any(vs => vs.Equals(newState)))
            {
                throw new Exception(ErrorCode.NOT_ALLOW);
            }
            //Kiểm người dùng hiện tại
            var userLogged = await _accountRepository.GetUserByIdAsync(GetSidLogged());
            if (userLogged == null)
            {
                throw new Exception(ErrorCode.NOT_AUTHORIZED);
            }
            //Kiểm dữ liệu của order
            var order = await GetOrderByIdServiceAsync(orderId);
            var currentState = await _statusRepository.GetStatusNameByStatusIdRepositoryAsync(order.StatusOrderId);

            //kiểm xem order có thuộc shipper hay không (chưa làm)
            /*if (userLogged.Id != order.)
            {
                throw new Exception(ErrorCode.NOT_ALLOW);
            }*/

            string? status = CheckValidStatus(currentState, newState);
            if (status == null)
            {
                throw new Exception(ResponseCodeConstantsOrder.UPDATE_ORDER_FAILED);
            }
            var statusId = await _statusRepository.FindIdByStatusNameAsync(newState);
            Order updateModel = new Order
            {
                Id = orderId,
                StatusOrderId = statusId
            };

            return await _orderRepository.UpdateOrderStateRepositoryAsync(updateModel);

        }
        public async Task<Guid> UpdateOrderStatusForCustomerServiceAsync(Guid orderId, string newState)
        {
            //shipper
            List<string> validStates = new List<string> {
                StatusOrderEnumCode.ORDER_CANCEL,
                StatusOrderEnumCode.ORDER_DONE,
                StatusOrderEnumCode.ORDER_REJECT,
            };
            if (!validStates.Any(vs => vs.Equals(newState)))
            {
                throw new Exception(ErrorCode.NOT_ALLOW);
            }
            //Kiểm người dùng hiện tại
            var userLogged = await _accountRepository.GetUserByIdAsync(GetSidLogged());
            if (userLogged == null)
            {
                throw new Exception(ErrorCode.NOT_AUTHORIZED);
            }
            //Kiểm dữ liệu của order
            var order = await GetOrderByIdServiceAsync(orderId);
            var currentState = await _statusRepository.GetStatusNameByStatusIdRepositoryAsync(order.StatusOrderId);

            //kiểm xem order có thuộc customer hay không 
            if (userLogged.Id != order.CustomerId)
            {
                throw new Exception(ErrorCode.NOT_ALLOW);
            }

            string? status = CheckValidStatus(currentState, newState);
            if (status == null)
            {
                throw new Exception(ResponseCodeConstantsOrder.UPDATE_ORDER_FAILED);
            }
            var statusId = await _statusRepository.FindIdByStatusNameAsync(newState);
            Order updateModel = new Order
            {
                Id = orderId,
                StatusOrderId = statusId
            };

            return await _orderRepository.UpdateOrderStateRepositoryAsync(updateModel);

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

        private string? CheckValidStatus(string currentStatus, string newStatus, string? tourDeliverOrGet = null)
        {
            switch(currentStatus)
            {
                case StatusOrderEnumCode.WAITING_STORE_ACCEPT:
                    if (newStatus == StatusOrderEnumCode.STORE_ACCEPT)
                    {
                        return newStatus;
                    }
                    if (newStatus == StatusOrderEnumCode.STORE_DECLINCE)
                    {
                        return newStatus;
                    }  
                    break;
                case StatusOrderEnumCode.STORE_ACCEPT:
                    if (newStatus == StatusOrderEnumCode.READY_TAKE_ORDER)
                    {
                        return newStatus;
                    }
                    if (newStatus == StatusOrderEnumCode.ORDER_CANCEL)
                    {
                        return newStatus;
                    }
                    break;
                case StatusOrderEnumCode.READY_TAKE_ORDER:
                    if (newStatus == StatusOrderEnumCode.WAITING_SHIPPER_ACCEPT)
                    {
                        return newStatus;
                    }
                    break;
                case StatusOrderEnumCode.WAITING_SHIPPER_ACCEPT:
                    if(newStatus == StatusOrderEnumCode.SHIPPER_ON_THE_WAY)
                    {
                        return newStatus;
                    }
                    break;
                case StatusOrderEnumCode.SHIPPER_ON_THE_WAY:
                    if (newStatus == StatusOrderEnumCode.SHIPPER_ARRIVED_CUSTOMER)
                    {
                        return newStatus;
                    }
                    break;
                case StatusOrderEnumCode.SHIPPER_ARRIVED_CUSTOMER:
                    if(tourDeliverOrGet == null)
                    {
                        throw new Exception(ErrorCode.NOT_ALLOW);
                    }
                    if (newStatus == StatusOrderEnumCode.SHIPPER_TAKE_ORDER && tourDeliverOrGet == StatusShipperTourEnum.GET)
                    {
                        return newStatus;
                    }
                    if (newStatus == StatusOrderEnumCode.ORDER_DONE && tourDeliverOrGet == StatusShipperTourEnum.DELIVER)
                    {
                        return newStatus;
                    }
                    if (newStatus == StatusOrderEnumCode.ORDER_REJECT && tourDeliverOrGet == StatusShipperTourEnum.DELIVER)
                    {
                        return newStatus;
                    }
                    break;
                case StatusOrderEnumCode.SHIPPER_TAKE_ORDER:
                    if (newStatus == StatusOrderEnumCode.DELIVER_TO_STORE)
                    {
                        return newStatus;
                    }
                    break;
                case StatusOrderEnumCode.DELIVER_TO_STORE:
                    if (newStatus == StatusOrderEnumCode.SHIPPER_ARRIVED_STORE)
                    {
                        return newStatus;
                    }
                    break;
                case StatusOrderEnumCode.SHIPPER_ARRIVED_STORE:
                    if (newStatus == StatusOrderEnumCode.READY_TO_WASH_ORDER)
                    {
                        return newStatus;
                    }
                    break;
                case StatusOrderEnumCode.READY_TO_WASH_ORDER:
                    if (newStatus == StatusOrderEnumCode.ORDER_IN_PROGRESS)
                    {
                        return newStatus;
                    }
                    break;
                case StatusOrderEnumCode.ORDER_IN_PROGRESS:
                    if (newStatus == StatusOrderEnumCode.WASH_DONE)
                    {
                        return newStatus;
                    }
                    break;
                case StatusOrderEnumCode.WASH_DONE:
                    if (newStatus == StatusOrderEnumCode.READY_DELIVERY_ORDER)
                    {
                        return newStatus;
                    }
                    break;
                case StatusOrderEnumCode.READY_DELIVERY_ORDER:
                    if (newStatus == StatusOrderEnumCode.ORDER_DONE)
                    {
                        return newStatus;
                    }
                    break;
                case StatusOrderEnumCode.ORDER_REJECT: 
                    if (newStatus == StatusOrderEnumCode.DELIVER_TO_STORE)
                    {
                        return newStatus;
                    }
                    break;
                case StatusOrderEnumCode.ORDER_DONE:
                    throw new Exception(ErrorCode.NOT_ALLOW);
                case StatusOrderEnumCode.STORE_DECLINCE:
                    throw new Exception(ErrorCode.NOT_ALLOW);
                case StatusOrderEnumCode.ORDER_CANCEL:
                    throw new Exception(ErrorCode.NOT_ALLOW);

            }
            return null;
        }

        #endregion
    }
}
