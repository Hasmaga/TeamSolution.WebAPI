using TeamSolution.Model;
using TeamSolution.Repository.Interface;
using TeamSolution.Service.Interface;

namespace TeamSolution.Service
{
    public class StoreProviderService : IStoreService
    {
        private readonly IStoreRepository _storeRepository;
        public StoreProviderService(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }

        public Task<Store> GetAllStore()
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateStoreRepository(Store store)
        {
            throw new NotImplementedException();
        }
    }
}
