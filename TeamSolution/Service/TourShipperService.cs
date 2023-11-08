using AutoMapper;
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
        private readonly IRoleRepository _roleRepository;
        private readonly ILogger _logger;
        private readonly IHttpContextAccessor _http;
        private readonly IMapper _mapper;
        public TourShipperService(ITourShipperRepository tourShipperRepository, 
            ILogger<TourShipperService> logger, 
            IHttpContextAccessor http, 
            IAccountRepository accountRepository,
            IStatusRepository statusRepository, 
            IOrderRepository orderRepository, IMapper mapper, IRoleRepository roleRepository)
        {
            _tourShipperRepository = tourShipperRepository;
            _logger = logger;
            _http = http;
            _accountRepository = accountRepository;
            _statusRepository = statusRepository;
            _orderRepository = orderRepository;
            _roleRepository= roleRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<TourShipper>> GetAllToursServiceAsync()
        {
            return await _tourShipperRepository.GetAllToursRepositoryAsync();
        }
        public async Task<TourShipper?> GetTourByTourIdServiceAsync(Guid tourId)
        {
            return await _tourShipperRepository.GetTourByTourIdRepositoryAsync(tourId);
        }
        public async Task<ResponseTourShipperModel?> GetTourByTourIdIncludeOrderServiceAsync(Guid tourId)
        {
            var entity = await _tourShipperRepository.GetTourByTourIdIncludeOrderRepositoryAsync(tourId);
            ResponseTourShipperModel reponse = new ResponseTourShipperModel();
            _mapper.Map(entity, reponse);
            return reponse;
        }
        public async Task<bool> CreateTourServiceAsync(TourShipperModel tour)
        {
            _logger.LogInformation("CreateTourServiceAsync");
            try
            {
                Guid statusReadyTakeId = await _statusRepository.FindIdByStatusNameAsync(StatusOrderEnumCode.READY_TAKE_ORDER);
                Guid statusReadyDeliveryId = await _statusRepository.FindIdByStatusNameAsync(StatusOrderEnumCode.READY_DELIVERY_ORDER);
                var userLogged = await _accountRepository.GetUserByIdAsync(GetSidLogged());

                //lấy rolename của user 
                var roleName = await _roleRepository.GetRoleByIdAsync(userLogged.RoleId);
                if(roleName.RoleName != ActorEnumCode.SHIPPER_MANAGER)
                {
                    throw new Exception(ErrorCode.NOT_ALLOW);
                }

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
                if (tour.DeliverOrGet == "GET") 
                { 
                    flag = await IsListOrdersValid(tour.Orders,statusReadyTakeId);
                }
                if (tour.DeliverOrGet == "DELIVER")
                {
                    flag = await IsListOrdersValid(tour.Orders, statusReadyDeliveryId);
                }
                if (!flag)
                {
                    throw new Exception(ErrorCode.NOT_FOUND);   
                }
                Guid wattingShipperAcceptId = await _statusRepository.FindIdByStatusNameAsync(StatusOrderEnumCode.WAITING_SHIPPER_ACCEPT);
                TourShipper newTour = new TourShipper
                {
                    ShipperManagerId = userLogged.Id,
                    ShipperId = (Guid)tour.ShipperId,
                    DeliverOrGet = tour.DeliverOrGet,
                    StatusId = statusId,
                };
                foreach (var orderId in tour.Orders)
                {
                    Guid? getOrderId = null, shipOrderId = null;
                    if(tour.DeliverOrGet == "GET")
                    {
                        getOrderId = newTour.Id;
                    }
                    if (tour.DeliverOrGet == "DELIVER")
                    {
                        shipOrderId = newTour.Id;
                    }
                    Order updateModel = new Order
                    {
                        Id = orderId,
                        StatusOrderId = wattingShipperAcceptId,
                        TourGetOrderId = getOrderId,
                        TourShipOrderId = shipOrderId,
                        UpdateDateTime = CoreHelper.SystemTimeNow
                    };
                    await _orderRepository.UpdateOrderStateRepositoryAsync(updateModel);
                }
                
                return await _tourShipperRepository.CreateTourRepositoryAsync(newTour);
            }
            catch(Exception ex)
            {
                throw;
            }
        }
        public async Task<Guid> ChangeTourStatusAcceptServiceAsync(UpdateTourShipperRequestModel tour)
        {
            

            // kiểm tra status được cập nhật đúng là Onprocess ko
            if ( tour.TourShipperModel?.StatusName == StatusShipperTourEnum.ON_PROCESS)
            {
                throw new Exception(ErrorCode.NOT_FOUND);   
            }
            // lấy thông tin của tour lên database
            var tourShipper = await _tourShipperRepository.GetTourByTourIdIncludeOrderRepositoryAsync(tour.Id);
            if (tourShipper == null)
            {
                throw new Exception(ErrorCode.NOT_FOUND);
            }
            //xác thực user
            var userLogged = await _accountRepository.GetUserByIdAsync(GetSidLogged());

            //lấy rolename của user 
            var roleName = await _roleRepository.GetRoleByIdAsync(userLogged.RoleId);
            if (roleName.RoleName != ActorEnumCode.SHIPPER || userLogged.Id != tourShipper.ShipperId)
            {
                throw new Exception(ErrorCode.NOT_ALLOW);
            }


            //lấy status name hiện tại của tour
            var tourStatusName = await _statusRepository.GetStatusNameByStatusIdRepositoryAsync(tourShipper.StatusId);
            if (tourStatusName == null)
            {
                throw new Exception(ErrorCode.NOT_FOUND);
            }
            //dùng hàm phụ trợ kiểm tra tính đúng sai của status mới
            if(! await IsTourStatusValid(tourStatusName, tour.TourShipperModel.StatusName))
            {
                throw new Exception(ErrorCode.NOT_ALLOW);
            }
            List<Order> orders = new List<Order>();
            if(tourShipper.DeliverOrGet == StatusShipperTourEnum.DELIVER && tourShipper.TourShipOrders != null)
            {
                orders = tourShipper.TourShipOrders.ToList();
            }
            if (tourShipper.DeliverOrGet == StatusShipperTourEnum.GET && tourShipper.TourGetOrders != null)
            {
                orders = tourShipper.TourGetOrders.ToList();
            }
            var onTheWayStatusId = await _statusRepository.FindIdByStatusNameAsync(StatusOrderEnumCode.SHIPPER_ON_THE_WAY);
            foreach (var order in orders)
            {
                order.StatusOrderId = onTheWayStatusId;
                order.UpdateDateTime = CoreHelper.SystemTimeNow;
                await _orderRepository.UpdateOrderStateRepositoryAsync(order);
            }
            TourShipper ts = new TourShipper
            {
                Id = tour.Id,
                StatusId = await _statusRepository.FindIdByStatusNameAsync(tour.TourShipperModel.StatusName),
                UpdateDateTime = CoreHelper.SystemTimeNow,
            };
            return await _tourShipperRepository.UpdateTourRepositoryAsync(ts);
        }
        public async Task<Guid> ChangeTourStatusDoneServiceAsync(UpdateTourShipperRequestModel tour)
        {
            
            // kiểm tra status được cập nhật đúng là Done ko
            if (tour.TourShipperModel?.StatusName == StatusShipperTourEnum.DONE)
            {
                throw new Exception(ErrorCode.NOT_FOUND);
            }

            // lấy thông tin của tour lên database
            var tourShipper = await _tourShipperRepository.GetTourByTourIdIncludeOrderRepositoryAsync(tour.Id);
            if (tourShipper == null)
            {
                throw new Exception(ErrorCode.NOT_FOUND);
            }

            //xác thực user
            var userLogged = await _accountRepository.GetUserByIdAsync(GetSidLogged());

            //lấy rolename của user 
            var roleName = await _roleRepository.GetRoleByIdAsync(userLogged.RoleId);
            if (roleName.RoleName != ActorEnumCode.SHIPPER || userLogged.Id != tourShipper.ShipperId)
            {
                throw new Exception(ErrorCode.NOT_ALLOW);
            }

            //lấy status name hiện tại của tour
            var tourStatusName = await _statusRepository.GetStatusNameByStatusIdRepositoryAsync(tourShipper.StatusId);
            if (tourStatusName == null)
            {
                throw new Exception(ErrorCode.NOT_FOUND);
            }
            //dùng hàm phụ trợ kiểm tra tính đúng sai của status mới
            if (!await IsTourStatusValid(tourStatusName, tour.TourShipperModel.StatusName))
            {
                throw new Exception(ErrorCode.NOT_ALLOW);
            }
            // Lấy status có điều kiện tiên quyết (Order done) hoặc ready to wash order
            var orderStatusDone = await _statusRepository.FindIdByStatusNameAsync(StatusOrderEnumCode.ORDER_DONE);
            var orderStatusReadyWash = await _statusRepository.FindIdByStatusNameAsync(StatusOrderEnumCode.READY_TO_WASH_ORDER);
            bool canDone = true;
            //nế trạng thái tour là đưa hàng thì lấy từng order status trong tour đưa đem đi so sánh với status kết thúc tour đưa
            if(tourShipper.DeliverOrGet == "DELIVER")
            {
                foreach (var order in tourShipper.TourShipOrders)
                {
                    if (order.StatusOrderId != orderStatusDone)
                    {
                        canDone = false;
                        break;
                    }
                }
            }
            //Giống đoạn code trên nhưng là tourr lấy hàng
            if (tourShipper.DeliverOrGet == "GET")
            {
                foreach(var order in tourShipper.TourGetOrders)
                {
                    if (order.StatusOrderId != orderStatusReadyWash)
                    {
                        canDone= false;
                        break;
                    }
                }
            }

            if (!canDone)
            {
                throw new Exception(ErrorCode.NOT_ALLOW);
            }
            TourShipper ts = new TourShipper
            {
                Id = tour.Id,
                StatusId = await _statusRepository.FindIdByStatusNameAsync(tour.TourShipperModel.StatusName),
                UpdateDateTime = CoreHelper.SystemTimeNow,
            };
            return await _tourShipperRepository.UpdateTourRepositoryAsync(ts);
        }
        public async Task<Guid> DeleteTourServiceAsync(Guid Id)
        {
            //xác thực user
            var userLogged = await _accountRepository.GetUserByIdAsync(GetSidLogged());

            //lấy rolename của user 
            var roleName = await _roleRepository.GetRoleByIdAsync(userLogged.RoleId);
            if (roleName.RoleName != ActorEnumCode.SHIPPER_MANAGER)
            {
                throw new Exception(ErrorCode.NOT_ALLOW);
            }
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

        
        private async Task<bool> IsTourStatusValid(string currentStatus, string newStatus)
        {
            switch (currentStatus)
            {
                //For ShipperTour status
                case StatusShipperTourEnum.WAITING_FOR_ACCEPT:
                    if (newStatus == StatusShipperTourEnum.ON_PROCESS) 
                    {
                        return await Task.FromResult(true);
                    }
                    break;
                case StatusShipperTourEnum.ON_PROCESS:
                    if (newStatus == StatusShipperTourEnum.DONE) 
                    {
                        return await Task.FromResult(true);
                    }
                    break;
            }
            return await Task.FromResult(false);
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
