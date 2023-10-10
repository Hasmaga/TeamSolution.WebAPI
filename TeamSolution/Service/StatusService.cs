using TeamSolution.Repository.Interface;
using TeamSolution.Service.Interface;

namespace TeamSolution.Service
{
    public class StatusService : IStatusService
    {
        private readonly IStatusRepository _statusRepository;
        public StatusService(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }
    }
}
