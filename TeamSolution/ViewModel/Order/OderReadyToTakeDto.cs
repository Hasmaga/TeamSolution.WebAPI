namespace TeamSolution.ViewModel.Order
{
    public class OderReadyToTakeDto
    {
       
        public Guid OrderId { get; set; }
        public string? OrderAddress { get; set; }
        public string? PhoneCustomer { get; set; }
        public string? StoreName{get; set; }
        public DateTime? TimeTakeOrder { get; set; }

    }
}
