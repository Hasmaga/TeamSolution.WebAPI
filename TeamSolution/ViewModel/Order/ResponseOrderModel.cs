using System.ComponentModel.DataAnnotations.Schema;

namespace TeamSolution.ViewModel.Order
{
    public class ResponseOrderModel
    {
        public Guid? Id { get; set; }
        public Guid? CustomerId { get; set; }
        //public Account? Customer { get; set; }

        public string? OrderAddress { get; set; }

        public string? PhoneCustomer { get; set; } 

        public string? PaymentMethod { get; set; }

        public Guid? StoreId { get; set; }
        //public Store? Store { get; set; }

        public DateTime? TimeTakeOrder { get; set; }

        public DateTime? TimeShipperTakeOrder { get; set; }

        public DateTime TimeDeliverOrder { get; set; }

        public DateTime? TimeShipperDeliverOrder { get; set; }

        public Guid? StatusOrderId { get; set; } // 1: Tạo đơn hàng thành công, 2: Chờ store xác nhận, 3: Store đã xác nhận, 4: Tìm shipper , 5: Shipper đã nhận đơn hàng , 6: Chờ lấy hàng, 7: Đã lấy hàng thành công, 8: Đang giao đến cửa hàng, 9: Đã giao đến cửa hàng thành công, 10: Đang giặt ủi tại cửa hàng, 11: Giặt ủi thành công, 12: Tìm shipper, 13: Shipper nhận đơn hàng, 14: Chờ shipper lấy đơn hàng, 15: Shipper lấy đơn hàng từ store thành công, 16: Shipper đang giao hàng, 17: Shipper giao hàng thành công 
        //public Status? StatusOrder { get; set; }

        public Guid? TourShipOrderId { get; set; }
        //public TourShipper? TourShipOrder { get; set; }

        public Guid? TourGetOrderId { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        //public TourShipper? TourGetOrder { get; set; }

        // RelationShip        
        /*public ICollection<OrderDetail>? OrderDetails { get; set; }
        public ICollection<CustomerComplainOrder>? CustomerComplainOrders { get; set; }*/
    }
}
