namespace TeamSolution.ViewModel.Order
{
    public class StoreOrderViewDto
    {
        public Guid Id { get; set; }
        public String OrderName { get; set; } = null!;
        public string OrderAddress { get; set; } = null!;
        public string PhoneCustomer { get; set; } = null!;
        public DateTime TimeTakeOrder { get; set; }
        public DateTime TimeDeliverOrder { get; set; }        
    }
}
