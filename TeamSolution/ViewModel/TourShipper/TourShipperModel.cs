using TeamSolution.ViewModel.Order;

namespace TeamSolution.ViewModel.TourShipper
{
    public class TourShipperModel
    {
        public Guid? ShipperManagerId { get; set; }
        public Guid? ShipperId { get; set; }
        public string? StatusName { get; set; }
        public string? DeliverOrGet { get; set; }
        public List<Guid>? Orders { get; set; }
    }
}
