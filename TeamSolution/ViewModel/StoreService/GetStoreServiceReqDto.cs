namespace TeamSolution.ViewModel.StoreService
{
    public class GetStoreServiceReqDto
    {
        public Guid Id { get; set; }
        public string? ServiceDescription { get; set; }
        public decimal ServicePrice { get; set; }
        public int? ServiceDuration { get; set; }
        public string? ServiceType { get; set; }
    }
}
