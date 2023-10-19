namespace TeamSolution.Model.Dto
{
    public class CreateNewOrderReqDto
    {
        public String OrderAddress { get; set; }
        public String OrderPhone { get; set; }
        public String PaymentMethod { get; set; }
        public Guid StoreId { get; set; }
        public DateTime TimeTakeOrder { get; set; }
        public DateTime TimeDeliveryOrder { get; set; }
        public List<CreateNewOrderDetailReqDto> OrderDetails { get; set; }
    }
}
