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
        
        [Column("UserId")]
        public Guid UserId { get; set; }
        public User User { get; set; }

        [Column("IsDeleted")]
        public bool IsDeleted { get; set; }

        [Column("CreateDateTime")]
        public DateTime CreateDateTime { get; set; }

        [Column("UpdateDatetime")]
        public DateTime UpdateDatetime { get; set; }

        [Column("StoreId")]
        public Guid StoreId { get; set; }
        public Store Store { get; set; }

        public FeedBack(int rating, string comment, Guid userId, bool isDeleted, DateTime createDateTime, DateTime updateDatetime, Guid storeId)
        {
            Rating = rating;
            Comment = comment;
            UserId = userId;
            IsDeleted = isDeleted;
            CreateDateTime = createDateTime;
            UpdateDatetime = updateDatetime;
            StoreId = storeId;
        }
    }
}
