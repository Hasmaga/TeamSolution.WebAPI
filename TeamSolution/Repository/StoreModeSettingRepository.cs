using TeamSolution.DAO.Interface;
using TeamSolution.Repository.Interface;

namespace TeamSolution.Repository
{
    public class StoreModeSettingRepository : IStoreModeSettingRepository
    {
        private readonly IStoreModeSettingDAO _storeModeSetingDAO;
        public StoreModeSettingRepository(IStoreModeSettingDAO storeModeSetingDAO)
        {
            _storeModeSetingDAO = storeModeSetingDAO;
        }
    }
}
