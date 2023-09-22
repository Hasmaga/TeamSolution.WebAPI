using System.ComponentModel.DataAnnotations.Schema;
using TeamSolution.Model.Abstract;

namespace TeamSolution.Model
{
    [Table("Store", Schema = "dbo")]
    public class Store : Common
    {
        [Column("StoreModeSettingId")]
        public Guid StoreModeSettingId { get; set; }
        public StoreModeSetting StoreModeSeting { get; set; }

        [Column("Address")]
        public string Address { get; set; }

        [Column("Phone")]
        public string Phone { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<FeedBack> FeedBacks { get; set; }

        public Store(Guid storeModeSettingId, string address, string phone)
        {
            StoreModeSettingId = storeModeSettingId;
            Address = address;
            Phone = phone;
        }
    }
}
