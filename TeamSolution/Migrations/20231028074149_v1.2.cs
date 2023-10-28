using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamSolution.Migrations
{
    /// <inheritdoc />
    public partial class v12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_TourShipper_TourShipperId",
                schema: "dbo",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "TourShipper");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                schema: "dbo",
                table: "StoreService");

            migrationBuilder.DropColumn(
                name: "IsClose",
                table: "ShipperReportIssueTour");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "ShipperReportIssueTour");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                schema: "dbo",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Issue");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "dbo",
                table: "FeedBack");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "CustomerComplainOrder");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                schema: "dbo",
                table: "Account");

            migrationBuilder.RenameColumn(
                name: "DeleteDateTime",
                table: "TourShipper",
                newName: "DeleteDataTime");

            migrationBuilder.RenameColumn(
                name: "DeleteDateTime",
                schema: "dbo",
                table: "Store",
                newName: "DeleteDataTime");

            migrationBuilder.RenameColumn(
                name: "IsCloseDateTime",
                table: "ShipperReportIssueTour",
                newName: "DeleteDataTime");

            migrationBuilder.RenameColumn(
                name: "DeleteDateTime",
                schema: "dbo",
                table: "Order",
                newName: "DeleteDataTime");

            migrationBuilder.RenameColumn(
                name: "TourShipperId",
                schema: "dbo",
                table: "Order",
                newName: "TourShipOrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_TourShipperId",
                schema: "dbo",
                table: "Order",
                newName: "IX_Order_TourShipOrderId");

            migrationBuilder.RenameColumn(
                name: "DeleteDateTime",
                table: "Issue",
                newName: "DeleteDataTime");

            migrationBuilder.RenameColumn(
                name: "UpdateDatetime",
                schema: "dbo",
                table: "FeedBack",
                newName: "UpdateDateTime");

            migrationBuilder.RenameColumn(
                name: "DeleteDateTime",
                schema: "dbo",
                table: "Account",
                newName: "DeleteDataTime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateDateTime",
                schema: "dbo",
                table: "StoreService",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteDataTime",
                schema: "dbo",
                table: "StoreService",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDateTime",
                schema: "dbo",
                table: "Store",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDateTime",
                schema: "dbo",
                table: "Status",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteDataTime",
                schema: "dbo",
                table: "Status",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDateTime",
                schema: "dbo",
                table: "Status",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CloseDateTime",
                table: "ShipperReportIssueTour",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteDataTime",
                schema: "dbo",
                table: "Role",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDateTime",
                schema: "dbo",
                table: "OrderDetail",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteDataTime",
                schema: "dbo",
                table: "OrderDetail",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDateTime",
                schema: "dbo",
                table: "OrderDetail",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TourGetOrderId",
                schema: "dbo",
                table: "Order",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteDataTime",
                schema: "dbo",
                table: "FeedBack",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteDataTime",
                table: "CustomerComplainOrder",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Wallet",
                schema: "dbo",
                table: "Account",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Order_TourGetOrderId",
                schema: "dbo",
                table: "Order",
                column: "TourGetOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_TourShipper_TourGetOrderId",
                schema: "dbo",
                table: "Order",
                column: "TourGetOrderId",
                principalTable: "TourShipper",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_TourShipper_TourShipOrderId",
                schema: "dbo",
                table: "Order",
                column: "TourShipOrderId",
                principalTable: "TourShipper",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_TourShipper_TourGetOrderId",
                schema: "dbo",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_TourShipper_TourShipOrderId",
                schema: "dbo",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_TourGetOrderId",
                schema: "dbo",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "DeleteDataTime",
                schema: "dbo",
                table: "StoreService");

            migrationBuilder.DropColumn(
                name: "UpdateDateTime",
                schema: "dbo",
                table: "Store");

            migrationBuilder.DropColumn(
                name: "CreateDateTime",
                schema: "dbo",
                table: "Status");

            migrationBuilder.DropColumn(
                name: "DeleteDataTime",
                schema: "dbo",
                table: "Status");

            migrationBuilder.DropColumn(
                name: "UpdateDateTime",
                schema: "dbo",
                table: "Status");

            migrationBuilder.DropColumn(
                name: "CloseDateTime",
                table: "ShipperReportIssueTour");

            migrationBuilder.DropColumn(
                name: "DeleteDataTime",
                schema: "dbo",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "CreateDateTime",
                schema: "dbo",
                table: "OrderDetail");

            migrationBuilder.DropColumn(
                name: "DeleteDataTime",
                schema: "dbo",
                table: "OrderDetail");

            migrationBuilder.DropColumn(
                name: "UpdateDateTime",
                schema: "dbo",
                table: "OrderDetail");

            migrationBuilder.DropColumn(
                name: "TourGetOrderId",
                schema: "dbo",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "DeleteDataTime",
                schema: "dbo",
                table: "FeedBack");

            migrationBuilder.DropColumn(
                name: "DeleteDataTime",
                table: "CustomerComplainOrder");

            migrationBuilder.RenameColumn(
                name: "DeleteDataTime",
                table: "TourShipper",
                newName: "DeleteDateTime");

            migrationBuilder.RenameColumn(
                name: "DeleteDataTime",
                schema: "dbo",
                table: "Store",
                newName: "DeleteDateTime");

            migrationBuilder.RenameColumn(
                name: "DeleteDataTime",
                table: "ShipperReportIssueTour",
                newName: "IsCloseDateTime");

            migrationBuilder.RenameColumn(
                name: "DeleteDataTime",
                schema: "dbo",
                table: "Order",
                newName: "DeleteDateTime");

            migrationBuilder.RenameColumn(
                name: "TourShipOrderId",
                schema: "dbo",
                table: "Order",
                newName: "TourShipperId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_TourShipOrderId",
                schema: "dbo",
                table: "Order",
                newName: "IX_Order_TourShipperId");

            migrationBuilder.RenameColumn(
                name: "DeleteDataTime",
                table: "Issue",
                newName: "DeleteDateTime");

            migrationBuilder.RenameColumn(
                name: "UpdateDateTime",
                schema: "dbo",
                table: "FeedBack",
                newName: "UpdateDatetime");

            migrationBuilder.RenameColumn(
                name: "DeleteDataTime",
                schema: "dbo",
                table: "Account",
                newName: "DeleteDateTime");

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "TourShipper",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateDateTime",
                schema: "dbo",
                table: "StoreService",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                schema: "dbo",
                table: "StoreService",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsClose",
                table: "ShipperReportIssueTour",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "ShipperReportIssueTour",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                schema: "dbo",
                table: "Order",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Issue",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "dbo",
                table: "FeedBack",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "CustomerComplainOrder",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<decimal>(
                name: "Wallet",
                schema: "dbo",
                table: "Account",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                schema: "dbo",
                table: "Account",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_TourShipper_TourShipperId",
                schema: "dbo",
                table: "Order",
                column: "TourShipperId",
                principalTable: "TourShipper",
                principalColumn: "Id");
        }
    }
}
