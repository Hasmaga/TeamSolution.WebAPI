using Microsoft.AspNetCore.Mvc;
using TeamSolution.Repository.Interface;

namespace TeamSolution.Controllers
{
    [Route("storeapi")]
    [ApiController]
    public class StoreController : Controller
    {
        private readonly IStoreRepository _storeRepository;
        public StoreController(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }
    }
}
