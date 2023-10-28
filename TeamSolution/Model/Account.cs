using System.ComponentModel.DataAnnotations.Schema;
using TeamSolution.Model.Abstract;

namespace TeamSolution.Model
{
    [Table("Account", Schema ="dbo")]
    public class Account : Common
    {
        // Table Column
        [Column("FirstName")]
        public string FirstName { get; set; } = null!;

        [Column("LastName")]
        public string LastName { get; set; } = null!;

        [Column("Email")]
        public string Email { get; set; } = null!;

        [Column("PasswordHash")]
        public string PasswordHash { get; set; } = null!;

        [Column("PasswordSalt")]
        public string PasswordSalt { get; set; } = null!;

        [Column("PhoneNumber")]
        public string? PhoneNumber { get; set; } = null;

        [Column("IsActive")]
        public bool IsActive { get; set; } = true;

        [Column("ForgotPasswordTimes")]
        public int ForgotPasswordTimes { get; set; } = 0;

        [Column("RoleId")]
        public Guid RoleId { get; set; }    
        public Role Role { get; set; } = null!;

        [Column("StatusId")]
        public Guid StatusId { get; set; }         
        public Status Status { get; set; } = null!;

        [Column("Address")]
        public string? Address { get; set; } = null;

        [Column("Wallet")]
        public decimal Wallet { get; set; } = 0;        

        [Column("ShipperPerformance")]
        public int? ShipperPerformance { get; set; } = null;

        [Column("ShipperAvalability")]
        public bool? ShipperAvalability { get; set; } = null;        

        [Column("StoreId")]
        public Guid? StoreId { get; set; } = null;
        public Store? Store { get; set; }

        [Column("OtpCode")]
        public string? OtpCode { get; set; } = null;

        [Column("OtpCodeCreated")]
        public DateTime? OtpCodeCreated { get; set; } = null;

        [Column("OtpCodeExpired")]
        public DateTime? OtpCodeExpired { get; set; } = null;

        public ICollection<Issue>? AccountFixIssues { get; set; } 
        public ICollection<Issue>? AccountIssues { get; set; } 
        public ICollection<Order>? Orders { get; set; } 
        public ICollection<FeedBack>? FeedBacks { get; set; }
        public ICollection<TourShipper>? TourShippersManager { get; set; }
        public ICollection<TourShipper>? TourShippers { get; set; }       
    }
}
