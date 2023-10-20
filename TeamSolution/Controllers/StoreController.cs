using Microsoft.AspNetCore.Mvc;
using TeamSolution.Service.Interface;

namespace TeamSolution.Controllers
{
    [Route("storeapi")]
    [ApiController]
    public class StoreController : Controller
    {
        private readonly IStoreService _storeService;
        public StoreController(IStoreService storeService)
        {
            _storeService = storeService;
        }


        
    }
}
