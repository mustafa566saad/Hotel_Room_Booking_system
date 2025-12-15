using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel_Room_Booking_system.Migrations
{
    /// <inheritdoc />
    public partial class hotel_V11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Rooms");

            migrationBuilder.AlterColumn<double>(
                name: "BasePrice",
                table: "Rooms",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "BasePrice",
                table: "Rooms",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<double>(
                name: "TotalPrice",
                table: "Rooms",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
