using System.ComponentModel.DataAnnotations.Schema;
using TeamSolution.Model.Abstract;

namespace TeamSolution.Model
{
    [Table("User", Schema ="dbo")]
    public class User : Common
    {
        [Column("FirstName")]
        public string FirstName { get; set; }

        [Column("LastName")]
        public string LastName { get; set; }

        [Column("Email")]
        public string Email { get; set; }

        [Column("PasswordHash")]
        public string PasswordHash { get; set; }

        [Column("PasswordSalt")]
        public string PasswordSalt { get; set; }

        [Column("PhoneNumber")]
        public string PhoneNumber { get; set; }

        [Column("IsActive")]
        public bool IsActive { get; set; }

        [Column("ForgotPasswordTimes")]
        public int ForgotPasswordTimes { get; set; }

        [Column("RoleId")]
        public Guid RoleId { get; set; }
        public Role Role { get; set; }
    }
}
