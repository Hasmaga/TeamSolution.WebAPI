using System.ComponentModel.DataAnnotations.Schema;
using TeamSolution.Model.Abstract;

namespace TeamSolution.Model
{
    [Table("Status", Schema = "dbo")]
    public class Status : Common
    {
        [Column("StatusName")]
        public string StatusName { get; set; } = null!;

        [Column("IsDeleted")]
        public bool IsDeleted { get; set; }

        [Column("Description")]
        public string Description { get; set; } = null!;

        // Relationship
        public ICollection<Account>? Accounts { get; set; }
        public ICollection<Order>? Orders { get; set; }       
    }
}
