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
        Task<Guid> ChangeTourStatusAcceptServiceAsync(UpdateTourShipperRequestModel tour);
        Task<Guid> ChangeTourStatusDoneServiceAsync(UpdateTourShipperRequestModel tour);
        Task<Guid> DeleteTourServiceAsync(Guid tourId);
    }
}
