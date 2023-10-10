using System.ComponentModel.DataAnnotations.Schema;
using TeamSolution.Model.Abstract;

namespace TeamSolution.Model
{
    [Table("StoreService", Schema ="dbo")]
    public class StoreService : Common
    {
        [Column("StoreId")]
        public Guid StoreId { get; set; }
        public Store Store { get; set; }

        [Column("ServiceDescription")]
        public string? ServiceDescription { get; set; }

        [Column("ServicePrice")]
        public decimal ServicePrice { get; set; }

        [Column("ServiceDuration")]
        public int? ServiceDuration { get; set; }

        [Column("ServiceType")]
        public string? ServiceType { get; set; }

        [Column("IsAvailable")]
        public bool IsAvailable { get; set; } = true;

        [Column("CreateDateTime")]
        public DateTime CreateDateTime { get; set; }

        [Column("UpdateDateTime")]
        public DateTime UpdateDateTime { get; set; }

        [Column("IsDelete")]
        public bool IsDelete { get; set; } = false;

        // Relationship
        public ICollection<OrderDetail> OrderDetails { get; set; }

        #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public StoreService(Guid storeId, string? serviceDescription, decimal servicePrice, int? serviceDuration, string? serviceType, bool isAvailable, DateTime createDateTime, DateTime updateDateTime, bool isDelete)
        #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            StoreId = storeId;
            ServiceDescription = serviceDescription;
            ServicePrice = servicePrice;
            ServiceDuration = serviceDuration;
            ServiceType = serviceType;
            IsAvailable = isAvailable;
            CreateDateTime = createDateTime;
            UpdateDateTime = updateDateTime;
            IsDelete = isDelete;
        }
    }
}
