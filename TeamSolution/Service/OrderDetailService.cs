using TeamSolution.Service.Interface;
using TeamSolution.Model;
using TeamSolution.Repository.Interface;
using TeamSolution.Enum;
using TeamSolution.Repository;
using static System.Net.WebRequestMethods;
using System.Security.Claims;
using TeamSolution.ViewModel.OrderDetail;

namespace TeamSolution.Service
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IStatusRepository _statusRepository;
        private readonly ILogger _logger;
        private readonly IHttpContextAccessor _http;
        private readonly IAccountRepository _accountRepository;
        private readonly IStoreServiceRepository _storeServiceRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IStoreRepository _storeRepository;
        public OrderDetailService(IOrderDetailRepository orderDetailRepository, IStatusRepository statusRepository, ILogger<OrderDetailService> logger, IHttpContextAccessor http, IAccountRepository accountRepository, IStoreServiceRepository storeServiceRepository, IOrderRepository orderRepository, IStoreRepository storeRepository)
        {
            _orderDetailRepository = orderDetailRepository;
            _statusRepository = statusRepository;
            _logger = logger;
            _http = http;
            _accountRepository = accountRepository;
            _storeServiceRepository = storeServiceRepository;
            _orderRepository = orderRepository;
            _storeRepository = storeRepository;
        }

        public Task<bool> CreateOrderDetailServiceAsync(OrderDetail orderDetail)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateOrderDetailStoreServiceIdAsync(Guid orderDetailId, Guid newStoreServiceId)
        {
            _logger.LogInformation("UpdateStoreServiceForOrderDetail: " + "OrderDetail: " + orderDetailId + "StoreService: " + newStoreServiceId);

            var userLogin = await _accountRepository.GetUserByIdAsync(GetSidLogged());
            var orderDetail = await _orderDetailRepository.GetOrderDetailByOrderDetailIdRepositoryAsync(orderDetailId);
            var updateStoreService = await _storeServiceRepository.GetStoreServiceByIdRepositoryAsync(newStoreServiceId);
            var order = await _orderRepository.GetOrderByOrderIdRepositoryAsync(orderDetailId);
            
            if (userLogin == null)
            {
                throw new Exception(ErrorCode.NOT_AUTHORIZED);
            }

            if (orderDetail == null)
            {
                throw new Exception(ErrorCode.NOT_FOUND);
            }

            if (updateStoreService == null)
            {
                throw new Exception(ErrorCode.NOT_FOUND);
            }

            if (order == null)
            {
                throw new Exception(ErrorCode.NOT_FOUND);
            }

            var orderStatus = order.StatusOrderId;
            var userOrderId = order.CustomerId;

            if(userLogin.Id != userOrderId)
            {
                throw new Exception(ErrorCode.NOT_AUTHORIZED);
            }

            // Get order status name 
            var orderstatusname = await _statusRepository.GetStatusNameByStatusIdRepositoryAsync(orderStatus);
            if (orderstatusname == null)
            {
                throw new Exception(ErrorCode.NOT_FOUND);
            }
            
            if(orderstatusname != StatusOrderEnumCode.WAITING_STORE_ACCEPT)
            {
                throw new Exception(ErrorCode.NOT_ALLOW);
            }

            if(await _orderDetailRepository.UpdateOrderDetailByOrderServiceIdRepositoryAsync(orderDetailId, newStoreServiceId))
            {
                return true;
            }
            else
            {
                return false;
            }        
        }

        public async Task<ICollection<GetOrderDetailByOrderIdReqDto>> GetOrderDetailByOrderIdServiceAsync(Guid orderId)
        {
            _logger.LogInformation("GetOrderDetailByOrderIdServiceAsync: " + orderId);
            try
            {
                var userLoggin = await _accountRepository.GetUserByIdAsync(GetSidLogged());
                var order = await _orderRepository.GetOrderByOrderIdRepositoryAsync(orderId);
                var store = await _storeRepository.GetStoreByAccountIdRepositoryAsync(userLoggin.Id);
                if (userLoggin == null)
                {
                    throw new Exception(ErrorCode.NOT_AUTHORIZED);
                }
                if (order == null)
                {
                    throw new Exception(ErrorCode.NOT_FOUND);
                }                
                var orderDetails = await _orderDetailRepository.GetListOrderDetailByOrderIdRepositoryAsync(orderId);
                // Convert OrderDetail to GetOrderDetailByOrderIdReqDto
                var result = new List<GetOrderDetailByOrderIdReqDto>();
                foreach (var item in orderDetails)
                { 
                    var storeService = await _storeServiceRepository.GetStoreServiceByIdRepositoryAsync(item.StoreServiceId);
                    result.Add(new GetOrderDetailByOrderIdReqDto
                    {
                        Id = item.Id,
                        StoreServiceType = storeService.ServiceType,
                        Weight = item.Weight,
                    });
                }
                return result;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
            
        }

        #region Private Method
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
