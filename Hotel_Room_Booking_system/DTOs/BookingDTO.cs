using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel_Room_Booking_system.DTOs
{
    public class BookingDTO
    {
        public string UserEmail { get; set; }
        public int RoomId { get; set; }

        [DataType(DataType.Date)]
        public DateTime CheckIn { get; set; }

        [DataType(DataType.Date)]
        public DateTime CheckOut { get; set; }

        public decimal TotalPrice { get; set; }

        [StringLength(20)]
        public string Status { get; set; } = BookingStatus.Pending.ToString();

        public string PaymentMethod { get; set; } = paymentMethod.Cash.ToString();
    }
}
