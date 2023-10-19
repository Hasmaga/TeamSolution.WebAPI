using TeamSolution.Repository.Interface;

namespace TeamSolution.Repository
{
    public class CustomerComplainStoreRepository : ICustomerComplainStoreRepository
    {
        private readonly ICustomerComplainStoreRepository _customerComplainStoreRepository;
        public CustomerComplainStoreRepository(ICustomerComplainStoreRepository customerComplainStoreRepository)
        {
            _customerComplainStoreRepository = customerComplainStoreRepository;
        }
    }
}
