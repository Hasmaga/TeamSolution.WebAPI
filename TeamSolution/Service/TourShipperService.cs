using System.Security.Claims;
using System.Text;
using TeamSolution.Enum;
using TeamSolution.Model;
using TeamSolution.Repository;
using TeamSolution.Repository.Interface;
using TeamSolution.Service.Interface;
using TeamSolution.ViewModel.TourShipper;

namespace TeamSolution.Service
{
    public class TourShipperService : ITourShipperService
    {
        private readonly ITourShipperRepository _tourShipperRepository;
        private readonly IHttpContextAccessor _http;
        private readonly ILogger _logger;
        private readonly IAccountRepository _accountRepository;
        private readonly IStatusRepository _statusRepository;
        private readonly IOrderRepository _orderRepository;

        public TourShipperService(
            ITourShipperRepository tourShipperRepository,
        IHttpContextAccessor http,
        IAccountRepository accountRepository,
        ILogger<AccountService> logger,
        IStatusRepository statusRepository,
        IOrderRepository orderRepository)
        {
            _logger = logger;
            _http = http;
            _accountRepository = accountRepository;
            _tourShipperRepository = tourShipperRepository;
            _statusRepository = statusRepository;
            _orderRepository = orderRepository;
        }

        public async Task<bool> AddTourShipper(AddTour tourShipper)
        {
            try
            {
                _logger.LogInformation("AddTourShipper");
                var userLogged = await _accountRepository.GetUserByIdAsync(GetSidLogged());

                var statusId = await _statusRepository.FindIdByStatusNameAsync(StatusOrderEnumCode.WAITING_SHIPPER_ACCEPT);
                TourShipper newTour = new TourShipper
                {

                    ShipperManagerId = userLogged.Id,
                    ShipperId = tourShipper.shipperId,
                    CreateDateTime = DateTime.Now,
                    StatusId = statusId,
                    DeliverOrGet = tourShipper.DeliverOrGet
                };
                if (await _tourShipperRepository.AddTourShipper(newTour))
                {
                    foreach (var item in tourShipper.OrderIds)
                    {                      
                        if (await _orderRepository.UpdateOrderWithToGetOrderId(item,newTour.Id))
                        {
                            continue;
                        }
                        else { return false; 
                        }                                               
                    }
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }          
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
