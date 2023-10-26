using TeamSolution.Model;
using TeamSolution.ViewModel.Feedback;

namespace TeamSolution.Service.Interface
{
    public interface IFeedBackService
    {
        Task<FeedBack> GetFeedBackById(Guid id);
        Task<bool> CreateFeedbackServiceAsync(CreateNewFeedbackReqDto feedback);
    }
}
