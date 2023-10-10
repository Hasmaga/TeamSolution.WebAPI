using TeamSolution.Model;

namespace TeamSolution.Service.Interface
{
    public interface IFeedBackService
    {
        Task<FeedBack> GetFeedBackById(Guid id);
    }
}
