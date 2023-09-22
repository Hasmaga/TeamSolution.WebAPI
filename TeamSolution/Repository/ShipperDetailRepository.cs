using TeamSolution.DAO.Interface;
using TeamSolution.Repository.Interface;

namespace TeamSolution.Repository
{
    public class ShipperDetailRepository : IShipperDetailRepository
    {
        private readonly IShipperDetailDAO _shipperDetailDAO;
        public ShipperDetailRepository(IShipperDetailDAO shipperDetailDAO)
        {
            _shipperDetailDAO = shipperDetailDAO;
        }
    }
}
