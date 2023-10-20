using TeamSolution.ViewModel.OrderDetail;

namespace TeamSolution.ViewModel.Order
{
    public class CreateNewOrderReqDto
    {
        public string OrderAddress { get; set; }
        public string OrderPhone { get; set; }
        public string PaymentMethod { get; set; }
        public Guid StoreId { get; set; }
        public DateTime TimeTakeOrder { get; set; }
        public DateTime TimeDeliveryOrder { get; set; }
        public List<CreateNewOrderDetailReqDto> OrderDetails { get; set; }
    }
}
