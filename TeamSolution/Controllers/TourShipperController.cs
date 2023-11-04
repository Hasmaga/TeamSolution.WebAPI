using Microsoft.AspNetCore.Mvc;
using TeamSolution.Service.Interface;

namespace TeamSolution.Controllers
{
    [Route("storeserviceapi")]
    [ApiController]
    public class TourShipperController : Controller
    {        
        private readonly ITourShipperService _TourShipperService;

        public TourShipperController(ITourShipperService TourShipperService)
        {
            _TourShipperService = TourShipperService;
        }


    }
}
