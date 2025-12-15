
namespace Hotel_Room_Booking_system.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string Roles { get; set; }= "User";

        [StringLength(200)]
        public string? Address { get; set; }
        public ICollection<Booking>? Bookings { get; set; }
        public ICollection<Review>? Reviews { get; set; }
    }
}
