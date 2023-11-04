using TeamSolution.Model;
using TeamSolution.ViewModel.TourShipper;

namespace TeamSolution.Service.Interface
{
    public interface ITourShipperService
    {
        Task<bool> AddTourShipper(AddTour tourShipper);
    }
}
