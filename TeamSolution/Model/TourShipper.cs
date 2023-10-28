using System.ComponentModel.DataAnnotations.Schema;
using TeamSolution.Model.Abstract;

namespace TeamSolution.Model
{
    [Table("TourShipper")]
    public class TourShipper : Common
    {
        [Column("ShipperManagerId")]
        public Guid ShipperManagerId { get; set; }
        public Account? ShipperManager { get; set; }

        [Column("ShipperId")]
        public Guid ShipperId { get; set; }
        public Account? Shipper { get; set; }        

        [Column("StatusId")]
        public Guid StatusId { get; set; }
        public Status? Status { get; set; }

        [Column("DeliverOrGet")]
        public string DeliverOrGet { get; set; } = null!;        

        // Relationship
        public ICollection<ShipperReportIssueTour>? ShipperReportIssueTours { get; set; }
        public ICollection<Order>? TourShipOrders { get; set; }
        public ICollection<Order>? TourGetOrders { get; set; }        
    }
}
