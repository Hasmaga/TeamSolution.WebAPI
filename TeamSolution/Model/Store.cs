using System.ComponentModel.DataAnnotations.Schema;
using TeamSolution.Model.Abstract;

namespace TeamSolution.Model
{
    [Table("Store", Schema = "dbo")]
    public class Store : Common
    {
        [Column("Address")]
        public string Address { get; set; }

        [Column("StoreManagerId")]
        public Guid StoreManagerId { get; set; }
        public Account StoreManager { get; set; }

        [Column("Phone")]
        public string Phone { get; set; }

        [Column("OperationTime")]
        public string? OperationTime { get; set; }

        [Column("IsDelete")]
        public bool IsDelete { get; set; } = false;

        [Column("CreateDateTime")]
        public DateTime CreateDateTime { get; set; }

        [Column("DeleteDateTime")]
        public DateTime? DeleteDateTime { get; set; }

        [Column("StoreAvalability")]
        public bool StoreAvalability { get; set; } = true;

        [Column("StoreName")]
        public string StoreName { get; set; }

        [Column("StoreDescription")]
        public string? StoreDescription { get; set; }

        [Column("StoreImage")]
        public string? StoreImage { get; set; }

        [Column("StoreRating")]
        public decimal? StoreRating { get; set; }

        // Relationship
        public ICollection<Order> Orders { get; set; }
        public ICollection<StoreService> StoreServices { get; set; }
        public ICollection<FeedBack> FeedBacks { get; set; }      

        #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Store(string address, Guid storeManagerId, string phone, string? operationTime, bool isDelete, DateTime createDateTime, DateTime? deleteDateTime, bool storeAvalability, string storeName, string? storeDescription, string? storeImage, decimal? storeRating)
        #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            Address = address;
            StoreManagerId = storeManagerId;
            Phone = phone;
            OperationTime = operationTime;
            IsDelete = isDelete;
            CreateDateTime = createDateTime;
            DeleteDateTime = deleteDateTime;
            StoreAvalability = storeAvalability;
            StoreName = storeName;
            StoreDescription = storeDescription;
            StoreImage = storeImage;
            StoreRating = storeRating;
        }
    }
}
