﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TeamSolution.DatabaseContext;

#nullable disable

namespace TeamSolution.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231014103840_v1.1")]
    partial class v11
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TeamSolution.Model.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Address");

                    b.Property<DateTime>("CreateDateTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("CreateDateTime");

                    b.Property<DateTime?>("DeleteDateTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("DeleteDateTime");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("FirstName");

                    b.Property<int>("ForgotPasswordTimes")
                        .HasColumnType("int")
                        .HasColumnName("ForgotPasswordTimes");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit")
                        .HasColumnName("IsActive");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit")
                        .HasColumnName("IsDelete");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("LastName");

                    b.Property<string>("OtpCode")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("OtpCode");

                    b.Property<DateTime?>("OtpCodeCreated")
                        .HasColumnType("datetime2")
                        .HasColumnName("OtpCodeCreated");

                    b.Property<DateTime?>("OtpCodeExpired")
                        .HasColumnType("datetime2")
                        .HasColumnName("OtpCodeExpired");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("PasswordHash");

                    b.Property<string>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("PasswordSalt");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("PhoneNumber");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("RoleId");

                    b.Property<bool?>("ShipperAvalability")
                        .HasColumnType("bit")
                        .HasColumnName("ShipperAvalability");

                    b.Property<int?>("ShipperPerformance")
                        .HasColumnType("int")
                        .HasColumnName("ShipperPerformance");

                    b.Property<Guid?>("StatusId")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("StatusId");

                    b.Property<Guid?>("StoreId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("StoreId");

                    b.Property<DateTime?>("UpdateDateTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("UpdateDateTime");

                    b.Property<decimal?>("Wallet")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("Wallet");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("StatusId");

                    b.ToTable("Account", "dbo");
                });

            modelBuilder.Entity("TeamSolution.Model.CustomerComplainOrder", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<DateTime>("CreateDateTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("CreateDateTime");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Description");

                    b.Property<bool>("IsClose")
                        .HasColumnType("bit")
                        .HasColumnName("IsClose");

                    b.Property<DateTime?>("IsCloseDateTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("IsCloseDateTime");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit")
                        .HasColumnName("IsDelete");

                    b.Property<string>("MainComplain")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("MainComplain");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("OrderId");

                    b.Property<DateTime?>("UpdateDateTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("UpdateDateTime");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("CustomerComplainOrder");
                });

            modelBuilder.Entity("TeamSolution.Model.FeedBack", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Comment");

                    b.Property<DateTime>("CreateDateTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("CreateDateTime");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("CustomerId");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("IsDeleted");

                    b.Property<int>("Rating")
                        .HasColumnType("int")
                        .HasColumnName("Rating");

                    b.Property<Guid>("StoreId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("StoreId");

                    b.Property<DateTime?>("UpdateDatetime")
                        .HasColumnType("datetime2")
                        .HasColumnName("UpdateDatetime");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("StoreId");

                    b.ToTable("FeedBack", "dbo");
                });

            modelBuilder.Entity("TeamSolution.Model.Issue", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<Guid>("AccountFixIssueId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("AccountFixIssueId");

                    b.Property<Guid>("AccountIssueId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("AccountIssueId");

                    b.Property<DateTime>("CreateDateTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("CreateDateTime");

                    b.Property<DateTime?>("DeleteDateTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("DeleteDateTime");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Description");

                    b.Property<bool>("IsClose")
                        .HasColumnType("bit")
                        .HasColumnName("IsClose");

                    b.Property<DateTime?>("IsCloseDateTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("IsCloseDateTime");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit")
                        .HasColumnName("IsDelete");

                    b.Property<string>("IssueTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("IssueTitle");

                    b.Property<string>("MainIssus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("MainIssus");

                    b.Property<DateTime?>("UpdateDateTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("UpdateDateTime");

                    b.HasKey("Id");

                    b.HasIndex("AccountFixIssueId");

                    b.HasIndex("AccountIssueId");

                    b.ToTable("Issue");
                });

            modelBuilder.Entity("TeamSolution.Model.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<DateTime>("CreateDateTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("CreateDateTime");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("CustomerId");

                    b.Property<DateTime?>("DeleteDateTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("DeleteDateTime");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit")
                        .HasColumnName("IsDelete");

                    b.Property<string>("OrderAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("OrderAddress");

                    b.Property<string>("PaymentMethod")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("PaymentMethod");

                    b.Property<string>("PhoneCustomer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("PhoneCustomer");

                    b.Property<Guid>("StatusOrderId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("StatusOrderId");

                    b.Property<Guid>("StoreId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("StoreId");

                    b.Property<DateTime>("TimeDeliverOrder")
                        .HasColumnType("datetime2")
                        .HasColumnName("TimeDeliverOrder");

                    b.Property<DateTime?>("TimeShipperDeliverOrder")
                        .HasColumnType("datetime2")
                        .HasColumnName("TimeShipperDeliverOrder");

                    b.Property<DateTime?>("TimeShipperTakeOrder")
                        .HasColumnType("datetime2")
                        .HasColumnName("TimeShipperTakeOrder");

                    b.Property<DateTime>("TimeTakeOrder")
                        .HasColumnType("datetime2")
                        .HasColumnName("TimeTakeOrder");

                    b.Property<Guid?>("TourShipperId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("TourShipperId");

                    b.Property<DateTime?>("UpdateDateTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("UpdateDateTime");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("StatusOrderId");

                    b.HasIndex("StoreId");

                    b.HasIndex("TourShipperId");

                    b.ToTable("Order", "dbo");
                });

            modelBuilder.Entity("TeamSolution.Model.OrderDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("OrderId");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("Price");

                    b.Property<Guid>("StoreServiceId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("StoreServiceId");

                    b.Property<decimal>("Weight")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("Weight");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("StoreServiceId");

                    b.ToTable("OrderDetail", "dbo");
                });

            modelBuilder.Entity("TeamSolution.Model.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<DateTime>("CreateDateTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("CreateDateTime");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("RoleName");

                    b.Property<DateTime?>("UpdateDateTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("UpdateDateTime");

                    b.HasKey("Id");

                    b.ToTable("Role", "dbo");
                });

            modelBuilder.Entity("TeamSolution.Model.ShipperReportIssueTour", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<DateTime>("CreateDateTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("CreateDateTime");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Description");

                    b.Property<bool>("IsClose")
                        .HasColumnType("bit")
                        .HasColumnName("IsClose");

                    b.Property<DateTime?>("IsCloseDateTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("IsCloseDateTime");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit")
                        .HasColumnName("IsDelete");

                    b.Property<string>("MainIssue")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("MainIssue");

                    b.Property<Guid>("TourId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("TourId");

                    b.Property<DateTime?>("UpdateDateTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("UpdateDateTime");

                    b.HasKey("Id");

                    b.HasIndex("TourId");

                    b.ToTable("ShipperReportIssueTour");
                });

            modelBuilder.Entity("TeamSolution.Model.Status", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Description");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("IsDeleted");

                    b.Property<string>("StatusName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("StatusName");

                    b.HasKey("Id");

                    b.ToTable("Status", "dbo");
                });

            modelBuilder.Entity("TeamSolution.Model.Store", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Address");

                    b.Property<DateTime>("CreateDateTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("CreateDateTime");

                    b.Property<DateTime?>("DeleteDateTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("DeleteDateTime");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit")
                        .HasColumnName("IsDelete");

                    b.Property<string>("OperationTime")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("OperationTime");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Phone");

                    b.Property<bool>("StoreAvalability")
                        .HasColumnType("bit")
                        .HasColumnName("StoreAvalability");

                    b.Property<string>("StoreDescription")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("StoreDescription");

                    b.Property<string>("StoreImage")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("StoreImage");

                    b.Property<Guid>("StoreManagerId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("StoreManagerId");

                    b.Property<string>("StoreName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("StoreName");

                    b.Property<decimal?>("StoreRating")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("StoreRating");

                    b.HasKey("Id");

                    b.HasIndex("StoreManagerId")
                        .IsUnique();

                    b.ToTable("Store", "dbo");
                });

            modelBuilder.Entity("TeamSolution.Model.StoreService", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<DateTime>("CreateDateTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("CreateDateTime");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit")
                        .HasColumnName("IsAvailable");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit")
                        .HasColumnName("IsDelete");

                    b.Property<string>("ServiceDescription")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ServiceDescription");

                    b.Property<int?>("ServiceDuration")
                        .HasColumnType("int")
                        .HasColumnName("ServiceDuration");

                    b.Property<decimal>("ServicePrice")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("ServicePrice");

                    b.Property<string>("ServiceType")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ServiceType");

                    b.Property<Guid>("StoreId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("StoreId");

                    b.Property<DateTime>("UpdateDateTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("UpdateDateTime");

                    b.HasKey("Id");

                    b.HasIndex("StoreId");

                    b.ToTable("StoreService", "dbo");
                });

            modelBuilder.Entity("TeamSolution.Model.TourShipper", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<DateTime>("CreateDateTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("CreateDateTime");

                    b.Property<DateTime?>("DeleteDateTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("DeleteDateTime");

                    b.Property<string>("DeliverOrGet")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("DeliverOrGet");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit")
                        .HasColumnName("IsDelete");

                    b.Property<Guid>("ShipperId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ShipperId");

                    b.Property<Guid>("ShipperManagerId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ShipperManagerId");

                    b.Property<Guid>("StatusId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("StatusId");

                    b.Property<DateTime?>("UpdateDateTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("UpdateDateTime");

                    b.HasKey("Id");

                    b.HasIndex("ShipperId");

                    b.HasIndex("ShipperManagerId");

                    b.HasIndex("StatusId");

                    b.ToTable("TourShipper");
                });

            modelBuilder.Entity("TeamSolution.Model.Account", b =>
                {
                    b.HasOne("TeamSolution.Model.Role", "Role")
                        .WithMany("Accounts")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TeamSolution.Model.Status", "Status")
                        .WithMany("Accounts")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("TeamSolution.Model.CustomerComplainOrder", b =>
                {
                    b.HasOne("TeamSolution.Model.Order", "Order")
                        .WithMany("CustomerComplainOrders")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("TeamSolution.Model.FeedBack", b =>
                {
                    b.HasOne("TeamSolution.Model.Account", "Customer")
                        .WithMany("FeedBacks")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TeamSolution.Model.Store", "Store")
                        .WithMany("FeedBacks")
                        .HasForeignKey("StoreId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Store");
                });

            modelBuilder.Entity("TeamSolution.Model.Issue", b =>
                {
                    b.HasOne("TeamSolution.Model.Account", "AccountFixIssue")
                        .WithMany("AccountFixIssues")
                        .HasForeignKey("AccountFixIssueId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TeamSolution.Model.Account", "AccountIssue")
                        .WithMany("AccountIssues")
                        .HasForeignKey("AccountIssueId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("AccountFixIssue");

                    b.Navigation("AccountIssue");
                });

            modelBuilder.Entity("TeamSolution.Model.Order", b =>
                {
                    b.HasOne("TeamSolution.Model.Account", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TeamSolution.Model.Status", "StatusOrder")
                        .WithMany("Orders")
                        .HasForeignKey("StatusOrderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TeamSolution.Model.Store", "Store")
                        .WithMany("Orders")
                        .HasForeignKey("StoreId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TeamSolution.Model.TourShipper", "TourShipper")
                        .WithMany()
                        .HasForeignKey("TourShipperId");

                    b.Navigation("Customer");

                    b.Navigation("StatusOrder");

                    b.Navigation("Store");

                    b.Navigation("TourShipper");
                });

            modelBuilder.Entity("TeamSolution.Model.OrderDetail", b =>
                {
                    b.HasOne("TeamSolution.Model.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TeamSolution.Model.StoreService", "StoreService")
                        .WithMany("OrderDetails")
                        .HasForeignKey("StoreServiceId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("StoreService");
                });

            modelBuilder.Entity("TeamSolution.Model.ShipperReportIssueTour", b =>
                {
                    b.HasOne("TeamSolution.Model.TourShipper", "Tour")
                        .WithMany("ShipperReportIssueTours")
                        .HasForeignKey("TourId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tour");
                });

            modelBuilder.Entity("TeamSolution.Model.Store", b =>
                {
                    b.HasOne("TeamSolution.Model.Account", "StoreManager")
                        .WithOne("Store")
                        .HasForeignKey("TeamSolution.Model.Store", "StoreManagerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("StoreManager");
                });

            modelBuilder.Entity("TeamSolution.Model.StoreService", b =>
                {
                    b.HasOne("TeamSolution.Model.Store", "Store")
                        .WithMany("StoreServices")
                        .HasForeignKey("StoreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Store");
                });

            modelBuilder.Entity("TeamSolution.Model.TourShipper", b =>
                {
                    b.HasOne("TeamSolution.Model.Account", "Shipper")
                        .WithMany("TourShippers")
                        .HasForeignKey("ShipperId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TeamSolution.Model.Account", "ShipperManager")
                        .WithMany("TourShippersManager")
                        .HasForeignKey("ShipperManagerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TeamSolution.Model.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Shipper");

                    b.Navigation("ShipperManager");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("TeamSolution.Model.Account", b =>
                {
                    b.Navigation("AccountFixIssues");

                    b.Navigation("AccountIssues");

                    b.Navigation("FeedBacks");

                    b.Navigation("Orders");

                    b.Navigation("Store")
                        .IsRequired();

                    b.Navigation("TourShippers");

                    b.Navigation("TourShippersManager");
                });

            modelBuilder.Entity("TeamSolution.Model.Order", b =>
                {
                    b.Navigation("CustomerComplainOrders");

                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("TeamSolution.Model.Role", b =>
                {
                    b.Navigation("Accounts");
                });

            modelBuilder.Entity("TeamSolution.Model.Status", b =>
                {
                    b.Navigation("Accounts");

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("TeamSolution.Model.Store", b =>
                {
                    b.Navigation("FeedBacks");

                    b.Navigation("Orders");

                    b.Navigation("StoreServices");
                });

            modelBuilder.Entity("TeamSolution.Model.StoreService", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("TeamSolution.Model.TourShipper", b =>
                {
                    b.Navigation("ShipperReportIssueTours");
                });
#pragma warning restore 612, 618
        }
    }
}