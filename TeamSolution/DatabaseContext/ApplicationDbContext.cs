using Microsoft.EntityFrameworkCore;
using TeamSolution.Model;

namespace TeamSolution.DatabaseContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Configure the relationship between each Table
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FeedBack>()
                .ToTable("FeedBacks");

            modelBuilder.Entity<Order>()
                .ToTable("Orders");

            modelBuilder.Entity<OrderDetail>()
                .ToTable("OrderDetails");

            modelBuilder.Entity<ShipperDetail>()
                .ToTable("ShipperDetails");

            modelBuilder.Entity<Status>()
                .ToTable("Statuses");

            modelBuilder.Entity<Store>()
                .ToTable("Stores");

            modelBuilder.Entity<StoreModeSetting>()
                .ToTable("StoreModeSetings");

            modelBuilder.Entity<Role>()
                .ToTable("Roles");

            modelBuilder.Entity<User>()
                .ToTable("Users");

            modelBuilder.Entity<FeedBack>()
                .HasKey(f => f.Id);
            
            modelBuilder.Entity<Order>()
                .HasKey(o => o.Id);
            
            modelBuilder.Entity<OrderDetail>()
                .HasKey(od => od.Id);
            
            modelBuilder.Entity<ShipperDetail>()
                .HasKey(sd => sd.Id);
            
            modelBuilder.Entity<Status>()
                .HasKey(s => s.Id);
            
            modelBuilder.Entity<Store>()
                .HasKey(s => s.Id);
            
            modelBuilder.Entity<StoreModeSetting>()
                .HasKey(sms => sms.Id);
            
            modelBuilder.Entity<Role>()
                .HasKey(r => r.Id);
            
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);
            
            modelBuilder.Entity<FeedBack>()
                .HasOne(f => f.User)
                .WithMany(u => u.FeedBacks)
                .HasForeignKey(f => f.UserId);

            modelBuilder.Entity<FeedBack>()
                .HasOne(f => f.Store)
                .WithMany(s => s.FeedBacks)
                .HasForeignKey(f => f.StoreId);
            
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.OrderDetail)
                .WithMany(od => od.Orders)
                .HasForeignKey(od => od.OrderDetailId);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Status)
                .WithMany(s => s.OrderDetails)
                .HasForeignKey(od => od.StatusId)
                .OnDelete(DeleteBehavior.NoAction);
            
            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Store)
                .WithMany(s => s.OrderDetails)
                .HasForeignKey(od => od.StoreId)
                .OnDelete(DeleteBehavior.NoAction);
            
            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.StoreModeSeting)
                .WithMany(sms => sms.OrderDetails)
                .HasForeignKey(od => od.StoreModeSettingId)
                .OnDelete(DeleteBehavior.NoAction);
            
            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.ShipperDetail)
                .WithMany(sd => sd.OrderDetails)
                .HasForeignKey(od => od.ShipperDetailId)
                .OnDelete(DeleteBehavior.NoAction);
            
            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.StaffBegin)
                .WithMany(u => u.OrderDetailsBegin)
                .HasForeignKey(od => od.StaffBeginId)
                .OnDelete(DeleteBehavior.NoAction);
            
            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.StaffDone)
                .WithMany(u => u.OrderDetailsDone)
                .HasForeignKey(od => od.StaffDoneId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Status)
                .WithMany(s => s.Users)
                .HasForeignKey(u => u.StatusId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Status>()
                .HasMany(s => s.Users)
                .WithOne(u => u.Status)
                .HasForeignKey(u => u.StatusId);

            modelBuilder.Entity<User>()
                .HasOne(s => s.Store)
                .WithMany(u => u.Users)
                .HasForeignKey(s => s.StoreId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Store>()
                .HasOne(s => s.StoreModeSeting)
                .WithMany(s => s.Stores)
                .HasForeignKey(s => s.StoreModeSettingId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ShipperDetail>()
                .HasOne(sd => sd.Status)
                .WithMany(s => s.ShipperDetails)
                .HasForeignKey(sd => sd.StatusId);                

            modelBuilder.Entity<ShipperDetail>()
                .HasOne(sd => sd.User)
                .WithMany(u => u.ShipperDetails)
                .HasForeignKey(sd => sd.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }

        public DbSet<FeedBack> FeedBacks { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<ShipperDetail> ShipperDetails { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<StoreModeSetting> StoreModeSetings { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
