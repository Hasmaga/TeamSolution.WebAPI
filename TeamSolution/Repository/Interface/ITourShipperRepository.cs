using TeamSolution.Model;

namespace TeamSolution.Repository.Interface
{
    public interface ITourShipperRepository
    {
        Task<TourShipper?> GetTourByTourIdRepositoryAsync(Guid tourId);
        Task<TourShipper?> GetTourByTourIdIncludeOrderRepositoryAsync(Guid tourId);
        Task<ICollection<TourShipper>> GetAllToursRepositoryAsync(bool includeIsDeleted = false);
        Task<bool> CreateTourRepositoryAsync(TourShipper tour);
        Task<Guid> UpdateTourRepositoryAsync(TourShipper tour, CancellationToken cancellationToken = default);
        Task<Guid> DeleteTourRepositoryAsync(TourShipper tour, CancellationToken cancellationToken = default);
        Task<List<Account>> GetAllAccountsWithRoleAsync(string roleEnum);
    }
}
