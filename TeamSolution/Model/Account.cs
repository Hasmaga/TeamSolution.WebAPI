using System.ComponentModel.DataAnnotations.Schema;
using TeamSolution.Model.Abstract;

namespace TeamSolution.Model
{
    [Table("Account", Schema ="dbo")]
    public class Account : Common
    {
        // Table Column
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
        public string? PhoneNumber { get; set; } = null;

        [Column("IsActive")]
        public bool IsActive { get; set; } = true;

        [Column("ForgotPasswordTimes")]
        public int ForgotPasswordTimes { get; set; } = 0;
        
        [Column("RoleId")]
        public Guid RoleId { get; set; }              

        [Column("StatusId")]
        public Guid? StatusId { get; set; } = null;        

        [Column("Address")]
        public string? Address { get; set; } = null;

        [Column("Wallet")]
        public decimal? Wallet { get; set; } = null;

        [Column("IsDelete")]
        public bool IsDelete { get; set; } = false;

        [Column("CreateDateTime")]
        public DateTime CreateDateTime { get; set; }

        [Column("UpdateDateTime")]
        public DateTime? UpdateDateTime { get; set; } = null;        

        [Column("ShipperPerformance")]
        public int? ShipperPerformance { get; set; } = null;

        [Column("ShipperAvalability")]
        public bool? ShipperAvalability { get; set; } = null;

        [Column("DeleteDateTime")]
        public DateTime? DeleteDateTime { get; set; } = null;

        [Column("StoreId")]
        public Guid? StoreId { get; set; } = null;

        [Column("OtpCode")]
        public string? OtpCode { get; set; } = null;

        [Column("OtpCodeCreated")]
        public DateTime? OtpCodeCreated { get; set; } = null;

        [Column("OtpCodeExpired")]
        public DateTime? OtpCodeExpired { get; set; } = null;

        // Relationship
        public Role Role { get; set; }        
        public Store Store { get; set;} 
        public Status Status { get; set; }
        public ICollection<Issue> AccountFixIssues { get; set; }
        public ICollection<Issue> AccountIssues { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<FeedBack> FeedBacks { get; set; }
        public ICollection<TourShipper> TourShippersManager { get; set; }
        public ICollection<TourShipper> TourShippers { get; set; }


        // Constructor
        public Account()
        {
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.                    
        public Account(
            string firstName, string lastName, string email, string passwordHash, string passwordSalt,
            string? phoneNumber, bool isActive, int forgotPasswordTimes, Guid roleId, Guid? statusId,
            string? address, decimal? wallet, bool isDelete, DateTime createDateTime, DateTime? updateDateTime,
            int? shipperPerformance, bool? shipperAvalability, DateTime? deleteDateTime, Guid? storeId,
            string? otpCode, DateTime? otpCodeExpired, DateTime? otpCodeCreated)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
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
            ShipperPerformance = shipperPerformance;
            ShipperAvalability = shipperAvalability;
            DeleteDateTime = deleteDateTime;
            StoreId = storeId;
            OtpCode = otpCode;
            OtpCodeExpired = otpCodeExpired;
            OtpCodeCreated = otpCodeCreated;
        }
    }
}
