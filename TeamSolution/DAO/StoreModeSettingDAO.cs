using TeamSolution.DAO.Interface;
using TeamSolution.DatabaseContext;

namespace TeamSolution.DAO
{
    public class StoreModeSettingDAO : IStoreModeSettingDAO
    {
        private readonly ApplicationDbContext _context;

        public StoreModeSettingDAO(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
