using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel_Room_Booking_system.Migrations
{
    /// <inheritdoc />
    public partial class hotelV13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Bookings_BookingId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_BookingId",
                table: "Reviews");

            migrationBuilder.RenameColumn(
                name: "BookingId",
                table: "Reviews",
                newName: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_RoomId",
                table: "Reviews",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Rooms_RoomId",
                table: "Reviews",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "RoomId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Rooms_RoomId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_RoomId",
                table: "Reviews");

            migrationBuilder.RenameColumn(
                name: "RoomId",
                table: "Reviews",
                newName: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_BookingId",
                table: "Reviews",
                column: "BookingId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Bookings_BookingId",
                table: "Reviews",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "BookingId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
