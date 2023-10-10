using System.ComponentModel.DataAnnotations.Schema;
using TeamSolution.Model.Abstract;

namespace TeamSolution.Model
{
    [Table("OrderDetail", Schema ="dbo")]
    public class OrderDetail : Common
    {
        [Column("OrderId")]
        public Guid OrderId { get; set; }
        public Order Order { get; set; }


        [Column("StoreServiceId")]
        public Guid StoreServiceId { get; set; }
        public StoreService StoreService { get; set; }

        [Column("Weight")]
        public decimal Weight { get; set; }

        [Column("Price")]
        public decimal Price { get; set; }

        #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public OrderDetail(Guid orderId, Guid storeServiceId, decimal weight, decimal price)
        #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            OrderId = orderId;
            StoreServiceId = storeServiceId;
            Weight = weight;
            Price = price;
        }
    }
}
