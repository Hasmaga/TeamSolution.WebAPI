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

        public Task<Store> GetAllStore()
        {
            throw new NotImplementedException();
        }
        public Task<ICollection<Store>> GetAllStoreEntity() 
        {
            return _storeRepository.GetAll();
        }
        public Task<Guid> CreateStoreAsync(StoreModel store)
        {
            var entity = new Store(
                storeName:store.StoreName,
                storeDescription:store.StoreDescription,
                address:store.Address,
                phone:store.Address,
                storeManagerId:store.StoreManagerId,
                storeAvalability:false,
                operationTime:null,
                createDateTime:DateTime.Now,
                deleteDateTime:null,
                isDelete:false,
                storeImage:null,
                storeRating:null
                );
            //Thật zô nghĩa
            //Dùng constructor rồi thì map làm cái gì nữa
            //_mapper.Map(store, entity);
            return _storeRepository.CreateAsync(entity);
        }
    }
}
