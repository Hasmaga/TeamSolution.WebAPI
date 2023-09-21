using TeamSolution.DAO.Interface;
using TeamSolution.DatabaseContext;

namespace TeamSolution.DAO
{
    public class StatusDAO : IStatusDAO
    {
        private readonly ApplicationDbContext _context;

        public StatusDAO(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
