using AutoMapper;
using TeamSolution.Enum;
using TeamSolution.Model;
using TeamSolution.Repository.Interface;
using TeamSolution.Service.Interface;
using TeamSolution.ViewModel.Store;
using TeamSolution.ViewModel.StoreService;

namespace TeamSolution.Service
{
    public class StoreProviderService : IStoreService
    {
        private readonly IStoreRepository _storeRepository;
        private IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IStoreServiceRepository _storeServiceRepository;

        public StoreProviderService(IStoreRepository storeRepository, IMapper mapper, ILogger<StoreProviderService> logger, IStoreServiceRepository storeServiceRepository)
        {
            _storeRepository = storeRepository;
            _mapper = mapper;
            _logger = logger;
            _storeServiceRepository = storeServiceRepository;
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
                StoreImage = store.StoreImage
            };
            return _storeRepository.CreateAsync(entity);
        }
        public Task<Guid> UpdateStoreAsync(UpdateStoreRequestModel updateStoreRequest)
        {
            Store store = new Store();
            _mapper.Map(updateStoreRequest.StoreModel, store);
            store.UpdateDateTime= DateTime.Now;
            return _storeRepository.UpdateAsync(store);
        }
        public Task<Guid> DeleteStoreAsync(Guid id)
        {
            Store store = new Store();
            store.Id = id;
            store.DeleteDateTime = DateTime.Now;
            return _storeRepository.DeleteAsync(store);
        }

        public async Task<ICollection<Store>> GetFilterStoreByStoreNameServiceAsync(string storeName)
        {
            try
            {
                var result = await _storeRepository.GetFilterStoreByStoreNameRepositoryAsync(storeName);                
                return result.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<GetStoreAndStoreServiceReqDto> GetStoreInformationAndStoreServiceByStoreIdServiceAsync(Guid id)
        {
            _logger.LogInformation("Get store information and store service by store id");
            try
            {
                var result = new GetStoreAndStoreServiceReqDto();
                var storeService = new List<GetStoreServiceReqDto>();
                var store = await _storeRepository.GetStoreByIdRepositoryAsync(id);
                if (store == null)
                {
                    throw new Exception(ResponseCodeConstantsStore.STORE_NOT_FOUND);
                }
                var storeServices = await _storeServiceRepository.GetStoreServiceByStoreIdRepositoryAsync(id);
                if (storeService == null)
                {
                    throw new Exception(ResponseCodeConstantsStoreService.STORE_SERVICE_NOT_FOUND);
                }
                
                result.Id = store.Id;
                result.StoreName = store.StoreName;
                result.Address = store.Address;
                result.StoreRating = store.StoreRating;
                result.StoreDescription = store.StoreDescription;
                result.StoreImage = store.StoreImage;
                result.OperationTime = store.OperationTime;
                result.Phone = store.Phone;
                foreach (var item in storeServices)
                {
                    storeService.Add(new GetStoreServiceReqDto
                    {
                        Id = item.Id,
                        ServiceDescription = item.ServiceDescription,
                        ServicePrice = item.ServicePrice,
                        ServiceDuration = item.ServiceDuration,
                        ServiceType = item.ServiceType
                    });
                }
                result.StoreServices = storeService;
                return result;                
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
