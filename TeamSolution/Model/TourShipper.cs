using System.ComponentModel.DataAnnotations.Schema;
using TeamSolution.Model.Abstract;

namespace TeamSolution.Model
{
    [Table("TourShipper")]
    public class TourShipper : Common
    {
        [Column("ShipperManagerId")]
        public Guid ShipperManagerId { get; set; }
        public Account ShipperManager { get; set; }

        [Column("ShipperId")]
        public Guid ShipperId { get; set; }
        public Account Shipper { get; set; }

        [Column("CreateDateTime")]
        public DateTime CreateDateTime { get; set; }

        [Column("UpdateDateTime")]
        public DateTime? UpdateDateTime { get; set; }

        [Column("IsDelete")]
        public bool IsDelete { get; set; } = false;

        [Column("StatusId")]
        public Guid StatusId { get; set; }
        public Status Status { get; set; }

        [Column("DeliverOrGet")]
        public string DeliverOrGet { get; set; }

        [Column("DeleteDateTime")]
        public DateTime? DeleteDateTime { get; set; }

        // Relationship
        public ICollection<ShipperReportIssueTour> ShipperReportIssueTours { get; set; }

        #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public TourShipper(Guid shipperManagerId, Guid shipperId, DateTime createDateTime, DateTime? updateDateTime, bool isDelete, Guid statusId, string deliverOrGet, DateTime? deleteDateTime)
        #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            ShipperManagerId = shipperManagerId;
            ShipperId = shipperId;
            CreateDateTime = createDateTime;
            UpdateDateTime = updateDateTime;
            IsDelete = isDelete;
            StatusId = statusId;
            DeliverOrGet = deliverOrGet;
            DeleteDateTime = deleteDateTime;
        }
    }
}
