using Microsoft.AspNetCore.Mvc;
using TeamSolution.Repository.Interface;
using TeamSolution.Service.Interface;

namespace TeamSolution.Controllers
{
    [Route("shipperreportissuetour")]
    [ApiController]
    public class ShipperReportIssueTourController : Controller
    {        
        private readonly IShipperReportIssueTourService _shipperReportIssueTourService;

        public ShipperReportIssueTourController(IShipperReportIssueTourService shipperReportIssueTourService)
        {
            _shipperReportIssueTourService = shipperReportIssueTourService;
        }
    }
}
