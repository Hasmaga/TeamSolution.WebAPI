using System.ComponentModel.DataAnnotations.Schema;
using TeamSolution.ViewModel.Order;

namespace TeamSolution.ViewModel.TourShipper
{
    public class ResponseTourShipperModel
    {
        
        public Guid? ShipperManagerId { get; set; }
        //public Account? ShipperManager { get; set; }


        public Guid? ShipperId { get; set; }
        //public Account? Shipper { get; set; }

        public Guid? StatusId { get; set; }
        //public Status? Status { get; set; }

        public string? DeliverOrGet { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        // Relationship
        //public ICollection<ShipperReportIssueTour>? ShipperReportIssueTours { get; set; }
        public ICollection<ResponseOrderModel>? TourShipOrders { get; set; }
        public ICollection<ResponseOrderModel>? TourGetOrders { get; set; }
    }
}
