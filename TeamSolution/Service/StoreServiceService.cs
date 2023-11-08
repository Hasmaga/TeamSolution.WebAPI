using AutoMapper;
using TeamSolution.Model;
using TeamSolution.Repository.Interface;
using TeamSolution.Service.Interface;
using TeamSolution.ViewModel.StoreService;

namespace TeamSolution.Service
{
    public class StoreServiceService : IStoreServiceService
    {
        private readonly IStoreServiceRepository _storeServiceRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public StoreServiceService(IStoreServiceRepository storeServiceRepository, IMapper mapper, ILogger<StoreProviderService> logger)
        {
            _storeServiceRepository = storeServiceRepository;
            _mapper = mapper;
            _logger = logger;
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
    }
}
