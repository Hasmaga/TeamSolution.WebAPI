using TeamSolution.Model;

namespace TeamSolution.Repository.Interface
{
    public interface IFeedBackRepository
    {
        Task<FeedBack> GetFeedBackById(Guid id);
    }
}
