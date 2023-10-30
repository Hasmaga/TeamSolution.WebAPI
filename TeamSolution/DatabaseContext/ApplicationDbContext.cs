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
            
            // 1 Role have many Account
            modelBuilder.Entity<Role>()
                .HasMany(r => r.Accounts)
                .WithOne(a => a.Role)
                .HasForeignKey(a => a.RoleId);

            // 1 Customer have many Issue
            modelBuilder.Entity<Account>()
                .HasMany(a => a.AccountIssues)
                .WithOne(i => i.AccountIssue)
                .HasForeignKey(i => i.AccountIssueId)
                .OnDelete(DeleteBehavior.Restrict);

            // 1 Manager can fix many Issue
            modelBuilder.Entity<Account>()
                .HasMany(a => a.AccountFixIssues)
                .WithOne(i => i.AccountFixIssue)
                .HasForeignKey(i => i.AccountFixIssueId)
                .OnDelete(DeleteBehavior.Restrict);

            // 1 Customer have many Order
            modelBuilder.Entity<Account>()
                .HasMany(a => a.Orders)
                .WithOne(o => o.Customer)
                .HasForeignKey(o => o.CustomerId);

            // 1 Customer have many FeedBack
            modelBuilder.Entity<Account>()
                .HasMany(a => a.FeedBacks)
                .WithOne(f => f.Customer)
                .HasForeignKey(f => f.CustomerId);

            // 1 Tour have many ShipperReportIssueTour
            modelBuilder.Entity<TourShipper>()
                .HasMany(t => t.ShipperReportIssueTours)
                .WithOne(srit => srit.Tour)
                .HasForeignKey(srit => srit.TourId);         

            // 1 Shipper have many TourShipper
            modelBuilder.Entity<Account>()
                .HasMany(a => a.TourShippers)
                .WithOne(ts => ts.Shipper)
                .HasForeignKey(ts => ts.ShipperId)
                .OnDelete(DeleteBehavior.Restrict);

            // 1 ShipperManager have many TourShipper
            modelBuilder.Entity<Account>()
                .HasMany(a => a.TourShippersManager)
                .WithOne(ts => ts.ShipperManager)
                .HasForeignKey(ts => ts.ShipperManagerId)
                .OnDelete(DeleteBehavior.Restrict);

            // 1 Order have many OrderDetail
            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderDetails)
                .WithOne(od => od.Order)
                .HasForeignKey(od => od.OrderId);

            // 1 Order have many CustomerComplainStore
            modelBuilder.Entity<Order>()
                .HasMany(o => o.CustomerComplainOrders)
                .WithOne(ccs => ccs.Order)
                .HasForeignKey(ccs => ccs.OrderId);           

            // 1 Store have many Order
            modelBuilder.Entity<Store>()
                .HasMany(s => s.Orders)
                .WithOne(o => o.Store)
                .HasForeignKey(o => o.StoreId)
                .OnDelete(DeleteBehavior.Restrict);

            // 1 Store have many StoreService
            modelBuilder.Entity<Store>()
                .HasMany(s => s.StoreServices)
                .WithOne(ss => ss.Store)
                .HasForeignKey(ss => ss.StoreId);

            // 1 Store have many FeedBack by Customer
            modelBuilder.Entity<Store>()
                .HasMany(s => s.FeedBacks)
                .WithOne(f => f.Store)
                .HasForeignKey(f => f.StoreId)
                .OnDelete(DeleteBehavior.Restrict);

            // 1 Store have 1 StoreManagerAccount
            modelBuilder.Entity<Store>()
                .HasOne(s => s.StoreManager)
                .WithOne(a => a.Store)
                .HasForeignKey<Store>(s => s.StoreManagerId);

            // 1 StoreService have many OrderDetail
            modelBuilder.Entity<StoreService>()
                .HasMany(ss => ss.OrderDetails)
                .WithOne(od => od.StoreService)
                .HasForeignKey(od => od.StoreServiceId)
                .OnDelete(DeleteBehavior.Restrict);

            // 1 Status have many Order
            modelBuilder.Entity<Status>()
                .HasMany(s => s.Orders)
                .WithOne(o => o.StatusOrder)
                .HasForeignKey(o => o.StatusOrderId)
                .OnDelete(DeleteBehavior.Restrict);

            // 1 Status have many Account
            modelBuilder.Entity<Status>()
                .HasMany(s => s.Accounts)
                .WithOne(a => a.Status)
                .HasForeignKey(a => a.StatusId);            

            // 1 Status have many Order
            modelBuilder.Entity<Status>()
                .HasMany(s => s.Orders)
                .WithOne(o => o.StatusOrder)
                .HasForeignKey(o => o.StatusOrderId);

            // Config wallet of Account
            modelBuilder.Entity<Account>()
                .Property(a => a.Wallet)
                .HasColumnType("decimal(18,2)");

            // Cogfig price of OrderDetail
            modelBuilder.Entity<OrderDetail>()
                .Property(od => od.Price)
                .HasColumnType("decimal(18,2)");

            // Config weight of OrderDetail
            modelBuilder.Entity<OrderDetail>()
                .Property(od => od.Weight)
                .HasColumnType("decimal(18,2)");

            // Config StoreRating of Store
            modelBuilder.Entity<Store>()
                .Property(s => s.StoreRating)
                .HasColumnType("decimal(18,2)");

            // Cofig ServicePrice in StoreService
            modelBuilder.Entity<StoreService>()
                .Property(ss => ss.ServicePrice)
                .HasColumnType("decimal(18,2)");

            // Cofig TourShipOrder in TourShipper
            modelBuilder.Entity<TourShipper>()
                .HasMany(ts => ts.TourShipOrders)
                .WithOne(o => o.TourShipOrder)
                .HasForeignKey(o => o.TourShipOrderId);

            // Cofig TourGetOrder in TourShipper
            modelBuilder.Entity<TourShipper>()
                .HasMany(ts => ts.TourGetOrders)
                .WithOne(o => o.TourGetOrder)
                .HasForeignKey(o => o.TourGetOrderId);
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<CustomerComplainOrder> CustomerComplainStores { get; set; }
        public DbSet<FeedBack> FeedBacks { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<ShipperReportIssueTour> ShipperReportIssueTours { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<StoreService> StoreServices { get; set; }
        public DbSet<TourShipper> TourShippers { get; set; }        
    }
}
