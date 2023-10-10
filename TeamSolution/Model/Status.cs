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

        // Relationship
        public ICollection<Account> Accounts { get; set; }
        public ICollection<Order> Orders { get; set; }

        public Status(string statusName, bool isDeleted, string description)
        {
            StatusName = statusName;
            IsDeleted = isDeleted;
            Description = description;
        }
    }
}
