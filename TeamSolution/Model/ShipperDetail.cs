using System.ComponentModel.DataAnnotations.Schema;
using TeamSolution.Model.Abstract;

namespace TeamSolution.Model
{
    [Table("ShipperDetail", Schema ="dbo")]
    public class ShipperDetail : Common
    {
        [Column("UserId")]
        public Guid UserId { get; set; }
        public User User { get; set; }

        [Column("StatusId")]
        public Guid StatusId { get; set; }
        public Status Status { get; set; }

        [Column("Description")]
        public string Description { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }

        public ShipperDetail(Guid userId, Guid statusId, string description)
        {
            UserId = userId;
            StatusId = statusId;
            Description = description;
        }
    }
}
