namespace Hotel_Room_Booking_system.DTOs
{
    public class BookingDTO
    {
        [DataType(DataType.EmailAddress)]
        public string UserEmail { get; set; }
        public int RoomId { get; set; }

        [DataType(DataType.Date)]
        public DateTime CheckIn { get; set; }

        [DataType(DataType.Date)]
        public DateTime CheckOut { get; set; }

        public string PaymentMethod { get; set; }
    }
}
