using System.ComponentModel.DataAnnotations.Schema;
using TeamSolution.Model.Abstract;

namespace TeamSolution.Model
{
    [Table("Store", Schema = "dbo")]
    public class Store : Common
    {
        [Column("Address")]
        public string Address { get; set; } = null!;

        [Column("StoreManagerId")]
        public Guid StoreManagerId { get; set; }
        public Account? StoreManager { get; set; }

        [Column("Phone")]
        public string Phone { get; set; } = null!;

        [Column("OperationTime")]
        public string? OperationTime { get; set; }

        [Column("IsDelete")]
        public bool IsDelete { get; set; } = false;        

        [Column("StoreAvalability")]
        public bool StoreAvalability { get; set; } = true;

        [Column("StoreName")]
        public string StoreName { get; set; } = null!;

        [Column("StoreDescription")]
        public string? StoreDescription { get; set; }

        [Column("StoreImage")]
        public string? StoreImage { get; set; }

        [Column("StoreRating")]
        public decimal? StoreRating { get; set; }

        // Relationship
        public ICollection<Order>? Orders { get; set; }
        public ICollection<StoreService>? StoreServices { get; set; }
        public ICollection<FeedBack>? FeedBacks { get; set; }      

        
    }
}
