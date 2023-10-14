using Microsoft.AspNetCore.Mvc;
using TeamSolution.Service.Interface;

namespace TeamSolution.Controllers
{
    [Route("customercomplainstoreapi")]
    [ApiController]
    public class CustomerComplainStoreController : Controller
    {
        private ICustomerComplainStoreService _customerComplainStoreService;
        public CustomerComplainStoreController(ICustomerComplainStoreService customerComplainStoreService)
        {
            _customerComplainStoreService = customerComplainStoreService;
        }
    }
}
