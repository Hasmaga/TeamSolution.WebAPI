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

        [Column("StatusId")]
        public Guid StatusId { get; set; }
        public Status Status { get; set; }

        [Column("Address")]
        public string Address { get; set; }

        [Column("Wallet")]
        public decimal Wallet { get; set; }

        [Column("IsDelete")]
        public bool IsDelete { get; set; }

        [Column("CreateDateTime")]
        public DateTime CreateDateTime { get; set; }

        [Column("UpdateDateTime")]
        public DateTime UpdateDateTime { get; set; }

        [Column("StoreId")]
        public Guid StoreId { get; set; }
        public Store Store { get; set; }

        public ICollection<FeedBack> FeedBacks { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<OrderDetail> OrderDetailsBegin { get; set; }
        public ICollection<OrderDetail> OrderDetailsDone { get; set; }
        public ICollection<ShipperDetail> ShipperDetails { get; set; }

        public User(string firstName, string lastName, string email, string passwordHash, string passwordSalt, string phoneNumber, bool isActive, int forgotPasswordTimes, Guid roleId, Guid statusId, string address, decimal wallet, bool isDelete, DateTime createDateTime, DateTime updateDateTime, Guid storeId)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
            PhoneNumber = phoneNumber;
            IsActive = isActive;
            ForgotPasswordTimes = forgotPasswordTimes;
            RoleId = roleId;
            StatusId = statusId;
            Address = address;
            Wallet = wallet;
            IsDelete = isDelete;
            CreateDateTime = createDateTime;
            UpdateDateTime = updateDateTime;
            StoreId = storeId;
        }
    }
}
