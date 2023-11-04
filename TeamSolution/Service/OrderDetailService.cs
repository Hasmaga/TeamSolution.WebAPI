using TeamSolution.Service.Interface;
using TeamSolution.Model;
using TeamSolution.Repository.Interface;
using TeamSolution.Enum;
using TeamSolution.Repository;
using static System.Net.WebRequestMethods;
using System.Security.Claims;

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
        public OrderDetailService(IOrderDetailRepository orderDetailRepository, IStatusRepository statusRepository, ILogger<OrderDetailService> logger, IHttpContextAccessor http, IAccountRepository accountRepository, IStoreServiceRepository storeServiceRepository, IOrderRepository orderRepository)
        {
            _orderDetailRepository = orderDetailRepository;
            _statusRepository = statusRepository;
            _logger = logger;
            _http = http;
            _accountRepository = accountRepository;
            _storeServiceRepository = storeServiceRepository;
            _orderRepository = orderRepository;
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

            




            //var orderDetail = await _orderDetailRepository.GetOrderDetailByIdAsync(orderDetailId);
            //var statusOrderId = await _orderDetailRepository.GetStatusOrderIdByOrderDetailId(orderDetailId);
            //if (orderDetail == null)
            //{
            //    return false; // Không tìm thấy OrderDetail
            //}





            //// Kiểm tra trạng thái đơn hàng và áp dụng logic kiểm tra
            //// Lấy trạng thái dưới dạng Task<Guid>
            //var readyToWashingStatusId = await _statusRepository.FindIdByStatusNameAsync(StatusOrderEnum.READY_TO_WASHING);
            //var shipperIsComingStatusId = await _statusRepository.FindIdByStatusNameAsync(StatusOrderEnum.SHIPPER_IS_COMING);

            //// Kiểm tra trạng thái đơn hàng và áp dụng logic kiểm tra

            //if ((statusOrderId != readyToWashingStatusId) && (statusOrderId != shipperIsComingStatusId))
            //{
            //    return false; // Không thể cập nhật chế độ giặt cho đơn hàng này ở trạng thái hiện tại
            //}

            //orderDetail.StoreServiceId = newStoreServiceId;

            //await _orderDetailRepository.UpdateOrderDetailAsync(orderDetail);

            //return true;
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
