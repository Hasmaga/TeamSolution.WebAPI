using TeamSolution.Model;
using TeamSolution.ViewModel.TourShipper;

namespace TeamSolution.Service.Interface
{
    public interface ITourShipperService
    {
        Task<TourShipper?> GetTourByTourIdServiceAsync(Guid tourId);
        Task<ResponseTourShipperModel?> GetTourByTourIdIncludeOrderServiceAsync(Guid tourId);
        Task<ICollection<TourShipper>> GetAllToursServiceAsync();

        Task<bool> CreateTourServiceAsync(TourShipperModel tour);
        Task<Guid> UpdateTourServiceAsync(UpdateTourShipperRequestModel tour);
        Task<Guid> DeleteTourServiceAsync(Guid tourId);
    }
}
