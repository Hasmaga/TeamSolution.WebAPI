using System.Security.Claims;
using TeamSolution.Enum;
using TeamSolution.Model;
using TeamSolution.Repository.Interface;
using TeamSolution.Service.Interface;
using TeamSolution.ViewModel.Feedback;

namespace TeamSolution.Service
{
    public class FeedBackService : IFeedBackService
    {
        private readonly IFeedBackRepository _feedBackRepository;
        private readonly ILogger _logger;
        private readonly IHttpContextAccessor _http;
        private readonly IAccountRepository _accountRepository;
        private readonly IStoreRepository _storeRepository;

        public FeedBackService(IFeedBackRepository feedBackRepository,
                               ILogger<FeedBackService> logger,
                               IHttpContextAccessor http,
                               IAccountRepository accountRepository,
                               IStoreRepository storeRepository)
        {
            _feedBackRepository = feedBackRepository;
            _logger = logger;
            _http = http;
            _accountRepository = accountRepository;
            _storeRepository = storeRepository;
        }

        public async Task<bool> CreateFeedbackServiceAsync(CreateNewFeedbackReqDto feedback)
        {
            try
            {
                var store = await _storeRepository.GetStoreByIdRepositoryAsync(feedback.StoreId);
                var userLogged = await _accountRepository.GetUserByIdAsync(GetSidLogged());
                if (store is not null)
                {
                    FeedBack newFeedBack = new FeedBack
                        (
                        rating: feedback.Rating,
                        comment: feedback.Comment,
                        customerId: userLogged.Id,
                        isDeleted: false,
                        createDateTime: DateTime.Now,
                        updateDatetime: null,
                        storeId: feedback.StoreId
                        );
                    var result = await _feedBackRepository.CreateFeedbackRepositoryAsync(newFeedBack);
                    if (result)
                    {
                        return true;
                    }
                }
                return false;
                
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Create Feedback At Service: " + ex.ToString());
                throw;
            }
        }

        public Task<FeedBack> GetFeedBackById(Guid id)
        {
            throw new NotImplementedException();
        }

        #region private method
        private Guid GetSidLogged()
        {
            var sid = _http.HttpContext.User.FindFirst(ClaimTypes.Sid).Value;
            if (sid == null)
            {
                throw new Exception(ErrorCode.USER_NOT_FOUND);
            }
            return Guid.Parse(sid);
        }
        #endregion
    }
}
