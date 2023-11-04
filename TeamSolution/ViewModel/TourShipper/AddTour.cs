using TeamSolution.ViewModel.Order;
using TeamSolution.ViewModel.OrderDetail;

namespace TeamSolution.ViewModel.TourShipper
{
    public class AddTour
    {
        public Guid shipperId { get; set; }
        public string DeliverOrGet {  get; set; }
        public List<Guid> OrderIds { get; set; }
    }
}
