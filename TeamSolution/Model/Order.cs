using System.ComponentModel.DataAnnotations.Schema;
using TeamSolution.Model.Abstract;

namespace TeamSolution.Model
{
    [Table("Order", Schema ="dbo")]
    public class Order : Common
    {
        [Column("CustomerId")]
        public Guid CustomerId { get; set; }
        public Account? Customer { get; set; }

        [Column("OrderAddress")]
        public string OrderAddress { get; set; } = null!;

        [Column("PhoneCustomer")]
        public string PhoneCustomer { get; set; } = null!;

        [Column("PaymentMethod")]
        public string PaymentMethod { get; set; } = null!; // thanh toan khi nhan hang or thanh toan truoc

        [Column("StoreId")]
        public Guid StoreId { get; set; }
        public Store? Store { get; set; }

        [Column("TimeTakeOrder")]
        public DateTime TimeTakeOrder { get; set; }

        [Column("TimeShipperTakeOrder")]
        public DateTime? TimeShipperTakeOrder { get; set; }

        [Column("TimeDeliverOrder")]
        public DateTime TimeDeliverOrder { get; set; }

        [Column("TimeShipperDeliverOrder")]
        public DateTime? TimeShipperDeliverOrder { get; set; }

        [Column("StatusOrderId")]
        public Guid StatusOrderId { get; set; } // 1: Tạo đơn hàng thành công, 2: Chờ store xác nhận, 3: Store đã xác nhận, 4: Tìm shipper , 5: Shipper đã nhận đơn hàng , 6: Chờ lấy hàng, 7: Đã lấy hàng thành công, 8: Đang giao đến cửa hàng, 9: Đã giao đến cửa hàng thành công, 10: Đang giặt ủi tại cửa hàng, 11: Giặt ủi thành công, 12: Tìm shipper, 13: Shipper nhận đơn hàng, 14: Chờ shipper lấy đơn hàng, 15: Shipper lấy đơn hàng từ store thành công, 16: Shipper đang giao hàng, 17: Shipper giao hàng thành công 
        public Status? StatusOrder { get; set; }

        [Column("TourShipOrderId")]
        public Guid? TourShipOrderId { get; set; }
        public TourShipper? TourShipOrder { get; set; }

        [Column("TourGetOrderId")]
        public Guid? TourGetOrderId { get; set; }
        public TourShipper? TourGetOrder { get; set; }        

        // RelationShip        
        public ICollection<OrderDetail>? OrderDetails { get; set; }
        public ICollection<CustomerComplainOrder>? CustomerComplainOrders { get; set; }             
    }
}
// Co the add MessageId de customer va shipper chat voi nhau or chat voi store