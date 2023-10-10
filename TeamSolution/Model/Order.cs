using System.ComponentModel.DataAnnotations.Schema;
using TeamSolution.Model.Abstract;

namespace TeamSolution.Model
{
    [Table("Order", Schema ="dbo")]
    public class Order : Common
    {
        [Column("CustomerId")]
        public Guid CustomerId { get; set; }
        public Account Customer { get; set; }

        [Column("OrderAddress")]
        public string OrderAddress { get; set; }

        [Column("PhoneCustomer")]
        public string PhoneCustomer { get; set; }

        [Column("CreateDateTime")]
        public DateTime CreateDateTime { get; set; }        

        [Column("PaymentMethod")]
        public string PaymentMethod { get; set; } // thanh toan khi nhan hang or thanh toan truoc

        [Column("StoreId")]
        public Guid StoreId { get; set; }
        public Store Store { get; set; }

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
        public Status StatusOrder { get; set; }

        [Column("TourShipperId")]
        public Guid? TourShipperId { get; set; }
        public TourShipper TourShipper { get; set; }        

        [Column("UpdateDateTime")]
        public DateTime? UpdateDateTime { get; set; }

        [Column("IsDelete")]
        public bool IsDelete { get; set; } = false;

        [Column("DeleteDateTime")]
        public DateTime? DeleteDateTime { get; set; }

        // RelationShip        
        public ICollection<OrderDetail> OrderDetails { get; set; }
        public ICollection<CustomerComplainOrder> CustomerComplainOrders { get; set; }        

        #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Order(Guid customerId, string orderAddress, string phoneCustomer, DateTime createDateTime, string paymentMethod, Guid storeId, DateTime timeTakeOrder, DateTime? timeShipperTakeOrder, DateTime timeDeliverOrder, DateTime? timeShipperDeliverOrder, Guid statusOrderId, Guid? tourShipperId, DateTime? updateDateTime, bool isDelete, DateTime? deleteDateTime)
        #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            CustomerId = customerId;
            OrderAddress = orderAddress;
            PhoneCustomer = phoneCustomer;
            CreateDateTime = createDateTime;            
            PaymentMethod = paymentMethod;
            StoreId = storeId;
            TimeTakeOrder = timeTakeOrder;
            TimeShipperTakeOrder = timeShipperTakeOrder;
            TimeDeliverOrder = timeDeliverOrder;
            TimeShipperDeliverOrder = timeShipperDeliverOrder;
            StatusOrderId = statusOrderId;
            TourShipperId = tourShipperId;
            UpdateDateTime = updateDateTime;
            IsDelete = isDelete;
            DeleteDateTime = deleteDateTime;
        }
    }
}
// Co the add MessageId de customer va shipper chat voi nhau or chat voi store