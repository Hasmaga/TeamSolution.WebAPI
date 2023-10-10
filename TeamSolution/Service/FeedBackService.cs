using TeamSolution.Model;
using TeamSolution.Repository.Interface;
using TeamSolution.Service.Interface;

namespace TeamSolution.Service
{
    public class FeedBackService : IFeedBackService
    {
        private readonly IFeedBackRepository _feedBackRepository;

        public FeedBackService(IFeedBackRepository feedBackRepository)
        {
            _feedBackRepository = feedBackRepository;
        }

        public Task<FeedBack> GetFeedBackById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
