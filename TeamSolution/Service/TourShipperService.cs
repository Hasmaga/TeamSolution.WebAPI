using System.Security.Claims;
using TeamSolution.Enum;
using TeamSolution.Helper;
using TeamSolution.Model;
using TeamSolution.Repository;
using TeamSolution.Repository.Interface;
using TeamSolution.Service.Interface;
using TeamSolution.ViewModel.TourShipper;
using static System.Net.WebRequestMethods;

namespace TeamSolution.Service
{
    public class TourShipperService : ITourShipperService
    {
        private readonly ITourShipperRepository _tourShipperRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IStatusRepository _statusRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger _logger;
        private readonly IHttpContextAccessor _http;
        public TourShipperService(ITourShipperRepository tourShipperRepository, 
            ILogger<TourShipperService> logger, 
            IHttpContextAccessor http, 
            IAccountRepository accountRepository,
            IStatusRepository statusRepository, 
            IOrderRepository orderRepository)
        {
            _tourShipperRepository = tourShipperRepository;
            _logger = logger;
            _http = http;
            _accountRepository = accountRepository;
            _statusRepository = statusRepository;
            _orderRepository = orderRepository;
        }

        public async Task<ICollection<TourShipper>> GetAllToursServiceAsync()
        {
            return await _tourShipperRepository.GetAllToursRepositoryAsync();
        }

        public async Task<TourShipper?> GetTourByTourIdServiceAsync(Guid tourId)
        {
            return await _tourShipperRepository.GetTourByTourIdRepositoryAsync(tourId);
        }
        public async Task<TourShipper?> GetTourByTourIdIncludeOrderServiceAsync(Guid tourId)
        {
            return await _tourShipperRepository.GetTourByTourIdIncludeOrderRepositoryAsync(tourId);
        }

        public async Task<bool> CreateTourServiceAsync(TourShipperModel tour)
        {
            _logger.LogInformation("CreateTourServiceAsync");
            try
            {
                var statusReadyTakeId = await _statusRepository.FindIdByStatusNameAsync(StatusOrderEnumCode.READY_TAKE_ORDER);
                var statusReadyDeliveryId = await _statusRepository.FindIdByStatusNameAsync(StatusOrderEnumCode.READY_DELIVERY_ORDER);
                var userLogged = await _accountRepository.GetUserByIdAsync(GetSidLogged());
               
                //Tạm bỏ qua xác thực role

                //Validate tam thoi
                if(tour.ShipperId == null) {
                    throw new Exception(ErrorCode.NOT_FOUND);
                }
                if(tour.DeliverOrGet == null)
                {
                    throw new Exception(ErrorCode.NOT_FOUND);
                }
                //Default status for tour on create
                tour.StatusName = StatusShipperTourEnum.WAITING_FOR_ACCEPT;
                Guid statusId = await _statusRepository.FindIdByStatusNameAsync(tour.StatusName);

                if (tour.Orders==null || tour.Orders.Count < 1)
                {
                    throw new Exception(ErrorCode.NOT_FOUND);
                }

                bool flag = false;
                if (tour.DeliverOrGet == "DELIVER") 
                { 
                    flag = await IsListOrdersValid(tour.Orders,statusReadyTakeId);
                }
                if (tour.DeliverOrGet == "GET")
                {
                    flag = await IsListOrdersValid(tour.Orders, statusReadyDeliveryId);
                }
                if (!flag)
                {
                    throw new Exception(ErrorCode.NOT_FOUND);   
                }
                Guid wattingShipperAcceptId = await _statusRepository.FindIdByStatusNameAsync(StatusOrderEnumCode.WAITING_SHIPPER_ACCEPT);
                foreach (var orderId in tour.Orders)
                {
                    Order updateModel = new Order
                    {
                        Id = orderId,
                        StatusOrderId = wattingShipperAcceptId,
                        UpdateDateTime = CoreHelper.SystemTimeNow
                    };
                    await _orderRepository.UpdateOrderStateRepositoryAsync(updateModel);
                }
                TourShipper newTour = new TourShipper
                {
                    ShipperManagerId = userLogged.Id,
                    ShipperId = (Guid) tour.ShipperId,
                    DeliverOrGet = tour.DeliverOrGet,
                    StatusId = statusId,
                };
                return await _tourShipperRepository.CreateTourRepositoryAsync(newTour);
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<Guid> UpdateTourServiceAsync(UpdateTourShipperRequestModel tour)
        {
            throw new NotImplementedException();
        }
        public async Task<Guid> DeleteTourServiceAsync(Guid Id)
        {
            TourShipper tourShipper = new TourShipper
            {
                Id = Id,
                DeleteDateTime = CoreHelper.SystemTimeNow
            };
            return await _tourShipperRepository.DeleteTourRepositoryAsync(tourShipper);
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

        
        private bool IsTourStatusValid(string currentStatus, string newStatus)
        {
            switch (currentStatus)
            {
                //For ShipperTour status
                case StatusShipperTourEnum.WAITING_FOR_ACCEPT:
                    if (newStatus == StatusShipperTourEnum.ON_PROCESS) 
                    {
                        return true;
                    }
                    break;
                case StatusShipperTourEnum.ON_PROCESS:
                    if (newStatus == StatusShipperTourEnum.DONE) 
                    {
                        return true;
                    }
                    break;
            }
            return false;
        }
        private async Task<bool> IsListOrdersValid(List<Guid> orders, Guid validStatus)
        {
            //Reason not use design like IsTourStatusValid: if we do that, we have to access to database alots.
            foreach (var orderId in orders)
            {
                var order = await _orderRepository.GetOrderByIdRepositoryAsync(orderId);
                if (order == null)
                {
                    //never happen when use
                    throw new Exception(orderId + ErrorCode.NOT_FOUND);
                }
                if (order.StatusOrderId != validStatus)
                {
                    return false;     
                }

            }
            return true;
        }
        #endregion
    }
}
