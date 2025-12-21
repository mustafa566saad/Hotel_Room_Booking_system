using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Hotel_Room_Booking_system.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        [JsonIgnore]
        public ApplicationUser User { get; set; }


        [ForeignKey("Room")]
        public int RoomId { get; set; }
        [JsonIgnore]
        public Room Room { get; set; }


        [DataType(DataType.Date)]
        public DateTime CheckIn { get; set; }

        [DataType(DataType.Date)]
        public DateTime CheckOut { get; set; }

        public int Nights => (CheckOut - CheckIn).Days;

        public decimal TotalPrice { get; set; }

        [StringLength(20)]
        public string Status { get; set; } = BookingStatus.Pending.ToString();

        public string PaymentMethod { get; set; }
    }
}
