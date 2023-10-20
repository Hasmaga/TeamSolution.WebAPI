using TeamSolution.Repository;
using TeamSolution.Repository.Interface;
using TeamSolution.Service.Interface;

namespace TeamSolution.Service
{
    public class IssueService : IIssueService
    {
        private IIssueRepository _repository;
        public IssueService (IIssueRepository repository)
        {
            _repository = repository;
        }




        // commebt

    }
}
