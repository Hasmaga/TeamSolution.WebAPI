using TeamSolution.Model;

namespace TeamSolution.Repository.Interface
{
    public interface IFeedBackRepository
    {
        Task<bool> CreateFeedbackRepositoryAsync(FeedBack feedBack);
    }
}
