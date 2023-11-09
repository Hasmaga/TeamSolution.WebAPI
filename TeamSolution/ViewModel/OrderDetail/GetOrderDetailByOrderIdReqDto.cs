namespace TeamSolution.ViewModel.OrderDetail
{
    public class GetOrderDetailByOrderIdReqDto
    {
        public Guid Id { get; set; }
        public string StoreServiceType { get; set; }
        public decimal Weight { get; set; }
    }
}
