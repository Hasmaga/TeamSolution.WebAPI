using TeamSolution.ViewModel.StoreService;

namespace TeamSolution.ViewModel.Store
{
    public class GetStoreAndStoreServiceReqDto
    {
        public Guid Id { get; set; }
        public string StoreName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public decimal? StoreRating { get; set; }
        public string? StoreDescription { get; set; }
        public string? StoreImage { get; set; }
        public string? OperationTime { get; set; }
        public string? Phone { get; set; }
        public ICollection<GetStoreServiceReqDto> StoreServices { get; set; } = null!;
    }
}
