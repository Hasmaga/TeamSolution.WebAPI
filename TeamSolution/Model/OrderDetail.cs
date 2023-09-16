using System.ComponentModel.DataAnnotations.Schema;
using TeamSolution.Model.Abstract;

namespace TeamSolution.Model
{
    [Table("OrderDetail", Schema ="dbo")]
    public class OrderDetail : Common
    {
        [Column("StatusId")]
        public Guid StatusId { get; set; }
        public Status Status { get; set; }

        [Column("Weight")]
        public decimal Weight { get; set; }

        [Column("OrderDateTime")]
        public DateTime OrderDateTime { get; set; }

        [Column("StoreId")]
        public Guid StoreId { get; set; }
        public Store Store { get; set; }

        [Column("StoreModeSettingId")]
        public Guid StoreModeSettingId { get; set; }
        public StoreModeSeting StoreModeSeting { get; set; }

        [Column("TotalMoney")]
        public decimal TotalMoney { get; set; }

        [Column("DoneTime")]
        public DateTime DoneTime { get; set; }

        [Column("ShipperDetailId")]
        public Guid ShipperDetailId { get; set; }
        public ShipperDetail ShipperDetail { get; set; }

        [Column("StaffBeginId")]
        public Guid StaffBeginId { get; set; }
        public User StaffBegin { get; set; }

        [Column("StaffDoneId")]
        public Guid StaffDoneId { get; set; }
        public User StaffDone { get; set; }

        [Column("Description")]
        public string Description { get; set; }

        [Column("ShipFee")]
        public decimal ShipFee { get; set; }

        [Column("AppFee")]
        public decimal AppFee { get; set; }

        public ICollection<Order> Orders { get; set; }

        public OrderDetail(Guid statusId, decimal weight, DateTime orderDateTime, Guid storeId, Guid storeModeSettingId, decimal totalMoney, DateTime doneTime, Guid shipperDetailId, Guid staffBeginId, Guid staffDoneId, string description, decimal shipFee, decimal appFee)
        {
            StatusId = statusId;
            Weight = weight;
            OrderDateTime = orderDateTime;
            StoreId = storeId;
            StoreModeSettingId = storeModeSettingId;
            TotalMoney = totalMoney;
            DoneTime = doneTime;
            ShipperDetailId = shipperDetailId;
            StaffBeginId = staffBeginId;
            StaffDoneId = staffDoneId;
            Description = description;
            ShipFee = shipFee;
            AppFee = appFee;
        }
    }
}
