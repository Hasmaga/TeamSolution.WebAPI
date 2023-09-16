using System.ComponentModel.DataAnnotations.Schema;
using TeamSolution.Model.Abstract;

namespace TeamSolution.Model
{
    [Table("Order", Schema ="dbo")]
    public class Order : Common
    {
        [Column("OrderDetailId")]
        public Guid OrderDetailId { get; set; }
        public OrderDetail OrderDetail { get; set; }

        [Column("UserId")]
        public Guid UserId { get; set; }
        public User User { get; set; }

        public Order(Guid orderDetailId, Guid userId)
        {
            OrderDetailId = orderDetailId;
            UserId = userId;
        }
    }
}
