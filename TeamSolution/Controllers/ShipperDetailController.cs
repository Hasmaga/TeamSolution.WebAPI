using Microsoft.AspNetCore.Mvc;
using TeamSolution.Repository.Interface;

namespace TeamSolution.Controllers
{
    [Route("shipperdetailapi")]
    [ApiController]
    public class ShipperDetailController : Controller
    {
        private readonly IShipperDetailRepository _shipperDetailRepository;
        public ShipperDetailController(IShipperDetailRepository shipperDetailRepository)
        {
            _shipperDetailRepository = shipperDetailRepository;
        }
    }
}
