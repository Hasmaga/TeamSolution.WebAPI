using System.ComponentModel.DataAnnotations.Schema;
using TeamSolution.Model.Abstract;

namespace TeamSolution.Model
{
    [Table("Role", Schema ="dbo")]
    public class Role : Common
    {
        [Column("RoleName")]
        public string RoleName { get; set; }

        [Column("CreateDateTime")]
        public DateTime CreateDateTime { get; set; }

        [Column("UpdateDateTime")]
        public DateTime? UpdateDateTime { get; set; }

        public ICollection<Account> Accounts { get; set; }

        public Role(string roleName, DateTime createDateTime, DateTime? updateDateTime)
        {
            RoleName = roleName;
            CreateDateTime = createDateTime;
            UpdateDateTime = updateDateTime;
        }
    }
}
