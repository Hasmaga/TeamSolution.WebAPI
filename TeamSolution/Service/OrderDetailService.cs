using TeamSolution.Service.Interface;
using TeamSolution.Model;
using TeamSolution.Repository.Interface;
using TeamSolution.Enum;
using TeamSolution.Repository;

namespace TeamSolution.Service
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IStatusRepository _statusRepository;
        public OrderDetailService(IOrderDetailRepository orderDetailRepository, IStatusRepository statusRepository)
        {
            _orderDetailRepository = orderDetailRepository;
            _statusRepository = statusRepository;
        }

        public Task<bool> CreateOrderDetailServiceAsync(OrderDetail orderDetail)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateOrderDetailStoreServiceIdAsync(Guid orderDetailId, Guid newStoreServiceId)
        {
            var orderDetail = await _orderDetailRepository.GetOrderDetailByIdAsync(orderDetailId);
            var statusOrderId = await _orderDetailRepository.GetStatusOrderIdByOrderDetailId(orderDetailId);
            if (orderDetail == null)
            {
                return false; // Không tìm thấy OrderDetail
            }





            // Kiểm tra trạng thái đơn hàng và áp dụng logic kiểm tra
            // Lấy trạng thái dưới dạng Task<Guid>
            var readyToWashingStatusId = await _statusRepository.FindIdByStatusNameAsync(StatusOrderEnum.READY_TO_WASHING);
            var shipperIsComingStatusId = await _statusRepository.FindIdByStatusNameAsync(StatusOrderEnum.SHIPPER_IS_COMING);

            // Kiểm tra trạng thái đơn hàng và áp dụng logic kiểm tra

            if ((statusOrderId != readyToWashingStatusId) && (statusOrderId != shipperIsComingStatusId))
            {
                return false; // Không thể cập nhật chế độ giặt cho đơn hàng này ở trạng thái hiện tại
            }

            orderDetail.StoreServiceId = newStoreServiceId;

            await _orderDetailRepository.UpdateOrderDetailAsync(orderDetail);

            return true;
        }
    }
}
