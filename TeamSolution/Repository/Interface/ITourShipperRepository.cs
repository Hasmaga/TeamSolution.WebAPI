using TeamSolution.Model;

namespace TeamSolution.Repository.Interface
{
    public interface ITourShipperRepository
    {
        Task<bool> AddTourShipper(TourShipper tourShipper);
    }
}
