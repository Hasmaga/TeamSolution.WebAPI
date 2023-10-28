using System.ComponentModel.DataAnnotations.Schema;
using TeamSolution.Model.Abstract;

namespace TeamSolution.Model
{
    [Table("OrderDetail", Schema ="dbo")]
    public class OrderDetail : Common
    {
        [Column("OrderId")]
        public Guid OrderId { get; set; }
        public Order? Order { get; set; }

        [Column("StoreServiceId")]
        public Guid StoreServiceId { get; set; }
        public StoreService? StoreService { get; set; }

        [Column("Weight")]
        public decimal Weight { get; set; }

        [Column("Price")]
        public decimal Price { get; set; }        
    }
}
