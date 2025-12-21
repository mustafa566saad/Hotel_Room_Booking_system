namespace Hotel_Room_Booking_system.DTOs
{
    public class ReviewDTO
    {
        public int RoomId { get; set; }
        public string UserId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
    }
}
