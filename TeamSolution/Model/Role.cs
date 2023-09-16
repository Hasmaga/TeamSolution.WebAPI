using System.ComponentModel.DataAnnotations.Schema;
using TeamSolution.Model.Abstract;

namespace TeamSolution.Model
{
    [Table("Role", Schema ="dbo")]
    public class Role : Common
    {
        [Column("RoleName")]
        public string RoleName { get; set; }

        public ICollection<User> Users { get; set; }    

        public Role(string roleName)
        {
            RoleName = roleName;
        }

    }
}
