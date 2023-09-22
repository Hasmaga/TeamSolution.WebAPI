using TeamSolution.DAO.Interface;
using TeamSolution.Model;
using TeamSolution.Repository.Interface;

namespace TeamSolution.Repository
{
    public class FeedBackRepository : IFeedBackRepository
    {
        private readonly IFeedBackDAO _feedBackDAO;

        public FeedBackRepository(IFeedBackDAO feedBackDAO)
        {
            _feedBackDAO = feedBackDAO;
        }

        public Task<FeedBack> GetFeedBackById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
