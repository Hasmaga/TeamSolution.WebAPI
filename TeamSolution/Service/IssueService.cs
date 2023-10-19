using TeamSolution.Repository;
using TeamSolution.Service.Interface;

namespace TeamSolution.Service
{
    public class IssueService : IIssueService
    {
        private IssueRepository _repository;
        public IssueService (IssueRepository repository)
        {
            _repository = repository;
        }




        // commebt

    }
}
