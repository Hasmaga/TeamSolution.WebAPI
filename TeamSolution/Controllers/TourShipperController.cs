using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamSolution.Enum;
using TeamSolution.Model;
using TeamSolution.Repository;
using TeamSolution.Repository.Interface;
using TeamSolution.Service;
using TeamSolution.Service.Interface;
using TeamSolution.ViewModel.TourShipper;

namespace TeamSolution.Controllers
{
    [Route("storeserviceapi")]
    [ApiController]
    public class TourShipperController : Controller
    {        
        private readonly ITourShipperService _TourShipperService;
        private readonly IAccountRepository _accountRepository;

        public TourShipperController(ITourShipperService TourShipperService, IAccountRepository accountRepository)
        {
            _TourShipperService = TourShipperService;
            _accountRepository = accountRepository;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<TourShipper>> AddTourShipper(AddTour tourShipper)
        {
            try
            {
                if (await _TourShipperService.AddTourShipper(tourShipper))
                {
                    return StatusCode(200, SucessfulCode.CREATE_TOUR_SUCCESSFULLY);
                }
                else
                {
                    return StatusCode(500, ErrorCode.CREATE_TOUR_FAIL);
                }   
            }
            catch (Exception)
            {
               
                return StatusCode(500, ErrorCode.SERVER_ERROR);
            }
        }
    }
}
