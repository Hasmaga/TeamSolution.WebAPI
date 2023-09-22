using TeamSolution.DAO.Interface;
using TeamSolution.Repository.Interface;

namespace TeamSolution.Repository
{
    public class StatusRepository : IStatusRepository
    {
        private readonly IStatusDAO _statusDAO;
        public StatusRepository(IStatusDAO statusDAO)
        {
            _statusDAO = statusDAO;
        }
    }
}
