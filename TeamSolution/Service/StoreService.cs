using TeamSolution.Model;
using TeamSolution.Repository.Interface;
using TeamSolution.Service.Interface;

namespace TeamSolution.Service
{
    public class StoreService : IStoreService
    {
        private readonly IStoreRepository _storeRepository;
        public StoreService(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }

        public Task<Store> GetAllStore()
        {
            throw new NotImplementedException();
        }
    }
}
