namespace TeamSolution.ViewModel.StoreService
{
    public class CreateStoreServiceReqDto
    {
        public string? ServiceDescription { get; set; }
        public decimal ServicePrice { get; set; }
        public int? ServiceDuration { get; set; }
        public string? ServiceType { get; set; }
    }
}
