using AutoMapper;
using TeamSolution.Model;
using TeamSolution.Repository.Interface;
using TeamSolution.Service.Interface;
using TeamSolution.ViewModel.Store;

namespace TeamSolution.Service
{
    public class StoreProviderService : IStoreService
    {
        private readonly IStoreRepository _storeRepository;
        private IMapper _mapper;

        public StoreProviderService(IStoreRepository storeRepository,IMapper mapper)
        {
            _storeRepository = storeRepository;
            _mapper = mapper;
        }

        public Task<Store> GetStoreById(Guid id)
        {
            return _storeRepository.GetStoreByIdRepositoryAsync(id);
        }
        public Task<ICollection<Store>> GetAllStore() 
        {
            return _storeRepository.GetAll();
        }
        public Task<Guid> CreateStoreAsync(StoreModel store)
        {
            var entity = new Store
            {
                StoreName = store.StoreName,
                StoreDescription = store.StoreDescription,
                Address = store.Address,
                StoreManagerId = store.StoreManagerId,
                OperationTime = store.OperationTime,
                StoreImage = store.StoreImage,

            };                
            return _storeRepository.CreateAsync(entity);
        }
        public Task<Guid> UpdateStoreAsync(UpdateStoreRequestModel updateStoreRequest)
        {
            return _storeRepository.UpdateAsync(updateStoreRequest);
        }
        public Task<Guid> DeleteStoreAsync(Guid id)
        {
            return _storeRepository.DeleteAsync(id);
        }

        public Task<bool> CreateStoreRepository(Store store)
        {
            throw new NotImplementedException();
        }
    }
}
