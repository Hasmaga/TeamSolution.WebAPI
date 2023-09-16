using System.ComponentModel.DataAnnotations.Schema;
using TeamSolution.Model.Abstract;

namespace TeamSolution.Model
{
    [Table("Status", Schema = "dbo")]
    public class Status : Common
    {
        [Column("StatusName")]
        public string StatusName { get; set; }

        [Column("IsDeleted")]
        public bool IsDeleted { get; set; }

        [Column("Description")]
        public string Description { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
        public ICollection<ShipperDetail> ShipperDetails { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<Store> Stores { get; set; }

        public Status(string statusName, bool isDeleted, string description)
        {
            StatusName = statusName;
            IsDeleted = isDeleted;
            Description = description;
        }
    }
}
