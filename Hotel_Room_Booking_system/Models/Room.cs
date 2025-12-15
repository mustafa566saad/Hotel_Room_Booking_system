namespace Hotel_Room_Booking_system.Models
{
    public class Room
    {
        [Key]
        public int RoomId { get; set; }

        [Range(1, 10)]
        public int Capacity { get; set; } = (int)CapacityRoom.Single;

        [Range(0, 100000)]
        public double BasePrice { get; set; }

        [StringLength(20)]
        public string Status { get; set; } = RoomStatus.Available.ToString();

        public string Description { get; set; }
        
        public ICollection<string>? ImagesURLs { get; set; }
        public ICollection<Booking>? Bookings { get; set; }
    }
}
