using System.ComponentModel.DataAnnotations.Schema;
using TeamSolution.Model.Abstract;

namespace TeamSolution.Model
{
    [Table("StoreService", Schema ="dbo")]
    public class StoreService : Common
    {
        [Column("StoreId")]
        public Guid StoreId { get; set; }
        public Store? Store { get; set; }

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

        // Relationship
        public ICollection<OrderDetail>? OrderDetails { get; set; }

        
    }
}
