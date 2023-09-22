using TeamSolution.DAO.Interface;
using TeamSolution.Model;
using TeamSolution.Repository.Interface;

namespace TeamSolution.Repository
{
    public class StoreRepository : IStoreRepository
    {
        private readonly IStoreDAO _storeDAO;
        public StoreRepository(IStoreDAO storeDAO)
        {
            _storeDAO = storeDAO;
        }

        public Task<Store> GetAllStore()
        {
            throw new NotImplementedException();
        }
    }
}
