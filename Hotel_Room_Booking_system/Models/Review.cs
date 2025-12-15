using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel_Room_Booking_system.Models
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }

        [ForeignKey("Booking")]
        public int BookingId { get; set; }
        public Booking Booking { get; set; }


        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }


        [Range(1, 5)]
        public int Rating { get; set; }

        [StringLength(500)]
        public string? Comment { get; set; }

    }
}
