using AutoMapper;
using System.Security.Claims;
using TeamSolution.Enum;
using TeamSolution.Model;
using TeamSolution.Repository;
using TeamSolution.Repository.Interface;
using TeamSolution.Service.Interface;
using TeamSolution.ViewModel.StoreService;
using static System.Net.WebRequestMethods;

namespace TeamSolution.Service
{
    public class StoreServiceService : IStoreServiceService
    {
        private readonly IStoreServiceRepository _storeServiceRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IHttpContextAccessor _http;
        private readonly IAccountRepository _accountRepository;
        private readonly IStoreRepository _storeRepository;

        public StoreServiceService(IStoreServiceRepository storeServiceRepository, IMapper mapper, ILogger<StoreProviderService> logger, IHttpContextAccessor http, IAccountRepository accountRepository, IStoreRepository storeRepository)
        {
            _storeServiceRepository = storeServiceRepository;
            _mapper = mapper;
            _logger = logger;
            _http = http;
            _accountRepository = accountRepository;
            _storeRepository = storeRepository;
        }

        public async Task<List<GetStoreServiceReqDto>> GetStoreServiceByStoreIdServiceAsync(Guid id)
        {
            _logger.LogInformation("GetStoreServiceByStoreIdServiceAsync");
            try
            {
                var result = await _storeServiceRepository.GetStoreServiceByStoreIdRepositoryAsync(id);
                // Convet StoreService to GetStoreServiceReqDto
                var storeService = _mapper.Map<List<GetStoreServiceReqDto>>(result);
                return storeService;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<List<GetStoreServiceReqDto>> GetStoreServiceByHttpContextServiceAsync()
        {
            _logger.LogInformation("GetStoreServiceByStoreIdServiceAsync");
            try
            {
                var userLogged = await _accountRepository.GetUserByIdAsync(GetSidLogged());
                if (userLogged == null)
                {
                    throw new Exception(ErrorCode.USER_NOT_FOUND);
                }
                var store = await _storeRepository.GetStoreByAccountIdRepositoryAsync(userLogged.Id);
                if (store == null)
                {
                    throw new Exception(ResponseCodeConstantsStore.STORE_NOT_FOUND);
                }
                var result = await _storeServiceRepository.GetStoreServiceByStoreIdRepositoryAsync(store.Id);
                // Convet StoreService to GetStoreServiceReqDto
                var storeService = _mapper.Map<List<GetStoreServiceReqDto>>(result);
                return storeService;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<bool> CreateStoreServiceServiceAsync(CreateStoreServiceReqDto createStoreServiceReqDto)
        {
            try {
                var userLogged = await _accountRepository.GetUserByIdAsync(GetSidLogged());
                if (userLogged == null)
                {
                    throw new Exception(ErrorCode.USER_NOT_FOUND);
                }
                var store = await _storeRepository.GetStoreByAccountIdRepositoryAsync(userLogged.Id);
                if (store == null)
                {
                    throw new Exception(ResponseCodeConstantsStore.STORE_NOT_FOUND);
                }
                var newStoreService = new StoreService
                {                    
                    StoreId = store.Id,
                    ServiceDescription = createStoreServiceReqDto.ServiceDescription,
                    ServicePrice = createStoreServiceReqDto.ServicePrice,
                    ServiceDuration = createStoreServiceReqDto.ServiceDuration,
                    ServiceType = createStoreServiceReqDto.ServiceType
                };
                if (await _storeServiceRepository.CreateStoreServiceRepository(newStoreService))
                {
                    return true;
                }
                return false;                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
            
        }

        private Guid GetSidLogged()
        {
            var sid = _http.HttpContext?.User.FindFirst(ClaimTypes.Sid)?.Value;
            if (sid == null)
            {
                throw new Exception(ErrorCode.USER_NOT_FOUND);
            }
            return Guid.Parse(sid);
        }
        
    }
}
