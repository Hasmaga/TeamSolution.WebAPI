namespace TeamSolution.ViewModel.Store
{
    public class StoreModel
    {
        public string StoreName { get; set; }
        public string Address { get; set; }
        public string? StoreDescription { get; set; }
        public string Phone { get; set; }
        public string? StoreImage { get; set; }
        public Guid StoreManagerId { get; set; }
        public string? OperationTime { get; set; }
    }
}
