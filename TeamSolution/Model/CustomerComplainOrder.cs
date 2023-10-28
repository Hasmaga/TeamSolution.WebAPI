using System.ComponentModel.DataAnnotations.Schema;
using TeamSolution.Model.Abstract;

namespace TeamSolution.Model
{
    [Table("CustomerComplainOrder")]
    public class CustomerComplainOrder : Common
    {
        [Column("OrderId")]
        public Guid OrderId { get; set; }
        public Order? Order { get; set; }

        [Column("MainComplain")]
        public string MainComplain { get; set; } = null!;

        [Column("Description")]
        public string Description { get; set; } = null!;
       
        [Column("IsClose")]
        public bool IsClose { get; set; } = false;

        [Column("IsCloseDateTime")]
        public DateTime? IsCloseDateTime { get; set; }        
    }
}
