using System.ComponentModel.DataAnnotations.Schema;
using TeamSolution.Model.Abstract;

namespace TeamSolution.Model
{
    [Table("FeedBack", Schema = "dbo")]
    public class FeedBack : Common
    {
        [Column("Rating")]
        public int Rating { get; set; }

        [Column("Comment")]        
        public string Comment { get; set; }
        
        [Column("CustomerId")]
        public Guid CustomerId { get; set; }
        public Account Customer { get; set; }

        [Column("IsDeleted")]
        public bool IsDeleted { get; set; } = false;

        [Column("CreateDateTime")]
        public DateTime CreateDateTime { get; set; }

        [Column("UpdateDatetime")]
        public DateTime? UpdateDatetime { get; set; }

        [Column("StoreId")]
        public Guid StoreId { get; set; }
        public Store Store { get; set; }

        #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public FeedBack(int rating, string comment, Guid customerId, bool isDeleted, DateTime createDateTime, DateTime? updateDatetime, Guid storeId)
        #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            Rating = rating;
            Comment = comment;
            CustomerId = customerId;
            IsDeleted = isDeleted;
            CreateDateTime = createDateTime;
            UpdateDatetime = updateDatetime;
            StoreId = storeId;
        }
    }
}
