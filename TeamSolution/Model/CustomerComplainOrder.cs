using System.ComponentModel.DataAnnotations.Schema;
using TeamSolution.Model.Abstract;

namespace TeamSolution.Model
{
    [Table("CustomerComplainOrder")]
    public class CustomerComplainOrder : Common
    {
        [Column("OrderId")]
        public Guid OrderId { get; set; }
        public Order Order { get; set; }

        [Column("MainComplain")]
        public string MainComplain { get; set; }

        [Column("Description")]
        public string Description { get; set; }

        [Column("IsDelete")]
        public bool IsDelete { get; set; } = false;

        [Column("CreateDateTime")]
        public DateTime CreateDateTime { get; set; }

        [Column("UpdateDateTime")]
        public DateTime? UpdateDateTime { get; set; }

        [Column("IsClose")]
        public bool IsClose { get; set; } = false;

        [Column("IsCloseDateTime")]
        public DateTime? IsCloseDateTime { get; set; }

        #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public CustomerComplainOrder(Guid orderId, string mainComplain, string description, bool isDelete, DateTime createDateTime, DateTime? updateDateTime, bool isClose, DateTime? isCloseDateTime)
        #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            OrderId = orderId;
            MainComplain = mainComplain;
            Description = description;
            IsDelete = isDelete;
            CreateDateTime = createDateTime;
            UpdateDateTime = updateDateTime;
            IsClose = isClose;
            IsCloseDateTime = isCloseDateTime;
        }
    }
}
