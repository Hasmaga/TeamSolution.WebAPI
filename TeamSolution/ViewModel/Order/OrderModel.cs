namespace TeamSolution.ViewModel.Order
{
    public class OrderModel
    {
        public string? OrderAddress { get; set; }
        public string? PhoneCustomer { get; set; }
        //public string? PaymentMethod { get; set; }
        //public Guid StoreId { get; set; }
        public DateTime? TimeTakeOrder { get; set; }
        public DateTime? TimeDeliverOrder { get; set; }
    }
}
