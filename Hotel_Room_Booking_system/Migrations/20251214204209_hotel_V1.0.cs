using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Hotel_Room_Booking_system.Migrations
{
    /// <inheritdoc />
    public partial class hotel_V10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SeasonalPrices_Rooms_RoomId",
                table: "SeasonalPrices");

            migrationBuilder.DropTable(
                name: "RoomImages");

            migrationBuilder.DropIndex(
                name: "IX_SeasonalPrices_RoomId",
                table: "SeasonalPrices");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "SeasonalPrices");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "SeasonalPrices");

            migrationBuilder.DropColumn(
                name: "TotalPriced",
                table: "Rooms");

            migrationBuilder.RenameColumn(
                name: "SeasonalPriceId",
                table: "SeasonalPrices",
                newName: "Id");

            migrationBuilder.AlterColumn<decimal>(
                name: "IncreasePercentage",
                table: "SeasonalPrices",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Rooms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagesURLs",
                table: "Rooms",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TotalPrice",
                table: "Rooms",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.InsertData(
                table: "SeasonalPrices",
                columns: new[] { "Id", "EndDate", "IncreasePercentage", "StartDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 8, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.25m, new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(2026, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.30m, new DateTime(2025, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SeasonalPrices",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SeasonalPrices",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "ImagesURLs",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Rooms");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "SeasonalPrices",
                newName: "SeasonalPriceId");

            migrationBuilder.AlterColumn<double>(
                name: "IncreasePercentage",
                table: "SeasonalPrices",
                type: "float",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "SeasonalPrices",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "SeasonalPrices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Rooms",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "TotalPriced",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "RoomImages",
                columns: table => new
                {
                    ImageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomImages", x => x.ImageId);
                    table.ForeignKey(
                        name: "FK_RoomImages_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "RoomId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SeasonalPrices_RoomId",
                table: "SeasonalPrices",
                column: "RoomId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoomImages_RoomId",
                table: "RoomImages",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_SeasonalPrices_Rooms_RoomId",
                table: "SeasonalPrices",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "RoomId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
